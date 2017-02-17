using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    public class CountryItem
    {
        public int Viewbagidformenu { get; set; }
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        public string Id { get; set; }
    }
}
