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
    
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        [Authorize(Roles = "Admin, Funcionario, Encargado, Cliente")]
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            try
            {
                BLCliente bl = new BLCliente();
                SCliente p = bl.getCliente(id);
                return Ok(p);
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
                BLCliente bl = new BLCliente();
                return Ok(bl.getAll());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IHttpActionResult addCliente(SCliente a)
        {
            try
            {
                BLCliente bl = new BLCliente();
                return Ok(bl.addCliente(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IHttpActionResult updateCliente(SCliente a)
        {
            try
            {
                BLCliente bl = new BLCliente();
                return Ok(bl.updateCliente(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IHttpActionResult deleteCliente(int id)
        {
            try
            {
                BLCliente bl = new BLCliente();
                return Ok(bl.deleteCliente(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
