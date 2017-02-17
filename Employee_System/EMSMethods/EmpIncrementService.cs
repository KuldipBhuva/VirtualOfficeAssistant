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
    public class EmpIncrementService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public int Insert(EmpIncrementItem model)
        {
            try
            {
                Mapper.CreateMap<EmpIncrementItem, EmployeeIncrement>();
                EmployeeIncrement objEmp = Mapper.Map<EmployeeIncrement>(model);
                DbContext.EmployeeIncrements.Add(objEmp);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<EmpIncrementItem> IncrementListData(int EmpId)
        {
            var objInc = (from EmpInc in DbContext.EmployeeIncrements
                          join Emp in DbContext.employee_master on EmpInc.EmpId equals Emp.id
                          where EmpInc.EmpId == (EmpId == 0 ? EmpInc.EmpId : EmpId)
                          select new EmpIncrementItem
                          {
                              EmpDetails = new EmployeeItem()
                              {
                                  Empname = Emp.Empname
                              },
                              IncAmount = EmpInc.IncAmount,
                              IncDate = EmpInc.IncDate,
                              IncId = EmpInc.IncId,
                              EmpId = EmpInc.EmpId
                          }
                ).Distinct().ToList();
            return objInc;
        }

        public EmpIncrementItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeIncrement, EmpIncrementItem>();
            EmployeeIncrement objInc = DbContext.EmployeeIncrements.SingleOrDefault(m => m.IncId == id);
            EmpIncrementItem objIncItem = Mapper.Map<EmpIncrementItem>(objInc);
            return objIncItem;
        }

        public int Update(EmpIncrementItem model)
        {
            Mapper.CreateMap<EmpIncrementItem, EmployeeIncrement>();
            EmployeeIncrement objIncs = DbContext.EmployeeIncrements.SingleOrDefault(m => m.IncId == model.IncId);
            objIncs = Mapper.Map(model, objIncs);
            return DbContext.SaveChanges();
        }

    }
}
