using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Vehicle
{
    public class PetrolExpItem
    {
        public int Viewbagidformenu { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Vehicle Required")]
        [Display(Name = "Vehicle :")]
        public Nullable<int> VID { get; set; }
        [Required(ErrorMessage = "Date Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bill Date :")]
        public Nullable<System.DateTime> Bill_Date { get; set; }
        [Required(ErrorMessage = "Employee Required")]
        [Display(Name = "Employee :")]
        public Nullable<int> EmpID { get; set; }
        [Required(ErrorMessage = "Quantity Required")]
        [Display(Name = "Quantity :")]
        public string Qty { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        [Display(Name = "Amount :")]
        public string Amount { get; set; }
        [Required(ErrorMessage = "KM From Required")]
        [Display(Name = "KM From :")]
        public string KM_From { get; set; }
        [Required(ErrorMessage = "KM To Required")]
        [Display(Name = "KM To :")]
        public string KM_To { get; set; }
        [Display(Name = "Remark :")]
        public string Remarks { get; set; }
        public List<PetrolExpItem> ListPCard { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public List<EmployeeItem> ListEmp { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public VehicleItem VehicleDetails { get; set; }        
    }
}
