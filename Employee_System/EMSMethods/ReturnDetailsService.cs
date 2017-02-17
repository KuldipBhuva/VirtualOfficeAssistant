using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Assests;
using EMSDomain.ViewModel.Company;
using EMSDomain.ViewModel.Employee;

namespace EMSMethods
{
    public class ReturnDetailsService
    {

        EmployeeEntities DbContext = new EmployeeEntities();


        public int InsertReturnDetails(ReturnItem model)
        {
            Mapper.CreateMap<ReturnItem, ReturnDetail>();
            ReturnDetail objReturnItem = Mapper.Map<ReturnDetail>(model);
            DbContext.ReturnDetails.Add(objReturnItem);
            return DbContext.SaveChanges();
        }
        public int InsertReturnStock(StockItem model)
        {
            Mapper.CreateMap<StockItem, Stock_Master>();
            Stock_Master objStockItem = Mapper.Map<Stock_Master>(model);
            DbContext.Stock_Master.Add(objStockItem);
            return DbContext.SaveChanges();
        }

        public int Update(ReturnItem model)
        {

            ReturnDetail objIssue = DbContext.ReturnDetails.SingleOrDefault(m => m.ReId == model.ReId);
            objIssue.ItemId = model.ItemId;
            objIssue.ReturnableAmt = model.ReturnableAmt;
            objIssue.Qty = model.Qty;
            objIssue.ReDate = model.ReDate;
            objIssue.ItemId = model.ItemId;
            objIssue.EmpId = model.EmpId;
            objIssue.UpdatedDate = System.DateTime.Now;
            return DbContext.SaveChanges();

        }

        public ReturnItem GetById(int Id)
        {
            var objItem = (from Return in DbContext.ReturnDetails
                           where Return.ReId == Id
                           select new ReturnItem()
                           {
                               EmpId = Return.EmpId,
                               ItemId = Return.ItemId,
                               Qty = Return.Qty,
                               ReturnableAmt = Return.ReturnableAmt,
                               ReDate = Return.ReDate,
                               CompId = Return.CompId,
                               
                           }
             ).FirstOrDefault();

            return objItem;
        }

        public List<CompanyItem> GetAllComp()
        {
            Mapper.CreateMap<Company_master, CompanyItem>();
            List<Company_master> tblMaster = DbContext.Company_master.ToList();
            List<CompanyItem> lstmasterdata = Mapper.Map<List<CompanyItem>>(tblMaster);
            return lstmasterdata;
        }
        public List<EmployeeItem> GetEmp()
        {
            Mapper.CreateMap<employee_master, EmployeeItem>();
            List<employee_master> tblEmp = DbContext.employee_master.Where(m => m.WorkingStatus != 94).ToList();
            List<EmployeeItem> lstEmp = Mapper.Map<List<EmployeeItem>>(tblEmp);
            return lstEmp;
        }

        public List<AssestsItem> GetALLItems()
        {

            var objItem = (from item in DbContext.Item_Master
                           select new AssestsItem()
                           {
                               ItemName = item.ItemName,
                               OpStock = item.OpStock,
                               CatId = item.CatId,
                               ItemId = item.ItemId
                           }
                           ).Distinct().ToList();

            return objItem;
        }

        public List<ReturnItem> GetALLReturnDetail()
        {

            var objItem = (from returnitem in DbContext.ReturnDetails
                           join comp in DbContext.Company_master on returnitem.CompId equals comp.id
                           join item in DbContext.Item_Master on returnitem.ItemId equals item.ItemId
                           join Emp in DbContext.employee_master on returnitem.EmpId equals Emp.id
                           select new ReturnItem()
                           {
                               Qty = returnitem.Qty,
                               ReDate = returnitem.ReDate,
                               ReId = returnitem.ReId,
                               ReturnableAmt = returnitem.ReturnableAmt,
                               ItemId = returnitem.ItemId,
                               CompName = comp.CompName,
                               EmpName = Emp.Empname,
                               ItemName = item.ItemName
                           }
                           ).Distinct().ToList();

            return objItem;
        }
    }
}
