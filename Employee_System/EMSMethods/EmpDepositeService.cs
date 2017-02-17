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
    public class EmpDepositeService
    {
        EmployeeEntities DBContext = new EmployeeEntities();

        public int Insert(EmpDepositeItem model)
        {
            try
            {
                Mapper.CreateMap<EmpDepositeItem, EmployeeDeposite>();
                EmployeeDeposite objempDeposite = Mapper.Map<EmployeeDeposite>(model);
                DBContext.EmployeeDeposites.Add(objempDeposite);
                return DBContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<EmpDepositeItem> DepositListData(int EmpId)
        {
            var objDep = (from EmpDep in DBContext.EmployeeDeposites
                          join Emp in DBContext.employee_master on EmpDep.EmpId equals Emp.id
                          where EmpDep.EmpId == (EmpId == 0 ? EmpDep.EmpId : EmpId)
                          select new EmpDepositeItem
                          {
                              EmpDetails = new EmployeeItem()
                              {
                                  Empname = Emp.Empname
                              },
                              DepAmount = EmpDep.DepAmount,
                              DepRemarks = EmpDep.DepRemarks,
                              DepDescription = EmpDep.DepDescription,
                              InvestDate = EmpDep.InvestDate,
                              MaturityDate = EmpDep.MaturityDate,
                              Status = EmpDep.Status,
                              EmpId = EmpDep.EmpId,
                              DepId = EmpDep.DepId
                          }
                ).Distinct().ToList();
            return objDep;
        }

        public EmpDepositeItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeDeposite, EmpDepositeItem>();
            EmployeeDeposite objDep = DBContext.EmployeeDeposites.SingleOrDefault(m => m.DepId == id);
            EmpDepositeItem objDepItem = Mapper.Map<EmpDepositeItem>(objDep);
            return objDepItem;
        }

        public int Update(EmpDepositeItem model)
        {
            Mapper.CreateMap<EmpDepositeItem, EmployeeDeposite>();
            EmployeeDeposite objDep = DBContext.EmployeeDeposites.SingleOrDefault(m => m.DepId == model.DepId);
            objDep = Mapper.Map(model, objDep);
            return DBContext.SaveChanges();
        }
    }
}
