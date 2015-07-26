using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogViajes.EntityModels;
using BlogViajes.Repository;
using System.Configuration;

namespace BlogViajes.Web.Controllers
{
    public class UsersController : Controller
    {
        UserRepository repository = new UserRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Subscribe(string Email)
        {
            UserModel us = repository.GetByMail(Email);
            if (us != null)
            {
                Session["registered"] = true;
                return RedirectToAction(TempData["Action"].ToString(), TempData["Controler"].ToString(), TempData["Param"].ToString());
            }
            us = new UserModel();
            us.FirstDateOn = DateTime.Now;
            us.LastDateOn = DateTime.Now;
            us.UserMail = Email;
            us.UserName = Email;
            if (Email.ToLower().Contains("hotmail"))
            {
                us.Type = UserType.Hotmail.ToString();
            }
            else if (Email.ToLower().Contains("yahoo"))
            {
                us.Type = UserType.Yahoo.ToString();
            }
            else
            {
                us.Type = UserType.Gmail.ToString();
            }
            repository.Save(us);
            Session["registered"] = true;
            return RedirectToAction(TempData["Action"].ToString(), TempData["Controler"].ToString(), TempData["Param"].ToString());
        }

    }
}
