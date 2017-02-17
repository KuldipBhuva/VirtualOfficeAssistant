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
    public class EmpContactService
    {
        EmployeeEntities DBContext = new EmployeeEntities();

        public int Insert(EmpContactItem model)
        {
            try
            {

                Mapper.CreateMap<EmpContactItem, EmployeeContact>();
                EmployeeContact objempContact = Mapper.Map<EmployeeContact>(model);
                DBContext.EmployeeContacts.Add(objempContact);
                return DBContext.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public EmpContactItem GetById(int id)
        {
            Mapper.CreateMap<EmployeeContact, EmpContactItem>();
            EmployeeContact objEmpContct = DBContext.EmployeeContacts.SingleOrDefault(m => m.Id == id);
            EmpContactItem objContactItem = Mapper.Map<EmpContactItem>(objEmpContct);
            return objContactItem;
        }

        public int Update(EmpContactItem model)
        {
            Mapper.CreateMap<EmpContactItem, EmployeeContact>();
            EmployeeContact objEmpcontact = DBContext.EmployeeContacts.SingleOrDefault(m => m.Id == model.Id);
            objEmpcontact = Mapper.Map(model, objEmpcontact);
            return DBContext.SaveChanges();
        }

        public List<EmpContactItem> GetContactDetails(int? Empid)
        {
            //Mapper.CreateMap<EmployeeContact, EmpContactItem>();
            //List<EmployeeContact> objEmpContact = DBContext.EmployeeContacts.Where(m => m.Id == (Empid == 0 ? m.Id : Empid)).ToList();
            //List<EmpContactItem> objEmpContItem = Mapper.Map<List<EmpContactItem>>(objEmpContact);
            //return objEmpContItem;

            var a = (from EmpContact in DBContext.EmployeeContacts
                     join Emp in DBContext.employee_master on EmpContact.EmpId equals Emp.id
                     join md in DBContext.Masters_Tran on EmpContact.Country equals md.Id into m
                     from m1 in m.DefaultIfEmpty()
                     where EmpContact.EmpId == (Empid == 0 ? EmpContact.EmpId : Empid)
                     // where EmpContact.EmpId == Empid
                     select new EmpContactItem()
                     {
                         ConDetails=new clsMasterData()
                         {
                             Name=m1.Name
                         },
                         EmployeeDetails = new EmployeeItem()
                         {
                             Empname = Emp.Empname,
                         },
                         
                         Area = EmpContact.Area,
                         City = EmpContact.City,
                         Pincode = EmpContact.Pincode,
                         State = EmpContact.State,
                         EmpId = EmpContact.EmpId,
                         Id = EmpContact.Id
                     }
                ).Distinct().ToList();
            return a;
        }

        public List<EmpContactItem> GetDocument(int? Empid)
        {
            var objDoc = (from doc in DBContext.EmployeeDocuments
                          where doc.DocCategoryId == 2 && doc.EmpId == Empid
                          select new EmpContactItem()
                          {
                              DocDetails = new EMSDomain.ViewModel.DocumentItem()
                              {
                                  FileName = doc.FilleName,
                                  FileUrl = doc.FileUrl,
                                  DocCategoryId = doc.DocCategoryId,
                                  EmpId = doc.EmpId,
                                  Id = doc.Id
                              }
                          }
                ).Distinct().ToList();
            return objDoc;
        }

        public string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
        public List<clsMasterData> getCountry()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = DBContext.Masters_Tran.Where(m => m.MasterId == 2 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;

        }
    }
}
