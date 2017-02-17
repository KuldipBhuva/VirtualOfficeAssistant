using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Assests;
using EMSDomain.ViewModel.Category;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class IssueDetailController : Controller
    {
        //
        // GET: /IssueDetail/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ItemService objItemService = new ItemService();
            CategoryService objCatServ = new CategoryService();
            CategoryItem objCatItem = new CategoryItem();
            List<CategoryItem> lstCat = new List<CategoryItem>();
            IssueItem objItem = new IssueItem();
            List<IssueItem> lstIssue = new List<IssueItem>();
            IssueService objIssueService = new IssueService();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstCat = objCatServ.GetALL(cid);
            objItem.lstCategory = new List<CategoryItem>();
            objItem.lstCategory.AddRange(lstCat);

            List<AssestsItem> lstItem = new List<AssestsItem>();
            lstItem = objItemService.GetALLItems(cid);
            objItem.lstAllItem = new List<AssestsItem>();
            objItem.lstAllItem.AddRange(lstItem);

            lstIssue = objIssueService.GetALL();
            objItem.lstIssueItem = new List<IssueItem>();
            objItem.lstIssueItem.AddRange(lstIssue);

            return View(objItem);
        }


        [HttpPost]
        public ActionResult Create(IssueItem Model)
        {
            IssueItem objIssueItem = new IssueItem();
            IssueService objIssueService = new IssueService();

            int i = objIssueService.InsertIssueDetails(Model);
            StockItem objStockModel = new StockItem();
            objStockModel.ItemId = Model.Item_id;
            objStockModel.IssueQty = Model.IssueQuantity;
            objStockModel.IssueDate = Model.IssueDate;
            int j = objIssueService.InsertIssueStock(objStockModel);
            return RedirectToAction("Create");
        }


        public ActionResult BindAllItem(int Cat_id)
        {
            ItemService objItemService = new ItemService();
            IssueItem objIssueItem = new IssueItem();

            List<AssestsItem> lstItem = new List<AssestsItem>();
            lstItem = objItemService.GetItemsBYCatId(Cat_id);
            objIssueItem.lstAllItem = new List<AssestsItem>();
            objIssueItem.lstAllItem.AddRange(lstItem);
            return Json(objIssueItem.lstAllItem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int Id)
        {
            IssueService objIssueService = new IssueService();
            IssueItem objIssueItem = new IssueItem();
            objIssueItem = objIssueService.GetById(Id);
            CategoryService objCatServ = new CategoryService();
            List<CategoryItem> lstCat = new List<CategoryItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstCat = objCatServ.GetALL(cid);
            objIssueItem.lstCategory = new List<CategoryItem>();
            objIssueItem.lstCategory.AddRange(lstCat);

            ItemService objItemService = new ItemService();
            List<AssestsItem> lstItem = new List<AssestsItem>();
            lstItem = objItemService.GetALLItems(cid);
            objIssueItem.lstAllItem = new List<AssestsItem>();
            objIssueItem.lstAllItem.AddRange(lstItem);

            List<IssueItem> lstIssue = new List<IssueItem>();
            lstIssue = objIssueService.GetALL();
            objIssueItem.lstIssueItem = new List<IssueItem>();
            objIssueItem.lstIssueItem.AddRange(lstIssue);

            return View(objIssueItem);
        }

        [HttpPost]
        public ActionResult Edit(IssueItem Model)
        {
            IssueItem objIssueItem = new IssueItem();
            IssueService objIssueService = new IssueService();

            int i = objIssueService.Update(Model);
            return RedirectToAction("Create");
        }


        public ActionResult AvailQuantity(int AvailQt)
        {
            //int strEmp = AvailQnty;
            List<IssueItem> lstIssue = new List<IssueItem>();
            IssueItem objIssueItem = new IssueItem();
            IssueService objEmp = new IssueService();
            lstIssue = objEmp.GetQuntyDetails(AvailQt);
            objIssueItem.lstIssueItem = new List<IssueItem>();
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            objIssueItem.lstIssueItem.AddRange(lstIssue);
            // lstRepay.
            if (objIssueItem.lstIssueItem.Count != 0)
            {
                return Json(objIssueItem.lstIssueItem[0].Available, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
