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
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {

        [HttpPost]
        [Route("registro")]
        public IHttpActionResult Registro(SUsuario u)
        {
            u.Rol = "Cliente";
            BLUsuario blusuario = new BLUsuario();
            if(blusuario.addUsuario(u) == "OK")
            {
                var token = TokenGenerator.GenerateTokenJwt(u.Email);
                return Ok(token);
            }
            return InternalServerError();
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
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("google")]
        public IHttpActionResult AuthenticateGoogle(string token)
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
                return Unauthorized();
            }
        }


    }
}
