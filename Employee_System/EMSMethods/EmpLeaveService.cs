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
    public class EmpLeaveService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(EmpLeaveItem model)
        {
            try
            {
                Mapper.CreateMap<EmpLeaveItem, EmployeeLeave>();
                EmployeeLeave objEmp = Mapper.Map<EmployeeLeave>(model);
                DbContext.EmployeeLeaves.Add(objEmp);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<EmpLeaveItem> LeaveListData(int EmpId)
        {
            var objDep = (from EmpLeave in DbContext.EmployeeLeaves
                          join Emp in DbContext.employee_master on EmpLeave.EmpID equals Emp.id
                          join ltype in DbContext.Masters_Tran on EmpLeave.LTypeID equals ltype.Id
                          where EmpLeave.EmpID == (EmpId == 0 ? EmpLeave.EmpID : EmpId)
                          select new EmpLeaveItem
                          {
                              MasterDetails=new clsMasterData()
                              {
                                  Name=ltype.Name
                              },
                              EmpDetails = new EmployeeItem()
                              {
                                  Empname = Emp.Empname
                              },                              
                              LID = EmpLeave.LID,
                              LFrom = EmpLeave.LFrom,
                              LTo = EmpLeave.LTo,                              
                              Remarks = EmpLeave.Remarks,
                              Status = EmpLeave.Status,
                              EmpID=EmpLeave.EmpID,
                           
                          }
                ).Distinct().ToList();
            return objDep;
        }

        public EmpLeaveItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeLeave, EmpLeaveItem>();
            EmployeeLeave objDep = DbContext.EmployeeLeaves.SingleOrDefault(m => m.LID == id);
            EmpLeaveItem objDepItem = Mapper.Map<EmpLeaveItem>(objDep);
            return objDepItem;
        }

        public int Update(EmpLeaveItem model)
        {
            //Mapper.CreateMap<EmpLeaveItem, EmployeeLeave>();
            EmployeeLeave objDep = DbContext.EmployeeLeaves.SingleOrDefault(m => m.LID == model.LID);
            objDep.EmpID = model.EmpID;
            objDep.LFrom = model.LFrom;
            objDep.LTo = model.LTo;
            objDep.LTypeID = model.LTypeID;
            objDep.Remarks = model.Remarks;
            objDep.UpdatedBy = model.UpdatedBy;
            objDep.UpdatedDate = model.UpdatedDate;
            objDep.Status = model.Status;
            //objDep = Mapper.Map(model, objDep);
            return DbContext.SaveChanges();
        }
        public List<clsMasterData> GetALLLeaveTypeId()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 36 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}

