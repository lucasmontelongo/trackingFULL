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
                STrayecto ActualTrayecto = null;

                SAEPaquete p = d.paquete;

                BLCliente bl = new BLCliente();
                BLUsuario blusuario = new BLUsuario();
                SUsuario oUsuario = blusuario.login(usuario);
                BLPaquete blPaquete = new BLPaquete();

                if (usuario == null)
                {
                    return Content(HttpStatusCode.Unauthorized, "El usuario no existe");
                }
                else if (oUsuario.Rol != "Admin" && oUsuario.Rol != "Funcionario" && oUsuario.Rol != "Encargado")
                {
                    return Content(HttpStatusCode.Unauthorized, "El usuario no esta autorisado para usar elte servicio");
                }

                if (p.adelanta != null)
                {
                    if (p.Id == null)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Error en paquete, se intento adelantar pero no se recibio el identificador");
                    }
                    else
                    {
                        return Ok(blPaquete.avanzar(new SPaquetePuntoControl() { IdPaquete = (int)p.Id, IdEmpleado = oUsuario.Id }));
                    }
                }
                if (p.atrasa != null)
                {
                    if (p.Id == null)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Error en paquete, se intento retroceder pero no se recibio el identificador");
                    } 
                    else
                    {

                        return Ok(blPaquete.retroceder(new SPaquetePuntoControl() { IdPaquete = (int)p.Id, IdEmpleado = oUsuario.Id }));
                    }
                }
                if (p.entrega != null)
                {
                    if (p.Id == null)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Error en paquete, se intento entregar pero no se recibio el identificador");
                    }
                    else if (p.code == null)
                    {
                        return Content(HttpStatusCode.Unauthorized, "Error en paquete, se intento entregar pero no se recibio el codigo");
                    }
                    else
                    {
                        return Ok(blPaquete.entregaCliente(new SPaquetePuntoControl()
                        {
                            IdEmpleado = oUsuario.Id,
                            IdPaquete = (int)p.Id
                        }, p.code));
                    }
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
                    else
                    {
                        bl.validacion(p.Destinatario);
                    }
                    if (p.IdRemitente != null)
                    {
                        cActualRemitente = bl.getCliente((int)p.IdRemitente);
                    }
                    else
                    {
                        bl.validacion(p.Remitente);
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
                    else if (trayecto == null && d.IdTrayecto == null)
                    {
                        return Content(HttpStatusCode.NotFound, "Error en el trayecto, faltan los datos");
                    }
                    else if (d.IdTrayecto != null)
                    {
                        BLTrayecto blTrayecto = new BLTrayecto();
                        ActualTrayecto = blTrayecto.getTrayecto((int)d.IdTrayecto);
                        if (ActualTrayecto == null)
                        {
                            return Content(HttpStatusCode.NotFound, "El trayecto no existe.");
                        }
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
                            ActualTrayecto = blTrayecto.addTrayecto(creaAgencias(trayecto));
                        }
                        else
                        {
                            if (blTrayecto.paquetesTransito(trayecto) != 0)
                            {
                                return Content(HttpStatusCode.NotFound, "Hay paquetes en transito");
                            }
                            else
                            {
                                ActualTrayecto = blTrayecto.updateTrayecto(creaAgencias(trayecto));
                            }

                        }
                    }

                    if (p.IdTrayecto == null)
                    {
                        p.IdTrayecto = ActualTrayecto.Id;
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
                    return Ok(new SAERespuesta() {
                        usuario = usuario,
                        trayecto = t,
                        paquete = np
                    });
                } 
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e.ToString());
            }
        }
    }
}