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
    public class AdvancePaymentItem
    {
        public int Viewbagidformenu { get; set; }

        public int Id { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Please Select Company")]
        public Nullable<int> CompId { get; set; }
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Please Select Employee")]
        public Nullable<int> EmpId { get; set; }
        [Display(Name = "Advance Amt.")]
        [Required(ErrorMessage = "Please Enter Amt.")]
        public Nullable<decimal> AdvAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Year { get; set; }

        public List<CompanyItem> ListComp { get; set; } //For comp DDL
        public List<EmployeeItem> ListEmp { get; set; } //For comp DDL
        public List<AdvancePaymentItem> ListAdvPayment { get; set; } //For comp DDL

        public string CompName { get; set; }
        public string EmpName { get; set; }


    }
}
