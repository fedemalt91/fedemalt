using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogViajes.Web.Models;

namespace BlogViajes.Web.Controllers
{
    public class ServicioController : Controller
    {
        //
        // GET: /Servicio/

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            ContactoModel model = new ContactoModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Contacto(ContactoModel model)
        {
            return View();
        }
    }
}
