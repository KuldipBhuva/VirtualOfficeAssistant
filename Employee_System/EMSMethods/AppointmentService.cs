using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;

namespace EMSMethods
{
    public class AppointmentService
    {
        EmployeeEntities DbContext = new EmployeeEntities();
        public int Insert(AppointmentsItem model)
        {
            try
            {
                Mapper.CreateMap<AppointmentsItem, AppointmentsMaster>();
                AppointmentsMaster objAppoint = Mapper.Map<AppointmentsMaster>(model);
                DbContext.AppointmentsMasters.Add(objAppoint);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<AppointmentsItem> getAppointmentData(int cid)
        {
            var data = (from AM in DbContext.AppointmentsMasters
                        where AM.CompID == (cid == 0 ? AM.CompID : cid)
                        select new AppointmentsItem()
                        {
                            ID = AM.ID,
                            Subject = AM.Subject,
                            Status = AM.Status,
                            StDt = AM.StDt,
                            StTime = AM.StTime,
                            EndDt = AM.EndDt,
                            EndTime = AM.EndTime,
                            Priority = AM.Priority,
                            Reminder = AM.Reminder,
                            ReDate = AM.ReDate,
                            ReTime = AM.ReTime,
                            Remarks = AM.Remarks
                        }
                          ).Distinct().ToList();
            return data;
        }
        public int Update(AppointmentsItem model)
        {
            Mapper.CreateMap<AppointmentsItem, AppointmentsMaster>();
            AppointmentsMaster objAppoint = DbContext.AppointmentsMasters.SingleOrDefault(m => m.ID == model.ID);
            objAppoint = Mapper.Map(model, objAppoint);
            return DbContext.SaveChanges();
        }
        public AppointmentsItem GetById(int id)
        {
            Mapper.CreateMap<AppointmentsMaster, AppointmentsItem>();
            AppointmentsMaster objAppoint = DbContext.AppointmentsMasters.SingleOrDefault(m => m.ID == id);
            AppointmentsItem objAppointItem = Mapper.Map<AppointmentsItem>(objAppoint);
            return objAppointItem;
        }
    }
}
