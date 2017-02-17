using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel
{
    public class CredentialItem
    {
        public int Viewbagidformenu { get; set; }
        public int CrID { get; set; }
        [Required(ErrorMessage = "Username Required")]
        [Display(Name = "* Username :")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "* Password :")]
        public string Password { get; set; }
        public string AssignTo { get; set; }
        public string Remarks { get; set; }
        public string URL { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public Nullable<int> CompID { get; set; }

        public List<CredentialItem> ListCredential { get; set; }
        public List<CompanyItem> ListCompany { get; set; }
        public List<BranchItem> ListBranch { get; set; }
    }
}
