using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Insurance;
using EMSDomain.ViewModel.Tenancy;
using EMSDomain.ViewModel.TradeLicense;
using EMSDomain.ViewModel.Vehicle;

namespace EMSDomain.ViewModel.Employee
{
    [Serializable]
    public class EmpAllItem
    {
        public string filter { get; set; }
        public int Viewbagidformenu { get; set; }
        public string JobTitle { get; set; }
        public EmployeeItem EmployeeItem { get; set; }

        public EmpRelativeItem EmpRelativeItem { get; set; }
        public EmpContactItem EmpContactItem { get; set; }
        public EmpPassportItem EmpPassportItem { get; set; }
        public CompanyItem CompanyItem { get; set; }
        public BranchItem BranchItem { get; set; }
        public clsMasterData desiDetails { get; set; }
        public List<EmpAllItem> EmployeeALLDetails { get; set; }

        public List<EmpHealthItem> EmpLicenceItem { get; set; }
        public List<EmpHealthItem> EmpLicenceItem30 { get; set; }
        public List<EmpPassportItem> EmppassItem { get; set; }
        public List<EmpPassportItem> EmppassItem30 { get; set; }
        public List<EmpHealthItem> EmpHealthItem { get; set; }
        public List<EmpHealthItem> EmpHealthItem30 { get; set; }
        public List<EmpHealthItem> EmpLabourItem { get; set; }
        public List<EmpHealthItem> EmpLabourItem30 { get; set; }
        public List<EmpInsuranceItem> EmpInsuItem { get; set; }
        public List<EmpInsuranceItem> EmpInsuItem30 { get; set; }
        public List<VehicleItem> ListVehicle { get; set; }
        public List<VehicleItem> ListVehicle30 { get; set; }
        public List<EmpIncrementItem> ListIncrement { get; set; }
        public List<EmpVisaItem> ListVisa { get; set; }
        public List<EmpVisaItem> ListVisa30 { get; set; }
        public List<EmpHealthItem> ListEmirates { get; set; }
        public List<EmpHealthItem> ListEmirates30 { get; set; }
        public List<EmployeeItem> ListEmpDOB { get; set; }
        public List<EmployeeItem> ListEmpBS { get; set; }
        public List<EmpPassportItem> ListPass { get; set; }
        public List<EmployeeItem> ListEmpLabour { get; set; }
        public List<EmpVisaItem> ListEmpVisa { get; set; }
        public List<clsMasterData> listMaster { get; set; }
        public List<EmployeeItem> ListEmpEID { get; set; }
        public List<EmployeeItem> EmployeeList { get; set; }
        public List<clsMasterData> listMasterWS { get; set; }
        public List<TradeLicenseItem> ListCompDoc30 { get; set; }
        public List<TradeLicenseItem> ListCompDoc { get; set; }
        public List<TenancyItem> ListTenancy30 { get; set; }
        public List<TenancyItem> ListTenancy { get; set; }
    }
}
