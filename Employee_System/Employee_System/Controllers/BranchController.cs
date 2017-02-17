using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Branch;
using EMSDomain.ViewModel.Company;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class BranchController : Controller
    {
        //
        // GET: /Branch/     
        public ActionResult Create()
        {
            BranchService objBranch = new BranchService();
            BranchItem objItem = new BranchItem();
            List<BranchItem> lstBranch = new List<BranchItem>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstBranch = objBranch.getBranch(cid);
            objItem.ListBranch = new List<BranchItem>();
            objItem.ListBranch.AddRange(lstBranch);
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objBranch.getCountry();
            objItem.ListCon = new List<clsMasterData>();
            objItem.ListCon.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objBranch.GetAllComp();
            objItem.ListComp = new List<CompanyItem>();
            objItem.ListComp.AddRange(lstComp);
            #endregion
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objItem);
        }

        [HttpPost]
        public ActionResult Create(BranchItem model)
        {
            BranchService objBranch = new BranchService();
            BranchItem objItem = new BranchItem();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (model.CompID == null)
            {
                model.CompID = cid;
            }
            objBranch.Insert(model);
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objBranch.GetAllComp();
            objItem.ListComp = new List<CompanyItem>();
            objItem.ListComp.AddRange(lstComp);
            #endregion
            
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }
        [HttpPost]
        public ActionResult Edit(BranchItem model)
        {
            BranchService objBranch = new BranchService();
            BranchItem objItem = new BranchItem();
            objBranch.Update(model);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objBranch.GetAllComp();
            objItem.ListComp = new List<CompanyItem>();
            objItem.ListComp.AddRange(lstComp);
            #endregion
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            BranchService objBranch = new BranchService();
            BranchItem objBItem = new BranchItem();
            objBItem = objBranch.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<BranchItem> lstBranch = new List<BranchItem>();
            objBItem.ListBranch = new List<BranchItem>();
            objBItem.ListBranch.AddRange(lstBranch);
            ViewBag.Menuid = Request.QueryString["menuId"];
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objBranch.getCountry();
            objBItem.ListCon = new List<clsMasterData>();
            objBItem.ListCon.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objBranch.GetAllComp();
            objBItem.ListComp = new List<CompanyItem>();
            objBItem.ListComp.AddRange(lstComp);
            #endregion
            return View(objBItem);
        }
        public ActionResult View(int id)
        {
            BranchService objBranch = new BranchService();
            BranchItem objBItem = new BranchItem();
            objBItem = objBranch.GetById(id);
            //Session["Empid"] = objPassItem.EmpId;
            List<BranchItem> lstBranch = new List<BranchItem>();
            objBItem.ListBranch = new List<BranchItem>();
            objBItem.ListBranch.AddRange(lstBranch);
            ViewBag.Menuid = Request.QueryString["menuId"];
            #region Bind DropDown Country
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objBranch.getCountry();
            objBItem.ListCon = new List<clsMasterData>();
            objBItem.ListCon.AddRange(lstMasters);

            #endregion
            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objBranch.GetAllComp();
            objBItem.ListComp = new List<CompanyItem>();
            objBItem.ListComp.AddRange(lstComp);
            #endregion
            return View(objBItem);
        }
    }
}
