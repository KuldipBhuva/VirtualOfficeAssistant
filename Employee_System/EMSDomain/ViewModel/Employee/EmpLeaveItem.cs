using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    public class EmpLeaveItem
    {
        public int Viewbagidformenu { get; set; }
        public int LID { get; set; }
        public int LeaveId { get; set; }
        public Nullable<int> EmpID { get; set; }
        [Required(ErrorMessage = "From Date Required")]
        [Display(Name="Leave From :")]
        public Nullable<System.DateTime> LFrom { get; set; }
        [Required(ErrorMessage = "To Date Required")]
        [Display(Name="Leave To :")]
        public Nullable<System.DateTime> LTo { get; set; }
        [Required(ErrorMessage = "Leave Type Required")]
        [Display(Name="Leave Type :")]
        public Nullable<int> LTypeID { get; set; }
        [Display(Name = "Remarks :")]
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "Status :")]
        public string Status { get; set; }
        public List<EmpLeaveItem> ListLeave { get; set; }
        public EmployeeItem EmpDetails { get; set; } //for employee name
       
        public List<clsMasterData> ListMasterTable { get; set; } // For DropDown Allowances
        public clsMasterData MasterDetails { get; set; } 
    }
}
