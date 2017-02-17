using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Insurance;

namespace EMSMethods
{
    public class EmpInsuranceService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(EmpInsuranceItem model)
        {
            try
            {
                Mapper.CreateMap<EmpInsuranceItem, EmployeeInsurance>();
                EmployeeInsurance objEmp = Mapper.Map<EmployeeInsurance>(model);
                DbContext.EmployeeInsurances.Add(objEmp);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<clsMasterData> GetALLIComp()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 14 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public List<BranchItem> GetBranch()
        {
            Mapper.CreateMap<Branch_master, BranchItem>();
            List<Branch_master> lstBranch = DbContext.Branch_master.ToList();
            List<BranchItem> lstBranchItem = Mapper.Map<List<BranchItem>>(lstBranch);
            return lstBranchItem;
        }
        public List<clsMasterData> GetALLPType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 37 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public List<CompanyItem> GetComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> listComp = Mapper.Map<List<CompanyItem>>(tblMaster);
            return listComp;

        }
        public List<EmployeeItem> GetEmp()
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.ToList();
            List<EmployeeItem> listEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return listEmp;

        }
        public List<EmpInsuranceItem> InsuranceListData(int cid)
        {
            var objIns = (from EmpInsurance in DbContext.EmployeeInsurances
                          join branch in DbContext.Branch_master on EmpInsurance.Branch equals branch.id into BLJ
                          from left in BLJ.DefaultIfEmpty()
                          join masterComp in DbContext.Masters_Tran on EmpInsurance.Icomp equals masterComp.Id
                          join masterType in DbContext.Masters_Tran on EmpInsurance.Ptype equals masterType.Id
                          join emp in DbContext.employee_master on EmpInsurance.EmpId equals emp.id into LJ
                          from subpet in LJ.DefaultIfEmpty()
                          where (EmpInsurance.Ptype == 41 || EmpInsurance.Ptype == 43) && EmpInsurance.CompID == (cid == 0 ? EmpInsurance.CompID : cid)
                          select new EmpInsuranceItem
                          {
                              MasterDetails = new clsMasterData()
                              {
                                  Name = masterComp.Name
                              },
                              MasterDetailsType = new clsMasterData()
                              {
                                  Name = masterType.Name
                              },
                              BranchDetails = new BranchItem()
                              {
                                  //BranchName = branch.BranchName
                                  BranchName = (left == null ? String.Empty : left.BranchName)
                              },
                              EmpDetails = new EmployeeItem()
                              {
                                  //Empname=emp.Empname
                                  Empname = (subpet == null ? String.Empty : subpet.Empname)
                              },
                              EmpId = EmpInsurance.EmpId,
                              Pname = EmpInsurance.Pname,
                              Icomp = EmpInsurance.Icomp,
                              Ptype = EmpInsurance.Ptype,
                              Pno = EmpInsurance.Pno,
                              Pamt = EmpInsurance.Pamt,
                              Pdate = EmpInsurance.Pdate,
                              PExpDate = EmpInsurance.PExpDate,
                              PremiumDate = EmpInsurance.PremiumDate,
                              Branch = EmpInsurance.Branch,
                              Remarks = EmpInsurance.Remarks,
                              IID = EmpInsurance.IID
                          }
                ).Distinct().ToList();
            return objIns;
        }
        public List<EmpInsuranceItem> EmpInsuranceList(int EmpId)
        {
            var objIns = (from EmpInsurance in DbContext.EmployeeInsurances
                          join branch in DbContext.Branch_master on EmpInsurance.Branch equals branch.id into BLJ
                          from left in BLJ.DefaultIfEmpty()
                          join masterComp in DbContext.Masters_Tran on EmpInsurance.Icomp equals masterComp.Id
                          join masterType in DbContext.Masters_Tran on EmpInsurance.Ptype equals masterType.Id
                          join emp in DbContext.employee_master on EmpInsurance.EmpId equals emp.id into LJ
                          from subpet in LJ.DefaultIfEmpty()
                          where EmpInsurance.Ptype == 40 && EmpInsurance.EmpId == EmpId
                          select new EmpInsuranceItem
                          {
                              MasterDetails = new clsMasterData()
                              {
                                  Name = masterComp.Name
                              },
                              MasterDetailsType = new clsMasterData()
                              {
                                  Name = masterType.Name
                              },
                              BranchDetails = new BranchItem()
                              {
                                  //BranchName = branch.BranchName
                                  BranchName = (left == null ? String.Empty : left.BranchName)
                              },
                              EmpDetails = new EmployeeItem()
                              {
                                  //Empname=emp.Empname
                                  Empname = (subpet == null ? String.Empty : subpet.Empname)
                              },
                              EmpId = EmpInsurance.EmpId,
                              Pname = EmpInsurance.Pname,
                              Icomp = EmpInsurance.Icomp,
                              Ptype = EmpInsurance.Ptype,
                              Pno = EmpInsurance.Pno,
                              Pamt = EmpInsurance.Pamt,
                              Pdate = EmpInsurance.Pdate,
                              PExpDate = EmpInsurance.PExpDate,
                              PremiumDate = EmpInsurance.PremiumDate,
                              Branch = EmpInsurance.Branch,
                              Remarks = EmpInsurance.Remarks,
                              IID = EmpInsurance.IID
                          }
                ).Distinct().ToList();
            return objIns;
        }
        public List<EmpInsuranceItem> VehicleInsuranceList(int cid)
        {
            var objIns = (from EmpInsurance in DbContext.EmployeeInsurances
                          join branch in DbContext.Branch_master on EmpInsurance.Branch equals branch.id into BLJ
                          from left in BLJ.DefaultIfEmpty()
                          join masterComp in DbContext.Masters_Tran on EmpInsurance.Icomp equals masterComp.Id
                          join masterType in DbContext.Masters_Tran on EmpInsurance.Ptype equals masterType.Id
                          join emp in DbContext.employee_master on EmpInsurance.EmpId equals emp.id into LJ
                          from subpet in LJ.DefaultIfEmpty()
                          where EmpInsurance.Ptype == 42 && EmpInsurance.CompID == (cid == 0 ? EmpInsurance.CompID : cid)
                          select new EmpInsuranceItem
                          {
                              MasterDetails = new clsMasterData()
                              {
                                  Name = masterComp.Name
                              },
                              MasterDetailsType = new clsMasterData()
                              {
                                  Name = masterType.Name
                              },
                              BranchDetails = new BranchItem()
                              {
                                  //BranchName = branch.BranchName
                                  BranchName = (left == null ? String.Empty : left.BranchName)
                              },
                              EmpDetails = new EmployeeItem()
                              {
                                  //Empname=emp.Empname
                                  Empname = (subpet == null ? String.Empty : subpet.Empname)
                              },
                              EmpId = EmpInsurance.EmpId,
                              Pname = EmpInsurance.Pname,
                              Icomp = EmpInsurance.Icomp,
                              Ptype = EmpInsurance.Ptype,
                              Pno = EmpInsurance.Pno,
                              Pamt = EmpInsurance.Pamt,
                              Pdate = EmpInsurance.Pdate,
                              PExpDate = EmpInsurance.PExpDate,
                              PremiumDate = EmpInsurance.PremiumDate,
                              Branch = EmpInsurance.Branch,
                              Remarks = EmpInsurance.Remarks,
                              IID = EmpInsurance.IID
                          }
                ).Distinct().ToList();
            return objIns;
        }
        public int Update(EmpInsuranceItem model)
        {
            Mapper.CreateMap<EmpInsuranceItem, EmployeeInsurance>();
            EmployeeInsurance objInsu = DbContext.EmployeeInsurances.SingleOrDefault(m => m.IID == model.IID);
            objInsu = Mapper.Map(model, objInsu);
            return DbContext.SaveChanges();
        }
        public EmpInsuranceItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeInsurance, EmpInsuranceItem>();
            EmployeeInsurance objInsu = DbContext.EmployeeInsurances.SingleOrDefault(m => m.IID == id);
            EmpInsuranceItem objInsuItem = Mapper.Map<EmpInsuranceItem>(objInsu);
            return objInsuItem;
        }
    }
}
