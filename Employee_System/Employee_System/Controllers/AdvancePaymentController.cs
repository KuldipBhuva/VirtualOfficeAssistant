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
    public class AdvancePaymentController : Controller
    {
        //
        // GET: /AdvancePayment/
        AdvancePaymentService objPayService = new AdvancePaymentService();
        AdvancePaymentItem objPayItem = new AdvancePaymentItem();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            objPayService = new AdvancePaymentService();
            objPayItem = new AdvancePaymentItem();

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

            List<AdvancePaymentItem> objPay = new List<AdvancePaymentItem>();
            objPay = objPayService.GetPaymentDetails(cid);
            objPayItem.ListAdvPayment = new List<AdvancePaymentItem>();
            objPayItem.ListAdvPayment.AddRange(objPay);
            return View(objPayItem);
        }
        [HttpPost]
        public ActionResult Create(AdvancePaymentItem model)
        {
            objPayService = new AdvancePaymentService();
            objPayItem = new AdvancePaymentItem();
            objPayService.Insert(model);

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

            List<AdvancePaymentItem> objPay = new List<AdvancePaymentItem>();
            objPay = objPayService.GetPaymentDetails(cid);
            objPayItem.ListAdvPayment = new List<AdvancePaymentItem>();
            objPayItem.ListAdvPayment.AddRange(objPay);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Edit(AdvancePaymentItem Model)
        {
            AdvancePaymentService objPayService = new AdvancePaymentService();
            AdvancePaymentItem objPayItem = new AdvancePaymentItem();
            
            objPayService.Update(Model);
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int id)
        {
            List<AdvancePaymentItem> objPay = new List<AdvancePaymentItem>();
            objPayItem = new AdvancePaymentItem();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            objPayItem = objPayService.GetById(id);
            objPay = objPayService.GetPaymentDetails(cid);
            objPayItem.ListAdvPayment = new List<AdvancePaymentItem>();
            objPayItem.ListAdvPayment.AddRange(objPay);

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

            return View(objPayItem);
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
