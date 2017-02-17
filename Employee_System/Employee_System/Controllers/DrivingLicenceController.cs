using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel;
using EMSMethods;
using EMSDomain.Model;


namespace Employee_System.Controllers
{
    public class DrivingLicenceController : Controller
    {
        //
        // GET: /EmpDriveLicence/
        
        EmployeeEntities _db = new EmployeeEntities();
        EmpDriveLicenseService objDLicence = new EmpDriveLicenseService();        
        public ActionResult delete(int id, int DLId, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            EmpDriveLicenseService objService = new EmpDriveLicenseService();
            List<EmpDLicenceItem> lstItem = new List<EmpDLicenceItem>();
            EmpDLicenceItem objDoc = new EmpDLicenceItem();
            objDoc = objService.GetById(DLId);
            _db.EmpDrivingLicences.Remove(_db.EmpDrivingLicences.Find(DLId));
            _db.SaveChanges();

            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Index()
        {
            return View();
        }
        // for bind List Grid
        public ActionResult Create()
        {

            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            objDLicence = new EmpDriveLicenseService();
            List<EmpDLicenceItem> listLicence = new List<EmpDLicenceItem>();
            listLicence = objDLicence.LicenceListData(Empid);
            ViewBag.Empid = Empid;
            EmpDLicenceItem objExpitem = new EmpDLicenceItem();
            objExpitem.ListLicence = new List<EmpDLicenceItem>();
            objExpitem.ListLicence.AddRange(listLicence);

            listLicence = objDLicence.GetLicenceDoc(Empid);
            objExpitem.ListLicenceDoc = new List<EmpDLicenceItem>();
            objExpitem.ListLicenceDoc.AddRange(listLicence);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objExpitem);
        }

        [HttpPost]
        public ActionResult Create(EmpDLicenceItem model)
        {
            model.DStatus = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.CreatedBy = Convert.ToString(Empid);
            model.CreatedDate = System.DateTime.Now;
            model.EmpId = Empid;

            objDLicence = new EmpDriveLicenseService();
            objDLicence.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(EmpDLicenceItem model)
        {
            model.id = model.DLId;
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            model.UpdatedBy = Convert.ToString(Empid);
            model.UpdatedDate = System.DateTime.Now;
            EmpDriveLicenseService objDl = new EmpDriveLicenseService();
            objDl.Update(model);
            ViewBag.Empid = Empid;
            ViewBag.menuID = model.Viewbagidformenu;
            return RedirectToAction("Create", new { @id = Empid , @menuId = model.Viewbagidformenu});
        }

        public ActionResult Edit(int id, int DLId)
        {
            EmpDriveLicenseService objDlService = new EmpDriveLicenseService();
            EmpDLicenceItem objDlItem = new EmpDLicenceItem();
            objDlItem = objDlService.GetById(DLId);

            List<EmpDLicenceItem> lstDl = new List<EmpDLicenceItem>();
            objDlItem.ListLicence = new List<EmpDLicenceItem>();
            objDlItem.ListLicence.AddRange(lstDl);
            ViewBag.DLId = DLId;
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objDlItem);
        }
        //[HttpDelete]
        //public ActionResult DeleteDoc(int id)
        //{
        //    //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
        //    EmpDriveLicenseService objDl = new EmpDriveLicenseService();
        //    objDl.DeleteDoc(id);

        //    //return RedirectToAction("Create",new { @id = Empid});
        //    return RedirectToAction("Create");
        //}
        public ActionResult deleteDoc(int? id, int DID, int menuid)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            objDLicence = new EmpDriveLicenseService();
            EmpDLicenceItem objExpitem = new EmpDLicenceItem();
            EMSDomain.ViewModel.DocumentItem objDoc = new EMSDomain.ViewModel.DocumentItem();
            objDoc = objDLicence.byID(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            _db.EmployeeDocuments.Remove(_db.EmployeeDocuments.Find(DID));
            _db.SaveChanges();
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = Empid, @menuId = Request.QueryString["menuId"] });
        }
        //[HttpDelete]
        //public ActionResult Delete(int id)
        //{
        //    Student student = context.Student.FirstOrDefault(s => s.ID.Equals(id));
        //    if (student != null)
        //    {
        //        context.DeleteObject(student);
        //        context.SaveChanges();
        //    }

        //    return RedirectToAction("Index");
        //}

        //public ActionResult Delete(int id)
        //{
        //    EmpDriveLicenseService objDlService = new EmpDriveLicenseService();
        //    EMSDomain.ViewModel.DocumentItem objDlItem = new EMSDomain.ViewModel.DocumentItem();
        //    objDlItem = objDlService.byID(id);
        //    List<EMSDomain.ViewModel.DocumentItem> lstDl = new List<EMSDomain.ViewModel.DocumentItem>();
        //    objDlItem.ListDoc = new List<EMSDomain.ViewModel.DocumentItem>();
        //    objDlItem.ListDoc.AddRange(lstDl);
        //    ViewBag.DocId = id;
        //    return View("DeleteDoc");
        //}
        //[HttpPost]

        //public JsonResult DeleteAJAX(int id)
        //{
            

        //    EmployeeDocument data = (from item in _db.EmployeeDocuments

        //                     where item.Id == id

        //                     select item).SingleOrDefault();

        //    _db.EmployeeDocuments.Remove(data);

        //    _db.SaveChanges();

        //    return Json("Record deleted successfully!");

        //}
    }

}
