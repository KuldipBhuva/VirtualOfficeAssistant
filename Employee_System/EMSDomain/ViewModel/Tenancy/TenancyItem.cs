using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Tenancy
{
    [Serializable]
    public class TenancyItem
    {
        public int Menuid { get; set; }
        public int Viewbagidformenu { get; set; }
        public int TnID { get; set; }
        [Display(Name = "Tenancy No")]
        [Required(ErrorMessage = "Tenancy Number Required")]
        public string TnNo { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Date Required")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> TDate { get; set; }
        public string LandLord { get; set; }
        public string Tn { get; set; }
        [Display(Name = "Tenancy Type")]
        public string TnType { get; set; }
        [Display(Name = "Select Company")]
        public Nullable<int> TnTypeCompId { get; set; }
        [Display(Name = "Select Employee")]
        public Nullable<int> TnTypeEmpId { get; set; }
        [Display(Name = "Tenancy Type")]
        [Required(ErrorMessage = "Tenancy Type Required")]
        public Nullable<int> TnTypeId { get; set; }
        public string Others { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Period { get; set; }
        [Display(Name = "From")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Display(Name = "To")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Rent { get; set; }
        public string Terms { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<CompanyItem> ListCompany { get; set; }
        public List<EmployeeItem> ListEmployee { get; set; }
        public List<TenancyItem> ListTenancy { get; set; }

        public CompanyItem compDetails { get; set; }
        public EmployeeItem Empdetails { get; set; }
        public List<clsMasterData> ListTT { get; set; }

        [Display(Name = "Select Employee")]
        public int? TenTypeEmpID { get; set; }

        public clsMasterData TTDetails { get; set; }
        #region for tenancy Documents
        public int TenDocId { get; set; }
        public Nullable<int> TenId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
       
        #endregion
    }
}
