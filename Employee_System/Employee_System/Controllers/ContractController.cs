using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_System.Controllers
{
    public class ContractController : Controller
    {
        //
        // GET: /Contract/
        ContractService objService = new ContractService();
        List<ContractModel> objList = new List<ContractModel>();
        ContractModel objModel = new ContractModel();
        public ActionResult Index()
        {
            objList = objService.GetALL();
            objModel.ListContract = new List<ContractModel>();
            objModel.ListContract.AddRange(objList);
            CompanyService compService = new CompanyService();
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = compService.GetALL();
            objModel.ListComp = new List<CompanyItem>();
            objModel.ListComp.AddRange(lstComp);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(ContractModel model, HttpPostedFileBase[] files)
        {
            int uid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
            }
            model.CreatedBy = uid;
            model.CreatedDate = System.DateTime.Now;
            if (files != null)
            {
                string strtext = "Contract";
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        string filename = System.IO.Path.GetFileName(file.FileName);
                        string folderPath = Server.MapPath("~/UploadedDocs/") + strtext;
                        //  obj.EmpId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                        string destinationPath = folderPath;
                        string employeeFolderPath = destinationPath;
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

                        model.FileName = filename;
                        model.FilePath = Path.Combine("/UploadedDocs/" + strtext + "/", fileNewName);
                    }
                }
            }
            objService.Insert(model);
            return RedirectToAction("Index", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult Edit(int id)
        {
            objModel = objService.GetById(id);
            CompanyService compService = new CompanyService();
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = compService.GetALL();
            objModel.ListComp = new List<CompanyItem>();
            objModel.ListComp.AddRange(lstComp);

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Edit(ContractModel model, HttpPostedFileBase[] files)
        {
             int uid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
            }
            model.UpdatedBy=uid;
            model.UpdatedDate=System.DateTime.Now;
            if (files != null)
            {
                string strtext = "Contract";
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null)
                    {
                        string filename = System.IO.Path.GetFileName(file.FileName);
                        string folderPath = Server.MapPath("~/UploadedDocs/") + strtext;
                        //  obj.EmpId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
                        string destinationPath = folderPath;
                        string employeeFolderPath = destinationPath;
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

                        model.FileName = filename;
                        model.FilePath = Path.Combine("/UploadedDocs/" + strtext + "/", fileNewName);
                    }
                }
            }
            objService.Update(model);
            return RedirectToAction("Index", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult View(int id)
        {
            objModel = objService.GetById(id);
            CompanyService compService = new CompanyService();
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = compService.GetALL();
            objModel.ListComp = new List<CompanyItem>();
            objModel.ListComp.AddRange(lstComp);

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
    }
}
