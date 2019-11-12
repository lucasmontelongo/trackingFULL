using Newtonsoft.Json;
using RestSharp;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBLayer.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Envios()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult SeguirPaquete()
        {
            return View();
        }

        public ActionResult Recibidos()
        {
            return View();
        }

        [Route("paquetesenviados")]
        public ActionResult PaquetesEnviados()
        {
            try
            {
                if (Session["USUARIO_ID"] != null)
                {
                    var client = new RestClient(Direcciones.ApiRest + "usuario/paquetesenviados");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Session["USUARIO_TOKEN"].ToString());
                    request.AddQueryParameter("id", Session["USUARIO_ID"].ToString());
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
                }
                else
                {
                    ViewBag.Error = "No tienes acceso";
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
                if (Session["USUARIO_ID"] != null)
                {
                    var client = new RestClient(Direcciones.ApiRest + "usuario/paquetesrecibidos");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + Session["USUARIO_TOKEN"].ToString());
                    request.AddQueryParameter("id", Session["USUARIO_ID"].ToString());
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        List<Models.Paquete> paquetes = new List<Models.Paquete>();
                        paquetes = JsonConvert.DeserializeObject<List<Models.Paquete>>(response.Content);
                        ViewBag.PaquetesRecibidos = paquetes;
                    }
                    else
                    {
                        ViewBag.Error = response.Content;
                    }
                }
                else
                {
                    ViewBag.Error = "No tienes acceso";
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