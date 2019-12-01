using RestSharp;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBLayer2.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Route("nuevoadministrador")]
        public ActionResult nuevoAdministrador()
        {
            return View();
        }

        [HttpPost]
        [Route("nuevoadministrador")]
        public ActionResult nuevoAdministrador(string email)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "admin/prueba");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddHeader("email", email);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "El usuario con el email " + email + " ahora tiene permisos de administrador";                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return nuevoAdministrador();
            }
        }

    }
}