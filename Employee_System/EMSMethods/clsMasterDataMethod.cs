using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel;
using EMSDomain.Model;
using AutoMapper;
using EMSMethods;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class clsMasterDataMethod
    {
        EmployeeEntities dbcontext = new EmployeeEntities();

        public int Insert(clsMasterData model)
        {
            Mapper.CreateMap<clsMasterData, Masters_Tran>();
            Masters_Tran objmasterdata = Mapper.Map<Masters_Tran>(model);
            dbcontext.Masters_Tran.Add(objmasterdata);
            return dbcontext.SaveChanges();
        }


        public List<clsMasterData> GetALL(int? MID)
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = dbcontext.Masters_Tran.Where(m => m.MasterId == (MID == 0 ? m.MasterId : MID)).ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }

        public int Update(clsMasterData model)
        {
            Mapper.CreateMap<clsMasterData, Masters_Tran>();
            Masters_Tran tblmastertran = dbcontext.Masters_Tran.SingleOrDefault(m => m.Id == model.Id);
            tblmastertran = Mapper.Map(model, tblmastertran);
            return dbcontext.SaveChanges();

        }

        public clsMasterData GetById(int id)
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            Masters_Tran objmastertrantbl = dbcontext.Masters_Tran.SingleOrDefault(m => m.Id == id);
            clsMasterData objmasterdata = Mapper.Map<clsMasterData>(objmastertrantbl);
            return objmasterdata;

        }
        public List<EmpDesignationItem> GetAllDesignation()
        {
            var objDesg = (from desg in dbcontext.Masters
                           join mt in dbcontext.Masters_Tran on desg.MId equals mt.MasterId
                           where mt.MasterId == 8
                           select new EmpDesignationItem
                           {
                               Id = mt.Id,
                               Name = mt.Name
                           }).ToList();
            return objDesg;

        }

        public List<clsMasterData> GetALLGrid(int? MasterId1)
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = dbcontext.Masters_Tran.Where(m => m.MasterId == (MasterId1 == null || MasterId1 == 0 ? m.MasterId : MasterId1)).ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }

    }
}

