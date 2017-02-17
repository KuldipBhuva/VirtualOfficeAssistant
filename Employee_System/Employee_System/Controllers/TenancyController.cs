using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Tenancy;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class TenancyController : Controller
    {
        TenancyService objTenancyService = new TenancyService();
        TenancyItem objTenancyItem = new TenancyItem();

        EmployeeEntities db = new EmployeeEntities();
        public ActionResult delete(int id, int menuid)
        {
            try
            {
                //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                TenancyService objTenService = new TenancyService();
                TenancyItem objDoc = new TenancyItem();
                objDoc = objTenService.GetByid(id);
                db.TenancyMasters.Remove(db.TenancyMasters.Find(id));
                db.SaveChanges();
                //ViewBag.Empid = Empid;
                ViewBag.Menuid = Request.QueryString["menuId"];
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "First Delete This Tenancy No's All Documents. ";
                //ViewBag.Action = "Create";
                //ViewBag.Controller = "TenancyController";
                //ViewBag.view = "Tenancy" + id + menuid;
                return View("Error");
            }
            return RedirectToAction("Create", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult deleteDoc(int id,int DID, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            TenancyService objTenService = new TenancyService();
            TenancyDocumentItem objDoc = new TenancyDocumentItem();
            objDoc = objTenService.getByID(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.TenancyDocuments.Remove(db.TenancyDocuments.Find(DID));
            db.SaveChanges();
            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Edit", new {@id=id,@menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Create()
        {
            objTenancyService = new TenancyService();
            objTenancyItem = new TenancyItem();

            //CompanyBind
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objTenancyService.GetCompany();
            objTenancyItem.ListCompany = new List<CompanyItem>();
            objTenancyItem.ListCompany.AddRange(lstCompany);

            //Employee Bind
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objTenancyService.GetEmployee();
            objTenancyItem.ListEmployee = new List<EmployeeItem>();
            objTenancyItem.ListEmployee.AddRange(lstEmp);

            // ListGrid bind
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<TenancyItem> ListTenancy = new List<TenancyItem>();
            ListTenancy = objTenancyService.GetAll(cid);

            objTenancyItem.ListTenancy = new List<TenancyItem>();
            //  objTenancyItem.ListTenancy.Add(new TenancyItem{TnID=Encrypt(ListTenancy)})
            objTenancyItem.ListTenancy.AddRange(ListTenancy);

            //TenancyType
            List<clsMasterData> lstTT = new List<clsMasterData>();
            lstTT = objTenancyService.GetTenancyType();
            objTenancyItem.ListTT = new List<clsMasterData>();
            objTenancyItem.ListTT.AddRange(lstTT);

            ViewBag.Menuid = (Request.QueryString["menuid"]);
            return View(objTenancyItem);
        }




        [HttpPost]
        public ActionResult Create(TenancyItem model, HttpPostedFileBase[] files)
        {
            if (model.TenTypeEmpID != null && model.TnTypeId == null)
            {
                model.TnTypeId = model.TenTypeEmpID;
            }

            objTenancyService = new TenancyService();
            objTenancyItem = new TenancyItem();
            model.Status = "Active";
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.TnTypeCompId == null)
            {
                model.TnTypeCompId = cid;
            }
            objTenancyService.Insert(model);


            if (files != null)
            {
                string s = objTenancyService.Getid();
                string strtext = "Tenancy";
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
                        objTenancyItem.CreatedBy = uid;
                        objTenancyItem.CreatedDate = System.DateTime.Now;
                        objTenancyItem.TenId = Convert.ToInt32(s);
                        objTenancyItem.FileUrl = Path.Combine("/UploadedDocs/" + strtext + "/" + s + "/", fileNewName);
                        objTenancyItem.FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        objTenancyService.Save(objTenancyItem);
                        //string filepathtosave = "Images/" + filename;
                        /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                    }
                }
            }

            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            objTenancyService = new TenancyService();
            objTenancyItem = new TenancyItem();

           // string strid = objTenancyService.Decrypt(id.ToString());
            //string strid = id.ToString();
            //int Tid = Convert.ToInt32(strid);

            objTenancyItem = objTenancyService.GetByid(id);

            //CompanyBind
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objTenancyService.GetCompany();
            objTenancyItem.ListCompany = new List<CompanyItem>();
            objTenancyItem.ListCompany.AddRange(lstCompany);

            //Employee Bind
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objTenancyService.GetEmployee();
            objTenancyItem.ListEmployee = new List<EmployeeItem>();
            objTenancyItem.ListEmployee.AddRange(lstEmp);

            // ListGrid bind

            List<TenancyItem> ListTenancy = new List<TenancyItem>();
            ListTenancy = objTenancyService.getTenancyDoc(id);
            objTenancyItem.ListTenancy = new List<TenancyItem>();
            objTenancyItem.ListTenancy.AddRange(ListTenancy);

            //TenancyType
            List<clsMasterData> lstTT = new List<clsMasterData>();
            lstTT = objTenancyService.GetTenancyType();
            objTenancyItem.ListTT = new List<clsMasterData>();
            objTenancyItem.ListTT.AddRange(lstTT);
            
            //ViewBag.Menuid = objTenancyService.Decrypt(Request.QueryString["menuid"]);
            ViewBag.Menuid = (Request.QueryString["menuid"]);

            return View(objTenancyItem);
        }
        public ActionResult View(int id)
        {
            objTenancyService = new TenancyService();
            objTenancyItem = new TenancyItem();

            // string strid = objTenancyService.Decrypt(id.ToString());
            //string strid = id.ToString();
            //int Tid = Convert.ToInt32(strid);

            objTenancyItem = objTenancyService.GetByid(id);

            //CompanyBind
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objTenancyService.GetCompany();
            objTenancyItem.ListCompany = new List<CompanyItem>();
            objTenancyItem.ListCompany.AddRange(lstCompany);

            //Employee Bind
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objTenancyService.GetEmployee();
            objTenancyItem.ListEmployee = new List<EmployeeItem>();
            objTenancyItem.ListEmployee.AddRange(lstEmp);

            // ListGrid bind

            List<TenancyItem> ListTenancy = new List<TenancyItem>();
            ListTenancy = objTenancyService.getTenancyDoc(id);
            objTenancyItem.ListTenancy = new List<TenancyItem>();
            objTenancyItem.ListTenancy.AddRange(ListTenancy);

            //TenancyType
            List<clsMasterData> lstTT = new List<clsMasterData>();
            lstTT = objTenancyService.GetTenancyType();
            objTenancyItem.ListTT = new List<clsMasterData>();
            objTenancyItem.ListTT.AddRange(lstTT);
            //ViewBag.Menuid = objTenancyService.Decrypt(Request.QueryString["menuid"]);
            ViewBag.Menuid = (Request.QueryString["menuid"]);

            return View(objTenancyItem);
        }
        [HttpPost]
        public ActionResult Edit(TenancyItem model, HttpPostedFileBase[] files)
        {
            objTenancyService = new TenancyService();
            objTenancyItem = new TenancyItem();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            model.UpdatedBy = uid;
            model.UpdatedDate = System.DateTime.Now;
            objTenancyService.Update(model);


            if (files != null)
            {
                //string s = objTenancyService.Getid();
                string s = Convert.ToString(model.TnID);
                string strtext = "Tenancy";
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
                        objTenancyItem.UpdatedBy = uid;
                        objTenancyItem.UpdatedDate = System.DateTime.Now;
                        objTenancyItem.TenId = Convert.ToInt32(s);
                        objTenancyItem.FileUrl = Path.Combine("/UploadedDocs/" + strtext + "/" + s + "/", fileNewName);
                        objTenancyItem.FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        objTenancyService.Save(objTenancyItem);
                        //string filepathtosave = "Images/" + filename;
                        /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                    }
                }
            }

            return RedirectToAction("Edit", new { @menuId = model.Viewbagidformenu });
        }
    }
}
