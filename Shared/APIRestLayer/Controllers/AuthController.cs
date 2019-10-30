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
using System.Dynamic;

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
            try
            {
                if (login == null)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);

                BLUsuario blusuario = new BLUsuario();
                SUsuario usuario = blusuario.login(login);
                if (usuario != null)
                {
                    dynamic res = new ExpandoObject();
                    res.Token = TokenGenerator.GenerateTokenJwt(login.Email); ;
                    res.Email = usuario.Email;
                    res.Rol = usuario.Rol;
                    return Ok(res);
                }
                else
                {
                    return Content(HttpStatusCode.Unauthorized, "El usuario no existe");
                }
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
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

        [HttpGet]
        [Route("prueba")]
        public IHttpActionResult prueba()
        {
            try
            {
                BLUsuario bl = new BLUsuario();
                return Ok(bl.pruebaEmail());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

    }
}
