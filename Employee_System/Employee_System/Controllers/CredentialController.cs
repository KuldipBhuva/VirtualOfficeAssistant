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
    public class CredentialController : Controller
    {
        //
        // GET: /Credential/

        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                CredentialService objService = new CredentialService();
                CredentialItem objDoc = new CredentialItem();
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
            List<CredentialItem> lst = new List<CredentialItem>();
            CredentialService objService = new CredentialService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lst = objService.getCredentialData(cid);
            CredentialItem objModel = new CredentialItem();
            objModel.ListCredential = new List<CredentialItem>();
            objModel.ListCredential.AddRange(lst);
            ViewBag.Menuid = Request.QueryString["menuId"];

            SponsorService objService1 = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService1.GetCompany();
            objModel.ListCompany = new List<CompanyItem>();
            objModel.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objModel.ListBranch = new List<BranchItem>();
            objModel.ListBranch.AddRange(lstBranch);

            #endregion

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Create(CredentialItem model)
        {
            CredentialService objService = new CredentialService();
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
        public ActionResult Edit(CredentialItem model)
        {
            //    int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            CredentialService objAppoint = new CredentialService();
            objAppoint.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            CredentialService objAppointService = new CredentialService();
            CredentialItem objAItem = new CredentialItem();
            objAItem = objAppointService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<CredentialItem> lstAppoint = new List<CredentialItem>();
            objAItem.ListCredential = new List<CredentialItem>();
            objAItem.ListCredential.AddRange(lstAppoint);
            ViewBag.Menuid = Request.QueryString["menuId"];

            SponsorService objService1 = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService1.GetCompany();
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
            CredentialService objAppointService = new CredentialService();
            CredentialItem objAItem = new CredentialItem();
            objAItem = objAppointService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<CredentialItem> lstAppoint = new List<CredentialItem>();
            objAItem.ListCredential = new List<CredentialItem>();
            objAItem.ListCredential.AddRange(lstAppoint);
            ViewBag.Menuid = Request.QueryString["menuId"];


            SponsorService objService1 = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService1.GetCompany();
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
