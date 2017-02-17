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
    public class ConsignmentService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = Dbcontext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public int Insert(ConsignmentModel model)
        {
            Mapper.CreateMap<ConsignmentModel, ConsignmentMaster>();
            ConsignmentMaster objCompany = Mapper.Map<ConsignmentMaster>(model);
            Dbcontext.ConsignmentMasters.Add(objCompany);
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
                            InvoiceNo=cm.InvoiceNo,
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
        public List<PartyModel> getConsigner(int rid, int cid)
        {
            Mapper.CreateMap<PartyMaster, PartyModel>();
            List<PartyMaster> objCompanyMaster = Dbcontext.PartyMasters.Where(m => m.Status == true && m.CompID == (rid == 111 || cid == 0 ? m.CompID : cid)).ToList();
            List<PartyModel> objCompItem = Mapper.Map<List<PartyModel>>(objCompanyMaster);
            return objCompItem;
        }
        public List<PartyModel> getConsignee(int rid, int cid)
        {
            Mapper.CreateMap<PartyMaster, PartyModel>();
            List<PartyMaster> objCompanyMaster = Dbcontext.PartyMasters.Where(m => m.Status == true && m.CompID == (rid == 111 || cid == 0 ? m.CompID : cid)).ToList();
            List<PartyModel> objCompItem = Mapper.Map<List<PartyModel>>(objCompanyMaster);
            return objCompItem;
        }
        public List<VehicleItem> getVehicle(int rid, int cid)
        {
            Mapper.CreateMap<VehicleMaster, VehicleItem>();
            List<VehicleMaster> objCompanyMaster = Dbcontext.VehicleMasters.Where(m => m.Status == "Active" && m.CompID == (rid == 111 || cid == 0 ? m.CompID : cid)).ToList();
            List<VehicleItem> objCompItem = Mapper.Map<List<VehicleItem>>(objCompanyMaster);
            return objCompItem;
        }
        public int Update(ConsignmentModel model)
        {
            Mapper.CreateMap<ConsignmentModel, ConsignmentMaster>();
            ConsignmentMaster objComp = Dbcontext.ConsignmentMasters.SingleOrDefault(m => m.GrNo == model.GrNo);
            objComp = Mapper.Map(model, objComp);
            return Dbcontext.SaveChanges();
        }
        public ConsignmentModel GetById(int id)
        {
            Mapper.CreateMap<ConsignmentMaster, ConsignmentModel>();
            ConsignmentMaster objComp = Dbcontext.ConsignmentMasters.SingleOrDefault(m => m.GrNo == id);
            ConsignmentModel objCItem = Mapper.Map<ConsignmentModel>(objComp);
            return objCItem;
        }
        public PartyModel getPartyById(int id)
        {
            Mapper.CreateMap<PartyMaster, PartyModel>();
            PartyMaster objComp = Dbcontext.PartyMasters.SingleOrDefault(m => m.PID == id);
            PartyModel objCItem = Mapper.Map<PartyModel>(objComp);
            return objCItem;
        }
        public ConsignmentModel getGRbyID(int grid)
        {
            //Mapper.CreateMap<ConsignmentMaster, ConsignmentModel>();
            //List<ConsignmentMaster> objCompanyMaster = Dbcontext.ConsignmentMasters.ToList();
            //List<ConsignmentModel> objCompItem = Mapper.Map<List<ConsignmentModel>>(objCompanyMaster);
            //return objCompItem;

            var data = (from cm in Dbcontext.ConsignmentMasters
                        join pm in Dbcontext.PartyMasters on cm.ConsigneeID equals pm.PID
                        join ppm in Dbcontext.PartyMasters on cm.ConsignorID equals ppm.PID
                        where cm.GrNo == grid
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
                                PartyName = pm.PartyName,
                                BillingAddress = pm.BillingAddress,
                                BillingName = pm.BillingName,
                                BillingPhone = pm.BillingPhone,
                                Address = pm.Address,
                                ContactPerson = pm.ContactPerson,
                                Phone = pm.Phone,
                                Mobile = pm.Mobile,
                                Email = pm.Email

                            },
                            ConsignorDetails = new PartyModel()
                            {
                                PartyName = ppm.PartyName,
                                BillingAddress=ppm.BillingAddress,
                                BillingName=ppm.BillingName,
                                BillingPhone=ppm.BillingPhone,
                                Address=ppm.Address,
                                ContactPerson=ppm.ContactPerson,
                                Phone=ppm.Phone,
                                Mobile=ppm.Mobile,
                                Email=ppm.Email
                            }
                        }).SingleOrDefault();
            return data;
        }
        public List<ChallanModel> getChallanTran(int grid)
        {
            var data = (from ct in Dbcontext.ChallanTrans
                        join cm in Dbcontext.ChallanMasters on ct.CID equals cm.CID
                        join gr in Dbcontext.ConsignmentMasters on ct.GRID equals gr.GrNo
                        join pm in Dbcontext.PartyMasters on gr.ConsigneeID equals pm.PID
                        join ppm in Dbcontext.PartyMasters on gr.ConsignorID equals ppm.PID
                        where ct.GRID == grid
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
                                BookingDate = gr.BookingDate,
                                InvoiceAmount = gr.InvoiceAmount,
                                Packages = gr.Packages,
                                Source = gr.Source,
                                Destination = gr.Destination,
                                DeliveryMode = gr.DeliveryMode,
                                TaxPaidBy = gr.TaxPaidBy
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
