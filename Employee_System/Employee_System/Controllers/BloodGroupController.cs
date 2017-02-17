using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class BloodGroupController : Controller
    {
        //
        // GET: /BloodGroup/

        public ActionResult Index()
        {
            clsBloodGroupMethod BGM = new clsBloodGroupMethod();
            List<BloodGroupItem> lstBG = BGM.Getall();
            return View(lstBG);
        }

        public ActionResult Create()
        {
            clsBloodGroupMethod objBGMethod = new clsBloodGroupMethod();
            BloodGroupItem objBG = new BloodGroupItem();
            List<BloodGroupItem> lstBG = BindGrid();

            objBG.bloodList = new List<BloodGroupItem>();   //line1
            objBG.bloodList.AddRange(lstBG);   //line2 list
            return View(objBG);
        }

        [HttpPost]
        public ActionResult Create(BloodGroupItem model)
        {
            clsBloodGroupMethod objBlood = new clsBloodGroupMethod();
            objBlood.Insert(model);
            return RedirectToAction("Create");
        }

        public List<BloodGroupItem> BindGrid()
        {
            try
            {
                clsBloodGroupMethod objBlood = new clsBloodGroupMethod();
                List<BloodGroupItem> lstBG = objBlood.Getall();
                return lstBG;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
