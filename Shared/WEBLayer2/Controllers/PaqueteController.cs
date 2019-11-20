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
    public class PaqueteController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<List<Models.Paquete>>(response.Content));
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.TRAYECTOS = JsonConvert.DeserializeObject<List<Trayecto>>(response.Content);
                    client = new RestClient(Direcciones.ApiRest + "cliente");
                    request = new RestRequest(Method.GET);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ViewBag.CLIENTES = JsonConvert.DeserializeObject<List<Models.Cliente>>(response.Content);
                        return View();
                    }
                    ViewBag.ERROR = response.Content;
                    return View();
                }
                ViewBag.ERROR = response.Content;
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Paquete collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "El paquete fue dado de alta correctamente";
                    return RedirectToAction("index", "cliente");
                }
                ViewBag.ERROR = "Hubo un problema al dar de alta el paquete: " + response.Content.ToString() + ". Por favor revise todos los datos y vuelva a intentarlo";
                return Create();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return Create();
            }
        }

        [HttpGet]
        [Route("detalle")]
        public ActionResult Detalle(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    Models.Paquete p = JsonConvert.DeserializeObject<Models.Paquete>(response.Content);
                    ViewBag.Paquete = p;
                    client = new RestClient(Direcciones.ApiRest + "cliente");
                    request = new RestRequest(Method.GET);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                    request.AddQueryParameter("id", p.IdDestinatario.ToString());
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        Models.Cliente destinatario = JsonConvert.DeserializeObject<Models.Cliente>(response.Content);
                        ViewBag.Destinatario = destinatario;
                        client = new RestClient(Direcciones.ApiRest + "cliente");
                        request = new RestRequest(Method.GET);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                        request.AddQueryParameter("id", p.IdRemitente.ToString());
                        response = client.Execute(request);
                        if (response.StatusCode.ToString() == "OK")
                        {
                            Models.Cliente remitente = JsonConvert.DeserializeObject<Models.Cliente>(response.Content);
                            ViewBag.Remitente = remitente;
                            client = new RestClient(Direcciones.ApiRest + "trayecto/puntoscontrol");
                            request = new RestRequest(Method.GET);
                            request.AddHeader("content-type", "application/json");
                            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                            request.AddQueryParameter("id", p.IdTrayecto.ToString());
                            response = client.Execute(request);
                            if (response.StatusCode.ToString() == "OK")
                            {
                                List<Models.PuntoControl> puntosControl = JsonConvert.DeserializeObject<List<Models.PuntoControl>>(response.Content);
                                ViewBag.PuntosDeControl = puntosControl;
                                client = new RestClient(Direcciones.ApiRest + "paquete/puntoscontrol");
                                request = new RestRequest(Method.GET);
                                request.AddHeader("content-type", "application/json");
                                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                                request.AddQueryParameter("id", id.ToString());
                                response = client.Execute(request);
                                if (response.StatusCode.ToString() == "OK")
                                {
                                    List<Models.PaquetePuntoControl> paquetespuntocontrol = JsonConvert.DeserializeObject<List<Models.PaquetePuntoControl>>(response.Content);
                                    ViewBag.PaquetesPuntoDeControl = paquetespuntocontrol;
                                }
                                else
                                {
                                    ViewBag.Error = response.Content;
                                }
                            }
                            else
                            {
                                ViewBag.Error = response.Content;
                            }
                        }
                        else
                        {
                            ViewBag.Error = response.Content;
                        }
                    }
                    else
                    {
                        ViewBag.Error = response.Content;
                    }
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