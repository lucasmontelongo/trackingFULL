using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shared.Entities;
using BussinessLogicLayer.BL;


namespace APIRestLayer.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/completo")]
    public class CompletoController : ApiController
    {

        private STrayecto creaAgencias(SEATrayecto trayecto)
        {
            BLAgencia blAgencia = new BLAgencia();
            List<SPuntoControl> ListaPuntosControl = new List<SPuntoControl>();
            trayecto.ListaPuntosControl.ForEach(x =>
            {
                if (x.Agencia != null)
                {
                    if (x.IdAgencia == null)
                    {
                        x.Agencia = blAgencia.addAgencia(x.Agencia);
                        x.IdAgencia = x.Id;
                    }
                    else
                    {
                        blAgencia.updateAgencia(x.Agencia);
                    }
                }
                ListaPuntosControl.Add(x);
            });
            STrayecto t = trayecto;
            t.ListaPuntosControl = ListaPuntosControl;
            return t;
        } 

        [HttpPost]
        public IHttpActionResult Authenticate(SAEData d)
        {
            try
            {
                /*
                SAEData d = new SAEData()
                {
                    usuario = new SUsuario()
                    {
                        Email = "lucasmontelongo@outlook.com",
                        Password = "123456"
                    },
                    trayecto = new SEATrayecto()
                    {
                        //   Id = 1,
                        Nombre = "Trayecto2",
                        ListaPuntosControl = new List<SEAPuntoControlAgencia>()
                        {
                            new SEAPuntoControlAgencia()
                            {
                                Orden = 1,
                                Tiempo = 0,
                                IdAgencia = 1,
                                IdTrayecto = 1,
                                Borrado = false,
                                Nombre = "Recibido en origen"
                            },
                            new SEAPuntoControlAgencia()
                            {
                                Orden = 2,
                                Tiempo = 10,
                                IdAgencia = null,
                                IdTrayecto = 3,
                                Borrado = false,
                                Nombre = "Recibido en origen",
                                Agencia = new SAgencia()
                            }
                        }
                    },
                    paquete = new SPaquete()
                    {
                        //    Id = 0,
                        Codigo = "codigo",
                        CodigoConfirmacion = "codigo",
                        FechaIngreso = new DateTime(),
                        FechaEntrega = new DateTime(),
                        IdTrayecto = 0,
                        IdRemitente = 6,
                        IdDestinatario = 5,
                        Borrado = false
                    }
                };
                */
                SUsuario usuario = d.usuario;
                SEATrayecto trayecto = d.trayecto;
                SPaquete p = d.paquete;

                BLUsuario blusuario = new BLUsuario();
                SUsuario oUsuario = blusuario.login(usuario);
                if (usuario == null)
                {
                    return Content(HttpStatusCode.Unauthorized, "El usuario no existe");
                }

                STrayecto t = trayecto;

                if (p.IdTrayecto != null && p.IdTrayecto != t.Id)
                {
                    return Content(HttpStatusCode.NotFound, "Error en el paquete, no pertenece al trayecto enviado");

                }
                else if (trayecto != null)
                {
                    BLTrayecto blTrayecto = new BLTrayecto();
                    string sMsgTrayecto = trayecto.validasionCrearAgencias();
                    if (sMsgTrayecto != "")
                    {
                        return Content(HttpStatusCode.NotFound, sMsgTrayecto);
                    }
                    else if (trayecto.Id == null)
                    {
                        t = blTrayecto.addTrayecto(creaAgencias(trayecto));
                    } 
                    else
                    {
                        if (blTrayecto.paquetesTransito(trayecto) != 0)
                        {
                            return Content(HttpStatusCode.NotFound, "Hay paquetes en transito");
                        }
                        else
                        {
                            t = blTrayecto.updateTrayecto(creaAgencias(trayecto));
                        }

                    }
                }

                BLPaquete blPaquete = new BLPaquete();
                if (p.IdTrayecto == null)
                {
                    p.IdTrayecto = t.Id;
                }
                if (p.Id == null)
                {
                    p = blPaquete.addPaquete(p);
                }
                else
                {
                    p = blPaquete.updatePaquete(p);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

/*
 
//TRAYECTOS
    {
        "Nombre": "Trayecto4",
        "ListaPuntosControl": [
            {
                "Orden": 1,
                "Tiempo": 0,
                "IdAgencia": 1,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "Recibido en origen"
            },
            {
                "Orden": 2,
                "Tiempo": 6550,
                "IdAgencia": 1,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "Esperando en origen"
            },
            {
                "Orden": 3,
                "Tiempo": 5699,
                "IdAgencia": null,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "En viaje"
            },
            {
                "Orden": 4,
                "Tiempo": 4566,
                "IdAgencia": 3,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "Recibido en agencia"
            },
            {
                "Orden": 5,
                "Tiempo": 0,
                "IdAgencia": 3,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "Esperando en agencia"
            },
            {
                "Orden": 6,
                "Tiempo": 0,
                "IdAgencia": null,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "En viaje"
            },
            {
                "Orden": 7,
                "Tiempo": 0,
                "IdAgencia": 2,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "Recibido en destino"
            },
            {
                "Orden": 8,
                "Tiempo": 0,
                "IdAgencia": null,
                "IdTrayecto": 1,
                "Borrado": false,
                "Nombre": "Entregado al cliente"
            }
        ]
    }


//USUARIOS

     
*/
