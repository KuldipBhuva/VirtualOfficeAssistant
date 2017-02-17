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
    public class EmpHealthItem
    {
        public string Filters { get; set; }
        public DateTime? Before { get; set; }
        public Nullable<int> withinDays { get; set; }
        public int Viewbagidformenu { get; set; }
        public int HCId { get; set; }
        public int HId { get; set; }
        [Required]
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Select Type")]
        public string Type { get; set; }
        [Display(Name = "Card No")]
        [Required(ErrorMessage = "Please Enter Card No.")]
        public string CardNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [Required(ErrorMessage = "Please Select Issue Date")]
        [Display(Name = "Issue Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Required(ErrorMessage = "Please Select Expiry Date")]
        [Display(Name = "Expiry Date")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string EmiratesID { get; set; }
        [Display(Name = "Emirates Exp. Date")]
        public Nullable<System.DateTime> EmiratesExpDate { get; set; }
        public string PersonalNo { get; set; }
        public List<EmpHealthItem> ListHealth { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public CompanyItem CompDetails { get; set; }

        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
        public List<EmpHealthItem> ListHealthDoc { get; set; }

        public List<EmpHealthItem> ListLabourDoc { get; set; }
        public List<EmpHealthItem> ListEIDDoc { get; set; }
        public List<EmpDLicenceItem> ListLicenceDoc { get; set; }
        public List<EmpHealthItem> ListDL { get; set; }
        public List<EmpHealthItem> ListHealthRpt { get; set; }
        public List<EmpHealthItem> ListLabourRpt { get; set; }
        public List<EmpHealthItem> ListEIDRpt { get; set; }

    }
}
