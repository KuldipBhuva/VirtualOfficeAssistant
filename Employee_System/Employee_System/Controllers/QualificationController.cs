using EMSDomain.ViewModel.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSMethods;
using EMSDomain.ViewModel;
using EMSDomain.Model;

namespace Employee_System.Controllers
{
    public class QualificationController : Controller
    {
        //
        // GET: /Qualification/

        EmpQualificationService objQualService = new EmpQualificationService();
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int QId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpQualificationService objService = new EmpQualificationService();
            List<EmpQualificationItem> lstItem = new List<EmpQualificationItem>();
            EmpQualificationItem objDoc = new EmpQualificationItem();
            objDoc = objService.GetById(QId);
            db.EmployeeQualifications.Remove(db.EmployeeQualifications.Find(QId));
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

            objQualService = new EmpQualificationService();
            List<EmpQualificationItem> listQual = new List<EmpQualificationItem>();
            listQual = objQualService.ExperienceListData(Empid);
            ViewBag.Empid = Empid;
            EmpQualificationItem objQualitem = new EmpQualificationItem();
            objQualitem.ListQualification = new List<EmpQualificationItem>();
            objQualitem.ListQualification.AddRange(listQual);

            List<clsMasterData> lstMaster = new List<clsMasterData>();
            lstMaster = objQualService.GetALLIComp();
            objQualitem.ListMasterDetails = new List<clsMasterData>();
            objQualitem.ListMasterDetails.AddRange(lstMaster);

            listQual = objQualService.GetQualificationDoc(Empid);
            objQualitem.ListQualificationDoc = new List<EmpQualificationItem>();
            objQualitem.ListQualificationDoc.AddRange(listQual);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objQualitem);
        }

        [HttpPost]
        public ActionResult Create(EmpQualificationItem model)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.Empid = Empid;
            objQualService = new EmpQualificationService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedDate = System.DateTime.Now;
            model.CreatedBy = uid;
            objQualService.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpQualificationItem model)
        {
            model.Empid = model.VQid;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.Empid = Empid;
            objQualService = new EmpQualificationService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objQualService.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { @id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int VQid)
        {
            objQualService = new EmpQualificationService();
            EmpQualificationItem objQualItem = new EmpQualificationItem();
            objQualItem = objQualService.GetById(VQid);
            //Session["Empid"] = objExpItem.EmpId;
            List<EmpQualificationItem> lstDl = new List<EmpQualificationItem>();
            objQualItem.ListQualification = new List<EmpQualificationItem>();
            objQualItem.ListQualification.AddRange(lstDl);

            List<clsMasterData> lstMaster = new List<clsMasterData>();
            lstMaster = objQualService.GetALLIComp();
            objQualItem.ListMasterDetails = new List<clsMasterData>();
            objQualItem.ListMasterDetails.AddRange(lstMaster);
            ViewBag.Menuid = Request.QueryString["menuId"];
            ViewBag.VQid = VQid;
            return View(objQualItem);
        }
        public ActionResult View(int id, int VQid)
        {
            objQualService = new EmpQualificationService();
            EmpQualificationItem objQualItem = new EmpQualificationItem();
            objQualItem = objQualService.GetById(VQid);
            //Session["Empid"] = objExpItem.EmpId;
            List<EmpQualificationItem> lstDl = new List<EmpQualificationItem>();
            objQualItem.ListQualification = new List<EmpQualificationItem>();
            objQualItem.ListQualification.AddRange(lstDl);

            List<clsMasterData> lstMaster = new List<clsMasterData>();
            lstMaster = objQualService.GetALLIComp();
            objQualItem.ListMasterDetails = new List<clsMasterData>();
            objQualItem.ListMasterDetails.AddRange(lstMaster);
            ViewBag.Menuid = Request.QueryString["menuId"];
            ViewBag.VQid = VQid;
            return View(objQualItem);
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
