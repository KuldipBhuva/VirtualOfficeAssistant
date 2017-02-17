using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Employee
{
    public class EmpIncrementItem
    {
        public int Viewbagidformenu { get; set; }
        public int VIncId { get; set; }
        public int IncId { get; set; }
        public Nullable<int> EmpId { get; set; }

        [Required(ErrorMessage = "Please Enter Increment Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Increment Amount")]
        public Nullable<decimal> IncAmount { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Increment")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Increment Date")]
        public Nullable<System.DateTime> IncDate { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public EmployeeItem EmpDetails { get; set; }
        public List<EmpIncrementItem> ListIncrement { get; set; }

        public CompanyItem CompDetails { get; set; }
    }
}
