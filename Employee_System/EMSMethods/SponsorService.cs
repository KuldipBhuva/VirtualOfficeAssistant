using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;

namespace EMSMethods
{
    public class SponsorService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(SponsorItem model)
        {
            try
            {
                Mapper.CreateMap<SponsorItem, SponsorMaster>();
                SponsorMaster objAppoint = Mapper.Map<SponsorMaster>(model);
                DbContext.SponsorMasters.Add(objAppoint);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<CompanyItem> GetCompany()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> lstCompany = DbContext.Company_master.ToList();
            List<CompanyItem> objComp = Mapper.Map<List<CompanyItem>>(lstCompany);
            return objComp;
        }
        public List<SponsorItem> getSponsorData(int cid)
        {
            Mapper.CreateMap<SponsorMaster, SponsorItem>();
            List<SponsorMaster> objCompanyMaster = DbContext.SponsorMasters.Where(m => m.CompID == (cid == 0 ? m.CompID : cid)).ToList();
            List<SponsorItem> objCompItem = Mapper.Map<List<SponsorItem>>(objCompanyMaster);
            return objCompItem;
        }
        public int Update(SponsorItem model)
        {
            Mapper.CreateMap<SponsorItem, SponsorMaster>();
            SponsorMaster objAppoint = DbContext.SponsorMasters.SingleOrDefault(m => m.SID == model.SID);
            objAppoint = Mapper.Map(model, objAppoint);
            return DbContext.SaveChanges();
        }
        public SponsorItem GetById(int id)
        {
            Mapper.CreateMap<SponsorMaster, SponsorItem>();
            SponsorMaster objAppoint = DbContext.SponsorMasters.SingleOrDefault(m => m.SID == id);
            SponsorItem objAppointItem = Mapper.Map<SponsorItem>(objAppoint);
            return objAppointItem;
        }
    }
}
