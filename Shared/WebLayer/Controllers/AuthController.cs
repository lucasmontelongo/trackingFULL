using Newtonsoft.Json;
using RestSharp;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBLayer.Models;

namespace WEBLayer.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(Usuario u)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "auth/login");
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
                    Session["USUARIO_ID"] = usuario.Id;
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    ViewBag.Message = "Cagamos dijo ramos";
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("registro")]
        public ActionResult Registro()
        {
            return View();
        }
        
        [HttpPost]
        [Route("registro")]
        public ActionResult Registro(Usuario u)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "auth/registro");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"Email\":\"" + u.Email + "\",\"Password\":\"" + u.Password + "\"}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.Confirmacion = true;
                    return View();
                }
                else
                {
                    ViewBag.Message = response.Content;
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("confirmaremail")]
        public ActionResult ConfirmarEmail(string email, string codigoConfirmacion)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "/auth/confirmaremail");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddQueryParameter("email", email);
                request.AddQueryParameter("codigoConfirmacion", codigoConfirmacion);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.Respuesta = response.Content;
                    return View();
                }
                else
                {
                    ViewBag.Message = response.Content;
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}