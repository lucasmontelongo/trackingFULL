using Newtonsoft.Json;
using RestSharp;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        // POST: Trayecto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                Models.Trayecto t = new Models.Trayecto()
                {
                    Nombre = Convert.ToString(collection["nombreTrayecto"]),
                    Version = 0,
                    Id = 1,
                    Borrado = false,
                    ListaPuntosControl = new List<Models.PuntoControl>()
                };
                bool stop = false;
                int linea = 1;
                while (!stop)
                {
                    if (collection["nombrePC" + linea] != null)
                    {
                        Models.PuntoControl pc = new Models.PuntoControl()
                        {
                            Nombre = collection["nombrePC" + linea],
                            Orden = Int32.Parse(collection["ordenPC" + linea]),
                            Tiempo = Int32.Parse(collection["tiempoPC" + linea]),
                            Borrado = false,
                            IdTrayecto = 1,
                            Id = 1
                        };
                        if (collection["idAgenciaPC" + linea] != "null")
                        {
                            pc.IdAgencia = Int32.Parse(collection["idAgenciaPC" + linea]);
                        }
                        t.ListaPuntosControl.Add(pc);
                        linea = linea + 1;
                    }
                    else
                    {
                        stop = true;
                    }
                }
                request.AddJsonBody(t);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {

                    return RedirectToAction("Index");
                }
                ViewBag.ERROR = response.Content;
                return View();
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
                    ViewBag.PUNTOSCONTROL = JsonConvert.DeserializeObject<List<Models.PuntoControl>>(response.Content);
                    client = new RestClient(Direcciones.ApiRest + "agencia");
                    request = new RestRequest(Method.GET);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                    response = client.Execute(request);
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
                ViewBag.ERROR = response.Content;
            }
            ViewBag.ERROR = response.Content;
            return View();
        }

        // POST: Trayecto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "trayecto");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                Models.Trayecto t = new Models.Trayecto()
                {
                    Nombre = Convert.ToString(collection["nombreTrayecto"]),
                    Version = 0,
                    Id = Int32.Parse(collection["idTrayecto"]),
                    Borrado = false,
                    ListaPuntosControl = new List<Models.PuntoControl>()
                };
                bool stop = false;
                int linea = 1;
                while (!stop)
                {
                    if (collection["nombrePC" + linea] != null)
                    {
                        Models.PuntoControl pc;
                        if (collection["idPC" + linea] != null)
                        {
                            pc = new Models.PuntoControl()
                            {
                                Nombre = collection["nombrePC" + linea],
                                Orden = Int32.Parse(collection["ordenPC" + linea]),
                                Tiempo = Int32.Parse(collection["tiempoPC" + linea]),
                                IdAgencia = Int32.Parse(collection["idAgenciaPC" + linea]),
                                IdTrayecto = Int32.Parse(collection["idTrayectoPC" + linea]),
                                Id = Int32.Parse(collection["idPC" + linea])
                            };
                            if (collection["borradoPC" + linea] == "1")
                            {
                                pc.Borrado = true;
                            }
                            else
                            {
                                pc.Borrado = false;
                            }
                        }
                        else
                        {
                            pc = new Models.PuntoControl()
                            {
                                Nombre = collection["nombrePC" + linea],
                                Orden = Int32.Parse(collection["ordenPC" + linea]),
                                Tiempo = Int32.Parse(collection["tiempoPC" + linea]),
                                IdAgencia = Int32.Parse(collection["idAgenciaPC" + linea]),
                                IdTrayecto = Int32.Parse(collection["idTrayecto"]),
                                Id = -56,
                                Borrado = false
                            };
                        }
                        t.ListaPuntosControl.Add(pc);
                        linea = linea + 1;
                    }
                    else
                    {
                        stop = true;
                    }
                }
                request.AddJsonBody(t);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {

                    return RedirectToAction("Index");
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
    }
}
