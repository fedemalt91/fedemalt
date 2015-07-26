using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogViajes.EntityModels;
using System.Data.Entity;
namespace BlogViajes.Repository
{
    public class UserRepository
    {
        BlogViajesContext dbContext;

        public UserRepository(string connectionString)
        {
            dbContext = new BlogViajesContext(connectionString);
        }

        public List<UserModel> GetAll()
        {
            return dbContext.Users.ToList();
        }

        public UserModel GetByMail(string email)
        {
            return dbContext.Users.FirstOrDefault(x => x.UserMail == email);
        }

        public void Save(UserModel user)
        {
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }

    }
}
