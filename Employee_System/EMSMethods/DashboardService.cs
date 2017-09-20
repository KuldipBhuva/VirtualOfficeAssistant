using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Insurance;
using EMSDomain.ViewModel.Tenancy;
using EMSDomain.ViewModel.TradeLicense;
using EMSDomain.ViewModel.Vehicle;

namespace EMSMethods
{
    public class DashboardService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public List<AppointmentsItem> getCalenderData()
        {
            var objAppoinment = (from AM in DbContext.AppointmentsMasters

                                 select new AppointmentsItem()
                                 {
                                     ID = AM.ID,
                                     Subject = AM.Subject,
                                     Status = AM.Status,
                                     StDt = AM.StDt,
                                     StTime = AM.StTime,
                                     EndDt = AM.EndDt,
                                     EndTime = AM.EndTime,
                                     Priority = AM.Priority,
                                     Reminder = AM.Reminder,
                                     ReDate = AM.ReDate,
                                     ReTime = AM.ReTime,
                                     Remarks = AM.Remarks
                                 }
                ).ToList();

            return objAppoinment;
        }
        //public List<EmpDLicenceItem> GetLicence()
        //{
        //    var objLicence = (from Emp in DbContext.employee_master
        //                      join EmpLicence in DbContext.EmpDrivingLicences on Emp.id equals EmpLicence.EmpId
        //                      where EmpLicence.ExpiryDate <= (System.DateTime.Now)
        //                      select new EmpDLicenceItem()
        //                      {
        //                          LicenceNo = EmpLicence.LicenceNo,
        //                          IssueCountry = EmpLicence.IssueCountry,
        //                          IssueDate = EmpLicence.IssueDate,
        //                          ExpiryDate = EmpLicence.ExpiryDate,
        //                          DRemarks = EmpLicence.DRemarks,
        //                          DStatus = EmpLicence.DStatus,
        //                          EmpDetails = new EmployeeItem()
        //                          {
        //                              Empname = Emp.Empname,
        //                              id = Emp.id
        //                          }
        //                      }
        //        ).ToList();

        //    return objLicence;
        //}
        public List<EmpHealthItem> GetLicence(int cid)
        {
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= (System.DateTime.Now) && HC.Type == "Driving License" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                             ).ToList();
            return objData;

        }
        public List<EmpHealthItem> GetLicence30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime expiryDate = DateTime.Today.AddDays(30);
            DateTime Next30 = today.AddDays(+30);
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= Next30 && HC.ExpiryDate > today && HC.Type == "Driving License" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                             ).ToList();
            return objData;

        }
        public List<EmpDLicenceItem> getLicCount()
        {
            var licenceCount = (from Emp in DbContext.employee_master
                                join EmpLicence in DbContext.EmpDrivingLicences on Emp.id equals EmpLicence.EmpId
                                where Convert.ToDateTime(System.DateTime.Now.AddDays(30)) < EmpLicence.ExpiryDate
                                select new EmpDLicenceItem()
                                {

                                }
                              ).ToList();
            return licenceCount;
        }
        public List<EmpPassportItem> GetPassport(int cid)
        {
            var objPass = (from Emp in DbContext.employee_master
                           join EmpPass in DbContext.EmployeePassports on Emp.id equals EmpPass.EmpId
                           join comp in DbContext.Company_master on Emp.Compid equals comp.id
                           join branch in DbContext.Branch_master on Emp.Branchid equals branch.id into br
                           from b in br.DefaultIfEmpty()
                           join m in DbContext.Masters_Tran on EmpPass.IssueCountry equals m.Id
                           where EmpPass.ExpiryDate <= (System.DateTime.Now) && Emp.WorkingStatus != 94 && Emp.Compid == (cid == 0 ? Emp.Compid : cid)
                           select new EmpPassportItem()
                              {
                                  EmpDetails = new EmployeeItem()
                                  {
                                      Empname = Emp.Empname,
                                      id = Emp.id
                                  },
                                  EmpComp = new CompanyItem()
                                  {
                                      CompName = comp.CompName,
                                      id = comp.id
                                  },
                                  CompBranch = new BranchItem()
                                  {
                                      BranchName = (b == null ? string.Empty : b.BranchName),
                                  },
                                  CountryDetails = new clsMasterData()
                                  {
                                      Name = m.Name
                                  },
                                  Id = EmpPass.Id,
                                  PassportNo = EmpPass.PassportNo,
                                  PlaceIssue = EmpPass.PlaceIssue,
                                  IssueCountry = EmpPass.IssueCountry,
                                  ExpiryDate = EmpPass.ExpiryDate,
                              }
                ).ToList();
            return objPass;
        }
        public List<EmpPassportItem> GetPassport30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime expiryDate = DateTime.Today.AddDays(30);
            DateTime Next30 = today.AddDays(+30);

            //var objPass = (from pass in DbContext.EmployeePassports.Where(p=>p.ExpiryDate<=Next30 && p.ExpiryDate>today).DefaultIfEmpty()
            //               from emp in DbContext.employee_master.Where(e => e.id == pass.EmpId).DefaultIfEmpty()
            //               from br in DbContext.Branch_master.Where(b => b.id == emp.Branchid).DefaultIfEmpty()
            //               from cm in DbContext.Company_master.Where(c => c.id == emp.Compid).DefaultIfEmpty()
            //               from mt in DbContext.Masters_Tran.Where(m => m.Id == pass.IssueCountry).DefaultIfEmpty()
            //               //where pass.ExpiryDate <= Next30 && pass.ExpiryDate > today
            //               select new EmpPassportItem()
            //               {
            //                   EmpDetails = new EmployeeItem()
            //                   {
            //                       Empname = emp.Empname,
            //                       Empno = emp.Empno,
            //                       id = emp.id
            //                   },
            //                   EmpComp = new CompanyItem()
            //                   {
            //                       CompName = cm.CompName,
            //                       id = cm.id
            //                   },
            //                   CompBranch = new BranchItem()
            //                   {
            //                       //BranchName = br.BranchName,
            //                       BranchName = (br == null ? string.Empty : br.BranchName)
            //                       //id = br.id
            //                   },
            //                   CountryDetails = new clsMasterData()
            //                   {
            //                       Name=mt.Name
            //                   },
            //                   Id=pass.Id,
            //                   PassportNo=pass.PassportNo,
            //                   PlaceIssue = pass.PlaceIssue,
            //                   IssueCountry = pass.IssueCountry,
            //                   ExpiryDate = pass.ExpiryDate,
            //               }

            var objPass = (from Emp in DbContext.employee_master.DefaultIfEmpty()
                           join EmpPass in DbContext.EmployeePassports on Emp.id equals EmpPass.EmpId into ep
                           from p in ep.DefaultIfEmpty()
                           join comp in DbContext.Company_master on Emp.Compid equals comp.id into comp
                           from c in comp.DefaultIfEmpty()
                           //join branch in DbContext.Branch_master on Emp.Branchid equals branch.id //into br
                           //from b in br.DefaultIfEmpty()
                           from branch in DbContext.Branch_master.Where(b => b.id == Emp.Branchid).DefaultIfEmpty()
                           join m in DbContext.Masters_Tran on p.IssueCountry equals m.Id into mt
                           from m1 in mt.DefaultIfEmpty()
                           where p.ExpiryDate <= Next30 && p.ExpiryDate > today && Emp.WorkingStatus != 94 && Emp.Compid == (cid == 0 ? Emp.Compid : cid)
                           select new EmpPassportItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = Emp.Empname,
                                   id = Emp.id,
                                   Empno = Emp.Empno
                               },
                               EmpComp = new CompanyItem()
                               {
                                   CompName = c.CompName,
                                   id = c.id
                               },
                               CompBranch = new BranchItem()
                               {
                                   BranchName = (branch == null ? string.Empty : branch.BranchName),
                                   //BranchName = branch.BranchName,
                                   //id = b.id
                               },
                               CountryDetails = new clsMasterData()
                               {
                                   Name = m1.Name
                               },
                               Id = p.Id,
                               PassportNo = p.PassportNo,
                               PlaceIssue = p.PlaceIssue,
                               IssueCountry = p.IssueCountry,
                               ExpiryDate = p.ExpiryDate,
                           }
               ).ToList();
            return objPass;
        }
        public List<EmpHealthItem> GetHealthCard(int cid)
        {
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= (System.DateTime.Now) && HC.Type == "Health" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                             ).ToList();
            return objData;
        }
        public List<EmpHealthItem> GetHealthCard30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime expiryDate = DateTime.Today.AddDays(30);
            DateTime Next30 = today.AddDays(+30);
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= Next30 && HC.ExpiryDate > today && HC.Type == "Health" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                             ).ToList();
            return objData;
        }
        public List<EmpHealthItem> GetLabourCard(int cid)
        {
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= (System.DateTime.Now) && HC.Type == "Labour" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                             ).ToList();
            return objData;
        }
        public List<EmpHealthItem> GetLabourCard30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= Next30 && HC.ExpiryDate > today && HC.Type == "Labour" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                             ).ToList();
            return objData;
        }
        public List<EmpInsuranceItem> getInsu(int cid)
        {
            var objData = (from IM in DbContext.EmployeeInsurances
                           join CM in DbContext.Company_master on IM.CompID equals CM.id
                           join BM in DbContext.Branch_master on IM.Branch equals BM.id into br
                           from b in br.DefaultIfEmpty()
                           join MT in DbContext.Masters_Tran on IM.Icomp equals MT.Id
                           join MT1 in DbContext.Masters_Tran on IM.Ptype equals MT1.Id
                           join EM in DbContext.employee_master on IM.EmpId equals EM.id
                           where IM.Pdate <= (System.DateTime.Now) && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpInsuranceItem()
                           {
                               MasterDetails = new clsMasterData()
                               {
                                   Name = MT.Name,
                               },
                               MasterDetailsType = new clsMasterData()
                               {
                                   Name = MT1.Name
                               },
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               BranchDetails = new BranchItem()
                               {
                                   BranchName = (b == null ? string.Empty : b.BranchName)
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               Pname = IM.Pname,
                               Pno = IM.Pno,
                               PExpDate = IM.PExpDate
                           }
                             ).Distinct().ToList();
            return objData;
        }
        public List<EmpInsuranceItem> getInsu30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from IM in DbContext.EmployeeInsurances
                           join CM in DbContext.Company_master on IM.CompID equals CM.id
                           join BM in DbContext.Branch_master on IM.Branch equals BM.id into br
                           from b in br.DefaultIfEmpty()
                           join MT in DbContext.Masters_Tran on IM.Icomp equals MT.Id
                           join MT1 in DbContext.Masters_Tran on IM.Ptype equals MT1.Id
                           join EM in DbContext.employee_master on IM.EmpId equals EM.id
                           where IM.Pdate <= Next30 && IM.Pdate > today && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpInsuranceItem()
                           {
                               MasterDetails = new clsMasterData()
                               {
                                   Name = MT.Name,
                               },
                               MasterDetailsType = new clsMasterData()
                               {
                                   Name = MT1.Name
                               },
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               BranchDetails = new BranchItem()
                               {
                                   BranchName = (b == null ? string.Empty : b.BranchName)
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               Pname = IM.Pname,
                               Pno = IM.Pno,
                               PExpDate = IM.PExpDate
                           }
                             ).Distinct().ToList();
            return objData;
        }
        public List<VehicleItem> getVehicle(int cid)
        {
            var objData = (from VM in DbContext.VehicleMasters
                           join CM in DbContext.Company_master on VM.CompID equals CM.id
                           join BM in DbContext.Branch_master on VM.Branch_ID equals BM.id into br
                           from b in br.DefaultIfEmpty()
                           join EM in DbContext.employee_master on VM.EmpID equals EM.id
                           where VM.Reg_Exp_Date <= (System.DateTime.Now) && EM.WorkingStatus != 94 && VM.Status == "Active" && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new VehicleItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               BranchDetails = new BranchItem()
                               {
                                   BranchName = (b == null ? string.Empty : b.BranchName)
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               VehicleName = VM.VehicleName,
                               VehicleNo = VM.VehicleNo,
                               Reg_Exp_Date = VM.Reg_Exp_Date
                           }
                            ).Distinct().ToList();
            return objData;
        }
        public List<VehicleItem> getVehicle30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from VM in DbContext.VehicleMasters
                           join CM in DbContext.Company_master on VM.CompID equals CM.id
                           join BM in DbContext.Branch_master on VM.Branch_ID equals BM.id into br
                           from b in br.DefaultIfEmpty()
                           join EM in DbContext.employee_master on VM.EmpID equals EM.id
                           where VM.Reg_Exp_Date <= Next30 && VM.Reg_Exp_Date > today && EM.WorkingStatus != 94 && VM.Status == "Active" && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new VehicleItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               BranchDetails = new BranchItem()
                               {
                                   BranchName = (b == null ? string.Empty : b.BranchName)
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               VehicleName = VM.VehicleName,
                               VehicleNo = VM.VehicleNo,
                               Reg_Exp_Date = VM.Reg_Exp_Date
                           }
                            ).Distinct().ToList();
            return objData;
        }
        public List<EmpIncrementItem> getIncrement(int cid)
        {

            var objData = (from IM in DbContext.EmployeeIncrements
                           join EM in DbContext.employee_master on IM.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where IM.IncDate <= (System.DateTime.Now) && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpIncrementItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname,
                                   DOJ = EM.DOJ,
                                   DOL = EM.DOL,
                                   RejoinDate = EM.RejoinDate
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               IncAmount = IM.IncAmount,
                               IncDate = IM.IncDate,
                           }
                ).Distinct().ToList();
            return objData;
        }
        public List<EmpIncrementItem> getIncrement30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from IM in DbContext.EmployeeIncrements
                           join EM in DbContext.employee_master on IM.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where IM.IncDate <= Next30 && IM.IncDate > today && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpIncrementItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname,
                                   DOJ = EM.DOJ,
                                   DOL = EM.DOL,
                                   RejoinDate = EM.RejoinDate
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               IncAmount = IM.IncAmount,
                               IncDate = IM.IncDate,
                           }
                ).Distinct().ToList();
            return objData;
        }
        public List<EmpVisaItem> getVisa(int cid)
        {
            var objData = (from EV in DbContext.EmployeeVisas
                           join EM in DbContext.employee_master on EV.EmpId equals EM.id
                           join m in DbContext.Masters_Tran on EV.IssueCountry equals m.Id
                           where EV.ExpiryDate <= (System.DateTime.Now) && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpVisaItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname,
                               },
                               ConDetails = new clsMasterData()
                               {
                                   Name = m.Name
                               },
                               IssueDate = EV.IssueDate,
                               ExpiryDate = EV.ExpiryDate,
                               IssueCountry = EV.IssueCountry
                           }
                ).Distinct().ToList();
            return objData;
        }
        public List<EmpVisaItem> getVisa30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from EV in DbContext.EmployeeVisas
                           join EM in DbContext.employee_master on EV.EmpId equals EM.id
                           join m in DbContext.Masters_Tran on EV.IssueCountry equals m.Id
                           where EV.ExpiryDate <= Next30 && EV.ExpiryDate > today && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpVisaItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname,
                               },
                               ConDetails = new clsMasterData()
                               {
                                   Name = m.Name
                               },
                               IssueDate = EV.IssueDate,
                               ExpiryDate = EV.ExpiryDate,
                               IssueCountry = EV.IssueCountry
                           }
                ).Distinct().ToList();
            return objData;
        }
        //public List<EmpVisaItem> getEmirates()
        //{
        //    DateTime today = System.DateTime.Now;
        //    var objData = (from EV in DbContext.EmployeeVisas
        //                   join EM in DbContext.employee_master on EV.EmpId equals EM.id
        //                   join CM in DbContext.Company_master on EM.Compid equals CM.id
        //                   where EV.EmiratesDate <= (System.DateTime.Now)
        //                   where EV.EmiratesDate <= today
        //                   select new EmpVisaItem()
        //                   {
        //                       CompDetails = new CompanyItem()
        //    {
        //        CompName = CM.CompName
        //    },
        //                       EmpDetails = new EmployeeItem()
        //                       {
        //                           Empname = EM.Empname,
        //                       },
        //                       EmiratesId = EV.EmiratesId,
        //                       EmiratesDate = EV.EmiratesDate,
        //                       IssueDate = EV.IssueDate,
        //                       ReniewDate = EV.ReniewDate,
        //                       ExpiryDate = EV.ExpiryDate,
        //                       IssueCountry = EV.IssueCountry
        //                   }
        //        ).Distinct().ToList();
        //    return objData;
        //}
        public List<EmpHealthItem> getEmirates(int cid)
        {
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= (System.DateTime.Now) && HC.Type == "Emirates ID" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }).ToList();
            return objData;
        }
        public List<EmpHealthItem> getEmirates30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from HC in DbContext.EmployeeHealthCards
                           join EM in DbContext.employee_master on HC.EmpId equals EM.id
                           join CM in DbContext.Company_master on EM.Compid equals CM.id
                           where HC.ExpiryDate <= Next30 && HC.ExpiryDate > today && HC.Type == "Emirates ID" && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                           select new EmpHealthItem()
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = EM.Empname
                               },
                               CompDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               HId = HC.HId,
                               CardNo = HC.CardNo,
                               IssueDate = HC.IssueDate,
                               ExpiryDate = HC.ExpiryDate
                           }
                  ).ToList();
            return objData;

        }
        public List<TradeLicenseItem> getCompDoc(int cid)
        {
            var objData = (from tlm in DbContext.Trade_License
                           join mt in DbContext.Masters_Tran on tlm.LicenseTypeID equals mt.Id into mast
                           from m in mast.DefaultIfEmpty()
                           where tlm.ExpiryDate <= (System.DateTime.Now) && tlm.CompID == (cid == 0 ? tlm.CompID : cid)
                           select new TradeLicenseItem()
                           {
                               TID = tlm.TID,
                               Code = tlm.Code,
                               Lno = tlm.Lno,
                               IssueDate = tlm.IssueDate,
                               ExpiryDate = tlm.ExpiryDate,
                               LicenseTypeID = tlm.LicenseTypeID,
                               LicTypeDetails = new clsMasterData()
                               {
                                   Name = (m == null ? string.Empty : m.Name)
                               }
                           }).ToList();
            return objData;
        }
        public List<TradeLicenseItem> getCompDoc30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from tlm in DbContext.Trade_License
                           join mt in DbContext.Masters_Tran on tlm.LicenseTypeID equals mt.Id into mast
                           from m in mast.DefaultIfEmpty()
                           where tlm.ExpiryDate <= Next30 && tlm.ExpiryDate > today && tlm.CompID == (cid == 0 ? tlm.CompID : cid)
                           select new TradeLicenseItem()
                           {
                               TID = tlm.TID,
                               Code = tlm.Code,
                               Lno = tlm.Lno,
                               IssueDate = tlm.IssueDate,
                               ExpiryDate = tlm.ExpiryDate,
                               LicenseTypeID = tlm.LicenseTypeID,
                               LicTypeDetails = new clsMasterData()
                               {
                                   Name = (m == null ? string.Empty : m.Name)
                               }
                           }
                  ).ToList();
            return objData;

        }
        public List<TenancyItem> getTenancy(int cid)
        {
            var objData = (from tm in DbContext.TenancyMasters
                           join mt in DbContext.Masters_Tran on tm.TnTypeId equals mt.Id into mast
                           from m in mast.DefaultIfEmpty()
                           where tm.ToDate <= (System.DateTime.Now) && tm.Status == "Active" && tm.TnTypeCompId == (cid == 0 ? tm.TnTypeCompId : cid)
                           select new TenancyItem()
                           {
                               TnID = tm.TnID,
                               TnNo = tm.TnNo,
                               TDate = tm.TDate,
                               LandLord = tm.LandLord,
                               TnTypeId = tm.TnTypeId,
                               Period = tm.Period,
                               FromDate = tm.FromDate,
                               ToDate = tm.ToDate,
                               Rent = tm.Rent,
                               Terms = tm.Terms,

                               TTDetails = new clsMasterData()
                               {
                                   Name = (m == null ? string.Empty : m.Name)
                               }
                           }).ToList();
            return objData;
        }
        public List<TenancyItem> getTenancy30(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var objData = (from tm in DbContext.TenancyMasters
                           join mt in DbContext.Masters_Tran on tm.TnTypeId equals mt.Id into mast
                           from m in mast.DefaultIfEmpty()
                           where tm.ToDate <= Next30 && tm.ToDate > today && tm.Status == "Active" && tm.TnTypeCompId == (cid == 0 ? tm.TnTypeCompId : cid)
                           select new TenancyItem()
                           {
                               TnID = tm.TnID,
                               TnNo = tm.TnNo,
                               TDate = tm.TDate,
                               LandLord = tm.LandLord,
                               TnTypeId = tm.TnTypeId,
                               Period = tm.Period,
                               FromDate = tm.FromDate,
                               ToDate = tm.ToDate,
                               Rent = tm.Rent,
                               Terms = tm.Terms,
                               TTDetails = new clsMasterData()
                               {
                                   Name = (m == null ? string.Empty : m.Name)
                               }
                           }
                  ).ToList();
            return objData;

        }
        public List<ContractModel> getExpContract(int cid)
        {
            var data = (from cm in DbContext.ContractMasters
                        join comp in DbContext.Company_master on cm.CompID equals comp.id
                        where cm.ToDate <= (System.DateTime.Now) && cm.CompID == (cid == 0 ? cm.CompID : cid)
                        select new ContractModel()
                        {
                            ContID = cm.ContID,
                            CompID = cm.CompID,
                            ContractWith = cm.ContractWith,
                            ContarctFor = cm.ContarctFor,
                            FrmDate = cm.FrmDate,
                            ToDate = cm.ToDate,
                            Amount = cm.Amount,
                            FileName = cm.FileName,
                            FilePath = cm.FilePath,
                            CompDetails = new CompanyItem()
                            {
                                CompName=comp.CompName
                            }
                        }).ToList();
            return data;

        }
        public List<ContractModel> get30Contract(int cid)
        {
            DateTime today = DateTime.Today;
            DateTime Next30 = today.AddDays(+30);
            var data = (from cm in DbContext.ContractMasters
                        join comp in DbContext.Company_master on cm.CompID equals comp.id
                        where cm.ToDate <= Next30 && cm.ToDate > today && cm.CompID == (cid == 0 ? cm.CompID : cid)
                        select new ContractModel()
                        {
                            ContID = cm.ContID,
                            CompID = cm.CompID,
                            ContractWith = cm.ContractWith,
                            ContarctFor = cm.ContarctFor,
                            FrmDate = cm.FrmDate,
                            ToDate = cm.ToDate,
                            Amount = cm.Amount,
                            FileName = cm.FileName,
                            FilePath = cm.FilePath,
                            CompDetails = new CompanyItem()
                            {
                                CompName = comp.CompName
                            }
                        }).ToList();
            return data;

        }
        //Missing Info
        public List<EmployeeItem> getEmpDOB(int cid)
        {
            var data = (from EM in DbContext.employee_master.DefaultIfEmpty()
                        join CM in DbContext.Company_master on EM.Compid equals CM.id
                        where EM.DOB == null && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmployeeItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            Empno = EM.Empno,
                            Empname = EM.Empname

                        }).ToList();
            return data;
        }
        public List<EmployeeItem> getEmpBS(int cid)
        {
            var data = (from EM in DbContext.employee_master.DefaultIfEmpty()
                        join CM in DbContext.Company_master on EM.Compid equals CM.id
                        where EM.BasicSalary == null && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmployeeItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            Empno = EM.Empno,
                            Empname = EM.Empname

                        }).ToList();
            return data;
        }
        public List<EmployeeItem> getLabour(int cid)
        {
            var data = (from EM in DbContext.employee_master.DefaultIfEmpty()
                        join CM in DbContext.Company_master on EM.Compid equals CM.id
                        where !DbContext.EmployeeHealthCards.Any(EHC => (EHC.EmpId == EM.id) && EHC.Type == "Labour") && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmployeeItem
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName,
                                Compcode = CM.Compcode
                            },
                            Empname = EM.Empname,
                            Empno = EM.Empno
                        }

                           ).ToList();
            return data;
        }

        public List<EmpPassportItem> getEmpPassport(int cid)
        {
            var data = (
                        from EM in DbContext.employee_master.DefaultIfEmpty()
                        join CM in DbContext.Company_master on EM.Compid equals CM.id
                        where !DbContext.EmployeePassports.Any(EP => (EP.EmpId == EM.id)) && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpPassportItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = EM.Empno,
                                Empname = EM.Empname
                            }

                        }).ToList();
            return data;
        }
        public List<EmpVisaItem> getEmpVisa(int cid)
        {
            var data = (from EM in DbContext.employee_master.DefaultIfEmpty()
                        join CM in DbContext.Company_master on EM.Compid equals CM.id
                        where !DbContext.EmployeeVisas.Any(EP => (EP.EmpId == EM.id)) && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmpVisaItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empname = EM.Empname,
                                Empno = EM.Empno
                            }
                        }).ToList();
            return data;
        }
        public List<EmployeeItem> getEmpEID(int cid)
        {
            var data = (from EM in DbContext.employee_master.DefaultIfEmpty()
                        join CM in DbContext.Company_master on EM.Compid equals CM.id
                        where !DbContext.EmployeeHealthCards.Any(EHC => (EHC.EmpId == EM.id) && EHC.Type == "Emirates ID") && EM.WorkingStatus != 94 && EM.Compid == (cid == 0 ? EM.Compid : cid)
                        select new EmployeeItem
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = CM.CompName,
                                Compcode = CM.Compcode
                            },
                            Empname = EM.Empname,
                            Empno = EM.Empno
                        }

                          ).ToList();
            return data;
        }


    }
}
