using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /EmployeeContact/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int cid, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpContactService objService = new EmpContactService();
            List<EmpContactItem> lstItem = new List<EmpContactItem>();
            EmpContactItem objDoc = new EmpContactItem();
            objDoc = objService.GetById(cid);           
            db.EmployeeContacts.Remove(db.EmployeeContacts.Find(cid));
            db.SaveChanges();
          
            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            List<EmpContactItem> objListContact = new List<EmpContactItem>();
            EmpContactService objEmpService = new EmpContactService();
            objListContact = objEmpService.GetContactDetails(id);
            EmpContactItem objEmpContItem = new EmpContactItem();
            objEmpContItem.ListContactdetails = new List<EmpContactItem>();
            objEmpContItem.ListContactdetails.AddRange(objListContact);
            
            return View(objEmpContItem);

        }

        public ActionResult Create()
        {

            EmpContactService objEmpService = new EmpContactService();
            List<EmpContactItem> objListContact = new List<EmpContactItem>();
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            objListContact = objEmpService.GetContactDetails(Empid);
            ViewBag.EmpId = Empid;
            EmpContactItem objEmpContItem = new EmpContactItem();
            objEmpContItem.ListContactdetails = new List<EmpContactItem>();
            objEmpContItem.ListContactdetails.AddRange(objListContact);
            objListContact = objEmpService.GetDocument(Empid);
            objEmpContItem.ListContactdetailsDoc = new List<EmpContactItem>();
            objEmpContItem.ListContactdetailsDoc.AddRange(objListContact);

            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objEmpService.getCountry();
            objEmpContItem.ListMaster = new List<clsMasterData>();
            objEmpContItem.ListMaster.AddRange(lstMasters);

            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objEmpContItem);
        }

        [HttpPost]
        public ActionResult Create(EmpContactItem model)
        {
            model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            model.EmpId = Empid;

            EmpContactService objEmpService = new EmpContactService();

            objEmpService.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult View(int id, int cid)
        {
            EmpContactService objContactService = new EmpContactService();
            EmpContactItem objContactitem = new EmpContactItem();
            objContactitem = objContactService.GetById(cid);
            Session["Empid"] = objContactitem.EmpId;
            List<EmpContactItem> lstEmpContcts = new List<EmpContactItem>();
            objContactitem.ListContactdetails = new List<EmpContactItem>();
            objContactitem.ListContactdetails.AddRange(lstEmpContcts);

            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objContactService.getCountry();
            objContactitem.ListMaster = new List<clsMasterData>();
            objContactitem.ListMaster.AddRange(lstMasters);

            #endregion
            ViewBag.Cid = cid;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objContactitem);
        }
        [HttpPost]
        public ActionResult Edit(EmpContactItem model)
        {
            model.Id = model.Cid;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            EmpContactService objContact = new EmpContactService();
            objContact.Update(model);
            ViewBag.EmpId = Empid;
            return RedirectToAction("Create", new { id = Empid , @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id, int cid)
        {
            EmpContactService objContactService = new EmpContactService();
            EmpContactItem objContactitem = new EmpContactItem();
            objContactitem = objContactService.GetById(cid);
            Session["Empid"] = objContactitem.EmpId;
            List<EmpContactItem> lstEmpContcts = new List<EmpContactItem>();
            objContactitem.ListContactdetails = new List<EmpContactItem>();
            objContactitem.ListContactdetails.AddRange(lstEmpContcts);

            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objContactService.getCountry();
            objContactitem.ListMaster = new List<clsMasterData>();
            objContactitem.ListMaster.AddRange(lstMasters);

            #endregion
            ViewBag.Cid = cid;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objContactitem);
        }

        public FileResult ShowDocument(string FilePath)
        {
            return File(Server.MapPath("~/UploadDocument/2/") + FilePath, GetMimeType(FilePath));
        }
        private string GetMimeType(string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
