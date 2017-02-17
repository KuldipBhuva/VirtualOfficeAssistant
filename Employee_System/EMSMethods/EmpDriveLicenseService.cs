using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class EmpDriveLicenseService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public int Insert(EmpDLicenceItem model)
        {
            try
            {
                Mapper.CreateMap<EmpDLicenceItem, EmpDrivingLicence>();
                EmpDrivingLicence objDL = Mapper.Map<EmpDrivingLicence>(model);
                DbContext.EmpDrivingLicences.Add(objDL);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }        
        public List<EmpDLicenceItem> LicenceListData(int EmpId)
        {
            try
            {
                Mapper.CreateMap<EmpDrivingLicence, EmpDLicenceItem>();
                var objExp = (from EmpLicence in DbContext.EmpDrivingLicences
                              join Emp in DbContext.employee_master on EmpLicence.EmpId equals Emp.id
                              where EmpLicence.EmpId == (EmpId == 0 ? EmpLicence.EmpId : EmpId)
                              select new EmpDLicenceItem
                              {
                                  EmpDetails = new EmployeeItem()
                                  {
                                      Empname = Emp.Empname
                                  },
                                  LicenceNo = EmpLicence.LicenceNo,
                                  IssueCountry = EmpLicence.IssueCountry,
                                  ExpiryDate = EmpLicence.ExpiryDate,
                                  IssueDate = EmpLicence.IssueDate,
                                  DStatus = EmpLicence.DStatus,
                                  id = EmpLicence.id,
                                  EmpId = EmpLicence.EmpId
                              }
                    ).Distinct().ToList();
                return objExp;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public EmpDLicenceItem GetById(int id)
        {
            Mapper.CreateMap<EmpDrivingLicence, EmpDLicenceItem>();
            EmpDrivingLicence objDl = DbContext.EmpDrivingLicences.SingleOrDefault(m => m.id == id);
            EmpDLicenceItem objDlItem = Mapper.Map<EmpDLicenceItem>(objDl);
            return objDlItem;
        }

        public int Update(EmpDLicenceItem model)
        {
            Mapper.CreateMap<EmpDLicenceItem, EmpDrivingLicence>();
            EmpDrivingLicence objDl = DbContext.EmpDrivingLicences.SingleOrDefault(m => m.id == model.id);
            objDl = Mapper.Map(model, objDl);
            return DbContext.SaveChanges();
        }
        public EMSDomain.ViewModel.DocumentItem byID(int id)
        
        {
            Mapper.CreateMap<EmployeeDocument, EMSDomain.ViewModel.DocumentItem>();
            EmployeeDocument objDoc = DbContext.EmployeeDocuments.SingleOrDefault(m => m.Id == id);
            EMSDomain.ViewModel.DocumentItem objDocItem = Mapper.Map<EMSDomain.ViewModel.DocumentItem>(objDoc);
            DbContext.EmployeeDocuments.Remove(objDoc);
            
            return objDocItem;
        }
        public int DeleteDoc(int id)
        {
            Mapper.CreateMap<EmployeeDocument, EMSDomain.ViewModel.DocumentItem>();
            EmployeeDocument objDoc = DbContext.EmployeeDocuments.SingleOrDefault(m => m.Id == id);
            EMSDomain.ViewModel.DocumentItem objDocItem = Mapper.Map<EMSDomain.ViewModel.DocumentItem>(objDoc);
            DbContext.EmployeeDocuments.Remove(objDoc);
            return DbContext.SaveChanges();

            //Mapper.CreateMap<EmployeeDocument, EmployeeDocument>();
            //EmployeeDocument objDL = Mapper.Map<EmployeeDocument>(id);
            //DbContext.EmployeeDocuments.Remove(objDL);
            //return DbContext.SaveChanges();
        }
        public List<EmpDLicenceItem> GetLicenceDoc(int empid)
        {
            var objLicence = (from doc in DbContext.EmployeeDocuments
                              where doc.DocCategoryId == 133 && doc.EmpId==empid
                              select new EmpDLicenceItem()
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
    }
}
