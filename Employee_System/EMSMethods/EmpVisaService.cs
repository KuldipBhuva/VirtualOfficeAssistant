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
    public class EmpVisaService
    {
        EmployeeEntities DbContext = new EmployeeEntities();

        public int Insert(EmpVisaItem model)
        {
            try
            {
                Mapper.CreateMap<EmpVisaItem, EmployeeVisa>();
                EmployeeVisa objVisa = Mapper.Map<EmployeeVisa>(model);
                DbContext.EmployeeVisas.Add(objVisa);
                return DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public List<EmpVisaItem> VisaListData(int EmpId)
        {
            try
            {
                Mapper.CreateMap<EmployeeVisa, EmpVisaItem>();
                var objVisa = (from EmpVisa in DbContext.EmployeeVisas
                               join Emp in DbContext.employee_master on EmpVisa.EmpId equals Emp.id
                               join mt in DbContext.Masters_Tran on EmpVisa.IssueCountry equals mt.Id into p
                               from p1 in p.DefaultIfEmpty()
                               where EmpVisa.EmpId == (EmpId == 0 ? EmpVisa.EmpId : EmpId)
                               select new EmpVisaItem
                               {
                                   ConDetails=new clsMasterData()
                                   {
                                       Name=p1.Name
                                   },
                                   EmpDetails = new EmployeeItem()
                                   {
                                       Empname = Emp.Empname
                                   },
                                   
                                   EmpId = EmpVisa.EmpId,
                                   VisaNo = EmpVisa.VisaNo,
                                   VisaGroupId = EmpVisa.VisaGroupId,
                                   VisaStatusId = EmpVisa.VisaStatusId,
                                   VisaTypeId = EmpVisa.VisaTypeId,
                                   VId = EmpVisa.VId,
                                   IssueCountry = EmpVisa.IssueCountry,
                                   IssueDate = EmpVisa.IssueDate,
                                   ExpiryDate = EmpVisa.ExpiryDate,
                                   ReniewDate = EmpVisa.ReniewDate,
                                   Remarks = EmpVisa.Remarks,
                                   Status = EmpVisa.Status,
                                   PlaceOfIssue = EmpVisa.PlaceOfIssue,
                                   ComID=EmpVisa.ComID,
                                   Designation=EmpVisa.Designation
                               }
                    ).Distinct().ToList();
                return objVisa;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public EmpVisaItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeVisa, EmpVisaItem>();
            EmployeeVisa objVisa = DbContext.EmployeeVisas.SingleOrDefault(m => m.VId == id);
            EmpVisaItem objVisaItem = Mapper.Map<EmpVisaItem>(objVisa);
            return objVisaItem;
        }

        public int Update(EmpVisaItem model)
        {
            Mapper.CreateMap<EmpVisaItem, EmployeeVisa>();
            EmployeeVisa objVisa = DbContext.EmployeeVisas.SingleOrDefault(m => m.VId == model.VId);
            objVisa = Mapper.Map(model, objVisa);
            return DbContext.SaveChanges();
        }

        public List<clsMasterData> GetVisaStatus()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstMastertran = DbContext.Masters_Tran.Where(m => m.MasterId == 29 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstMastertran);
            return lstData;
        }
        public List<clsMasterData> GetVisaType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstMastertran = DbContext.Masters_Tran.Where(m => m.MasterId == 30 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstMastertran);
            return lstData;
        }
        public List<EmpVisaItem> GetVisaDoc(int empid)
        {
            var objVisa = (from doc in DbContext.EmployeeDocuments
                           //where doc.DocCategoryId == 385 && doc.EmpId == empid
                           where doc.DocCategoryId == 144 && doc.EmpId == empid
                           select new EmpVisaItem()
                           {
                               DocDetails = new EMSDomain.ViewModel.DocumentItem()
                                {
                                    EmpId = doc.EmpId,
                                    FileName = doc.FilleName,
                                    FileUrl = doc.FileUrl,
                                    Id = doc.Id
                                }
                           }

                ).Distinct().ToList();
            return objVisa;
        }

        public List<clsMasterData> GetEmiratesStatus()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstMastertran = DbContext.Masters_Tran.Where(m => m.MasterId == 6).ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstMastertran);
            return lstData;
        }
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DbContext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
