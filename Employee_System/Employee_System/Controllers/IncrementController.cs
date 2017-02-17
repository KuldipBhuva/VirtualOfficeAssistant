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
    public class IncrementController : Controller
    {
        //
        // GET: /EmpIncrement/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int IId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpIncrementService objService = new EmpIncrementService();
            List<EmpIncrementItem> lstItem = new List<EmpIncrementItem>();
            EmpIncrementItem objDoc = new EmpIncrementItem();
            objDoc = objService.GetById(IId);
            db.EmployeeIncrements.Remove(db.EmployeeIncrements.Find(IId));
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

            EmpIncrementService objInc = new EmpIncrementService();
            List<EmpIncrementItem> listInc = new List<EmpIncrementItem>();
            listInc = objInc.IncrementListData(Empid);
            ViewBag.Empid = Empid;
            EmpIncrementItem objIncitem = new EmpIncrementItem();
            objIncitem.ListIncrement = new List<EmpIncrementItem>();
            objIncitem.ListIncrement.AddRange(listInc);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objIncitem);
        }

        [HttpPost]
        public ActionResult Create(EmpIncrementItem model)
        {
            //model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            EmpIncrementService objEmpInc = new EmpIncrementService();
            objEmpInc.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpIncrementItem model)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            EmpIncrementService objPass = new EmpIncrementService();
            objPass.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int IncId)
        {
            EmpIncrementService objPassService = new EmpIncrementService();
            EmpIncrementItem objPassItem = new EmpIncrementItem();
            objPassItem = objPassService.GetById(IncId);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpIncrementItem> lstPassport = new List<EmpIncrementItem>();
            objPassItem.ListIncrement = new List<EmpIncrementItem>();
            objPassItem.ListIncrement.AddRange(lstPassport);
            ViewBag.IncId = IncId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPassItem);
        }
        public ActionResult View(int id, int IncId)
        {
            EmpIncrementService objPassService = new EmpIncrementService();
            EmpIncrementItem objPassItem = new EmpIncrementItem();
            objPassItem = objPassService.GetById(IncId);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpIncrementItem> lstPassport = new List<EmpIncrementItem>();
            objPassItem.ListIncrement = new List<EmpIncrementItem>();
            objPassItem.ListIncrement.AddRange(lstPassport);
            ViewBag.IncId = IncId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPassItem);
        }
    }
}
