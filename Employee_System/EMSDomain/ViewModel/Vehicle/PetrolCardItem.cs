using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Vehicle
{
    public class PetrolCardItem
    {
        public int ID { get; set; }
        //[Required(ErrorMessage = "Vehicle Required")]
        [Display(Name = "Vehicle :")]
        public Nullable<int> VID { get; set; }
        //[Required(ErrorMessage = "No. Required")]
        [Display(Name = "Petrol Card No. :")]
        public string PetroCardNo { get; set; }
        //[Required(ErrorMessage = "Petrol Card Required")]
        [Display(Name = "Petrol Card :")]
        public Nullable<int> PetrolCardNameID { get; set; }
        //[Required(ErrorMessage = "Payment Mode Required")]
        [Display(Name = "Payment Mode :")]
        public Nullable<int> PaymentModeID { get; set; }
        //[Required(ErrorMessage = "Issue Date Required")]
        [Display(Name = "Issue Date :")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        //[Required(ErrorMessage = "Vehicle Required")]
        [Display(Name = "Expiry Date :")]
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string Status { get; set; }
        public List<PetrolCardItem> ListPetrolCard { get; set; }
        public VehicleItem VehicleDetails { get; set; }
        public MasterDataItem MasterDetails { get; set; }
        public MasterDataItem MasterDetails1 { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public List<clsMasterData> ListMaster { get; set; } //For Payment Mode DDL
        public List<clsMasterData> ListMaster1 { get; set; } //for PCardType DDL
    }
}
