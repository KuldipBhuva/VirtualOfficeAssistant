using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;

namespace EMSDomain.ViewModel
{
    public class ChallanModel
    {
        public int Viewbagidformenu { get; set; }
        public int CID { get; set; }
        public string ChallanNo { get; set; }
        [Required(ErrorMessage = "Vehicle Required")]
        public Nullable<int> VID { get; set; }
        [Required(ErrorMessage = "Driver Required")]
        public Nullable<int> EmpID { get; set; }
        [Required(ErrorMessage = "Start Date/Time Required")]
        public Nullable<System.DateTime> StartDate { get; set; }
        [Required(ErrorMessage = "End Date/Time Required")]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Required(ErrorMessage = "Source Required")]
        public string Source { get; set; }
        [Required(ErrorMessage = "Destination Required")]
        public string Destination { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> CompID { get; set; }


        #region tran
        public int CTID { get; set; }
        //public Nullable<int> CID { get; set; }
        //[Required(ErrorMessage = "GR No. Required")]
        public Nullable<int> GRID { get; set; }
        public string GRCode { get; set; }
        #endregion

        public List<VehicleItem> ListVehicle { get; set; }
        public List<EmployeeItem> ListEmp { get; set; }
        public List<ConsignmentModel> ListGrNo { get; set; }
        public List<ChallanModel> ListChallan { get; set; }
        public List<ChallanModel> ListChallanTran { get; set; }

        public VehicleItem VehicleDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }

        public ChallanModel ChallanDetails { get; set; }
        public ConsignmentModel ConsignDetails { get; set; }

        public PartyModel ConsigneeDetails { get; set; }
        public PartyModel ConsignorDetails { get; set; }

    }
}
