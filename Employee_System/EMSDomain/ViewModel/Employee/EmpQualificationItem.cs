using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    public class EmpQualificationItem
    {
        public int Viewbagidformenu { get; set; }
        public int Qid { get; set; }
        public int VQid { get; set; }
        public Nullable<int> Empid { get; set; }
        [Required(ErrorMessage = "School Name Required")]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        [Required(ErrorMessage = "Board Name Required")]        
        public string Board { get; set; }
        [Required(ErrorMessage = "From Date Required")]
        [Display(Name = "From Date")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "To Date Required")]
        [Display(Name = "To Date")]
        public Nullable<System.DateTime> ToDate { get; set; }
        [Display(Name = "Certificate")]
        public string CertType { get; set; }
        [Display(Name = "Certificate Title")]
        public string CertTitle { get; set; }
        [Required(ErrorMessage = "Percentage Required")]
        public Nullable<decimal> Percentage { get; set; }
        public string Grade { get; set; }
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Certificate Type Required")]
        [Display(Name = "Certificate Type")]
        public Nullable<int> CertificateID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public EmployeeItem EmpDetails { get; set; }
        public List<clsMasterData> ListMasterDetails { get; set; }
        public List<EmpQualificationItem> ListQualification { get; set; }
        public List<EmpQualificationItem> ListQualificationDoc { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }

    }
}
