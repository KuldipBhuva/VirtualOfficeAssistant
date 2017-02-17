using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel;

namespace EMSMethods
{
    public class PartyServices
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public List<PartyModel> BindParty()
        {
            Mapper.CreateMap<PartyMaster, PartyModel>();
            List<PartyMaster> objpartyMst = DbContext.PartyMasters.ToList();
            List<PartyModel> objpartyItem = Mapper.Map<List<PartyModel>>(objpartyMst);
            return objpartyItem;
        }


        public List<PartyModel> BindParentParty(int cid, int id)
        {
            var data = (from pm in DbContext.PartyMasters

                        where pm.CompID == (cid == 0 ? pm.CompID : cid) && pm.PID != id
                        select new PartyModel()

                        {
                            PID = pm.PID,
                            PPID = pm.PPID,

                            PartyName = pm.PartyName

                        }).ToList();
            return data;
        }
        public List<PartyModel> BindParentPartyAll(int cid)
        {
            var data = (from pm in DbContext.PartyMasters

                        where pm.CompID == (cid == 0 ? pm.CompID : cid)
                        select new PartyModel()

                        {
                            PID = pm.PID,
                            PPID = pm.PPID,

                            PartyName = pm.PartyName

                        }).ToList();
            return data;
        }
        public List<PartyModel> BindParentPartyByCID(int cid)
        {
            var data = (from pm in DbContext.PartyMasters

                        where pm.PID == cid
                        select new PartyModel()

                        {
                            PID = pm.PID,
                            PPID = pm.PPID,

                            PartyName = pm.PartyName

                        }).ToList();
            return data;
        }

        public int InsertParty(PartyModel model)
        {
            try
            {
                PartyMaster objParty = new PartyMaster();
                objParty.PPID = model.PPID;
                objParty.PartyName = model.PartyName;
                objParty.Mobile = model.Mobile;
                objParty.Email = model.Email;
                objParty.ContactPerson = model.ContactPerson;
                objParty.CompID = model.CompID;
                objParty.BillingPhone = model.BillingPhone;
                objParty.BillingName = model.BillingName;
                objParty.BillingAddress = model.BillingAddress;
                objParty.Address = model.Address;
                objParty.Status = model.Status;
                DbContext.PartyMasters.Add(objParty);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<PartyModel> bindList(int cid)
        {
            var objdata = (from Party in DbContext.PartyMasters
                           where Party.CompID == cid
                           select new PartyModel
                             {
                                 PartyName = Party.PartyName,
                                 Phone = Party.Phone,
                                 Mobile = Party.Mobile,
                                 Email = Party.Email,
                                 ContactPerson = Party.ContactPerson,
                                 BillingPhone = Party.BillingPhone,
                                 BillingAddress = Party.BillingAddress,
                                 BillingName = Party.BillingName,
                                 Status = Party.Status,
                                 PID = Party.PID


                             }).ToList();
            return objdata;
        }

        public PartyModel GetById(int Id)
        {
            var objdata = (from Party in DbContext.PartyMasters
                           where Party.PID == Id
                           select new PartyModel
                           {
                               PartyName = Party.PartyName,
                               Phone = Party.Phone,
                               Mobile = Party.Mobile,
                               Email = Party.Email,
                               ContactPerson = Party.ContactPerson,
                               Address =Party.Address,
                               BillingPhone = Party.BillingPhone,
                               BillingAddress = Party.BillingAddress,
                               BillingName = Party.BillingName,
                               Status = Party.Status,
                               CompID = Party.CompID,
                               PPID = Party.PPID,
                               PID = Party.PID


                           }).FirstOrDefault();
            return objdata;
        }
        public int Update(PartyModel model)
        {
            Mapper.CreateMap<PartyModel, PartyMaster>();
            PartyMaster objVehicle = DbContext.PartyMasters.SingleOrDefault(m => m.PID == model.PID);
            objVehicle = Mapper.Map(model, objVehicle);
            DbContext.SaveChanges();
            return DbContext.SaveChanges();


        }
    }
}
