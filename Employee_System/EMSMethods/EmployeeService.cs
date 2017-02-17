using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class EmployeeService
    {
        EmployeeEntities DbContext = new EmployeeEntities();


        //for Bind Country DropDown 
        public List<CountryItem> CountryList()
        {
            RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
            List<CountryItem> countryNames = new List<CountryItem>();

            //To get the Country Names from the CultureInfo installed in windows
            foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);
                countryNames.Add(new CountryItem() { CountryName = country.DisplayName, Id = country.DisplayName });
            }

            //Assigning all Country names to IEnumerable
            IEnumerable<CountryItem> nameAdded = countryNames.GroupBy(x => x.CountryName).Select(x => x.FirstOrDefault()).ToList<CountryItem>().OrderBy(x => x.CountryName);
            return nameAdded.ToList();
        }
        public string getEMP(int EmpID)
        {
            var EMP = (from EM in DbContext.employee_master
                       where EM.id == EmpID
                       select
                         EM.Empname + "," + EM.Photo
                       ).FirstOrDefault();

            return EMP;
        }
        public int Insert(EmpAllItem model)
        {
            try
            {

                Mapper.CreateMap<EmployeeItem, employee_master>();
                employee_master objEmp = Mapper.Map<employee_master>(model.EmployeeItem);
                DbContext.employee_master.Add(objEmp);
                DbContext.SaveChanges();

                int p = DbContext.employee_master.Max(item => item.id);
                if (p != 0)
                {
                    //For inserting values in EmployeeRelative table
                    //model.EmpRelativeItem.EmpId = p;
                    //model.EmpContactItem.EmpId = p;
                    //model.EmpPassportItem.EmpId = p;
                }

                //Mapper.CreateMap<EmpRelativeItem, EmployeeRelative>();
                //EmployeeRelative objRelative = Mapper.Map<EmployeeRelative>(model.EmpRelativeItem);
                //DbContext.EmployeeRelatives.Add(objRelative);
                //DbContext.SaveChanges();


                // For inserting values in EmployeeContact table
                //Mapper.CreateMap<EmpContactItem, EmployeeContact>();
                //EmployeeContact objEmpCont = Mapper.Map<EmployeeContact>(model.EmpContactItem);
                //DbContext.EmployeeContacts.Add(objEmpCont);
                //DbContext.SaveChanges();


                //// For inserting values in EmployeePassport Table
                //Mapper.CreateMap<EmpPassportItem, EmployeePassport>();
                //EmployeePassport objPassport = Mapper.Map<EmployeePassport>(model.EmpPassportItem);
                //DbContext.EmployeePassports.Add(objPassport);
                //DbContext.SaveChanges();


                return DbContext.employee_master.Max(item => item.id);




            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation(
                              "Class: {0}, Property: {1}, Error: {2}",
                              validationErrors.Entry.Entity.GetType().FullName,
                              validationError.PropertyName,
                              validationError.ErrorMessage);
                    }
                }
                return 1;
            }
        }

        public List<EmpAllItem> GetALLDetails(int FID, int cid)
        {
            //     Mapper.Initialize(cfg =>
            //    {
            //        cfg.CreateMap<EmployeeContact, EmpContactItem>();
            //        cfg.CreateMap<EmployeePassport, EmpPassportItem>();
            //        cfg.CreateMap<EmployeeRelative, EmpRelativeItem>();
            //    });
            //     List<EmpAllItem> a = new List<EmpAllItem>();
            //     return a ;
            EmpContactItem ContactItem = new EmpContactItem();
            EmpPassportItem PassportItem = new EmpPassportItem();
            List<EmployeeItem> a = new List<EmployeeItem>();


            var ab = (from Emp in DbContext.employee_master
                      join Contact in DbContext.EmployeeContacts on Emp.id equals Contact.EmpId into Contact1
                      from C in Contact1.DefaultIfEmpty()
                      join Passport in DbContext.EmployeePassports on Emp.id equals Passport.EmpId into Passport1
                      from P in Passport1.DefaultIfEmpty()
                      join Relative in DbContext.EmployeeRelatives on Emp.id equals Relative.EmpId into Relative1
                      from R in Relative1.DefaultIfEmpty()
                      join Company in DbContext.Company_master on Emp.Compid equals Company.id into Company1
                      from Companys in Company1.DefaultIfEmpty()
                      join Branch in DbContext.Branch_master on Emp.Branchid equals Branch.id into BranchMst
                      from Branchs in BranchMst.DefaultIfEmpty()
                      //where Emp.Compid == (Compid == 0 ? Emp.Compid : Compid)
                      where Emp.WorkingStatus == (FID == 0 ? Emp.WorkingStatus : FID) && Emp.Compid == (cid == 0 ? Emp.Compid : cid)
                      select new EmpAllItem
                      {
                          //EmpContactItem = new EmpContactItem()
                          //{
                          //    Area = C.Area,
                          //    City = C.City,
                          //},
                          EmployeeItem = new EmployeeItem()
                          {
                              Empno = Emp.Empno,
                              Empname = Emp.Empname,
                              Mobile = Emp.Mobile,
                              id = Emp.id,
                              JobTitle = Emp.JobTitle
                          },
                          //EmpPassportItem = new EmpPassportItem()
                          //{
                          //    PassportNo = P.PassportNo

                          //},
                          //EmpRelativeItem = new EmpRelativeItem()
                          //{
                          //    RName = R.RName,
                          //    RAddress = R.RAddress
                          //},
                          CompanyItem = new CompanyItem()
                          {
                              CompName = Companys.CompName,
                          },
                          BranchItem = new BranchItem()
                          {
                              BranchName = Branchs.BranchName
                          }

                          //Company = Companys.CompName
                      }

                  ).Distinct().ToList();


            return ab;
            //  
        }

        public List<EmployeeItem> GetALL(int cid)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> lstMaster = DbContext.employee_master.Where(m => m.Compid == (cid == 0 ? m.Compid : cid)).ToList();
            List<EmployeeItem> objmasterp = Mapper.Map<List<EmployeeItem>>(lstMaster);
            return objmasterp;
            //    var data = (from em in DbContext.employee_master
            //                join lm in DbContext.Login_Master on em.Compid equals lm.CompId
            //                where em.Compid == (cid == 0 ? em.Compid : cid)
            //                select new EmployeeItem()
            //                {
            //                    id=em.id,
            //                    Empno=em.Empno,
            //                    Empname=em.Empname,
            //                    Compid=em.Compid,
            //                    Branchid=em.Branchid,
            //                    EmployeeType=em.EmployeeType,
            //                    bloodid=em.bloodid,
            //                    Photo=em.Photo,
            //                    alias=em.alias,
            //                    FileNumber=em.FileNumber,
            //                    BasicSalary=em.BasicSalary,
            //                    Category=em.Category,
            //                    Mobile=em.Mobile,
            //                    HomeTel=em.HomeTel,
            //                    LocalTel=em.LocalTel,
            //                    Email=em.Email,
            //                    DOJ=em.DOJ,
            //                    Remarks=em.Remarks,
            //                    DOL=em.DOL,
            //                    RejoinDate=em.RejoinDate,
            //                    DueDate=em.DueDate,
            //                    Status=em.Status,
            //                    Profile=em.Profile,
            //                    CreatedBy=em.CreatedBy,
            //                    CreatedDate=em.CreatedDate,
            //                    UpdatedBy=em.UpdatedBy,
            //                    UpdatedDate=em.UpdatedDate,
            //                    DOB=em.DOB,
            //                    JobTitle=em.JobTitle,
            //                    DOD=em.DOD,
            //                    WorkingStatus=em.WorkingStatus,
            //                    Graduity=em.Graduity
            //                }).ToList();
            //    return data;
        }

        public EmployeeItem GetById(int id)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            employee_master objmaster = DbContext.employee_master.SingleOrDefault(m => m.id == id);
            EmployeeItem objmasterp = Mapper.Map<EmployeeItem>(objmaster);
            return objmasterp;

        }

        public void Update(EmpAllItem model)
        {
            Mapper.CreateMap<EmployeeItem, employee_master>();
            employee_master objmaster = DbContext.employee_master.SingleOrDefault(m => m.id == model.EmployeeItem.id);
            objmaster = Mapper.Map(model.EmployeeItem, objmaster);
            DbContext.SaveChanges();

        }

        public List<MasterDataItem> GetMasterData(int id)
        {
            Mapper.CreateMap<Masters_Tran, MasterDataItem>();
            List<Masters_Tran> objMasterData = DbContext.Masters_Tran.Where(m => m.MasterId == id).ToList();
            List<MasterDataItem> objmasterList = Mapper.Map<List<MasterDataItem>>(objMasterData);
            return objmasterList;

        }

        // for Retrieve Employee data on based on Company List
        public List<EmpAllItem> EmpDetailsByCompany(int Compid)
        {
            //Mapper.CreateMap<employee_master, EmpAllItem>();
            //List<employee_master> objEm = DbContext.employee_master.Where(m => m.Compid == Compid).ToList();
            //List<EmpAllItem> objEmpItem = Mapper.Map<List<EmpAllItem>>(objEm);
            //return objEmpItem;
            var EmpDetails = (from Emp in DbContext.employee_master
                              join Company in DbContext.Company_master on Emp.Compid equals Company.id into CompanyMst
                              from Companys in CompanyMst.DefaultIfEmpty()
                              join Branch in DbContext.Branch_master on Emp.Branchid equals Branch.id into BranchMst
                              from Branchs in BranchMst.DefaultIfEmpty()
                              where Emp.Compid == Compid
                              select new EmpAllItem()
                              {
                                  EmployeeItem = new EmployeeItem()
                                  {
                                      Empname = Emp.Empname,
                                      Mobile = Emp.Mobile,
                                      id = Emp.id,
                                      JobTitle = Emp.JobTitle
                                  },
                                  CompanyItem = new CompanyItem()
                                  {
                                      CompName = Companys.CompName,
                                  },
                                  BranchItem = new BranchItem()
                                  {
                                      BranchName = Branchs.BranchName
                                  }


                              }
                      ).Distinct().ToList();

            return EmpDetails;
        }
        public List<EmployeeItem> GetEmpByComp(int CompCode)
        {
            var Vehicle = (from data in DbContext.employee_master.Where(c => c.Compid == CompCode)
                           select new EmployeeItem()
                           {
                               id = data.id,
                               Empname = data.Empname
                           }
              ).ToList();
            return Vehicle;
        }
        public List<clsMasterData> GetJobTitle()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 42 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;
        }
        public List<clsMasterData> GetWorkingStatus()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 11 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;
        }
        public List<EmpAllItem> getEmpContacts(int cid)
        {
            var ab = (from Emp in DbContext.employee_master
                      //join Contact in DbContext.EmployeeContacts on Emp.id equals Contact.EmpId into Contact1
                      //from C in Contact1.DefaultIfEmpty()
                      //join Passport in DbContext.EmployeePassports on Emp.id equals Passport.EmpId into Passport1
                      //from P in Passport1.DefaultIfEmpty()
                      //join Relative in DbContext.EmployeeRelatives on Emp.id equals Relative.EmpId into Relative1
                      //from R in Relative1.DefaultIfEmpty()
                      join mt in DbContext.Masters_Tran on Emp.JobTitle equals mt.Id into mtt
                      from m in mtt.DefaultIfEmpty()
                      join Company in DbContext.Company_master on Emp.Compid equals Company.id into Company1
                      from Companys in Company1.DefaultIfEmpty()
                      join Branch in DbContext.Branch_master on Emp.Branchid equals Branch.id into BranchMst
                      from Branchs in BranchMst.DefaultIfEmpty()
                      where Emp.Compid == (cid == 0 ? Emp.Compid : cid) && Emp.WorkingStatus != 94
                      //where Emp.Compid == (Compid == 0 ? Emp.Compid : Compid)
                      //where Emp.WorkingStatus == (FID == 0 ? Emp.WorkingStatus : FID)
                      select new EmpAllItem
                      {
                          //EmpContactItem = new EmpContactItem()
                          //{
                          //    Area = C.Area,
                          //    City = C.City,
                          //},
                          EmployeeItem = new EmployeeItem()
                          {
                              Empno = Emp.Empno,
                              Empname = Emp.Empname,
                              Mobile = Emp.Mobile,
                              id = Emp.id,
                              JobTitle = Emp.JobTitle
                          },
                          //EmpPassportItem = new EmpPassportItem()
                          //{
                          //    PassportNo = P.PassportNo

                          //},
                          //EmpRelativeItem = new EmpRelativeItem()
                          //{
                          //    RName = R.RName,
                          //    RAddress = R.RAddress
                          //},
                          CompanyItem = new CompanyItem()
                          {
                              CompName = (Companys == null ? string.Empty : Companys.CompName)
                          },
                          BranchItem = new BranchItem()
                          {
                              BranchName = (Branchs == null ? string.Empty : Branchs.BranchName)
                          },
                          desiDetails = new clsMasterData()
                          {
                              Name = (m == null ? string.Empty : m.Name)
                          }

                          //Company = Companys.CompName
                      }

                 ).Distinct().ToList();


            return ab;
            //  

        }
        public EmployeeItem getEmpByID(int id)
        {
            var data = (from Emp in DbContext.employee_master
                        join Company in DbContext.Company_master on Emp.Compid equals Company.id into CompanyMst
                        from Companys in CompanyMst.DefaultIfEmpty()

                        join mt in DbContext.Masters_Tran on Emp.JobTitle equals mt.Id into mast
                        from m in mast.DefaultIfEmpty()

                        join pm in DbContext.EmployeePassports on Emp.id equals pm.EmpId into pass
                        from p in pass.DefaultIfEmpty()

                        //join master in DbContext.Masters_Tran on Convert.ToInt32(p.Nationality.ToString()) equals master.Id into nt
                        //from n in nt.DefaultIfEmpty()
                        join vm in DbContext.EmployeeVisas on Emp.id equals vm.EmpId into visa
                        from v in visa.DefaultIfEmpty()
                        where Emp.id == id
                        select new EmployeeItem()
                              {

                                  Empname = Emp.Empname,
                                  Mobile = Emp.Mobile,
                                  id = Emp.id,
                                  JobTitle = Emp.JobTitle,
                                  Photo = Emp.Photo,
                                  Empno = Emp.Empno,
                                  DOB = Emp.DOB,
                                  CompDetails = new CompanyItem()
                                  {
                                      CompName = (Companys == null ? string.Empty : Companys.CompName),
                                      Phone = (Companys == null ? string.Empty : Companys.Phone),
                                      Logo = (Companys == null ? string.Empty : Companys.Logo)
                                  },
                                  DesignationDetails = new clsMasterData()
                                  {
                                      Name = (m == null ? string.Empty : m.Name)
                                  },
                                  //NationDetails = new clsMasterData()
                                  //{
                                  //    Name = (n == null ? string.Empty : n.Name)
                                  //},
                                  VisaDetails = new EmpVisaItem()
                                  {
                                      //ExpiryDate = (v == null ? DateTime.MaxValue : v.ExpiryDate)
                                      ExpiryDate = v.ExpiryDate
                                  }


                              }
                     ).SingleOrDefault();

            return data;
        }
    }
}
