using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Menu;

namespace EMSDomain.ViewModel
{
    public class LoginItem
    
    {
        public int Viewbagidformenu { get; set; }
        public int UId { get; set; }
        [Required(ErrorMessage = "Role Required", AllowEmptyStrings = false)]
        [Display(Name = "*Role :")]
        public Nullable<int> RoleID { get; set; }
        [Required(ErrorMessage = "Username Required", AllowEmptyStrings = false)]
        [Display(Name = "*User Name :")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password Required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 4)]
        [Display(Name = "*Password :")]
        public string Password { get; set; }
        public string CompCode { get; set; }
        public Nullable<int> CompID { get; set; }

        public Nullable<int> EmpId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        #region menuHeader
        public bool IsChecked { get; set; }
        public int? MHId { get; set; }
        public string MHName { get; set; }
        public string MHDescription { get; set; }
        public string Icon { get; set; }
        public string Status { get; set; }
        //public Nullable<int> UID { get; set; }
        #endregion
        public int UHTID { get; set; }

       
        #region Menu
        public int? FormId { get; set; }
        public string FormTitle { get; set; }
        public string FormName { get; set; }
        public string FormUrl { get; set; }
        
        #endregion

        public int UMTID { get; set; }
        public Nullable<int> MID { get; set; }
        
        //public Nullable<int> UID { get; set; }
        //public Nullable<int> FormID { get; set; }
   
        public Nullable<System.DateTime> UpdatetdDate { get; set; }

        public FormItem FormDetails { get; set; }


        public virtual employee_master employee_master { get; set; }

        public List<LoginItem> ListUser { get; set; }
        public List<MenuItem> ListModules { get; set; }
        public List<LoginItem> ListForm { get; set; }
        public List<UserFormTranItem> ListLeftMenu { get; set; }
        public UserFormTranItem UFormDetails { get; set; }
        public MenuWiseFormItem MenuWiseForm { get; set; }
        public List<CompanyItem> ListCompany { get; set; }
    }
}
