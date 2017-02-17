using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel;
using EMSDomain.Model;
using EMSMethods;
using AutoMapper;

namespace EMSMethods
{
    public class clsLoginMethod
    {

        EmployeeEntities dbcontext = new EmployeeEntities();

        public LoginItem GetDetails(string UserName, String Password)
        {
            Mapper.CreateMap<Login_Master, LoginItem>();
            Login_Master Login = dbcontext.Login_Master.FirstOrDefault(m => m.UserName == UserName && m.Password == Password);
            LoginItem objLogin = Mapper.Map<LoginItem>(Login);
            return objLogin;
        }






    }
}
