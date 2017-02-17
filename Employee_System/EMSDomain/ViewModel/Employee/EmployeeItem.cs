using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmployeeItem
    {        
        public string Filters { get; set; }
        public DateTime? Before { get; set; }
        public Nullable<int> withinDays { get; set; }
        public int id { get; set; }
        public int Viewbagidformenu { get; set; }
        [Display(Name = "Employee No")]
        public string Empno { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        [Display(Name = "Employee Name")]
        public string Empname { get; set; }

        //  public Nullable<int> Compid { get; set; }
        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Please Select Company")]
        public int? Compid { get; set; }
        
        [Display(Name = "Branch Name")]
        public int? Branchid { get; set; }
        public Nullable<int> vcid { get; set; }
        public Nullable<int> vbid { get; set; }
        [Required(ErrorMessage = "Please Select Employee Type")]
        [Display(Name = "Type")]
        public string EmployeeType { get; set; }
        public Nullable<int> bloodid { get; set; }
        public string Photo { get; set; }
        [Display(Name = "Alias")]
        public string alias { get; set; }
        [Display(Name = "File Number")]
        public string FileNumber { get; set; }
       
        [Display(Name = "Basic Salary")]
        public Nullable<decimal> BasicSalary { get; set; }
        public string Category { get; set; }


        //[Required(ErrorMessage = "Please Enter Mobile No. ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }
        [Display(Name = "Home Phone")]
        public string HomeTel { get; set; }
        [Display(Name = "Local Phone")]
        public string LocalTel { get; set; }
        //[Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [Required(ErrorMessage = "Please Enter DOJ")]
        [Display(Name = "Date of Joining")]
        public Nullable<System.DateTime> DOJ { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Date of Leave")]
        public Nullable<System.DateTime> DOL { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Re-join Date")]
        public Nullable<System.DateTime> RejoinDate { get; set; }
        [Display(Name = "Due Date")]
        public Nullable<System.DateTime> DueDate { get; set; }
        public string Status { get; set; }

        [DataType(DataType.MultilineText)]
        public string Profile { get; set; }

        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [Display(Name = "Date of Birth")]
        public Nullable<System.DateTime> DOB { get; set; }
        public int? JobTitle { get; set; }
        [Display(Name = "Date of Discontinue")]
        public Nullable<System.DateTime> DOD { get; set; }
        [Display(Name = "Working Status")]
        public Nullable<int> WorkingStatus { get; set; }
        public string EmiratesID { get; set; }
        public string Labour{ get; set; }
        public string Health { get; set; }
        public string DrivingLicense { get; set; }
        public string CompName { get; set; }
        public string BranchName { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassIDate { get; set; }
        public DateTime? PassEDate { get; set; }
        public string UID { get; set; }
        public string VisaNo { get; set; }
        public DateTime? VisaIDate { get; set; }
        public DateTime? VisaExpDate { get; set; }
        public string JobRole { get; set; }

        [Required(ErrorMessage = "Select Company Name")]
        public List<CompanyItem> CompanyList { get; set; }
        [Required(ErrorMessage = "Select Company Name")]
        public List<BranchItem> BranchList { get; set; }
        public Nullable<decimal> Graduity { get; set; }
        public int Days { get; set; }
        public decimal? OT { get; set; }
        public string Message { get; set; }
        public decimal? DA { get; set; }

        public List<EmployeeItem> EmployeeList { get; set; }
        public CompanyItem CompDetails { get; set; }
        public List<CompanyItem> ListComp { get; set; }
        public List<clsMasterData> listMaster { get; set;}
        public List<clsMasterData> listMasterWS { get; set; }
        public BranchItem BranchDetails { get; set; }
        public clsMasterData DesignationDetails { get; set; }
        public clsMasterData NationDetails { get; set; }
        public EmpVisaItem VisaDetails { get; set; }
        public EmpPassportItem PassDetails { get; set; }
        public EmpHealthItem HealthDetails { get; set; }
        public EmpHealthItem LabourDetails { get; set; }
        public EmpHealthItem EIDDetails { get; set; }
        public EmpHealthItem DLDetails { get; set; }
    }
}
