using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EMSDomain.Model;
using EMSDomain.ViewModel.Category;

namespace EMSMethods
{
    public class CategoryService
    {
        EmployeeEntities dbContext = new EmployeeEntities();

        public int Insert(CategoryItem model)
        {
            Mapper.CreateMap<CategoryItem, Category_Master>();
            Category_Master objCat = Mapper.Map<Category_Master>(model);
            dbContext.Category_Master.Add(objCat);
            return dbContext.SaveChanges();
        }

        public List<CategoryItem> GetALL(int cid)
        {
            Mapper.CreateMap<Category_Master, CategoryItem>();
            List<Category_Master> lstCat = dbContext.Category_Master.Where(m=>m.CompID==(cid==0?m.CompID:cid)).ToList();
            List<CategoryItem> objmasterp = Mapper.Map<List<CategoryItem>>(lstCat);
            return objmasterp;
        }

        public CategoryItem GetById(int id)
        {
            Mapper.CreateMap<Category_Master, CategoryItem>();
            Category_Master objCat = dbContext.Category_Master.SingleOrDefault(m => m.Cat_id == id);
            CategoryItem objCatItem = Mapper.Map<CategoryItem>(objCat);
            return objCatItem;

        }

        public int Update(CategoryItem model)
        {

            Category_Master objCat = dbContext.Category_Master.SingleOrDefault(m => m.Cat_id == model.Cat_id);
            objCat.CatName = model.CatName;
            objCat.CatDesc = model.CatDesc;
            objCat.UpdatedDate = System.DateTime.Now;
            return dbContext.SaveChanges();

        }
    }
}
