using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Assests
{
    public class ReturnItem
    {
        public int ReId { get; set; }
        [Required(ErrorMessage = "Select a Company Name")]
        [Display(Name = "Company Name")]
        public Nullable<int> CompId { get; set; }
        [Required(ErrorMessage = "Select a Employee Name")]
        [Display(Name = "Employee Name")]
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Select a Item")]
        [Display(Name = "Item")]
        public Nullable<int> ItemId { get; set; }
        public Nullable<System.DateTime> ReDate { get; set; }
        [Required(ErrorMessage = "Enter Quantity")]
        [Display(Name = "Quantity")]
        public Nullable<decimal> Qty { get; set; }
        [Required(ErrorMessage = "Select Status of Amount")]
        [Display(Name = "Returnable Amount")]
        public string ReturnableAmt { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompID { get; set; }



        public List<CompanyItem> ListComp { get; set; } //For comp DDL
        public List<EmployeeItem> ListEmp { get; set; } //For Emp DDL
        public List<ReturnItem> lstReturnItem { get; set; } //For this DDL
        public List<AssestsItem> lstAllItem { get; set; }
        public string CompName { get; set; }
        public string EmpName { get; set; }
        public string ItemName { get; set; }
    }
}
