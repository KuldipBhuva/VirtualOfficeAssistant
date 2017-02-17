using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class RePaymentService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public List<CompanyItem> GetAllComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> lstmasterdata = Mapper.Map<List<CompanyItem>>(tblMaster);
            return lstmasterdata;
        }
        public List<EmployeeItem> GetEmp(int? cid)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.Where(m => m.WorkingStatus != 94 && m.Compid == (cid == 0 ? m.Compid : cid)).ToList();
            List<EmployeeItem> lstEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return lstEmp;
        }

        public int Insert(RePaymentItem model)
        {
            Repayment_Master objPayItem = new Repayment_Master();
            //  objPayItem.CompId = model.CompId;
            objPayItem.EmpId = model.EmpId;
            objPayItem.Payment = model.Payment;
            objPayItem.Month = model.Month;
            objPayItem.Year = model.Year;
            objPayItem.CompId = model.CompId;
            objPayItem.Date = model.Date;
            DbContext.Repayment_Master.Add(objPayItem);
            return DbContext.SaveChanges();


        }

        public int InsertRePayment(int? Month, int? Year, int? CompId, int? EmpId, decimal? Payment, int? Installment)
        {
            //object[] params1 = {
            //    new SqlParameter("@Month", Month),
            //    new SqlParameter("@Year", Year),
            //    new SqlParameter("@CompId", CompId),
            //    new SqlParameter("@EmpId", EmpId),
            //    new SqlParameter("@Payment", Payment),
            //    new SqlParameter("@Installment", Installment)
            //                   };

            var Repayment = DbContext.Database.SqlQuery<RePaymentItem>
                ("EXEC Usp_AddRepayment @Month,@Year,@CompId,@EmpId,@Payment,@Installment", new SqlParameter("Month", Month), new SqlParameter("Year", Year), new SqlParameter("CompId", CompId), new SqlParameter("EmpId", EmpId), new SqlParameter("Payment", Payment), new SqlParameter("@Installment", Installment)).ToList<RePaymentItem>();
            return 1;
        }

        public List<RePaymentItem> GetPaymentDetails(int cid)
        {
            var objData = (from RP in DbContext.Repayment_Master
                           join emp in DbContext.employee_master on RP.EmpId equals emp.id
                           join comp in DbContext.Company_master on emp.Compid equals comp.id
                           where RP.CompId == (cid == 0 ? RP.CompId : cid)
                           select new RePaymentItem
                           {
                               Payment = RP.Payment,
                               CompId = comp.id,
                               EmpId = RP.EmpId,
                               CompName = comp.CompName,
                               EmpName = emp.Empname,
                               Month = RP.Month,
                               Year = RP.Year,
                               Id = RP.Id

                           }
                           ).ToList();

            return objData;
        }


        public RePaymentItem GetById(int id)
        {
            Mapper.CreateMap<Repayment_Master, RePaymentItem>();
            Repayment_Master objRep = DbContext.Repayment_Master.SingleOrDefault(m => m.Id == id);
            RePaymentItem objRepItem = Mapper.Map<RePaymentItem>(objRep);
            return objRepItem;
        }

        public int Update(RePaymentItem model)
        {
            model.Date = System.DateTime.Now;
            Mapper.CreateMap<RePaymentItem, Repayment_Master>();
            Repayment_Master objRep = DbContext.Repayment_Master.SingleOrDefault(m => m.Id == model.Id);
            objRep = Mapper.Map(model, objRep);
            return DbContext.SaveChanges();
        }

        public List<RePaymentItem> GetAmtDetails(int EmpId)
        {
            object[] params1 = {
                new SqlParameter("@EmpId", EmpId)};

            //Get student name of string type
            var Repayment = DbContext.Database.SqlQuery<RePaymentItem>
                ("EXEC GetEmpPayment @EmpId", new SqlParameter("EmpId", EmpId)).ToList<RePaymentItem>();
            //DbContext.Database.SqlQuery<PayrollItem>("exec PayrollReport", params1).ToList<PayrollItem>();
            return Repayment;
        }


        public List<RePaymentItem> GetLedgerDetails(int? EmpId, int? CompId)
        {
            object[] params1 = {
                new SqlParameter("@CompId", CompId),
                new SqlParameter("@EmpId", EmpId)};
            //Get student name of string type
            var Repayment = DbContext.Database.SqlQuery<RePaymentItem>
                ("EXEC GetLedger @CompId,@EmpId", new SqlParameter("CompId", CompId), new SqlParameter("EmpId", EmpId)).ToList<RePaymentItem>();
            //DbContext.Database.SqlQuery<PayrollItem>("exec PayrollReport", params1).ToList<PayrollItem>();
            return Repayment;
        }


    }
}
