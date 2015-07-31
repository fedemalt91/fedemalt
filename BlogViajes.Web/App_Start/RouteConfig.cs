using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogViajes.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "sitemap.xml",
               url: "sitemap.xml",
               defaults: new { controller = "Servicio", action = "SitemapXml" },
               namespaces: new[] { "BlogViajes.Web.Controllers" }
           );

            routes.MapRoute(
                name: "robots.txt",
                url: "robots.txt",
                defaults: new { controller = "Servicio", action = "RobotsText" },
                namespaces: new[] { "BlogViajes.Web.Controllers" }
            );

            routes.MapRoute(
                name: "googleid.html",
                url: "google{id}.html",
                defaults: new { controller = "Servicio", action = "Google" },
                namespaces: new[] { "BlogViajes.Web.Controllers" }
            );
           

            routes.MapRoute(
                name: "ArticulosList",
                url: "Articulos/Lista/{Page}/{Category}",
                defaults: new { controller = "Articulos", action = "Lista", Page = UrlParameter.Optional, Category = UrlParameter.Optional  }
            );

            routes.MapRoute(
               name: "Destinos",
               url: "Articulos/Destinos/{Page}/{CategoryId}",
               defaults: new { controller = "Articulos", action = "Destinos"}
           );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Articulos", action = "Lista", id = UrlParameter.Optional }
           );
        }
    }
}