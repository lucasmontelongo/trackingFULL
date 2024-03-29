﻿using BussinessLogicLayer.BL;
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
        [Route("addpermisos")]
        public IHttpActionResult addpermisos(string email, string role)
        {
            try
            {
                BLAdmin bl = new BLAdmin();
                return Ok(bl.add(email, role));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("estadistica/trayectopaquete")]
        public IHttpActionResult trayectoPaquete()
        {
            try
            {
                BLAdmin bl = new BLAdmin();
                return Ok(bl.trayectoPaquete());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        [Route("estadistica/paquetesingresados")]
        public IHttpActionResult paquetesIngresados(string tipo)
        {
            try
            {
                BLAdmin bl = new BLAdmin();
                return Ok(bl.paquetesIngresadosEntregados(tipo));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
