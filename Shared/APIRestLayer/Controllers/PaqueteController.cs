using BussinessLogicLayer.BL;
using Shared.Entities;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestLayer.Controllers
{
    [RoutePrefix("api/paquete")]
    public class PaqueteController : ApiController
    {
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                SPaquete p = bl.getPaquete(id);
                string rolToken = TokenInfo.getClaim(Request, "role");
                if (rolToken == "Cliente")
                {
                    string emailToken = TokenInfo.getClaim(Request, "email");
                    BLCliente blc = new BLCliente();
                    SCliente c = blc.getClienteByEmail(emailToken);
                    if (p.IdDestinatario == c.Id || p.IdRemitente == c.Id)
                    {
                        return Ok(p);
                    }
                    else
                    {
                        throw new ECompartida("Alto ahi maleante! No tienes acceso a esta informacion");
                    }
                }
                else
                {
                    return Ok(p);
                }
                
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.getAll());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult addPaquete(SPaquete a)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.addPaquete(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult updatePaquete(SPaquete a)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.updatePaquete(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult deletePaquete(int id)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.deletePaquete(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Encargado, Funcionario, Admin")]
        [HttpPost]
        [Route("avanzar")]
        public IHttpActionResult avanzar(SPaquetePuntoControl a)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.avanzar(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [Authorize(Roles = "Encargado, Funcionario, Admin")]
        [HttpPost]
        [Route("retroceder")]
        public IHttpActionResult retroceder(SPaquetePuntoControl a)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.retroceder(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [Authorize]
        [HttpGet]
        [Route("puntoscontrol")]
        public IHttpActionResult puntoscontrol(int id)
        {
            try
            {
                BLPaquetePuntoControl bl = new BLPaquetePuntoControl();
                return Ok(bl.puntosControlDeUnPaquete(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [Authorize]
        [HttpGet]
        [Route("detalle")]
        public IHttpActionResult detalle(int id)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.detallesPaquete(TokenInfo.getClaim(Request, "email"), TokenInfo.getClaim(Request, "role"), id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("filtro")]
        public IHttpActionResult filtro(PaqueteFiltroDTO filtro)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.filtro(filtro));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [Authorize(Roles = "Admin, Funcionario, Encargado")]
        [HttpGet]
        [Route("entregacliente")]
        public IHttpActionResult entregaCliente(int IdEmpleado, int IdPaquete, string codigo)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                SPaquetePuntoControl ppc = new SPaquetePuntoControl()
                {
                    IdEmpleado = IdEmpleado,
                    IdPaquete = IdPaquete
                };
                return Ok(bl.entregaCliente(ppc, codigo));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [Authorize]
        [HttpGet]
        [Route("updateenviodomicilio")]
        public IHttpActionResult updateEnvioDomicilio(string IdPaquete, bool Envio)
        {
            try
            {
                string email = TokenInfo.getClaim(Request, "email");
                SDomicilio d = new SDomicilio()
                {
                    Envio = Envio,
                    IdPaquete = Int32.Parse(IdPaquete)
                };
                BLPaquete bl = new BLPaquete();
                return Ok(bl.updateEnvioDomicilio(d, email));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

    }
}
