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
    public class SponsorController : Controller
    {
        //
        // GET: /Sponsor/

        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                SponsorService objService = new SponsorService();
                SponsorItem objDoc = new SponsorItem();
                objDoc = objService.GetById(id);
                db.CredentialMasters.Remove(db.CredentialMasters.Find(id));
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
            List<SponsorItem> lst = new List<SponsorItem>();
            SponsorService objService = new SponsorService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lst = objService.getSponsorData(cid);
            SponsorItem objModel = new SponsorItem();
            objModel.ListSponsor = new List<SponsorItem>();
            objModel.ListSponsor.AddRange(lst);

            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objModel.ListCompany = new List<CompanyItem>();
            objModel.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objModel.ListBranch = new List<BranchItem>();
            objModel.ListBranch.AddRange(lstBranch);

            #endregion

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Create(SponsorItem model)
        {
            SponsorService objService = new SponsorService();
            model.Status = 1;
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
            {
                model.CompID = cid;
            }
            
            objService.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(SponsorItem model)
        {
            //    int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            SponsorService objAppoint = new SponsorService();
            objAppoint.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            SponsorService objAppointService = new SponsorService();
            SponsorItem objAItem = new SponsorItem();
            objAItem = objAppointService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<SponsorItem> lstAppoint = new List<SponsorItem>();
            objAItem.ListSponsor = new List<SponsorItem>();
            objAItem.ListSponsor.AddRange(lstAppoint);
            ViewBag.Menuid = Request.QueryString["menuId"];

            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objAppointService.GetCompany();
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
            SponsorService objAppointService = new SponsorService();
            SponsorItem objAItem = new SponsorItem();
            objAItem = objAppointService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<SponsorItem> lstAppoint = new List<SponsorItem>();
            objAItem.ListSponsor = new List<SponsorItem>();
            objAItem.ListSponsor.AddRange(lstAppoint);
            ViewBag.Menuid = Request.QueryString["menuId"];

            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objAppointService.GetCompany();
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
