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
    public class EmpAbscondingService
    {
        EmployeeEntities DBContext = new EmployeeEntities();

        public int Insert(EmpAbscondingItem model)
        {
            try
            {
                Mapper.CreateMap<EmpAbscondingItem, EmployeeAbsconding>();
                EmployeeAbsconding objempAbsconding = Mapper.Map<EmployeeAbsconding>(model);
                DBContext.EmployeeAbscondings.Add(objempAbsconding);
                return DBContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EmpAbscondingItem> AbsListData(int EmpId)
        {
            var objAbs = (from EmpAbs in DBContext.EmployeeAbscondings
                          join Emp in DBContext.employee_master on EmpAbs.EmpId equals Emp.id
                          where EmpAbs.EmpId == (EmpId == 0 ? EmpAbs.EmpId : EmpId)
                          select new EmpAbscondingItem
                          {
                              EmpDetails = new EmployeeItem()
                              {
                                  Empname = Emp.Empname
                              },
                              AbscHistory = EmpAbs.AbscHistory,
                              Remarks = EmpAbs.Remarks,
                              Status = EmpAbs.Status,
                              EmpId = EmpAbs.EmpId,
                              AbscId = EmpAbs.AbscId
                          }
                ).Distinct().ToList();
            return objAbs;
        }

        public EmpAbscondingItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeAbsconding, EmpAbscondingItem>();
            EmployeeAbsconding objAbs = DBContext.EmployeeAbscondings.SingleOrDefault(m => m.AbscId == id);
            EmpAbscondingItem objAbsItem = Mapper.Map<EmpAbscondingItem>(objAbs);
            return objAbsItem;
        }

        public int Update(EmpAbscondingItem model)
        {
            Mapper.CreateMap<EmpAbscondingItem, EmployeeAbsconding>();
            EmployeeAbsconding objAbs = DBContext.EmployeeAbscondings.SingleOrDefault(m => m.AbscId == model.AbscId);
            objAbs = Mapper.Map(model, objAbs);
            return DBContext.SaveChanges();
        }
        public List<EmpAbscondingItem> GetAbscondingDoc(int empid)
        {
            var objLicence = (from doc in DBContext.EmployeeDocuments
                              where doc.DocCategoryId == 132 && doc.EmpId == empid
                              select new EmpAbscondingItem()
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
