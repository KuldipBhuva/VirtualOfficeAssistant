using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpPassportItem
    {
        public string Filters { get; set; }
        public DateTime? Before { get; set; }
        public Nullable<int> withinDays { get; set; }
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        public int PasId { get; set; }
        public Nullable<int> EmpId { get; set; }
        public Nullable<int> MasterId { get; set; }
        public Nullable<int> PassportStatusId { get; set; }
        public Nullable<int> PassportID { get; set; }
        [Required(ErrorMessage = "Please Enter Passport No")]
        [Display(Name = "Passport No")]
        public string PassportNo { get; set; }
        [Required(ErrorMessage = "Please Enter Place of Issue")]
        [Display(Name = "Issue Place")]
        public string PlaceIssue { get; set; }
        [Required(ErrorMessage = "Please Enter Country of Issue")]
        [Display(Name = "Issue Country")]
        public Nullable<int> IssueCountry { get; set; }
        [Required(ErrorMessage = "Please Select Date of Issue")]
        [Display(Name = "Issue Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Please Select Date of Expiry")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [Display(Name = "Renew Date")]
        
        public Nullable<System.DateTime> RenewDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public int Nationality { get; set; }
        [Display(Name = "Passport No")]
        public string OPassportNo { get; set; }
        [Display(Name = "Issue Place")]
        public string OPlaceIssue { get; set; }
        public Nullable<int> OIssueCountry { get; set; }
        public Nullable<System.DateTime> OIssueDate { get; set; }
        public Nullable<System.DateTime> OExpiryDate { get; set; }
        public Nullable<System.DateTime> ORenewDate { get; set; }
        public string ORemarks { get; set; }
        public Nullable<int> ONationality { get; set; }

        //public virtual employee_master employee_master { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IssueDate < ExpiryDate)
            {
                yield return new ValidationResult("IssueDate must be greater than ExpiryDate");
            }
        }

        public List<EmpPassportItem> ListPassport { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public CompanyItem EmpComp { get; set; }
        public BranchItem CompBranch { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
        public List<EmpPassportItem> ListPassportDoc { get; set; }
        public CompanyItem CompDetails { get; set; }
        public List<EmpPassportItem> ListPassRpt { get; set; }
        public List<clsMasterData> ListCountry { get; set; }
        public List<clsMasterData> ListNationality { get; set; }
        public clsMasterData CountryDetails { get; set; }
    }
}
