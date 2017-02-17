using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class EmpPassportService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public int Insert(EmpPassportItem model)
        {
            try
            {
                Mapper.CreateMap<EmpPassportItem, EmployeePassport>();
                EmployeePassport objEmpPassport = Mapper.Map<EmployeePassport>(model);
                DbContext.EmployeePassports.Add(objEmpPassport);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EmpPassportItem> PassportListData(int EmpId)
        {
            Mapper.CreateMap<EmployeePassport, EmpPassportItem>();
            var objPass = (from EmpPass in DbContext.EmployeePassports
                           join Emp in DbContext.employee_master on EmpPass.EmpId equals Emp.id
                           join mt in DbContext.Masters_Tran on EmpPass.IssueCountry equals mt.Id into p1
                           from p in p1.DefaultIfEmpty()
                           where EmpPass.EmpId == (EmpId == 0 ? EmpPass.EmpId : EmpId)
                           select new EmpPassportItem
                           {
                               CountryDetails=new clsMasterData()
                               {
                                   Name=p.Name
                               },
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = Emp.Empname
                               },
                               PassportNo = EmpPass.PassportNo,
                               IssueDate = EmpPass.IssueDate,
                               IssueCountry = EmpPass.IssueCountry,
                               ExpiryDate = EmpPass.ExpiryDate,
                               EmpId = EmpPass.EmpId,
                               Id = EmpPass.Id
                           }
                ).Distinct().ToList();
            return objPass;
        }

        public EmpPassportItem GetById(int id)
        {
            Mapper.CreateMap<EmployeePassport, EmpPassportItem>();
            EmployeePassport objPassport = DbContext.EmployeePassports.SingleOrDefault(m => m.Id == id);
            EmpPassportItem objPassItem = Mapper.Map<EmpPassportItem>(objPassport);
            return objPassItem;
        }

        public int Update(EmpPassportItem model)
        {
            Mapper.CreateMap<EmpPassportItem, EmployeePassport>();
            EmployeePassport objPass = DbContext.EmployeePassports.SingleOrDefault(m => m.Id == model.Id);
            objPass = Mapper.Map(model, objPass);
            return DbContext.SaveChanges();
        }

        //public enum ItemTypes
        //{
        //    int Passport = 60;

        //}

        public List<EmpPassportItem> GetPassportDoc(int? Empid)
        {
            var objDoc = (from doc in DbContext.EmployeeDocuments
                          where doc.DocCategoryId == 141 // For Passport Document Category Id
                          && doc.EmpId == Empid
                          select new EmpPassportItem()
                          {
                              DocDetails = new EMSDomain.ViewModel.DocumentItem()
                              {
                                  FileName = doc.FilleName,
                                  FileUrl = doc.FileUrl,
                                  DocCategoryId = doc.DocCategoryId,
                                  EmpId = doc.EmpId,
                                  Id = doc.Id
                              }
                          }
                ).Distinct().ToList();
            return objDoc;
        }
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public List<clsMasterData> getNationality()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 44 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
