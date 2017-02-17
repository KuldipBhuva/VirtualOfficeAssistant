using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Company
{
    [Serializable]
    public class CompanyItem
    {
        public int Viewbagidformenu { get; set; }
        public int id { get; set; }
        [Display(Name = "Company Code")]
        public string Compcode { get; set; }
        [Required(ErrorMessage = "Company Name Required")]
        [Display(Name = "Company Name")]
        public string CompName { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State Required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Country Required")]
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Sign { get; set; }

        public List<CompanyItem> ListCompany { get; set; }
        public List<clsMasterData> ConList { get; set; }
        
    }
}
