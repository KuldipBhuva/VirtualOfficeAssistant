using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.Tenancy;

namespace EMSMethods
{
    public class TenancyService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();

        public int Insert(TenancyItem model)
        {
            Mapper.CreateMap<TenancyItem, TenancyMaster>();
            TenancyMaster dbTenancy = Mapper.Map<TenancyMaster>(model);
            Dbcontext.TenancyMasters.Add(dbTenancy);
            return Dbcontext.SaveChanges();
        }

        public TenancyItem GetByid(int id)
        {
            Mapper.CreateMap<TenancyMaster, TenancyItem>();
            TenancyMaster dbTenancy = Dbcontext.TenancyMasters.SingleOrDefault(m => m.TnID == id);
            TenancyItem objItem = Mapper.Map<TenancyItem>(dbTenancy);
            return objItem;
        }

        public List<TenancyItem> GetAll(int cid)
        {
            var objTenancy = (from tenancy in Dbcontext.TenancyMasters
                              join mt in Dbcontext.Masters_Tran on tenancy.TnTypeId equals mt.Id into mast
                              from m in mast.DefaultIfEmpty()
                              join comp in Dbcontext.Company_master on tenancy.TnTypeCompId equals comp.id into tcomp
                              from c in tcomp.DefaultIfEmpty()
                              //join Emp in Dbcontext.employee_master on tenancy.TnTypeEmpId equals Emp.id
                              where tenancy.TnTypeCompId==(cid==0?tenancy.TnTypeCompId:cid)
                              select new TenancyItem()
                              {
                                  TnNo = tenancy.TnNo,
                                  Subject = tenancy.Subject,
                                  Status = tenancy.Status,
                                  Address = tenancy.Address,
                                  LandLord = tenancy.LandLord,
                                  FromDate = tenancy.FromDate,
                                  ToDate = tenancy.ToDate,
                                  TnTypeCompId = tenancy.TnTypeCompId,
                                  TnTypeEmpId = tenancy.TnTypeEmpId,
                                  TnType = tenancy.TnType,
                                  TnID = tenancy.TnID,
                                  Others = tenancy.Others,
                                  Period = tenancy.Period,
                                  Rent = tenancy.Rent,
                                  TTDetails = new clsMasterData()
                                  {
                                      Name=(m==null?string.Empty:m.Name)
                                  },
                                  //  Empdetails = new EmployeeItem()
                                  //{
                                  //    id = Emp.id,
                                  //    Empname = Emp.Empname
                                  //},
                                  compDetails = new CompanyItem()
                                  {
                                      CompName=(c==null?string.Empty:c.CompName)                                      
                                  }
                              }
                ).Distinct().ToList();

            return objTenancy;
        }

        public int Update(TenancyItem model)
        {
            TenancyMaster objten = new TenancyMaster();
            objten = Dbcontext.TenancyMasters.SingleOrDefault(m => m.TnID == model.TnID);
            objten.TnNo = model.TnNo;
            objten.TDate = model.TDate;
            objten.Address = model.Address;
            objten.FromDate = model.FromDate;
            objten.ToDate = model.ToDate;
            objten.LandLord = model.LandLord;
            objten.Others = model.Others;
            objten.Period = model.Period;
            objten.Phone = model.Phone;
            objten.Remarks = model.Remarks;
            objten.Rent = model.Rent;
            objten.Status = model.Status;
            objten.Subject = model.Subject;
            objten.TnTypeCompId = model.TnTypeCompId;
            objten.TnTypeEmpId = model.TnTypeEmpId;
            objten.TnTypeId = model.TnTypeId;
            objten.TnType = model.TnType;
            objten.Terms = model.Terms;
            return Dbcontext.SaveChanges();
        }

        public List<CompanyItem> GetCompany()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> lstCompany = Dbcontext.Company_master.ToList();
            List<CompanyItem> objComp = Mapper.Map<List<CompanyItem>>(lstCompany);
            return objComp;
        }
        public List<EmployeeItem> GetEmployee()
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> lstEmployee = Dbcontext.employee_master.Where(m=>m.WorkingStatus!=94).ToList();
            List<EmployeeItem> objEmp = Mapper.Map<List<EmployeeItem>>(lstEmployee);
            return objEmp;
        }


        public string Getid()
        {
            try
            {
                string s = Dbcontext.TenancyMasters.Max(m => m.TnID).ToString();
                return s;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(TenancyItem model)
        {
            TenancyDocument objTenancy = new TenancyDocument();
            objTenancy.FileName = model.FileName;
            objTenancy.FileUrl = model.FileUrl;
            objTenancy.TenId = model.TenId;
            objTenancy.CreatedDate = System.DateTime.Now;
            Dbcontext.TenancyDocuments.Add(objTenancy);
            Dbcontext.SaveChanges();
        }

        public void DocUpdate(TenancyItem model)
        {
            TenancyDocument objTenancy = new TenancyDocument();
            objTenancy = Dbcontext.TenancyDocuments.SingleOrDefault(m => m.TenId == model.TenId);

            objTenancy.FileName = model.FileName;
            objTenancy.FileUrl = model.FileUrl;
            objTenancy.TenId = model.TenId;
            objTenancy.CreatedDate = System.DateTime.Now;

            Dbcontext.SaveChanges();
        }
        //For Documents

        public List<TenancyItem> getTenancyDoc(int id)
        {
            var objTenancy = (from Tenancy in Dbcontext.TenancyMasters
                              join tenDoc in Dbcontext.TenancyDocuments on Tenancy.TnID equals tenDoc.TenId
                              where tenDoc.TenId == id
                              select new TenancyItem()
                              {

                                  FileName = tenDoc.FileName,
                                  FileUrl = tenDoc.FileUrl,
                                  TenId = tenDoc.TenId,
                                  TenDocId = tenDoc.TenDocId
                              }
                ).Distinct().ToList();

            return objTenancy;
        }


        //start  for get id if exists on table tenancydocuments
        public int? gettendocid(int? id)
        {
            var a = (from data in Dbcontext.TenancyDocuments
                     where data.TenId == id
                     select new
                     {
                         data.TenId
                     }
                ).SingleOrDefault();
            if (a != null)
            {
                return a.TenId;
            }
            else
            {
                return 0;
            }
        }

        //end
        public TenancyDocumentItem getByID(int id)
        {
            Mapper.CreateMap<TenancyDocument, TenancyDocumentItem>();
            TenancyDocument objDoc = Dbcontext.TenancyDocuments.SingleOrDefault(m => m.TenDocId == id);
            TenancyDocumentItem objDocItem = Mapper.Map<TenancyDocumentItem>(objDoc);
            Dbcontext.TenancyDocuments.Remove(objDoc);            
            return objDocItem;
             
        }

        public List<TenancyItem> GetTypeCompany(int? Compid)
        {
            var objComp = (from Tenancy in Dbcontext.TenancyMasters
                           join comp in Dbcontext.Company_master on Tenancy.TnTypeId equals comp.id
                           where comp.id == Compid
                           select new TenancyItem()
                           {
                               compDetails = new CompanyItem()
                               {
                                   id = comp.id,
                                   CompName = comp.CompName
                               }
                           }
                ).Distinct().ToList();

            return objComp;
        }
        public List<TenancyItem> GetTypeEmpid(int? Employeeid)
        {
            var objComp = (from Tenancy in Dbcontext.TenancyMasters
                           join Emp in Dbcontext.employee_master on Tenancy.TnTypeId equals Emp.id
                           where Emp.id == Employeeid
                           select new TenancyItem()
                           {
                               Empdetails = new EmployeeItem()
                                {
                                    id = Emp.id,
                                    Empname = Emp.Empname
                                }
                           }
                ).Distinct().ToList();

            return objComp;
        }
        public List<clsMasterData> GetTenancyType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstEmirates = Dbcontext.Masters_Tran.Where(m => m.MasterId == 45 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstEmirates);
            return lstData;
        }
        //public string Encrypt(string plainText)
        //{
        //    string key = "jdsg432387#";
        //    byte[] EncryptKey = { };
        //    byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
        //    EncryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    byte[] inputByte = Encoding.UTF8.GetBytes(plainText);
        //    MemoryStream mStream = new MemoryStream();
        //    CryptoStream cStream = new CryptoStream(mStream, des.CreateEncryptor(EncryptKey, IV), CryptoStreamMode.Write);
        //    cStream.Write(inputByte, 0, inputByte.Length);
        //    cStream.FlushFinalBlock();
        //    return Convert.ToBase64String(mStream.ToArray());
        //}

        //public string Decrypt(string encryptedText)
        //{
        //    string key = "jdsg432387#";
        //    byte[] DecryptKey = { };
        //    byte[] IV = { 55, 34, 87, 64, 87, 195, 54, 21 };
        //    byte[] inputByte = new byte[encryptedText.Length];

        //    DecryptKey = System.Text.Encoding.UTF8.GetBytes(key.Substring(0, 8));
        //    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //    inputByte = Convert.FromBase64String(encryptedText);
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(DecryptKey, IV), CryptoStreamMode.Write);
        //    cs.Write(inputByte, 0, inputByte.Length);
        //    cs.FlushFinalBlock();
        //    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //    return encoding.GetString(ms.ToArray());
        //}

    }
}
