using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Branch
{
    [Serializable]
    public class BranchItem
    {
        public int Viewbagidformenu { get; set; }
        public int id { get; set; }
        [Required(ErrorMessage = "Enter Company Name", AllowEmptyStrings = false)]
        [Display(Name = "Company")]
        //public string Compcode { get; set; }
        public int CompID { get; set; }
        public string BranchCode { get; set; }
        [Required(ErrorMessage = "Enter Branch Name", AllowEmptyStrings = false)]
        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Address", AllowEmptyStrings = false)]
        public string BranchAdd { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Enter City", AllowEmptyStrings = false)]
        public string City { get; set; }
        [Required(ErrorMessage = "Enter State", AllowEmptyStrings = false)]
        public string State { get; set; }
        [Required(ErrorMessage = "Enter Country", AllowEmptyStrings = false)]
        public string Country { get; set; }

        public CompanyItem CompDetails { get; set; }
        public List<BranchItem> ListBranch { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public List<clsMasterData> ListCon { get; set; }
    }
}
