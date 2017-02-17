using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Menu;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        EmployeeEntities DbContext = new EmployeeEntities();
        public ActionResult Create()
        {
            List<LoginItem> lstLogin = new List<LoginItem>();
            UserService objUser = new UserService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstLogin = objUser.getUser(cid);
            LoginItem objUserItem = new LoginItem();
            objUserItem.ListUser = new List<LoginItem>();
            objUserItem.ListUser.AddRange(lstLogin);
            ViewBag.Menuid = Request.QueryString["menuId"];
            //#region Bind DropDown comp
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objUserItem.ListCompany = new List<CompanyItem>();
            objUserItem.ListCompany.AddRange(objCompany);

            //#endregion
            return View(objUserItem);
        }
        [HttpPost]
        public ActionResult Create(LoginItem model)
        {
            UserService objUser = new UserService();
            //EncDec objEnDec = new EncDec();
            //var pwd=objEnDec.Encrypt(model.Password);
            //model.Password = pwd;
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
            {
                model.CompID = cid;
            }

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedDate = System.DateTime.Now;
            model.CreatedBy = uid;
            objUser.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(LoginItem model)
        {
            UserService objUser = new UserService();
            LoginItem objItem = new LoginItem();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objUser.Update(model);
            //int UID = Convert.ToInt32(Session["UserId"].ToString());
            int UID = model.UId;
            foreach (var i in model.ListModules)
            {
                int? MHId = i.MHId;
                bool chk = i.IsChecked;
                
                
                    int id = objUser.checkMHID(MHId,UID);
                    int uhid = model.UHTID;
                    if (id == null || id == 0)
                    {
                        //if (chk == true)
                        //{
                            objUser.InsertModules(model, MHId, chk, UID);
                        //}       
                    }
                    else
                    {
                        //objUser.updateModules(model, MHId, chk, UID);
                        UserModulesTran userData = DbContext.UserModulesTrans.Where(u => u.UID == UID && u.MHID==MHId).SingleOrDefault();
                        //userData.UMTID = model.UMTID;
                        userData.MHID = MHId;
                        userData.IsChecked = chk;
                        userData.UID = UID;
                        userData.UpdatedBy = UID;
                        userData.UpdatedDate = System.DateTime.Now;
                        DbContext.SaveChanges();
                    }                
            }
            foreach (var i in model.ListForm)
            {
                
                int umtid = model.UMTID;
                bool chk = i.IsChecked;
                int? formID = i.FormId;
                int? id = objUser.checkFrmID(formID, UID);
                int? uhid = model.UHTID;
                int? mhid = i.MHId;
                
                if (id == null || id == 0)
                {
                    //if (chk == true)
                    //{
                    objUser.InsertLeftMenu(model,chk,formID);
                    //}       
                }
                else
                {
                    UserFormTran userData = DbContext.UserFormTrans.Where(u => u.UID == UID && u.FormID==formID).SingleOrDefault();
                    //userData.UMTID = model.UMTID;
                    userData.FormID = formID;
                    userData.IsChecked = chk;
                    DbContext.SaveChanges();

                    userData.UpdatetdDate = System.DateTime.Now;
                    userData.UpdatedBy = UID;
                    //objUser.updateModules(model, MHId, chk, UID);
                    //objUser.updateModules(model1);
                }              
            }
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }    
        public ActionResult Edit(int id)
        {
            UserService objUser = new UserService();
            LoginItem objBItem = new LoginItem();
            
            
            
            objBItem = objUser.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<LoginItem> lstUser = new List<LoginItem>();
            objBItem.ListUser = new List<LoginItem>();
            objBItem.ListUser.AddRange(lstUser);
            //EncDec objEnDec = new EncDec();
            //var pwd = objEnDec.Decrypt(objBItem.Password);
            //objBItem.Password = pwd;

            List<MenuItem> objlstItem = new List<MenuItem>();
            objlstItem = objUser.getModules(id);
            objBItem.ListModules = new List<MenuItem>();
            objBItem.ListModules.AddRange(objlstItem);

            //#region Bind DropDown comp
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objBItem.ListCompany = new List<CompanyItem>();
            objBItem.ListCompany.AddRange(objCompany);

            //#endregion

            if (objlstItem.Count == 0)
            {
                MenuItem objMenuItem = new MenuItem();
                //MenuService objMenuService = new MenuService();
                List<MenuItem> objListMenu = new List<MenuItem>();
                objListMenu = objUser.GetModules();
                objBItem.ListModules = new List<MenuItem>();
                objBItem.ListModules.AddRange(objListMenu);
            }

            List<LoginItem> lstUForm = new List<LoginItem>();
            
            lstUForm = objUser.GetLeftMenu(id);
            objBItem.ListForm = new List<LoginItem>();
            objBItem.ListForm.AddRange(lstUForm);
            //return PartialView();
            if (lstUForm.Count == 0)
            {
                List<LoginItem> lstForm = new List<LoginItem>();
                lstForm = objUser.GetFormMenu();
                objBItem.ListForm = new List<LoginItem>();
                objBItem.ListForm.AddRange(lstForm);
            }
            //int UID=Convert.ToInt32(Request.QueryString["id"]);
            

            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objBItem);
        }
    }
}
