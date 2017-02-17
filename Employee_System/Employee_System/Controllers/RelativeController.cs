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
    public class RelativeController : Controller
    {
        //
        // GET: /EmpRelative/
        EmpRelativeService objRel = new EmpRelativeService();
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int cid, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpRelativeService objService = new EmpRelativeService();
            List<EmpRelativeItem> lstItem = new List<EmpRelativeItem>();
            EmpRelativeItem objDoc = new EmpRelativeItem();
            objDoc = objService.GetById(cid);
            db.EmployeeRelatives.Remove(db.EmployeeRelatives.Find(cid));
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
            objRel = new EmpRelativeService();
            List<EmpRelativeItem> listRel = new List<EmpRelativeItem>();
            listRel = objRel.RelativeListData(Empid);
            ViewBag.Empid = Empid;
            EmpRelativeItem objRelitem = new EmpRelativeItem();
            objRelitem.ListRelative = new List<EmpRelativeItem>();
            objRelitem.ListRelative.AddRange(listRel);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objRelitem);
        }

        [HttpPost]
        public ActionResult Create(EmpRelativeItem model)
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
            objRel = new EmpRelativeService();
            objRel.Insert(model);
            return RedirectToAction("Create", new {@menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpRelativeItem model)
        {
            model.Id = model.RId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            EmpRelativeService objRel = new EmpRelativeService();
            objRel.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int RId)
        {
            EmpRelativeService objRel = new EmpRelativeService();
            EmpRelativeItem objRelItem = new EmpRelativeItem();
            objRelItem = objRel.GetById(RId);
            //Session["Empid"] = objRelItem.EmpId;
            List<EmpRelativeItem> lstRel = new List<EmpRelativeItem>();
            objRelItem.ListRelative = new List<EmpRelativeItem>();
            objRelItem.ListRelative.AddRange(lstRel);
            ViewBag.RId = RId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objRelItem);
        }
        public ActionResult View(int id, int RId)
        {
            EmpRelativeService objRel = new EmpRelativeService();
            EmpRelativeItem objRelItem = new EmpRelativeItem();
            objRelItem = objRel.GetById(RId);
            //Session["Empid"] = objRelItem.EmpId;
            List<EmpRelativeItem> lstRel = new List<EmpRelativeItem>();
            objRelItem.ListRelative = new List<EmpRelativeItem>();
            objRelItem.ListRelative.AddRange(lstRel);
            ViewBag.RId = RId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objRelItem);
        }

    }
}
