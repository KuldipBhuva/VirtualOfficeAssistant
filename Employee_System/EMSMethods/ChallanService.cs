using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;

namespace EMSMethods
{
    public class ChallanService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();
        public int Insert(ChallanModel model)
        {
            Mapper.CreateMap<ChallanModel, ChallanMaster>();
            ChallanMaster objCompany = Mapper.Map<ChallanMaster>(model);
            Dbcontext.ChallanMasters.Add(objCompany);
            //Dbcontext.SaveChanges();

            //int id = Dbcontext.ChallanMasters.Max(m => m.CID);
            //if (id != null && id != 0)
            //{
            //    model.CID = id;
            //    Mapper.CreateMap<ChallanModel, ChallanTran>();
            //    ChallanTran objTran = Mapper.Map<ChallanTran>(model);
            //    Dbcontext.ChallanTrans.Add(objTran);
            //}
            return Dbcontext.SaveChanges();
        }
        public int InsertChallanTran(ChallanModel model)
        {
            Mapper.CreateMap<ChallanModel, ChallanTran>();
            ChallanTran objTran = Mapper.Map<ChallanTran>(model);
            Dbcontext.ChallanTrans.Add(objTran);
            return Dbcontext.SaveChanges();
        }
        public List<ConsignmentModel> GetALL(int rid, int cid)
        {
            //Mapper.CreateMap<ConsignmentMaster, ConsignmentModel>();
            //List<ConsignmentMaster> objCompanyMaster = Dbcontext.ConsignmentMasters.ToList();
            //List<ConsignmentModel> objCompItem = Mapper.Map<List<ConsignmentModel>>(objCompanyMaster);
            //return objCompItem;

            var data = (from cm in Dbcontext.ConsignmentMasters
                        join pm in Dbcontext.PartyMasters on cm.ConsigneeID equals pm.PID
                        join ppm in Dbcontext.PartyMasters on cm.ConsignorID equals ppm.PID
                        where cm.CompID == (rid == 111 || cid == 0 ? cm.CompID : cid)
                        select new ConsignmentModel()
                        {
                            GrNo = cm.GrNo,
                            Packages = cm.Packages,
                            InvoiceAmount = cm.InvoiceAmount,

                            ConsigneeDetails = new PartyModel()
                            {
                                PartyName = pm.PartyName
                            },
                            ConsignorDetails = new PartyModel()
                            {
                                PartyName = ppm.PartyName
                            }
                        }).ToList();
            return data;
        }
        //public List<ChallanModel> getChalan(int rid, int cid)
        //{
        //    Mapper.CreateMap<ChallanMaster, ChallanModel>();
        //    List<ChallanMaster> objCompanyMaster = Dbcontext.ChallanMasters.Where(m => m.Status == true && m.CompID == (rid == 111 || cid == 0 ? m.CompID : cid)).ToList();
        //    List<ChallanModel> objCompItem = Mapper.Map<List<ChallanModel>>(objCompanyMaster);
        //    return objCompItem;
        //}
        public List<ChallanModel> getAllChallan(int rid, int cid)
        {
            //Mapper.CreateMap<ChallanMaster, ChallanModel>();
            //ChallanMaster objComp = Dbcontext.ChallanMasters.SingleOrDefault(m => m.CID == id);
            //ChallanModel objCItem = Mapper.Map<ChallanModel>(objComp);
            //return objCItem;

            var data = (from cm in Dbcontext.ChallanMasters
                        join vm in Dbcontext.VehicleMasters on cm.VID equals vm.VID
                        join em in Dbcontext.employee_master on cm.EmpID equals em.id
                        where cm.CompID == (rid == 111 || cid == 0 ? cm.CompID : cid)
                        select new ChallanModel()
                        {
                            CID = cm.CID,
                            ChallanNo = cm.ChallanNo,
                            CompID = cm.CompID,
                            VID = cm.VID,
                            EmpID = cm.EmpID,
                            StartDate = cm.StartDate,
                            EndDate = cm.EndDate,
                            Source = cm.Source,
                            Destination = cm.Destination,
                            Status = cm.Status,
                            VehicleDetails = new VehicleItem()
                            {
                                VehicleName = vm.VehicleName,
                                VehicleNo = vm.VehicleNo,
                                WeightBearCapacity = vm.WeightBearCapacity,
                                Length = vm.Length,
                                LengthToUse = vm.LengthToUse,
                                Width = vm.Width,
                                WidthToUse = vm.WidthToUse,
                                Height = vm.Height,
                                HeightToUse = vm.HeightToUse,
                                PermitType = vm.PermitType,
                                PermitNo = vm.PermitNo,
                                PermitExpiryDate = vm.PermitExpiryDate,
                                TVStatus = vm.TVStatus,
                                VehicleOwnerName = vm.VehicleOwnerName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = em.Empno,
                                Empname = em.Empname,
                                Photo = em.Photo,
                                Mobile = em.Mobile,
                                HomeTel = em.HomeTel,
                                LocalTel = em.LocalTel,
                                Email = em.Email
                            }

                        }).OrderByDescending(m => m.CID).ToList();
            return data;

        }
        public List<ConsignmentModel> getGRNo(int rid, int cid)
        {
            //Mapper.CreateMap<ConsignmentMaster, ConsignmentModel>();
            //List<ConsignmentMaster> objCompanyMaster = Dbcontext.ConsignmentMasters.Where(m => m.Status == true && m.CompID == (rid == 111 || cid == 0 ? m.CompID : cid)).ToList();
            //List<ConsignmentModel> objCompItem = Mapper.Map<List<ConsignmentModel>>(objCompanyMaster);
            //return objCompItem;

            var data = (from cm in Dbcontext.ConsignmentMasters
                        where !Dbcontext.ChallanTrans.Any(m => m.GRID == cm.GrNo) && cm.CompID == (rid == 111 || cid == 0 ? cm.CompID : cid) && cm.Status==true
                        select new ConsignmentModel()
                        {
                            GrNo=cm.GrNo
                        }).ToList();
            return data;
        }
        public List<VehicleItem> getVehicle(int rid, int cid)
        {
            Mapper.CreateMap<VehicleMaster, VehicleItem>();
            List<VehicleMaster> objCompanyMaster = Dbcontext.VehicleMasters.Where(m => m.Status == "Active" && m.CompID == (rid == 111 || cid == 0 ? m.CompID : cid)).ToList();
            List<VehicleItem> objCompItem = Mapper.Map<List<VehicleItem>>(objCompanyMaster);
            return objCompItem;
        }
        public List<EmployeeItem> GetEmp(int cid)
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = Dbcontext.employee_master.Where(m => m.WorkingStatus != 94 && m.Compid == (cid == 0 ? m.Compid : cid)).ToList();
            List<EmployeeItem> lstEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return lstEmp;
        }
        public int Update(ChallanModel model)
        {
            Mapper.CreateMap<ChallanModel, ChallanMaster>();
            ChallanMaster objComp = Dbcontext.ChallanMasters.SingleOrDefault(m => m.CID == model.CID);
            objComp = Mapper.Map(model, objComp);
            return Dbcontext.SaveChanges();
        }
        public ChallanModel GetById(int id)
        {
            //Mapper.CreateMap<ChallanMaster, ChallanModel>();
            //ChallanMaster objComp = Dbcontext.ChallanMasters.SingleOrDefault(m => m.CID == id);
            //ChallanModel objCItem = Mapper.Map<ChallanModel>(objComp);
            //return objCItem;

            var data = (from cm in Dbcontext.ChallanMasters
                        join vm in Dbcontext.VehicleMasters on cm.VID equals vm.VID
                        join em in Dbcontext.employee_master on cm.EmpID equals em.id
                        where cm.CID == id
                        select new ChallanModel()
                        {
                            CID = cm.CID,
                            ChallanNo = cm.ChallanNo,
                            CompID = cm.CompID,
                            VID = cm.VID,
                            EmpID = cm.EmpID,
                            StartDate = cm.StartDate,
                            EndDate = cm.EndDate,
                            Source = cm.Source,
                            Destination = cm.Destination,
                            Status = cm.Status,
                            VehicleDetails = new VehicleItem()
                            {
                                VehicleName = vm.VehicleName,
                                VehicleNo = vm.VehicleNo,
                                WeightBearCapacity = vm.WeightBearCapacity,
                                Length = vm.Length,
                                LengthToUse = vm.LengthToUse,
                                Width = vm.Width,
                                WidthToUse = vm.WidthToUse,
                                Height = vm.Height,
                                HeightToUse = vm.HeightToUse,
                                PermitType = vm.PermitType,
                                PermitNo = vm.PermitNo,
                                PermitExpiryDate = vm.PermitExpiryDate,
                                TVStatus = vm.TVStatus,
                                VehicleOwnerName = vm.VehicleOwnerName
                            },
                            EmpDetails = new EmployeeItem()
                            {
                                Empno = em.Empno,
                                Empname = em.Empname,
                                Photo = em.Photo,
                                Mobile = em.Mobile,
                                HomeTel = em.HomeTel,
                                LocalTel = em.LocalTel,
                                Email = em.Email
                            }

                        }).SingleOrDefault();
            return data;

        }
        public ChallanModel challanTranById(int id)
        {
            Mapper.CreateMap<ChallanTran, ChallanModel>();
            ChallanTran objComp = Dbcontext.ChallanTrans.SingleOrDefault(m => m.CTID == id);
            ChallanModel objCItem = Mapper.Map<ChallanModel>(objComp);
            return objCItem;
        }
        public ConsignmentModel getGR(int id)
        {
            var data = (from cm in Dbcontext.ConsignmentMasters
                        join pm in Dbcontext.PartyMasters on cm.ConsigneeID equals pm.PID
                        join ppm in Dbcontext.PartyMasters on cm.ConsignorID equals ppm.PID
                        join cb in Dbcontext.PartyMasters on cm.CompBrokerID equals cb.PID into br
                        from b in br.DefaultIfEmpty()
                        where cm.GrNo == id
                        select new ConsignmentModel()
                        {
                            GrNo = cm.GrNo,
                            CompID = cm.CompID,
                            LR = cm.LR,
                            TaxPaidBy = cm.TaxPaidBy,
                            DeliveryMode = cm.DeliveryMode,
                            BillingName = cm.BillingName,
                            BillingAddress = cm.BillingAddress,
                            ConsignorID = cm.ConsignorID,
                            ConsigneeID = cm.ConsigneeID,
                            CompBrokerID = cm.CompBrokerID,
                            ConsigneeInvoiceNo = cm.ConsigneeInvoiceNo,
                            ConsigneeInvoiceValue = cm.ConsigneeInvoiceValue,
                            Source = cm.Source,
                            Destination = cm.Destination,
                            PickUp = cm.PickUp,
                            Packages = cm.Packages,
                            PackingMethod = cm.PackingMethod,
                            ActualWeight = cm.ActualWeight,
                            ChargedWeight = cm.ChargedWeight,
                            Rate = cm.Rate,
                            FreightCharge = cm.FreightCharge,
                            LabourCharge = cm.LabourCharge,
                            OtherCharge = cm.OtherCharge,
                            InvoiceNo = cm.InvoiceNo,
                            InvoiceAmount = cm.InvoiceAmount,
                            Remarks = cm.Remarks,
                            Discount = cm.Discount,
                            BookingDate = cm.BookingDate,
                            Status = cm.Status,
                            ConsigneeDetails = new PartyModel()
                            {
                                PartyName = pm.PartyName
                            },
                            ConsignorDetails = new PartyModel()
                            {
                                PartyName = ppm.PartyName
                            },
                            CompBrokeDetails = new PartyModel()
                            {
                                PartyName = (b == null ? string.Empty : b.PartyName)
                            }
                        }).SingleOrDefault();
            return data;
        }
        public List<ChallanModel> getChallanTran(int cid)
        {
            var data = (from ct in Dbcontext.ChallanTrans
                        join cm in Dbcontext.ChallanMasters on ct.CID equals cm.CID
                        join gr in Dbcontext.ConsignmentMasters on ct.GRID equals gr.GrNo
                        join pm in Dbcontext.PartyMasters on gr.ConsigneeID equals pm.PID
                        join ppm in Dbcontext.PartyMasters on gr.ConsignorID equals ppm.PID
                        where ct.CID == cid && gr.Status==true
                        select new ChallanModel()
                        {
                            CTID = ct.CTID,
                            CID = ct.CID,
                            GRID = ct.GRID,
                            ChallanNo = cm.ChallanNo,
                            StartDate = cm.StartDate,
                            EndDate = cm.EndDate,
                            Source = cm.Source,
                            Destination = cm.Destination,  
                          
                            ConsignDetails = new ConsignmentModel()
                            {
                                GrNo = gr.GrNo,
                                LR = gr.LR,
                                PickUp=gr.PickUp,
                                BookingDate = gr.BookingDate,
                                InvoiceAmount = gr.InvoiceAmount,
                                InvoiceNo=gr.InvoiceNo,
                                Packages = gr.Packages,
                                Source = gr.Source,
                                Destination = gr.Destination,
                                DeliveryMode = gr.DeliveryMode,
                                TaxPaidBy = gr.TaxPaidBy,
                                PackingMethod=gr.PackingMethod,
                                ChargedWeight=gr.ChargedWeight,
                                Rate=gr.Rate
                            },
                            ConsigneeDetails = new PartyModel()
                            {
                                PartyName = pm.PartyName
                            },
                            ConsignorDetails = new PartyModel()
                            {
                                PartyName = ppm.PartyName
                            }
                        }).ToList();
            return data;
        }
        public List<ChallanModel> getAllChallanTran()
        {
            var data = (from ct in Dbcontext.ChallanTrans
                        join cm in Dbcontext.ChallanMasters on ct.CID equals cm.CID
                        join gr in Dbcontext.ConsignmentMasters on ct.GRID equals gr.GrNo
                        join pm in Dbcontext.PartyMasters on gr.ConsigneeID equals pm.PID
                        join ppm in Dbcontext.PartyMasters on gr.ConsignorID equals ppm.PID
                        where cm.Status==true && gr.Status==true
                        select new ChallanModel()
                        {
                            CTID = ct.CTID,
                            CID = ct.CID,
                            GRID = ct.GRID,
                            ChallanNo = cm.ChallanNo,
                            StartDate = cm.StartDate,
                            EndDate = cm.EndDate,
                            Source = cm.Source,
                            Destination = cm.Destination,
                            Status=cm.Status,
                            ConsignDetails = new ConsignmentModel()
                            {
                                GrNo = gr.GrNo,
                                InvoiceNo=gr.InvoiceNo,
                                LR = gr.LR,
                                BookingDate = gr.BookingDate,
                                InvoiceAmount = gr.InvoiceAmount,
                                Packages = gr.Packages,
                                Source = gr.Source,
                                Destination = gr.Destination,
                                DeliveryMode = gr.DeliveryMode,
                                TaxPaidBy = gr.TaxPaidBy,
                                Status=gr.Status
                            },
                            ConsigneeDetails = new PartyModel()
                            {
                                PartyName = pm.PartyName
                            },
                            ConsignorDetails = new PartyModel()
                            {
                                PartyName = ppm.PartyName
                            }
                        }).ToList();
            return data;
        }
    }
}
