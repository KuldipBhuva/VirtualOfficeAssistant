using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Menu
{
    public class UserModulesItem
    {
        public bool? IsChecked { get; set; }
        public int UHTID { get; set; }
        public Nullable<int> MHID { get; set; }
        public string Status { get; set; }
        public Nullable<int> CeatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UID { get; set; }
        public List<UserModulesItem> ListModules { get; set; }
        public List<UserModulesItem> MenuItemList { get; set; }
        public MenuItem MenuHeaderDetails { get; set; }
        
    }
}
