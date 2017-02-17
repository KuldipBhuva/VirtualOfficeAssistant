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
using EMSDomain.ViewModel.Expenses;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class OtherExpenseController : Controller
    {
        //
        // GET: /OtherExpense/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                OtherExpService objTenService = new OtherExpService();
                OtherExpItem objDoc = new OtherExpItem();
                objDoc = objTenService.GetById(id);
                db.Other_Expenses.Remove(db.Other_Expenses.Find(id));
                db.SaveChanges();
                //ViewBag.Empid = Empid;
                ViewBag.Menuid = Request.QueryString["menuId"];
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMsg = "First Delete This Tenancy No's All Documents. ";                
                return View("Error");
            }
            return RedirectToAction("Create", new { @menuId = Request.QueryString["menuId"] });
        }

        public ActionResult Create()
        {
            List<OtherExpItem> lstOExp = new List<OtherExpItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
         
            OtherExpService objOExp = new OtherExpService();

            lstOExp = objOExp.OtherExpData(cid);
            OtherExpItem objOExpItem = new OtherExpItem();
            objOExpItem.ListOExp = new List<OtherExpItem>();
            objOExpItem.ListOExp.AddRange(lstOExp);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objOExp.GetAllComp();
            objOExpItem.ListComp = new List<CompanyItem>();
            objOExpItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objOExp.GetEmp();
            objOExpItem.ListEmp = new List<EmployeeItem>();
            objOExpItem.ListEmp.AddRange(lstEmp);
            #endregion

            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objOExp.GetBranch();
            objOExpItem.ListBranch = new List<BranchItem>();
            objOExpItem.ListBranch.AddRange(lstBranch);

            #endregion

            #region Bind DropDown Exp
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objOExp.GetExp();
            objOExpItem.ListMaster = new List<clsMasterData>();
            objOExpItem.ListMaster.AddRange(lstMasters1);

            #endregion

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objOExpItem);
        }
        [HttpPost]
        public ActionResult Create(OtherExpItem model)
        {
            OtherExpService objTExp = new OtherExpService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
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
            objTExp.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult FillBranch(int Compid)
        {
            //string strCompCode = Convert.ToString(Compid);
            List<BranchItem> lstBranch = new List<BranchItem>();
            BranchItem objBranchItem = new BranchItem();
            BranchService objBranchS = new BranchService();
            lstBranch = objBranchS.GetBranchByComp(Compid);
            objBranchItem.ListBranch = new List<BranchItem>();
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            objBranchItem.ListBranch.AddRange(lstBranch);
            //var e = new { BranchItem = objBranchItem.ListBranch, EmployeeItem = objBranchItem.ListBranch };
            return Json(objBranchItem.ListBranch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillEmp(int Compid)
        {
            int strCompCode = Compid;
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            EmployeeItem objEmpItem = new EmployeeItem();
            EmployeeService objEmp = new EmployeeService();
            lstEmp = objEmp.GetEmpByComp(strCompCode);
            objEmpItem.EmployeeList = new List<EmployeeItem>();
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            objEmpItem.EmployeeList.AddRange(lstEmp);
            return Json(objEmpItem.EmployeeList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Edit(OtherExpItem model)
        {

            OtherExpService objTExp = new OtherExpService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdateBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objTExp.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        
        public ActionResult Edit(int id)
        {
            OtherExpService objOExpService = new OtherExpService();
            OtherExpItem objOExpItem = new OtherExpItem();
            objOExpItem = objOExpService.GetById(id);
            List<OtherExpItem> lstVehicle = new List<OtherExpItem>();
            objOExpItem.ListOExp = new List<OtherExpItem>();
            objOExpItem.ListOExp.AddRange(lstVehicle);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objOExpService.GetAllComp();
            objOExpItem.ListComp = new List<CompanyItem>();
            objOExpItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objOExpService.GetEmp();
            objOExpItem.ListEmp = new List<EmployeeItem>();
            objOExpItem.ListEmp.AddRange(lstEmp);
            #endregion
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objOExpService.GetBranch();
            objOExpItem.ListBranch = new List<BranchItem>();
            objOExpItem.ListBranch.AddRange(lstBranch);

            #endregion
            #region Bind DropDown Exp
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objOExpService.GetExp();
            objOExpItem.ListMaster = new List<clsMasterData>();
            objOExpItem.ListMaster.AddRange(lstMasters1);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objOExpItem);
        }
        public ActionResult View(int id)
        {
            OtherExpService objOExpService = new OtherExpService();
            OtherExpItem objOExpItem = new OtherExpItem();
            objOExpItem = objOExpService.GetById(id);
            List<OtherExpItem> lstVehicle = new List<OtherExpItem>();
            objOExpItem.ListOExp = new List<OtherExpItem>();
            objOExpItem.ListOExp.AddRange(lstVehicle);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objOExpService.GetAllComp();
            objOExpItem.ListComp = new List<CompanyItem>();
            objOExpItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objOExpService.GetEmp();
            objOExpItem.ListEmp = new List<EmployeeItem>();
            objOExpItem.ListEmp.AddRange(lstEmp);
            #endregion
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objOExpService.GetBranch();
            objOExpItem.ListBranch = new List<BranchItem>();
            objOExpItem.ListBranch.AddRange(lstBranch);

            #endregion
            #region Bind DropDown Exp
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objOExpService.GetExp();
            objOExpItem.ListMaster = new List<clsMasterData>();
            objOExpItem.ListMaster.AddRange(lstMasters1);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objOExpItem);
        }
    }
}
