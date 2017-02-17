using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSMethods;
using System.Security;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.TradeLicense;


namespace Employee_System.Controllers
{
    public class DocumentController : Controller
    {
        //
        // GET: /Document/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult CompAllDoc()
        {

            DocumentItem obj = new DocumentItem();
            DocumentService objService = new DocumentService();
            List<DocumentItem> lstItem = new List<DocumentItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstItem = objService.getCompAllTradeDoc(cid);
            obj.ListTradeDoc = new List<DocumentItem>();
            obj.ListTradeDoc.AddRange(lstItem);

            lstItem = objService.getCompAllTenancyDoc(cid);
            obj.ListTenancyDoc = new List<DocumentItem>();
            obj.ListTenancyDoc.AddRange(lstItem);

            lstItem = objService.getCompAllVehicleDoc(cid);
            obj.ListVehicleDoc = new List<DocumentItem>();
            obj.ListVehicleDoc.AddRange(lstItem);
            //#region Bind DropDown comp
            
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            obj.ListCompany = new List<CompanyItem>();
            obj.ListCompany.AddRange(objCompany);
            //#endregion
            return View(obj);
        }
        public ActionResult DownloadTradeDoc(FormCollection All)
        {
            DocumentService objService = new DocumentService();
            DocumentItem obj = new DocumentItem();
            //int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<DocumentItem> lstDoc = new List<DocumentItem>();
            lstDoc = objService.getCompAllTradeDoc(cid);
            obj.ListDoc = new List<DocumentItem>();
            obj.ListDoc.AddRange(lstDoc);
            //string fullpath = obj.FileUrl;

            //string fileName = obj.FileName;

            //string[] files = Directory.GetDirectories(
            //        Server.MapPath(fullpath));
            List<string> Downloads = new List<string>();
            Response.ContentType = "application/zip";
            ZipFile zip;
            byte[] zipContent = null;
            string strfilePath = "";
            using (zip = new ZipFile())
            {
                zip.ParallelDeflateThreshold = -1;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                foreach (var file in lstDoc)
                {
                    //Downloads.Add(Path.GetFileName(Convert.ToString(file)));
                    // the following entry will be inserted at the root in the archive.
                    strfilePath = Server.MapPath("~" + file.FileUrl);

                    zip.AddFile(strfilePath, string.Empty);
                }

                using (var ms = new MemoryStream())
                {

                    zip.Save(ms);
                    zipContent = ms.ToArray();
                }
            }

            //string name = lstDoc[0].CompDetails.CompName;
            Response.AppendHeader("Content-Disposition", string.Concat("inline; filename=\"", "TradeDocuments.zip", "\""));
            return File(zipContent, "zip");
        }
        public ActionResult DownloadTenancyDoc(FormCollection All)
        {
            DocumentService objService = new DocumentService();
            DocumentItem obj = new DocumentItem();
            //int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<DocumentItem> lstDoc = new List<DocumentItem>();
            lstDoc = objService.getCompAllTenancyDoc(cid);
            obj.ListDoc = new List<DocumentItem>();
            obj.ListDoc.AddRange(lstDoc);
            //string fullpath = obj.FileUrl;

            //string fileName = obj.FileName;

            //string[] files = Directory.GetDirectories(
            //        Server.MapPath(fullpath));
            List<string> Downloads = new List<string>();
            Response.ContentType = "application/zip";
            ZipFile zip;
            byte[] zipContent = null;
            string strfilePath = "";
            using (zip = new ZipFile())
            {
                zip.ParallelDeflateThreshold = -1;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                foreach (var file in lstDoc)
                {
                    //Downloads.Add(Path.GetFileName(Convert.ToString(file)));
                    // the following entry will be inserted at the root in the archive.
                    strfilePath = Server.MapPath("~" + file.FileUrl);

                    zip.AddFile(strfilePath, string.Empty);
                }

                using (var ms = new MemoryStream())
                {

                    zip.Save(ms);
                    zipContent = ms.ToArray();
                }
            }

            //string name = lstDoc[0].CompDetails.CompName;
            Response.AppendHeader("Content-Disposition", string.Concat("inline; filename=\"", "TenancyDocuments.zip", "\""));
            return File(zipContent, "zip");
        }
        public ActionResult DownloadVehicleDoc(FormCollection All)
        {
            DocumentService objService = new DocumentService();
            DocumentItem obj = new DocumentItem();
            //int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<DocumentItem> lstDoc = new List<DocumentItem>();
            lstDoc = objService.getCompAllVehicleDoc(cid);
            obj.ListDoc = new List<DocumentItem>();
            obj.ListDoc.AddRange(lstDoc);
            //string fullpath = obj.FileUrl;

            //string fileName = obj.FileName;

            //string[] files = Directory.GetDirectories(
            //        Server.MapPath(fullpath));
            List<string> Downloads = new List<string>();
            Response.ContentType = "application/zip";
            ZipFile zip;
            byte[] zipContent = null;
            string strfilePath = "";
            using (zip = new ZipFile())
            {
                zip.ParallelDeflateThreshold = -1;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                foreach (var file in lstDoc)
                {
                    //Downloads.Add(Path.GetFileName(Convert.ToString(file)));
                    // the following entry will be inserted at the root in the archive.
                    strfilePath = Server.MapPath("~" + file.FileUrl);

                    zip.AddFile(strfilePath, string.Empty);
                }

                using (var ms = new MemoryStream())
                {

                    zip.Save(ms);
                    zipContent = ms.ToArray();
                }
            }

            //string name = lstDoc[0].CompDetails.CompName;
            Response.AppendHeader("Content-Disposition", string.Concat("inline; filename=\"", "VehicleDocuments.zip", "\""));
            return File(zipContent, "zip");
        }
        public ActionResult getTradeDoc(int Compid)
        {
            DocumentItem obj = new DocumentItem();
            DocumentService objService = new DocumentService();
            List<DocumentItem> lstItem = new List<DocumentItem>();
            lstItem = objService.getTradeDocByComp(Compid);
            obj.ListTenancyDoc = new List<DocumentItem>();
            obj.ListTenancyDoc.AddRange(lstItem);
            return PartialView("_TradeDoc", obj.ListTenancyDoc);
        }
        public ActionResult getTenancyDoc(int Compid)
        {
            DocumentItem obj = new DocumentItem();
            DocumentService objService = new DocumentService();
            List<DocumentItem> lstItem = new List<DocumentItem>();
            lstItem = objService.getTenancyDocByComp(Compid);
            obj.ListTradeDoc = new List<DocumentItem>();
            obj.ListTradeDoc.AddRange(lstItem);
            return PartialView("_TenancyDoc", obj.ListTradeDoc);
        }
        public ActionResult getVehicleDoc(int Compid)
        {
            DocumentItem obj = new DocumentItem();
            DocumentService objService = new DocumentService();
            List<DocumentItem> lstItem = new List<DocumentItem>();
            lstItem = objService.getVehicleDocByComp(Compid);
            obj.ListVehicleDoc = new List<DocumentItem>();
            obj.ListVehicleDoc.AddRange(lstItem);
            return PartialView("_VehicleDoc", obj.ListVehicleDoc);
        }
        public ActionResult delete(int id, int DID, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            DocumentService objService = new DocumentService();
            List<EMSDomain.ViewModel.DocumentItem> lstDoc = new List<EMSDomain.ViewModel.DocumentItem>();
            DocumentItem objDoc = new DocumentItem();
            objDoc = objService.GetById(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            db.EmployeeDocuments.Remove(db.EmployeeDocuments.Find(DID));
            db.SaveChanges();
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Index", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Index()
        {
            DocumentItem obj = new DocumentItem();
            DocumentService objService = new DocumentService();
            List<clsMasterData> lstMaster = new List<clsMasterData>();
            lstMaster = objService.GetDoc();
            obj.ListDocumentMaster = new List<clsMasterData>();
            obj.ListDocumentMaster.AddRange(lstMaster);

            ViewBag.Menuid = Request.QueryString["menuId"];
            int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            List<DocumentItem> lstDoc = new List<DocumentItem>();
            lstDoc = objService.getAllDocByEmpID(id);
            obj.ListDoc = new List<DocumentItem>();
            obj.ListDoc.AddRange(lstDoc);

            //DocumentItem objDoc = new DocumentItem();
            //objDoc = objService.GetById(DID);
            //string path = objDoc.FileUrl;
            //var fullPath = Server.MapPath(path);
            //string[] files = Directory.GetFiles(
            //       Server.MapPath(fullPath));
            //List<string> downloads = new List<string>();
            //foreach (string file in files)
            //{
            //    downloads.Add(Path.GetFileName(file));
            //}

            return View(obj);

        }

        /// <summary>
        /// Post method for uploading files
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase[] files, DocumentItem model)
        {

            try
            {
                string uid = null;
                if (Session["UserId"] != null)
                {
                    uid = Session["UserId"].ToString();
                }
                DocumentItem obj = new DocumentItem();
                DocumentService objService = new DocumentService();
                /*Lopp for multiple files*/
                var categoryText = Request.Form["hdDocText"];
                var categoryId = Request.Form["DocCategoryId"];

                foreach (HttpPostedFileBase file in files)
                {
                    /*Geting the file name*/
                    string filename = System.IO.Path.GetFileName(file.FileName);
                    string folderPath = Server.MapPath("~/UploadedDocs/") + categoryText;
                    obj.EmpId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                    string destinationPath = folderPath;
                    string employeeFolderPath = destinationPath + "/" + obj.EmpId;
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
                    obj.CreatedBy = uid;
                    obj.CreatedDate = System.DateTime.Now;
                    obj.DocCategoryId = Convert.ToInt32(categoryId);
                    obj.FileUrl = Path.Combine("/UploadedDocs/" + categoryText + "/" + obj.EmpId + "/", fileNewName);
                    obj.FileName = Path.GetFileNameWithoutExtension(file.FileName);
                    objService.Save(obj);
                    //string filepathtosave = "Images/" + filename;
                    /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                }

                List<clsMasterData> lstMData = new List<clsMasterData>();
                lstMData = objService.GetDocuments();
                //GetDocuments()

                ViewBag.EmpID = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                ViewBag.Message = "File Uploaded successfully.";
            }
            catch
            {
                ViewBag.Message = "Error while uploading the files.";
            }
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            ViewBag.Empid = Empid;
            return RedirectToAction("Index", new { id = Empid, @menuId = model.Viewbagidformenu });
        }
        [assembly: AllowPartiallyTrustedCallers]
        public ActionResult Download(FormCollection All)
        {
            DocumentService objService = new DocumentService();
            DocumentItem obj = new DocumentItem();
            int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            List<DocumentItem> lstDoc = new List<DocumentItem>();
            lstDoc = objService.getAllDocByEmpID(id);
            obj.ListDoc = new List<DocumentItem>();
            obj.ListDoc.AddRange(lstDoc);
            //string fullpath = obj.FileUrl;

            //string fileName = obj.FileName;

            //string[] files = Directory.GetDirectories(
            //        Server.MapPath(fullpath));
            List<string> Downloads = new List<string>();
            Response.ContentType = "application/zip";
            ZipFile zip;
            byte[] zipContent = null;
            string strfilePath = "";
            using (zip = new ZipFile())
            {
                zip.ParallelDeflateThreshold = -1;
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                foreach (var file in lstDoc)
                {
                    //Downloads.Add(Path.GetFileName(Convert.ToString(file)));
                    // the following entry will be inserted at the root in the archive.
                    strfilePath = Server.MapPath("~" + file.FileUrl);
                    
                    zip.AddFile(strfilePath, string.Empty);    
                }
                
                using (var ms = new MemoryStream())
                {
                    
                    zip.Save(ms);
                    zipContent = ms.ToArray();
                }
            }
            
            string name = lstDoc[0].EmpDetails.Empname;
            Response.AppendHeader("Content-Disposition", string.Concat("inline; filename=\"", name + ".zip", "\""));
            return File(zipContent, "zip");

            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //ViewBag.Empid = Empid;
            //return View("Index");


            //try
            //{


            //    var Im = (from p in db.EmployeeDocuments
            //              where p.Event_Id == 1332
            //              select p.Event_Photo);

            //    Response.Clear();

            //    var downloadFileName = string.Format("YourDownload-{0}.zip", DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss"));
            //    Response.ContentType = "application/zip";

            //    Response.AddHeader("content-disposition", "filename=" + downloadFileName);

            //    using (ZipFile zipFile = new ZipFile())
            //    {
            //        zipFile.AddDirectoryByName("Files");
            //        foreach (var userPicture in Im)
            //        {
            //            zipFile.AddFile(Server.MapPath(@"\") + userPicture.Remove(0, 1), "Files");
            //        }
            //        zipFile.Save(Response.OutputStream);

            //        //Response.Close();
            //    }
            //    return View();
            //}
            //catch (Exception ex)
            //{
            //    return View();
            //}

        }
        //[HttpPost]
        //public ActionResult Downloads(DocumentItem model)
        //{
        //    int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
        //    ViewBag.Empid = Empid;
        //    return RedirectToAction("Index", new { id = Empid, @menuId = model.Viewbagidformenu });
        //}
        //public ActionResult Download(int id)
        //{
        //    DocumentItem obj = new DocumentItem();
        //    DocumentService objService = new DocumentService();
        //    List<DocumentItem> lstDoc = new List<DocumentItem>();
        //    lstDoc = objService.getAllDocByEmpID(id);
        //    obj.ListDoc = new List<DocumentItem>();
        //    obj.ListDoc.AddRange(lstDoc);            
        //    //var fullPath = Server.MapPath(files);

        //    var ZipStream = new ZipOutputStream(Response.OutputStream);
            

        //        Response.AddHeader("Content-Disposition", "attachment; filename=Documents.zip");
        //    Response.ContentType = "application/zip";
        //    foreach (var path in lstDoc)
        //    {
        //        string filepath = path.FileUrl;
        //        byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(filepath));
        //        var fileEntry = new ZipEntry(Path.GetFileName(Server.MapPath("~/UploadedDocs/" + path)))
        //        {
        //            Size = fileBytes.Length
        //        };
        //        ZipStream.PutNextEntry(fileEntry);
        //        ZipStream.Write(fileBytes, 0, fileBytes.Length);                
        //    }
        //    using (ZipFile zip = new ZipFile(Response.OutputStream))
        //    {
        //        zip.AddDirectory(Server.MapPath("~/Directories/hello"));
        //        zip.Save(Server.MapPath("~/Directories/hello/sample.zip"));
        //        return File(Server.MapPath("~/Directories/hello/sample.zip"),
        //                                   "application/zip", "sample.zip");
        //    }  
        //    ZipStream.Flush();
        //    ZipStream.Close();

            
        //    return View("Index");
        //}
        //[HttpGet]
        //public FileResult Download()
        //{
        //    // Create file on disk
        //    using (ZipFile zip = new ZipFile(Response.OutputStream))
        //    {
        //        zip.AddDirectory(Server.MapPath("~/Directories/hello"));
        //        //zip.Save(Response.OutputStream);
        //        zip.Save(Server.MapPath("~/Directories/hello/sample.zip"));
        //    }

        //    // Read bytes from disk
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(
        //        Server.MapPath("~/Directories/hello/sample.zip"));
        //    string fileName = "sample.zip";

        //    // Return bytes as stream for download
        //    return File(fileBytes, "application/zip", fileName);
        //}
    }
}




