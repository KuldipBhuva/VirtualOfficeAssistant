using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Expenses
{
    public class TExpenseItem
    {
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Company Required")]
        public Nullable<int> Companyid { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date Required")]
        public Nullable<System.DateTime> TDate { get; set; }
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Employee Required")]
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Country Required")]
        public string Country { get; set; }
        [Display(Name = "Travelling From")]
        [Required(ErrorMessage = "Travelling From Date Required")]
        public Nullable<System.DateTime> TFromDate { get; set; }
        [Display(Name = "Travelling To")]
        [Required(ErrorMessage = "Travelling To Date Required")]
        public Nullable<System.DateTime> TToDate { get; set; }
        public Nullable<decimal> Fare { get; set; }
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "From Date Required")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "To Date Required")]
        [Display(Name = "To Date")]
        public Nullable<System.DateTime> ToDate { get; set; }
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
        [Display(Name = "Accomadation Exp.")]
        public Nullable<decimal> AccomodationExp { get; set; }
        [Display(Name = "Food Exp.")]
        public Nullable<decimal> FoodExp { get; set; }
        [Display(Name = "Convenyance Exp.")]
        public Nullable<decimal> Conv_Exp { get; set; }
        [Display(Name = "Other Exp.")]
        public Nullable<decimal> OtherExp { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }


        public CompanyItem CompDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public List<TExpenseItem> ListTExp { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public List<EmployeeItem> ListEmp { get; set; }
        public List<clsMasterData> ListCon { get; set; }
    }
}
