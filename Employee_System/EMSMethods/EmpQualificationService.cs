using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSMethods
{
    public class EmpQualificationService
    {

        EmployeeEntities DBcontext = new EmployeeEntities();

        public int Insert(EmpQualificationItem model)
        {
            try
            {
                Mapper.CreateMap<EmpQualificationItem, EmployeeQualification>();
                EmployeeQualification objQual = Mapper.Map<EmployeeQualification>(model);
                DBcontext.EmployeeQualifications.Add(objQual);
                return DBcontext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<EmpQualificationItem> ExperienceListData(int EmpId)
        {
            try
            {
                Mapper.CreateMap<EmployeeQualification, EmpQualificationItem>();
                var objExp = (from EmpQual in DBcontext.EmployeeQualifications
                              join Emp in DBcontext.employee_master on EmpQual.Empid equals Emp.id
                              where EmpQual.Empid == (EmpId == 0 ? EmpQual.Empid : EmpId)
                              select new EmpQualificationItem
                              {
                                  EmpDetails = new EmployeeItem()
                                  {
                                      Empname = Emp.Empname
                                  },
                                  Board = EmpQual.Board,
                                  FromDate = EmpQual.FromDate,
                                  ToDate = EmpQual.ToDate,
                                  SchoolName = EmpQual.SchoolName,
                                  Percentage = EmpQual.Percentage,
                                  Grade = EmpQual.Grade,
                                  CertTitle = EmpQual.CertTitle,
                                  Qid = EmpQual.Qid
                              }
                    ).Distinct().ToList();
                return objExp;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public EmpQualificationItem GetById(int id)
        {
            try
            {
                Mapper.CreateMap<EmployeeQualification, EmpQualificationItem>();
                EmployeeQualification objQual = DBcontext.EmployeeQualifications.SingleOrDefault(m => m.Qid == id);
                EmpQualificationItem objQualItem = Mapper.Map<EmpQualificationItem>(objQual);
                return objQualItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int Update(EmpQualificationItem model)
        {
            Mapper.CreateMap<EmpQualificationItem, EmployeeQualification>();
            EmployeeQualification objQual = DBcontext.EmployeeQualifications.SingleOrDefault(m => m.Qid == model.Qid);
            objQual = Mapper.Map(model, objQual);
            return DBcontext.SaveChanges();
        }

        public List<clsMasterData> GetALLIComp()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DBcontext.Masters_Tran.Where(m => m.MasterId == 3 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
        public List<EmpQualificationItem> GetQualificationDoc(int empid)
        {
            var objLicence = (from doc in DBcontext.EmployeeDocuments
                              where doc.DocCategoryId == 143 && doc.EmpId == empid
                              select new EmpQualificationItem()
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
            return objLicence;
        }
    }
}
