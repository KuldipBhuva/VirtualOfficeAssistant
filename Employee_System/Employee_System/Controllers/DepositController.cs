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
    public class DepositController : Controller
    {
        //
        // GET: /EmpDeposit/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int DId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpDepositeService objService = new EmpDepositeService();
            List<EmpDepositeItem> lstItem = new List<EmpDepositeItem>();
            EmpDepositeItem objDoc = new EmpDepositeItem();
            objDoc = objService.GetById(DId);
            db.EmployeeDeposites.Remove(db.EmployeeDeposites.Find(DId));
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

            EmpDepositeService objDeposit = new EmpDepositeService();
            List<EmpDepositeItem> listDep = new List<EmpDepositeItem>();
            listDep = objDeposit.DepositListData(Empid);
            ViewBag.Empid = Empid;
            EmpDepositeItem objDepitem = new EmpDepositeItem();
            objDepitem.ListDeposit = new List<EmpDepositeItem>();
            objDepitem.ListDeposit.AddRange(listDep);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objDepitem);
        }

        [HttpPost]
        public ActionResult Create(EmpDepositeItem model)
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
            EmpDepositeService objDeposit = new EmpDepositeService();
            objDeposit.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpDepositeItem model)
        {
            model.DepId = model.DId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            EmpDepositeService objPass = new EmpDepositeService();
            objPass.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { @id = Empid , @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int DepId)
        {
            EmpDepositeService objDepositeService = new EmpDepositeService();
            EmpDepositeItem objDepositeItem = new EmpDepositeItem();
            objDepositeItem = objDepositeService.GetById(DepId);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpDepositeItem> lstPassport = new List<EmpDepositeItem>();
            objDepositeItem.ListDeposit = new List<EmpDepositeItem>();
            objDepositeItem.ListDeposit.AddRange(lstPassport);
            ViewBag.DeptId = DepId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objDepositeItem);
        }
        public ActionResult View(int id, int DepId)
        {
            EmpDepositeService objDepositeService = new EmpDepositeService();
            EmpDepositeItem objDepositeItem = new EmpDepositeItem();
            objDepositeItem = objDepositeService.GetById(DepId);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpDepositeItem> lstPassport = new List<EmpDepositeItem>();
            objDepositeItem.ListDeposit = new List<EmpDepositeItem>();
            objDepositeItem.ListDeposit.AddRange(lstPassport);
            ViewBag.DeptId = DepId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objDepositeItem);
        }
    }
}
