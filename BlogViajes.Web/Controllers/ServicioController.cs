using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogViajes.Web.Models;
using BlogViajes.Repository;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using System.Diagnostics;

namespace BlogViajes.Web.Controllers
{
    public class ServicioController : Controller
    {
        private const string SitemapsNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";




        //
        //
        // GET: /Servicio/
        ContentRepository repository = new ContentRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);
        CategoryRepository catRepository = new CategoryRepository(ConfigurationManager.ConnectionStrings[0].ConnectionString);


        public ActionResult Info()
        {
            return View();
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Any)]
        public FileContentResult RobotsText()
        {
            var content = new StringBuilder("User-agent: *" + Environment.NewLine);

            if (string.Equals(ConfigurationManager.AppSettings["SiteStatus"], "live", StringComparison.InvariantCultureIgnoreCase))
            {
                content.Append("Disallow: ").Append("/Account" + Environment.NewLine);
                content.Append("Disallow: ").Append("/Error" + Environment.NewLine);
                content.Append("Disallow: ").Append("/signalr" + Environment.NewLine);
                content.Append("Sitemap: ").Append(ConfigurationManager.AppSettings["HostName"]).Append("/sitemap.xml" + Environment.NewLine);

            }
            else
            {
                // disallow indexing for test and dev servers
                content.Append("Disallow: /" + Environment.NewLine);
            }


            return File(
                    Encoding.UTF8.GetBytes(content.ToString()),
                    "text/plain");
        }

        [NonAction]
        private IEnumerable<SitemapNode> GetSitemapNodes()
        {
            List<SitemapNode> nodes = new List<SitemapNode>();

            var cates = catRepository.GetByType(EntityModels.CategoryType.Section);
            foreach (var ii in cates)
            {
                nodes.Add(new SitemapNode(this.ControllerContext.RequestContext, new { area = "", controller = "Articulos", action = new string(ii.Name.ToCharArray().Where(x=>x != ' ').ToArray()) })
                    {
                        Frequency = SitemapFrequency.Always,
                        Priority = 0.8
                    });
            }

            var cateA = catRepository.GetAll();
            foreach (var iA in cateA)
            {
                nodes.Add(new SitemapNode(this.ControllerContext.RequestContext, "/Articulos/ListaCategoria/" + new string(iA.Name.ToCharArray().Where(x => x != ' ').ToArray()))
                {
                    Frequency = SitemapFrequency.Always,
                    Priority = 0.8
                });
            }

            var cateD = catRepository.GetByType(EntityModels.CategoryType.Destination);
            foreach (var iD in cateD)
            {
                nodes.Add(new SitemapNode(this.ControllerContext.RequestContext, "/Articulos/Destinos/" + new string(iD.Name.ToCharArray().Where(x => x != ' ').ToArray()))
                {
                    Frequency = SitemapFrequency.Always,
                    Priority = 0.8
                });
            }


            var items = repository.GetAll(null, null);
            foreach (var item in items)
            {
                nodes.Add(new SitemapNode(this.ControllerContext.RequestContext, new { area = "", controller = "Articulos", action = "Ver", id = item.Id })
                {
                    Frequency = SitemapFrequency.Weekly,
                    Priority = 0.5,
                    LastModified = item.DatePosted
                });
            }

            return nodes;
        }

        [NonAction]
        private string GetSitemapXml()
        {
            XElement root;
            XNamespace xmlns = SitemapsNamespace;

            var nodes = GetSitemapNodes();

            root = new XElement(xmlns + "urlset");


            foreach (var node in nodes)
            {
                root.Add(
                new XElement(xmlns + "url",
                    new XElement(xmlns + "loc", Uri.EscapeUriString(node.Url)),
                    node.Priority == null ? null : new XElement(xmlns + "priority", node.Priority.Value.ToString("F1", CultureInfo.InvariantCulture)),
                    node.LastModified == null ? null : new XElement(xmlns + "lastmod", node.LastModified.Value.ToLocalTime().ToString("yyyy-MM-ddTHH:mm:sszzz")),
                    node.Frequency == null ? null : new XElement(xmlns + "changefreq", node.Frequency.Value.ToString().ToLowerInvariant())
                    ));
            }

            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms, Encoding.UTF8))
                {
                    root.Save(writer);
                }

                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }


        [HttpGet]
        [OutputCache(Duration = 24 * 60 * 60, Location = System.Web.UI.OutputCacheLocation.Any)]
        public ActionResult SitemapXml()
        {
            Trace.WriteLine("sitemap.xml was requested. User Agent: " + Request.Headers.Get("User-Agent"));

            var content = GetSitemapXml();
            return Content(content, "application/xml", Encoding.UTF8);
        }

        public ActionResult Google(string id)
        {
            if (ConfigurationManager.AppSettings["GoogleId"] == id)
                return View(model: id);
            else
                return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Search(string searchCriteria)
        {
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = 1;
            int count = repository.GetCountSearch(searchCriteria);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(EntityModels.CategoryType.Tag);
            model.ContentList = repository.GetAllSearch(1, searchCriteria);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "Search";
            model.ActionToGo = "Search";
            TempData["Controler"] = "Servicio";
            TempData["Param"] = model.ActualPage;
            TempData["SearchCrit"] = searchCriteria;
            return View(model);
        }

        public ActionResult Search(int page, string searchCriteria)
        {
            ArticulosListModel model = new ArticulosListModel();
            model.ActualPage = 1;
            int count = repository.GetCountSearch(searchCriteria);
            model.AmtPages = count % 10 == 0 ? count / 10 : (int)(Math.Round(Convert.ToDouble(count / 10), 0) + 1);
            model.Categories = catRepository.GetByType(EntityModels.CategoryType.Tag);
            model.ContentList = repository.GetAllSearch(1, searchCriteria);
            model.FeatureList = repository.GetFeatured();
            model.RecentList = repository.GetRecent();
            ViewBag.Title = "Viajero por definición";
            TempData["Action"] = "Search";
            model.ActionToGo = searchCriteria;
            TempData["Controler"] = "Servicio";
            TempData["Param"] = model.ActualPage;
            TempData["SearchCrit"] = searchCriteria;
            return View(model);
        }
        public ActionResult Contacto()
        {
            ContactoModel model = new ContactoModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Contacto(ContactoModel model)
        {
            var fromAddress = new MailAddress("fedemalt91@gmail.com");
            var fromPassword = "Federosa1357";
            var toAddress = new MailAddress("fedemalt91@gmail.com");

            string subject = "Consulta desde la pagina" + model.UserName;
            string body = model.UserEmail + "/r/n" + model.Texto;

            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };

            using (var message = new MailMessage(new MailAddress(model.UserEmail, model.UserName), toAddress)
            {

                Subject = subject,
                Body = body
            })


                smtp.Send(message);
            return RedirectToAction("Lista", "Articulos", null);
        }
    }
}
