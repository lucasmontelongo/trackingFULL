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
    [Authorize(Roles = "Admin")]
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<List<Models.Cliente>>(response.Content));
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

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<Models.Cliente>(response.Content));
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

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "El cliente fue dado de alta correctamente";
                    return RedirectToAction("index", "cliente");
                }
                ViewBag.ERROR = "Hubo un problema al dar de alta el cliente: " + response.Content.ToString() + ". Por favor revise todos los datos y vuelva a intentarlo";
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e.Message;
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<Models.Cliente>(response.Content));
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

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Cliente collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "Se actualizo correctamente";
                    return RedirectToAction("index", "cliente");
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

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    return View(JsonConvert.DeserializeObject<Models.Cliente>(response.Content));
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

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "cliente");
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "Se elimino correctamente";
                    return RedirectToAction("index", "cliente");
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
    }
}
