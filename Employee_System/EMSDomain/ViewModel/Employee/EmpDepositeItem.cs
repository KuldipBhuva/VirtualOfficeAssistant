using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpDepositeItem
    {
        public int Viewbagidformenu { get; set; }
        public int DId { get; set; }
        public int DepId { get; set; }
        public Nullable<int> EmpId { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string DepDescription { get; set; }

        [Required(ErrorMessage = "Please enter Proper Amount")]
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Deposite Amount")]
        public Nullable<decimal> DepAmount { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Invest")]
        [Display(Name = "Invest Date")]
        public Nullable<System.DateTime> InvestDate { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Maturity")]
        [Display(Name = "Maturity Date")]
        public Nullable<System.DateTime> MaturityDate { get; set; }

        [Display(Name = "Remarks")]
        public string DepRemarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpDepositeItem> ListDeposit { get; set; }
        public EmployeeItem EmpDetails { get; set; }
    }
}
