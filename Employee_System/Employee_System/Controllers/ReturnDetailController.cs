using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Assests;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class ReturnDetailController : Controller
    {
        //
        // GET: /ReurnDetail/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
        {

            ReturnItem objRetItem = new ReturnItem();
            ReturnDetailsService objRetService = new ReturnDetailsService();

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objRetService.GetAllComp();
            objRetItem.ListComp = new List<CompanyItem>();
            objRetItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objRetService.GetEmp();
            objRetItem.ListEmp = new List<EmployeeItem>();
            objRetItem.ListEmp.AddRange(lstEmp);
            #endregion

            List<AssestsItem> lstItem = new List<AssestsItem>();
            lstItem = objRetService.GetALLItems();
            objRetItem.lstAllItem = new List<AssestsItem>();
            objRetItem.lstAllItem.AddRange(lstItem);

            List<ReturnItem> lstReturn = new List<ReturnItem>();
            lstReturn = objRetService.GetALLReturnDetail();
            objRetItem.lstReturnItem = new List<ReturnItem>();
            objRetItem.lstReturnItem.AddRange(lstReturn);
            return View(objRetItem);
        }

        [HttpPost]
        public ActionResult Create(ReturnItem Model)
        {
            ReturnItem objRetItem = new ReturnItem();
            ReturnDetailsService objRetService = new ReturnDetailsService();

            int i = objRetService.InsertReturnDetails(Model);
            StockItem objStockModel = new StockItem();
            objStockModel.ItemId = Model.ItemId;
            objStockModel.ReturnQty = Model.Qty;
            objStockModel.ReturnDate = Model.ReDate;
            int j = objRetService.InsertReturnStock(objStockModel);
            return RedirectToAction("Create");

        }

        public ActionResult Edit(int Id)
        {
            ReturnItem objRetItem = new ReturnItem();
            ReturnDetailsService objRetService = new ReturnDetailsService();
            objRetItem = objRetService.GetById(Id);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objRetService.GetAllComp();
            objRetItem.ListComp = new List<CompanyItem>();
            objRetItem.ListComp.AddRange(lstComp);
            #endregion

            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objRetService.GetEmp();
            objRetItem.ListEmp = new List<EmployeeItem>();
            objRetItem.ListEmp.AddRange(lstEmp);
            #endregion

            List<AssestsItem> lstItem = new List<AssestsItem>();
            lstItem = objRetService.GetALLItems();
            objRetItem.lstAllItem = new List<AssestsItem>();
            objRetItem.lstAllItem.AddRange(lstItem);

            List<ReturnItem> lstReturn = new List<ReturnItem>();
            lstReturn = objRetService.GetALLReturnDetail();
            objRetItem.lstReturnItem = new List<ReturnItem>();
            objRetItem.lstReturnItem.AddRange(lstReturn);
            return View(objRetItem);

        }

        [HttpPost]
        public ActionResult Edit(ReturnItem Model)
        {
            ReturnItem objRetItem = new ReturnItem();
            ReturnDetailsService objRetService = new ReturnDetailsService();

            int i = objRetService.Update(Model);
            return RedirectToAction("Create");

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
