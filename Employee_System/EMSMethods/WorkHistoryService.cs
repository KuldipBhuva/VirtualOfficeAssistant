using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class WorkHistoryService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public int Insert(EmpWorkHistoryItem model)
        {
            try
            {
                WorkHistory objWorkHistory = new WorkHistory();
                objWorkHistory.EmpId = model.EmpId;
                objWorkHistory.CompId = model.CompId;
                objWorkHistory.BranchId = model.BranchId;
                objWorkHistory.DesignationId = model.DesignationId;
                objWorkHistory.FromDate = model.FromDate;
                objWorkHistory.CreatedOn = DateTime.UtcNow;
                objWorkHistory.CreatedBy = 1;
                objWorkHistory.ToDate = model.ToDate;
                objWorkHistory.Remarks = model.Remarks;
                objWorkHistory.Perfomance = model.Perfomance;
                DbContext.WorkHistories.Add(objWorkHistory);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int Update(EmpWorkHistoryItem model)
        {
            try
            {
                WorkHistory objWorkHistory = new WorkHistory();
                objWorkHistory = DbContext.WorkHistories.SingleOrDefault(m => m.Id == model.Id);
                objWorkHistory.EmpId = model.EmpId;
                objWorkHistory.CompId = model.CompId;
                objWorkHistory.BranchId = model.BranchId;
                objWorkHistory.FromDate = model.FromDate;
                objWorkHistory.ToDate = model.ToDate;
                objWorkHistory.DesignationId = model.DesignationId;
                objWorkHistory.Remarks = model.Remarks;
                objWorkHistory.Perfomance = model.Perfomance;
                DbContext.WorkHistories.Add(objWorkHistory);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmpWorkHistoryItem> GetByEmpId(int EmpId)
        {
            var objPass = (from WH in DbContext.WorkHistories
                           join Emp in DbContext.employee_master on WH.EmpId equals Emp.id
                           join Desg in DbContext.Masters_Tran on WH.DesignationId equals Desg.Id
                           join Mast in DbContext.Masters on Desg.MasterId equals Mast.MId
                           join Comp in DbContext.Company_master on WH.CompId equals Comp.id
                           join Branch in DbContext.Branch_master on WH.BranchId equals Branch.id
                           where WH.EmpId == (EmpId == 0 ? WH.EmpId : EmpId) && Mast.MasterType == "Designation"
                           select new EmpWorkHistoryItem
                           {
                               EmpName = Emp.Empname,
                               CompName = Comp.CompName,
                               BranchName = Branch.BranchName,
                               DesignationName = Desg.Name,
                               CompId = WH.CompId,
                               BranchId = WH.BranchId,
                               DesignationId = WH.DesignationId
                           }
                ).ToList();
            return objPass;
        }
        public EmpWorkHistoryItem GetByPk(int wokrHistId)
        {
            var obj = (from wh in DbContext.WorkHistories
                       where wh.Id == wokrHistId
                       select new EmpWorkHistoryItem
                       {
                           CompId = wh.CompId,
                           BranchId = wh.BranchId,
                           FromDate = wh.FromDate,
                           ToDate = wh.ToDate,
                           DesignationId = wh.DesignationId,
                           Remarks = wh.Remarks,
                           Perfomance = wh.Perfomance
                       }).Single();
            return obj;
        }
    }
}
