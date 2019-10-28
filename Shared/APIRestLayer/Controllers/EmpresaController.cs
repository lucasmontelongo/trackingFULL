using BussinessLogicLayer.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestLayer.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
    {
        [HttpGet]
        [Route("nuevoAdmin")]
        public IHttpActionResult newAdmin(int id)
        {
            try
            {
                BLUsuario bl = new BLUsuario();
                return Ok(bl.newAdmin(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
