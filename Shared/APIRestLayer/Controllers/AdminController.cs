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
        [HttpPost]
        public IHttpActionResult addAdmin(string email)
        {
            try
            {

            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
