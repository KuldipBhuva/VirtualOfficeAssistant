using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Employee;
using EMSMethods;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Branch;

namespace Employee_System.Controllers
{
    public class WorkHistoryController : Controller
    {
        //
        // GET: /WorkHistory/
        WorkHistoryService objService = new WorkHistoryService();

        public ActionResult Index()
        {
            int id = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            WorkHistoryService objWorkHistoryService = new WorkHistoryService();
            var obj = objWorkHistoryService.GetByEmpId(id);
            return View(obj);
        }
        public ActionResult Create()
        {
            // For Bind Company 
            #region Company Bind
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL();

            EmpWorkHistoryItem objEmpWorkHistoryItem = new EmpWorkHistoryItem();
            objEmpWorkHistoryItem.CompanyList = new List<CompanyItem>();
            objEmpWorkHistoryItem.CompanyList.AddRange(objCompany);

            #endregion

            //For Bind Branch 
            #region  Bind Branch
            List<BranchItem> objbranchitm = new List<BranchItem>();
            BranchService objbranchservice = new BranchService();
            objbranchitm = objbranchservice.GetAll();

            //objEmpItem = new EmployeeItem();
            objEmpWorkHistoryItem.BranchList = new List<BranchItem>();
            objEmpWorkHistoryItem.BranchList.AddRange(objbranchitm);

            // for designation ddl
            clsMasterDataMethod objclsMasterDataMethod = new clsMasterDataMethod();
            objEmpWorkHistoryItem.DesignationList = objclsMasterDataMethod.GetAllDesignation();
            objEmpWorkHistoryItem.EmpId = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"].ToString());
            #endregion

            return View(objEmpWorkHistoryItem);
        }
        [HttpPost]
        public ActionResult Create(EmpWorkHistoryItem model)
        {

            if (ModelState.IsValid)
            {
               // model.Status = "Active";
                //model.CreatedBy = Convert.ToString(Empid);
                //model.CreatedDate = System.DateTime.Now;
                objService.Insert(model);
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            // For Bind Company 
            #region Company Bind
            List<CompanyItem> objCompany = new List<CompanyItem>();
            CompanyService objListCompany = new CompanyService();
            objCompany = objListCompany.GetALL(); 

            EmpWorkHistoryItem objEmpWorkHistoryItem = new EmpWorkHistoryItem();
            objEmpWorkHistoryItem.CompanyList = new List<CompanyItem>();
            objEmpWorkHistoryItem.CompanyList.AddRange(objCompany);

            #endregion

            //For Bind Branch 
            #region  Bind Branch
            List<BranchItem> objbranchitm = new List<BranchItem>();
            BranchService objbranchservice = new BranchService();
            objbranchitm = objbranchservice.GetAll();

            //objEmpItem = new EmployeeItem();
            objEmpWorkHistoryItem.BranchList = new List<BranchItem>();
            objEmpWorkHistoryItem.BranchList.AddRange(objbranchitm);

            // for designation ddl
            WorkHistoryService objWorkHistoryService = new WorkHistoryService();
            objEmpWorkHistoryItem = objWorkHistoryService.GetByPk(id);
            #endregion

            return View(objEmpWorkHistoryItem);
        }
        [HttpPost]
        public ActionResult Edit(EmpWorkHistoryItem model)
        {
            if (ModelState.IsValid)
            {
                objService.Update(model);
            }
            return View();
        }
    }
}
