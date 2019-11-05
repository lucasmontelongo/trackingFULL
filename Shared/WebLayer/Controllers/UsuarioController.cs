using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBLayer.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Envios()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult SeguirPaquete()
        {
            return View();
        }

        public ActionResult Recibidos()
        {
            return View();
        }
    }
}