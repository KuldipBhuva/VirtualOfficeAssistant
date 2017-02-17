using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class AppointmentController : Controller
    {
        //
        // GET: /Appointment/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                AppointmentService objService = new AppointmentService();
                AppointmentsItem objDoc = new AppointmentsItem();
                objDoc = objService.GetById(id);
                db.AppointmentsMasters.Remove(db.AppointmentsMasters.Find(id));
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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            List<AppointmentsItem> lstAppoint = new List<AppointmentsItem>();
            AppointmentService objAppoint = new AppointmentService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstAppoint = objAppoint.getAppointmentData(cid);
            AppointmentsItem objAppointItem = new AppointmentsItem();
            objAppointItem.ListAppointment = new List<AppointmentsItem>();
            objAppointItem.ListAppointment.AddRange(lstAppoint);
            ViewBag.Menuid = Request.QueryString["menuId"];

            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objAppointItem.ListCompany = new List<CompanyItem>();
            objAppointItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objAppointItem.ListBranch = new List<BranchItem>();
            objAppointItem.ListBranch.AddRange(lstBranch);

            #endregion

            return View(objAppointItem);
        }
        [HttpPost]
        public ActionResult Create(AppointmentsItem model)
        {
            AppointmentService objAppoint = new AppointmentService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
            {
                model.CompID = cid;
            }
            objAppoint.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(AppointmentsItem model)
        {
            //    int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            AppointmentService objAppoint = new AppointmentService();
            objAppoint.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            AppointmentService objAppointService = new AppointmentService();
            AppointmentsItem objAItem = new AppointmentsItem();
            objAItem = objAppointService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<AppointmentsItem> lstAppoint = new List<AppointmentsItem>();
            objAItem.ListAppointment = new List<AppointmentsItem>();
            objAItem.ListAppointment.AddRange(lstAppoint);           
            ViewBag.Menuid = Request.QueryString["menuId"];

            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objAItem.ListCompany = new List<CompanyItem>();
            objAItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objAItem.ListBranch = new List<BranchItem>();
            objAItem.ListBranch.AddRange(lstBranch);

            #endregion

            return View(objAItem);
        }
        public ActionResult View(int id)
        {
            AppointmentService objAppointService = new AppointmentService();
            AppointmentsItem objAItem = new AppointmentsItem();
            objAItem = objAppointService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<AppointmentsItem> lstAppoint = new List<AppointmentsItem>();
            objAItem.ListAppointment = new List<AppointmentsItem>();
            objAItem.ListAppointment.AddRange(lstAppoint);
            ViewBag.Menuid = Request.QueryString["menuId"];

            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objAItem.ListCompany = new List<CompanyItem>();
            objAItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objAItem.ListBranch = new List<BranchItem>();
            objAItem.ListBranch.AddRange(lstBranch);

            #endregion

            return View(objAItem);
        }
    }
}
