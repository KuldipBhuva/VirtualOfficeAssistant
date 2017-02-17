using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;

namespace EMSDomain.ViewModel.Employee
{
    public class EmpRelativeItem
    {
        public int Viewbagidformenu { get; set; }
        public int RId { get; set; }
        public int Id { get; set; }
        [Required]
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Enter Relative Name")]
        [Display(Name = "Name")]
        public string RName { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        [Display(Name = "Address")]
        public string RAddress { get; set; }
        [Required(ErrorMessage = "Please Enter City Name")]
        [Display(Name = "City")]
        public string RCity { get; set; }
        //[Required(ErrorMessage = "Please Enter Phone No.")]
        [Display(Name = "Phone")]
        public string RPhone { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email")]
        public string REmail { get; set; }
        [Required(ErrorMessage = "Please Enter Relation")]
        [Display(Name = "Relation")]
        public string Relation { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile")]
        [Display(Name = "Mobile")]
        public string RMobile { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }


        public List<EmpRelativeItem> ListRelative { get; set; }    // use List Object For Bind List grid
        public List<MasterDataItem> MasterDataList { get; set; }
        public EmployeeItem EmployeeDetails { get; set; } // use this class For Bind Employee Name in Gridview

        //public virtual employee_master employee_master { get; set; }
    }
}
