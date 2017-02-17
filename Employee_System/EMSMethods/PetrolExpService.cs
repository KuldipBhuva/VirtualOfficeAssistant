using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;

namespace EMSMethods
{
    public class PetrolExpService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(PetrolExpItem model)
        {
            try
            {
                Mapper.CreateMap<PetrolExpItem, Petrol_Expense>();
                Petrol_Expense objPetrol = Mapper.Map<Petrol_Expense>(model);
                DbContext.Petrol_Expense.Add(objPetrol);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<PetrolExpItem> PetrolListData(int cid)
        {
            var objData = (from petrol in DbContext.Petrol_Expense
                           join emp in DbContext.employee_master on petrol.EmpID equals emp.id
                           join VM in DbContext.VehicleMasters on petrol.VID equals VM.VID
                           where VM.CompID == (cid == 0 ? VM.CompID : cid)
                           select new PetrolExpItem
                           {
                               EmpDetails = new EmployeeItem()
                               {
                                   Empname = emp.Empname
                               },
                               VehicleDetails = new VehicleItem()
                               {
                                   VehicleName = VM.VehicleName
                               },
                               ID = petrol.ID,
                               Bill_Date = petrol.Bill_Date,
                               Qty = petrol.Qty,
                               Amount = petrol.Amount,
                               KM_From = petrol.KM_From,
                               KM_To = petrol.KM_To,
                               Remarks = petrol.Remarks
                           }
                ).Distinct().ToList();
            return objData;
        }
        public List<VehicleItem> GetVehicle(int cid)
        {
            Mapper.CreateMap<VehicleMaster, VehicleItem>();
            List<VehicleMaster> tblVehicle = DbContext.VehicleMasters.Where(m => m.Status == "Active" && m.CompID == (cid == 0 ? m.CompID : cid)).ToList();
            List<VehicleItem> lstVehicle = Mapper.Map<List<VehicleItem>>(tblVehicle);
            return lstVehicle;
        }
        public List<EmployeeItem> GetEmp(int cid)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.Where(m => m.WorkingStatus != 94 && m.Compid == (cid == 0 ? m.Compid : cid)).ToList();
            List<EmployeeItem> listEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return listEmp;

        }
        public PetrolExpItem GetById(int id)
        {
            Mapper.CreateMap<Petrol_Expense, PetrolExpItem>();
            Petrol_Expense objDep = DbContext.Petrol_Expense.SingleOrDefault(m => m.ID == id);
            PetrolExpItem objDepItem = Mapper.Map<PetrolExpItem>(objDep);
            return objDepItem;
        }

        public int Update(PetrolExpItem model)
        {
            Mapper.CreateMap<PetrolExpItem, Petrol_Expense>();
            Petrol_Expense objDep = DbContext.Petrol_Expense.SingleOrDefault(m => m.ID == model.ID);
            objDep = Mapper.Map(model, objDep);
            return DbContext.SaveChanges();
        }
    }
}
