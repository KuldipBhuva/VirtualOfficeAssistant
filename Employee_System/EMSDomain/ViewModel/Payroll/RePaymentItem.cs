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
    public class RePaymentItem
    {
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Please Select Employee")]
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Enter Payment(Amt.)")]
        public Nullable<decimal> Payment { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "* Month")]
        [Required(ErrorMessage = "Please Select Month")]
        public Nullable<int> Month { get; set; }
        [Display(Name = "* Year")]
        [Required(ErrorMessage = "* Please Select Year")]
        public Nullable<int> Year { get; set; }
        [Display(Name = "Company")]
        //[Required(ErrorMessage = "Please Select Company")]
        public Nullable<int> CompId { get; set; }
        public List<CompanyItem> ListComp { get; set; } //For comp DDL
        public List<EmployeeItem> ListEmp { get; set; } //For comp DDL
        public List<RePaymentItem> ListRePayment { get; set; } //For comp DDL
        public decimal? Rs { get; set; }
        public string CompName { get; set; }
        public string EmpName { get; set; }

        public string MonthName { get; set; }
        public decimal? AdPay { get; set; }

        public int Installment { get; set; }
    }
}
