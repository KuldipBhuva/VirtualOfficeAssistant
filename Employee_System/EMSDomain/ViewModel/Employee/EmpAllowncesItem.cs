using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpAllowncesItem
    {
        public int Viewbagidformenu { get; set; }
        public int AId { get; set; }
        public int AlwId { get; set; }
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Select Allowances")]
        [Display(Name = "Allowances")]
        public Nullable<int> AllownaceID { get; set; }

        [Required(ErrorMessage = "Please Enter Amount")]
        public Nullable<decimal> Amount { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpAllowncesItem> ListAllownces { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public List<clsMasterData> ListMasterTable { get; set; } // For DropDown Allowances
        public clsMasterData MasterDetails { get; set; } // For DropDown Allowances
    }
}
