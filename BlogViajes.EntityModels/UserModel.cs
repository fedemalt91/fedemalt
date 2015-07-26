using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace BlogViajes.EntityModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public String Type { get; set; }
        public String UserName { get; set; }
        public DateTime FirstDateOn { get; set; }
        public DateTime LastDateOn { get; set; }
        public String UserMail { get; set; }
    }

    public class BlogViajesContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<ContentModel> Contents { get; set; }
        public BlogViajesContext(string connectionString)
            : base("DefaultConnection")
        {

        }

    }
}
