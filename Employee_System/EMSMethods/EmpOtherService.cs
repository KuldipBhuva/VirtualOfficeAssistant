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
    public class EmpOtherService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public int Insert(EmpOtherItem model)
        {
            try
            {
                Mapper.CreateMap<EmpOtherItem, EmployeeOtherDetail>();
                EmployeeOtherDetail objEmpOther = Mapper.Map<EmployeeOtherDetail>(model);
                DbContext.EmployeeOtherDetails.Add(objEmpOther);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public List<EmpOtherItem> OtherListData(int EmpId)
        {
            var objOther = (from EmpOther in DbContext.EmployeeOtherDetails
                          join Emp in DbContext.employee_master on EmpOther.EmpId equals Emp.id
                          where EmpOther.EmpId == (EmpId == 0 ? EmpOther.EmpId : EmpId)
                          select new EmpOtherItem
                          {
                              EmpDetails = new EmployeeItem()
                              {
                                  Empname = Emp.Empname
                              },
                             AwardName=EmpOther.AwardName,
                             AwardDate=EmpOther.AwardDate,
                             Remarks=EmpOther.Remarks,
                             Status=EmpOther.Status,
                             EmpId=EmpOther.EmpId,
                             OtherId=EmpOther.OtherId

                          }
                ).Distinct().ToList();
            return objOther;
        }

        public EmpOtherItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeOtherDetail, EmpOtherItem>();
            EmployeeOtherDetail objOther = DbContext.EmployeeOtherDetails.SingleOrDefault(m => m.OtherId == id);
            EmpOtherItem objOtherItem = Mapper.Map<EmpOtherItem>(objOther);
            return objOtherItem;
        }

        public int Update(EmpOtherItem model)
        {
            Mapper.CreateMap<EmpOtherItem, EmployeeOtherDetail>();
            EmployeeOtherDetail objOther = DbContext.EmployeeOtherDetails.SingleOrDefault(m => m.OtherId == model.OtherId);
            objOther = Mapper.Map(model, objOther);
            return DbContext.SaveChanges();
        }
    }
}
