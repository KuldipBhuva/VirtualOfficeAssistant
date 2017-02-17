using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class VehicleReparingController : Controller
    {
        //
        // GET: /VehicleReparing/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                VehicleRepairingService objService = new VehicleRepairingService();
                VehicleRepairingItem objDoc = new VehicleRepairingItem();
                objDoc = objService.GetById(id);
                db.VehicleRepairings.Remove(db.VehicleRepairings.Find(id));
                db.SaveChanges();
                //ViewBag.Empid = Empid;
                ViewBag.Menuid = Request.QueryString["menuId"];
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMsg = "First Delete This Tenancy No's All Documents. ";                
                return View("Error");
            }
            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Create()
        {
            int Empid = 0;
            //if (Url.RequestContext.RouteData.Values["id"].ToString() != null)
            //{
            //Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //}
            List<VehicleRepairingItem> lstVRep = new List<VehicleRepairingItem>();
            VehicleRepairingService objVrepairing = new VehicleRepairingService();
            int cid = 0;            
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstVRep = objVrepairing.VehicleRepairingListData(cid);
            VehicleRepairingItem objVRepItem = new VehicleRepairingItem();
            objVRepItem.VehicleList = new List<VehicleRepairingItem>();
            objVRepItem.VehicleList.AddRange(lstVRep);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objVrepairing.GetAllComp();
            objVRepItem.ListComp = new List<CompanyItem>();
            objVRepItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objVrepairing.GetVehicle(cid);
            objVRepItem.ListVehicle = new List<VehicleItem>();
            objVRepItem.ListVehicle.AddRange(lstVehicle);

            #endregion

            #region Bind DropDown Rtype
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objVrepairing.GetRType();
            objVRepItem.ListMaster = new List<clsMasterData>();
            objVRepItem.ListMaster.AddRange(lstMasters1);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVRepItem);            
        }
        [HttpPost]
        public ActionResult Create(VehicleRepairingItem model)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.emp = Empid;            
            VehicleRepairingService objVRep = new VehicleRepairingService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
            {
                model.CompID = cid;
            }
            
            objVRep.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(VehicleRepairingItem model)
        {
            //    int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            VehicleRepairingService objVRepairing = new VehicleRepairingService();            
            model.UpdatedOn = System.DateTime.Now;
            objVRepairing.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            VehicleRepairingService objVService = new VehicleRepairingService();
            VehicleRepairingItem objVItem = new VehicleRepairingItem();
            objVItem = objVService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<VehicleRepairingItem> lstVehicle = new List<VehicleRepairingItem>();
            objVItem.VehicleList = new List<VehicleRepairingItem>();
            objVItem.VehicleList.AddRange(lstVehicle);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            VehicleRepairingService objVrepairing = new VehicleRepairingService();
            //VehicleRepairingItem objVRepItem = new VehicleRepairingItem();
            lstComp = objVrepairing.GetAllComp();
            objVItem.ListComp = new List<CompanyItem>();
            objVItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle1 = new List<VehicleItem>();
            lstVehicle1 = objVrepairing.GetVehicle(cid);
            objVItem.ListVehicle = new List<VehicleItem>();
            objVItem.ListVehicle.AddRange(lstVehicle1);

            #endregion

            #region Bind DropDown Rtype
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objVrepairing.GetRType();
            objVItem.ListMaster = new List<clsMasterData>();
            objVItem.ListMaster.AddRange(lstMasters1);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVItem);
        }
        public ActionResult View(int id)
        {
            VehicleRepairingService objVService = new VehicleRepairingService();
            VehicleRepairingItem objVItem = new VehicleRepairingItem();
            objVItem = objVService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<VehicleRepairingItem> lstVehicle = new List<VehicleRepairingItem>();
            objVItem.VehicleList = new List<VehicleRepairingItem>();
            objVItem.VehicleList.AddRange(lstVehicle);

            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            VehicleRepairingService objVrepairing = new VehicleRepairingService();
            //VehicleRepairingItem objVRepItem = new VehicleRepairingItem();
            lstComp = objVrepairing.GetAllComp();
            objVItem.ListComp = new List<CompanyItem>();
            objVItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle1 = new List<VehicleItem>();
            lstVehicle1 = objVrepairing.GetVehicle(cid);
            objVItem.ListVehicle = new List<VehicleItem>();
            objVItem.ListVehicle.AddRange(lstVehicle1);

            #endregion

            #region Bind DropDown Rtype
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objVrepairing.GetRType();
            objVItem.ListMaster = new List<clsMasterData>();
            objVItem.ListMaster.AddRange(lstMasters1);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVItem);
        }
        public ActionResult FillVehicle(int Compid)
        {
            int CompCode = Compid;
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            VehicleItem objVehicleItem = new VehicleItem();
            VehicleMasterService objVehicle = new VehicleMasterService();
            lstVehicle = objVehicle.GetVehicleByComp(CompCode);
            objVehicleItem.ListVehicle = new List<VehicleItem>();            
            objVehicleItem.ListVehicle.AddRange(lstVehicle);
            return Json(objVehicleItem.ListVehicle, JsonRequestBehavior.AllowGet);
        }
    }
}
