using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class ConsignmentController : Controller
    {
        //
        // GET: /Consignment/

        public ActionResult Index()
        {
            ConsignmentService objService = new ConsignmentService();
            ConsignmentModel objModel = new ConsignmentModel();
            List<ConsignmentModel> objList = new List<ConsignmentModel>();

            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }

            objList = objService.GetALL(rid, cid);
            objModel.ListConsignment = new List<ConsignmentModel>();
            objModel.ListConsignment.AddRange(objList);

            List<PartyModel> lstConsigner = new List<PartyModel>();
            lstConsigner = objService.getConsigner(rid, cid);
            objModel.ListConsigner = new List<PartyModel>();
            objModel.ListConsigner.AddRange(lstConsigner);

            List<PartyModel> lstConsignee = new List<PartyModel>();
            lstConsignee = objService.getConsigner(rid, cid);
            objModel.ListConsignee = new List<PartyModel>();
            objModel.ListConsignee.AddRange(lstConsignee);

            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objService.getVehicle(rid, cid);
            objModel.ListVehicle = new List<VehicleItem>();
            objModel.ListVehicle.AddRange(lstVehicle);
            ViewBag.Menuid = (Request.QueryString["menuid"]);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(ConsignmentModel model)
        {
            ConsignmentService objService = new ConsignmentService();
            int uid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                int cid = 0;
                if (Session["CompID"] != null)
                {
                    cid = Convert.ToInt32(Session["CompID"].ToString());
                }
                if (model.CompID == null)
                {
                    model.CompID = cid;
                }
                model.InvoiceNo = "GR";
                model.CreatedBy = uid;
                model.CreatedDate = System.DateTime.Now;
                model.Status = true;
                objService.Insert(model);
            }
            return RedirectToAction("Index", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult Edit(int id)
        {
            ConsignmentService objService = new ConsignmentService();
            ConsignmentModel objModel = new ConsignmentModel();

            objModel = objService.GetById(id);

            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }

            List<PartyModel> lstConsigner = new List<PartyModel>();
            lstConsigner = objService.getConsigner(rid, cid);
            objModel.ListConsigner = new List<PartyModel>();
            objModel.ListConsigner.AddRange(lstConsigner);

            List<PartyModel> lstConsignee = new List<PartyModel>();
            lstConsignee = objService.getConsigner(rid, cid);
            objModel.ListConsignee = new List<PartyModel>();
            objModel.ListConsignee.AddRange(lstConsignee);

            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objService.getVehicle(rid, cid);
            objModel.ListVehicle = new List<VehicleItem>();
            objModel.ListVehicle.AddRange(lstVehicle);
            ViewBag.Menuid = (Request.QueryString["menuid"]);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Edit(ConsignmentModel model)
        {
            ConsignmentService objService = new ConsignmentService();
            int uid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                model.UpdatedBy = uid;
                model.UpdatedDate = System.DateTime.Now;
                objService.Update(model);
            }
            return RedirectToAction("Index", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult View(int id)
        {
            ConsignmentService objService = new ConsignmentService();
            ConsignmentModel objModel = new ConsignmentModel();

            objModel = objService.GetById(id);

            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }

            List<PartyModel> lstConsigner = new List<PartyModel>();
            lstConsigner = objService.getConsigner(rid, cid);
            objModel.ListConsigner = new List<PartyModel>();
            objModel.ListConsigner.AddRange(lstConsigner);

            List<PartyModel> lstConsignee = new List<PartyModel>();
            lstConsignee = objService.getConsigner(rid, cid);
            objModel.ListConsignee = new List<PartyModel>();
            objModel.ListConsignee.AddRange(lstConsignee);

            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objService.getVehicle(rid, cid);
            objModel.ListVehicle = new List<VehicleItem>();
            objModel.ListVehicle.AddRange(lstVehicle);
            ViewBag.Menuid = (Request.QueryString["menuid"]);
            return View(objModel);
        }
        public ActionResult getParty(int SID)
        {
            List<PartyModel> lstVhcl = new List<PartyModel>();
            PartyModel objModel = new PartyModel();
            ConsignmentService objEmp = new ConsignmentService();
            objModel = objEmp.getPartyById(SID);
            return PartialView("_Contact", objModel);
            //return Json(objModel, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Invoice(string id)
        {
            ConsignmentService objService = new ConsignmentService();
            ConsignmentModel objModel = new ConsignmentModel();
            EncDec objEnDec = new EncDec();
            int cid = Convert.ToInt32(objEnDec.Decrypt(id));
            objModel = objService.getGRbyID(cid);

            //List<ChallanModel> lstChallan = new List<ChallanModel>();
            //lstChallan = objService.getChallanTran(id);
            //objModel.ListChallan = new List<ChallanModel>();
            //objModel.ListChallan.AddRange(lstChallan);
            return View(objModel);
        }
    }
}
