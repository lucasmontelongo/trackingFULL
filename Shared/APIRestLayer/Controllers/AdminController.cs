using BussinessLogicLayer.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestLayer.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {

        [HttpGet]
        public IHttpActionResult prueba()
        {
            return Ok("adad");
        }

        [HttpPost] //vete a saber por que, pero esta porqueria no la agarra si haces una peticion, en algun momento me enterare el porque
        [Route("banana")]
        public IHttpActionResult pepe(string email)
        {
            try
            {
                BLAdmin bl = new BLAdmin();
                return Ok(bl.add(email));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
