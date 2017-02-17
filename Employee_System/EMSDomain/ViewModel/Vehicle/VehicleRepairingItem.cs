using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Vehicle
{
    public class VehicleRepairingItem
    {
        public int Viewbagidformenu { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Company Required")]
        [Display(Name = "Company :")]        
        public Nullable<int> CompID { get; set; }
        [Required(ErrorMessage = "Vehicle Required")]
        [Display(Name = "Vehicle :")]
        public Nullable<int> VID { get; set; }
        //[Required(ErrorMessage = "Repairing Type Required")]
        [Display(Name = "Repairing Type :")]
        public Nullable<int> Repairing_TypeID { get; set; }
        [Required(ErrorMessage = "Expense/Recharge Date Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: d}")]
        [Display(Name = "Expense Date :")]
        public Nullable<System.DateTime> Rep_Date { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        [Display(Name = "Amount Paid :")]
        public string Amount_Paid { get; set; }
        [Display(Name = "Remarks :")]
        public string Remarks { get; set; }
        [Required(ErrorMessage = "Expense Type Required")]
        public Nullable<int> ExpType { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public List<VehicleRepairingItem> VehicleList { get; set; }
        public clsMasterData MasterDetails { get; set; }
        public CompanyItem CompanyDetails { get; set; }
        public VehicleItem VehicleDetails { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public List<clsMasterData> ListMaster { get; set; }
        public VehicleRepairingItem ListVRepairing { get; set; }
    }
}
