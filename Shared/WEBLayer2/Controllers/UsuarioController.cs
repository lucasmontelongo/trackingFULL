using Newtonsoft.Json;
using RestSharp;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBLayer2.Models;

namespace WEBLayer2.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [Route("paquetesenviados")]
        public ActionResult PaquetesEnviados()
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "usuario/paquetesenviados");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<Models.Paquete> paquetes = new List<Models.Paquete>();
                    paquetes = JsonConvert.DeserializeObject<List<Models.Paquete>>(response.Content);
                    ViewBag.PaquetesEnviados = paquetes;
                }
                else
                {
                    ViewBag.Error = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [Route("paquetesrecibidos")]
        public ActionResult PaquetesRecibidos()
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "usuario/paquetesrecibidos");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<Models.Paquete> paquetes = new List<Models.Paquete>();
                    paquetes = JsonConvert.DeserializeObject<List<Models.Paquete>>(response.Content);
                    ViewBag.PaquetesRecibidos = paquetes;
                }
                else
                {
                    ViewBag.Error = response.Content; //cuando la api tiene un problema con la autorizacion responde con un 500 y no muestra ningun mensaje, hay que corregirlo
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [Route("entrega")]
        [HttpGet]
        public ActionResult Entrega(int id)
        {
            try
            {
                ViewBag.IdPaquete = id;//mauro
                EnvioDomicilioDTO eb = new EnvioDomicilioDTO()
                {
                    IdPaquete = id
                };
                return View(eb);
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [Route("entrega")]
        [HttpPost]
        public ActionResult Entrega(EnvioDomicilioDTO en)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete/updateenviodomicilio");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("IdPaquete", en.IdPaquete.ToString());
                request.AddQueryParameter("Envio", en.Envio.ToString());
                request.AddQueryParameter("Hora", en.Hora.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    if (response.Content == "true")
                    {
                        ViewBag.OK = "Se actualizo tu preferencia de envio para el paquete";
                        return RedirectToAction("Index");
                    }
                    ViewBag.ERROR = "Ha ocurrido un error al intentar actualizar tu preferencia de envio";
                    return Entrega(en.IdPaquete);
                }
                else
                {
                    ViewBag.Error = response.Content; //cuando la api tiene un problema con la autorizacion responde con un 500 y no muestra ningun mensaje, hay que corregirlo
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [Route("estadopaquete")]
        [HttpGet]
        public ActionResult EstadoPaquete()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        [Route("estadopaquete")]
        [HttpPost]
        public ActionResult EstadoPaquete(string codigo)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete/consultarestado");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("codigo", codigo);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                     ViewBag.OK = response.Content;
                }
                else
                {
                    ViewBag.Error = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

    }
}