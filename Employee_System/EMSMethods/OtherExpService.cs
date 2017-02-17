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
using EMSDomain.ViewModel.Expenses;

namespace EMSMethods
{
    public class OtherExpService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(OtherExpItem model)
        {
            try
            {
                Mapper.CreateMap<OtherExpItem, Other_Expenses>();
                Other_Expenses objOExp = Mapper.Map<Other_Expenses>(model);
                DbContext.Other_Expenses.Add(objOExp);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<OtherExpItem> OtherExpData(int cid)
        {
            var data = (from OE in DbContext.Other_Expenses
                        join CM in DbContext.Company_master on OE.CompID equals CM.id
                        join EM in DbContext.employee_master on OE.EmpID equals EM.id
                        join BM in DbContext.Branch_master on OE.BranchID equals BM.id into br
                        from b in br.DefaultIfEmpty()
                        join MT in DbContext.Masters_Tran on OE.ExpID equals MT.Id
                        where OE.CompID == (cid == 0 ? OE.CompID : cid)
                        select new OtherExpItem()
          {
              CompDetails = new EMSDomain.ViewModel.Company.CompanyItem()
              {
                  CompName = CM.CompName
              },
              EmpDetails = new EMSDomain.ViewModel.Employee.EmployeeItem()
              {
                  Empname = EM.Empname
              },
              BranchDetails = new EMSDomain.ViewModel.Branch.BranchItem()
              {
                  BranchName = (b == null ? string.Empty : b.BranchName)
              },
              MasterDetails = new EMSDomain.ViewModel.clsMasterData()
              {
                  Name = MT.Name
              },
              OEID = OE.OEID,
              CompID = OE.CompID,
              BranchID = OE.BranchID,
              Date = OE.Date,
              EmpID = OE.EmpID,
              ExpID = OE.ExpID,
              Remarks = OE.Remarks,
              Amount = OE.Amount
          }).Distinct().ToList();
            return data;
        }
        public List<CompanyItem> GetAllComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> lstmasterdata = Mapper.Map<List<CompanyItem>>(tblMaster);
            return lstmasterdata;
        }
        public List<EmployeeItem> GetEmp()
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.Where(m => m.WorkingStatus != 94).ToList();
            List<EmployeeItem> lstEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return lstEmp;
        }
        public List<BranchItem> GetBranch()
        {
            Mapper.CreateMap<Branch_master, BranchItem>();
            List<Branch_master> lstBranch = DbContext.Branch_master.ToList();
            List<BranchItem> lstBranchItem = Mapper.Map<List<BranchItem>>(lstBranch);
            return lstBranchItem;
        }
        public int Update(OtherExpItem model)
        {
            Other_Expenses TE = new Other_Expenses();
            TE = DbContext.Other_Expenses.SingleOrDefault(m => m.OEID == model.OEID);
            TE.CompID = model.CompID;
            TE.EmpID = model.EmpID;
            TE.Date = model.Date;
            TE.BranchID = model.BranchID;
            TE.ExpID = model.ExpID;
            TE.Remarks = model.Remarks;
            TE.Amount = model.Amount;
            //TE.Status = model.Status;
            TE.UpdateBy = model.UpdateBy;
            TE.UpdatedDate = model.UpdatedDate;
            return DbContext.SaveChanges();
        }
        public OtherExpItem GetById(int id)
        {
            Mapper.CreateMap<Other_Expenses, OtherExpItem>();
            Other_Expenses objOExp = DbContext.Other_Expenses.SingleOrDefault(m => m.OEID == id);
            OtherExpItem objOExpITem = Mapper.Map<OtherExpItem>(objOExp);
            return objOExpITem;
        }
        public List<clsMasterData> GetExp()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 40 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;
        }
    }
}
