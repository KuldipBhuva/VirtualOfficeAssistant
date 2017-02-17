using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel;


namespace EMSDomain.ViewModel
{
    public class PartyModel
    {
        public int Viewbagidformenu { get; set; }
        public int PID { get; set; }

        [Display(Name = "Branch :")]
        public Nullable<int> PPID { get; set; }
        [Required(ErrorMessage = "Party Name Required")]
        [Display(Name = "Party Name :*")]
        public string PartyName { get; set; }
        [Required(ErrorMessage = "Contact Person Required")]

        [Display(Name = "Contact Person:*")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Mobile Required")]

        [Display(Name = "Mobile:*")]
        public Nullable<decimal> Mobile { get; set; }
        [Required(ErrorMessage = "Phone Required")]

        [Display(Name = "Phone:*")]
        public Nullable<decimal> Phone { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Address Required")]

        [Display(Name = "Address:*")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Billing Address Required")]

        [Display(Name = "Billing Address:*")]
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "Billing Name Required")]

        [Display(Name = "Billing Name:")]
        public string BillingName { get; set; }
        [Display(Name = "Billing Phone:")]
        public Nullable<decimal> BillingPhone { get; set; }
        [Display(Name = "Status")]
        public Nullable<bool> Status { get; set; }
        [Display(Name = "Company")]
        public Nullable<int> CompID { get; set; }
        [Required(ErrorMessage = "Select Company Name")]
        public List<CompanyItem> CompanyList { get; set; }
        public List<PartyModel> ListParty { get; set; }
        public List<PartyModel> ListPartyDrp { get; set; }
    }
}
