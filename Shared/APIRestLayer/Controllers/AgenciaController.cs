using BussinessLogicLayer.BL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestLayer.Controllers
{
    /// <summary>
    /// customer controller class for testing security token
    /// </summary>
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/agencia")]
    public class AgenciaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            try
            {
                BLAgencia bl = new BLAgencia();
                return Ok(bl.getAgencia(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                BLAgencia bl = new BLAgencia();
                return Ok(bl.getAll());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }
        
        [HttpPost]
        public IHttpActionResult addAgencia(SAgencia a)
        {
            try
            {
                BLAgencia bl = new BLAgencia();
                return Ok(bl.addAgencia(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        public IHttpActionResult updateAgencia(SAgencia a)
        {
            try
            {
                BLAgencia bl = new BLAgencia();
                return Ok(bl.updateAgencia(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

    }
}
