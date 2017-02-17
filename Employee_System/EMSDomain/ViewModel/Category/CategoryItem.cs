using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSDomain.ViewModel.Category
{
    public class CategoryItem
    {
        public int Viewbagidformenu { get; set; }
        public int Cat_id { get; set; }
        [Required(ErrorMessage = "Enter Category Name")]
        [Display(Name = "Category")]
        public string CatName { get; set; }
        [Display(Name = "Description")]
        public string CatDesc { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> CompID { get; set; }

        public List<CategoryItem> lstCateory { get; set; }
        public List<CompanyItem> ListCompany { get; set; }
        public List<BranchItem> ListBranch { get; set; }

    }
}
