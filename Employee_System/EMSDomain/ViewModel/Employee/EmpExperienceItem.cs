using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpExperienceItem
    {
        public string modeView { get; set; }
        public string modeEdit { get; set; }
        public int Viewbagidformenu { get; set; }
        public int VEmpId { get; set; }
        public int ExpId { get; set; }
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Please Enter Company Name")]
        [Display(Name = "Company")]
        public string CompName { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        [Required(ErrorMessage = "Please Enter Contact Person Name")]
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        [Required(ErrorMessage = "Please Enter Your Role")]
        [Display(Name = "Role")]
        public string Roles { get; set; }
        [Required(ErrorMessage = "Please Select From-Date")]
        [Display(Name = "From Date")]
        public Nullable<System.DateTime> FromDate { get; set; }
        [Required(ErrorMessage = "Please Select To-Date")]
        [Display(Name = "To Date")]
        public Nullable<System.DateTime> ToDate { get; set; }
        [Required(ErrorMessage = "Please Enter Department")]
        public string Department { get; set; }
        [Required(ErrorMessage = "Please Enter Designation")]
        public string Designation { get; set; }
        [Required(ErrorMessage = "Please Enter Salary")]
        public Nullable<decimal> Salary { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpExperienceItem> ListExperience { get; set; }
        public List<EmpExperienceItem> ListExperienceDoc { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
        public List<clsMasterData> ListCountry { get; set; }
    }
}
