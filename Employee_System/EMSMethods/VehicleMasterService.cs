using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Vehicle;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class VehicleMasterService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(VehicleItem model, PetrolCardItem model1)
        {
            try
            {
                Mapper.CreateMap<VehicleItem, VehicleMaster>();
                VehicleMaster objVehicle = Mapper.Map<VehicleMaster>(model);
                DbContext.VehicleMasters.Add(objVehicle);
                DbContext.SaveChanges();

                int Vid = DbContext.VehicleMasters.Max(item => item.VID);
                if (Vid != null || Vid != '0')
                {
                    model1.VID = Vid;
                    model1.Status = "Active";
                    Mapper.CreateMap<PetrolCardItem, Petrol_Card>();
                    Petrol_Card objPetrol = Mapper.Map<Petrol_Card>(model1);
                    DbContext.Petrol_Card.Add(objPetrol);
                    return DbContext.SaveChanges();
                }
                return DbContext.VehicleMasters.Max(item => item.VID);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<VehicleItem> VehicleListData(string status, int cid)
        {
            var objData = (from vehicle in DbContext.VehicleMasters
                           join comp in DbContext.Company_master on vehicle.CompID equals comp.id
                           where (status == "0" || vehicle.Status == status) && vehicle.CompID == (cid == 0 ? vehicle.CompID : cid)
                           select new VehicleItem
                           {
                               CompDetails = new CompanyItem()
                               {
                                   CompName = comp.CompName
                               },
                               CompID = vehicle.CompID,
                               VehicleNo = vehicle.VehicleNo,
                               VehicleName = vehicle.VehicleName,
                               ModelName = vehicle.ModelName,
                               Market_Value = vehicle.Market_Value,
                               CurrentValue = vehicle.CurrentValue,
                               VID = vehicle.VID,
                               EmpID = vehicle.EmpID,
                               Branch_ID = vehicle.Branch_ID,
                               Reg_Date = vehicle.Reg_Date,
                               Reg_Exp_Date = vehicle.Reg_Exp_Date,
                               SalikNo = vehicle.SalikNo
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
        public List<BranchItem> GetBranch()
        {
            Mapper.CreateMap<Branch_master, BranchItem>();
            List<Branch_master> lstBranch = DbContext.Branch_master.ToList();
            List<BranchItem> lstBranchItem = Mapper.Map<List<BranchItem>>(lstBranch);
            return lstBranchItem;
        }
        public List<EmployeeItem> GetEmp(int cid)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.Where(m => m.WorkingStatus != 94 && m.Compid == (cid == 0 ? m.Compid : cid)).ToList();
            List<EmployeeItem> lstEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return lstEmp;
        }
        public int Update(VehicleItem model, PetrolCardItem model1)
        {
            Mapper.CreateMap<VehicleItem, VehicleMaster>();
            VehicleMaster objVehicle = DbContext.VehicleMasters.SingleOrDefault(m => m.VID == model.VID);
            objVehicle = Mapper.Map(model, objVehicle);
            DbContext.SaveChanges();

            model1.VID = model.VID;
            Mapper.CreateMap<PetrolCardItem, Petrol_Card>();
            Petrol_Card objPetrol = DbContext.Petrol_Card.SingleOrDefault(m => m.VID == model1.VID);
            objPetrol = Mapper.Map(model1, objPetrol);

            return DbContext.SaveChanges();


        }
        public VehicleItem GetById(int id)
        {
            var objVehicleItem = (from Vhcl in DbContext.VehicleMasters
                                  join ptrol in DbContext.Petrol_Card on Vhcl.VID equals ptrol.VID into P
                                  from p1 in P.DefaultIfEmpty()
                                  where Vhcl.VID == id
                                  select new VehicleItem()
                                  {
                                      VID = Vhcl.VID,
                                      VehicleName = Vhcl.VehicleName,
                                      VehicleNo = Vhcl.VehicleNo,
                                      Reg_Date = Vhcl.Reg_Date,
                                      Purchased_Date = Vhcl.Purchased_Date,
                                      Reg_Exp_Date = Vhcl.Reg_Exp_Date,
                                      Remarks = Vhcl.Remarks,
                                      Status = Vhcl.Status,
                                      CompID = Vhcl.CompID,
                                      Branch_ID = Vhcl.Branch_ID,
                                      Depriciation = Vhcl.Depriciation,
                                      EmpID = Vhcl.EmpID,
                                      Market_Value = Vhcl.Market_Value,
                                      Purchase_Value = Vhcl.Purchase_Value,
                                      ModelName = Vhcl.ModelName,
                                      CurrentValue = Vhcl.CurrentValue,
                                      UsedFor = Vhcl.UsedFor,
                                      SalikNo = Vhcl.SalikNo,
                                      PCardItem = new PetrolCardItem()
                                      {
                                          PetrolCardNameID = p1.PetrolCardNameID,
                                          PetroCardNo = p1.PetroCardNo,
                                          Remarks = p1.Remarks,
                                          Status = p1.Status,
                                          IssueDate = p1.IssueDate,
                                          ExpireDate = p1.ExpireDate,
                                          ID = (p1.ID == null || p1.ID == 0) ? 0 : p1.ID,
                                          PaymentModeID = p1.PaymentModeID


                                      },
                                      WeightBearCapacity = Vhcl.WeightBearCapacity,
                                      Length = Vhcl.Length,
                                      LengthToUse = Vhcl.LengthToUse,
                                      Width = Vhcl.Width,
                                      Height = Vhcl.Height,
                                      HeightToUse = Vhcl.HeightToUse,
                                      PermitType = Vhcl.PermitType,
                                      PermitNo = Vhcl.PermitNo,
                                      PermitExpiryDate = Vhcl.PermitExpiryDate,
                                      TVStatus = Vhcl.TVStatus,
                                      VehicleOwnerName = Vhcl.VehicleOwnerName,
                                      OwnerAddress = Vhcl.OwnerAddress,
                                      FromDate = Vhcl.FromDate,
                                      ToDate = Vhcl.ToDate
                                  }
                ).Distinct().FirstOrDefault();
            return objVehicleItem;
        }
        public List<VehicleItem> getByIDVehicle(int id)
        {
            var objVehicleItem = (from Vhcl in DbContext.VehicleMasters
                                  join ptrol in DbContext.Petrol_Card on Vhcl.VID equals ptrol.VID into P
                                  from p1 in P.DefaultIfEmpty()
                                  join em in DbContext.employee_master on Vhcl.EmpID equals em.id
                                  join cm in DbContext.Company_master on Vhcl.CompID equals cm.id
                                  join bm in DbContext.Branch_master on Vhcl.Branch_ID equals bm.id
                                  where Vhcl.VID == id
                                  select new VehicleItem()
                                  {
                                      VID = Vhcl.VID,
                                      VehicleName = Vhcl.VehicleName,
                                      VehicleNo = Vhcl.VehicleNo,
                                      Reg_Date = Vhcl.Reg_Date,
                                      Purchased_Date = Vhcl.Purchased_Date,
                                      Reg_Exp_Date = Vhcl.Reg_Exp_Date,
                                      Remarks = Vhcl.Remarks,
                                      Status = Vhcl.Status,
                                      CompID = Vhcl.CompID,
                                      Branch_ID = Vhcl.Branch_ID,
                                      Depriciation = Vhcl.Depriciation,
                                      EmpID = Vhcl.EmpID,
                                      Market_Value = Vhcl.Market_Value,
                                      Purchase_Value = Vhcl.Purchase_Value,
                                      ModelName = Vhcl.ModelName,
                                      CurrentValue = Vhcl.CurrentValue,
                                      PCardItem = new PetrolCardItem()
                                      {
                                          PetrolCardNameID = p1.PetrolCardNameID,
                                          PetroCardNo = p1.PetroCardNo,
                                          Remarks = p1.Remarks,
                                          Status = p1.Status,
                                          IssueDate = p1.IssueDate,
                                          ExpireDate = p1.ExpireDate,
                                          ID = (p1.ID == null || p1.ID == 0) ? 0 : p1.ID,
                                          PaymentModeID = p1.PaymentModeID
                                      },
                                      CompDetails = new CompanyItem()
                                      {
                                          CompName = cm.CompName
                                      },
                                      BranchDetails = new BranchItem()
                                      {
                                          BranchName = bm.BranchName
                                      },
                                      EmpDetails = new EmployeeItem()
                                      {
                                          Empname = em.Empname,
                                          Empno = em.Empno
                                      },
                                      WeightBearCapacity = Vhcl.WeightBearCapacity,
                                      Length = Vhcl.Length,
                                      LengthToUse = Vhcl.LengthToUse,
                                      Width = Vhcl.Width,
                                      Height = Vhcl.Height,
                                      HeightToUse = Vhcl.HeightToUse,
                                      PermitType = Vhcl.PermitType,
                                      PermitNo = Vhcl.PermitNo,
                                      PermitExpiryDate = Vhcl.PermitExpiryDate,
                                      TVStatus = Vhcl.TVStatus,
                                      VehicleOwnerName = Vhcl.VehicleOwnerName,
                                      OwnerAddress = Vhcl.OwnerAddress,
                                      FromDate = Vhcl.FromDate,
                                      ToDate = Vhcl.ToDate
                                  }
                ).Distinct().ToList();
            return objVehicleItem;
        }
        public List<VehicleItem> GetVehicleByComp(int CompCode)
        {
            var Vehicle = (from data in DbContext.VehicleMasters.Where(c => c.CompID == CompCode && c.Status == "Active")
                           select new VehicleItem()
                           {
                               VID = data.VID,
                               VehicleName = data.VehicleName,
                           }
              ).ToList();
            return Vehicle;
        }
        public string Getid()
        {
            try
            {
                string s = DbContext.VehicleMasters.Max(m => m.VID).ToString();
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save(VehicleDocumentItem model)
        {
            VehicleDocument objVehicleDoc = new VehicleDocument();
            objVehicleDoc.FileName = model.FileName;
            objVehicleDoc.FileUrl = model.FileUrl;
            objVehicleDoc.VId = model.VId;
            objVehicleDoc.Status = "Active";
            objVehicleDoc.CreatedDate = System.DateTime.Now;
            DbContext.VehicleDocuments.Add(objVehicleDoc);
            DbContext.SaveChanges();
        }
        public List<VehicleDocumentItem> getVehicleDoc(int id)
        {
            var objVDoc = (from VM in DbContext.VehicleMasters
                           join VD in DbContext.VehicleDocuments on VM.VID equals VD.VId
                           where VD.VId == id
                           select new VehicleDocumentItem()
                           {

                               FileName = VD.FileName,
                               FileUrl = VD.FileUrl,
                               VDocId = VD.VDocId,
                               VId = VD.VId
                           }
                ).Distinct().ToList();

            return objVDoc;
        }
    }
}
