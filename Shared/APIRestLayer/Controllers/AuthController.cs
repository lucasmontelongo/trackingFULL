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
        public IHttpActionResult Authenticate(LoginRequest login)
        {
            if (login == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            //TODO: Validate credentials Correctly, this code is only for demo !!
            bool isCredentialValid = (login.Password == "123456");
            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(login.Username);
                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
