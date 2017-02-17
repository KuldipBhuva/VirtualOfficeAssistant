using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Employee
{
    public class EmpWorkHistoryItem
    {
        public int Viewbagidformenu { get; set; }
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public int CompId { get; set; }
        public string CompName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string Remarks { get; set; }
        public string Perfomance { get; set; }
        //[Required(ErrorMessage = "Select Company Name")]
        public List<CompanyItem> CompanyList { get; set; }
        //[Required(ErrorMessage = "Select Company Name")]
        public List<BranchItem> BranchList { get; set; }
        //[Required(ErrorMessage = "Please select designation")]
        public List<EmpDesignationItem> DesignationList { get; set; }
    }
}
