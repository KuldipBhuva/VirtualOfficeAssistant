using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMSDomain.ViewModel;
using EMSDomain.Model;
using AutoMapper;

namespace EMSMethods
{
    public class clsBloodGroupMethod
    {
        EmployeeEntities dbContext = new EmployeeEntities();

        public int Insert(BloodGroupItem Model)
        {
            Mapper.CreateMap<BloodGroupItem, Blood_Group_Master>();
            Blood_Group_Master objbg = Mapper.Map<Blood_Group_Master>(Model);
            dbContext.Blood_Group_Master.Add(objbg);
            return dbContext.SaveChanges();
        }

        public List<BloodGroupItem> Getall()
        {
            Mapper.CreateMap<Blood_Group_Master, BloodGroupItem>();
            List<Blood_Group_Master> objBG = dbContext.Blood_Group_Master.ToList();
            List<BloodGroupItem> objBGP = Mapper.Map<List<BloodGroupItem>>(objBG);
            return objBGP;
        }


    }
}
