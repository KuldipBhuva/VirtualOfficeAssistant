using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class EmpAllowancesService
    {
        EmployeeEntities DBcontext = new EmployeeEntities();


        public int Insert(EmpAllowncesItem model)
        {
            try
            {
                Mapper.CreateMap<EmpAllowncesItem, EmployeeAllowance>();
                EmployeeAllowance objAllow = Mapper.Map<EmployeeAllowance>(model);
                DBcontext.EmployeeAllowances.Add(objAllow);
                return DBcontext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<EmpAllowncesItem> AllowancesListData(int EmpId)
        {
            try
            {
                Mapper.CreateMap<EmployeeAllowance, EmpAllowncesItem>();
                var objExp = (from EmpAll in DBcontext.EmployeeAllowances
                              join Emp in DBcontext.employee_master on EmpAll.EmpId equals Emp.id
                              join Mst in DBcontext.Masters_Tran on EmpAll.AllownaceID equals Mst.Id
                              where EmpAll.EmpId == (EmpId == 0 ? EmpAll.EmpId : EmpId)
                              select new EmpAllowncesItem
                              {
                                  EmpDetails = new EmployeeItem()
                                  {
                                      Empname = Emp.Empname
                                  },
                                  Amount = EmpAll.Amount,
                                  Remarks = EmpAll.Remarks,
                                  Status = EmpAll.Status,
                                  AllownaceID = EmpAll.AllownaceID,
                                  EmpId = EmpAll.EmpId,
                                  AId = EmpAll.AId,
                                  MasterDetails = new clsMasterData()
                                  {
                                      MasterId = Mst.MasterId,
                                      Name = Mst.Name,
                                      Id = Mst.Id
                                  }


                              }
                    ).Distinct().ToList();
                return objExp;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public EmpAllowncesItem GetById(int id)
        {
            try
            {
                Mapper.CreateMap<EmployeeAllowance, EmpAllowncesItem>();
                EmployeeAllowance objAllow = DBcontext.EmployeeAllowances.SingleOrDefault(m => m.AId == id);
                EmpAllowncesItem objAllowItem = Mapper.Map<EmpAllowncesItem>(objAllow);
                return objAllowItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int Update(EmpAllowncesItem model)
        {
            Mapper.CreateMap<EmpAllowncesItem, EmployeeAllowance>();
            EmployeeAllowance objAllow = DBcontext.EmployeeAllowances.SingleOrDefault(m => m.AId == model.AId);
            objAllow = Mapper.Map(model, objAllow);
            return DBcontext.SaveChanges();
        }

        public List<clsMasterData> GetALLAllowanceId()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DBcontext.Masters_Tran.Where(m => m.MasterId == 7 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
