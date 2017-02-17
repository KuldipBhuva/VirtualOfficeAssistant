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
    public class PetrolCardController : Controller
    {
        //
        // GET: /PetrolCard/

        public ActionResult Create()
        {
            List<PetrolCardItem> lstPCard = new List<PetrolCardItem>();
            PetrolCardService objPCard = new PetrolCardService();
            lstPCard = objPCard.PetrolCardListData();
            PetrolCardItem objPCardItem = new PetrolCardItem();
            objPCardItem.ListPetrolCard = new List<PetrolCardItem>();
            objPCardItem.ListPetrolCard.AddRange(lstPCard);

            #region DDL Vehicle
            List<VehicleItem> lstVehicle = new List<VehicleItem>();
            lstVehicle = objPCard.GetVehicle();
            objPCardItem.ListVehicle = new List<VehicleItem>();
            objPCardItem.ListVehicle.AddRange(lstVehicle);
            #endregion

            #region DDL PaymentType
            List<clsMasterData> lstMasters1 = new List<clsMasterData>();
            lstMasters1 = objPCard.GetPaymentType();
            objPCardItem.ListMaster = new List<clsMasterData>();
            objPCardItem.ListMaster.AddRange(lstMasters1);

            #endregion
            #region DDL PCardType
            List<clsMasterData> lstMasters = new List<clsMasterData>();
            lstMasters = objPCard.GetPCardType();
            objPCardItem.ListMaster1 = new List<clsMasterData>();
            objPCardItem.ListMaster1.AddRange(lstMasters);

            #endregion

            return View(objPCardItem);
        }
        [HttpPost]
        public ActionResult Create(PetrolCardItem model)
        {          
            PetrolCardService objPCardService = new PetrolCardService();
            model.Status = "Active";
            objPCardService.Insert(model);
            return RedirectToAction("Create");
        }

    }
}
