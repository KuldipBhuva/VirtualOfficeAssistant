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
    public class EmpHealthService
    {
        EmployeeEntities DBcontext = new EmployeeEntities();

        public int Insert(EmpHealthItem model)
        {
            try
            {
                Mapper.CreateMap<EmpHealthItem, EmployeeHealthCard>();
                EmployeeHealthCard objHealth = Mapper.Map<EmployeeHealthCard>(model);
                DBcontext.EmployeeHealthCards.Add(objHealth);
                return DBcontext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<EmpHealthItem> HealthcardListData(int EmpId)
        {
            try
            {
                Mapper.CreateMap<EmployeeHealthCard, EmpHealthItem>();
                var objHealth = (from EmpHealth in DBcontext.EmployeeHealthCards
                                 join Emp in DBcontext.employee_master on EmpHealth.EmpId equals Emp.id
                                 where EmpHealth.EmpId == (EmpId == 0 ? EmpHealth.EmpId : EmpId)
                                 select new EmpHealthItem
                                 {
                                     EmpDetails = new EmployeeItem()
                                     {
                                         Empname = Emp.Empname
                                     },
                                     CardNo = EmpHealth.CardNo,
                                     IssueDate = EmpHealth.IssueDate,
                                     ExpiryDate = EmpHealth.ExpiryDate,
                                     Type = EmpHealth.Type,
                                     EmpId = EmpHealth.EmpId,
                                     HId = EmpHealth.HId
                                 }
                    ).Distinct().ToList();
                return objHealth;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public EmpHealthItem GetById(int id)
        {
            try
            {
                Mapper.CreateMap<EmployeeHealthCard, EmpHealthItem>();
                EmployeeHealthCard objHealth = DBcontext.EmployeeHealthCards.SingleOrDefault(m => m.HId == id);
                EmpHealthItem objHealthItem = Mapper.Map<EmpHealthItem>(objHealth);
                return objHealthItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int Update(EmpHealthItem model)
        {
            Mapper.CreateMap<EmpHealthItem, EmployeeHealthCard>();
            EmployeeHealthCard objHealth = DBcontext.EmployeeHealthCards.SingleOrDefault(m => m.HId == model.HId);
            objHealth = Mapper.Map(model, objHealth);
            return DBcontext.SaveChanges();
        }

        public List<EmpHealthItem> GetHealthDoc(int? Empid)
        {
            var objDoc = (from doc in DBcontext.EmployeeDocuments
                          where doc.DocCategoryId == 136 // For Passport Document Category Id
                          && doc.EmpId == Empid
                          select new EmpHealthItem()
                          {
                              DocDetails = new EMSDomain.ViewModel.DocumentItem()
                              {
                                  FileName = doc.FilleName,
                                  FileUrl = doc.FileUrl,
                                  DocCategoryId = doc.DocCategoryId,
                                  EmpId = doc.EmpId,
                                  Id = doc.Id
                              }
                          }
                ).Distinct().ToList();
            return objDoc;
        }
        public List<EmpHealthItem> GetLabourDoc(int? Empid)
        {
            var objDoc = (from doc in DBcontext.EmployeeDocuments
                          where doc.DocCategoryId == 137 // For Passport Document Category Id
                          && doc.EmpId == Empid
                          select new EmpHealthItem()
                          {
                              DocDetails = new EMSDomain.ViewModel.DocumentItem()
                              {
                                  FileName = doc.FilleName,
                                  FileUrl = doc.FileUrl,
                                  DocCategoryId = doc.DocCategoryId,
                                  EmpId = doc.EmpId,
                                  Id = doc.Id
                              }
                          }
                ).Distinct().ToList();
            return objDoc;
        }
        public List<EmpHealthItem> GetEIDDoc(int? Empid)
        {
            var objDoc = (from doc in DBcontext.EmployeeDocuments
                          where doc.DocCategoryId == 134 // For EID Document Category Id
                          && doc.EmpId == Empid
                          select new EmpHealthItem()
                          {
                              DocDetails = new EMSDomain.ViewModel.DocumentItem()
                              {
                                  FileName = doc.FilleName,
                                  FileUrl = doc.FileUrl,
                                  DocCategoryId = doc.DocCategoryId,
                                  EmpId = doc.EmpId,
                                  Id = doc.Id
                              }
                          }
                ).Distinct().ToList();
            return objDoc;
        }
    }
}
