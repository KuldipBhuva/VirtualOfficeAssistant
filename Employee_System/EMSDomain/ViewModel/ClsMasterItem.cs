using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel
{
    public class ClsMasterItem
    {
        public int MId { get; set; }
        [Required]
        public string MasterType { get; set; }
        public string MasterDesc { get; set; }
        public string MasterCode { get; set; }
        public List<ClsMasterP> MasterList { get; set; }
    }
}
