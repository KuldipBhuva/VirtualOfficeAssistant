using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSMethods;
using EMSDomain.ViewModel.TradeLicense;
using EMSDomain.Model;
using EMSDomain.ViewModel.Tenancy;
using EMSDomain.ViewModel.Vehicle;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Payroll;

namespace Employee_System.Controllers
{
    public class ReportController : Controller
    {
        //
        // GET: /Report/
        public ActionResult DLReport()
        {
            EmpHealthItem data = TempData["data"] as EmpHealthItem;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(data);
        }
        
        [HttpPost]
        public ActionResult DLReport(EmpHealthItem model)
        {
            string expdt;
            if (Convert.ToString(model.withinDays) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            EmpHealthItem objAllItem = new EmpHealthItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);

                string day = Convert.ToString(model.withinDays);


                List<EmpHealthItem> objMaster = objclsmethod.getDlicenseRpt(date,cid);
                objAllItem.ListDL = new List<EmpHealthItem>();
                objAllItem.ListDL.AddRange(objMaster);


            }
            else if (Convert.ToString(model.withinDays) != "")
            {
                string day = Convert.ToString(model.withinDays);
                List<EmpHealthItem> objMaster = objclsmethod.getDLbyDay(day,cid);
                objAllItem.ListDL = new List<EmpHealthItem>();
                objAllItem.ListDL.AddRange(objMaster);
            }
            else
            {
                List<EmpHealthItem> objMaster = objclsmethod.getDlicenseAllRpt(cid);
                objAllItem.ListDL = new List<EmpHealthItem>();
                objAllItem.ListDL.AddRange(objMaster);
            }
            TempData["data"] = objAllItem;
            return RedirectToAction("DLReport", new { @menuId = model.Viewbagidformenu });
        }

        //Tenacy 

        public ActionResult TenancyReport()
        {
            ViewBag.Menuid = Request.QueryString["menuId"];
            TenancyItem objAllItem = new TenancyItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<TenancyItem> objMaster = objclsmethod.gettenacny(cid);
            objAllItem.ListTenancy = new List<TenancyItem>();
            objAllItem.ListTenancy.AddRange(objMaster);


            return View("TenancyReport", objAllItem);
            //return View();
        }




        public ActionResult VehicalReport()
        {
            ViewBag.Menuid = Request.QueryString["menuId"];
            VehicleItem objAllItem = new VehicleItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<VehicleItem> objMaster = objclsmethod.getvehicalDetails(cid);
            objAllItem.ListVehicle = new List<VehicleItem>();
            objAllItem.ListVehicle.AddRange(objMaster);


            return View("VehicalReport", objAllItem);
            //return View();
        }
        //[HttpPost]
        //public ActionResult TenancyReport(TenancyItem model)
        //{

        //    TenancyItem objAllItem = new TenancyItem();
        //    ReportService objclsmethod = new ReportService();

        //        List<TenancyItem> objMaster = objclsmethod.gettenacny();
        //        objAllItem.ListTenancy = new List<TenancyItem>();
        //        objAllItem.ListTenancy.AddRange(objMaster);


        //        return View("TenancyReport", objAllItem);
        //}


        public ActionResult TradeReport()
        {
            TradeLicenseService objTrend = new TradeLicenseService();
            TradeLicenseItem objTradeItem = new TradeLicenseItem();
            List<clsMasterData> lstMasterDetails = new List<clsMasterData>();
            ReportService objclsmethod = new ReportService();
            //ViewBag.EID = objTradeItem;
            //objTradeItem = TempData["data"] as TradeLicenseItem;  
            //Emirates
            lstMasterDetails = objTrend.GetEmirates();
            objTradeItem.lstEmirates = new List<clsMasterData>();
            objTradeItem.lstEmirates.AddRange(lstMasterDetails);
                              
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTradeItem);
        }
        [HttpPost]
        public ActionResult TradeReport(TradeLicenseItem model)
        {
            string expdt;
            if (Convert.ToString(model.EmiratesID) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            TradeLicenseItem objAllItem = new TradeLicenseItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);
                List<TradeLicenseItem> objMaster = objclsmethod.gettradelicenseRpt(date,cid);
                objAllItem.LstTradeLicense = new List<TradeLicenseItem>();
                objAllItem.LstTradeLicense.AddRange(objMaster);


            }
            else if (model.EmiratesID != null)
            {
                int EID = Convert.ToInt32(model.EmiratesID);
                List<TradeLicenseItem> objMaster = objclsmethod.getTradeByEID(EID,cid);
                objAllItem.LstTradeLicense = new List<TradeLicenseItem>();
                objAllItem.LstTradeLicense.AddRange(objMaster);
            }
            else
            {
                List<TradeLicenseItem> objMaster = objclsmethod.getAllTradeRpt(cid);
                objAllItem.LstTradeLicense = new List<TradeLicenseItem>();
                objAllItem.LstTradeLicense.AddRange(objMaster);
            }
            TradeLicenseService objTrend = new TradeLicenseService();
            List<clsMasterData> lstMasterDetails = new List<clsMasterData>();
            //Emirates
            lstMasterDetails = objTrend.GetEmirates();
            objAllItem.lstEmirates = new List<clsMasterData>();
            objAllItem.lstEmirates.AddRange(lstMasterDetails);
            TempData["data"] = objAllItem;
            return View("TradeReport",objAllItem);
        }
        public ActionResult PassportReport()
        {
            EmpPassportItem data = TempData["data"] as EmpPassportItem;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(data);
        }
        [HttpPost]
        public ActionResult PassportReport(EmpPassportItem model)
        {
            string expdt;
            if (Convert.ToString(model.withinDays) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            EmpPassportItem objAllItem = new EmpPassportItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);

                string day = Convert.ToString(model.withinDays);

                
              
                List<EmpPassportItem> objMaster = objclsmethod.getPassportRpt(date,cid);
                objAllItem.ListPassRpt = new List<EmpPassportItem>();
                objAllItem.ListPassRpt.AddRange(objMaster);


            }
            else if (Convert.ToString(model.withinDays) != "")
            {
                string day = Convert.ToString(model.withinDays);
                List<EmpPassportItem> objMaster = objclsmethod.getPassportbyDay(day,cid);
                objAllItem.ListPassRpt = new List<EmpPassportItem>();
                objAllItem.ListPassRpt.AddRange(objMaster);
            }
            else
            {
                List<EmpPassportItem> objMaster = objclsmethod.getPassportAllRpt(cid);
                objAllItem.ListPassRpt = new List<EmpPassportItem>();
                objAllItem.ListPassRpt.AddRange(objMaster);
            }
            TempData["data"] = objAllItem;
            return RedirectToAction("PassportReport", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult HealthReport()
        {
            EmpHealthItem data = TempData["data"] as EmpHealthItem;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(data);
        }
        [HttpPost]
        public ActionResult HealthReport(EmpHealthItem model)
        {
            string expdt;
            if (Convert.ToString(model.withinDays) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            EmpHealthItem objAllItem = new EmpHealthItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);

                string day = Convert.ToString(model.withinDays);


                List<EmpHealthItem> objMaster = objclsmethod.getHealthRpt(date,cid);
                objAllItem.ListDL = new List<EmpHealthItem>();
                objAllItem.ListDL.AddRange(objMaster);


            }
            else if (Convert.ToString(model.withinDays) != "")
            {
                string day = Convert.ToString(model.withinDays);
                List<EmpHealthItem> objMaster = objclsmethod.getHealthbyDay(day,cid);
                objAllItem.ListDL = new List<EmpHealthItem>();
                objAllItem.ListDL.AddRange(objMaster);
            }
            else
            {
                List<EmpHealthItem> objMaster = objclsmethod.getHealthbyAllRpt(cid);
                objAllItem.ListDL = new List<EmpHealthItem>();
                objAllItem.ListDL.AddRange(objMaster);
            }
            TempData["data"] = objAllItem;
            return RedirectToAction("HealthReport", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult LabourReport()
        {
            EmpHealthItem data = TempData["data"] as EmpHealthItem;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(data);
        }
        [HttpPost]
        public ActionResult LabourReport(EmpHealthItem model)
        {
            string expdt;
            if (Convert.ToString(model.withinDays) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            EmpHealthItem objAllItem = new EmpHealthItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);

                string day = Convert.ToString(model.withinDays);


                List<EmpHealthItem> objMaster = objclsmethod.getLabourRpt(date,cid);
                objAllItem.ListLabourRpt = new List<EmpHealthItem>();
                objAllItem.ListLabourRpt.AddRange(objMaster);


            }
            else if (Convert.ToString(model.withinDays) != "")
            {
                string day = Convert.ToString(model.withinDays);
                List<EmpHealthItem> objMaster = objclsmethod.getLabourbyDay(day,cid);
                objAllItem.ListLabourRpt = new List<EmpHealthItem>();
                objAllItem.ListLabourRpt.AddRange(objMaster);
            }
            else
            {
                List<EmpHealthItem> objMaster = objclsmethod.getLabourAllRpt(cid);
                objAllItem.ListLabourRpt = new List<EmpHealthItem>();
                objAllItem.ListLabourRpt.AddRange(objMaster);
            }
            TempData["data"] = objAllItem;
            return RedirectToAction("LabourReport", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult EIDReport()
        {
            EmpHealthItem data = TempData["data"] as EmpHealthItem;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(data);
        }
        [HttpPost]
        public ActionResult EIDReport(EmpHealthItem model)
        {
            string expdt;
            if (Convert.ToString(model.withinDays) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            EmpHealthItem objAllItem = new EmpHealthItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);

                string day = Convert.ToString(model.withinDays);


                List<EmpHealthItem> objMaster = objclsmethod.getEIDRpt(date,cid);
                objAllItem.ListEIDRpt = new List<EmpHealthItem>();
                objAllItem.ListEIDRpt.AddRange(objMaster);


            }
            else if (Convert.ToString(model.withinDays) != "")
            {
                string day = Convert.ToString(model.withinDays);
                List<EmpHealthItem> objMaster = objclsmethod.getEIDbyDay(day,cid);
                objAllItem.ListEIDRpt = new List<EmpHealthItem>();
                objAllItem.ListEIDRpt.AddRange(objMaster);
            }
            else
            {
                List<EmpHealthItem> objMaster = objclsmethod.getEIDAllRpt(cid);
                objAllItem.ListEIDRpt = new List<EmpHealthItem>();
                objAllItem.ListEIDRpt.AddRange(objMaster);
            }
            TempData["data"] = objAllItem;
            return RedirectToAction("EIDReport", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult VisaReport()
        {
            EmpVisaItem data = TempData["data"] as EmpVisaItem;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(data);
        }
        [HttpPost]
        public ActionResult VisaReport(EmpVisaItem model)
        {
            string expdt;
            if (Convert.ToString(model.withinDays) != "")
            {
                expdt = "";
            }
            else
            {
                expdt = Convert.ToString(model.ExpiryDate);
            }
            EmpVisaItem objAllItem = new EmpVisaItem();
            ReportService objclsmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (expdt != "")
            {
                DateTime date = Convert.ToDateTime(model.ExpiryDate);

                string day = Convert.ToString(model.withinDays);


                List<EmpVisaItem> objMaster = objclsmethod.getVisaRpt(date,cid);
                objAllItem.ListVisa = new List<EmpVisaItem>();
                objAllItem.ListVisa.AddRange(objMaster);


            }
            else if (Convert.ToString(model.withinDays) != "")
            {
                string day = Convert.ToString(model.withinDays);
                List<EmpVisaItem> objMaster = objclsmethod.getVisabyDay(day,cid);
                objAllItem.ListVisa = new List<EmpVisaItem>();
                objAllItem.ListVisa.AddRange(objMaster);

            }
            else
            {
                List<EmpVisaItem> objMaster = objclsmethod.getVisaAllRpt(cid);
                objAllItem.ListVisa = new List<EmpVisaItem>();
                objAllItem.ListVisa.AddRange(objMaster);

            }
            TempData["data"] = objAllItem;
            //return View(objAllItem);
            return RedirectToAction("VisaReport", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult EmpReport()
        {
            EmployeeItem objEmpItem = new EmployeeItem();

            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            ReportService objmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstEmp = objmethod.GetData(cid);

            objEmpItem.EmployeeList = new List<EmployeeItem>();
            objEmpItem.EmployeeList.AddRange(lstEmp);

            //#region Bind DropDown comp
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objEmpItem.ListComp = new List<CompanyItem>();
            objEmpItem.ListComp.AddRange(objCompany);

            //#endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objEmpItem);
        }
        [HttpPost]
        public ActionResult EmpReport(EmployeeItem model)
        {
            EmployeeItem objEmpItem = new EmployeeItem();
            //#region Bind DropDown comp
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objEmpItem.ListComp = new List<CompanyItem>();
            objEmpItem.ListComp.AddRange(objCompany);
            ViewBag.Compid = model.Compid;
            //#endregion
            return View(objEmpItem);
        }
        public ActionResult FillGrid(int Compid)
        {
            string strCompCode = Convert.ToString(Compid);
            EmployeeItem objEmp = new EmployeeItem();
            List<EmployeeItem> objmasterdatalst = new List<EmployeeItem>();
            CompanyService objCompService = new CompanyService();
            objmasterdatalst = objCompService.GetALLGrid(Compid);
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            //// objBranchItem.ListBranch.AddRange(lstBranch);
            objEmp.EmployeeList = new List<EmployeeItem>();
            objEmp.EmployeeList.AddRange(objmasterdatalst);
            return PartialView("_listEmp", objEmp.EmployeeList);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Passport()
        {
            return View();
        }
        public ActionResult Healthcard()
        {
            return View();
        }
        public ActionResult Workpermitt()
        {
            return View();
        }
        public ActionResult Insurance()
        {
            return View();
        }

        public ActionResult PayrollReport()
        {
            PayrollService payrollService = new PayrollService();
            PayrollItem payrollitem = new PayrollItem();
            //List<PayrollItem> payrollItemList = new List<PayrollItem>();
            //payrollItemList = payrollService.GetPayrollDetails1();
            //payrollitem.lstPayroll = new List<PayrollItem>();
            //payrollitem.lstPayroll.AddRange(payrollItemList);

            List<CompanyItem> clsCompany = new List<CompanyItem>();
            clsCompany = payrollService.BindCompany();
            payrollitem.lstCompany = new List<CompanyItem>();
            payrollitem.lstCompany.AddRange(clsCompany);
            BindMonthYear();
            //return View(objPayItem);
            return View(payrollitem);
        }
        [HttpPost]
        public ActionResult PayrollReport(PayrollItem model)
        {
            List<PayrollItem> payrollItemList = new List<PayrollItem>();
            PayrollService payrollService = new PayrollService();
            int? cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (cid == 0)
            {
                cid = model.CompId;
            }
            payrollItemList = payrollService.GetPayrollDetailsReport(model.Month, model.Year,cid);
            PayrollItem payrollitem = new PayrollItem();
            payrollitem.lstPayroll = new List<PayrollItem>();
            payrollitem.lstPayroll.AddRange(payrollItemList);
            BindMonthYear();
            List<CompanyItem> clsCompany = new List<CompanyItem>();
            clsCompany = payrollService.BindCompany();
            payrollitem.lstCompany = new List<CompanyItem>();
            payrollitem.lstCompany.AddRange(clsCompany);
            return View(payrollitem);
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
            drodwnitems.Add(new SelectListItem { Text = "2016", Value = "2016", Selected = true });
            drodwnitems.Add(new SelectListItem { Text = "2017", Value = "2017" });
            drodwnitems.Add(new SelectListItem { Text = "2018", Value = "2018" });
            //Assign the value to ViewBag

            ViewBag.Year = drodwnitems;






        }



        public ActionResult LedgerReport()
        {
            RePaymentService objPayService = new RePaymentService();
            RePaymentItem objPayItem = new RePaymentItem();

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
            return View(objPayItem);
        }

        [HttpPost]
        public ActionResult LedgerReport(RePaymentItem Model)
        {
            RePaymentService objPayService = new RePaymentService();
            RePaymentItem objPayItem = new RePaymentItem();
            int? EmpId = Model.EmpId;
            int? CompId = Model.CompId;
            int? cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (cid == 0)
            {
                cid = Model.CompId;
            }
            List<RePaymentItem> lstRepay = new List<RePaymentItem>();
            lstRepay = objPayService.GetLedgerDetails(EmpId, cid);
            objPayItem.ListRePayment = new List<RePaymentItem>();
            objPayItem.ListRePayment.AddRange(lstRepay);

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
        public ActionResult EmpExpReport()
        {
            EmployeeItem objEmpItem = new EmployeeItem();

            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            ReportService objmethod = new ReportService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstEmp = objmethod.GetData(cid);

            objEmpItem.EmployeeList = new List<EmployeeItem>();
            objEmpItem.EmployeeList.AddRange(lstEmp);

           
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objEmpItem);
        }
    }
}
