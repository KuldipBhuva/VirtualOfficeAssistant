using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;
using Microsoft.Reporting.WebForms;

namespace Employee_System.Controllers
{
    public class VehicleMasterController : Controller
    {
        //
        // GET: /VehicleMaster/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult getData(int id)
        {
            //string strCompCode = Convert.ToString(Compid);
            //VehicleItem objmasterdata = new VehicleItem();
            //List<VehicleItem> objmasterdatalst = new List<VehicleItem>();
            //VehicleMasterService objmasterdatamethod = new VehicleMasterService();
            //objmasterdatalst = objmasterdatamethod.GetById(id);
            ////objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            ////// objBranchItem.ListBranch.AddRange(lstBranch);
            //objmasterdata.ListVehicle = new List<VehicleItem>();
            //objmasterdata.ListVehicle.AddRange(objmasterdatalst);
            //return PartialView("_DataList", objmasterdata.ListVehicle);

            VehicleMasterService objmethod = new VehicleMasterService();
            VehicleItem objVehicleItem = new VehicleItem();
            List<VehicleItem> lstmasterp = new List<VehicleItem>();
            lstmasterp = objmethod.getByIDVehicle(id);

            
            objVehicleItem.ListVehicle = new List<VehicleItem>();
            objVehicleItem.ListVehicle.AddRange(lstmasterp);

            return PartialView("_DataList", objVehicleItem.ListVehicle);

        }
        public ActionResult Create()
        {
            //int Empid = 0;
            //if (Url.RequestContext.RouteData.Values["id"].ToString() != null)
            //{
            //    Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //}
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            VehicleMasterService objVehicle = new VehicleMasterService();
            lstVehicle = objVehicle.VehicleListData("Active",cid);
            VehicleItem objVehicleItem = new VehicleItem();
            objVehicleItem.ListVehicle = new List<VehicleItem>();
            objVehicleItem.ListVehicle.AddRange(lstVehicle);

           
            //#region Bind DropDown comp
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objVehicleItem.ListCompany = new List<CompanyItem>();
            objVehicleItem.ListCompany.AddRange(objCompany);
            
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            //#endregion
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objVehicleItem.ListBranch = new List<BranchItem>();
            objVehicleItem.ListBranch.AddRange(lstBranch);

            #endregion
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objVehicle.GetEmp(cid);
            objVehicleItem.ListEmp = new List<EmployeeItem>();
            objVehicleItem.ListEmp.AddRange(lstEmp);

            #endregion
            PetrolCardService objPCard = new PetrolCardService();
            PetrolCardItem objPCardItem = new PetrolCardItem();
            #region DDL PaymentType
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objPCard.GetPaymentType();
            objPCardItem.ListMaster = new List<clsMasterData>();
            objPCardItem.ListMaster.AddRange(lstMasters1);

            objVehicleItem.ListMasterTablePayment = objPCardItem.ListMaster;
            #endregion
            #region DDL PCardType
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objPCard.GetPCardType();
            objPCardItem.ListMaster1 = new List<clsMasterData>();
            objPCardItem.ListMaster1.AddRange(lstMasters);
            objVehicleItem.ListMasterTableCard = objPCardItem.ListMaster1;
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objVehicleItem);
        }
        [HttpPost]
        public ActionResult Create(VehicleItem model, HttpPostedFileBase[] files)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            model.Status = "Active";
            model.PCardItem.Status = "Active";
            VehicleMasterService objVehicle = new VehicleMasterService();
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
            model.CreatedOn = System.DateTime.Now;
            
            objVehicle.Insert(model, model.PCardItem);

            VehicleDocumentItem objVehicleItem = new VehicleDocumentItem();
            VehicleMasterService obj = new VehicleMasterService();


            if (files != null)
            {
                string s = objVehicle.Getid();
                string strtext = "Vehicle";
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

                        objVehicleItem.CreatedBy = uid;
                        objVehicleItem.CreatedDate = System.DateTime.Now;
                        objVehicleItem.VId = Convert.ToInt32(s);
                        objVehicleItem.FileUrl = Path.Combine("/UploadedDocs/" + strtext + "/" + s + "/", fileNewName);
                        objVehicleItem.FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        obj.Save(objVehicleItem);
                        //string filepathtosave = "Images/" + filename;
                        /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                    }
                }
            }

            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(VehicleItem model, HttpPostedFileBase[] files)
        {
            //    int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //model.EmpID = Empid;
            VehicleMasterService objVservice = new VehicleMasterService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            
            model.UpdatedBy = uid;
            model.UpdatedOn = System.DateTime.Now;
            objVservice.Update(model, model.PCardItem);

            VehicleDocumentItem objVehicleItem = new VehicleDocumentItem();
            VehicleMasterService obj = new VehicleMasterService();


            if (files != null)
            {
                string s = Convert.ToString(model.VID);
                string strtext = "Vehicle";
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
                        objVehicleItem.UpdatedBy = uid;
                        objVehicleItem.UpdatedDate = System.DateTime.Now;
                        objVehicleItem.VId = Convert.ToInt32(s);
                        objVehicleItem.FileUrl = Path.Combine("/UploadedDocs/" + strtext + "/" + s + "/", fileNewName);
                        objVehicleItem.FileName = Path.GetFileNameWithoutExtension(file.FileName);
                        obj.Save(objVehicleItem);
                        //string filepathtosave = "Images/" + filename;
                        /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                    }
                }
            }

            return RedirectToAction("Edit", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            VehicleMasterService objVService = new VehicleMasterService();
            VehicleItem objVItem = new VehicleItem();
            List<VehicleItem> lstVItem = new List<VehicleItem>();
            objVItem = objVService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;

            objVItem.ListVehicle = new List<VehicleItem>();
            objVItem.ListVehicle.AddRange(lstVItem);

            //Bind Doc Grid
            List<VehicleDocumentItem> ListVehicle = new List<VehicleDocumentItem>();
            ListVehicle = objVService.getVehicleDoc(id);
            objVItem.ListVehicleDocument = new List<VehicleDocumentItem>();
            objVItem.ListVehicleDocument.AddRange(ListVehicle);

            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objVItem.ListCompany = new List<CompanyItem>();
            objVItem.ListCompany.AddRange(objCompany);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objVService.GetEmp(cid);
            objVItem.ListEmp = new List<EmployeeItem>();
            objVItem.ListEmp.AddRange(lstEmp);
            #endregion

            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVService.GetBranch();
            objVItem.ListBranch = new List<BranchItem>();
            objVItem.ListBranch.AddRange(lstBranch);
            #endregion

            PetrolCardService objPCard = new PetrolCardService();
            PetrolCardItem objPCardItem = new PetrolCardItem();
            #region DDL PaymentType
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objPCard.GetPaymentType();
            objPCardItem.ListMaster = new List<clsMasterData>();
            objPCardItem.ListMaster.AddRange(lstMasters1);

            objVItem.ListMasterTablePayment = objPCardItem.ListMaster;
            #endregion
            #region DDL PCardType
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objPCard.GetPCardType();
            objPCardItem.ListMaster1 = new List<clsMasterData>();
            objPCardItem.ListMaster1.AddRange(lstMasters);
            objVItem.ListMasterTableCard = objPCardItem.ListMaster1;
            #endregion



            ViewBag.Menuid = Request.QueryString["menuId"];
            var aa = ViewBag.Menuid;

            return View(objVItem);
        }
        public ActionResult View(int id)
        {
            VehicleMasterService objVService = new VehicleMasterService();
            VehicleItem objVItem = new VehicleItem();
            List<VehicleItem> lstVItem = new List<VehicleItem>();
            objVItem = objVService.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;

            objVItem.ListVehicle = new List<VehicleItem>();
            objVItem.ListVehicle.AddRange(lstVItem);

            //Bind Doc Grid
            List<VehicleDocumentItem> ListVehicle = new List<VehicleDocumentItem>();
            ListVehicle = objVService.getVehicleDoc(id);
            objVItem.ListVehicleDocument = new List<VehicleDocumentItem>();
            objVItem.ListVehicleDocument.AddRange(ListVehicle);

            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();
            objVItem.ListCompany = new List<CompanyItem>();
            objVItem.ListCompany.AddRange(objCompany);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            #region Bind DropDown Emp
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objVService.GetEmp(cid);
            objVItem.ListEmp = new List<EmployeeItem>();
            objVItem.ListEmp.AddRange(lstEmp);
            #endregion

            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVService.GetBranch();
            objVItem.ListBranch = new List<BranchItem>();
            objVItem.ListBranch.AddRange(lstBranch);
            #endregion

            PetrolCardService objPCard = new PetrolCardService();
            PetrolCardItem objPCardItem = new PetrolCardItem();
            #region DDL PaymentType
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objPCard.GetPaymentType();
            objPCardItem.ListMaster = new List<clsMasterData>();
            objPCardItem.ListMaster.AddRange(lstMasters1);

            objVItem.ListMasterTablePayment = objPCardItem.ListMaster;
            #endregion
            #region DDL PCardType
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objPCard.GetPCardType();
            objPCardItem.ListMaster1 = new List<clsMasterData>();
            objPCardItem.ListMaster1.AddRange(lstMasters);
            objVItem.ListMasterTableCard = objPCardItem.ListMaster1;
            #endregion



            ViewBag.Menuid = Request.QueryString["menuId"];
            var aa = ViewBag.Menuid;

            return View(objVItem);
        }
        public ActionResult delete(int id, int DID, int menuid)
        {
            //int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            EmpExperienceService objExpService = new EmpExperienceService();
            EMSDomain.ViewModel.DocumentItem objDoc = new EMSDomain.ViewModel.DocumentItem();
            objDoc = objExpService.byID(DID);
            string path = objDoc.FileUrl;
            var fullPath = Server.MapPath(path);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            db.VehicleDocuments.Remove(db.VehicleDocuments.Find(DID));
            db.SaveChanges();
            //ViewBag.Empid = Empid;
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Edit", new { @id = id, @menuId = Request.QueryString["menuId"] });
        }
        public ActionResult FillBranch(int Compid)
        {
            //string strCompCode = Convert.ToString(Compid);
            List<BranchItem> lstBranch = new List<BranchItem>();
            BranchItem objBranchItem = new BranchItem();
            BranchService objBranchS = new BranchService();
            lstBranch = objBranchS.GetBranchByComp(Compid);
            objBranchItem.ListBranch = new List<BranchItem>();
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            objBranchItem.ListBranch.AddRange(lstBranch);
            //var e = new { BranchItem = objBranchItem.ListBranch, EmployeeItem = objBranchItem.ListBranch };
            return Json(objBranchItem.ListBranch, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillEmp(int Compid)
        {
            int strCompCode = Compid;
            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            EmployeeItem objEmpItem = new EmployeeItem();
            EmployeeService objEmp = new EmployeeService();
            lstEmp = objEmp.GetEmpByComp(strCompCode);
            objEmpItem.EmployeeList = new List<EmployeeItem>();
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            objEmpItem.EmployeeList.AddRange(lstEmp);
            return Json(objEmpItem.EmployeeList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillVData(string FID)
        {
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            VehicleMasterService objVehicle = new VehicleMasterService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstVehicle = objVehicle.VehicleListData(FID,cid);
            VehicleItem objVehicleItem = new VehicleItem();
            objVehicleItem.ListVehicle = new List<VehicleItem>();
            objVehicleItem.ListVehicle.AddRange(lstVehicle);


            ////string strCompCode = Convert.ToString(Compid);
            //List<EmpAllItem> lstEmp = new List<EmpAllItem>();
            //EmpAllItem objEmpItem = new EmpAllItem();
            //EmployeeService objEmp = new EmployeeService();
            //lstEmp = objEmp.GetALLDetails(FID);
            //objEmpItem.EmployeeALLDetails = new List<EmpAllItem>();
            ////objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            //objEmpItem.EmployeeALLDetails.AddRange(lstEmp);
            //var e = new { BranchItem = objBranchItem.ListBranch, EmployeeItem = objBranchItem.ListBranch };
            //return Json(objEmpItem.EmployeeALLDetails, JsonRequestBehavior.AllowGet);
            return PartialView("_Grid", objVehicleItem.ListVehicle);
        }    
    }
}
