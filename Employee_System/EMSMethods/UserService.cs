using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Menu;

namespace EMSMethods
{
    public class UserService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(LoginItem model)
        {
            try
            {
                Mapper.CreateMap<LoginItem, Login_Master>();
                Login_Master objLogin = Mapper.Map<Login_Master>(model);
                DbContext.Login_Master.Add(objLogin);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int InsertModules(LoginItem model, int? MHId, bool chk, int? UID)
        {
            try
            {
                Mapper.CreateMap<LoginItem, UserModulesTran>();
                UserModulesTran objLogin = Mapper.Map<UserModulesTran>(model);
                objLogin.MHID = MHId;
                objLogin.IsChecked = chk;
                objLogin.CreatedDate = System.DateTime.Now;
                objLogin.CeatedBy = UID;
                objLogin.UID = UID;
                DbContext.UserModulesTrans.Add(objLogin);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public int updateModules(UserModulesItem model, int MHId, bool chk, int UID)
        public int updateModules(UserModulesItem model1)
        {
            try
            {
                //Mapper.CreateMap<UserModulesItem, UserModulesTran>();
                //UserModulesTran objLogin = new UserModulesTran();
                ////objLogin.MHID = MHId;
                //objLogin.IsChecked = chk;
                //objLogin.UpdatedDate = System.DateTime.Now;
                //objLogin.UpdatedBy = UID;
                ////objLogin.UID = UID;
                //UserModulesTran objUMT = DbContext.UserModulesTrans.SingleOrDefault(m => m.MHID == MHId && m.UID == UID);
                //objUMT = Mapper.Map(model,objLogin);
                //return DbContext.SaveChanges();


                Mapper.CreateMap<UserModulesItem, UserModulesTran>();
                UserModulesTran objBranch = DbContext.UserModulesTrans.SingleOrDefault(m => m.UHTID == model1.UHTID);
                objBranch = Mapper.Map(model1, objBranch);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int checkMHID(int? MHID, int? uid)
        {
            Mapper.CreateMap<UserModulesTran, UserModulesItem>();
            UserModulesTran objBranch = DbContext.UserModulesTrans.SingleOrDefault(m => m.MHID == MHID && m.UID == uid);
            UserModulesItem objBItem = Mapper.Map<UserModulesItem>(objBranch);
            if (objBItem == null)
            {
                return 0;
            }
            else
            {

                return objBItem.MHID.Value;
            }
        }
        public int checkFrmID(int? formID, int? uid)
        {
            Mapper.CreateMap<UserFormTran, UserFormTranItem>();
            UserFormTran objBranch = DbContext.UserFormTrans.SingleOrDefault(m => m.FormID == formID && m.UID == uid);
            UserFormTranItem objBItem = Mapper.Map<UserFormTranItem>(objBranch);
            if (objBItem == null)
            {
                return 0;
            }
            else
            {

                return objBItem.FormID.Value;
            }
        }
        public int InsertLeftMenu(LoginItem model, bool chk, int? formID)
        {
            try
            {
                Mapper.CreateMap<LoginItem, UserFormTran>();
                UserFormTran objLogin = Mapper.Map<UserFormTran>(model);
                objLogin.FormID = formID;
                objLogin.IsChecked = chk;
                objLogin.CreatedBy = model.UId;
                objLogin.CreatedDate = System.DateTime.Now;
                DbContext.UserFormTrans.Add(objLogin);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<MenuItem> getModules(int id)
        {
            var data = (from um in DbContext.UserModulesTrans
                        join mh in DbContext.MenuHeaderMasters on um.MHID equals mh.MHId
                        where um.UID == id
                        select new MenuItem()
                        {
                            UMItem = new UserModulesItem()
                            {
                                UHTID = um.UHTID
                            },
                            IsChecked = um.IsChecked,
                            MHName = mh.MHName,
                            MHId = um.MHID,
                            
                        }).ToList();
            return data;
            //Mapper.CreateMap<UserModulesTran, MenuItem>();
            //List<UserModulesTran> objBranch = DbContext.UserModulesTrans.Where(m => m.UID ==id).ToList();
            //List<MenuItem> objBItem = Mapper.Map<List<MenuItem>>(objBranch);
            //return objBItem;
        }
        public List<LoginItem> getUser(int cid)
        {
            Mapper.CreateMap<Login_Master, LoginItem>();
            List<Login_Master> tblMaster = DbContext.Login_Master.Where(m=>m.CompId==(cid==0?m.CompId:cid)).ToList();
            List<LoginItem> lstmasterdata = Mapper.Map<List<LoginItem>>(tblMaster);
            return lstmasterdata;
        }
        public int Update(LoginItem model)
        {
            Mapper.CreateMap<LoginItem, Login_Master>();
            Login_Master objBranch = DbContext.Login_Master.SingleOrDefault(m => m.UId == model.UId);
            objBranch = Mapper.Map(model, objBranch);
            return DbContext.SaveChanges();
        }

        public LoginItem GetById(int id)
        {
            Mapper.CreateMap<Login_Master, LoginItem>();
            Login_Master objBranch = DbContext.Login_Master.SingleOrDefault(m => m.UId == id);
            LoginItem objBItem = Mapper.Map<LoginItem>(objBranch);
            return objBItem;

            //var a = (
            //      from umt in DbContext.UserFormTrans
            //      join lm in DbContext.Login_Master on umt.UID equals lm.UId
            //       where lm.UId==id
            //       select new LoginItem()
            //       {
            //          UMTID=umt.UMTID,
            //          UId=umt.UID,
            //          FormId=umt.FormID,
            //          IsChecked=umt.IsChecked
            //       }

            //    ).SingleOrDefault();

            //return a;
        }
        public List<MenuItem> GetModules()
        {
            Mapper.CreateMap<MenuHeaderMaster, MenuItem>();
            List<MenuHeaderMaster> objMenuhader = DbContext.MenuHeaderMasters.Where(m => m.Status == "Active").ToList();
            List<MenuItem> objMenuItem = Mapper.Map<List<MenuItem>>(objMenuhader);
            return objMenuItem;
        }
        public List<LoginItem> GetFormMenu()
        {
            var a = (
                   from MenuTrans in DbContext.MenuTrans
                   join Menu in DbContext.MenuHeaderMasters on MenuTrans.MHId equals Menu.MHId
                   join Form in DbContext.FormMasters on MenuTrans.FormId equals Form.FormId

                   select new LoginItem()
                   {
                       FormName = Form.FormName,
                       FormId = Form.FormId,
                       FormUrl = Form.FormUrl,
                       MHId=Menu.MHId,
                       MenuWiseForm = new MenuWiseFormItem()
                       {
                           MHID = MenuTrans.MHId

                       }

                   }

                ).ToList();

            return a;
        }
        public List<LoginItem> GetLeftMenu(int id)
        {
            var a = (
                   from um in DbContext.UserFormTrans
                   join fm in DbContext.FormMasters on um.FormID equals fm.FormId
                   join mt in DbContext.MenuTrans on um.FormID equals mt.FormId
                   where um.UID==id
                   //select new FormItem()
                   select new LoginItem()
                   {
                      UFormDetails=new UserFormTranItem()
                      {
                          UMTID=um.UMTID,
                          MID=um.MID,
                          UID=um.UID,
                          //IsChecked=um.IsChecked,
                          FormID=um.FormID,
                      },                       
                       FormId = um.FormID,
                       IsChecked=um.IsChecked,
                       FormName = fm.FormName,
                       FormTitle = fm.FormTitle,
                       FormUrl = fm.FormUrl,
                       MHId = mt.MHId
                       
                   }).ToList();

            return a;
        }

    }
}
