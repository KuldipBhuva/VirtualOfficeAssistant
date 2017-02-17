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
    public class EmpRelativeService
    {
        EmployeeEntities DBContext = new EmployeeEntities();

        //For Insert Records in EmployeeRelative table
        public int Insert(EmpRelativeItem model)
        {
            Mapper.CreateMap<EmpRelativeItem, EmployeeRelative>();
            EmployeeRelative objRelative = Mapper.Map<EmployeeRelative>(model);
            DBContext.EmployeeRelatives.Add(objRelative);
            return DBContext.SaveChanges();
        }

        // for  Display Gridview by Empid
        public List<EmpRelativeItem> RelativeListData(int EmpId)
        {
            Mapper.CreateMap<EmployeeRelative, EmpRelativeItem>();
            var objRel = (from EmpRel in DBContext.EmployeeRelatives
                          join Emp in DBContext.employee_master on EmpRel.EmpId equals Emp.id
                          where EmpRel.EmpId == (EmpId == 0 ? EmpRel.EmpId : EmpId)
                          select new EmpRelativeItem
                          {
                              EmployeeDetails = new EmployeeItem()
                              {
                                  Empname = Emp.Empname
                              },
                              RName = EmpRel.RName,
                              RMobile = EmpRel.RMobile,
                              Relation = EmpRel.Relation,
                              EmpId = EmpRel.EmpId,
                              Id = EmpRel.Id
                          }
                ).Distinct().ToList();
            return objRel;
        }

        public EmpRelativeItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeRelative, EmpRelativeItem>();
            EmployeeRelative objRelative = DBContext.EmployeeRelatives.SingleOrDefault(m => m.Id == id);
            EmpRelativeItem objRelativeItem = Mapper.Map<EmpRelativeItem>(objRelative);
            return objRelativeItem;
        }

        public int Update(EmpRelativeItem model)
        {
            Mapper.CreateMap<EmpRelativeItem, EmployeeRelative>();
            EmployeeRelative objRel = DBContext.EmployeeRelatives.SingleOrDefault(m => m.Id == model.Id);
            objRel = Mapper.Map(model, objRel);
            return DBContext.SaveChanges();
        }

    }
}
