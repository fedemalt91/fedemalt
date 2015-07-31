using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Routing;

namespace BlogViajes.Web.Models
{
    public class ContactoModel
    {
        [Required]
        [Display(Name="Nombre")]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        [Display(Name = "E-Mail")]
        public string UserEmail { get; set; }
        [Required]
        [Display(Name = "Consulta")]
        [DataType(DataType.MultilineText)]
        public string Texto { get; set; }
    }
    public class SitemapNode
    {
        public string Url { get; set; }
        public DateTime? LastModified { get; set; }
        public SitemapFrequency? Frequency { get; set; }
        public double? Priority { get; set; }


        public SitemapNode(string url)
        {
            Url = url;
            Priority = null;
            Frequency = null;
            LastModified = null;
        }

        public SitemapNode(RequestContext request, object routeValues)
        {
            Url = GetUrl(request, new RouteValueDictionary(routeValues));
            Priority = null;
            Frequency = null;
            LastModified = null;
        }

        public SitemapNode(RequestContext request, string routeValues)
        {
            Url = GetUrl(request, routeValues);
            Priority = null;
            Frequency = null;
            LastModified = null;
        }

        private string GetUrl(RequestContext request, RouteValueDictionary values)
        {
            var routes = RouteTable.Routes;
            var data = routes.GetVirtualPath(request, values);

            if (data == null)
            {
                return null;
            }

            var baseUrl = request.HttpContext.Request.Url;
            var relativeUrl = data.VirtualPath;

            return request.HttpContext != null &&
                   (request.HttpContext.Request != null && baseUrl != null)
                       ? new Uri(baseUrl, relativeUrl).AbsoluteUri
                       : null;
        }

        private string GetUrl(RequestContext request, string values)
        {
           

            var baseUrl = request.HttpContext.Request.Url;
            var relativeUrl = values;

            return request.HttpContext != null &&
                   (request.HttpContext.Request != null && baseUrl != null)
                       ? new Uri(baseUrl, relativeUrl).AbsoluteUri
                       : null;
        }



    }

    public enum SitemapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }
}