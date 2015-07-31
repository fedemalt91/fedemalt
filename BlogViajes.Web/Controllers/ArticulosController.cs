using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogViajes.Repository;
using System.Configuration;
using BlogViajes.EntityModels;
using BlogViajes.Web.Models;

namespace BlogViajes.Web.Controllers
{
    public class ArticulosController : Controller
    {
        ContentRepository repository = new ContentRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        CategoryRepository catRepository = new CategoryRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        UserRepository userRepository = new UserRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);

        public ActionResult Ver(int Id)
        {
            ContentModel model = repository.Get(Id);
            if (model != null)
            {
                ArticulosVerModel verModel = new ArticulosVerModel();

                verModel.Categories = catRepository.GetByType(EntityModels.CategoryType.Tag);
                verModel.Content = model;
                verModel.FeatureList = repository.GetFeatured();
                verModel.RecentList = repository.GetRecent();
                ViewBag.Title = "Viajero por definición";
                TempData["Action"] = "Ver";
                TempData["Controler"] = "Articulos";
                TempData["Param"] = Id;
                return View(verModel);
            }
            return RedirectToAction("Lista");
        }


        public ActionResult Lista(int? Page, int? Category)
        {
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = (Int32)((Page == null || Page == 0 || Page == 1) ? 1 : Page);
            int count = repository.GetCount(null);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(EntityModels.CategoryType.Tag);
            model.ContentList = repository.GetAll(Page, Category);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "Lista";
            model.ActionToGo = "Lista";
            TempData["Controler"] = "Articulos";
            TempData["Param"] = model.ActualPage;
            TempData["Category"] = Category;
            TempData["NombreCategoria"] = Category == null ? "Bienvenido!" : catRepository.Get((int)Category).Name;
            return View(model);
        }

        public ActionResult ListaCategoria(int? Page, int? Category)
        {
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = (Int32)((Page == null || Page == 0 || Page == 1) ? 1 : Page);
            int count = repository.GetCount(null);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(EntityModels.CategoryType.Tag);
            model.ContentList = repository.GetAll(Page, Category);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "ListaCategoria";
            model.ActionToGo = "ListaCategoria";
            TempData["Controler"] = "Articulos";
            TempData["Param"] = model.ActualPage;
            TempData["Category"] = Category;
            TempData["NombreCategoria"] = Category == null ? "Bienvenido!" : catRepository.Get((int)Category).Name;
            return View("Lista",model);
        }

        public ActionResult VuelosBaratos(int? Page)
        {
            CategoryModel cate = catRepository.GetByName("VuelosBaratos");
            if (cate == null)
            {
                return RedirectToAction("Lista");
            }
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = (Int32)((Page == null || Page == 0 || Page == 1) ? 1 : Page);
            int count = repository.GetCount(cate.Id);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(CategoryType.Tag);
            model.ContentList = repository.GetAll(Page, cate.Id);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "VuelosBaratos";
            model.ActionToGo = "VuelosBaratos";
            TempData["Controler"] = "Articulos";
            TempData["Param"] = model.ActualPage;
            TempData["Category"] = "VuelosBaratos";
            TempData["NombreCategoria"] = "Vuelos Baratos";
            return View("Lista", model);

        }


        public ActionResult Experiencias(int? Page)
        {
            CategoryModel cate = catRepository.GetByName("Experiencias");
            if (cate == null)
            {
                return RedirectToAction("Lista");
            }
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = (Int32)((Page == null || Page == 0 || Page == 1) ? 1 : Page);
            int count = repository.GetCount(cate.Id);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(CategoryType.Tag);
            model.ContentList = repository.GetAll(Page, cate.Id);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "Experiencias";
            model.ActionToGo = "Experiencias";
            TempData["Controler"] = "Articulos";
            TempData["Param"] = model.ActualPage;
            TempData["Category"] = "Experiencias";
            TempData["NombreCategoria"] = "Experiencias";
            return View("Lista", model);
        }


        public ActionResult Tips(int? Page)
        {
            CategoryModel cate = catRepository.GetByName("Tips");
            if (cate == null)
            {
                return RedirectToAction("Lista");
            }
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = (Int32)((Page == null || Page == 0 || Page == 1) ? 1 : Page);
            int count = repository.GetCount(cate.Id);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(CategoryType.Tag);
            model.ContentList = repository.GetAll(Page, cate.Id);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "Tips";
            model.ActionToGo = "Tips";
            TempData["Controler"] = "Articulos";
            TempData["Param"] = model.ActualPage;
            TempData["Category"] = "Tips";
            TempData["NombreCategoria"] = "Consejos";
            return View("Lista", model);

        }

        public ActionResult Destinos(int? Page, int CategoryId)
        {
            CategoryModel cate = catRepository.Get(CategoryId);
            if (cate == null)
            {
                return RedirectToAction("Lista");
            }
            GuiaListModel model = new GuiaListModel();
            model.ActualPage = (Int32)((Page == null || Page == 0 || Page == 1) ? 1 : Page);
            int count = repository.GetCount(cate.Id);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(CategoryType.Tag);
            model.ContentList = repository.GetAll(Page, cate.Id);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            model.City = CategoryId;
            model.ActionToGo = "Destinos";
            TempData["Action"] = "Destinos";
            TempData["Controler"] = "Articulos";
            TempData["Param"] = model.ActualPage;
            TempData["Category"] = CategoryId;
            TempData["NombreCategoria"] = "Guias";
            return View("Guia", model);

        }


        [HttpPost]
        public ActionResult PostToContent(string ContentId, string texto, string user, string email)
        {


            int iparentContent = Convert.ToInt32(ContentId);
            try
            {
                UserModel us = userRepository.GetByMail(email);
                if (us == null)
                {
                    us = new UserModel();
                    us.FirstDateOn = DateTime.Now;
                    us.LastDateOn = DateTime.Now;
                    us.UserMail = email;
                    us.UserName = user;
                    // userRepository.Save(us);
                    Session["registered"] = true;
                }
                CommentModel comment = new CommentModel();
                comment.Comment = texto;
                comment.ContentWherePosted = repository.Get(iparentContent);
                if (comment.ContentWherePosted == null)
                {
                    throw new Exception("Error: por favor intente nuevamente");
                }
                comment.PostingUser = us;
                comment.IsFatherNode = true;
                comment.IsActive = true;
                comment.DatePosted = DateTime.Now;
                repository.SaveComment(comment);

                return PartialView("SingleComment", comment);
            }
            catch (Exception e)
            {
                return new EmptyResult() ;

            }

        }

        [HttpPost]
        public ActionResult PostComment(string parentCommentId, string parentContent, string texto, string user, string email)
        {

            int iparentCommentId = Convert.ToInt32(parentCommentId);
            int iparentContent = Convert.ToInt32(parentContent);
            try
            {
                UserModel us = userRepository.GetByMail(email);
                if (us == null)
                {
                    us = new UserModel();
                    us.FirstDateOn = DateTime.Now;
                    us.LastDateOn = DateTime.Now;
                    us.UserMail = email;
                    us.UserName = user;
                    // userRepository.Save(us);
                    Session["registered"] = true;
                }
                CommentModel comment = new CommentModel();
                comment.Comment = texto;
                comment.ContentWherePosted = repository.Get(iparentContent);
                if (comment.ContentWherePosted == null)
                {
                    throw new Exception("Error: por favor intente nuevamente");
                }
                comment.PostingUser = us;
                comment.IsFatherNode = false;
                comment.IsActive = true;
                comment.DatePosted = DateTime.Now;
                if (iparentCommentId != null)
                {
                    comment.IsFatherNode = false;
                    //repository.SaveComment(comment);
                    repository.AddCommentToComment(comment, iparentCommentId);
                }
                return PartialView("SingleComment",comment);
            }
            catch (Exception e)
            {
                return PartialView("SingleComment", e.Message );

            }

        }

        #region aux
        public FileResult GetImage(int Id)
        {
            string filePath = HttpContext.Server.MapPath("/Content/images/Articulos");
            string fileName = string.Format("{0}\\mini{1}.png", filePath, Id);
            if (!System.IO.File.Exists(fileName))
            {
                fileName = string.Format("{0}\\default.png", filePath);
            }
            return File(fileName, "image/png");
        }

        public FileResult GetFullImage(int Id)
        {
            string filePath = HttpContext.Server.MapPath("/Content/images/Articulos");
            string fileName = string.Format("{0}\\full{1}.png", filePath, Id);
            if (!System.IO.File.Exists(fileName))
            {
                fileName = string.Format("{0}\\default.png", filePath);
            }
            return File(fileName, "image/png");
        }

        public ActionResult LocationCategory()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            categories = catRepository.GetByType(CategoryType.Destination);
            return PartialView("_LocationCategory", categories);

        }
        #endregion

    }
}
