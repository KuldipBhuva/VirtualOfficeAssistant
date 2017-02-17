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
    public class AbscondingController : Controller
    {
        //
        // GET: /EmpAbsconding/
        EmployeeEntities db = new EmployeeEntities();        
        public ActionResult delete(int id, int AId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpAbscondingService objService = new EmpAbscondingService();
            List<EmpAbscondingItem> lstItem = new List<EmpAbscondingItem>();
            EmpAbscondingItem objDoc = new EmpAbscondingItem();
            objDoc = objService.GetById(AId);
            db.EmployeeAbscondings.Remove(db.EmployeeAbscondings.Find(AId));
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
            EmpAbscondingService objAbs = new EmpAbscondingService();
            List<EmpAbscondingItem> listAbs = new List<EmpAbscondingItem>();
            listAbs = objAbs.AbsListData(Empid);
            ViewBag.EmpId = Empid;   // 1st stmt Edit
            EmpAbscondingItem objAbsitem = new EmpAbscondingItem();
            objAbsitem.ListAbsconding = new List<EmpAbscondingItem>();
            objAbsitem.ListAbsconding.AddRange(listAbs);

            listAbs = objAbs.GetAbscondingDoc(Empid);
            objAbsitem.ListAbscondingDoc = new List<EmpAbscondingItem>();
            objAbsitem.ListAbscondingDoc.AddRange(listAbs);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objAbsitem);
        }

        [HttpPost]
        public ActionResult Create(EmpAbscondingItem model)
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
            EmpAbscondingService objEmpAbs = new EmpAbscondingService();
            objEmpAbs.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpAbscondingItem model)
        {
            EmpAbscondingService objAbs = new EmpAbscondingService();
            model.AbscId = model.AId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objAbs.Update(model);
            ViewBag.EmpId = Empid;  // 2nd stmt Edit
            return RedirectToAction("Create", new { @id = Empid , @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int Absid)
        {
            EmpAbscondingService objAbsService = new EmpAbscondingService();
            EmpAbscondingItem objAbsItem = new EmpAbscondingItem();
            objAbsItem = objAbsService.GetById(Absid);
            //Session["Empid"] = objAbsItem.EmpId;
            List<EmpAbscondingItem> lstAbs = new List<EmpAbscondingItem>();
            objAbsItem.ListAbsconding = new List<EmpAbscondingItem>();
            objAbsItem.ListAbsconding.AddRange(lstAbs);
            ViewBag.Absid = Absid;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objAbsItem);
        }
        public ActionResult View(int id, int Absid)
        {
            EmpAbscondingService objAbsService = new EmpAbscondingService();
            EmpAbscondingItem objAbsItem = new EmpAbscondingItem();
            objAbsItem = objAbsService.GetById(Absid);
            //Session["Empid"] = objAbsItem.EmpId;
            List<EmpAbscondingItem> lstAbs = new List<EmpAbscondingItem>();
            objAbsItem.ListAbsconding = new List<EmpAbscondingItem>();
            objAbsItem.ListAbsconding.AddRange(lstAbs);
            ViewBag.Absid = Absid;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objAbsItem);
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
