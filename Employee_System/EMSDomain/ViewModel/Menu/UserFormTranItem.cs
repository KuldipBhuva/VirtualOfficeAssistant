using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Menu
{
    public class UserFormTranItem
    {
        public bool? IsChecked { get; set; }
        public int UMTID { get; set; }
        public Nullable<int> MID { get; set; }
        public Nullable<int> UID { get; set; }
        public Nullable<int> FormID { get; set; }
        public string Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatetdDate { get; set; }

        public FormItem FormDetails { get; set; }
    }
}
