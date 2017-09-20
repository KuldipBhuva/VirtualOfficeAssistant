using EMSDomain.ViewModel.Company;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel
{
    public class ContractModel
    {
        public int Viewbagidformenu { get; set; }
        public int ContID { get; set; }
        [Required(ErrorMessage = "Company Required")]
        public Nullable<int> CompID { get; set; }
        [Required(ErrorMessage = "Contract For Required")]
        public string ContarctFor { get; set; }
        [Required(ErrorMessage = "From Date Required")]
        public Nullable<System.DateTime> FrmDate { get; set; }
        [Required(ErrorMessage = "To Date Required")]
        public Nullable<System.DateTime> ToDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        [Required(ErrorMessage = "Contract With Required")]
        public string ContractWith { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }


        public List<ContractModel> ListContract { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public CompanyItem CompDetails { get; set; }
    }
}
