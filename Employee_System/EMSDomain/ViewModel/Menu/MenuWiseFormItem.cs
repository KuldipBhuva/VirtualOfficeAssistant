using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Menu
{
    public class MenuWiseFormItem
    {
        public int FMTID { get; set; }
        public Nullable<int> FormId { get; set; }
        public Nullable<int> MHID { get; set; }
        public string Sequence { get; set; }
    }
}
