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
    public class ReturnChequeService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(ChequeReturnModel model)
        {
            try
            {
                Mapper.CreateMap<ChequeReturnModel, ChequeReturnMaster>();
                ChequeReturnMaster objAppoint = Mapper.Map<ChequeReturnMaster>(model);
                DbContext.ChequeReturnMasters.Add(objAppoint);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<ChequeReturnModel> GetReturnChq()
        {
            Mapper.CreateMap<ChequeReturnMaster, ChequeReturnModel>();
            List<ChequeReturnMaster> lstCompany = DbContext.ChequeReturnMasters.ToList();
            List<ChequeReturnModel> objComp = Mapper.Map<List<ChequeReturnModel>>(lstCompany);
            return objComp;
        }
        public int Update(ChequeReturnModel model)
        {
            Mapper.CreateMap<ChequeReturnModel, ChequeReturnMaster>();
            ChequeReturnMaster objComp = DbContext.ChequeReturnMasters.SingleOrDefault(m => m.CRID == model.CRID);
            objComp = Mapper.Map(model, objComp);
            return DbContext.SaveChanges();
        }
        public ChequeReturnModel GetById(int id)
        {
            Mapper.CreateMap<ChequeReturnMaster, ChequeReturnModel>();
            ChequeReturnMaster objComp = DbContext.ChequeReturnMasters.SingleOrDefault(m => m.CRID == id);
            ChequeReturnModel objCItem = Mapper.Map<ChequeReturnModel>(objComp);
            return objCItem;
        }
    }
}
