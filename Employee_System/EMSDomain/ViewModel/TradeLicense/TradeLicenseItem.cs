using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.TradeLicense
{
    [Serializable]
    public class TradeLicenseItem
    {

        //public int TID { get; set; }
        //public string Code { get; set; }
        //public string Lno { get; set; }
        public string Filters { get; set; }
        public DateTime? Before { get; set; }
        public Nullable<int> withinDays { get; set; }
        public int TID { get; set; }
        public int Viewbagidformenu { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Licence Number Required")]
        [Display(Name = "License No.")]
        public string Lno { get; set; }
        [Required(ErrorMessage = "Company Required")]
        [Display(Name = "Company Name")]
        public Nullable<int> CompID { get; set; }
        [Required(ErrorMessage = "Emirates Required")]
        [Display(Name = "Emirates")]
        public Nullable<int> EmiratesID { get; set; }
        //[Required(ErrorMessage = "Sponsor Required")]
        [Display(Name = "Sponsor")]
        public Nullable<int> SponsorID { get; set; }
        [Required(ErrorMessage = "Partner Required")]
        [Display(Name = "Partner 1")]
        public Nullable<int> PartnerID { get; set; }
        [Display(Name = "Partner 2")]
        public Nullable<int> PartnerID2 { get; set; }
        [Display(Name = "Partner 3")]
        public Nullable<int> PartnerID3 { get; set; }
        [Display(Name = "Partner 4")]
        public Nullable<int> PartnerID4 { get; set; }
        public Nullable<decimal> Fees { get; set; }
        [Required(ErrorMessage = "Issue Date Required")]
        [Display(Name = "Issue Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Required(ErrorMessage = "Expiry Date Required")]
        [Display(Name = "Expiry Date")]
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Required(ErrorMessage = "Type Required")]
        [Display(Name = "License Type")]
        public Nullable<int> LicenseTypeID { get; set; }


        public List<CompanyItem> lstCompany { get; set; } // For Company Bind dropdown
        public List<CompanyItem> lstSponsor { get; set; } //For Sponsor Bind dropdown
        public List<clsMasterData> lstEmirates { get; set; } // For Emirates Bind
        public List<clsMasterData> ListLicType { get; set; } // For Lic. Type Bind
        public List<clsMasterData> lstParter { get; set; } // For Partener Bind
        public CompanyItem CompnyDetails { get; set; }
        public clsMasterData MEmiratesDetails { get; set; }
        public clsMasterData MPartnerDetails { get; set; }
        public clsMasterData LicTypeDetails { get; set; }
        public List<TradeLicenseItem> LstTradeLicense { get; set; }
        public List<SponsorItem> ListSponsor { get; set; }

        public CompanyItem CompDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        //public List<TradeLicenseItem> ListDL { get; set; }

        #region tradeDocument
        public int TradeDOcId { get; set; }
        public Nullable<int> TradeId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Status { get; set; }
        #endregion


    }
}
