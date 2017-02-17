using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSDomain.ViewModel.Vehicle
{
    public class VehicleItem
    {        
        public int Viewbagidformenu { get; set; }
        public string filter { get; set; }
        public int VID { get; set; }
        [Required(ErrorMessage = "Company Required")]
        [Display(Name = "Company :*")]
        public Nullable<int> CompID { get; set; }
        [Required(ErrorMessage = "Vehicle No. Required")]
        [Display(Name = "Vehicle No. :*")]
        public string VehicleNo { get; set; }
        [Required(ErrorMessage = "Vehicle Name Required")]
        [Display(Name = "Vehicle Name :*")]
        public string VehicleName { get; set; }
        [Required(ErrorMessage = "Model Required")]
        [Display(Name = "Model Name :*")]
        public string ModelName { get; set; }
        [Required(ErrorMessage = "Employee Required")]
        [Display(Name = "Used By :")]
        public Nullable<int> EmpID { get; set; }
        [Display(Name = "Branch :")]
        public Nullable<int> Branch_ID { get; set; }
        [Display(Name = "Purchase Value :")]
        public string Purchase_Value { get; set; }
        [Display(Name = "Current Value :")]
        public string CurrentValue { get; set; }
        [Display(Name = "Depreciation(%) :")]
        public string Depriciation { get; set; }
        [Display(Name = "Market Value :")]
        public string Market_Value { get; set; }
        [Display(Name = "Purchased Date :")]
        public Nullable<System.DateTime> Purchased_Date { get; set; }
        [Required(ErrorMessage = "Registration Date Required")]
        [Display(Name = "Registration Date :")]
        public Nullable<System.DateTime> Reg_Date { get; set; }
        [Required(ErrorMessage = "Expiry Date Required")]
        [Display(Name = "Expiry Date :")]
        public Nullable<System.DateTime> Reg_Exp_Date { get; set; }
        [Display(Name = "Status :")]
        public string Status { get; set; }
        [Display(Name = "Remarks :")]
        public string Remarks { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UsedFor { get; set; }
        public string SalikNo { get; set; }

        [Display(Name = "Bear Capacity :")]
        public Nullable<decimal> WeightBearCapacity { get; set; }
        [Display(Name = "Length :")]
        public Nullable<decimal> Length { get; set; }
        [Display(Name = "Length To Use :")]
        public Nullable<decimal> LengthToUse { get; set; }
        [Display(Name = "Width :")]
        public Nullable<decimal> Width { get; set; }
        [Display(Name = "Width To Use :")]
        public Nullable<decimal> WidthToUse { get; set; }
        [Display(Name = "Height :")]
        public Nullable<decimal> Height { get; set; }
        [Display(Name = "Height To Use :")]
        public Nullable<decimal> HeightToUse { get; set; }
        [Display(Name = "Permit Type:")]
        public Nullable<int> PermitType { get; set; }
        [Display(Name = "Permit No:")]
        public string PermitNo { get; set; }
        [Display(Name = "Permit Expiry Date :")]
        public Nullable<System.DateTime> PermitExpiryDate { get; set; }
        [Display(Name = "TV Status :")]
        public Nullable<int> TVStatus { get; set; }
        [Display(Name = "Vehicle Owner Name:")]
        public string VehicleOwnerName { get; set; }
        [Display(Name = "Owner Address :")]
        public string OwnerAddress { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public List<VehicleItem> ListVehicleData { get; set; }
        public List<clsMasterData> ListMasterTablePayment { get; set; }
        public CompanyItem CompDetails { get; set; }
        public List<CompanyItem> ListCompany { get; set; }
        public List<BranchItem> ListBranch { get; set; }
        public List<EmployeeItem> ListEmp { get; set; }
        public PetrolExpItem Pdetails { get; set; }
        public PetrolCardItem PCardItem { get; set; }
        public List<clsMasterData> ListMasterTableCard { get; set; }
        public VehicleItem VehicleDetails { get; set; }
        public BranchItem BranchDetails { get; set; }
        public EmployeeItem EmpDetails { get; set; }
        public List<VehicleDocumentItem> ListVehicleDocument { get; set; }

    }
}
