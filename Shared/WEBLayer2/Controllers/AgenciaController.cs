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
    public class AgenciaController : Controller
    {
        // GET: Agencia
        public ActionResult Index()
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
                    return View(JsonConvert.DeserializeObject<List<Models.Agencia>>(response.Content));
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Agencia/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "agencia");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    Agencia a = JsonConvert.DeserializeObject<Models.Agencia>(response.Content);
                    return View(a);
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Agencia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agencia/Create
        [HttpPost]
        public ActionResult Create(Agencia collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "agencia");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "La agencia fue dada de alta correctamente";
                    return RedirectToAction("index", "agencia");
                }
                else
                {
                    ViewBag.ERROR = "Hubo un problema al dar de alta la agencia: " + response.Content.ToString() + ". Por favor revise todos los datos y vuelva a intentarlo";
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: Agencia/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient(Direcciones.ApiRest + "agencia");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
            request.AddQueryParameter("id", id.ToString());
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                return View(JsonConvert.DeserializeObject<Models.Agencia>(response.Content));
            }
            ViewBag.ERROR = "Ocurrio algun error al cargar los datos a modificar, por favor intente recargar la pagin nuevamente";
            return View();
        }

        // POST: Agencia/Edit/5
        [HttpPost]
        public ActionResult Edit(Agencia collection)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "agencia");
                var request = new RestRequest(Method.PUT);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddJsonBody(collection);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "Se ha actualizado la agencia correctamente";
                    return RedirectToAction("index", "agencia");
                }
                ViewBag.ERROR = response.Content;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Agencia/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient(Direcciones.ApiRest + "agencia");
            var request = new RestRequest(Method.GET);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
            request.AddQueryParameter("id", id.ToString());
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                return View(JsonConvert.DeserializeObject<Models.Agencia>(response.Content));
            }
            ViewBag.ERROR = "Ocurrio algun error al cargar los datos a eliminar, por favor intente recargar la pagin nuevamente";
            return View();
        }

        // POST: Agencia/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Agencia a)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "agencia");
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("id", id.ToString());
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "La agencia fue borrada correctamente";
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
