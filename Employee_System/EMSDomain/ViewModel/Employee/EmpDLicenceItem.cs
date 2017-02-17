using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpDLicenceItem
    {
        public int Viewbagidformenu { get; set; }
        public int DLId { get; set; }
        public int id { get; set; }
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Enter Licence Number")]
        [Display(Name = "Licence No")]
        public string LicenceNo { get; set; }
        [Required(ErrorMessage = "Please Select IssueDate")]
        [Display(Name = "Issue Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Required(ErrorMessage = "Please Select ExpiryDate")]
        [Display(Name = "Expiry Date")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        [Required(ErrorMessage = "Please Enter Country")]
        [Display(Name = "Issue Country")]
        public string IssueCountry { get; set; }
        [Display(Name = "Remarks")]
        public string DRemarks { get; set; }
        [Display(Name = "Status")]
        public string DStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpDLicenceItem> ListLicence { get; set; }
        public List<EmpDLicenceItem> ListLicence30 { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
        public List<EmpDLicenceItem> ListLicenceDoc { get; set; }        
    }
}
