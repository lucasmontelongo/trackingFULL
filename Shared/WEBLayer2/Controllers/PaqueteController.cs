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
        public ActionResult Create(PaqueteDTO collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente/nuevopaquete");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                List<Cliente> clientes = new List<Cliente>()
                {
                    collection.Remitente,
                    collection.Destinatario
                };
                request.AddJsonBody(clientes);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    client = new RestClient(Direcciones.ApiRest + "paquete");
                    request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(response.Content);
                    Paquete paquete = new Paquete()
                    {
                        IdTrayecto = collection.IdTrayecto
                    };
                    clientes.ForEach(x =>
                    {
                        if (x.Email == collection.Remitente.Email)
                        {
                            paquete.IdRemitente = x.Id;
                        }
                        else
                        {
                            paquete.IdDestinatario = x.Id;
                        }
                    });
                    request.AddJsonBody(paquete);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ViewBag.OK = "El paquete fue dado de alta correctamente";
                        return RedirectToAction("index", "paquete");
                    }
                    
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
        public ActionResult Details(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete/detalle");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    PaqueteDTO paquete = JsonConvert.DeserializeObject<PaqueteDTO>(response.Content);
                    return View(paquete);
                }
                ViewBag.Error = response.Content;
                return RedirectToAction("index", "paquete");
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return View();
            }
        }

        

    }
}