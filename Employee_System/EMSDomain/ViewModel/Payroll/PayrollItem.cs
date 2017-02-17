using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Payroll
{
    [Serializable]
    public class PayrollItem
    {
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        public int Empid { get; set; }
        [Required(ErrorMessage = "Please Change Date According To Month..")]
        public int? Days { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? OT { get; set; }
        public decimal? FareOT { get; set; }
        public decimal? OTTotal { get; set; }
        public decimal? Graduity { get; set; }
        public Int64? SubTotal { get; set; }
        public Int64? Payable { get; set; }
        public Int64? Payment { get; set; }
        public decimal? BasicSalary { get; set; }
        public string Empname1 { get; set; }
        public int id1 { get; set; }
        public string EmpName { get; set; }
        public decimal Amount { get; set; }
        public List<CompanyItem> lstCompany = new List<CompanyItem>();
        public List<EmployeeItem> lstEmployee = new List<EmployeeItem>();
        public int TotalDays { get; set; }
        public EmployeeItem EmployeeItem { get; set; }
        public EmpAllowncesItem EmpAllowItem { get; set; }
        public int CompId { get; set; }
        public List<PayrollItem> lstPayroll { get; set; }
        [Display(Name = "")]
        public string Message { get; set; }

        public decimal? DA { get; set; }
        public string CompName { get; set; }
        public string MonthName { get; set; }
        public string EmpNo { get; set; }
    }

}
