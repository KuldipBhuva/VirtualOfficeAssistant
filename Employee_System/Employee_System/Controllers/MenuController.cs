using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSDomain.ViewModel.Menu;
using EMSMethods;

namespace Employee_System.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        //public ActionResult Menu()
        //{
        //    MenuItem objMenuItem = new MenuItem();
        //    MenuService objMenuService = new MenuService();
        //    List<MenuItem> objListMenu = new List<MenuItem>();
        //    objListMenu = objMenuService.GetMenu();
        //    objMenuItem.MenuItemList = new List<MenuItem>();
        //    objMenuItem.MenuItemList.AddRange(objListMenu);
        //    return PartialView();

        //}
        public ActionResult Menu()
        {
            UserModulesItem objMenuItem = new UserModulesItem();
            MenuService objMenuService = new MenuService();
            List<UserModulesItem> objListMenu = new List<UserModulesItem>();
            int UID = Convert.ToInt32(Session["UserId"].ToString());
            objListMenu = objMenuService.GetModules(UID);
            objMenuItem.MenuItemList = new List<UserModulesItem>();
            objMenuItem.MenuItemList.AddRange(objListMenu);
            return PartialView();

        }
        public PartialViewResult LeftMenu(int? menuId)
        {
            //if (menuId == null)
            //{
            //    menuId = Convert.ToInt32(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["menuid"]);
            //}
            int mid = Convert.ToInt32(Request.QueryString["Mid"]);
            List<FormItem> lstForm = new List<FormItem>();
            MenuService menuService = new MenuService();

            lstForm = menuService.GetFormMenu().Where(m => m.MenuWiseForm.MHID == (menuId == null ? 1 : menuId)).ToList();
            if (Url.RequestContext.RouteData.Values["id"] != null)
            {
                string qrystring = Url.RequestContext.RouteData.Values["id"].ToString();
                ViewBag.EmpId = qrystring;

            }

            ViewBag.MenuId = (menuId == null ? 1 : menuId);

            return PartialView("_LeftMenu", lstForm);

        }

    }
}
