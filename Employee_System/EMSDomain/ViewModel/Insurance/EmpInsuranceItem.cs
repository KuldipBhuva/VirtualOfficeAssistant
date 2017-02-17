using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;

namespace EMSDomain.ViewModel.Insurance
{
    public class EmpInsuranceItem
    {
        public int Viewbagidformenu { get; set; }
        public int IID { get; set; }
        [Required(ErrorMessage = "Company Required")]
        [Display(Name = "*Company :")]
        public Nullable<int> CompID { get; set; }
        [Required(ErrorMessage = "Policy Name Required")]
        [Display(Name = "*Policy Name :")]
        public string Pname { get; set; }
        [Required(ErrorMessage = "Insurance Company Required")]
        [Display(Name = "*Ins. Company:")]
        public Nullable<int> Icomp { get; set; }
        [Required(ErrorMessage = "Insurance Person Required")]
        [Display(Name = "*Insurance Person :")]
        public string Iperson { get; set; }
        [Required(ErrorMessage = "Policy Type Required")]
        [Display(Name = "*Policy Type :")]
        public Nullable<int> Ptype { get; set; }
        [Required(ErrorMessage = "Policy No. Required")]
        [Display(Name = "*Policy No. :")]
        public string Pno { get; set; }
        [Required(ErrorMessage = "Policy Amount Required")]
        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Amount must be a Numbers only.")]
        [Display(Name = "*Policy Amount :")]
        public string Pamt { get; set; }
        [Required(ErrorMessage = "Policy Date Required")]                
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "*Policy Date :")]
        public Nullable<DateTime> Pdate { get; set; }
        [Required(ErrorMessage = "Policy Expiry Date Required")]
        [Display(Name = "*Policy Expiry Date :")]
        public Nullable<System.DateTime> PExpDate { get; set; }
        [Display(Name = " Branch :")]
        public Nullable<int> Branch { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = " Remarks :")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Employee Required")]
        [Display(Name = " Employee :")]
        public int? EmpId { get; set; }
        [Display(Name = "Premium Date :")]
        public Nullable<System.DateTime> PremiumDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Required(ErrorMessage = "Vehicle Required")]
        [Display(Name = "* Vehicle :")]
        public Nullable<int> VID { get; set; }

        public List<EmpInsuranceItem> ListInsurance { get; set; }
        public List<clsMasterData> ListMasterTable { get; set; } // For DropDown Insurance Company 
        public clsMasterData MasterDetails { get; set; }
        public clsMasterData MasterDetailsType { get; set; }
        public List<clsMasterData> ListPolicyType { get; set; }   // For DropDown ptype
        public List<BranchItem> ListBranch { get; set; }  //For Branch DDL
        public BranchItem BranchDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public List<CompanyItem> ListComp { get; set; } //For comp DDL
        public List<EmployeeItem> ListEmp { get; set; }
        public CompanyItem CompDetails { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        
    }
}
