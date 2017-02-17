using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Vehicle;

namespace EMSDomain.ViewModel
{
    public class ConsignmentModel
    {
        public int Viewbagidformenu { get; set; }
        public int GrNo { get; set; }
        [Required(ErrorMessage = "LR/Gate Pass No. Required")]
        public string LR { get; set; }
        //[Required(ErrorMessage = "Tax Paid By Required")]
        public Nullable<int> TaxPaidBy { get; set; }
        public string BillingName { get; set; }
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "Consigner Required")]
        public Nullable<int> ConsignorID { get; set; }
        [Required(ErrorMessage = "Consignee Required")]
        public Nullable<int> ConsigneeID { get; set; }
        [Required(ErrorMessage = "Source Required")]
        public string Source { get; set; }
        [Required(ErrorMessage = "Destination Required")]
        public string Destination { get; set; }
        [Required(ErrorMessage = "Packages Required")]
        public Nullable<decimal> Packages { get; set; }
        [Required(ErrorMessage = "Packing Required")]
        public string PackingMethod { get; set; }
        [Required(ErrorMessage = "Actual Weight Required")]
        public Nullable<decimal> ActualWeight { get; set; }
        [Required(ErrorMessage = "Charged Weight Required")]
        public Nullable<decimal> ChargedWeight { get; set; }
        [Required(ErrorMessage = "Rate Required")]
        public Nullable<decimal> Rate { get; set; }
        [Required(ErrorMessage = "Freight Charge Required")]
        public Nullable<decimal> FreightCharge { get; set; }
        public Nullable<decimal> LabourCharge { get; set; }
        public Nullable<decimal> OtherCharge { get; set; }
        public string InvoiceNo { get; set; }
        [Required(ErrorMessage = "Invoice Amount Required")]
        public Nullable<decimal> InvoiceAmount { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> Discount { get; set; }
        [Required(ErrorMessage = "Booking Date Required")]
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompID { get; set; }
        public Nullable<bool> Status { get; set; }

        [Required(ErrorMessage = "Delivery Mode Required")]
        public Nullable<int> DeliveryMode { get; set; }
        public Nullable<int> CompBrokerID { get; set; }
        [Required(ErrorMessage = "Consignee Invoice No Required")]
        public string ConsigneeInvoiceNo { get; set; }
        [Required(ErrorMessage = "Consignee Invoice Value Required")]
        public Nullable<decimal> ConsigneeInvoiceValue { get; set; }
        public string PickUp { get; set; }

        public List<ConsignmentModel> ListConsignment { get; set; }
        public List<PartyModel> ListConsigner { get; set; }
        public List<PartyModel> ListConsignee { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public PartyModel ConsigneeDetails { get; set; }
        public PartyModel ConsignorDetails { get; set; }
        public PartyModel CompBrokeDetails { get; set; }
        public List<ChallanModel> ListChallan { get; set; }
    }
}
