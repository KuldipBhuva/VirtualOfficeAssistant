using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Insurance;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class VehicleInsuranceController : Controller
    {
        //
        // GET: /VehicleInsurance/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                EmpInsuranceService objService = new EmpInsuranceService();
                EmpInsuranceItem objDoc = new EmpInsuranceItem();
                objDoc = objService.GetById(id);
                db.EmployeeInsurances.Remove(db.EmployeeInsurances.Find(id));
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
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            List<EmpInsuranceItem> lstInsu = new List<EmpInsuranceItem>();
            EmpInsuranceService objInsu = new EmpInsuranceService();
            //ViewBag.Empid = Empid;
            int cid = 0;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstInsu = objInsu.VehicleInsuranceList(cid);
            EmpInsuranceItem objEmpItem = new EmpInsuranceItem();
            objEmpItem.ListInsurance = new List<EmpInsuranceItem>();
            objEmpItem.ListInsurance.AddRange(lstInsu);
            VehicleRepairingService objVrepairing = new VehicleRepairingService();
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle1 = new List<VehicleItem>();
            lstVehicle1 = objVrepairing.GetVehicle(cid);
            objEmpItem.ListVehicle = new List<VehicleItem>();
            objEmpItem.ListVehicle.AddRange(lstVehicle1);

            #endregion

            #region Bind DropDown Icomp
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objInsu.GetALLIComp();
            objEmpItem.ListMasterTable = new List<clsMasterData>();
            objEmpItem.ListMasterTable.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Ptype
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objInsu.GetALLPType();
            objEmpItem.ListPolicyType = new List<clsMasterData>();
            objEmpItem.ListPolicyType.AddRange(lstMasters1);

            #endregion
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objInsu.GetBranch();
            objEmpItem.ListBranch = new List<BranchItem>();
            objEmpItem.ListBranch.AddRange(lstBranch);

            #endregion

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objInsu.GetComp();
            objEmpItem.ListComp = new List<CompanyItem>();
            objEmpItem.ListComp.AddRange(lstComp);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objInsu.GetEmp();
            objEmpItem.ListEmp = new List<EmployeeItem>();
            objEmpItem.ListEmp.AddRange(lstEmp);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objEmpItem);
        }
        [HttpPost]
        public ActionResult Create(EmpInsuranceItem model)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpId = Empid;
            model.Ptype = 42;
            EmpInsuranceService objEmpInc = new EmpInsuranceService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID==null)
            {
                model.CompID = cid;
            }
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objEmpInc.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(EmpInsuranceItem model)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpId = Empid;
            EmpInsuranceService objPass = new EmpInsuranceService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }

            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            model.Ptype = 42;
            objPass.Update(model);
            //ViewBag.Empid = Empid;

            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int IID)
        {
            EmpInsuranceService objInsService = new EmpInsuranceService();
            EmpInsuranceItem objInsItem = new EmpInsuranceItem();
            objInsItem = objInsService.GetById(IID);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpInsuranceItem> lstInsu = new List<EmpInsuranceItem>();
            objInsItem.ListInsurance = new List<EmpInsuranceItem>();
            objInsItem.ListInsurance.AddRange(lstInsu);
            ViewBag.IID = IID;
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            VehicleRepairingService objVrepairing = new VehicleRepairingService();
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle1 = new List<VehicleItem>();
            lstVehicle1 = objVrepairing.GetVehicle(cid);
            objInsItem.ListVehicle = new List<VehicleItem>();
            objInsItem.ListVehicle.AddRange(lstVehicle1);

            #endregion

            #region Bind DropDown Icomp
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objInsService.GetALLIComp();
            objInsItem.ListMasterTable = new List<clsMasterData>();
            objInsItem.ListMasterTable.AddRange(lstMasters);

            #endregion

            #region Bind DropDown Ptype
            lstMasters = objInsService.GetALLPType();
            objInsItem.ListPolicyType = new List<clsMasterData>();
            objInsItem.ListPolicyType.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objInsService.GetBranch();
            objInsItem.ListBranch = new List<BranchItem>();
            objInsItem.ListBranch.AddRange(lstBranch);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objInsService.GetEmp();
            objInsItem.ListEmp = new List<EmployeeItem>();
            objInsItem.ListEmp.AddRange(lstEmp);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objInsService.GetComp();
            objInsItem.ListComp = new List<CompanyItem>();
            objInsItem.ListComp.AddRange(lstComp);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objInsItem);
        }
        public ActionResult View(int IID)
        {
            EmpInsuranceService objInsService = new EmpInsuranceService();
            EmpInsuranceItem objInsItem = new EmpInsuranceItem();
            objInsItem = objInsService.GetById(IID);
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpInsuranceItem> lstInsu = new List<EmpInsuranceItem>();
            objInsItem.ListInsurance = new List<EmpInsuranceItem>();
            objInsItem.ListInsurance.AddRange(lstInsu);
            ViewBag.IID = IID;
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            VehicleRepairingService objVrepairing = new VehicleRepairingService();
            #region Bind DropDown Vehicle
            List<VehicleItem> lstVehicle1 = new List<VehicleItem>();
            lstVehicle1 = objVrepairing.GetVehicle(cid);
            objInsItem.ListVehicle = new List<VehicleItem>();
            objInsItem.ListVehicle.AddRange(lstVehicle1);
            #endregion

            #region Bind DropDown Icomp
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objInsService.GetALLIComp();
            objInsItem.ListMasterTable = new List<clsMasterData>();
            objInsItem.ListMasterTable.AddRange(lstMasters);

            #endregion

            #region Bind DropDown Ptype
            lstMasters = objInsService.GetALLPType();
            objInsItem.ListPolicyType = new List<clsMasterData>();
            objInsItem.ListPolicyType.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objInsService.GetBranch();
            objInsItem.ListBranch = new List<BranchItem>();
            objInsItem.ListBranch.AddRange(lstBranch);

            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objInsService.GetEmp();
            objInsItem.ListEmp = new List<EmployeeItem>();
            objInsItem.ListEmp.AddRange(lstEmp);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objInsService.GetComp();
            objInsItem.ListComp = new List<CompanyItem>();
            objInsItem.ListComp.AddRange(lstComp);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objInsItem);
        }
    }
}
