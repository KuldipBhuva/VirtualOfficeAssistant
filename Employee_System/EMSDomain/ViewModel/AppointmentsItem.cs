using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel
{
    [Serializable]
    public class AppointmentsItem
    {
        public int Viewbagidformenu { get; set; }
        public int ID { get; set; }
        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Status Required")]
        public string Status { get; set; }       
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Start Date Required")]
        [Display(Name = "*Start :")]
        public Nullable<System.DateTime> StDt { get; set; }
        [Required(ErrorMessage = "Start Time Required")]

        public string StTime { get; set; }
        [Required(ErrorMessage = "End Date Required")]
        [Display(Name = "*End :")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> EndDt { get; set; }
        [Required(ErrorMessage = "End Time Required")]
        public string EndTime { get; set; }
        [Required(ErrorMessage = "Priority Required")]
        public string Priority { get; set; }
        public string Reminder { get; set; }
        [Display(Name = "*Reminder :")]
        public Nullable<System.DateTime> ReDate { get; set; }
        public string ReTime { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> CompID { get; set; }

        public List<AppointmentsItem> ListAppointment { get; set; }
        public List<CompanyItem> ListCompany { get; set; }
        public List<BranchItem> ListBranch { get; set; }
    }
}
