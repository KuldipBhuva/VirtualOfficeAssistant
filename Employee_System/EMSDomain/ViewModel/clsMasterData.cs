using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;

namespace EMSDomain.ViewModel
{
    [Serializable]
    public class clsMasterData
    {
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Master Type:")]
        public Nullable<int> MasterId { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        //public Master Master1 { get; set; }
        public List<ClsMasterP> maserlist { get; set; }
        public ClsMasterP MastersAll { get; set; }
        public List<clsMasterData> MasterDataList { get; set; }
    }
}
