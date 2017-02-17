using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Expenses;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class TravelExpenseController : Controller
    {
        //
        // GET: /TravelExpence/
        EmployeeEntities db = new EmployeeEntities();        
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                TExpenseService objTenService = new TExpenseService();
                TExpenseItem objDoc = new TExpenseItem();
                objDoc = objTenService.GetById(id);
                db.TravelExpences.Remove(db.TravelExpences.Find(id));
                db.SaveChanges();
                //ViewBag.Empid = Empid;
                ViewBag.Menuid = Request.QueryString["menuId"];
            }
            catch (Exception ex)
            {
                //ViewBag.ErrorMsg = "First Delete This Tenancy No's All Documents. ";                
                return View("Error");
            }
            return RedirectToAction("Create", new {@menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Create()
        {
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<TExpenseItem> lstTExp = new List<TExpenseItem>();
            TExpenseService objTExp = new TExpenseService();
            lstTExp = objTExp.TExpenseData(cid);
            TExpenseItem objTExpItem = new TExpenseItem();
            objTExpItem.ListTExp = new List<TExpenseItem>();
            objTExpItem.ListTExp.AddRange(lstTExp);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objTExp.getCountry();
            objTExpItem.ListCon = new List<clsMasterData>();
            objTExpItem.ListCon.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objTExp.GetAllComp();
            objTExpItem.ListComp = new List<CompanyItem>();
            objTExpItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objTExp.GetEmp(cid);
            objTExpItem.ListEmp = new List<EmployeeItem>();
            objTExpItem.ListEmp.AddRange(lstEmp);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTExpItem);
        }
        [HttpPost]
        public ActionResult Create(TExpenseItem model)
        {            
            model.Status = "Active";
            TExpenseService objTExp = new TExpenseService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.Companyid == null)
            {
                model.Companyid = cid;
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objTExp.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(TExpenseItem model)
        {
       
            TExpenseService objTExp = new TExpenseService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objTExp.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            TExpenseService objTExpService = new TExpenseService();
            TExpenseItem objTExpItem = new TExpenseItem();
            objTExpItem = objTExpService.GetById(id);
            List<TExpenseItem> lstVehicle = new List<TExpenseItem>();
            objTExpItem.ListTExp = new List<TExpenseItem>();
            objTExpItem.ListTExp.AddRange(lstVehicle);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objTExpService.getCountry();
            objTExpItem.ListCon = new List<clsMasterData>();
            objTExpItem.ListCon.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objTExpService.GetAllComp();
            objTExpItem.ListComp = new List<CompanyItem>();
            objTExpItem.ListComp.AddRange(lstComp);
            #endregion
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objTExpService.GetEmp(cid);
            objTExpItem.ListEmp = new List<EmployeeItem>();
            objTExpItem.ListEmp.AddRange(lstEmp);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTExpItem);
        }
        public ActionResult View(int id)
        {
            TExpenseService objTExpService = new TExpenseService();
            TExpenseItem objTExpItem = new TExpenseItem();
            objTExpItem = objTExpService.GetById(id);
            List<TExpenseItem> lstVehicle = new List<TExpenseItem>();
            objTExpItem.ListTExp = new List<TExpenseItem>();
            objTExpItem.ListTExp.AddRange(lstVehicle);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objTExpService.getCountry();
            objTExpItem.ListCon = new List<clsMasterData>();
            objTExpItem.ListCon.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objTExpService.GetAllComp();
            objTExpItem.ListComp = new List<CompanyItem>();
            objTExpItem.ListComp.AddRange(lstComp);
            #endregion
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objTExpService.GetEmp(cid);
            objTExpItem.ListEmp = new List<EmployeeItem>();
            objTExpItem.ListEmp.AddRange(lstEmp);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTExpItem);
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
    }
}
