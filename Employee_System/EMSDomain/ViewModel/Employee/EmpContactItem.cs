using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpContactItem
    {
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        public int Cid { get; set; }
        public Nullable<int> EmpId { get; set; }
        [Required(ErrorMessage = "Area Required")]
        public string Area { get; set; }
        [Required(ErrorMessage = "Street Required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "City Required")]
        public string City { get; set; }
        public string PoBox { get; set; }
        [Required(ErrorMessage = "PinCode Required")]
        public string Pincode { get; set; }
        [Required(ErrorMessage = "State Required")]
        public string State { get; set; }
        [Required(ErrorMessage = "Country Required")]
        public int Country { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpContactItem> ListContactdetails { get; set; }
        public List<EmpContactItem> ListContactdetailsDoc { get; set; }
        public EmployeeItem EmployeeDetails { get; set; }
        public List<CountryItem> CountryList { get; set; }
        public EMSDomain.ViewModel.DocumentItem DocDetails { get; set; }
        public List<EMSDomain.ViewModel.DocumentItem> DocList { get; set; }
        public List<clsMasterData> ListMaster { get; set; }
        public clsMasterData ConDetails { get; set; }
    }
}
