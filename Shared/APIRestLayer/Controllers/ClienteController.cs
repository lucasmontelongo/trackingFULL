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
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            try
            {
                BLCliente bl = new BLCliente();
                return Ok(bl.getCliente(id));
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
                BLCliente bl = new BLCliente();
                return Ok(bl.getAll());
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

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