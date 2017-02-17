using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSMethods;
using EMSDomain.ViewModel;
using EMSDomain.Model;

namespace Employee_System.Controllers
{
    public class MastersController : Controller
    {
        //
        // GET: /Masters/

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Create()
        {
            List<ClsMasterP> lstmasterp = new List<ClsMasterP>();
            List<clsMasterMethod> objMasterMethod = new List<clsMasterMethod>();
            lstmasterp = BindGrid();
            ClsMasterP objmast = new ClsMasterP();
            objmast.MasterList = new List<ClsMasterP>();
            objmast.MasterList.AddRange(lstmasterp);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objmast);
        }

        [HttpPost]
        public ActionResult Create(ClsMasterP model)
        {
            clsMasterMethod objMaster = new clsMasterMethod();
            objMaster.Insert(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            clsMasterMethod objmethod = new clsMasterMethod();
            ClsMasterP objmaster = new ClsMasterP();
            objmaster = objmethod.GetById(id);
            List<ClsMasterP> lstmasterp = new List<ClsMasterP>();
            objmaster.MasterList = new List<ClsMasterP>();
            objmaster.MasterList.AddRange(lstmasterp);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objmaster);

        }

        [HttpPost]
        public ActionResult Edit(ClsMasterP model)
        {
            ClsMasterP objmasterp = new ClsMasterP();
            clsMasterMethod objmasterme = new clsMasterMethod();
            objmasterme.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });

        }

        public List<ClsMasterP> BindGrid()
        {
            clsMasterMethod objclsmethod = new clsMasterMethod();
            List<ClsMasterP> objMaster = objclsmethod.GetALL();
            return objMaster;
        }
    }
}
