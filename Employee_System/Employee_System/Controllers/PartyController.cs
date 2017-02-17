using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel;
using EMSMethods;


namespace Employee_System.Controllers
{
    public class PartyController : Controller
    {
        //
        // GET: /Party/
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult Create()
        {

            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            List<CompanyItem> lstComp = new List<CompanyItem>();
            List<PartyModel> lstparty1 = new List<PartyModel>();
            OtherExpService objOExp = new OtherExpService();
            PartyModel objParty = new PartyModel();
            PartyServices objPartys = new PartyServices();
            objParty.ListParty = new List<PartyModel>();
            lstparty1 = objPartys.bindList(cid);
            objParty.ListParty = new List<PartyModel>();
            objParty.ListParty.AddRange(lstparty1);

               

            #region Bind DropDown Comp
            List<PartyModel> lstparty = new List<PartyModel>();
            lstComp = objOExp.GetAllComp();
            objParty.CompanyList = new List<CompanyItem>();
            objParty.CompanyList.AddRange(lstComp);
            #endregion


            #region Bind DropDown Party


            lstparty = objPartys.BindParentPartyAll(cid);
            objParty.ListPartyDrp = new List<PartyModel>();
            objParty.ListPartyDrp.AddRange(lstparty);
            #endregion
            ViewBag.Menuid = (Request.QueryString["menuid"]);
            return View(objParty);
        }
        [HttpPost]
        public ActionResult Create(PartyModel Model)
        {
            PartyModel objCatItem = new PartyModel();
            PartyServices objParty = new PartyServices();
            int cid = 0;
            string uid = null;
            if (Session["UserId"] != null)
            {
                uid = Session["UserId"].ToString();
            }
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            if (Model.CompID == null)
            {
                Model.CompID = cid;
            }
            Model.Status = true;
            int i = objParty.InsertParty(Model);
            return RedirectToAction("Create", new { @menuId = Model.Viewbagidformenu });
        }

        public ActionResult FillVData()
        {
            List<PartyModel> lstParty = new List<PartyModel>();
            PartyServices objParty = new PartyServices();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            lstParty = objParty.bindList(cid);
            PartyModel objpartyItem = new PartyModel();
            objpartyItem.ListParty = new List<PartyModel>();
            objpartyItem.ListParty.AddRange(lstParty);
            return PartialView("_Grid", objpartyItem.ListParty);
        }
        [HttpPost]
        public ActionResult Edit(PartyModel model)
        {
            PartyServices objPservice = new PartyServices();
            model.CompID = Convert.ToInt32(Session["CompID"].ToString());
            objPservice.Update(model);
            return RedirectToAction("Create", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            OtherExpService objOExp = new OtherExpService();
            PartyServices objParty = new PartyServices();
            PartyModel  objpartyItem = new PartyModel();
            List<PartyModel> lstparty = new List<PartyModel>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            objpartyItem = objParty.GetById(id);
            objpartyItem.ListParty = new List<PartyModel>();
            objpartyItem.ListParty.AddRange(lstparty);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objOExp.GetAllComp();
            objpartyItem.CompanyList = new List<CompanyItem>();
            objpartyItem.CompanyList.AddRange(lstComp);
            #endregion

            #region Bind DropDown Party

            lstparty = objParty.BindParentParty(cid,id);
            objpartyItem.ListPartyDrp = new List<PartyModel>();
            objpartyItem.ListPartyDrp.AddRange(lstparty);

            #endregion
            ViewBag.Menuid = (Request.QueryString["menuid"]);

            return View(objpartyItem);
        }


        public ActionResult View(int id)
        {
            OtherExpService objOExp = new OtherExpService();
            PartyServices objParty = new PartyServices();
            PartyModel objpartyItem = new PartyModel();
            List<PartyModel> lstparty = new List<PartyModel>();
            int cid = 0;
            if (Session["CompID"] != null)
            {
                cid = Convert.ToInt32(Session["CompID"].ToString());
            }
            objpartyItem = objParty.GetById(id);
            objpartyItem.ListParty = new List<PartyModel>();
            objpartyItem.ListParty.AddRange(lstparty);

            #region Bind DropDown Comp
            List<CompanyItem> lstComp = new List<CompanyItem>();
            lstComp = objOExp.GetAllComp();
            objpartyItem.CompanyList = new List<CompanyItem>();
            objpartyItem.CompanyList.AddRange(lstComp);
            #endregion

            #region Bind DropDown Party

            lstparty = objParty.BindParentParty(cid, id);
            objpartyItem.ListPartyDrp = new List<PartyModel>();
            objpartyItem.ListPartyDrp.AddRange(lstparty);

            #endregion
           
       
            return View(objpartyItem);
        }
    }


}
