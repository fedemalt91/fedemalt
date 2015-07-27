using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogViajes.EntityModels;
using System.Data.Entity;
namespace BlogViajes.Repository
{
    public class ContentRepository
    {
        BlogViajesContext dbContext;

        public ContentRepository(string connectionString)
        {
            dbContext = new BlogViajesContext(connectionString);
        }

        public List<ContentModel> GetAll(int? Page, int? Category)
        {

            if (Page != null && Page != 0)
            {
                if (Category != null && Category != 0)
                {
                    return dbContext.Contents.Include("Comments").Include("Categories").Where(x => x.Categories.Any(y => y.Id == Category)).OrderBy(x => x.DatePosted).Skip(10 * ((int)Page - 1)).Take(10).ToList();
                }
                else
                {
                    return dbContext.Contents.Include("Comments").Include("Categories").OrderBy(x => x.DatePosted).Skip(10 * ((int)Page - 1)).Take(10).ToList();
                }
            }
            else
            {
                if (Category != null)
                {
                    return dbContext.Contents.Include("Comments").Include("Categories").Where(x => x.Categories.Any(y => y.Id == Category)).OrderBy(x => x.DatePosted).Take(10).ToList();
                }
                else
                {
                    return dbContext.Contents.Include("Comments").Include("Categories").OrderBy(x => x.DatePosted).Take(10).ToList();
                }
            }

        }


        public List<ContentModel> GetFeatured()
        {
            return dbContext.Contents.Where(x => x.IsFeatured).ToList();
        }

        public List<ContentModel> GetRecent()
        {
            return dbContext.Contents.OrderBy(x => x.DatePosted).Take(5).ToList();
        }

        public ContentModel Get(int Id)
        {
            return dbContext.Contents.Include("Comments").Include("Categories").FirstOrDefault(x => x.Id == Id);
        }
        public Int32 GetCount(int? categoryId)
        {
            if (categoryId != null)
            {
                return dbContext.Contents.Where(x => x.Categories.Any(y => y.Id == categoryId)).Count();
            }
            return dbContext.Contents.Count();
        }


    }
}
