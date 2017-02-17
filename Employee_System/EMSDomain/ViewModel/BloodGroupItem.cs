using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel
{
    public class BloodGroupItem
    {
        public int id { get; set; }
        public string BName { get; set; }
        public string BDescription { get; set; }
        public string BStatus { get; set; }
        public List<BloodGroupItem> bloodList { get; set; }
    }
}
