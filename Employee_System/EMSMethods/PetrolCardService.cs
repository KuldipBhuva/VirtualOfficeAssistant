using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Vehicle;

namespace EMSMethods
{
    public class PetrolCardService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(PetrolCardItem model)
        {
            try
            {
                Mapper.CreateMap<PetrolCardItem, Petrol_Card>();
                Petrol_Card objPetrol = Mapper.Map<Petrol_Card>(model);
                DbContext.Petrol_Card.Add(objPetrol);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<PetrolCardItem> PetrolCardListData()
        {
            var objData = (from petrol in DbContext.Petrol_Card
                           join VM in DbContext.VehicleMasters on petrol.VID equals VM.VID
                           join Mast in DbContext.Masters_Tran on petrol.PetrolCardNameID equals Mast.Id
                           join Mast1 in DbContext.Masters_Tran on petrol.PaymentModeID equals Mast1.Id
                           select new PetrolCardItem
                           {
                               VehicleDetails = new VehicleItem()
                               {
                                   VehicleName = VM.VehicleName
                               },
                               MasterDetails = new MasterDataItem()
                               {
                                   Name = Mast.Name
                               },
                               MasterDetails1 = new MasterDataItem()
                               {
                                   Name=Mast1.Name
                               },
                               IssueDate=petrol.IssueDate,
                               ExpireDate=petrol.ExpireDate,
                               Remarks=petrol.Remarks,
                               Status=petrol.Status
                           }
                ).Distinct().ToList();
            return objData;
        }
        public List<VehicleItem> GetVehicle()
        {
            Mapper.CreateMap<VehicleMaster, VehicleItem>();
            List<VehicleMaster> tblVehicle = DbContext.VehicleMasters.ToList();
            List<VehicleItem> lstVehicle = Mapper.Map<List<VehicleItem>>(tblVehicle);
            return lstVehicle;
        }
        public List<clsMasterData> GetPaymentType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 20 && m.Status=="Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public List<clsMasterData> GetPCardType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 38 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
