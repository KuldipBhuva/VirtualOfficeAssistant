using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSMethods
{
    public class ContractService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();
        public int Insert(ContractModel model)
        {
            Mapper.CreateMap<ContractModel, ContractMaster>();
            ContractMaster objCompany = Mapper.Map<ContractMaster>(model);
            Dbcontext.ContractMasters.Add(objCompany);
            return Dbcontext.SaveChanges();
        }
        public List<ContractModel> GetALL()
        {
            Mapper.CreateMap<ContractMaster, ContractModel>();
            List<ContractMaster> objCompanyMaster = Dbcontext.ContractMasters.ToList();
            List<ContractModel> objCompItem = Mapper.Map<List<ContractModel>>(objCompanyMaster);
            return objCompItem;
        }
        public int Update(ContractModel model)
        {
            Mapper.CreateMap<ContractModel, ContractMaster>();
            ContractMaster objComp = Dbcontext.ContractMasters.SingleOrDefault(m => m.ContID == model.ContID);
            objComp = Mapper.Map(model, objComp);
            return Dbcontext.SaveChanges();
        }
        public ContractModel GetById(int id)
        {
            Mapper.CreateMap<ContractMaster, ContractModel>();
            ContractMaster objComp = Dbcontext.ContractMasters.SingleOrDefault(m => m.ContID == id);
            ContractModel objCItem = Mapper.Map<ContractModel>(objComp);
            return objCItem;
        }
    }
}
