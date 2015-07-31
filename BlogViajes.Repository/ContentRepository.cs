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


        public void SaveContent(ContentModel model)
        {
            foreach (var item in model.Categories)
            {
                dbContext.Categories.Attach(item);
            }
            dbContext.Users.Attach(model.User);
            dbContext.Contents.Add(model);
            dbContext.SaveChanges();
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
            return dbContext.Contents.Where(x => x.IsFeatured).OrderBy(x => x.IsImportant).ThenBy(x => x.DatePosted).Take(5).ToList();
        }

        public List<ContentModel> GetRecent()
        {
            return dbContext.Contents.OrderBy(x => x.DatePosted).Take(5).ToList();
        }

        public ContentModel Get(int Id)
        {
            return dbContext.Contents.Include("user").Include("Comments").Include("Comments.PostingUser").Include("Categories").FirstOrDefault(x => x.Id == Id);
        }
        public Int32 GetCount(int? categoryId)
        {
            if (categoryId != null)
            {
                return dbContext.Contents.Where(x => x.Categories.Any(y => y.Id == categoryId)).Count();
            }
            return dbContext.Contents.Count();
        }



        public void AddCommentToComment(CommentModel comment, int? parentCommentId)
        {
            try
            {
                if (dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments.FirstOrDefault(x => x.Id == (int)parentCommentId).ChildComments != null)
                {
                    dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments.FirstOrDefault(x => x.Id == (int)parentCommentId).ChildComments.Add(comment);
                }
                else
                {
                    dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments.FirstOrDefault(x => x.Id == (int)parentCommentId).ChildComments = new List<CommentModel>();
                    dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments.FirstOrDefault(x => x.Id == (int)parentCommentId).ChildComments.Add(comment);
                }
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Error: intente nuevamente");
            }
        }

        public void SaveComment(CommentModel comment)
        {
            try
            {
                if (dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments != null)
                {
                    dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments.Add(comment);
                }
                else
                {
                    dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments = new List<CommentModel>();
                    dbContext.Contents.FirstOrDefault(x => x.Id == (int)comment.ContentWherePosted.Id).Comments.Add(comment);
                }
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw new Exception("Error: intente nuevamente");
            }
        }

        public List<ContentModel> GetAllSearch(int? page, string searchCriteria)
        {
            if (page != null && page != 0)
            {
                if (searchCriteria != null)
                {

                    return dbContext.Contents.Include("Categories").Include("Comments").Include("User").Where(x => ((x.Title.Contains(searchCriteria)) || (x.User.UserMail.Contains(searchCriteria)) || (x.User.UserName.Contains(searchCriteria)) || (x.Categories.Any(y => y.Name.Contains(searchCriteria))) || (x.Content.Contains(searchCriteria)))).OrderBy(x => x.DatePosted).Skip(10 * ((int)page - 1)).Take(10).ToList();

                }
                else
                {
                    return dbContext.Contents.Include("Categories").Include("Comments").Include("User").OrderBy(x => x.DatePosted).Skip(10 * ((int)page - 1)).Take(10).ToList();
                }
            }
            else
            {
                if (searchCriteria != null)
                {
                    return dbContext.Contents.Include("Categories").Include("Comments").Include("User").Where(x => (x.Title.Contains(searchCriteria)) || (x.User.UserMail.Contains(searchCriteria)) || (x.User.UserName.Contains(searchCriteria)) || (x.Categories.Any(y => y.Name.Contains(searchCriteria))) || (x.Content.Contains(searchCriteria))).Take(10).ToList();

                }
                else
                {
                    return dbContext.Contents.Include("Categories").Include("Comments").Include("User").Take(10).ToList();
                }
            }


        }

        public int GetCountSearch(string searchCriteria)
        {
            if (searchCriteria != null)
            {
                return dbContext.Contents.Include("Categories").Include("User").Where(x => (x.Title.Contains(searchCriteria)) || (x.User.UserMail.Contains(searchCriteria)) || (x.User.UserName.Contains(searchCriteria)) || (x.Categories.Any(y => y.Name.Contains(searchCriteria))) || (x.Content.Contains(searchCriteria))).Count();

            }
            return dbContext.Contents.Count();
        }
    }
}
