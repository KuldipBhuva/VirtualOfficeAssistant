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
    public class VehicleHistoryService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(VehicleHistoryItem model)
        {
            try
            {
                Mapper.CreateMap<VehicleHistoryItem, Vehicle_History>();
                Vehicle_History objVehicle = Mapper.Map<Vehicle_History>(model);
                DbContext.Vehicle_History.Add(objVehicle);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<VehicleHistoryItem> GetListVehicle(int cid)
        {
            var objData = (from VH in DbContext.Vehicle_History
                           join VM in DbContext.VehicleMasters on VH.VID equals VM.VID
                           join EM in DbContext.employee_master on VH.EmpID equals EM.id
                           where VM.CompID == (cid == 0 ? VM.CompID : cid)
                           select new VehicleHistoryItem
                                  {
                                      VehicleDetails = new VehicleItem()
                                      {
                                          VehicleName = VM.VehicleName
                                      },
                                      EmpDetails = new EmployeeItem()
                                      {
                                          Empname = EM.Empname
                                      },
                                      ID = VH.ID,
                                      From_Date = VH.From_Date,
                                      To_Date = VH.To_Date,
                                      Remarks = VH.Remarks
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
        public VehicleHistoryItem GetById(int id)
        {
            Mapper.CreateMap<Vehicle_History, VehicleHistoryItem>();
            Vehicle_History objVhtbl = DbContext.Vehicle_History.SingleOrDefault(m => m.ID == id);
            VehicleHistoryItem objVHItem = Mapper.Map<VehicleHistoryItem>(objVhtbl);
            return objVHItem;
        }

        public int Update(VehicleHistoryItem model)
        {
            Mapper.CreateMap<VehicleHistoryItem, Vehicle_History>();
            Vehicle_History objVH = DbContext.Vehicle_History.SingleOrDefault(m => m.ID == model.ID);
            objVH = Mapper.Map(model, objVH);
            return DbContext.SaveChanges();
        }
    }
}
