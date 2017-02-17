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
    public class PetrolExpController : Controller
    {
        //
        // GET: /PetrolCard/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                PetrolExpService objService = new PetrolExpService();
                PetrolExpItem objDoc = new PetrolExpItem();
                objDoc = objService.GetById(id);
                db.Petrol_Expense.Remove(db.Petrol_Expense.Find(id));
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
            List<PetrolExpItem> lstPcard = new List<PetrolExpItem>();
            PetrolExpService objPcard = new PetrolExpService();

            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }

            lstPcard = objPcard.PetrolListData(cid);
            PetrolExpItem objPCardItem = new PetrolExpItem();
            objPCardItem.ListPCard = new List<PetrolExpItem>();
            objPCardItem.ListPCard.AddRange(lstPcard);
          
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objPcard.GetVehicle(cid);
            objPCardItem.ListVehicle = new List<VehicleItem>();
            objPCardItem.ListVehicle.AddRange(lstVehicle);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objPcard.GetEmp(cid);
            objPCardItem.ListEmp = new List<EmployeeItem>();
            objPCardItem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View( objPCardItem);
        }
        [HttpPost]
        public ActionResult Create(PetrolExpItem model)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            PetrolExpService objPCard = new PetrolExpService();
            
            objPCard.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(PetrolExpItem model)
        {
            PetrolExpService objPExp = new PetrolExpService();
            objPExp.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            PetrolExpService objPExpService = new PetrolExpService();
            PetrolExpItem objPExpItem = new PetrolExpItem();
            objPExpItem = objPExpService.GetById(id);
            List<PetrolExpItem> lstPExp = new List<PetrolExpItem>();
            objPExpItem.ListPCard = new List<PetrolExpItem>();
            objPExpItem.ListPCard.AddRange(lstPExp);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objPExpService.GetVehicle(cid);
            objPExpItem.ListVehicle = new List<VehicleItem>();
            objPExpItem.ListVehicle.AddRange(lstVehicle);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objPExpService.GetEmp(cid);
            objPExpItem.ListEmp = new List<EmployeeItem>();
            objPExpItem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPExpItem);
        }
        public ActionResult View(int id)
        {
            PetrolExpService objPExpService = new PetrolExpService();
            PetrolExpItem objPExpItem = new PetrolExpItem();
            objPExpItem = objPExpService.GetById(id);
            List<PetrolExpItem> lstPExp = new List<PetrolExpItem>();
            objPExpItem.ListPCard = new List<PetrolExpItem>();
            objPExpItem.ListPCard.AddRange(lstPExp);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objPExpService.GetVehicle(cid);
            objPExpItem.ListVehicle = new List<VehicleItem>();
            objPExpItem.ListVehicle.AddRange(lstVehicle);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objPExpService.GetEmp(cid);
            objPExpItem.ListEmp = new List<EmployeeItem>();
            objPExpItem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objPExpItem);
        }
    }
}
