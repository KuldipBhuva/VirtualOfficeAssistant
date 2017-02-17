using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Employee
{
    public class DocumentItem
    {
        public int Viewbagidformenu { get; set; }
        public List<EmpDesignationItem> DesignationList { get; set; }
        public List<DocumentItem> ListDoc { get;set;}
     }
}
