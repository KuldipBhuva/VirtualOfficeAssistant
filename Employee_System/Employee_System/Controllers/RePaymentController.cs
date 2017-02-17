using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Payroll;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class RePaymentController : Controller
    {
        //
        // GET: /RePayment/
        RePaymentService objPayService = new RePaymentService();
        RePaymentItem objPayItem = new RePaymentItem();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            BindMonthYear();
            objPayService = new RePaymentService();
            objPayItem = new RePaymentItem();

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objPayService.GetAllComp();
            objPayItem.ListComp = new List<CompanyItem>();
            objPayItem.ListComp.AddRange(lstComp);
            #endregion
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objPayService.GetEmp(cid);
            objPayItem.ListEmp = new List<EmployeeItem>();
            objPayItem.ListEmp.AddRange(lstEmp);
            #endregion

            List<RePaymentItem> objPay = new List<RePaymentItem>();
            objPay = objPayService.GetPaymentDetails(cid);
            objPayItem.ListRePayment = new List<RePaymentItem>();
            objPayItem.ListRePayment.AddRange(objPay);
            return View(objPayItem);
        }
        [HttpPost]
        public ActionResult Create(RePaymentItem model)
        {
            BindMonthYear();
            objPayService = new RePaymentService();
            objPayItem = new RePaymentItem();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompId == null)
            {
                model.CompId = cid;
            }
            objPayService.InsertRePayment(model.Month,  model.Year,  model.CompId,  model.EmpId,  model.Payment,  model.Installment);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objPayService.GetAllComp();
            objPayItem.ListComp = new List<CompanyItem>();
            objPayItem.ListComp.AddRange(lstComp);
            #endregion
        
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objPayService.GetEmp(cid);
            objPayItem.ListEmp = new List<EmployeeItem>();
            objPayItem.ListEmp.AddRange(lstEmp);
            #endregion

            List<RePaymentItem> objPay = new List<RePaymentItem>();
            objPay = objPayService.GetPaymentDetails(cid);
            objPayItem.ListRePayment = new List<RePaymentItem>();
            objPayItem.ListRePayment.AddRange(objPay);
            return View(objPayItem);
        }

        [HttpPost]
        public ActionResult Edit(RePaymentItem Model)
        {
            RePaymentService objPayService = new RePaymentService();
            RePaymentItem objPayItem = new RePaymentItem();
            objPayService.Update(Model);
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int Id)
        {

            objPayService = new RePaymentService();
            objPayItem = new RePaymentItem();
            List<RePaymentItem> ListRePayments = new List<RePaymentItem>();
            List<CompanyItem> lstComp = new List<CompanyItem>();
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            objPayItem = objPayService.GetById(Id);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            ListRePayments = objPayService.GetPaymentDetails(cid);
            objPayItem.ListRePayment = new List<RePaymentItem>();
            objPayItem.ListRePayment.AddRange(ListRePayments);

            #region Bind DropDown Comp
            lstComp = objPayService.GetAllComp();
            objPayItem.ListComp = new List<CompanyItem>();
            objPayItem.ListComp.AddRange(lstComp);

            #endregion
         
            #region Bind DropDown Emp
            lstEmp = objPayService.GetEmp(cid);
            objPayItem.ListEmp = new List<EmployeeItem>();
            objPayItem.ListEmp.AddRange(lstEmp);
            #endregion


            //Session["Empid"] = objPassItem.EmpId;
            //  ViewBag.DeptId = DepId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            BindMonthYear();
            return View(objPayItem);
        }

        public ActionResult AdvanceAmount(int EmpId)
        {
            int strEmp = EmpId;
            List<RePaymentItem> lstRepay = new List<RePaymentItem>();
            RePaymentItem objRepayItem = new RePaymentItem();
            RePaymentService objEmp = new RePaymentService();
            lstRepay = objEmp.GetAmtDetails(strEmp);
            objRepayItem.ListRePayment = new List<RePaymentItem>();
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            objRepayItem.ListRePayment.AddRange(lstRepay);
            // lstRepay.
            if (objRepayItem.ListRePayment.Count != 0)
            {
                return Json(objRepayItem.ListRePayment[0].Payment, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }

        public void BindMonthYear()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            items.Add(new SelectListItem { Text = "January", Value = "1" });
            items.Add(new SelectListItem { Text = "February", Value = "2" });
            items.Add(new SelectListItem { Text = "March", Value = "3" });
            items.Add(new SelectListItem { Text = "April", Value = "4" });
            items.Add(new SelectListItem { Text = "May", Value = "5" });
            items.Add(new SelectListItem { Text = "June", Value = "6" });
            items.Add(new SelectListItem { Text = "July", Value = "7" });
            items.Add(new SelectListItem { Text = "August", Value = "8" });
            items.Add(new SelectListItem { Text = "September", Value = "9" });
            items.Add(new SelectListItem { Text = "October", Value = "10" });
            items.Add(new SelectListItem { Text = "November", Value = "11" });
            items.Add(new SelectListItem { Text = "December", Value = "12" });
            //Assign the value to ViewBag
            ViewBag.Months = items;

            //Populate year data in controller
            List<SelectListItem> drodwnitems = new List<SelectListItem>();
            drodwnitems.Add(new SelectListItem { Text = "--Select--", Value = "0" });
            drodwnitems.Add(new SelectListItem { Text = "2016", Value = "2016" });
            drodwnitems.Add(new SelectListItem { Text = "2017", Value = "2017" });
            drodwnitems.Add(new SelectListItem { Text = "2018", Value = "2018" });
            //Assign the value to ViewBag

            ViewBag.Year = drodwnitems;






        }
    }
}
