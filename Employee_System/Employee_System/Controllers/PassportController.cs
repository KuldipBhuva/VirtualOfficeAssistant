using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class PassportController : Controller
    {
        //
        // GET: /EmpPassport/
        EmpPassportService objpassport = new EmpPassportService();
        EmployeeEntities db = new EmployeeEntities();        
        public ActionResult delete(int id, int PId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpPassportService objService = new EmpPassportService();
            List<EmpPassportItem> lstItem = new List<EmpPassportItem>();
            EmpPassportItem objDoc = new EmpPassportItem();
            objDoc = objService.GetById(PId);
            db.EmployeePassports.Remove(db.EmployeePassports.Find(PId));
            db.SaveChanges();

            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            objpassport = new EmpPassportService();
            List<EmpPassportItem> listRel = new List<EmpPassportItem>();
            listRel = objpassport.PassportListData(Empid);
            ViewBag.Empid = Empid;
            EmpPassportItem objPasitem = new EmpPassportItem();
            objPasitem.ListPassport = new List<EmpPassportItem>();
            objPasitem.ListPassport.AddRange(listRel);

            listRel = objpassport.GetPassportDoc(Empid);
            objPasitem.ListPassportDoc = new List<EmpPassportItem>();
            objPasitem.ListPassportDoc.AddRange(listRel);
            #region Bind DropDown Nationality
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objpassport.getNationality();
            objPasitem.ListNationality = new List<clsMasterData>();
            objPasitem.ListNationality.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Country
            //List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objpassport.getCountry();
            objPasitem.ListCountry= new List<clsMasterData>();
            objPasitem.ListCountry.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPasitem);
        }

        [HttpPost]
        public ActionResult Create(EmpPassportItem model)
        {
            model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objpassport = new EmpPassportService();

            objpassport.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpPassportItem model)
        {
            model.Id = model.PasId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;

            EmpPassportService objPass = new EmpPassportService();
            objPass.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int PasId)
        {
            EmpPassportService objPassService = new EmpPassportService();
            EmpPassportItem objPassItem = new EmpPassportItem();
            objPassItem = objPassService.GetById(PasId);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpPassportItem> lstPassport = new List<EmpPassportItem>();
            objPassItem.ListPassport = new List<EmpPassportItem>();
            objPassItem.ListPassport.AddRange(lstPassport);
            #region Bind DropDown Nationality
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objpassport.getNationality();
            objPassItem.ListNationality = new List<clsMasterData>();
            objPassItem.ListNationality.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Country
            //List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objpassport.getCountry();
            objPassItem.ListCountry = new List<clsMasterData>();
            objPassItem.ListCountry.AddRange(lstMasters);

            #endregion
            ViewBag.PId = PasId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPassItem);
        }
        public ActionResult View(int id, int PasId)
        {
            EmpPassportService objPassService = new EmpPassportService();
            EmpPassportItem objPassItem = new EmpPassportItem();
            objPassItem = objPassService.GetById(PasId);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpPassportItem> lstPassport = new List<EmpPassportItem>();
            objPassItem.ListPassport = new List<EmpPassportItem>();
            objPassItem.ListPassport.AddRange(lstPassport);
            #region Bind DropDown Nationality
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objpassport.getNationality();
            objPassItem.ListNationality = new List<clsMasterData>();
            objPassItem.ListNationality.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Country
            //List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objpassport.getCountry();
            objPassItem.ListCountry = new List<clsMasterData>();
            objPassItem.ListCountry.AddRange(lstMasters);

            #endregion
            ViewBag.PId = PasId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPassItem);
        }
        public ActionResult deleteDoc(int? id, int DID, int menuid)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            
            EmpExperienceService objExpService = new EmpExperienceService();
            EMSDomain.ViewModel.DocumentItem objDoc = new EMSDomain.ViewModel.DocumentItem();
            objDoc = objExpService.byID(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.EmployeeDocuments.Remove(db.EmployeeDocuments.Find(DID));
            db.SaveChanges();
            ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = Empid, @menuId = Request.QueryString["menuId"] });
        }
    }
}
