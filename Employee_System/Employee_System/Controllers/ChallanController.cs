using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Vehicle;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class ChallanController : Controller
    {
        //
        // GET: /Challan/

        EmployeeEntities db = new EmployeeEntities();
        public ActionResult Delete(int id,int menuid)
        {
            ////int Empid = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            //ChallanService objService = new ChallanService();
            //List<EMSDomain.ViewModel.DocumentItem> lstDoc = new List<EMSDomain.ViewModel.DocumentItem>();
            //ChallanModel objModel = new ChallanModel();
            //objModel = objService.challanTranById(id);
            db.ChallanTrans.Remove(db.ChallanTrans.Find(id));
            db.SaveChanges();         
            ViewBag.Menuid = Request.QueryString["menuId"];

            return RedirectToAction("Index", new {@menuId = Request.QueryString["menuId"] });
        }
        public ActionResult Index()
        {
            ChallanService objService = new ChallanService();
            ChallanModel objModel = new ChallanModel();
            List<ChallanModel> lstChallan = new List<ChallanModel>();

            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }

            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objService.getVehicle(rid,cid);
            objModel.ListVehicle = new List<VehicleItem>();
            objModel.ListVehicle.AddRange(lstVehicle);

            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objService.GetEmp(cid);
            objModel.ListEmp = new List<EmployeeItem>();
            objModel.ListEmp.AddRange(lstEmp);

            List<ConsignmentModel> lstGrNo = new List<ConsignmentModel>();
            lstGrNo = objService.getGRNo(rid,cid);
            objModel.ListGrNo = new List<ConsignmentModel>();
            objModel.ListGrNo.AddRange(lstGrNo);

            lstChallan = objService.getAllChallan(rid, cid);
            objModel.ListChallan = new List<ChallanModel>();
            objModel.ListChallan.AddRange(lstChallan);

            lstChallan = objService.getAllChallanTran();
            objModel.ListChallanTran = new List<ChallanModel>();
            objModel.ListChallanTran.AddRange(lstChallan);

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(ChallanModel model)
        {
            ChallanService objService = new ChallanService();
            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }
            model.CompID = cid;
            model.ChallanNo = "CHLN";
            model.Status = true;
            objService.Insert(model);
            return RedirectToAction("Index", new { @menuId = model.Viewbagidformenu });
        }
        public ActionResult FillEmp()
        {
            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<VehicleItem> lstVhcl = new List<VehicleItem>();
            VehicleItem objVhclModel = new VehicleItem();
            ChallanService objEmp = new ChallanService();
            lstVhcl = objEmp.getVehicle(rid, cid);
            objVhclModel.ListVehicle = new List<VehicleItem>();            
            objVhclModel.ListVehicle.AddRange(lstVhcl);
            return Json(objVhclModel.ListVehicle, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getGRDetail(int GRID)
        {
            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }
            ConsignmentModel objModel = new ConsignmentModel();
            ChallanService objService = new ChallanService();
            objModel = objService.getGR(GRID);
            //return Json(objModel, JsonRequestBehavior.AllowGet);
            return PartialView("_GRDetail", objModel);
        }
        public ActionResult AddGR(int id)
        {
            ChallanService objService = new ChallanService();
            ChallanModel objModel = new ChallanModel();
            List<ChallanModel> lstChallan = new List<ChallanModel>();

            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }

            objModel = objService.GetById(id);

            List<ConsignmentModel> lstGrNo = new List<ConsignmentModel>();
            lstGrNo = objService.getGRNo(rid, cid);
            objModel.ListGrNo = new List<ConsignmentModel>();
            objModel.ListGrNo.AddRange(lstGrNo);

            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objService.getVehicle(rid, cid);
            objModel.ListVehicle = new List<VehicleItem>();
            objModel.ListVehicle.AddRange(lstVehicle);

            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objService.GetEmp(cid);
            objModel.ListEmp = new List<EmployeeItem>();
            objModel.ListEmp.AddRange(lstEmp);

            lstChallan = objService.getChallanTran(id);
            objModel.ListChallan = new List<ChallanModel>();
            objModel.ListChallan.AddRange(lstChallan);
            ViewBag.Menuid = Request.QueryString["menuId"];

            return View(objModel);
        }
        [HttpPost]
        public ActionResult AddGR(ChallanModel model,string cmd)
        {
            ChallanService objService = new ChallanService();
            int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            model.CID = id;
            if (cmd == "Add")
            {
                objService.InsertChallanTran(model);
            }
            else
            {
                objService.Update(model);
            }

            int uid = 0;
            int cid = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                cid = Convert.ToInt32(Session["CompID"].ToString());
                rid = Convert.ToInt32(Session["CompID"].ToString());
            }


            ChallanModel objModel = new ChallanModel();
            List<ChallanModel> lstChallan = new List<ChallanModel>();
            objModel = objService.GetById(id);

            List<ConsignmentModel> lstGrNo = new List<ConsignmentModel>();
            lstGrNo = objService.getGRNo(rid, cid);
            objModel.ListGrNo = new List<ConsignmentModel>();
            objModel.ListGrNo.AddRange(lstGrNo);

            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objService.getVehicle(rid, cid);
            objModel.ListVehicle = new List<VehicleItem>();
            objModel.ListVehicle.AddRange(lstVehicle);

            List<EmployeeItem> lstEmp = new List<EmployeeItem>();
            lstEmp = objService.GetEmp(cid);
            objModel.ListEmp = new List<EmployeeItem>();
            objModel.ListEmp.AddRange(lstEmp);

            lstChallan = objService.getChallanTran(id);
            objModel.ListChallan = new List<ChallanModel>();
            objModel.ListChallan.AddRange(lstChallan);
            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
        public ActionResult PrintChallan(string id)
        {
            ChallanService objService = new ChallanService();
            ChallanModel objModel = new ChallanModel();
            List<ChallanModel> lstChallan = new List<ChallanModel>();
            EncDec objEnDec = new EncDec();
            string cid = objEnDec.Decrypt(id);
         
            objModel = objService.GetById(Convert.ToInt32(cid));

            lstChallan = objService.getChallanTran(Convert.ToInt32(cid));
            objModel.ListChallanTran = new List<ChallanModel>();
            objModel.ListChallanTran.AddRange(lstChallan);
            return View(objModel);
        }
    }
}
