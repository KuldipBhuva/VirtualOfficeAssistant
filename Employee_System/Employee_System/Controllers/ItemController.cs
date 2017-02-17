using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Assests;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Category;
using EMSDomain.ViewModel.Company;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class ItemController : Controller
    {
        //
        // GET: /Item/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            CategoryService objCatServ = new CategoryService();
            CategoryItem objCatItem = new CategoryItem();
            List<CategoryItem> lstCat = new List<CategoryItem>();
            ItemService objItemServ = new ItemService();
            AssestsItem objItem = new AssestsItem();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstCat = objCatServ.GetALL(cid);
            objItem.lstCategory = new List<CategoryItem>();
            objItem.lstCategory.AddRange(lstCat);

            List<AssestsItem> lstItems = new List<AssestsItem>();
            lstItems = objItemServ.GetALLItems(cid);
            objItem.lstOfItems = new List<AssestsItem>();
            objItem.lstOfItems.AddRange(lstItems);

            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objItem.ListCompany = new List<CompanyItem>();
            objItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objItem.ListBranch = new List<BranchItem>();
            objItem.ListBranch.AddRange(lstBranch);
            #endregion

            return View(objItem);
        }

        [HttpPost]
        public ActionResult Create(AssestsItem Model)
        {
            AssestsItem objItem = new AssestsItem();
            ItemService objItemServ = new ItemService();

            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            Model.CreatedBy = uid;
            Model.CreatedDate = System.DateTime.Now;
            Model.Status = "Active";
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (Model.CompID == null)
            {
                Model.CompID = cid;
            }
            int i = objItemServ.Insert(Model);
            return RedirectToAction("Create");
        }

        public ActionResult Edit(int ItemId)
        {
            CategoryService objCatServ = new CategoryService();
            CategoryItem objCatItem = new CategoryItem();
            ItemService objItemServ = new ItemService();
            AssestsItem objItem = new AssestsItem();
            List<AssestsItem> lstItems = new List<AssestsItem>();
            objItem = objItemServ.GetItemsById(ItemId);
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstItems = objItemServ.GetALLItems(cid);
            objItem.lstOfItems = new List<AssestsItem>();
            objItem.lstOfItems.AddRange(lstItems);

            List<CategoryItem> lstCat = new List<CategoryItem>();
          
            lstCat = objCatServ.GetALL(cid);
            objItem.lstCategory = new List<CategoryItem>();
            objItem.lstCategory.AddRange(lstCat);

            SponsorService objService = new SponsorService();
            List<CompanyItem> lstCompany = new List<CompanyItem>();
            lstCompany = objService.GetCompany();
            objItem.ListCompany = new List<CompanyItem>();
            objItem.ListCompany.AddRange(lstCompany);

            VehicleMasterService objVehicle = new VehicleMasterService();
            #region Bind DropDown Branch
            List<BranchItem> lstBranch = new List<BranchItem>();
            lstBranch = objVehicle.GetBranch();
            objItem.ListBranch = new List<BranchItem>();
            objItem.ListBranch.AddRange(lstBranch);
            #endregion

            return View(objItem);
        }

        [HttpPost]
        public ActionResult Edit(AssestsItem Model)
        {
            AssestsItem objItem = new AssestsItem();
            ItemService objItemServ = new ItemService();
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            Model.UpdatedBy = uid;
            Model.UpdatedDate = System.DateTime.Now;            
            int i = objItemServ.Update(Model);
            return RedirectToAction("Create");
        }


    }
}
