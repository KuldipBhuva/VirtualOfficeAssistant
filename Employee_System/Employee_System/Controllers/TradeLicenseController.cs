using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.TradeLicense;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class TradeLicenseController : Controller
    {

        TradeLicenseService objTrend = new TradeLicenseService();
        TradeLicenseItem objTradeItem = new TradeLicenseItem();
        EmployeeEntities db = new EmployeeEntities();
        
        public ActionResult delete(int id, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            TradeLicenseService objTradeService = new TradeLicenseService();
            TradeLicenseItem objDoc = new TradeLicenseItem();
            objDoc = objTradeService.GetById(id);
            db.Trade_License.Remove(db.Trade_License.Find(id));
            db.SaveChanges();
            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult deleteDoc(int id, int DID, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                TradeLicenseService objTradeService = new TradeLicenseService();
                TradeDocumentItem objDoc = new TradeDocumentItem();
                objDoc = objTradeService.getByID(DID);
                string path = objDoc.FileUrl;
                var fullPath = Server.MapPath(path);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }

                db.TradeDocuments.Remove(db.TradeDocuments.Find(DID));
                db.SaveChanges();
                //ViewBag.Empid = Empid;
                ViewBag.Menuid = Request.QueryString["menuId"];
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return RedirectToAction("Edit", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Create()
        {
            objTrend = new TradeLicenseService();
            objTradeItem = new TradeLicenseItem();
            List<clsMasterData> lstMasterDetails = new List<clsMasterData>();

            //Emirates
            lstMasterDetails = objTrend.GetEmirates();
            objTradeItem.lstEmirates = new List<clsMasterData>();
            objTradeItem.lstEmirates.AddRange(lstMasterDetails);            

            //Lic Type 
            lstMasterDetails = objTrend.GetLicenseType();
            objTradeItem.ListLicType = new List<clsMasterData>();
            objTradeItem.ListLicType.AddRange(lstMasterDetails);
            //Partner
            lstMasterDetails = objTrend.GetPartner();
            objTradeItem.lstParter = new List<clsMasterData>();
            objTradeItem.lstParter.AddRange(lstMasterDetails);

            //Company
            List<CompanyItem> lstCmpny = new List<CompanyItem>();
            lstCmpny = objTrend.GetCompany();
            objTradeItem.lstCompany = new List<CompanyItem>();
            objTradeItem.lstCompany.AddRange(lstCmpny);

            //objTradeItem.lstSponsor = new List<CompanyItem>();
            //objTradeItem.lstSponsor.AddRange(lstCmpny);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }

            List<TradeLicenseItem> ListTrade = new List<TradeLicenseItem>();
            ListTrade = objTrend.GetAllList(cid);
            objTradeItem.LstTradeLicense = new List<TradeLicenseItem>();
            objTradeItem.LstTradeLicense.AddRange(ListTrade);

            //ddl Sponsor
            List<SponsorItem> lstSponsor = new List<SponsorItem>();
        
            lstSponsor = objTrend.getSponsorData(cid);
            objTradeItem.ListSponsor = new List<SponsorItem>();
            objTradeItem.ListSponsor.AddRange(lstSponsor);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTradeItem);
        }

        [HttpPost]
        public ActionResult Create(TradeLicenseItem model, HttpPostedFileBase[] files)
        {


            objTradeItem = new TradeLicenseItem();
            objTrend = new TradeLicenseService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
            {
                model.CompID = cid;
            }
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objTrend.Insert(model);
            string s = objTrend.Getid();
            string strtext = "Trade License";
            foreach (HttpPostedFileBase file in files)
            {
                if (file != null)
                {
                    string filename = System.IO.Path.GetFileName(file.FileName);
                    string folderPath = Server.MapPath("~/UploadedDocs/") + strtext;
                    //  obj.EmpId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                    string destinationPath = folderPath;
                    string employeeFolderPath = destinationPath + "/" + s;
                    // create folder if it is not exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                        if (!Directory.Exists(employeeFolderPath))
                        {
                            Directory.CreateDirectory(employeeFolderPath);
                            // create emp id folder of not exist
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(employeeFolderPath))
                        {
                            Directory.CreateDirectory(employeeFolderPath);
                            // create emp id folder of not exist
                        }
                    }
                    destinationPath = employeeFolderPath;
                    /*Saving the file in server folder*/
                    //file.SaveAs(Server.MapPath("~/Images/" + filename));
                    string fileNewName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    file.SaveAs(Path.Combine(destinationPath, fileNewName));
                    objTradeItem.CreatedBy = uid;
                    objTradeItem.CreatedDate = System.DateTime.Now;
                    objTradeItem.Status = "Active";
                    objTradeItem.TradeId = Convert.ToInt32(s);
                    objTradeItem.FileUrl = Path.Combine("/UploadedDocs/" + strtext + "/" + s + "/", fileNewName);
                    objTradeItem.FileName = Path.GetFileNameWithoutExtension(file.FileName);
                    objTrend.Save(objTradeItem);
                    //string filepathtosave = "Images/" + filename;
                    /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                }



            }
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        [HttpPost]
        public ActionResult Edit(TradeLicenseItem model, HttpPostedFileBase[] files)
        {
            objTrend = new TradeLicenseService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdateBy = uid;
            model.CreatedDate = System.DateTime.Now;
            objTrend.Update(model);

            //string s = objTrend.Getid();
            string s = Convert.ToString(model.TID);
            string strtext = "Trade License";
            foreach (HttpPostedFileBase file1 in files)
            {
                if (file1 != null)
                {
                    string filename = System.IO.Path.GetFileName(file1.FileName);
                    string folderPath = Server.MapPath("~/UploadedDocs/") + strtext;
                    //  obj.EmpId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                    string destinationPath = folderPath;
                    string employeeFolderPath = destinationPath + "/" + s;
                    // create folder if it is not exist
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                        if (!Directory.Exists(employeeFolderPath))
                        {
                            Directory.CreateDirectory(employeeFolderPath);
                            // create emp id folder of not exist
                        }
                    }
                    else
                    {
                        if (!Directory.Exists(employeeFolderPath))
                        {
                            Directory.CreateDirectory(employeeFolderPath);
                            // create emp id folder of not exist
                        }
                    }
                    destinationPath = employeeFolderPath;
                    /*Saving the file in server folder*/
                    //file.SaveAs(Server.MapPath("~/Images/" + filename));
                    string fileNewName = Guid.NewGuid() + Path.GetExtension(file1.FileName);

                    file1.SaveAs(Path.Combine(destinationPath, fileNewName));
                    objTradeItem.UpdateBy = uid;
                    objTradeItem.UpdatedDate = System.DateTime.Now;
                    objTradeItem.TradeId = Convert.ToInt32(s);
                    objTradeItem.FileUrl = Path.Combine("/UploadedDocs/" + strtext + "/" + s + "/", fileNewName);
                    objTradeItem.FileName = Path.GetFileNameWithoutExtension(file1.FileName);
                    objTrend.Save(objTradeItem);
                    //string filepathtosave = "Images/" + filename;
                    /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                }


            }
            return RedirectToAction("Edit", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult Edit(int id)
        {
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            objTrend = new TradeLicenseService();
            objTradeItem = new TradeLicenseItem();
            objTradeItem = objTrend.GetById(id);

            List<clsMasterData> lstMasterDetails = new List<clsMasterData>();

            //Emirates
            lstMasterDetails = objTrend.GetEmirates();
            objTradeItem.lstEmirates = new List<clsMasterData>();
            objTradeItem.lstEmirates.AddRange(lstMasterDetails);
            //Lic Type 
            lstMasterDetails = objTrend.GetLicenseType();
            objTradeItem.ListLicType = new List<clsMasterData>();
            objTradeItem.ListLicType.AddRange(lstMasterDetails);
            //Partner
            lstMasterDetails = objTrend.GetPartner();
            objTradeItem.lstParter = new List<clsMasterData>();
            objTradeItem.lstParter.AddRange(lstMasterDetails);

            //Company
            List<CompanyItem> lstCmpny = new List<CompanyItem>();
            lstCmpny = objTrend.GetCompany();
            objTradeItem.lstCompany = new List<CompanyItem>();
            objTradeItem.lstCompany.AddRange(lstCmpny);

            List<TradeLicenseItem> ListTrade = new List<TradeLicenseItem>();
            ListTrade = objTrend.ListDocTrend(id);
            objTradeItem.LstTradeLicense = new List<TradeLicenseItem>();
            objTradeItem.LstTradeLicense.AddRange(ListTrade);
            //ddl Sponsor
            List<SponsorItem> lstSponsor = new List<SponsorItem>();
            lstSponsor = objTrend.getSponsorData(cid);
            objTradeItem.ListSponsor = new List<SponsorItem>();
            objTradeItem.ListSponsor.AddRange(lstSponsor);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTradeItem);
        }
        public ActionResult View(int id)
        {
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            objTrend = new TradeLicenseService();
            objTradeItem = new TradeLicenseItem();
            objTradeItem = objTrend.GetById(id);

            List<clsMasterData> lstMasterDetails = new List<clsMasterData>();

            //Emirates
            lstMasterDetails = objTrend.GetEmirates();
            objTradeItem.lstEmirates = new List<clsMasterData>();
            objTradeItem.lstEmirates.AddRange(lstMasterDetails);
            //Lic Type 
            lstMasterDetails = objTrend.GetLicenseType();
            objTradeItem.ListLicType = new List<clsMasterData>();
            objTradeItem.ListLicType.AddRange(lstMasterDetails);
            //Partner
            lstMasterDetails = objTrend.GetPartner();
            objTradeItem.lstParter = new List<clsMasterData>();
            objTradeItem.lstParter.AddRange(lstMasterDetails);

            //Company
            List<CompanyItem> lstCmpny = new List<CompanyItem>();
            lstCmpny = objTrend.GetCompany();
            objTradeItem.lstCompany = new List<CompanyItem>();
            objTradeItem.lstCompany.AddRange(lstCmpny);

            List<TradeLicenseItem> ListTrade = new List<TradeLicenseItem>();
            ListTrade = objTrend.ListDocTrend(id);
            objTradeItem.LstTradeLicense = new List<TradeLicenseItem>();
            objTradeItem.LstTradeLicense.AddRange(ListTrade);
            //ddl Sponsor
            List<SponsorItem> lstSponsor = new List<SponsorItem>();
            lstSponsor = objTrend.getSponsorData(cid);
            objTradeItem.ListSponsor = new List<SponsorItem>();
            objTradeItem.ListSponsor.AddRange(lstSponsor);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objTradeItem);
        }

    }
}
