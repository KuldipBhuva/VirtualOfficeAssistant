using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel;
using EMSDomain.Model;
using AutoMapper;
using EMSMethods;

namespace EMSMethods
{
    public class clsMasterMethod
    {
        EmployeeEntities dbContext = new EmployeeEntities();

        public int Insert(ClsMasterP model)
        {
            Mapper.CreateMap<ClsMasterP, Master>();
            Master objMaster = Mapper.Map<Master>(model);
            dbContext.Masters.Add(objMaster);
            return dbContext.SaveChanges();
        }


        public List<ClsMasterP> GetALL()
        {
            Mapper.CreateMap<Master, ClsMasterP>();
            List<Master> lstMaster = dbContext.Masters.ToList();
            List<ClsMasterP> objmasterp = Mapper.Map<List<ClsMasterP>>(lstMaster);
            return objmasterp;
        }

        public ClsMasterP GetById(int id)
        {
            Mapper.CreateMap<Master, ClsMasterP>();
            Master objmaster = dbContext.Masters.SingleOrDefault(m => m.MId == id);
            ClsMasterP objmasterp = Mapper.Map<ClsMasterP>(objmaster);
            return objmasterp;

        }

        public int Update(ClsMasterP model)
        {
            Mapper.CreateMap<ClsMasterP, Master>();
            Master objmaster = dbContext.Masters.SingleOrDefault(m => m.MId == model.MId);
            objmaster = Mapper.Map(model, objmaster);
            return dbContext.SaveChanges();

        }



    }
}
