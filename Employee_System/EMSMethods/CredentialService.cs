using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;

namespace EMSMethods
{
    public class CredentialService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(CredentialItem model)
        {
            try
            {
                Mapper.CreateMap<CredentialItem, CredentialMaster>();
                CredentialMaster objAppoint = Mapper.Map<CredentialMaster>(model);
                DbContext.CredentialMasters.Add(objAppoint);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<CredentialItem> getCredentialData(int cid)
        {
            Mapper.CreateMap<CredentialMaster, CredentialItem>();
            List<CredentialMaster> objCompanyMaster = DbContext.CredentialMasters.Where(m => m.CompID == (cid == 0 ? m.CompID : cid)).ToList();
            List<CredentialItem> objCompItem = Mapper.Map<List<CredentialItem>>(objCompanyMaster);
            return objCompItem;
        }
        public int Update(CredentialItem model)
        {
            Mapper.CreateMap<CredentialItem, CredentialMaster>();
            CredentialMaster objAppoint = DbContext.CredentialMasters.SingleOrDefault(m => m.CrID == model.CrID);
            objAppoint = Mapper.Map(model, objAppoint);
            return DbContext.SaveChanges();
        }
        public CredentialItem GetById(int id)
        {
            Mapper.CreateMap<CredentialMaster, CredentialItem>();
            CredentialMaster objAppoint = DbContext.CredentialMasters.SingleOrDefault(m => m.CrID == id);
            CredentialItem objAppointItem = Mapper.Map<CredentialItem>(objAppoint);
            return objAppointItem;
        }
    }
}
