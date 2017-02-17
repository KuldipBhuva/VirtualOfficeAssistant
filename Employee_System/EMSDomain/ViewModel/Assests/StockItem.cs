using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Assests
{
   public class StockItem
    {
        public int StockId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<decimal> IssueQty { get; set; }
        public Nullable<System.DateTime> IssueDate { get; set; }
        public Nullable<decimal> ReturnQty { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompID { get; set; }
    }
}
