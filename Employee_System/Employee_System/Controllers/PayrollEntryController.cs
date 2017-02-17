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
    public class PayrollEntryController : Controller
    {
        //
        // GET: /PayrollEntry/

        PayrollService objPayService = new PayrollService();
        PayrollItem objPayItem = new PayrollItem();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            objPayService = new PayrollService();
            objPayItem = new PayrollItem();

            List<CompanyItem> clsCompany = new List<CompanyItem>();
            clsCompany = objPayService.BindCompany();
            objPayItem.lstCompany = new List<CompanyItem>();
            objPayItem.lstCompany.AddRange(clsCompany);
            BindMonthYear();

            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<PayrollItem> lstPayoll = new List<PayrollItem>();
            lstPayoll = objPayService.GridPayroll(cid);
            objPayItem.lstPayroll = new List<PayrollItem>();

            objPayItem.lstPayroll.AddRange(lstPayoll);
            return View(objPayItem);
        }

        [HttpPost]
        public ActionResult Create(IEnumerable<EmployeeItem> lstEmployee, PayrollItem payroll, IEnumerable<PayrollItem> lstPayroll)
        {
            string strMsg = "";
            if (TempData["Msg"] != "")
            {
                strMsg = TempData["Msg"].ToString();
            }

            objPayService = new PayrollService();
            List<CompanyItem> clsCompany = new List<CompanyItem>();
            clsCompany = objPayService.BindCompany();
            objPayItem.lstCompany = new List<CompanyItem>();
            objPayItem.lstCompany.AddRange(clsCompany);
            BindMonthYear();
            var lstEmp = lstEmployee.ToList();
            for (int i = 0; i < lstEmployee.ToList().Count; i++)
            {
                objPayItem = new PayrollItem();

                var ID = lstEmp[i].id;

                objPayItem.Month = Convert.ToInt32(payroll.Month);
                objPayItem.Year = Convert.ToInt32(payroll.Year);
                objPayItem.CompId = Convert.ToInt32(payroll.CompId);
                objPayItem.Empid = lstEmp[i].id;
                objPayItem.OT = lstEmp[i].OT;
                objPayItem.DA = lstEmp[i].DA;
                if (payroll.Month == 2)
                {
                    if ((payroll.Year % 4 == 0 && payroll.Year % 100 != 0) || (payroll.Year % 400 == 0))
                    {
                        objPayItem.TotalDays = 29;

                        objPayItem.Days = lstEmp[i].Days;
                    }
                    else
                    {
                        objPayItem.TotalDays = 28;
                        objPayItem.Days = lstEmp[i].Days;
                    }
                }
                if (payroll.Month == 1 || payroll.Month == 3 || payroll.Month == 5 || payroll.Month == 7 || payroll.Month == 8 || payroll.Month == 10 || payroll.Month == 12)
                {
                    objPayItem.TotalDays = 31;
                    if (lstEmp[i].Days > 31)
                    {
                        objPayItem.lstEmployee = new List<EmployeeItem>();
                        objPayItem.lstEmployee.AddRange(lstEmp);
                        ViewBag.ErrorMsg = "Data Not Successfully Added..Check Proper Days";
                        ModelState.AddModelError(string.Empty, "The item cannot be removed");
                        return View();
                    }
                    else
                    {
                        objPayItem.Days = lstEmp[i].Days;
                    }

                }
                if (payroll.Month == 4 || payroll.Month == 6 || payroll.Month == 9 || payroll.Month == 11)
                {
                    objPayItem.TotalDays = 30;
                    if (lstEmp[i].Days > 30)
                    {
                        ViewBag.ErrorMsg = "Data Not Successfully Added..Check Proper Days";
                        return View(objPayItem);
                    }
                    else
                    {
                        objPayItem.Days = lstEmp[i].Days;
                    }

                }
                List<CompanyItem> clsCompany1 = new List<CompanyItem>();
                clsCompany1 = objPayService.BindCompany();
                objPayItem.lstCompany = new List<CompanyItem>();
                objPayItem.lstCompany.AddRange(clsCompany1);
                if (strMsg.ToString() == "")
                {
                    objPayService.InsertPayroll(objPayItem);
                }
                else
                {
                    objPayService.UpdatePayroll(objPayItem);
                }
            }
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<PayrollItem> lstPayoll = new List<PayrollItem>();
            lstPayoll = objPayService.GridPayroll(cid);
            objPayItem.lstPayroll = new List<PayrollItem>();
            objPayItem.lstPayroll.AddRange(lstPayoll);
            ModelState.Clear();
            return View(objPayItem);
        }

        [HttpPost]
        public ActionResult Edit(IEnumerable<EmployeeItem> lstEmployee, PayrollItem payroll, IEnumerable<PayrollItem> lstPayroll)
        {
            string strMsg = TempData["Msg"].ToString();
            objPayService = new PayrollService();
            List<CompanyItem> clsCompany = new List<CompanyItem>();
            clsCompany = objPayService.BindCompany();
            objPayItem.lstCompany = new List<CompanyItem>();
            objPayItem.lstCompany.AddRange(clsCompany);
            BindMonthYear();
            var lstEmp = lstEmployee.ToList();
            for (int i = 0; i < lstEmployee.ToList().Count; i++)
            {
                objPayItem = new PayrollItem();

                var ID = lstEmp[i].id;

                objPayItem.Month = Convert.ToInt32(payroll.Month);
                objPayItem.Year = Convert.ToInt32(payroll.Year);
                objPayItem.CompId = Convert.ToInt32(payroll.CompId);
                objPayItem.Empid = lstEmp[i].id;
                objPayItem.OT = lstEmp[i].OT;
                objPayItem.DA = lstEmp[i].DA;
                if (payroll.Month == 2)
                {
                    if ((payroll.Year % 4 == 0 && payroll.Year % 100 != 0) || (payroll.Year % 400 == 0))
                    {
                        objPayItem.TotalDays = 29;
                        objPayItem.Days = lstEmp[i].Days;
                    }
                    else
                    {
                        objPayItem.TotalDays = 28;
                        objPayItem.Days = lstEmp[i].Days;
                    }
                }
                if (payroll.Month == 1 || payroll.Month == 3 || payroll.Month == 5 || payroll.Month == 7 || payroll.Month == 8 || payroll.Month == 10 || payroll.Month == 12)
                {
                    objPayItem.TotalDays = 31;
                    if (lstEmp[i].Days > 31)
                    {
                        objPayItem.lstEmployee = new List<EmployeeItem>();
                        objPayItem.lstEmployee.AddRange(lstEmp);
                        ViewBag.ErrorMsg = "Data Not Successfully Added..Check Proper Days";
                        ModelState.AddModelError(string.Empty, "The item cannot be removed");
                        return View();
                    }
                    else
                    {
                        objPayItem.Days = lstEmp[i].Days;
                    }

                }
                if (payroll.Month == 4 || payroll.Month == 6 || payroll.Month == 9 || payroll.Month == 11)
                {
                    objPayItem.TotalDays = 30;
                    if (lstEmp[i].Days > 30)
                    {
                        ViewBag.ErrorMsg = "Data Not Successfully Added..Check Proper Days";
                        return View(objPayItem);
                    }
                    else
                    {
                        objPayItem.Days = lstEmp[i].Days;
                    }

                }
                List<CompanyItem> clsCompany1 = new List<CompanyItem>();
                clsCompany1 = objPayService.BindCompany();
                objPayItem.lstCompany = new List<CompanyItem>();
                objPayItem.lstCompany.AddRange(clsCompany1);
                objPayService.UpdatePayroll(objPayItem);
            }
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int CompId, int Month, int Year)
        {
            objPayService = new PayrollService();
            objPayItem = new PayrollItem();
            List<PayrollItem> PayItem = new List<PayrollItem>();
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();

            PayItem = objPayService.GetPayrollDetails1(Month, Year, CompId);

            if (PayItem.Count != 0)
            {
                objPayItem.lstPayroll = new List<PayrollItem>();
                TempData["Msg"] = "EmployeeExists";
                objPayItem.lstPayroll.AddRange(PayItem);
                ViewBag.MonthList = Month;
                ViewBag.YearList = Year;

            }

            List<CompanyItem> clsCompany = new List<CompanyItem>();
            clsCompany = objPayService.BindCompany();
            objPayItem.lstCompany = new List<CompanyItem>();
            objPayItem.lstCompany.AddRange(clsCompany);
            BindMonthYear();

            // return View("_List", objPayItem);
            return View(objPayItem);

        }




        public ActionResult BindEmp(int CompId, int Month, int Year)
        {

            objPayService = new PayrollService();
            objPayItem = new PayrollItem();
            List<PayrollItem> PayItem = new List<PayrollItem>();
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            PayItem = objPayService.GetPayrollDetails1(Month, Year, CompId);

            if (PayItem.Count != 0)
            {
                objPayItem.lstPayroll = new List<PayrollItem>();
                TempData["Msg"] = "EmployeeExists";
                objPayItem.lstPayroll.AddRange(PayItem);
                ViewBag.MonthList = Month;
                ViewBag.YearList = Year;

                //lstEmp = objPayService.BindEmployee(CompId);
                //objPayItem.lstEmployee = new List<EmployeeItem>();
                //objPayItem.lstEmployee.AddRange(lstEmp);
            }
            else
            {
                TempData["Msg"] = "";
                if (CompId != 0)
                {

                    lstEmp = objPayService.BindEmployee(CompId);
                    objPayItem.lstEmployee = new List<EmployeeItem>();

                    objPayItem.lstEmployee.AddRange(lstEmp);
                }

                PayItem = objPayService.BindPayroll(CompId);
                objPayItem.lstPayroll = new List<PayrollItem>();
                objPayItem.lstPayroll.AddRange(PayItem);
                ViewBag.MonthList = Month;
                ViewBag.YearList = Year;
            }
            return PartialView("_List", objPayItem);

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
    }
}
