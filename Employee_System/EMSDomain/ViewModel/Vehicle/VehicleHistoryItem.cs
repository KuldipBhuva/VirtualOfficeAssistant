using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Vehicle
{
    public class VehicleHistoryItem
    {
        public int Viewbagidformenu { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Vehicle Required")]
        [Display(Name = "Vehicle :*")]
        public Nullable<int> VID { get; set; }
        [Required(ErrorMessage = "From Date Required")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: d}")]
        [Display(Name = "From Date :*")]
        public Nullable<System.DateTime> From_Date { get; set; }
        //[DataType(DataType.Date)]
        [Required(ErrorMessage = "To Date Required")]
        [Display(Name = "To Date :*")]
        public Nullable<System.DateTime> To_Date { get; set; }
        [Required(ErrorMessage = "Employee Required")]
        [Display(Name = "Employee :*")]
        public Nullable<int> EmpID { get; set; }        
        [Display(Name = "Remark :")]
        public string Remarks { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public VehicleItem VehicleDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public List<VehicleHistoryItem> ListVH { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public List<EmployeeItem> ListEmp { get; set; }
    }
}
