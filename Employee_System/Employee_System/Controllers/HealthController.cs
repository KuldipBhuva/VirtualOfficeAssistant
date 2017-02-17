using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel.Employee;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class HealthController : Controller
    {
        //
        // GET: /Health/
        EmpHealthService objHealthService = new EmpHealthService();
        EmployeeEntities db = new EmployeeEntities();        
        public ActionResult delete(int id, int HId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpHealthService objService = new EmpHealthService();
            List<EmpHealthItem> lstItem = new List<EmpHealthItem>();
            EmpHealthItem objDoc = new EmpHealthItem();
            objDoc = objService.GetById(HId);
            db.EmployeeHealthCards.Remove(db.EmployeeHealthCards.Find(HId));
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

            objHealthService = new EmpHealthService();
            List<EmpHealthItem> listHealth = new List<EmpHealthItem>();
            listHealth = objHealthService.HealthcardListData(Empid);
            ViewBag.Empid = Empid;
            EmpHealthItem objHealthitem = new EmpHealthItem();
            objHealthitem.ListHealth = new List<EmpHealthItem>();
            objHealthitem.ListHealth.AddRange(listHealth);

            //for doc
            listHealth = objHealthService.GetHealthDoc(Empid);
            objHealthitem.ListHealthDoc = new List<EmpHealthItem>();
            objHealthitem.ListHealthDoc.AddRange(listHealth);

            listHealth = objHealthService.GetLabourDoc(Empid);
            objHealthitem.ListLabourDoc = new List<EmpHealthItem>();
            objHealthitem.ListLabourDoc.AddRange(listHealth);

            listHealth = objHealthService.GetEIDDoc(Empid);
            objHealthitem.ListEIDDoc = new List<EmpHealthItem>();
            objHealthitem.ListEIDDoc.AddRange(listHealth);

            List<EmpDLicenceItem> listLicence = new List<EmpDLicenceItem>();            
            EmpDriveLicenseService objDLicence = new EmpDriveLicenseService();              
            //objHealthitem.ListLicence = new List<EmpDLicenceItem>();
            //objHealthitem.ListLicence.AddRange(listLicence);
            listLicence = objDLicence.GetLicenceDoc(Empid);
            objHealthitem.ListLicenceDoc = new List<EmpDLicenceItem>();
            objHealthitem.ListLicenceDoc.AddRange(listLicence);

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objHealthitem);
        }

        [HttpPost]
        public ActionResult Create(EmpHealthItem model)
        {
            model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            model.EmpId = Empid;
            objHealthService = new EmpHealthService();
            objHealthService.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(EmpHealthItem model)
        {
            model.HId = model.HCId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            model.EmpId = Empid;
            objHealthService = new EmpHealthService();
            objHealthService.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { @id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int HCId)
        {
            objHealthService = new EmpHealthService();
            EmpHealthItem objHealthItem = new EmpHealthItem();
            objHealthItem = objHealthService.GetById(HCId);
            //Session["Empid"] = objHealthItem.EmpId;
            List<EmpHealthItem> lstHealth = new List<EmpHealthItem>();
            objHealthItem.ListHealth = new List<EmpHealthItem>();
            objHealthItem.ListHealth.AddRange(lstHealth);
            ViewBag.HCId = HCId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objHealthItem);
        }
        public ActionResult View(int id, int HCId)
        {
            objHealthService = new EmpHealthService();
            EmpHealthItem objHealthItem = new EmpHealthItem();
            objHealthItem = objHealthService.GetById(HCId);
            //Session["Empid"] = objHealthItem.EmpId;
            List<EmpHealthItem> lstHealth = new List<EmpHealthItem>();
            objHealthItem.ListHealth = new List<EmpHealthItem>();
            objHealthItem.ListHealth.AddRange(lstHealth);
            ViewBag.HCId = HCId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objHealthItem);
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
