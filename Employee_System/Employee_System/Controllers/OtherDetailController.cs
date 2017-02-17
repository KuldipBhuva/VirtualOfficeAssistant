using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Employee;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class OtherDetailController : Controller
    {
        //
        // GET: /EmpOtherDetail/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());

            EmpOtherService objOther = new EmpOtherService();
            List<EmpOtherItem> listOther = new List<EmpOtherItem>();
            listOther = objOther.OtherListData(Empid);
            ViewBag.Empid = Empid;
            EmpOtherItem objDepitem = new EmpOtherItem();
            objDepitem.ListOther = new List<EmpOtherItem>();
            objDepitem.ListOther.AddRange(listOther);
            return View(objDepitem);
        }

        [HttpPost]
        public ActionResult Create(EmpOtherItem model)
        {
            model.Status = "Active";
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            model.CreatedBy = Convert.ToString(Empid);
            model.CreatedDate = System.DateTime.Now;
            EmpOtherService objEmpOther = new EmpOtherService();
            objEmpOther.Insert(model);
            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult Edit(EmpOtherItem model)
        {
            int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.EmpId = Empid;
            model.UpdatedBy = Convert.ToString(Empid);
            model.UpdatedDate = System.DateTime.Now;
            EmpOtherService objOther = new EmpOtherService();
            objOther.Update(model);
            ViewBag.Empid = Empid;
            return RedirectToAction("Create", new { @id = Empid });
        }

        public ActionResult Edit(int id,int OtId)
        {
            EmpOtherService objOtherService = new EmpOtherService();
            EmpOtherItem objOtherItem = new EmpOtherItem();
            objOtherItem = objOtherService.GetById(OtId);
            //Session["Empid"] = objOtherItem.EmpId;
            List<EmpOtherItem> lstOther = new List<EmpOtherItem>();
            objOtherItem.ListOther = new List<EmpOtherItem>();
            objOtherItem.ListOther.AddRange(lstOther);
            return View(objOtherItem);
        }


    }
}
