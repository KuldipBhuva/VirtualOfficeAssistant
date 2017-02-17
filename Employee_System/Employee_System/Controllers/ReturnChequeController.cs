using EMSDomain.ViewModel;
using EMSMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee_System.Controllers
{
    public class ReturnChequeController : Controller
    {
        //
        // GET: /Cheque/

        public ActionResult Index()
        {
            ReturnChequeService objService = new ReturnChequeService();
            List<ChequeReturnModel> lstModel = new List<ChequeReturnModel>();
            ChequeReturnModel objModel = new ChequeReturnModel();

            lstModel = objService.GetReturnChq();
            objModel.ListRetChq = new List<ChequeReturnModel>();
            objModel.ListRetChq.AddRange(lstModel);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(ChequeReturnModel model)
        {
            HttpPostedFileBase logo = Request.Files["file"];
            ReturnChequeService objService = new ReturnChequeService();
            model.Status = true;
            if (logo != null)
            {
                var logoPhoto = Path.GetFileName(logo.FileName);

                if (logoPhoto != "")
                {
                    var path = Path.Combine(Server.MapPath("/PhotoUpload/Attachment"), logoPhoto);
                    logo.SaveAs(path);
                    model.Attachment = logoPhoto;
                }
            }

            objService.Insert(model);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ChequeReturnModel model)
        {
            
            ReturnChequeService objService = new ReturnChequeService();
            HttpPostedFileBase logo = Request.Files["file"];
            

            if (logo != null)
            {
                var logoPhoto = Path.GetFileName(logo.FileName);
                if (logoPhoto != "")
                {

                    var path = Path.Combine(Server.MapPath("~/PhotoUpload/Attachment"), logoPhoto);
                    logo.SaveAs(path);
                    model.Attachment = logoPhoto;
                }
            }
         
            objService.Update(model);
            return RedirectToAction("Index", new { @menuId = model.Viewbagidformenu });
        }

        public ActionResult Edit(int id)
        {
            
            ReturnChequeService objService = new ReturnChequeService();
            ChequeReturnModel objModel = new ChequeReturnModel();
            objModel = objService.GetById(id);

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
        public ActionResult View(int id)
        {

            ReturnChequeService objService = new ReturnChequeService();
            ChequeReturnModel objModel = new ChequeReturnModel();
            objModel = objService.GetById(id);

            ViewBag.Menuid = Request.QueryString["menuId"];
            return View(objModel);
        }
    }
}
