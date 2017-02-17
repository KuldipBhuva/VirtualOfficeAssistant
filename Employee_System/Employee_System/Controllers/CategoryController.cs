using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Category;
using EMSDomain.ViewModel.Company;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            CategoryItem objCatItem = new CategoryItem();
            CategoryService objCatServ = new CategoryService();
            List<CategoryItem> lstCat = new List<CategoryItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstCat = objCatServ.GetALL(cid);

            objCatItem.lstCateory = new List<CategoryItem>();
            objCatItem.lstCateory.AddRange(lstCat);
            ViewBag.Menuid = Request.QueryString["menuId"];

            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objCatItem.ListCompany = new List<CompanyItem>();
            objCatItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objCatItem.ListBranch = new List<BranchItem>();
            objCatItem.ListBranch.AddRange(lstBranch);

            #endregion

            return View(objCatItem);

        }

        [HttpPost]
        public ActionResult Create(CategoryItem Model)
        {
            CategoryItem objCatItem = new CategoryItem();
            CategoryService objCatServ = new CategoryService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (Model.CompID == null)
            {
                Model.CompID = cid;
            }

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            Model.CreatedBy = uid;
            Model.CreatedDate = System.DateTime.Now;
            Model.Status = "Active";
            int i = objCatServ.Insert(Model);
            return RedirectToAction("Create", new { @menuId = Model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            CategoryItem objCatItem = new CategoryItem();
            CategoryService objCatServ = new CategoryService();
            objCatItem = objCatServ.GetById(id);
            List<CategoryItem> lstCat = new List<CategoryItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstCat = objCatServ.GetALL(cid);
            objCatItem.lstCateory = new List<CategoryItem>();
            objCatItem.lstCateory.AddRange(lstCat);
            ViewBag.Menuid = Request.QueryString["menuId"];


            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objCatItem.ListCompany = new List<CompanyItem>();
            objCatItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objCatItem.ListBranch = new List<BranchItem>();
            objCatItem.ListBranch.AddRange(lstBranch);

            #endregion

            return View(objCatItem);
        }


        [HttpPost]
        public ActionResult Edit(CategoryItem Model)
        {
            CategoryItem objCatItem = new CategoryItem();
            CategoryService objCatServ = new CategoryService();

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            Model.UpdatedBy = uid;
            Model.UpdatedDate = System.DateTime.Now;
           // Model.Status = "Active";
            int i = objCatServ.Update(Model);
            return RedirectToAction("Create", new { @menuId = Model.Viewbagidformenu });
        }




    }
}
