using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class EmpExperienceService
    {
        EmployeeEntities DBcontext = new EmployeeEntities();

        public int Insert(EmpExperienceItem model)
        {
            try
            {
                Mapper.CreateMap<EmpExperienceItem, EmployeeExperience>();
                EmployeeExperience objExper = Mapper.Map<EmployeeExperience>(model);
                DBcontext.EmployeeExperiences.Add(objExper);
                return DBcontext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<EmpExperienceItem> ExperienceListData(int EmpId)
        {
            try
            {
                Mapper.CreateMap<EmployeeExperience, EmpExperienceItem>();
                var objExp = (from EmpExp in DBcontext.EmployeeExperiences
                              join Emp in DBcontext.employee_master on EmpExp.EmpId equals Emp.id
                              where EmpExp.EmpId == (EmpId == 0 ? EmpExp.EmpId : EmpId)
                              select new EmpExperienceItem
                              {
                                  EmpDetails = new EmployeeItem()
                                  {
                                      Empname = Emp.Empname
                                  },
                                  CompName = EmpExp.CompName,
                                  ContactPerson = EmpExp.ContactPerson,
                                  FromDate = EmpExp.FromDate,
                                  ToDate = EmpExp.ToDate,
                                  Roles = EmpExp.Roles,
                                  ExpId = EmpExp.ExpId,
                                  EmpId = EmpExp.EmpId
                              }
                    ).Distinct().ToList();
                return objExp;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public EmpExperienceItem GetById(int id)
        {
            try
            {
                Mapper.CreateMap<EmployeeExperience, EmpExperienceItem>();
                EmployeeExperience objExp = DBcontext.EmployeeExperiences.SingleOrDefault(m => m.ExpId == id);
                EmpExperienceItem objExpItem = Mapper.Map<EmpExperienceItem>(objExp);
                return objExpItem;
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }
        public EMSDomain.ViewModel.DocumentItem byID(int id)
        {
            Mapper.CreateMap<EmployeeDocument, EMSDomain.ViewModel.DocumentItem>();
            EmployeeDocument objDoc = DBcontext.EmployeeDocuments.SingleOrDefault(m => m.Id == id);
            EMSDomain.ViewModel.DocumentItem objDocItem = Mapper.Map<EMSDomain.ViewModel.DocumentItem>(objDoc);
            DBcontext.EmployeeDocuments.Remove(objDoc);

            return objDocItem;
        }
        public int Update(EmpExperienceItem model)
        {
            Mapper.CreateMap<EmpExperienceItem, EmployeeExperience>();
            EmployeeExperience objExp = DBcontext.EmployeeExperiences.SingleOrDefault(m => m.ExpId == model.ExpId);
            objExp = Mapper.Map(model, objExp);
            return DBcontext.SaveChanges();
        }
        public List<EmpExperienceItem> GetExperienceDoc(int empid)
        {
            var objLicence = (from doc in DBcontext.EmployeeDocuments
                              where doc.DocCategoryId == 135 && doc.EmpId == empid
                              select new EmpExperienceItem()
                              {
                                  DocDetails = new EMSDomain.ViewModel.DocumentItem()
                                  {
                                      EmpId = doc.EmpId,
                                      FileName = doc.FilleName,
                                      FileUrl = doc.FileUrl,
                                      Id = doc.Id
                                  }
                              }

                ).Distinct().ToList();
            return objLicence;
        }
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DBcontext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
