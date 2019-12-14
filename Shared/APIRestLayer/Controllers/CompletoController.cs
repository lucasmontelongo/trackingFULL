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
            trayecto.ListaPuntosControlAgencia.ForEach(x =>
            {
                if (x.Agencia != null)
                {
                    if (x.IdAgencia == null)
                    {
                        x.Agencia = blAgencia.addAgencia(x.Agencia);
                        x.IdAgencia = x.Agencia.Id;
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
                SUsuario usuario = d.usuario;
                SEATrayecto trayecto = d.trayecto;

                SAEPaquete p = d.paquete;

                BLCliente bl = new BLCliente();
                BLUsuario blusuario = new BLUsuario();
                SUsuario oUsuario = blusuario.login(usuario);
                if (usuario == null)
                {
                    return Content(HttpStatusCode.Unauthorized, "El usuario no existe");
                }

                STrayecto t = trayecto;
                if ((p.IdDestinatario == p.IdRemitente && p.IdRemitente != null) || 
                    (p.Remitente != null && p.Destinatario != null && p.Destinatario.NumeroDocumento == p.Remitente.NumeroDocumento))
                {
                    return Content(HttpStatusCode.NotFound, "Error en el paquete, el destinatario no puede ser el remitente");
                }
                string sMsg = p.validacion();
                if (sMsg != "")
                {
                    return Content(HttpStatusCode.NotFound, sMsg);
                }else
                {
                    SCliente cActualDestinatario = null;
                    SCliente cActualRemitente = null;
                    if (p.IdDestinatario != null)
                    {
                        cActualDestinatario = bl.getCliente((int)p.IdDestinatario);
                    }
                    if (p.IdRemitente != null)
                    {
                        cActualRemitente = bl.getCliente((int)p.IdRemitente);
                    }


                    if (cActualDestinatario == null && p.Destinatario == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Error en el paquete, el destinatario no existe en el sistema");
                    }
                    if (cActualRemitente == null && p.Remitente == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Error en el paquete, el remitente no existe en el sistema");
                    }

                    if (p.IdTrayecto != null && p.IdTrayecto != t.Id)
                    {
                        return Content(HttpStatusCode.NotFound, "Error en el paquete, no pertenece al trayecto enviado");
                    }
                    else if (trayecto != null)
                    {
                        BLTrayecto blTrayecto = new BLTrayecto();
                        STrayecto ActualTrayecto = null;

                        if (trayecto.Id != null)
                        {
                            ActualTrayecto = blTrayecto.getTrayecto((int)trayecto.Id);
                        }

                        string sMsgTrayecto = trayecto.validasionCrearAgencias();
                        if (sMsgTrayecto != "")
                        {
                            return Content(HttpStatusCode.NotFound, sMsgTrayecto);
                        }
                        else if (trayecto.Id == null)
                        {
                            t = blTrayecto.addTrayecto(creaAgencias(trayecto));
                        }
                        else if (trayecto.compara(ActualTrayecto))
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

                    if (p.Destinatario != null && cActualDestinatario != null)
                    {
                        cActualDestinatario = bl.updateCliente(p.Destinatario);
                    }
                    else if (p.Destinatario != null && cActualDestinatario == null)
                    {
                        p.Destinatario.Id = 0;
                        cActualDestinatario = bl.addCliente(p.Destinatario);
                    }
                    if (p.Remitente != null && cActualRemitente != null)
                    {
                        cActualRemitente = bl.updateCliente(p.Remitente);
                    }
                    else if (p.Remitente != null && cActualRemitente == null)
                    {
                        p.Remitente.Id = 0;
                        cActualRemitente = bl.addCliente(p.Remitente);
                    }
                    p.IdDestinatario = cActualDestinatario.Id;
                    p.IdRemitente = cActualRemitente.Id;
                    SPaquete np = null;
                    if (p.Id == null)
                    {
                        np = blPaquete.addPaquete(p);
                    }
                    else
                    {
                        np = blPaquete.updatePaquete(p);
                    }
                    return Ok(new SAEData()
                    {
                        paquete = (SAEPaquete)np,
                        trayecto = trayecto,
                        usuario = usuario
                    });
                } 
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
