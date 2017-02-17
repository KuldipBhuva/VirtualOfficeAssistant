using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel
{
    public class SponsorItem
    {
        public int Viewbagidformenu { get; set; }
        public int SID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PassportNo { get; set; }
        public string EmiratesIDNo { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<int> Branch_ID { get; set; }

        public List<SponsorItem> ListSponsor { get;set; }
        public List<CompanyItem> ListCompany { get; set; }
        public List<BranchItem> ListBranch { get; set; }
    }
}
