using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel
{
    [Serializable]
    public class ClsMasterP
    {
        public int Viewbagidformenu { get; set; }
        public int MId { get; set; }
        [Required]
        [Display(Name = "Master Type")]
        public string MasterType { get; set; }
        [Display(Name = "Description")]
        public string MasterDesc { get; set; }
        [Display(Name = "Code")]
        public string MasterCode { get; set; }
        public List<ClsMasterP> MasterList { get; set; }
    }
}
