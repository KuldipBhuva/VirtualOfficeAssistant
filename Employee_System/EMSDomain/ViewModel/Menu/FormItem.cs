using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSDomain.ViewModel.Menu
{
    public class FormItem
    {
        public bool? IsChecked { get; set; }
        public int FormId { get; set; }
        public string FormTitle { get; set; }
        public string FormName { get; set; }
        public string FormUrl { get; set; }
        public string Status { get; set; }

        #region userform
        public int UMTID { get; set; }
        public Nullable<int> MID { get; set; }
        public Nullable<int> UID { get; set; }
        public Nullable<int> FormID1 { get; set; }
   
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatetdDate { get; set; }

        public FormItem FormDetails { get; set; }
        #endregion

        #region menutran
        public int MSId { get; set; }
        //public Nullable<int> FormId { get; set; }
        public int? MHId { get; set; }
        public string Sequence { get; set; }
        #endregion
        public MenuWiseFormItem MenuWiseForm { get; set; }

        public UserFormTranItem UFormDetails { get; set; }
        public FormItem menuTran { get; set; }
    }
}
