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
                if (!bl.paquetesEnTransito((int)a.Id))
                {
                    return Ok(bl.updateTrayecto(a));
                }
                throw new ECompartida("Hay paquetes en transito en este trayecto actualmente");
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
                if (!bl.paquetesEnTransito(id))
                {
                    return Ok(bl.deleteTrayecto(id));
                }
                throw new ECompartida("Hay paquetes en transito en este trayecto actualmente");
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

        [HttpPost]
        [Route("addpuntocontrol")]
        public IHttpActionResult addPuntoControl(SPuntoControl pc)
        {
            try
            {
                BLPuntoControl bl = new BLPuntoControl();
                BLTrayecto blT = new BLTrayecto();
                if (!blT.paquetesEnTransito((int)pc.IdTrayecto))
                {
                    return Ok(bl.addPuntoControl(pc));
                }
                throw new ECompartida("Hay paquetes en transito en este trayecto actualmente");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpGet]
        [Route("getpuntocontrol")]
        public IHttpActionResult getPuntoControl(int id)
        {
            try
            {
                BLPuntoControl bl = new BLPuntoControl();
                return Ok(bl.getPuntoControl(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpPut]
        [Route("editpuntocontrol")]
        public IHttpActionResult editPuntoControl(SPuntoControl pc)
        {
            try
            {
                BLPuntoControl bl = new BLPuntoControl();
                BLTrayecto blT = new BLTrayecto();
                if (!blT.paquetesEnTransito((int)pc.IdTrayecto))
                {
                    return Ok(bl.updatePuntoControl(pc));
                }
                throw new ECompartida("Hay paquetes en transito en este trayecto actualmente");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpDelete]
        [Route("deletepuntocontrol")]
        public IHttpActionResult deletePuntoControl(int id)
        {
            try
            {
                BLPuntoControl bl = new BLPuntoControl();
                BLTrayecto blT = new BLTrayecto();
                if (!blT.paquetesEnTransito(id))
                {
                    return Ok(bl.deletePuntoControl(id));
                }
                throw new ECompartida("Hay paquetes en transito en este trayecto actualmente");
                
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }

        }

    }
}
