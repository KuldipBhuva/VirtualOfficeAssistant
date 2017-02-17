using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class VehicleHistoryController : Controller
    {
        //
        // GET: /VehicleHistory/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                VehicleHistoryService objService = new VehicleHistoryService();
                VehicleHistoryItem objDoc = new VehicleHistoryItem();
                objDoc = objService.GetById(id);
                db.Vehicle_History.Remove(db.Vehicle_History.Find(id));
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
            List<VehicleHistoryItem> lstVHItem = new List<VehicleHistoryItem>();
            VehicleHistoryService objVH = new VehicleHistoryService();

            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstVHItem = objVH.GetListVehicle(cid);
            VehicleHistoryItem objVHITem = new VehicleHistoryItem();
            objVHITem.ListVH = new List<VehicleHistoryItem>();
            objVHITem.ListVH.AddRange(lstVHItem);

         
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objVH.GetVehicle(cid);
            objVHITem.ListVehicle = new List<VehicleItem>();
            objVHITem.ListVehicle.AddRange(lstVehicle);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objVH.GetEmp(cid);
            objVHITem.ListEmp = new List<EmployeeItem>();
            objVHITem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVHITem);
        }
        [HttpPost]
        public ActionResult Create(VehicleHistoryItem model)
        {
            VehicleHistoryService objVH = new VehicleHistoryService();
            
            objVH.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(VehicleHistoryItem model)
        {
            VehicleHistoryService objVH = new VehicleHistoryService();
            model.UpdatedOn = System.DateTime.Now;
            objVH.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            VehicleHistoryService objVHService = new VehicleHistoryService();
            VehicleHistoryItem objVHItem = new VehicleHistoryItem();
            objVHItem = objVHService.GetById(id);
            List<VehicleHistoryItem> lstPExp = new List<VehicleHistoryItem>();
            objVHItem.ListVH = new List<VehicleHistoryItem>();
            objVHItem.ListVH.AddRange(lstPExp);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objVHService.GetVehicle(cid);
            objVHItem.ListVehicle = new List<VehicleItem>();
            objVHItem.ListVehicle.AddRange(lstVehicle);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objVHService.GetEmp(cid);
            objVHItem.ListEmp = new List<EmployeeItem>();
            objVHItem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVHItem);
        }
        public ActionResult View(int id)
        {
            VehicleHistoryService objVHService = new VehicleHistoryService();
            VehicleHistoryItem objVHItem = new VehicleHistoryItem();
            objVHItem = objVHService.GetById(id);
            List<VehicleHistoryItem> lstPExp = new List<VehicleHistoryItem>();
            objVHItem.ListVH = new List<VehicleHistoryItem>();
            objVHItem.ListVH.AddRange(lstPExp);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objVHService.GetVehicle(cid);
            objVHItem.ListVehicle = new List<VehicleItem>();
            objVHItem.ListVehicle.AddRange(lstVehicle);

            #endregion

            
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objVHService.GetEmp(cid);
            objVHItem.ListEmp = new List<EmployeeItem>();
            objVHItem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVHItem);
        }
    }
}
