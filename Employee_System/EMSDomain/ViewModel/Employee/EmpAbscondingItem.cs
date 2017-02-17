using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpAbscondingItem
    {
        public int Viewbagidformenu { get; set; }
        public int AbscId { get; set; }
        public int AId { get; set; }
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Enter Absconding History")]
        [Display(Name = "Absconding History")]
        public string AbscHistory { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpAbscondingItem> ListAbsconding { get; set; }
        public List<EmpAbscondingItem> ListAbscondingDoc { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
    }
}
