using Newtonsoft.Json;
using RestSharp;
using Rotativa;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBLayer2.Models;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using QRCoder;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Shared.Exceptions;

namespace WEBLayer2.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [Route("addpermisos")]
        public ActionResult addpermisos()
        {
            return View();
        }

        [HttpPost]
        [Route("addpermisos")]
        public ActionResult addpermisos(string email, string role)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "admin/addpermisos");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Request.Cookies["Token"].Value);
                request.AddQueryParameter("email", email);
                request.AddQueryParameter("role", role);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ViewBag.OK = "El usuario con el email " + email + " ahora tiene permisos de " + role;                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return addpermisos();
            }
        }
        
        public ActionResult EtiquetaPDF(int Id, string r)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "paquete/detalle");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + r);
                request.AddQueryParameter("id", Id.ToString());
                IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                return View(JsonConvert.DeserializeObject<Models.PaqueteDTO>(response.Content));
        }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
}
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        private System.Drawing.Image GenerateQRCode(string content, int size)
        {
            QrEncoder encoder = new QrEncoder(ErrorCorrectionLevel.Q);
            QrCode qrCode;
            encoder.TryEncode(content, out qrCode);

            GraphicsRenderer gRenderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Two), System.Drawing.Brushes.Black, System.Drawing.Brushes.White);

            MemoryStream ms = new MemoryStream();
            gRenderer.WriteToStream(qrCode.Matrix, ImageFormat.Bmp, ms);

            var imageTemp = new Bitmap(ms);

            var image = new Bitmap(imageTemp, new System.Drawing.Size(new System.Drawing.Point(size, size)));

            return (System.Drawing.Image)image;
        }

        [Route("imprimirpdf")]
        public ActionResult imprimirpdf(int id)
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
                    PaqueteDTO paq = JsonConvert.DeserializeObject<Models.PaqueteDTO>(response.Content);
                    PDF pdf = new PDF();
                    System.Drawing.Image imagen = GenerateQRCode(id.ToString(), 100);
                    iTextSharp.text.Image imagenQR = iTextSharp.text.Image.GetInstance(imagen, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] res = pdf.PrepararReport(paq, imagenQR);
                    return File(res, "application/pdf");
                }
                else
                {
                    throw new ECompartida(response.Content);
                }
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        [HttpGet]
        public ActionResult TrayectoPaquete(string token)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "admin/estadistica/trayectopaquete");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<Shared.Entities.EstadisticaDTO> estadisticas = JsonConvert.DeserializeObject<List<Shared.Entities.EstadisticaDTO>>(response.Content);
                    List<string> ejeX = new List<string>();
                    List<int> ejeY = new List<int>();
                    estadisticas.ForEach(x =>
                    {
                        ejeX.Add(x.x);
                        ejeY.Add(x.y);
                    });
                    ViewBag.EJEX = ejeX;
                    ViewBag.EJEY = ejeY;
                    return View();
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        [HttpGet]
        public ActionResult PaquetesIngresados(string token)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "admin/estadistica/paquetesingresados");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddQueryParameter("tipo", "ingresado");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<Shared.Entities.EstadisticaDTO> estadisticas = JsonConvert.DeserializeObject<List<Shared.Entities.EstadisticaDTO>>(response.Content);
                    List<string> ejeX = new List<string>();
                    List<int> ejeY = new List<int>();
                    estadisticas.ForEach(x =>
                    {
                        ejeX.Add(x.x);
                        ejeY.Add(x.y);
                    });
                    ViewBag.EJEX = ejeX;
                    ViewBag.EJEY = ejeY;
                    return View();
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        [HttpGet]
        public ActionResult PaquetesEntregados(string token)
        {
            try
            {
                var client = new RestClient(Direcciones.ApiRest + "admin/estadistica/paquetesingresados");
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddQueryParameter("tipo", "entregado");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    List<Shared.Entities.EstadisticaDTO> estadisticas = JsonConvert.DeserializeObject<List<Shared.Entities.EstadisticaDTO>>(response.Content);
                    List<string> ejeX = new List<string>();
                    List<int> ejeY = new List<int>();
                    estadisticas.ForEach(x =>
                    {
                        ejeX.Add(x.x);
                        ejeY.Add(x.y);
                    });
                    ViewBag.EJEX = ejeX;
                    ViewBag.EJEY = ejeY;
                    return View();
                }
                else
                {
                    ViewBag.ERROR = response.Content;
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        public ActionResult Estadisticas()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Estadisticas(string tipo)
        {
            try
            {
                if (tipo == "trayectoPaquete")
                {
                    return RedirectToAction("TrayectoPaquete", new { token = Request.Cookies["Token"].Value });
                }
                if (tipo == "paquetesIngresados")
                {
                    return RedirectToAction("PaquetesIngresados", new { token = Request.Cookies["Token"].Value });
                }
                if (tipo == "paquetesEntregados")
                {
                    return RedirectToAction("PaquetesEntregados", new { token = Request.Cookies["Token"].Value });
                }
                return Estadisticas();
            }
            catch (Exception e)
            {
                ViewBag.ERROR = e;
                return View();
            }
        }

        

    }
}