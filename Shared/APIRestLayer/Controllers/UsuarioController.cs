using BussinessLogicLayer.BL;
using Shared.Entities;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestLayer.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin, Cliente, Encargado, Funcionario")]
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("paquetesenviados")]
        public IHttpActionResult GetPaquetesEnviados()
        {
            try
            {
                string email = APIRestLayer.TokenInfo.getClaim(Request, "email");
                BLUsuario bl = new BLUsuario();
                return Ok(bl.paquetesEnviados(email));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("paquetesrecibidos")]
        public IHttpActionResult GetPaquetesRecibidos()
        {
            try
            {
                string email = APIRestLayer.TokenInfo.getClaim(Request, "email");
                BLUsuario bl = new BLUsuario();
                return Ok(bl.paquetesRecibidos(email));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
