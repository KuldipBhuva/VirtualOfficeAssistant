using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.TradeLicense
{
    [Serializable]
    public class TradeDocumentItem
    {
        public int TradeDOcId { get; set; }
        public Nullable<int> TradeId { get; set; }
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<TradeDocumentItem> ListTradeDoc { get; set; }
    }
}
