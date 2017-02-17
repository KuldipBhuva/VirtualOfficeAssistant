using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Expenses;

namespace EMSMethods
{
    public class TExpenseService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(TExpenseItem model)
        {
            try
            {
                TravelExpence TE = new TravelExpence();
                TE.Companyid = model.Companyid;
                TE.EmpId = model.EmpId;
                TE.TDate = model.TDate;
                TE.Country = model.Country;
                TE.TFromDate = model.TFromDate;
                TE.TToDate = model.TToDate;
                TE.Fare = model.Fare;
                TE.FromDate = model.FromDate;
                TE.ToDate = model.ToDate;
                TE.City = model.City;
                TE.Id = model.Id;
                TE.AccomodationExp = model.AccomodationExp;
                TE.FoodExp = model.FoodExp;
                TE.OtherExp = model.OtherExp;
                TE.Conv_Exp = model.Conv_Exp;
                TE.Remarks = model.Remarks;
                TE.Status = model.Status;
                TE.CreatedBy = model.CreatedBy;
                TE.CreatedDate = model.CreatedDate;
                DbContext.TravelExpences.Add(TE);
                return DbContext.SaveChanges();
                //Mapper.CreateMap<TExpenseItem, TravelExpence>();
                //TravelExpence objTExp = Mapper.Map<TravelExpence>(model);
                //DbContext.TravelExpences.Add(objTExp);
                //return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<TExpenseItem> TExpenseData(int cid)
        {
            var objData = (from TE in DbContext.TravelExpences
                           join comp in DbContext.Company_master on TE.Companyid equals comp.id
                           join emp in DbContext.employee_master on TE.EmpId equals emp.id
                           where TE.Companyid == (cid == 0 ? TE.Companyid : cid)
                           select new TExpenseItem
                           {
                               CompDetails = new EMSDomain.ViewModel.Company.CompanyItem
                               {
                                   CompName = comp.CompName
                               },
                               EmpDetails = new EMSDomain.ViewModel.Employee.EmployeeItem
                               {
                                   Empname = emp.Empname
                               },
                               EmpId = TE.EmpId,
                               Companyid = TE.Companyid,
                               TDate = TE.TDate,
                               Country = TE.Country,
                               TFromDate = TE.TFromDate,
                               TToDate = TE.TToDate,
                               Fare = TE.Fare,
                               FromDate = TE.FromDate,
                               ToDate = TE.ToDate,
                               City = TE.City,
                               Id = TE.Id,
                               AccomodationExp = TE.AccomodationExp,
                               FoodExp = TE.FoodExp,
                               OtherExp = TE.OtherExp,
                               Remarks = TE.Remarks,
                               Status = TE.Status
                           }
                ).Distinct().ToList();
            return objData;
        }
        public int Update(TExpenseItem model)
        {
            TravelExpence TE = DbContext.TravelExpences.SingleOrDefault(m => m.Id == model.Id);
            TE.Companyid = model.Companyid;
            TE.EmpId = model.EmpId;
            TE.TDate = model.TDate;
            TE.Country = model.Country;
            TE.TFromDate = model.TFromDate;
            TE.TToDate = model.TToDate;
            TE.Fare = model.Fare;
            TE.FromDate = model.FromDate;
            TE.ToDate = model.ToDate;
            TE.City = model.City;
            TE.Id = model.Id;
            TE.AccomodationExp = model.AccomodationExp;
            TE.FoodExp = model.FoodExp;
            TE.OtherExp = model.OtherExp;
            TE.Remarks = model.Remarks;
            TE.Conv_Exp = model.Conv_Exp;
            TE.Status = model.Status;
            TE.UpdatedBy = model.UpdatedBy;
            TE.UpdatedDate = model.UpdatedDate;
            return DbContext.SaveChanges();
        }
        public TExpenseItem GetById(int id)
        {
            Mapper.CreateMap<TravelExpence, TExpenseItem>();
            TravelExpence objTExp = DbContext.TravelExpences.SingleOrDefault(m => m.Id == id);
            TExpenseItem objTExpItem = Mapper.Map<TExpenseItem>(objTExp);
            return objTExpItem;
        }
        public List<CompanyItem> GetAllComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> lstmasterdata = Mapper.Map<List<CompanyItem>>(tblMaster);
            return lstmasterdata;
        }
        public List<EmployeeItem> GetEmp(int cid)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.Where(m => m.WorkingStatus != 94 && m.Compid == (cid == 0 ? m.Compid : cid)).ToList();
            List<EmployeeItem> lstEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return lstEmp;
        }
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
