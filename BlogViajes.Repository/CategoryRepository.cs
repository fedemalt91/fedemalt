using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogViajes.EntityModels;
using System.Data.Entity;
namespace BlogViajes.Repository
{
    public class CategoryRepository
    {
        BlogViajesContext dbContext;

        public CategoryRepository(string connectionString)
        {
            dbContext = new BlogViajesContext(connectionString);
        }

        public List<CategoryModel> GetAll()
        {
            return dbContext.Categories.ToList();
        }

        public CategoryModel Get(int Id)
        {
            return dbContext.Categories.FirstOrDefault(x => x.Id == Id);
        }


        public List<String> GetNamesByType(CategoryType categoryType)
        {
            string aux = categoryType.ToString();
            return dbContext.Categories.Where(x => x.Type == aux).Select(x => x.Name).ToList();
        }


        public List<CategoryModel> GetByType(CategoryType categoryType)
        {
            string aux = categoryType.ToString();
            return dbContext.Categories.Where(x => x.Type == aux).ToList();
        }

        public CategoryModel GetByName(string p)
        {
            return dbContext.Categories.FirstOrDefault(x => x.Name == p);
        }

        public void Save(CategoryModel model)
        {
            dbContext.Categories.Add(model);
            dbContext.SaveChanges();
        }
    }
}
