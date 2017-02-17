using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSMethods;
using EMSDomain.Model;


namespace Employee_System.Controllers
{
    public class MasterDataController : Controller
    {
        //
        // GET: /MasterData/

        public ActionResult Index(clsMasterData model)
        {
            List<ClsMasterP> lstmasterp = new List<ClsMasterP>();
            clsMasterMethod objmastermethod = new clsMasterMethod();
            lstmasterp = objmastermethod.GetALL();

            clsMasterData objmasterdata = new clsMasterData();

            List<clsMasterData> objmasterdatalst = new List<clsMasterData>();
            clsMasterDataMethod objmasterdatamethod = new clsMasterDataMethod();
            objmasterdatalst = objmasterdatamethod.GetALL(model.MasterId);

            objmasterdata.MasterDataList = new List<clsMasterData>();
            objmasterdata.MasterDataList.AddRange(objmasterdatalst);



            // clsMasterData objmasterdata = new clsMasterData();
            if (objmasterdata.maserlist == null)
                objmasterdata.maserlist = new List<ClsMasterP>();
            objmasterdata.maserlist.AddRange(lstmasterp);
            return View(objmasterdata);
        }

        public ActionResult Getid(int? Compid)
        {
            ViewBag.id = Compid;
            return RedirectToAction("Create", new { @id = ViewBag.id });
        }
        public ActionResult Create()
        {
            //int Compid;
            //if (ViewBag.id != null)
            //    id = ViewBag.id;
            ////if (Compid == null)
            //{
            //    Compid = 0;
            //}
            int id = 0;
            List<ClsMasterP> lstmasterp = new List<ClsMasterP>();
            clsMasterMethod objmastermethod = new clsMasterMethod();
            lstmasterp = objmastermethod.GetALL();

            clsMasterData objmasterdata = new clsMasterData();

            List<clsMasterData> objmasterdatalst = new List<clsMasterData>();
            clsMasterDataMethod objmasterdatamethod = new clsMasterDataMethod();
            objmasterdatalst = objmasterdatamethod.GetALL(id);

            objmasterdata.MasterDataList = new List<clsMasterData>();
            objmasterdata.MasterDataList.AddRange(objmasterdatalst);



            // clsMasterData objmasterdata = new clsMasterData();
            if (objmasterdata.maserlist == null)
                objmasterdata.maserlist = new List<ClsMasterP>();
            objmasterdata.maserlist.AddRange(lstmasterp);
            if (TempData["Id"] != null)
            {
                objmasterdata.MasterId = (int)TempData["Id"];
            }
            else
            {
                objmasterdata.MasterId = 0;
            }

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objmasterdata);

        }

        [HttpPost]
        public ActionResult Create(clsMasterData model)
        {
            //var MasterId = Request.Form["hdnMasterId"];
            //var MasterId1 = Request.Form["MasterId"];

            clsMasterDataMethod objmasterdata = new clsMasterDataMethod();
            model.Status = "Active";
            objmasterdata.Insert(model);
            TempData["Id"] = model.MasterId;
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int Id)
        {
            clsMasterDataMethod objmethod = new clsMasterDataMethod();
            clsMasterData objmasterdata = objmethod.GetById(Id);

            List<ClsMasterP> lstmasterp = new List<ClsMasterP>();
            clsMasterMethod objmastermethod = new clsMasterMethod();
            lstmasterp = objmastermethod.GetALL();
            if (objmasterdata.maserlist == null)
                objmasterdata.maserlist = new List<ClsMasterP>();
            objmasterdata.maserlist.AddRange(lstmasterp);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objmasterdata);

        }


        [HttpPost]
        public ActionResult Edit(clsMasterData model)
        {
            clsMasterDataMethod objmasterdatamodel = new clsMasterDataMethod();
            objmasterdatamodel.Update(model);
            clsMasterData objmasterdata = new clsMasterData();
            List<clsMasterData> lstMstdata = new List<clsMasterData>();
            List<clsMasterDataMethod> lstmstDataMetjod = new List<clsMasterDataMethod>();

            clsMasterDataMethod objmethod = new clsMasterDataMethod();
            //  lstMstdata = objmethod.GetALL();

            objmasterdata.MasterDataList = new List<clsMasterData>();
            objmasterdata.MasterDataList.AddRange(lstMstdata);

            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult FillGrid(int Compid)
        {
            string strCompCode = Convert.ToString(Compid);
            clsMasterData objmasterdata = new clsMasterData();
            List<clsMasterData> objmasterdatalst = new List<clsMasterData>();
            clsMasterDataMethod objmasterdatamethod = new clsMasterDataMethod();
            objmasterdatalst = objmasterdatamethod.GetALLGrid(Compid);
            //objBranchItem.ListBranch.Add(new BranchItem { id = 0, BranchName = "--Select Branch--" });
            //// objBranchItem.ListBranch.AddRange(lstBranch);
            objmasterdata.MasterDataList = new List<clsMasterData>();
            objmasterdata.MasterDataList.AddRange(objmasterdatalst);
            return PartialView("_List", objmasterdata.MasterDataList);
        }
    }
}
