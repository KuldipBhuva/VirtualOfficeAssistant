using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    public class EmpOtherItem
    {
        public int Viewbagidformenu { get; set; }
        public int OtherId { get; set; }
        public Nullable<int> EmpId { get; set; }
        public string AwardName { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> AwardDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<EmpOtherItem> ListOther { get; set; }
        public EmployeeItem EmpDetails { get; set; }

    }
}
