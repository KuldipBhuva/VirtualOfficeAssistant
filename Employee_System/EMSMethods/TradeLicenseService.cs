using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.ViewModel;
using EMSDomain.Model;
using EMSDomain.ViewModel.TradeLicense;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Tenancy;


namespace EMSMethods
{
    public class TradeLicenseService
    {
        EmployeeEntities Dbcontext = new EmployeeEntities();

        public int Insert(TradeLicenseItem model)
        {
            Mapper.CreateMap<TradeLicenseItem, Trade_License>();
            Trade_License dbTrade = Mapper.Map<Trade_License>(model);
            Dbcontext.Trade_License.Add(dbTrade);
            return Dbcontext.SaveChanges();

        }

        public TradeLicenseItem GetById(int id)
        {
            Mapper.CreateMap<Trade_License, TradeLicenseItem>();
            Trade_License dbTrade = Dbcontext.Trade_License.SingleOrDefault(m => m.TID == id);
            TradeLicenseItem objTradeItem = Mapper.Map<TradeLicenseItem>(dbTrade);
            return objTradeItem;
        }

        public int Update(TradeLicenseItem model)
        {
            Trade_License Trade = new Trade_License();
            Trade = Dbcontext.Trade_License.SingleOrDefault(m => m.TID == model.TID);
            Trade.Remarks = model.Remarks;
            Trade.Code = model.Code;
            Trade.CompID = model.CompID;
            Trade.EmiratesID = model.EmiratesID;
            Trade.PartnerID = model.PartnerID;
            Trade.SponsorID = model.SponsorID;
            Trade.IssueDate = model.IssueDate;
            Trade.ExpiryDate = model.ExpiryDate;
            Trade.Fees = model.Fees;
            Trade.Lno = model.Lno;
            Trade.PartnerID2 = model.PartnerID2;
            Trade.PartnerID3 = model.PartnerID3;
            Trade.PartnerID4 = model.PartnerID4;
            Trade.LicenseTypeID = model.LicenseTypeID;
            //Trade.TID = model.TID;
            Trade.UpdateBy = "0";
            Trade.UpdatedDate = System.DateTime.Now;
            //Dbcontext.Trade_License.Add(Trade);
            return Dbcontext.SaveChanges();

        }


        //For Emirates Details
        public List<clsMasterData> GetEmirates()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstEmirates = Dbcontext.Masters_Tran.Where(m => m.MasterId == 6 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstEmirates);
            return lstData;
        }
        //For License Type
        public List<clsMasterData> GetLicenseType()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstEmirates = Dbcontext.Masters_Tran.Where(m => m.MasterId == 46 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstEmirates);
            return lstData;
        }
        //For Parter Details
        public List<clsMasterData> GetPartner()
        {
            Mapper.CreateMap<Masters_Tran, clsMasterData>();
            List<Masters_Tran> lstEmirates = Dbcontext.Masters_Tran.Where(m => m.MasterId == 18 && m.Status == "Active").ToList();
            List<clsMasterData> lstData = Mapper.Map<List<clsMasterData>>(lstEmirates);
            return lstData;
        }

        // For Company Details
        public List<CompanyItem> GetCompany()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> lstcompany = Dbcontext.Company_master.ToList();
            List<CompanyItem> lstItem = Mapper.Map<List<CompanyItem>>(lstcompany);
            return lstItem;
        }
        public TradeDocumentItem getByID(int id)
        {
            Mapper.CreateMap<TradeDocument, TradeDocumentItem>();
            TradeDocument objDoc = Dbcontext.TradeDocuments.SingleOrDefault(m => m.TradeDOcId == id);
            TradeDocumentItem objDocItem = Mapper.Map<TradeDocumentItem>(objDoc);
            Dbcontext.TradeDocuments.Remove(objDoc);
            return objDocItem;

        }
        public List<TradeLicenseItem> GetAllList(int cid)
        {
            var a = (from Trade in Dbcontext.Trade_License
                     join company in Dbcontext.Company_master on Trade.CompID equals company.id
                     // from tdoc in td.DefaultIfEmpty()
                     join lt in Dbcontext.Masters_Tran on Trade.LicenseTypeID equals lt.Id into type
                     from t in type.DefaultIfEmpty()
                     where Trade.CompID == (cid == 0 ? Trade.CompID : cid)
                     select new TradeLicenseItem()
                     {
                         Code = Trade.Code,
                         CompID = Trade.CompID,
                         Lno = Trade.Lno,
                         EmiratesID = Trade.EmiratesID,
                         Fees = Trade.Fees,
                         IssueDate = Trade.IssueDate,
                         ExpiryDate = Trade.ExpiryDate,
                         SponsorID = Trade.CompID,
                         TID = Trade.TID,
                         Remarks = Trade.Remarks,
                         PartnerID2 = Trade.PartnerID2,
                         PartnerID3 = Trade.PartnerID3,
                         PartnerID4 = Trade.PartnerID4,
                         PartnerID = Trade.PartnerID,

                         CompnyDetails = new CompanyItem()
                         {
                             CompName = company.CompName
                         },
                         LicTypeDetails = new clsMasterData()
                         {
                             Name = (t == null ? string.Empty : t.Name)
                         }



                     }
                ).Distinct().ToList();

            return a;
        }

        public string Getid()
        {
            var s = Dbcontext.Trade_License.Max(m => m.TID).ToString();
            return s;
        }

        public void Save(TradeLicenseItem model)
        {
            TradeDocument objTrade = new TradeDocument();
            objTrade.FileName = model.FileName;
            objTrade.FileUrl = model.FileUrl;
            objTrade.TradeId = model.TradeId;
            objTrade.CreatedDate = System.DateTime.Now;
            Dbcontext.TradeDocuments.Add(objTrade);
            Dbcontext.SaveChanges();
        }

        public List<TradeLicenseItem> ListDocTrend(int id)
        {
            var objDoc = (from Trade in Dbcontext.Trade_License
                          join tdoc in Dbcontext.TradeDocuments on Trade.TID equals tdoc.TradeId
                          where tdoc.TradeId == id
                          select new TradeLicenseItem()
                          {
                              FileName = tdoc.FileName,
                              FileUrl = tdoc.FileUrl,
                              TradeDOcId = (tdoc.TradeDOcId == null ? 0 : tdoc.TradeDOcId),
                              TradeId = tdoc.TradeId == null ? 0 : tdoc.TradeId
                          }
                ).Distinct().ToList();
            return objDoc;
        }
        public List<SponsorItem> getSponsorData(int cid)
        {
            Mapper.CreateMap<SponsorMaster, SponsorItem>();
            List<SponsorMaster> objCompanyMaster = Dbcontext.SponsorMasters.Where(m => m.Status == 1 && m.CompID == (cid == 0 ? m.CompID : cid)).ToList();
            List<SponsorItem> objCompItem = Mapper.Map<List<SponsorItem>>(objCompanyMaster);
            return objCompItem;
        }

    }
}
