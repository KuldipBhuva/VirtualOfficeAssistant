using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSMethods;
using AutoMapper;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.TradeLicense;



namespace EMSMethods
{
    public class CompanyService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = Dbcontext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public int Insert(CompanyItem model)
        {
            Mapper.CreateMap<CompanyItem, Company_master>();
            Company_master objCompany = Mapper.Map<Company_master>(model);
            Dbcontext.Company_master.Add(objCompany);
            return Dbcontext.SaveChanges();
        }
        public List<CompanyItem> GetbyCID(int cid)
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> objCompanyMaster = Dbcontext.Company_master.Where(m => m.id == (cid == 0 ? m.id : cid)).ToList();
            List<CompanyItem> objCompItem = Mapper.Map<List<CompanyItem>>(objCompanyMaster);
            return objCompItem;
        }
        public List<CompanyItem> GetALL()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> objCompanyMaster = Dbcontext.Company_master.ToList();
            List<CompanyItem> objCompItem = Mapper.Map<List<CompanyItem>>(objCompanyMaster);
            return objCompItem;
        }
        public int Update(CompanyItem model)
        {
            Mapper.CreateMap<CompanyItem, Company_master>();
            Company_master objComp = Dbcontext.Company_master.SingleOrDefault(m => m.id == model.id);
            objComp = Mapper.Map(model, objComp);
            return Dbcontext.SaveChanges();
        }
        public CompanyItem GetById(int id)
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            Company_master objComp = Dbcontext.Company_master.SingleOrDefault(m => m.id == id);
            CompanyItem objCItem = Mapper.Map<CompanyItem>(objComp);
            return objCItem;
        }
        //public List<EmployeeItem> GetALLGrid(int? compid)
        //{
        //    Mapper.CreateMap<employee_master, EmployeeItem>();
        //    List<employee_master> tblMaster = Dbcontext.employee_master.Where(m => m.Compid == (compid == null || compid == 0 ? m.Compid : compid)).ToList();
        //    List<EmployeeItem> lstdata = Mapper.Map<List<EmployeeItem>>(tblMaster);
        //    return lstdata;

        //}
        public List<EmployeeItem> GetALLGrid(int? compid)
        {
            var data = (from em in Dbcontext.employee_master
                        join cm in Dbcontext.Company_master on em.Compid equals cm.id
                        join bm in Dbcontext.Branch_master on em.Branchid equals bm.id
                        where em.Compid == compid
                        select new EmployeeItem()
                        {
                            CompDetails = new CompanyItem()
                            {
                                id = cm.id,
                                CompName = cm.CompName,
                            },
                            BranchDetails = new BranchItem()
                            {
                                BranchName = bm.BranchName
                            },
                            id = em.id,
                            Compid = em.Compid,
                            Empname = em.Empname,
                            Empno = em.Empno,
                            Mobile = em.Mobile,
                            FileNumber = em.FileNumber,
                            DOJ = em.DOJ
                        }).Distinct().ToList();
            return data;
        }

    }
}
