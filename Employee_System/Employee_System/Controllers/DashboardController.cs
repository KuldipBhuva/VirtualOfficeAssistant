using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Insurance;
using EMSDomain.ViewModel.Tenancy;
using EMSDomain.ViewModel.TradeLicense;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            

            // load menu
            //licence
            List<EmpHealthItem> lstDlicence = new List<EmpHealthItem>();
            EmpAllItem objEmp = new EmpAllItem();
            DashboardService objDashboard = new DashboardService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstDlicence = objDashboard.GetLicence(cid);

            objEmp.EmpLicenceItem = new List<EmpHealthItem>();
            objEmp.EmpLicenceItem.AddRange(lstDlicence);

            //Licence30
            List<EmpHealthItem> lstDlicence30 = new List<EmpHealthItem>();
            //EmpAllItem objEmp30 = new EmpAllItem();
            DashboardService objDashboard30 = new DashboardService();
            lstDlicence30 = objDashboard.GetLicence30(cid);

            objEmp.EmpLicenceItem30 = new List<EmpHealthItem>();
            objEmp.EmpLicenceItem30.AddRange(lstDlicence30);

            //Passport
            List<EmpPassportItem> lstPassport = new List<EmpPassportItem>();

            DashboardService objDash = new DashboardService();
            lstPassport = objDash.GetPassport(cid);

            objEmp.EmppassItem = new List<EmpPassportItem>();
            objEmp.EmppassItem.AddRange(lstPassport);

            //Passport30
            List<EmpPassportItem> lstPassport30 = new List<EmpPassportItem>();

            DashboardService objDash30 = new DashboardService();
            lstPassport30 = objDash30.GetPassport30(cid);

            objEmp.EmppassItem30 = new List<EmpPassportItem>();
            objEmp.EmppassItem30.AddRange(lstPassport30);

            //HealthCard
            List<EmpHealthItem> lstHealthCard = new List<EmpHealthItem>();

            DashboardService objDashHC = new DashboardService();
            lstHealthCard = objDashHC.GetHealthCard(cid);

            objEmp.EmpHealthItem = new List<EmpHealthItem>();
            objEmp.EmpHealthItem.AddRange(lstHealthCard);

            //HealthCard30
            List<EmpHealthItem> lstHealthCard30 = new List<EmpHealthItem>();

            DashboardService objDashHC30 = new DashboardService();
            lstHealthCard30 = objDashHC30.GetHealthCard30(cid);

            objEmp.EmpHealthItem30 = new List<EmpHealthItem>();
            objEmp.EmpHealthItem30.AddRange(lstHealthCard30);

            //Work Permit(Labour card)
            List<EmpHealthItem> lstLabourCard = new List<EmpHealthItem>();

            DashboardService objLCDB = new DashboardService();
            lstLabourCard = objLCDB.GetLabourCard(cid);

            objEmp.EmpLabourItem = new List<EmpHealthItem>();
            objEmp.EmpLabourItem.AddRange(lstLabourCard);

            //Work Permit(Labour card)30
            List<EmpHealthItem> lstLabourCard30 = new List<EmpHealthItem>();

            DashboardService objLCDB30 = new DashboardService();
            lstLabourCard30 = objLCDB30.GetLabourCard30(cid);

            objEmp.EmpLabourItem30 = new List<EmpHealthItem>();
            objEmp.EmpLabourItem30.AddRange(lstLabourCard30);

            //Insurance
            List<EmpInsuranceItem> lstInsu = new List<EmpInsuranceItem>();

            DashboardService objInsuDB = new DashboardService();
            lstInsu = objInsuDB.getInsu(cid);

            objEmp.EmpInsuItem = new List<EmpInsuranceItem>();
            objEmp.EmpInsuItem.AddRange(lstInsu);

            //Insurance30
            List<EmpInsuranceItem> lstInsu30 = new List<EmpInsuranceItem>();

            DashboardService objInsuDB30 = new DashboardService();
            lstInsu30 = objInsuDB30.getInsu30(cid);

            objEmp.EmpInsuItem30 = new List<EmpInsuranceItem>();
            objEmp.EmpInsuItem30.AddRange(lstInsu30);

            //VehicleRegistration
            List<VehicleItem> lstVehicle = new List<VehicleItem>();

            DashboardService objVehicleDB = new DashboardService();
            lstVehicle = objVehicleDB.getVehicle(cid);

            objEmp.ListVehicle = new List<VehicleItem>();
            objEmp.ListVehicle.AddRange(lstVehicle);
            //VehicleRegistration30
            List<VehicleItem> lstVehicle30 = new List<VehicleItem>();

            DashboardService objVehicleDB30 = new DashboardService();
            lstVehicle30 = objVehicleDB30.getVehicle30(cid);

            objEmp.ListVehicle30 = new List<VehicleItem>();
            objEmp.ListVehicle30.AddRange(lstVehicle30);

            //Increments
            List<EmpIncrementItem> lstIncrements = new List<EmpIncrementItem>();

            DashboardService objIncrementsDB = new DashboardService();
            lstIncrements = objIncrementsDB.getIncrement(cid);

            objEmp.ListIncrement = new List<EmpIncrementItem>();
            objEmp.ListIncrement.AddRange(lstIncrements);

            //Visa
            List<EmpVisaItem> lstVisa = new List<EmpVisaItem>();

            DashboardService objVisaDB = new DashboardService();
            lstVisa = objVisaDB.getVisa(cid);

            objEmp.ListVisa = new List<EmpVisaItem>();
            objEmp.ListVisa.AddRange(lstVisa);

            //Visa30
            List<EmpVisaItem> lstVisa30 = new List<EmpVisaItem>();

            DashboardService objVisaDB30 = new DashboardService();
            lstVisa30 = objVisaDB30.getVisa30(cid);

            objEmp.ListVisa30 = new List<EmpVisaItem>();
            objEmp.ListVisa30.AddRange(lstVisa30);

            //Emirates
            List<EmpHealthItem> lstEmirates = new List<EmpHealthItem>();

            DashboardService objEmiratesDB = new DashboardService();
            lstEmirates = objEmiratesDB.getEmirates(cid);

            objEmp.ListEmirates = new List<EmpHealthItem>();
            objEmp.ListEmirates.AddRange(lstEmirates);

            //Emirates30
            List<EmpHealthItem> lstEmirates30 = new List<EmpHealthItem>();

            DashboardService objEmiratesDB30 = new DashboardService();
            lstEmirates30 = objVisaDB.getEmirates30(cid);

            objEmp.ListEmirates30 = new List<EmpHealthItem>();
            objEmp.ListEmirates30.AddRange(lstEmirates30);

            //CompDoc
            List<TradeLicenseItem> lstCompDoc = new List<TradeLicenseItem>();

            DashboardService objService = new DashboardService();
            lstCompDoc = objService.getCompDoc(cid);

            objEmp.ListCompDoc = new List<TradeLicenseItem>();
            objEmp.ListCompDoc.AddRange(lstCompDoc);

            //CompDoc30
            List<TradeLicenseItem> lstCompDoc30 = new List<TradeLicenseItem>();


            lstCompDoc30 = objService.getCompDoc30(cid);

            objEmp.ListCompDoc30 = new List<TradeLicenseItem>();
            objEmp.ListCompDoc30.AddRange(lstCompDoc30);

            //Tenancy
            List<TenancyItem> lstTenancy = new List<TenancyItem>();


            lstTenancy = objService.getTenancy(cid);

            objEmp.ListTenancy = new List<TenancyItem>();
            objEmp.ListTenancy.AddRange(lstTenancy);

            //Tenancy30
            List<TenancyItem> lstTenancy30 = new List<TenancyItem>();


            lstTenancy30 = objService.getTenancy30(cid);

            objEmp.ListTenancy30 = new List<TenancyItem>();
            objEmp.ListTenancy30.AddRange(lstTenancy30);

            //Missing Info
            //EmpDOB
            List<EmployeeItem> lstDOB = new List<EmployeeItem>();

            DashboardService objDOB = new DashboardService();
            lstDOB = objDOB.getEmpDOB(cid);

            objEmp.ListEmpDOB = new List<EmployeeItem>();
            objEmp.ListEmpDOB.AddRange(lstDOB);

            //EmpBasicSalary
            List<EmployeeItem> lstBS = new List<EmployeeItem>();

            DashboardService objBS = new DashboardService();
            lstBS = objBS.getEmpBS(cid);

            objEmp.ListEmpBS = new List<EmployeeItem>();
            objEmp.ListEmpBS.AddRange(lstBS);

            //EmpPassport
            List<EmpPassportItem> lstPass = new List<EmpPassportItem>();

            DashboardService objPass = new DashboardService();
            lstPass = objPass.getEmpPassport(cid);

            objEmp.ListPass = new List<EmpPassportItem>();
            objEmp.ListPass.AddRange(lstPass);

            //EmpLabour
            List<EmployeeItem> lstLabour = new List<EmployeeItem>();

            DashboardService objLabour = new DashboardService();
            lstLabour = objLabour.getLabour(cid);

            objEmp.ListEmpLabour = new List<EmployeeItem>();
            objEmp.ListEmpLabour.AddRange(lstLabour);

            //EmpVisa
            List<EmpVisaItem> lstEmpVisa = new List<EmpVisaItem>();

            DashboardService objVisa = new DashboardService();
            lstEmpVisa = objVisa.getEmpVisa(cid);

            objEmp.ListEmpVisa = new List<EmpVisaItem>();
            objEmp.ListEmpVisa.AddRange(lstEmpVisa);

            //EmpEID
            List<EmployeeItem> lstEID = new List<EmployeeItem>();

            DashboardService objEID = new DashboardService();
            lstEID = objEID.getEmpEID(cid);

            objEmp.ListEmpEID = new List<EmployeeItem>();
            objEmp.ListEmpEID.AddRange(lstEID);
            return View(objEmp);

        }
        public ActionResult GetCalendarData()
        {
            List<AppointmentsItem> lstAppData = new List<AppointmentsItem>();
            AppointmentsItem objAppoint = new AppointmentsItem();
            AppointmentService objAppService = new AppointmentService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstAppData = objAppService.getAppointmentData(cid);

            var eventList = from e in lstAppData
                            select new
                            {
                                id = e.ID,
                                title = e.Subject,
                                start = e.StDt.Value.ToString("yyyy-MM-dd") + " " + e.StTime,
                                end = e.EndDt.Value.ToString("yyyy-MM-dd") + " " + e.EndTime
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Menu()
        {
            return PartialView("_Menu");
        }
        //public ActionResult Schedule()
        //{
        //    var scheduler = new DHXScheduler(this);
        //    scheduler.LoadData = true;
        //    scheduler.EnableDataprocessor = true;
        //    return View(scheduler);
        //}
        //public ActionResult Data()
        //{//events for loading to scheduler
        //    return new SchedulerAjaxData(new SampleDataContext().Events);
        //}
        //public ActionResult Save(Event updatedEvent, FormCollection formData)
        //{
        //    var action = new DataAction(formData);
        //    var context = new SampleDataContext();

        //    try
        //    {
        //        switch (action.Type)
        //        {
        //            case DataActionTypes.Insert: // your Insert logic
        //                context.Events.InsertOnSubmit(updatedEvent);
        //                break;
        //            case DataActionTypes.Delete: // your Delete logic
        //                updatedEvent = context.Events.SingleOrDefault(ev => ev.id == updatedEvent.id);
        //                context.Events.DeleteOnSubmit(updatedEvent);
        //                break;
        //            default:// "update" // your Update logic
        //                updatedEvent = context.Events.SingleOrDefault(
        //                ev => ev.id == updatedEvent.id);
        //                UpdateModel(updatedEvent);
        //                break;
        //        }
        //        context.SubmitChanges();
        //        action.TargetId = updatedEvent.id;
        //    }
        //    catch (Exception a)
        //    {
        //        action.Type = DataActionTypes.Error;
        //    }
        //    return (new AjaxSaveResponse(action));
        //}
    }
}
