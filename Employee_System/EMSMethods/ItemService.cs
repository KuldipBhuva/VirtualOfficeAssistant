using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Assests;

namespace EMSMethods
{
    public class ItemService
    {
        EmployeeEntities dbContext = new EmployeeEntities();

        public int Insert(AssestsItem model)
        {
            Mapper.CreateMap<AssestsItem, Item_Master>();
            Item_Master objItem = Mapper.Map<Item_Master>(model);
            dbContext.Item_Master.Add(objItem);
            return dbContext.SaveChanges();
        }

        public int Update(AssestsItem model)
        {
            Item_Master objItem = dbContext.Item_Master.Where(m => m.ItemId == model.ItemId).FirstOrDefault();
            objItem.CatId = model.CatId;
            objItem.ItemName = model.ItemName;
            objItem.OpStock = model.OpStock;
            objItem.ItemDesc = model.ItemDesc;
            objItem.UpdatedBy = model.UpdatedBy;
            objItem.UpdatedDate = model.UpdatedDate;
            return dbContext.SaveChanges();

        }

        public List<AssestsItem> GetALLItems(int cid)
        {

            var objItem = (from item in dbContext.Item_Master
                           join cat in dbContext.Category_Master on item.CatId equals cat.Cat_id
                           where item.CompID == (cid == 0 ? item.CompID : cid)
                           select new AssestsItem()
                           {
                               ItemName = item.ItemName,
                               OpStock = item.OpStock,
                               CatId = item.CatId,
                               CatName = cat.CatName,
                               ItemId = item.ItemId
                           }
                           ).Distinct().ToList();

            return objItem;
        }

        public AssestsItem GetItemsById(int ItemId)
        {

            Item_Master objItemMst = dbContext.Item_Master.SingleOrDefault(m => m.ItemId == ItemId);
            AssestsItem objItem = new AssestsItem();
            objItem.ItemName = objItemMst.ItemName;
            objItem.ItemId = objItemMst.ItemId;
            objItem.CatId = objItemMst.CatId;
            objItem.ItemDesc = objItemMst.ItemDesc;
            objItem.OpStock = objItemMst.OpStock;
            objItem.Status = objItemMst.Status;
            return objItem;
        }

        public List<AssestsItem> GetItemsBYCatId(int CatId)
        {

            var objItem = (from item in dbContext.Item_Master
                           join cat in dbContext.Category_Master on item.CatId equals cat.Cat_id
                           where item.CatId == CatId
                           select new AssestsItem()
                           {
                               ItemName = item.ItemName,
                               OpStock = item.OpStock,
                               CatId = item.CatId,
                               CatName = cat.CatName,
                               ItemId = item.ItemId
                           }
                           ).Distinct().ToList();

            return objItem;
        }



    }
}
