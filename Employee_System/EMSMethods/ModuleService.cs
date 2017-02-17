using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel;
using EMSDomain.Model;
using AutoMapper;
using EMSMethods;

namespace EMSMethods
{
    public class ModuleService
    {
        EmployeeEntities dbContext = new EmployeeEntities();
        //public List<ModuleItem> GetALL()
        //{
        //    //Mapper.CreateMap<Module, ModuleItem>();
        //    //List<Module> lstModule = dbContext.Modules.Where(m => m.Status == true).ToList();
        //    //List<ModuleItem> lstModuleItem = Mapper.Map<List<ModuleItem>>(lstModule);
        //    //return lstModuleItem;
        //}
    }
}
