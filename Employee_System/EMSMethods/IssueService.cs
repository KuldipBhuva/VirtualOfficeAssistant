using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Assests;

namespace EMSMethods
{
    public class IssueService
    {
        EmployeeEntities dbContext = new EmployeeEntities();

        public int InsertIssueDetails(IssueItem model)
        {
            Mapper.CreateMap<IssueItem, IssueDetail>();
            IssueDetail objIssueItem = Mapper.Map<IssueDetail>(model);
            dbContext.IssueDetails.Add(objIssueItem);
            return dbContext.SaveChanges();
        }
        public int InsertIssueStock(StockItem model)
        {
            Mapper.CreateMap<StockItem, Stock_Master>();
            Stock_Master objStockItem = Mapper.Map<Stock_Master>(model);
            dbContext.Stock_Master.Add(objStockItem);
            return dbContext.SaveChanges();
        }

        public List<IssueItem> GetALL()
        {
            var objItem = (from Issue in dbContext.IssueDetails
                           join cat in dbContext.Category_Master on Issue.Cat_id equals cat.Cat_id
                           join item in dbContext.Item_Master on Issue.Item_id equals item.ItemId
                           select new IssueItem()
                           {
                               IssueId = Issue.IssueId,
                               IssueQuantity = Issue.IssueQuantity,
                               ItemName = item.ItemName,
                               CatName = cat.CatName,
                               Cat_id=cat.Cat_id,
                               VendorName = Issue.VendorName
                           }
             ).Distinct().ToList();

            return objItem;
        }

        public IssueItem GetById(int Id)
        {
            var objItem = (from Issue in dbContext.IssueDetails
                           where Issue.IssueId == Id
                           select new IssueItem()
                           {

                               IssueId = Issue.IssueId,
                               IssueQuantity = Issue.IssueQuantity,
                               VendorName = Issue.VendorName,
                               Cat_id = Issue.Cat_id,
                               Item_id = Issue.Item_id,
                               InvoiceNo = Issue.InvoiceNo
                           }
             ).FirstOrDefault();

            return objItem;
        }
        public int Update(IssueItem model)
        {

            IssueDetail objIssue = dbContext.IssueDetails.SingleOrDefault(m => m.IssueId == model.IssueId);
            objIssue.Item_id = model.Item_id;
            objIssue.Cat_id = model.Cat_id;
            objIssue.IssueQuantity = model.IssueQuantity;
            objIssue.VendorName = model.VendorName;
            objIssue.ChallanNo = model.ChallanNo;

            objIssue.UpdatedDate = System.DateTime.Now;
            return dbContext.SaveChanges();

        }

        public List<IssueItem> GetQuntyDetails(int ItemId)
        {
            object[] params1 = {
                new SqlParameter("@ItemId", ItemId)};

            //Get student name of string type
            var Repayment = dbContext.Database.SqlQuery<IssueItem>
                ("EXEC Usp_AvailQuantity @ItemId", new SqlParameter("ItemId", ItemId)).ToList<IssueItem>();
            //DbContext.Database.SqlQuery<PayrollItem>("exec PayrollReport", params1).ToList<PayrollItem>();
            return Repayment;
        }
    }
}
