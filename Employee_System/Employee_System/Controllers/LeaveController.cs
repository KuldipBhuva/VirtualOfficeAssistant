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
    public class LeaveController : Controller
    {
        //
        // GET: /Leave/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int LId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpLeaveService objService = new EmpLeaveService();
            List<EmpLeaveItem> lstItem = new List<EmpLeaveItem>();
            EmpLeaveItem objDoc = new EmpLeaveItem();
            objDoc = objService.GetById(LId);
            db.EmployeeLeaves.Remove(db.EmployeeLeaves.Find(LId));
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

            //if (Url.RequestContext.RouteData.Values["id"].ToString()!=null)
            //{
            
            //}
            List<EmpLeaveItem> lstleave = new List<EmpLeaveItem>();
            EmpLeaveService objLeave = new EmpLeaveService();
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            lstleave = objLeave.LeaveListData(Empid);
            ViewBag.Empid = Empid;
            EmpLeaveItem objEmpItem = new EmpLeaveItem();
            objEmpItem.ListLeave = new List<EmpLeaveItem>();
            objEmpItem.ListLeave.AddRange(lstleave);

            #region Bind DropDown Leave
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objLeave.GetALLLeaveTypeId();
            objEmpItem.ListMasterTable = new List<clsMasterData>();
            objEmpItem.ListMasterTable.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objEmpItem);
        }

        [HttpPost]
        public ActionResult Create(EmpLeaveItem model)
        {
            model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpID = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            EmpLeaveService objEmpInc = new EmpLeaveService();
            objEmpInc.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(EmpLeaveItem model)
        {
            model.LID = model.LeaveId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpID = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            EmpLeaveService objPass = new EmpLeaveService();
            objPass.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int LeaveId)
        {
            EmpLeaveService objLeaveService = new EmpLeaveService();
            EmpLeaveItem objPassItem = new EmpLeaveItem();
            objPassItem = objLeaveService.GetById(LeaveId);
            //Session["Empid"] = objPassItem.EmpID;
            List<EmpLeaveItem> lstLeave = new List<EmpLeaveItem>();
            objPassItem.ListLeave = new List<EmpLeaveItem>();
            objPassItem.ListLeave.AddRange(lstLeave);
            #region Bind DropDown Leave
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objLeaveService.GetALLLeaveTypeId();
            objPassItem.ListMasterTable = new List<clsMasterData>();
            objPassItem.ListMasterTable.AddRange(lstMasters);
            ViewBag.LId = LeaveId;
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objPassItem);
        }
        public ActionResult View(int id, int LeaveId)
        {
            EmpLeaveService objLeaveService = new EmpLeaveService();
            EmpLeaveItem objPassItem = new EmpLeaveItem();
            objPassItem = objLeaveService.GetById(LeaveId);
            //Session["Empid"] = objPassItem.EmpID;
            List<EmpLeaveItem> lstLeave = new List<EmpLeaveItem>();
            objPassItem.ListLeave = new List<EmpLeaveItem>();
            objPassItem.ListLeave.AddRange(lstLeave);
            #region Bind DropDown Leave
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objLeaveService.GetALLLeaveTypeId();
            objPassItem.ListMasterTable = new List<clsMasterData>();
            objPassItem.ListMasterTable.AddRange(lstMasters);
            ViewBag.LId = LeaveId;
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objPassItem);
        }
    }
}
