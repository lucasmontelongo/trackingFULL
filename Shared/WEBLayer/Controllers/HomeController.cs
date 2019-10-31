using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBLayer.Models;

namespace WEBLayer.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Usuario u)
        {
            try
            {
                var client = new RestClient("http://localhost:52952/api/auth/login");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"Email\":\"" + u.Email + "\",\"Password\":\"" + u.Password + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var usuario = JsonConvert.DeserializeObject<Usuario>(response.Content);
                    Session["USUARIO_MAIL"] = usuario.Email;
                    Session["USUARIO_TOKEN"] = usuario.Token;
                    Session["USUARIO_ROL"] = usuario.Rol;
                }
                else
                {
                    ViewBag.Message = "Cagamos dijo ramos";
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Registro()
        {
            return View();
        }

        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}