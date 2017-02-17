using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Menu;

namespace EMSMethods
{
    public class MenuService
    {
        EmployeeEntities DBContext = new EmployeeEntities();

        public List<MenuItem> GetMenu()
        {
            Mapper.CreateMap<MenuHeaderMaster, MenuItem>();
            List<MenuHeaderMaster> objMenuhader = DBContext.MenuHeaderMasters.Where(m => m.Status == "Active").ToList();
            List<MenuItem> objMenuItem = Mapper.Map<List<MenuItem>>(objMenuhader);
            return objMenuItem;
        }
        public List<UserModulesItem> GetModules(int UID)
        {
            var data = (from umt in DBContext.UserModulesTrans
                        join mhm in DBContext.MenuHeaderMasters on umt.MHID equals mhm.MHId
                        where umt.IsChecked==true && umt.UID==UID
                        select new UserModulesItem()
          {
              MHID = umt.MHID,
              UHTID = umt.UHTID,
              UID = umt.UID,
              IsChecked =umt.IsChecked,
              MenuHeaderDetails = new MenuItem()
              {
                  MHId = mhm.MHId,
                  MHName = mhm.MHName
              }
          }).ToList();
            return data;
            //Mapper.CreateMap<MenuHeaderMaster, UserModulesItem>();
            //List<UserModulesTran> objMenuhader = DBContext.UserModulesTrans.Where(m => m.Status == "Active").ToList();
            //List<UserModulesItem> objMenuItem = Mapper.Map<List<UserModulesItem>>(objMenuhader);
            //return objMenuItem;
        }
        public List<FormItem> getForms(int id,int mhid)
        {
            var a = (
                   from um in DBContext.UserFormTrans
                   join fm in DBContext.FormMasters on um.FormID equals fm.FormId
                   join mt in DBContext.MenuTrans on fm.FormId equals mt.FormId
                   where um.UID == id && um.IsChecked==true && mt.MHId==mhid
                   select new FormItem()
                   {
                       MHId=mt.MHId,
                       UFormDetails = new UserFormTranItem()
                       {
                           UMTID = um.UMTID,
                           MID = um.MID,
                           UID = um.UID,
                           IsChecked = um.IsChecked,
                           FormID = um.FormID,
                       },
                       FormId = fm.FormId,
                       IsChecked = um.IsChecked,
                       FormName = fm.FormName,
                       FormTitle = fm.FormTitle,
                       FormUrl = fm.FormUrl,

                   }).ToList();

            return a;
        }
        public List<FormItem> GetFormMenu()
        {
            var a = (
                   from MenuTrans in DBContext.MenuTrans
                   join Menu in DBContext.MenuHeaderMasters on MenuTrans.MHId equals Menu.MHId
                   join Form in DBContext.FormMasters on MenuTrans.FormId equals Form.FormId

                   select new FormItem()
                   {
                       FormName = Form.FormName,
                       FormId = Form.FormId,
                       FormUrl = Form.FormUrl,
                       MHId=MenuTrans.MHId,
                       MenuWiseForm = new MenuWiseFormItem()
                       {
                           MHID = MenuTrans.MHId

                       }

                   }

                ).ToList();

            return a;
        }




        public static List<MenuHeaderMaster> GetMenus1()
        {
            using (var context = new EmployeeEntities())
            {
                return context.MenuHeaderMasters.ToList();
            }
        }

        public static List<FormMaster> GetFormByMenu()
        {
            using (var context = new EmployeeEntities())
            {
                return context.FormMasters.ToList();
            }
        }



    }
}
