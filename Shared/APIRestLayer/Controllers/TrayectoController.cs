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
    [Authorize(Roles = "Admin, Cliente")]
    [RoutePrefix("api/trayecto")]
    public class TrayectoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            try
            {
                BLTrayecto bl = new BLTrayecto();
                return Ok(bl.getTrayecto(id));
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
                BLTrayecto bl = new BLTrayecto();
                return Ok(bl.getAll());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpPost]
        public IHttpActionResult addTrayecto(STrayecto a)
        {
            try
            {
                BLTrayecto bl = new BLTrayecto();
                return Ok(bl.addTrayecto(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpPut]
        public IHttpActionResult updateTrayecto(STrayecto a)
        {
            try
            {
                BLTrayecto bl = new BLTrayecto();
                return Ok(bl.updateTrayecto(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }

        [HttpDelete]
        public IHttpActionResult deleteTrayecto(int id)
        {
            try
            {
                BLTrayecto bl = new BLTrayecto();
                return Ok(bl.deleteTrayecto(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }

        }

        [HttpGet]
        [Route("puntoscontrol")]
        public IHttpActionResult puntosControl(int id)
        {
            try
            {
                BLPuntoControl bl = new BLPuntoControl();
                return Ok(bl.puntosControlDeUnTrayecto(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

    }
}
