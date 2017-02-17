using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSMethods;
using AutoMapper;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;

namespace EMSMethods
{
    public class BranchService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public int Insert(BranchItem model)
        {
            Mapper.CreateMap<BranchItem, Branch_master>();
            Branch_master objBranch = Mapper.Map<Branch_master>(model);
            DbContext.Branch_master.Add(objBranch);
            return DbContext.SaveChanges();
        }
        public List<CompanyItem> GetAllComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> lstmasterdata = Mapper.Map<List<CompanyItem>>(tblMaster);
            return lstmasterdata;
        }
        public List<BranchItem> GetAll()
        {
            Mapper.CreateMap<Branch_master, BranchItem>();
            List<Branch_master> objBranchmaster = DbContext.Branch_master.ToList();
            List<BranchItem> objbranchitem = Mapper.Map<List<BranchItem>>(objBranchmaster);
            return objbranchitem;
        }

        public List<BranchItem> GetBranchByComp(int strCompCode)
        {
            var Branch_master = (from data in DbContext.Branch_master.Where(c => c.CompID == strCompCode)
                                 select new BranchItem()
                                 {
                                     id = data.id,
                                     BranchName = data.BranchName,
                                     BranchCode = data.BranchCode
                                 }
              ).ToList();
            return Branch_master;
        }
        public List<BranchItem> getBranch(int cid)
        {
            var data = (from BM in DbContext.Branch_master
                        join cm in DbContext.Company_master on BM.CompID equals cm.id
                        where BM.CompID == (cid == 0 ? BM.CompID : cid)
                        select new BranchItem
                        {
                            CompDetails = new CompanyItem()
                            {
                                CompName = cm.CompName
                            },
                            id = BM.id,
                            BranchName = BM.BranchName,
                            BranchAdd = BM.BranchAdd,
                            ContactPerson = BM.ContactPerson,
                            City = BM.City,
                            Phone = BM.Phone
                        }).ToList();
            return data;
        }
        public int Update(BranchItem model)
        {
            Mapper.CreateMap<BranchItem, Branch_master>();
            Branch_master objBranch = DbContext.Branch_master.SingleOrDefault(m => m.id == model.id);
            objBranch = Mapper.Map(model, objBranch);
            return DbContext.SaveChanges();
        }
        public BranchItem GetById(int id)
        {
            Mapper.CreateMap<Branch_master, BranchItem>();
            Branch_master objBranch = DbContext.Branch_master.SingleOrDefault(m => m.id == id);
            BranchItem objBItem = Mapper.Map<BranchItem>(objBranch);
            return objBItem;
        }
    }
}
