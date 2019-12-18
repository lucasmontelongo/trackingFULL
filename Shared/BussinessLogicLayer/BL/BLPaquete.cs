using DataAccessLayer.DAL;
using QRCoder;
using Shared.Entities;
using Shared.Exceptions;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLPaquete
    {
        private DALPaquete _dal;

        public BLPaquete()
        {
            _dal = new DALPaquete();
        }

        public SPaquete getPaquete(int id)
        {
            try
            {
                return _dal.getPaquete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> getAll()
        {
            return _dal.getAll();
        }

        public SPaquete addPaquete(SPaquete a)
        {
            try
            {
                if (new BLTrayecto().validarTrayecto((int)a.IdTrayecto))
                {
                    a.Id = 0;
                    a.FechaIngreso = DateTime.Now;
                    a.FechaEntrega = DateTime.Now;
                    a.CodigoConfirmacion = Randoms.RandomString(6);
                    a.Codigo = "";
                    a.Borrado = false;
                    //a.ListaPaquetePuntoControl = null;
                    SPaquete p = _dal.addPaquete(a);
                    if (p != null)
                    {
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(p.Id.ToString(), QRCodeGenerator.ECCLevel.Q);
                        Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                        p.Codigo = qrCode.GetGraphic(20);
                        SPaquete pr = _dal.updatePaquete(p);
                        BLEmail.nuevoPaquete(pr);
                        return pr;
                    }
                    throw new ECompartida("Algun error raro en a;adir el paquete");
                }
                throw new ECompartida("El trayecto no cumple con las condiciones necesarias, reviselo y vuelva a intentarlo");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPaquete updatePaquete(SPaquete a)
        {
            try
            {
                return _dal.updatePaquete(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deletePaquete(int id)
        {
            try
            {
                return _dal.deletePaquete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPaquetePuntoControl avanzar(SPaquetePuntoControl ppc)
        {
            try
            {
                var _dalPPC = new DALPaquetePuntoControl();
                var _dalPC = new DALPuntoControl();
                var _dalT = new DALTrayecto();
                SPaquete p = _dal.getPaquete(ppc.IdPaquete);
                if (p != null && p.Borrado == false)
                {
                    STrayecto t = _dalT.getTrayecto((int)p.IdTrayecto);
                    List<SPaquetePuntoControl> ppcList = _dalPPC.getAllByPaquete((int)p.Id);
                    ppc.FechaLlegada = DateTime.Now;
                    ppc.Borrado = false;
                    if (ppcList.Count > 0)
                    {
                        if (t.ListaPuntosControl.Max(x => x.Orden) > ppcList.Max(y => t.ListaPuntosControl.First(z => z.Id == y.IdPuntoControl).Orden)+1)
                        {
                            SPuntoControl pcActual = t.ListaPuntosControl.First(x => x.Orden == ppcList.Max(y => t.ListaPuntosControl.First(z => z.Id == y.IdPuntoControl).Orden) + 1);
                            ppc.IdPuntoControl = (int)t.ListaPuntosControl.First(x => x.Orden == pcActual.Orden).Id;
                            int tiempoEstimado = 0;
                            t.ListaPuntosControl.ForEach(x =>
                            {
                                if (x.Orden <= pcActual.Orden)
                                {
                                    tiempoEstimado += x.Tiempo;
                                }
                            });
                            int tiempoViaje = (p.FechaIngreso - ppc.FechaLlegada).Seconds;
                            if ((tiempoViaje <= tiempoEstimado))
                            {
                                ppc.Retraso -= (tiempoEstimado - tiempoViaje);
                            }
                            else
                            {
                                ppc.Retraso += (tiempoViaje - tiempoEstimado);
                            }
                            BLEmail.actualizacionEstado(new BLPaquete().getPaquete(ppc.IdPaquete));
                            return _dalPPC.addPaquetePuntoControl(ppc);
                        }
                        else if(t.ListaPuntosControl.Max(x => x.Orden) == ppcList.Max(y => t.ListaPuntosControl.First(z => z.Id == y.IdPuntoControl).Orden) + 1)
                        {
                            throw new ECompartida("Solo queda el ultimo paso de entrega, para esto debe realizar la peticion correspondiente enviando el codigo proporcionado por el cliente");
                        }
                        else
                        {
                            throw new ECompartida("El paquete ya llego a su punto final, no se puede avanzar mas");
                        }
                    }
                    else
                    {
                        ppc.IdPuntoControl = (int)t.ListaPuntosControl.First(x => x.Orden == 1).Id;
                        return _dalPPC.addPaquetePuntoControl(ppc);
                    }
                }

                throw new ECompartida("Error");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool retroceder(SPaquetePuntoControl ppc)
        {
            try
            {
                var _dalPPC = new DALPaquetePuntoControl();
                List<SPaquetePuntoControl> ppcList = _dalPPC.getAllByPaquete(ppc.IdPaquete);
                if(ppcList.Count() > 0)
                {
                    int ppcAEliminarId = ppcList.Max(x => x.Id);
                    SPaquetePuntoControl ppcAEliminar = ppcList.First(x => x.Id == ppcAEliminarId);
                    if (ppcAEliminar != null)
                    {
                        var _dalU = new DALUsuario();
                        SUsuario empleado = _dalU.getUsuario(ppc.IdEmpleado);
                        if ((empleado.Rol == "Funcionario" && empleado.Id == ppcAEliminar.IdEmpleado) || empleado.Rol == "Encargado" || empleado.Rol == "Admin")
                        {
                            BLEmail.actualizacionEstado(new BLPaquete().getPaquete(ppc.IdPaquete));
                            return _dalPPC.deletePaquetePuntoControl(ppcAEliminar.Id);
                        }
                        else
                        {
                            throw new ECompartida("El usuario que realizo la peticion no tiene autorizacion para realizar esta operacion");
                        }
                    }
                    throw new ECompartida("No se encontro ningun paquete con el ID enviado");
                }
                throw new ECompartida("No se puede retroceder mas el paquete");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> paquetesEnviados(int id)
        {
            try
            {
                return _dal.paquetesEnviados(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> paquetesRecibidos(int id)
        {
            try
            {
                return _dal.paquetesRecibidos(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dynamic detallesPaquete(string email, string role, int idPaquete)
        {
            try
            {
                SPaquete paquete = this.getPaquete(idPaquete);
                BLCliente blCliente = new BLCliente();
                if (role != "Admin")
                {
                    SCliente cliente = blCliente.getClienteByEmail(email);
                    if (cliente != null)
                    {
                        if (cliente.Id != paquete.IdDestinatario && cliente.Id != paquete.IdRemitente)
                        {
                            throw new ECompartida("No tienes acceso a la informacion de este paquete");
                        }
                    }
                    else
                    {
                        throw new ECompartida("El email enviado en la solicitud no pertenece a un cliente del sistema");
                    }
                }
                SCliente Remitente = blCliente.getCliente((int)paquete.IdRemitente);
                SCliente Destinatario = blCliente.getCliente((int)paquete.IdDestinatario);
                BLTrayecto bLTrayecto = new BLTrayecto();
                STrayecto Trayecto = bLTrayecto.getTrayecto((int)paquete.IdTrayecto);
                BLPuntoControl bLPuntoControl = new BLPuntoControl();
                Trayecto.ListaPuntosControl = bLPuntoControl.puntosControlDeUnTrayecto((int)paquete.IdTrayecto);
                BLPaquetePuntoControl bLPaquetePuntoControl = new BLPaquetePuntoControl();
                List<SPaquetePuntoControl> PaquetePuntosControl = bLPaquetePuntoControl.puntosControlDeUnPaquete((int)paquete.Id);
                dynamic respuesta = new ExpandoObject();
                respuesta.IdTrayecto = paquete.Id;
                respuesta.Qr = paquete.Codigo;
                respuesta.Trayecto = Trayecto;
                respuesta.Remitente = Remitente;
                respuesta.Destinatario = Destinatario;
                respuesta.PaquetePuntoControl = PaquetePuntosControl;
                return respuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> filtro(PaqueteFiltroDTO filtro)
        {
            try
            {
                List<SPaquete> todos = _dal.getAll();
                bool cambio = false;
                List<SPaquete> temporal = new List<SPaquete>();
                List<SPaquete> respuesta = new List<SPaquete>();
                BLCliente _blCliente = new BLCliente();
                if (filtro.FechaFinal != null && filtro.FechaInicio != null)
                {
                    todos.Where(x => ((x.FechaIngreso >= filtro.FechaInicio) && (x.FechaIngreso <= filtro.FechaFinal)) || ((x.FechaEntrega >= filtro.FechaInicio) && (x.FechaEntrega <= filtro.FechaFinal))).ToList().ForEach(x => {
                        respuesta.Add(x);
                    });
                    todos = respuesta;
                    respuesta = new List<SPaquete>();
                    cambio = true;
                }
                if (filtro.Remitente != null)
                {
                    todos.Where(x => x.IdRemitente == _blCliente.getClienteByEmail(filtro.Remitente).Id).ToList().ForEach(x =>
                    {
                        if (respuesta.FirstOrDefault(z => z.Id == x.Id) == null)
                        {
                            respuesta.Add(x);
                        }
                    });
                    todos = respuesta;
                    respuesta = new List<SPaquete>();
                    cambio = true;
                }
                if (filtro.Destinatario != null)
                {
                    todos.Where(x => x.IdDestinatario == _blCliente.getClienteByEmail(filtro.Destinatario).Id).ToList().ForEach(x =>
                    {
                        if (respuesta.FirstOrDefault(z => z.Id == x.Id) == null)
                        {
                            respuesta.Add(x);
                        }
                    });
                    todos = respuesta;
                    respuesta = new List<SPaquete>();
                    cambio = true;
                }
                if (filtro.Estado != null)
                {
                    if (filtro.Estado == "En viaje")
                    {
                        todos.Where(x => x.FechaIngreso == x.FechaEntrega).ToList().ForEach(x =>
                        {
                            if (respuesta.FirstOrDefault(z => z.Id == x.Id) == null)
                            {
                                respuesta.Add(x);
                            }
                        });
                        todos = respuesta;
                        respuesta = new List<SPaquete>();
                        cambio = true;
                    }
                    else
                    {
                        todos.Where(x => x.FechaIngreso != x.FechaEntrega).ToList().ForEach(x =>
                        {
                            if (respuesta.FirstOrDefault(z => z.Id == x.Id) == null)
                            {
                                respuesta.Add(x);
                            }
                        });
                        todos = respuesta;
                        respuesta = new List<SPaquete>();
                        cambio = true;
                    }
                }
                if (cambio)
                {
                    return todos;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPaquetePuntoControl entregaCliente(SPaquetePuntoControl ppc, string codigo)
        {
            try
            {
                SPaquete p = _dal.getPaquete(ppc.IdPaquete);
                if(p.CodigoConfirmacion == codigo)
                {
                    var _dalPPC = new DALPaquetePuntoControl();
                    var _dalPC = new DALPuntoControl();
                    var _dalT = new DALTrayecto();
                    ppc.FechaLlegada = DateTime.Now;
                    ppc.Borrado = false;
                    var pclist = _dalPC.puntosControlDeUnTrayecto((int)p.IdTrayecto);
                    ppc.IdPuntoControl = pclist.Max(x => (int)x.Id);
                    List<SPaquetePuntoControl> ppcList = _dalPPC.getAllByPaquete((int)p.Id);
                    ppcList.ForEach(x =>
                    {
                        if (x.IdPuntoControl == ppc.IdPuntoControl)
                        {
                            throw new ECompartida("El paquete ya fue entregado al cliente anteriormente");
                        }
                    });
                    if (ppcList.Count != (pclist.Count - 1))
                    {
                        throw new ECompartida("Aun falta avanzar pasos antes de poder entregar al cliente");
                    }
                    int tiempoEstimado = 0;
                    pclist.ForEach(x =>
                    {
                        if (x.Orden <= pclist.First(z => z.Id == ppc.IdPuntoControl).Orden)
                        {
                            tiempoEstimado += x.Tiempo;
                        }
                    });
                    int tiempoViaje = (p.FechaIngreso - ppc.FechaLlegada).Seconds;
                    if ((tiempoViaje <= tiempoEstimado))
                    {
                        ppc.Retraso -= (tiempoEstimado - tiempoViaje);
                    }
                    else
                    {
                        ppc.Retraso += (tiempoViaje - tiempoEstimado);
                    }
                    return _dalPPC.addPaquetePuntoControl(ppc);
                }
                throw new ECompartida("El codigo no coincide con el paquete");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool updateEnvioDomicilio(SDomicilio d, string email)
        {
            try
            {
                SPaquete p = getPaquete(d.IdPaquete);
                BLCliente _blC = new BLCliente();
                SCliente c = _blC.getCliente((int)p.IdDestinatario);
                if (c.Email == email)
                {
                    return _dal.updateEnvioDomicilio(d);
                }
                throw new ECompartida("No tienes permisos para realizar esta accion");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string consultarEstado(string codigo, string email)
        {
            try
            {
                BLCliente _blC = new BLCliente();
                SCliente c = _blC.getClienteByEmail(email);
                SPaquete p = _dal.consultaEstado(c.Id, codigo);
                if (p.FechaEntrega == p.FechaIngreso)
                {
                    return "El paquete se encuentra en viaje actualmente, para mas detalles: " + Direcciones.Web + "paquete/details/" + p.Id;
                }
                return "El paquete ya fue entregado, para mas detalles: " + Direcciones.Web + "paquete/details/" + p.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool tieneEnvio(int id)
        {
            try
            {
                BLAgencia blA = new BLAgencia();
                if (blA.getAgencia(id).EnvioDomicilio == true)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
