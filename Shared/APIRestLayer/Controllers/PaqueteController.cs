﻿using BussinessLogicLayer.BL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIRestLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/paquete")]
    public class PaqueteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.getPaquete(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.getAll());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public IHttpActionResult addPaquete(SPaquete a)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.addPaquete(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPut]
        public IHttpActionResult updatePaquete(SPaquete a)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.updatePaquete(a));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult deletePaquete(int id)
        {
            try
            {
                BLPaquete bl = new BLPaquete();
                return Ok(bl.deletePaquete(id));
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}
