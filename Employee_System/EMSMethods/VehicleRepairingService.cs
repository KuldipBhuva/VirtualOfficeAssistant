using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Vehicle;

namespace EMSMethods
{
    public class VehicleRepairingService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(VehicleRepairingItem model)
        {
            try
            {
                Mapper.CreateMap<VehicleRepairingItem, VehicleRepairing>();
                VehicleRepairing objVehicle = Mapper.Map<VehicleRepairing>(model);
                DbContext.VehicleRepairings.Add(objVehicle);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<VehicleRepairingItem> VehicleRepairingListData(int cid)
        {
            var objData = (from vehicle in DbContext.VehicleRepairings
                           join VM in DbContext.VehicleMasters on vehicle.VID equals VM.VID //into OBJ
                           //from left in OBJ.DefaultIfEmpty()
                           join CM in DbContext.Company_master on vehicle.CompID equals CM.id
                           join master in DbContext.Masters_Tran on vehicle.Repairing_TypeID equals master.Id into mast
                           from m in mast.DefaultIfEmpty()
                           where vehicle.CompID == (cid == 0 ? vehicle.CompID : cid)
                           select new VehicleRepairingItem
                           {
                               MasterDetails = new clsMasterData()
                               {
                                   Name = (m == null ? string.Empty : m.Name)
                               },
                               CompanyDetails = new CompanyItem()
                               {
                                   CompName = CM.CompName
                               },
                               VehicleDetails = new VehicleItem()
                               {
                                   VehicleName = VM.VehicleName
                                   //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID
                               },
                               ID = vehicle.ID,
                               CompID = vehicle.CompID,
                               VID = vehicle.VID,
                               //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                               Repairing_TypeID = vehicle.Repairing_TypeID,
                               Rep_Date = vehicle.Rep_Date,
                               Amount_Paid = vehicle.Amount_Paid,
                               Remarks = vehicle.Remarks,
                               UpdatedOn = vehicle.UpdatedOn,
                               ExpType = vehicle.ExpType
                           }
                ).Distinct().ToList();
            return objData;
        }
        public List<CompanyItem> GetAllComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> lstmasterdata = Mapper.Map<List<CompanyItem>>(tblMaster);
            return lstmasterdata;
        }
        public List<VehicleItem> GetVehicle(int cid)
        {
            Mapper.CreateMap<VehicleMaster, VehicleItem>();
            List<VehicleMaster> tblVehicle = DbContext.VehicleMasters.Where(m => m.Status == "Active" && m.CompID == (cid == 0 ? m.CompID : cid)).ToList();
            List<VehicleItem> lstVehicle = Mapper.Map<List<VehicleItem>>(tblVehicle);
            return lstVehicle;
        }
        public List<clsMasterData> GetRType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 23 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public int Update(VehicleRepairingItem model)
        {
            Mapper.CreateMap<VehicleRepairingItem, VehicleRepairing>();
            VehicleRepairing objRepairing = DbContext.VehicleRepairings.SingleOrDefault(m => m.ID == model.ID);
            objRepairing = Mapper.Map(model, objRepairing);
            return DbContext.SaveChanges();
        }
        public VehicleRepairingItem GetById(int id)
        {
            Mapper.CreateMap<VehicleRepairing, VehicleRepairingItem>();
            VehicleRepairing objRepairing = DbContext.VehicleRepairings.SingleOrDefault(m => m.ID == id);
            VehicleRepairingItem objVehicleItem = Mapper.Map<VehicleRepairingItem>(objRepairing);
            return objVehicleItem;
        }
    }

}
