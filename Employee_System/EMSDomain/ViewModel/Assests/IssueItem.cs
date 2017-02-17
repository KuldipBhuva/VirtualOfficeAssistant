using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Category;

namespace EMSDomain.ViewModel.Assests
{
    public class IssueItem
    {
        public int IssueId { get; set; }
        [Required(ErrorMessage = "Select a Category.")]
        [Display(Name = "Category")]
        public Nullable<int> Cat_id { get; set; }
        [Required(ErrorMessage = "Select a Item.")]
        [Display(Name = "Item")]
        public Nullable<int> Item_id { get; set; }
        [Required(ErrorMessage = "Enter Quantity.")]
        [Display(Name = "Quantity")]
        public Nullable<decimal> IssueQuantity { get; set; }
        [Required(ErrorMessage = "Enter Date.")]
        [Display(Name = "Date")]
        public Nullable<System.DateTime> IssueDate { get; set; }
        [Display(Name = "Vendor Name")]
        public string VendorName { get; set; }
        public string InvoiceNo { get; set; }
        public string ChallanNo { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompID { get; set; }

        public List<CategoryItem> lstCategory { get; set; }
        public string CatName { get; set; }
        public string ItemName { get; set; }

        public List<AssestsItem> lstAllItem { get; set; }

        public List<IssueItem> lstIssueItem { get; set; }

        public decimal Available { get; set; }

    }
}
