using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;

namespace EMSDomain.ViewModel.Menu
{
    public class MenuItem
    {
        public bool IsChecked { get; set; }
        public int? MHId { get; set; }
        public string MHName { get; set; }
        public string MHDescription { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; }

        public int MSId { get; set; }
        public Nullable<int> FormId { get; set; }
        
        public string Sequence { get; set; }

        public List<MenuItem> MenuItemList { get; set; }
        public MenuItem ModulesDetails { get; set; }
        public UserModulesItem UMItem { get; set; }
    }
}
