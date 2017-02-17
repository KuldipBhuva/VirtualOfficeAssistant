using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;
using EMSDomain.ViewModel.TradeLicense;

namespace EMSMethods
{
    public class DocumentService
    {
        EmployeeEntities dbContext = new EmployeeEntities();
        public void Save(DocumentItem model)
        {
            EmployeeDocument obj = new EmployeeDocument();
            obj.EmpId = model.EmpId;
            obj.FilleName = model.FileName;
            obj.DocCategoryId = model.DocCategoryId;
            obj.FileUrl = model.FileUrl;
            dbContext.EmployeeDocuments.Add(obj);
            dbContext.SaveChanges();
        }
        public void Update(DocumentItem model)
        {
            EmployeeDocument obj = dbContext.EmployeeDocuments.Where(m => m.Id == model.Id).SingleOrDefault();
            obj.EmpId = model.EmpId;
            obj.FilleName = model.FileName;
            obj.DocCategoryId = model.DocCategoryId;
            obj.FileUrl = model.FileUrl;
            dbContext.SaveChanges();
        }
        public DocumentItem GetById(int id)
        {
            EmployeeDocument model = dbContext.EmployeeDocuments.Where(m => m.Id == id).SingleOrDefault();
            DocumentItem obj = new DocumentItem();
            obj.EmpId = model.EmpId;
            obj.FileName = model.FilleName;
            obj.DocCategoryId = model.DocCategoryId;
            obj.FileUrl = model.FileUrl;
            return obj;
        }
        public List<clsMasterData> GetDocuments()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstMastertran = dbContext.Masters_Tran.Where(m => m.MasterId == 13 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstMastertran);
            return lstData;
        }

        public List<clsMasterData> GetDoc()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> tblMaster = dbContext.Masters_Tran.Where(m => m.MasterId == 13 && m.Status == "Active").ToList();
            List<clsMasterData> lstmasterdata = Mapper.Map<List<clsMasterData>>(tblMaster);
            return lstmasterdata;
        }
        public List<EMSDomain.ViewModel.DocumentItem> getAllDocByEmpID(int id)
        {
            var data = (from DM in dbContext.EmployeeDocuments
                        join EM in dbContext.employee_master on DM.EmpId equals EM.id
                        where DM.EmpId == id
                        select new EMSDomain.ViewModel.DocumentItem()
                        {
                            EmpDetails = new EmployeeItem()
                            {
                                Empname = EM.Empname
                            },
                            EmpId = DM.EmpId,
                            Id = DM.Id,
                            FileName = DM.FilleName,
                            FileUrl = DM.FileUrl
                        }).ToList();
            return data;
        }
        //Trade Documents
        public List<DocumentItem> getTradeDocByComp(int compID)
        {
            var data = (from tl in dbContext.Trade_License
                        join t in dbContext.TradeDocuments on tl.TID equals t.TradeId into t
                        from td in t.DefaultIfEmpty()
                        join cm in dbContext.Company_master on tl.CompID equals cm.id into c
                        from comp in c.DefaultIfEmpty()
                        where tl.CompID == compID
                        select new DocumentItem()
                        {
                            //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                            FileName = (td.FileName == null || td.FileName == "") ? null : td.FileName,
                            FileUrl = (td.FileUrl == null || td.FileUrl == "") ? null : td.FileUrl,
                            CompDetails = new CompanyItem()
                            {
                                CompName = (comp.CompName == null || comp.CompName == "") ? null : comp.CompName
                            },
                            //TradeDOcId = td.TradeDOcId,
                            //TradeId = td.TradeId,
                            //FileUrl = td.FileUrl
                        }).Distinct().ToList();
            return data;
        }
        public List<DocumentItem> getCompAllTradeDoc(int cid)
        {
            var data = (from tl in dbContext.Trade_License
                        join t in dbContext.TradeDocuments on tl.TID equals t.TradeId into t
                        from td in t.DefaultIfEmpty()
                        join cm in dbContext.Company_master on tl.CompID equals cm.id into c
                        from comp in c.DefaultIfEmpty()
                        where tl.CompID == (cid == 0 ? tl.CompID : cid)
                        select new DocumentItem()
                        {
                            //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                            FileName = (td.FileName == null || td.FileName == "") ? null : td.FileName,
                            FileUrl = (td.FileUrl == null || td.FileUrl == "") ? null : td.FileUrl,
                            CompDetails = new CompanyItem()
                            {
                                CompName = (comp.CompName == null || comp.CompName == "") ? null : comp.CompName
                            },
                            //TradeDOcId = td.TradeDOcId,
                            //TradeId = td.TradeId,
                            //FileUrl = td.FileUrl
                        }).Distinct().ToList();
            return data;
        }
        //Tenancy Documents
        public List<DocumentItem> getTenancyDocByComp(int compID)
        {
            var data = (from tl in dbContext.TenancyMasters
                        join t in dbContext.TenancyDocuments on tl.TnID equals t.TenId into t
                        from td in t.DefaultIfEmpty()
                        join cm in dbContext.Company_master on tl.TnTypeCompId equals cm.id into c
                        from comp in c.DefaultIfEmpty()
                        where tl.TnTypeCompId == compID
                        select new DocumentItem()
                        {
                            //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                            FileName = (td.FileName == null || td.FileName == "") ? null : td.FileName,
                            FileUrl = (td.FileUrl == null || td.FileUrl == "") ? null : td.FileUrl,
                            CompDetails = new CompanyItem()
                            {
                                CompName = (comp.CompName == null || comp.CompName == "") ? null : comp.CompName
                            },
                            //TradeDOcId = td.TradeDOcId,
                            //TradeId = td.TradeId,
                            //FileUrl = td.FileUrl
                        }).Distinct().ToList();
            return data;
        }
        public List<DocumentItem> getCompAllTenancyDoc(int cid)
        {
            var data = (from tl in dbContext.TenancyMasters
                        join t in dbContext.TenancyDocuments on tl.TnID equals t.TenId into t
                        from td in t.DefaultIfEmpty()
                        join cm in dbContext.Company_master on tl.TnTypeCompId equals cm.id into c
                        from comp in c.DefaultIfEmpty()
                        where tl.TnTypeCompId == (cid == 0 ? tl.TnTypeCompId : cid)
                        select new DocumentItem()
                        {
                            //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                            FileName = (td.FileName == null || td.FileName == "") ? null : td.FileName,
                            FileUrl = (td.FileUrl == null || td.FileUrl == "") ? null : td.FileUrl,
                            CompDetails = new CompanyItem()
                            {
                                CompName = (comp.CompName == null || comp.CompName == "") ? null : comp.CompName
                            },
                            //TradeDOcId = td.TradeDOcId,
                            //TradeId = td.TradeId,
                            //FileUrl = td.FileUrl
                        }).Distinct().ToList();
            return data;
        }
        //Vehicle documents
        public List<DocumentItem> getVehicleDocByComp(int compID)
        {
            var data = (from tl in dbContext.VehicleMasters
                        join t in dbContext.VehicleDocuments on tl.VID equals t.VId into t
                        from td in t.DefaultIfEmpty()
                        join cm in dbContext.Company_master on tl.CompID equals cm.id into c
                        from comp in c.DefaultIfEmpty()
                        where tl.CompID == compID
                        select new DocumentItem()
                        {
                            //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                            FileName = (td.FileName == null || td.FileName == "") ? null : td.FileName,
                            FileUrl = (td.FileUrl == null || td.FileUrl == "") ? null : td.FileUrl,
                            CompDetails = new CompanyItem()
                            {
                                CompName = (comp.CompName == null || comp.CompName == "") ? null : comp.CompName
                            },
                            //TradeDOcId = td.TradeDOcId,
                            //TradeId = td.TradeId,
                            //FileUrl = td.FileUrl
                        }).Distinct().ToList();
            return data;
        }
        public List<DocumentItem> getCompAllVehicleDoc(int cid)
        {
            var data = (from tl in dbContext.VehicleMasters
                        join t in dbContext.VehicleDocuments on tl.VID equals t.VId into t
                        from td in t.DefaultIfEmpty()
                        join cm in dbContext.Company_master on tl.CompID equals cm.id into c
                        from comp in c.DefaultIfEmpty()
                        where tl.CompID == (cid == 0 ? tl.CompID : cid)
                        select new DocumentItem()
                        {
                            //VID=(left.VID==null || left.VID==0 ) ? 0 : left.VID,
                            FileName = (td.FileName == null || td.FileName == "") ? null : td.FileName,
                            FileUrl = (td.FileUrl == null || td.FileUrl == "") ? null : td.FileUrl,
                            CompDetails = new CompanyItem()
                            {
                                CompName = (comp.CompName == null || comp.CompName == "") ? null : comp.CompName
                            },
                            //TradeDOcId = td.TradeDOcId,
                            //TradeId = td.TradeId,
                            //FileUrl = td.FileUrl
                        }).Distinct().ToList();
            return data;
        }
    }
}
