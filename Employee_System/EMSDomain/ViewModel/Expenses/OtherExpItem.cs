using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Expenses
{
    public class OtherExpItem
    {
        public int Viewbagidformenu { get; set; }
        public int OEID { get; set; }
        [Display(Name = "Company")]
        [Required(ErrorMessage = "Company Required")]
        public Nullable<int> CompID { get; set; }
        [Display(Name = "Branch")]
        [Required(ErrorMessage = "Branch Required")]
        public Nullable<int> BranchID { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date Required")]
        public Nullable<System.DateTime> Date { get; set; }
        [Display(Name = "Employee")]
        [Required(ErrorMessage = "Employee Required")]
        public Nullable<int> EmpID { get; set; }
        [Display(Name = "Expense")]
        [Required(ErrorMessage = "Expense Required")]
        public Nullable<int> ExpID { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        public Nullable<decimal> Amount { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public CompanyItem CompDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public BranchItem BranchDetails { get; set; }
        public clsMasterData MasterDetails { get; set; }
        public List<OtherExpItem> ListOExp { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public List<EmployeeItem> ListEmp { get; set; }
        public List<BranchItem> ListBranch { get; set; }
        public List<clsMasterData> ListMaster { get; set; }
    }
}
