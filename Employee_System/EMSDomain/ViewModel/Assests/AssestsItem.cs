using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Category;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Assests
{
    public class AssestsItem
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage="Select a Category")]
        [Display(Name="Category")]
        public Nullable<int> CatId { get; set; }
        [Display(Name = "Item")]
        public string ItemName { get; set; }
        [Display(Name = "Description")]
        public string ItemDesc { get; set; }
        [Display(Name = "Opening Stock")]
        public Nullable<decimal> OpStock { get; set; }
        public List<AssestsItem> lstOfItems { get; set; }
        public List<CategoryItem> lstCategory { get; set; }
        public string CatName { get; set; }
        public Nullable<int> CompID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public List<CompanyItem> ListCompany { get; set; }
        public List<BranchItem> ListBranch { get; set; }
    }
}
