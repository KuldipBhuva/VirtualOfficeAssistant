using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/            
        public ActionResult Create()
        {
            CompanyService objService = new CompanyService();
            List<CompanyItem> listCompany = new List<CompanyItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            listCompany = objService.GetbyCID(cid);
            CompanyItem objCompitem = new CompanyItem();
            objCompitem.ListCompany = new List<CompanyItem>();
            objCompitem.ListCompany.AddRange(listCompany);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objService.getCountry();
            objCompitem.ConList = new List<clsMasterData>();
            objCompitem.ConList.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objCompitem);
        }


        [HttpPost]
        public ActionResult Create(CompanyItem model)
        {
            CompanyService objCompany = new CompanyService();
            HttpPostedFileBase logo = Request.Files["file"];
            HttpPostedFileBase sign = Request.Files["SignFile"];
            if (logo != null)
            {
                var logoPhoto = Path.GetFileName(logo.FileName);

                if (logoPhoto != "")
                {
                    var path = Path.Combine(Server.MapPath("/PhotoUpload/Logo"), logoPhoto);
                    logo.SaveAs(path);
                    model.Logo = logoPhoto;
                }
            }
            if (sign != null)
            {
                var signFile = Path.GetFileName(sign.FileName);
                if (signFile != "")
                {
                    var path = Path.Combine(Server.MapPath("/PhotoUpload/Sign"), signFile);
                    sign.SaveAs(path);
                    model.Sign = signFile;
                }
            }
            objCompany.Insert(model);
            
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(CompanyItem model)
        {
            CompanyService objComp = new CompanyService();

            HttpPostedFileBase logo = Request.Files["file"];
            HttpPostedFileBase sign = Request.Files["SignFile"];
            
            if (logo != null)
            {
                var logoPhoto = Path.GetFileName(logo.FileName);
                if (logoPhoto != "")
                {

                    var path = Path.Combine(Server.MapPath("~/PhotoUpload/Logo"), logoPhoto);
                    logo.SaveAs(path);
                    model.Logo = logoPhoto;
                }
            }
            if (sign != null)
            {
                var signFile = Path.GetFileName(sign.FileName);
                if (signFile != "")
                {
                    var path = Path.Combine(Server.MapPath("~/PhotoUpload/Sign"), signFile);
                    sign.SaveAs(path);
                    model.Sign = signFile;
                }
            }
            objComp.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            CompanyService objComp = new CompanyService();
            CompanyItem objCItem = new CompanyItem();
            objCItem = objComp.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<CompanyItem> lstComp = new List<CompanyItem>();
            objCItem.ListCompany = new List<CompanyItem>();
            objCItem.ListCompany.AddRange(lstComp);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objComp.getCountry();
            objCItem.ConList = new List<clsMasterData>();
            objCItem.ConList.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objCItem);
        }
        public ActionResult View(int id)
        {
            CompanyService objComp = new CompanyService();
            CompanyItem objCItem = new CompanyItem();
            objCItem = objComp.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<CompanyItem> lstComp = new List<CompanyItem>();
            objCItem.ListCompany = new List<CompanyItem>();
            objCItem.ListCompany.AddRange(lstComp);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objComp.getCountry();
            objCItem.ConList = new List<clsMasterData>();
            objCItem.ConList.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objCItem);
        }
    }
}
