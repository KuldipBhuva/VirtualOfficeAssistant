using System;
using System.Collections.Generic;
using System.Data;
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
    public class PayrollService
    {
        EmployeeEntities DbContext = new EmployeeEntities();


        public int Dave()
        {
            return 1;
        }

        public List<CompanyItem> BindCompany()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> objcompMst = DbContext.Company_master.ToList();
            List<CompanyItem> objCompItem = Mapper.Map<List<CompanyItem>>(objcompMst);
            return objCompItem;
        }

        public List<EmployeeItem> BindEmployee(int CompId)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> objEmpMst = DbContext.employee_master.Where(m => m.Compid == (CompId == null || CompId == 0 ? m.Compid : CompId)).ToList();
            List<EmployeeItem> objEmpItem = Mapper.Map<List<EmployeeItem>>(objEmpMst);
            return objEmpItem;
        }

        public List<PayrollItem> BindPayroll(int CompId)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            PayrollItem pay = new PayrollItem();
            List<PayrollItem> objEmpItem1 = new List<PayrollItem>();
            //   objEmpItem1.Add(new PayrollItem() { Empname = "crank arm", id = 1234 });

            // List<employee_master> objEmpMst = DbContext.employee_master.Where(m => m.Compid == (CompId == null || CompId == 0 ? m.Compid : CompId)).ToList();
            List<PayrollItem> objEmpItem = Mapper.Map<List<PayrollItem>>(objEmpItem1);
            return objEmpItem;
        }


        public List<PayrollItem> GridPayroll(int cid)
        {
            Mapper.CreateMap<PayrollMaster, PayrollItem>();
            var objPay =
                 (
                  from Pay in DbContext.PayrollMasters
                  join Comp in DbContext.Company_master on Pay.CompId equals Comp.id
                  where Pay.CompId == (cid == 0 ? Pay.CompId : cid)
                  select new PayrollItem()
                  {
                      CompId = Pay.CompId,
                      MonthName = Pay.Month == 1 ? "January" : Pay.Month == 2 ? "February" : Pay.Month == 3 ? "March" : Pay.Month == 4 ? "April" : Pay.Month == 5 ? "May" :
                                  Pay.Month == 6 ? "June" : Pay.Month == 7 ? "July" : Pay.Month == 8 ? "August" : Pay.Month == 9 ? "September" : Pay.Month == 10 ? "October" : Pay.Month == 11 ? "November" : Pay.Month == 12 ? "December" : "",
                      Month = Pay.Month,
                      Year = Pay.Year,
                      CompName = Comp.CompName
                  }

                 ).Distinct().ToList();
            List<PayrollItem> objPayItem = Mapper.Map<List<PayrollItem>>(objPay);
            return objPayItem;

        }


        public int GetExistsEmployee(int Empid, int Month, int Year)
        {
            int i = Convert.ToInt32(DbContext.PayrollMasters.Where(m => m.EmpId == Empid && m.Month == Month && m.Year == Year).Select(m => m.EmpId).FirstOrDefault());
            return i;
        }

        public int InsertPayroll(PayrollItem model)
        {
            try
            {
                PayrollMaster objPayroll = new PayrollMaster();
                objPayroll.Days = model.Days;
                objPayroll.EmpId = model.Empid;
                objPayroll.Month = model.Month;
                objPayroll.Year = model.Year;
                objPayroll.CompId = model.CompId;
                objPayroll.OT = model.OT;
                objPayroll.CreatedBy = Convert.ToString(System.DateTime.Now);
                objPayroll.CreatedBy = Convert.ToString(System.DateTime.Now);
                objPayroll.TotalDays = model.TotalDays;
                objPayroll.DA = model.DA;
                objPayroll.CreatedDate = System.DateTime.Now;
                objPayroll.UpdatedDate = System.DateTime.Now;
                //Mapper.CreateMap<PayrollItem, PayrollMaster>();
                //PayrollMaster objPayroll = Mapper.Map<PayrollMaster>(model);
                DbContext.PayrollMasters.Add(objPayroll);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int UpdatePayroll(PayrollItem model)
        {
            try
            {
                PayrollMaster objPayroll = new PayrollMaster();
                objPayroll = DbContext.PayrollMasters.Where(m => m.EmpId == model.Empid && m.Month == model.Month && m.Year == model.Year && m.CompId == model.CompId).SingleOrDefault();
                objPayroll.Days = model.Days;
                objPayroll.EmpId = model.Empid;
                objPayroll.Month = model.Month;
                objPayroll.Year = model.Year;
                objPayroll.CompId = model.CompId;
                objPayroll.OT = model.OT;
                objPayroll.DA = model.DA;
                objPayroll.CreatedBy = Convert.ToString(System.DateTime.Now);
                objPayroll.CreatedBy = Convert.ToString(System.DateTime.Now);
                objPayroll.TotalDays = model.TotalDays;
                // DbContext.PayrollMasters.Add(objPayroll);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public List<PayrollItem> GetPayrollDetails()
        {

            var Allowanceslist = (from Emp in DbContext.employee_master
                                  join Pay in DbContext.PayrollMasters on Emp.id equals Pay.EmpId
                                  join Allowance in DbContext.EmployeeAllowances on Emp.id equals Allowance.EmpId
                                  select new EmpAllowncesItem()
                                  {
                                      EmpId = Allowance.EmpId,
                                      Amount = Allowance.Amount

                                  }
                              ).ToList().GroupBy(c => c.EmpId).
                              Select(l => new EmpAllowncesItem { Amount = l.Sum(c => c.Amount), EmpId = l.First().EmpId }).ToList();



            var PayrollDetails = (
                //from Allowance in Allowanceslist
                                     from Emp in DbContext.employee_master
                                     join Pay in DbContext.PayrollMasters on Emp.id equals Pay.EmpId
                                     //  join Allowance in Allowanceslist on Emp.id equals Allowanceslist.First().EmpId
                                     // join Allowance in DbContext.EmployeeAllowances on Emp.id equals Allowance.EmpId into Allow
                                     //   from Allw in Allow.DefaultIfEmpty()
                                     select new PayrollItem
                                     {
                                         EmployeeItem = new EmployeeItem()
                                        {
                                            BasicSalary = Emp.BasicSalary,
                                            id = Emp.id,
                                            Empname = Emp.Empname,
                                            Empno = Emp.Empno,

                                        },
                                         Days = Pay.Days,
                                         OT = Pay.OT,
                                         EmpAllowItem = new EmpAllowncesItem()
                                         {

                                             // Amount = Allowance.Amount
                                         }

                                     }

                                    ).Distinct().Where(m => m.EmployeeItem.BasicSalary != null || m.EmployeeItem.BasicSalary != 0).ToList();


            return PayrollDetails;
        }

        public List<PayrollItem> GetPayrollDetails1(int Month, int Year, int CompId)
        {
            object[] params1 = {
                new SqlParameter("@Month", Month),
                new SqlParameter("@CompId", CompId),
                new SqlParameter("@Year", Year)};
            //Get student name of string type
            var courseList = DbContext.Database.SqlQuery<PayrollItem>
                ("EXEC PayrollReport @Month, @CompId,@Year ", new SqlParameter("Month", Month), new SqlParameter("CompId", CompId), new SqlParameter("Year", Year)).ToList<PayrollItem>();
            //DbContext.Database.SqlQuery<PayrollItem>("exec PayrollReport", params1).ToList<PayrollItem>();
            return courseList;
        }

        public List<PayrollItem> GetPayrollDetailsReport(int? Month, int? Year, int? CompId)
        {
            object[] params1 = {
                new SqlParameter("@Month", Month),
                new SqlParameter("@CompId", CompId),
                new SqlParameter("@Year", Year)};
            //Get student name of string type
            var courseList = DbContext.Database.SqlQuery<PayrollItem>
                ("EXEC PayrollReport_New @Month, @CompId,@Year ", new SqlParameter("Month", Month), new SqlParameter("CompId", CompId), new SqlParameter("Year", Year)).ToList<PayrollItem>();
            //DbContext.Database.SqlQuery<PayrollItem>("exec PayrollReport", params1).ToList<PayrollItem>();
            return courseList;
        }



    }
}
