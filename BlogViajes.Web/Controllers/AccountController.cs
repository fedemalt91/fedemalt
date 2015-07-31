using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogViajes.EntityModels;
using BlogViajes.Repository;
using System.Configuration;
using BlogViajes.Web.Models;

namespace BlogViajes.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        ContentRepository repository = new ContentRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        CategoryRepository catRepository = new CategoryRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        UserRepository userRepository = new UserRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);

        public ActionResult LoadArticle(string pass)
        {
            if (String.IsNullOrEmpty(pass) || pass != "Federo@1357//Edimburgo@2017")
            {
                return RedirectToAction("Lista", "Articulos", null);
            }
            ContentModelLoad model = new ContentModelLoad();
            model.AllCategories = catRepository.GetAll();
            model.CategoriesId = new List<Int32>();
            return View(model);
        }

        [HttpPost]
        public ActionResult LoadArticle(ContentModelLoad model)
        {
            ContentModel modelo = new ContentModel();
            List<CategoryModel> cateList = new List<CategoryModel>();
            foreach (var item in model.CategoriesId)
            {
                cateList.Add(catRepository.Get(item));
            }
            modelo.Comments = new List<CommentModel>();
            modelo.Categories = cateList;
            modelo.Content = model.Content;
            modelo.DatePosted = DateTime.Now;
            modelo.IsActive = model.IsActive;
            modelo.IsFeatured = model.IsFeatured;
            modelo.IsImportant = model.IsImportant;
            modelo.Resume = model.Resume;
            modelo.Title = model.Title;
            UserModel us = userRepository.GetByMail(model.UserMail);
            if (us == null)
            {
                us = new UserModel();
                us.FirstDateOn = DateTime.Now;
                us.LastDateOn = DateTime.Now;
                us.UserMail = model.UserMail;
                us.UserName = model.UserName;
                // userRepository.Save(us);
            }
            modelo.User = us;
            repository.SaveContent(modelo);

            return View ("LoadImages", modelo.Id);
        }

        [HttpGet]
        public ActionResult LoadImage(string contentId, HttpPostedFileBase smallfile)
        {
            return new EmptyResult();

        }

        [HttpPost]
        public ActionResult LoadImage(int contentId, HttpPostedFileBase smallfile, HttpPostedFileBase fullfile)
        {

            if (smallfile != null && fullfile != null)
            {
                string spic = "mini" + contentId.ToString() + ".png";
                string spath = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/images/Articulos"),spic);
                string fpic = "full" + contentId.ToString() + ".png";
                string fpath = System.IO.Path.Combine(
                                       Server.MapPath("~/Content/images/Articulos"), fpic);
                 
                smallfile.SaveAs(spath);
                fullfile.SaveAs(fpath);
              
                

            }
            // after successfully uploading redirect the user
            return RedirectToAction("LoadArticle", "Account","Federo@1357//Edimburgo@2017");
        }

        //
        // GET: /Account/Details/5

        public ActionResult LoadCategory(string pass)
        {
            if (String.IsNullOrEmpty(pass) || pass != "Federo@1357//Edimburgo@2017")
            {
                return RedirectToAction("Lista", "Articulos", null);
            }
            CategoryModel model = new CategoryModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult LoadCategory(CategoryModel model)
        {
            if (catRepository.GetByName(model.Name) != null)
            {
                return RedirectToAction("LoadCategory", "Federo@1357//Edimburgo@2017");
            }
            catRepository.Save(model);
            return RedirectToRoute("/Account/LoadArticle?pass=Federo@1357//Edimburgo@2017");
        }

        //
        // POST: /Account/Create

       
    }
}
