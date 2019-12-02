using Newtonsoft.Json;
using RestSharp;
using Shared.Exceptions;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBLayer2.Models;

namespace WEBLayer2.Controllers
{
    public class TrayectoController : Controller
    {
        // GET: Trayecto
        public ActionResult Index()
        {
            var client = new RestClient(Direcciones.ApiRest + "trayecto");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                return View(JsonConvert.DeserializeObject<List<Models.Trayecto>>(response.Content));
            }
            return View();
        }

        // GET: Trayecto/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient(Direcciones.ApiRest + "trayecto");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
            request.AddQueryParameter("id", id.ToString());
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ViewBag.TRAYECTO = JsonConvert.DeserializeObject<Models.Trayecto>(response.Content);
                client = new RestClient(Direcciones.ApiRest + "trayecto/puntoscontrol");
                response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<List<Models.PuntoControl>>(response.Content));
                }
                ViewBag.ERROR = response.Content;
            }
            ViewBag.ERROR = response.Content;
            return View();
        }

        // GET: Trayecto/Create
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

        // POST: Trayecto/Create
        [HttpPost]
        public ActionResult Create(TrayectoDTO collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(new Trayecto()
                {
                    Id = 0,
                    Borrado = false,
                    Nombre = collection.Nombre,
                    Version = 0
                });
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new ECompartida(response.Content);
                }
            }
            catch(Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        // GET: Trayecto/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient(Direcciones.ApiRest + "trayecto");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
            request.AddQueryParameter("id", id.ToString());
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ViewBag.TRAYECTO = JsonConvert.DeserializeObject<Models.Trayecto>(response.Content);
                client = new RestClient(Direcciones.ApiRest + "trayecto/puntoscontrol");
                response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<PuntoControl> pclist = JsonConvert.DeserializeObject<List<Models.PuntoControl>>(response.Content);
                    ViewBag.PUNTOSCONTROL = JsonConvert.DeserializeObject<List<Models.PuntoControl>>(response.Content);
                    client = new RestClient(Direcciones.ApiRest + "agencia");
                    request = new RestRequest(Method.GET);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        List<Models.Agencia> a = new List<Models.Agencia>();
                        //a.Add(new Models.Agencia() { Id = 0, Nombre = "Ninguna", Borrado = false, EnvioDomicilio = false, IdEmpresa = 1, Ubicacion = "lerelele" });
                        foreach (var item in JsonConvert.DeserializeObject<List<Models.Agencia>>(response.Content))
                        {
                            a.Add(item);
                        }
                        ViewBag.AGENCIAS = a;
                        Trayecto t = new Trayecto()
                        {
                            Id = ViewBag.TRAYECTO.Id,
                            Nombre = ViewBag.TRAYECTO.Nombre,
                            ListaPuntosControl = pclist
                        };
                        
                        return View(t);
                    }
                    else
                    {
                        ViewBag.ERROR = response.Content;
                        return View();
                    }
                }
                ViewBag.ERROR = response.Content;
                return View();
            }
            ViewBag.ERROR = response.Content;
            return View();
        }

        // POST: Trayecto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Trayecto collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                collection.Id = id;
                collection.ListaPuntosControl = null;
                collection.Version = 0;
                collection.Borrado = false;
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {

                    return RedirectToAction("Index");
                }
                ViewBag.ERROR = response.Content;
                return Edit(id);
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return Edit(id);
            }
        }

        // GET: Trayecto/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient(Direcciones.ApiRest + "trayecto");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
            request.AddQueryParameter("id", id.ToString());
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ViewBag.TRAYECTO = JsonConvert.DeserializeObject<Models.Trayecto>(response.Content);
                client = new RestClient(Direcciones.ApiRest + "trayecto/puntoscontrol");
                response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.PUNTOSCONTROL = JsonConvert.DeserializeObject<List<Models.PuntoControl>>(response.Content);
                    return View();
                }
                ViewBag.ERROR = response.Content;
            }
            ViewBag.ERROR = response.Content;
            return View();
        }

        // POST: Trayecto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto");
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "El trayecto fue borrado correctamente";
                    return RedirectToAction("Index");
                }
                ViewBag.ERROR = response.Content;
                return View();
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult CreatePC(int idTrayecto)
        {
            try
            {
                ViewBag.IDTRAYECTO = idTrayecto;
                var client = new RestClient(Direcciones.ApiRest + "agencia");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.AGENCIAS = JsonConvert.DeserializeObject<List<Models.Agencia>>(response.Content);
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult CreatePC(PuntoControl collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto/addpuntocontrol");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return CreatePC(collection.IdTrayecto);
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return CreatePC(collection.IdTrayecto);
            }
        }

        [HttpGet]
        public ActionResult EditPC(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto/getpuntocontrol");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<PuntoControl>(response.Content));
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditPC(int id, PuntoControl collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto/editpuntocontrol");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return CreatePC(collection.IdTrayecto);
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return CreatePC(collection.IdTrayecto);
            }
        }

        [HttpGet]
        public ActionResult DeletePC(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto/getpuntocontrol");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<PuntoControl>(response.Content));
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeletePC(FormCollection collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto/deletepuntocontrol");
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", collection["Id"]);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return DeletePC(Int32.Parse(collection["Id"]));
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return DeletePC(Int32.Parse(collection["Id"]));
            }
        }

    }
}
