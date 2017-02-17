using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.TradeLicense;

namespace EMSDomain.ViewModel
{
    [Serializable]
    public class DocumentItem
    {
        //EmpDoc
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public string FileName { get; set; }
        [Required(ErrorMessage = "Select Document Type")]
        public int? DocCategoryId { get; set; }
        public string FileUrl { get; set; }

        //TenancyDoc
        public int TenDocId { get; set; }
        public Nullable<int> TenId { get; set; }
  
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        //TradeDoc
        public int TradeDOcId { get; set; }
        public Nullable<int> TradeId { get; set; }
       
        //VehicleDoc
        public int VDocId { get; set; }
        public Nullable<int> VId { get; set; }
        //Comp
        [Display(Name = "Company :")]
        public int compID { get; set; }

        public List<clsMasterData> ListDocumentMaster { get; set; }
        public List<EMSDomain.ViewModel.DocumentItem> ListDoc { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public List<CompanyItem> ListCompany { get; set; }
        public CompanyItem CompDetails { get; set; }
        public List<DocumentItem> ListTradeDoc { get; set; }
        public List<DocumentItem> ListTenancyDoc { get; set; }
        public List<DocumentItem> ListVehicleDoc { get; set; }
    }
}
