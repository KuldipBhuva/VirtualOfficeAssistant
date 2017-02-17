using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class VisaController : Controller
    {
        //
        // GET: /Visa/

        EmpVisaService objVisas = new EmpVisaService();
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int VId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpVisaService objService = new EmpVisaService();
            List<EmpVisaItem> lstItem = new List<EmpVisaItem>();
            EmpVisaItem objDoc = new EmpVisaItem();
            objDoc = objService.GetById(VId);
            db.EmployeeVisas.Remove(db.EmployeeVisas.Find(VId));
            db.SaveChanges();

            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {

            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            objVisas = new EmpVisaService();
            
            ViewBag.Empid = Empid;

            List<EmpVisaItem> listVisa = new List<EmpVisaItem>();
            listVisa = objVisas.VisaListData(Empid);
            EmpVisaItem objlistVisaitem = new EmpVisaItem();
            objlistVisaitem.ListVisa = new List<EmpVisaItem>();
            objlistVisaitem.ListVisa.AddRange(listVisa);

            List<clsMasterData> lstMData = new List<clsMasterData>();
            lstMData = objVisas.GetVisaStatus();
            objlistVisaitem.MasterDetails = new List<clsMasterData>();
            objlistVisaitem.MasterDetails.AddRange(lstMData);

            listVisa = objVisas.GetVisaDoc(Empid);
            objlistVisaitem.ListVisaDoc = new List<EmpVisaItem>();
            objlistVisaitem.ListVisaDoc.AddRange(listVisa);

            #region For Emirates Bind
            List<clsMasterData> lstEmirates = new List<clsMasterData>();
            lstEmirates = objVisas.GetEmiratesStatus();
            objlistVisaitem.EmiratesList = new List<clsMasterData>();
            objlistVisaitem.EmiratesList.AddRange(lstEmirates);
            #endregion
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objVisas.getCountry();
            objlistVisaitem.ListCountry = new List<clsMasterData>();
            objlistVisaitem.ListCountry.AddRange(lstMasters);

            #endregion
            #region ddl company
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();

            EmployeeItem objEmpItem = new EmployeeItem();
            objlistVisaitem.ListComp = new List<CompanyItem>();
            objlistVisaitem.ListComp.AddRange(objCompany);
            #endregion

            #region ddl visa type
            List<clsMasterData> lstMData1 = new List<clsMasterData>();
            lstMData1 = objVisas.GetVisaType();
            objlistVisaitem.ListVisaType = new List<clsMasterData>();
            objlistVisaitem.ListVisaType.AddRange(lstMData1);
            #endregion

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objlistVisaitem);
        }

        [HttpPost]
        public ActionResult Create(EmpVisaItem model)
        {
            model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objVisas = new EmpVisaService();
            model.CreatedDate = System.DateTime.Now;
            objVisas.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpVisaItem model)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objVisas = new EmpVisaService();
            model.CreatedDate = System.DateTime.Now;
            objVisas.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { @id = Empid, @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int VId)
        {
            objVisas = new EmpVisaService();
            EmpVisaItem objVisaItem = new EmpVisaItem();
            objVisaItem = objVisas.GetById(VId);
            //ViewBag.empid = objVisaItem.EmpId;
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpVisaItem> lstPassport = new List<EmpVisaItem>();
            objVisaItem.ListVisa = new List<EmpVisaItem>();
            objVisaItem.ListVisa.AddRange(lstPassport);

            List<clsMasterData> lstMData = new List<clsMasterData>();
            lstMData = objVisas.GetVisaStatus();

            objVisaItem.ListVisa = new List<EmpVisaItem>();
            objVisaItem.ListVisa.AddRange(lstPassport);

            objVisaItem.MasterDetails = new List<clsMasterData>();
            objVisaItem.MasterDetails.AddRange(lstMData);
            ViewBag.Menuid = Request.QueryString["menuId"];
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objVisas.getCountry();
            objVisaItem.ListCountry = new List<clsMasterData>();
            objVisaItem.ListCountry.AddRange(lstMasters);

            #endregion
            #region For Emirates Bind
            List<clsMasterData> lstEmirates = new List<clsMasterData>();
            lstEmirates = objVisas.GetEmiratesStatus();
            objVisaItem.EmiratesList = new List<clsMasterData>();
            objVisaItem.EmiratesList.AddRange(lstEmirates);
            #endregion
            #region ddl company
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();

            EmployeeItem objEmpItem = new EmployeeItem();
            objVisaItem.ListComp = new List<CompanyItem>();
            objVisaItem.ListComp.AddRange(objCompany);
            #endregion

            #region ddl visa type
            List<clsMasterData> lstMData1 = new List<clsMasterData>();
            lstMData1 = objVisas.GetVisaType();
            objVisaItem.ListVisaType = new List<clsMasterData>();
            objVisaItem.ListVisaType.AddRange(lstMData1);
            #endregion
            return View(objVisaItem);
        }
        public ActionResult View(int id, int VId)
        {
            objVisas = new EmpVisaService();
            EmpVisaItem objVisaItem = new EmpVisaItem();
            objVisaItem = objVisas.GetById(VId);
            //ViewBag.empid = objVisaItem.EmpId;
            //Session["Empid"] = objPassItem.EmpId;
            List<EmpVisaItem> lstPassport = new List<EmpVisaItem>();
            objVisaItem.ListVisa = new List<EmpVisaItem>();
            objVisaItem.ListVisa.AddRange(lstPassport);

            List<clsMasterData> lstMData = new List<clsMasterData>();
            lstMData = objVisas.GetVisaStatus();

            objVisaItem.ListVisa = new List<EmpVisaItem>();
            objVisaItem.ListVisa.AddRange(lstPassport);

            objVisaItem.MasterDetails = new List<clsMasterData>();
            objVisaItem.MasterDetails.AddRange(lstMData);
            ViewBag.Menuid = Request.QueryString["menuId"];
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objVisas.getCountry();
            objVisaItem.ListCountry = new List<clsMasterData>();
            objVisaItem.ListCountry.AddRange(lstMasters);

            #endregion
            #region For Emirates Bind
            List<clsMasterData> lstEmirates = new List<clsMasterData>();
            lstEmirates = objVisas.GetEmiratesStatus();
            objVisaItem.EmiratesList = new List<clsMasterData>();
            objVisaItem.EmiratesList.AddRange(lstEmirates);
            #endregion
            #region ddl company
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();

            EmployeeItem objEmpItem = new EmployeeItem();
            objVisaItem.ListComp = new List<CompanyItem>();
            objVisaItem.ListComp.AddRange(objCompany);
            #endregion

            #region ddl visa type
            List<clsMasterData> lstMData1 = new List<clsMasterData>();
            lstMData1 = objVisas.GetVisaType();
            objVisaItem.ListVisaType = new List<clsMasterData>();
            objVisaItem.ListVisaType.AddRange(lstMData1);
            #endregion
            return View(objVisaItem);
        }
        public ActionResult deleteDoc(int? id, int DID, int menuid)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            EmpExperienceService objExpService = new EmpExperienceService();
            EMSDomain.ViewModel.DocumentItem objDoc = new EMSDomain.ViewModel.DocumentItem();
            objDoc = objExpService.byID(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }


            db.EmployeeDocuments.Remove(db.EmployeeDocuments.Find(DID));
            db.SaveChanges();
            ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = Empid, @menuId = Request.QueryString["menuId"] });
        }
    }
}
