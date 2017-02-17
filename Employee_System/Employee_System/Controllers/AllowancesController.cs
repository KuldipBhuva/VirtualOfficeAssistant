using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Employee;
using EMSMethods;
using EMSDomain.ViewModel;
using EMSDomain.Model;

namespace Employee_System.Controllers
{
    public class AllowancesController : Controller
    {
        //
        // GET: /Allowances/
        EmpAllowancesService objAllowService = new EmpAllowancesService();
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int AId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpAllowancesService objService = new EmpAllowancesService();
            List<EmpAllowncesItem> lstItem = new List<EmpAllowncesItem>();
            EmpAllowncesItem objDoc = new EmpAllowncesItem();
            objDoc = objService.GetById(AId);
            db.EmployeeAllowances.Remove(db.EmployeeAllowances.Find(AId));
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
            EmpAllowncesItem objAllowitem = new EmpAllowncesItem();

            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            ViewBag.Empid = Empid;
            objAllowService = new EmpAllowancesService();
            List<EmpAllowncesItem> listAllowance = new List<EmpAllowncesItem>();

            #region ForBind Grid data
            listAllowance = objAllowService.AllowancesListData(Empid);
            objAllowitem = new EmpAllowncesItem();
            objAllowitem.ListAllownces = new List<EmpAllowncesItem>();
            objAllowitem.ListAllownces.AddRange(listAllowance);
            #endregion

            #region Bind DropDown Allowances
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objAllowService.GetALLAllowanceId();
            objAllowitem.ListMasterTable = new List<clsMasterData>();
            objAllowitem.ListMasterTable.AddRange(lstMasters);

            #endregion

            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objAllowitem);
        }

        [HttpPost]
        public ActionResult Create(EmpAllowncesItem model)
        {
            if (ModelState.IsValid)
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
                objAllowService = new EmpAllowancesService();
                objAllowService.Insert(model);

            }
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpAllowncesItem model)
        {

            model.AId = model.AlwId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            model.EmpId = Empid;
            objAllowService = new EmpAllowancesService();
            objAllowService.Update(model);
            ViewBag.Empid = Empid;

            return RedirectToAction("Create", new { @id = Empid ,  @menuId = model.Viewbagidformenu });

        }

        public ActionResult Edit(int id, int AlId)
        {
            objAllowService = new EmpAllowancesService();
            EmpAllowncesItem objAllowItem = new EmpAllowncesItem();
            objAllowItem = objAllowService.GetById(AlId);
            //Session["Empid"] = objAllowItem.EmpId;
            List<EmpAllowncesItem> lstDl = new List<EmpAllowncesItem>();
            objAllowItem.ListAllownces = new List<EmpAllowncesItem>();
            objAllowItem.ListAllownces.AddRange(lstDl);


            #region Bind DropDown Allowances
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objAllowService.GetALLAllowanceId();
            objAllowItem.ListMasterTable = new List<clsMasterData>();
            objAllowItem.ListMasterTable.AddRange(lstMasters);
            #endregion

            ViewBag.AlwId = AlId;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objAllowItem);
        }
        public ActionResult View(int id, int AlId)
        {
            objAllowService = new EmpAllowancesService();
            EmpAllowncesItem objAllowItem = new EmpAllowncesItem();
            objAllowItem = objAllowService.GetById(AlId);
            //Session["Empid"] = objAllowItem.EmpId;
            List<EmpAllowncesItem> lstDl = new List<EmpAllowncesItem>();
            objAllowItem.ListAllownces = new List<EmpAllowncesItem>();
            objAllowItem.ListAllownces.AddRange(lstDl);


            #region Bind DropDown Allowances
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objAllowService.GetALLAllowanceId();
            objAllowItem.ListMasterTable = new List<clsMasterData>();
            objAllowItem.ListMasterTable.AddRange(lstMasters);
            #endregion

            ViewBag.AlwId = AlId;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objAllowItem);
        }
    }
}
