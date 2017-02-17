using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpVisaItem
    {
        public string Filters { get; set; }
        public DateTime? Before { get; set; }
        public Nullable<int> withinDays { get; set; }
        public int Viewbagidformenu { get; set; }
        public int VId { get; set; }
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Visa No. Required")]
        [Display(Name = "Visa No.")]
        public string VisaNo { get; set; }
        [Display(Name = "Visa Status")]
        [Required(ErrorMessage = "Visa Status Required")]
        public Nullable<int> VisaStatusId { get; set; }
        [Display(Name = "Visa Group")]
        public Nullable<int> VisaGroupId { get; set; }
        [Display(Name = "Visa Type")]
        public Nullable<int> VisaTypeId { get; set; }
        [Required(ErrorMessage = "Place of Issue Required")]
        [Display(Name = "Place of Issue")]
        public string PlaceOfIssue { get; set; }
        [Required(ErrorMessage = "Issue Country Required")]
        [Display(Name = "Issue Country")]
        public Nullable<int> IssueCountry { get; set; }
        [Required(ErrorMessage = "Issue Date Required")]
        [Display(Name = "Issue Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Required(ErrorMessage = "Expiry Date Required")]
        [Display(Name = "Expiry Date")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        
        [Display(Name = "Renew Date")]
        public Nullable<System.DateTime> ReniewDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "Emirates ID")]
        public Nullable<int> EmiratesId { get; set; }
        [Display(Name = "Emirates Exp. Date")]
        public Nullable<System.DateTime> EmiratesDate { get; set; }
        public string UID { get; set; }
        [Display(Name = "Company")]
        public Nullable<int> ComID { get; set; }
        public string Designation { get; set; }

        public List<EmpVisaItem> ListVisa { get; set; }    // use List Object For Bind List grid
        public EmployeeItem EmpDetails { get; set; }
        public CompanyItem CompDetails { get; set; }
        public List<clsMasterData> MasterDetails { get; set; }
        public List<clsMasterData> ListVisaType { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
        public List<EmpVisaItem> ListVisaDoc { get; set; }
        public List<clsMasterData> EmiratesList { get; set; } // bind Dropdown For Emirates
        public List<clsMasterData> ListCountry { get; set; }
        public clsMasterData ConDetails { get; set; }
    }
}
