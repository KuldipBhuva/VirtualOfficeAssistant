using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {          
            return View();
        }
     
        [HttpPost]
        public ActionResult Index(LoginItem model)
        {

            clsLoginMethod objMEthod = new clsLoginMethod();
            //EncDec objEnDec = new EncDec();
            //var pwd = objEnDec.Encrypt(model.Password);
            //model.Password = pwd;
            LoginItem objLoginP = objMEthod.GetDetails(model.UserName, model.Password);
            if (objLoginP != null)
            {
                Session["UserId"] = objLoginP.UId;
                Session["UserName"] = objLoginP.UserName;
                Session["Password"] = objLoginP.Password;
                Session["RoleID"] = objLoginP.RoleID;
                Session["CompID"] = objLoginP.CompID;
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Please Check Credential");
                return View();
            }
        }
        public ActionResult Logout()
        {

            Session["UserId"] = null;
            Session["UserName"] = null;
            Session["Password"] = null;
            Session["RoleID"] = null;
            Session["CompID"] = null;
            return View("Index");
        }
    }
}
