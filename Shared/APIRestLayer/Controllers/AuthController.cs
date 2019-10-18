using APIRestLayer.Models;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using BussinessLogicLayer.BL;

namespace APIRestLayer.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {

        [HttpPost]
        [Route("registro")]
        public IHttpActionResult Registro(SUsuario u)
        {
            try
            {
                u.Rol = "Cliente";
                BLUsuario blusuario = new BLUsuario();
                blusuario.addUsuario(u);
                var token = TokenGenerator.GenerateTokenJwt(u.Email);
                return Ok(token);
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Authenticate(SUsuario login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            BLUsuario blusuario = new BLUsuario();
            string isCredentialValid = blusuario.login(login);
            if (isCredentialValid == "OK")
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Email);
                return Ok(token);
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, isCredentialValid);
            }
        }

        [HttpPost]
        [Route("google")]
        public IHttpActionResult AuthenticateGoogle(string token)
        {
            //Algun dia se hara esto

            return null;
        }

        [HttpGet]
        [Route("confirmaremail")]
        public IHttpActionResult ConfirmarEmail(string email, string codigoConfirmacion)
        {
            try
            {
                BLUsuario bl = new BLUsuario();
                return Ok(bl.confirmarEmail(email, codigoConfirmacion));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

    }
}
