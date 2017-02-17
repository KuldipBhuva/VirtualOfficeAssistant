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
    public class ExperienceController : Controller
    {
        //
        // GET: /EmpExperience/
        EmpExperienceService objExpService = new EmpExperienceService();
        EmployeeEntities _db = new EmployeeEntities();
        public ActionResult delete(int id, int ExpId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpExperienceService objService = new EmpExperienceService();
            List<EmpExperienceItem> lstItem = new List<EmpExperienceItem>();
            EmpExperienceItem objDoc = new EmpExperienceItem();
            objDoc = objService.GetById(ExpId);
            _db.EmployeeExperiences.Remove(_db.EmployeeExperiences.Find(ExpId));
            _db.SaveChanges();

            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        
        public ActionResult View(int id, int ExpId)
        {
            EmpExperienceItem objExpItem = new EmpExperienceItem();


            objExpService = new EmpExperienceService();

            objExpItem = objExpService.GetById(ExpId);
            //Session["Empid"] = objExpItem.EmpId;
            List<EmpExperienceItem> lstDl = new List<EmpExperienceItem>();
            objExpItem.ListExperience = new List<EmpExperienceItem>();
            objExpItem.ListExperience.AddRange(lstDl);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objExpService.getCountry();
            objExpItem.ListCountry = new List<clsMasterData>();
            objExpItem.ListCountry.AddRange(lstMasters);

            #endregion
            ViewBag.VExpId = ExpId;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objExpItem);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            objExpService = new EmpExperienceService();
            List<EmpExperienceItem> listExp = new List<EmpExperienceItem>();
            listExp = objExpService.ExperienceListData(Empid);
            ViewBag.Empid = Empid;
            EmpExperienceItem objExpitem = new EmpExperienceItem();
            objExpitem.ListExperience = new List<EmpExperienceItem>();
            objExpitem.ListExperience.AddRange(listExp);

            listExp = objExpService.GetExperienceDoc(Empid);
            objExpitem.ListExperienceDoc = new List<EmpExperienceItem>();
            objExpitem.ListExperienceDoc.AddRange(listExp);

            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objExpService.getCountry();
            objExpitem.ListCountry = new List<clsMasterData>();
            objExpitem.ListCountry.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objExpitem);
        }

        [HttpPost]
        public ActionResult Create(EmpExperienceItem model)
        {
            // model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objExpService = new EmpExperienceService();
            objExpService.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpExperienceItem model)
        {
            model.EmpId = model.VEmpId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objExpService = new EmpExperienceService();
            objExpService.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { @id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int ExpId)
        {
            EmpExperienceItem objExpItem = new EmpExperienceItem();


            objExpService = new EmpExperienceService();

            objExpItem = objExpService.GetById(ExpId);
            //Session["Empid"] = objExpItem.EmpId;
            List<EmpExperienceItem> lstDl = new List<EmpExperienceItem>();
            objExpItem.ListExperience = new List<EmpExperienceItem>();
            objExpItem.ListExperience.AddRange(lstDl);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objExpService.getCountry();
            objExpItem.ListCountry = new List<clsMasterData>();
            objExpItem.ListCountry.AddRange(lstMasters);

            #endregion
            ViewBag.VExpId = ExpId;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objExpItem);
        }
        public ActionResult deleteDoc(int? id, int DID, int menuid)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            objExpService = new EmpExperienceService();

            EMSDomain.ViewModel.DocumentItem objDoc = new EMSDomain.ViewModel.DocumentItem();
            objDoc = objExpService.byID(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            _db.EmployeeDocuments.Remove(_db.EmployeeDocuments.Find(DID));
            _db.SaveChanges();

            ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = Empid, @menuId = Request.QueryString["menuId"] });
        }
    }
}
