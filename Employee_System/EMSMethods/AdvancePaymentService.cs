using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Payroll;

namespace EMSMethods
{
    public class AdvancePaymentService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

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

        public int Insert(AdvancePaymentItem model)
        {
            Advance_Payroll_Master objPayItem = new Advance_Payroll_Master();
            objPayItem.CompId = model.CompId;
            objPayItem.EmpId = model.EmpId;
            objPayItem.AdvAmount = model.AdvAmount;
            objPayItem.CreatedDate = System.DateTime.Now;
            objPayItem.UpdatedDate = System.DateTime.Now;
            DbContext.Advance_Payroll_Master.Add(objPayItem);
            return DbContext.SaveChanges();


        }

        public List<AdvancePaymentItem> GetPaymentDetails(int cid)
        {
            var objData = (from AP in DbContext.Advance_Payroll_Master
                           join comp in DbContext.Company_master on AP.CompId equals comp.id
                           join emp in DbContext.employee_master on AP.EmpId equals emp.id
                           where AP.CompId == (cid == 0 ? AP.CompId : cid)
                           select new AdvancePaymentItem
                           {
                               AdvAmount = AP.AdvAmount,
                               CompId = AP.CompId,
                               EmpId = AP.EmpId,
                               CompName = comp.CompName,
                               EmpName = emp.Empname,
                               Id = AP.Id

                           }
                           ).ToList();

            return objData;
        }

        public AdvancePaymentItem GetById(int id)
        {
            try
            {
                Mapper.CreateMap<Advance_Payroll_Master, AdvancePaymentItem>();
                Advance_Payroll_Master objAdvance = DbContext.Advance_Payroll_Master.SingleOrDefault(m => m.Id == id);
                AdvancePaymentItem objAdvanceItem = Mapper.Map<AdvancePaymentItem>(objAdvance);
                return objAdvanceItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int Update(AdvancePaymentItem model)
        {
            //  model.CreatedDate = System.DateTime.Now;
            model.UpdatedDate = System.DateTime.Now;
            Mapper.CreateMap<AdvancePaymentItem, Advance_Payroll_Master>();
            Advance_Payroll_Master objRep = DbContext.Advance_Payroll_Master.SingleOrDefault(m => m.Id == model.Id);
            objRep = Mapper.Map(model, objRep);
            return DbContext.SaveChanges();
        }


    }
}
