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
                a.Id = 0;
                a.FechaIngreso = DateTime.Now;
                a.FechaEntrega = DateTime.Now;
                a.CodigoConfirmacion = Randoms.RandomString(6);
                a.Codigo = "";
                a.Borrado = false;
                a.ListaPaquetePuntoControl = null;
                SPaquete p = _dal.addPaquete(a);
                if (p != null)
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(p.Id.ToString(), QRCodeGenerator.ECCLevel.Q);
                    Base64QRCode qrCode = new Base64QRCode(qrCodeData);
                    p.Codigo = qrCode.GetGraphic(20);
                    return _dal.updatePaquete(p);
                }
                throw new ECompartida("Algun error raro en a;adir el paquete"); 
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
                return null;
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
                    STrayecto t = _dalT.getTrayecto(p.IdTrayecto);
                    List<SPaquetePuntoControl> ppcList = _dalPPC.getAllByPaquete(p.Id);
                    ppc.FechaLlegada = DateTime.Now;
                    ppc.Borrado = false;
                    if (ppcList.Count > 0)
                    {
                        if (t.ListaPuntosControl.Max(x => x.Orden) > ppcList.Max(y => t.ListaPuntosControl.First(z => z.Id == y.IdPuntoControl).Orden)+1)
                        {
                            SPuntoControl pcActual = t.ListaPuntosControl.First(x => x.Orden == ppcList.Max(y => t.ListaPuntosControl.First(z => z.Id == y.IdPuntoControl).Orden) + 1);
                            ppc.IdPuntoControl = t.ListaPuntosControl.First(x => x.Orden == pcActual.Orden).Id;
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
                        ppc.IdPuntoControl = t.ListaPuntosControl.First(x => x.Orden == 1).Id;
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
                    throw new ECompartida("El email enviado en la solicitud no pertenece a un cliente del sistema");
                }
                SCliente Remitente = blCliente.getCliente(paquete.IdRemitente);
                SCliente Destinatario = blCliente.getCliente(paquete.IdDestinatario);
                BLTrayecto bLTrayecto = new BLTrayecto();
                STrayecto Trayecto = bLTrayecto.getTrayecto(paquete.IdTrayecto);
                BLPuntoControl bLPuntoControl = new BLPuntoControl();
                Trayecto.ListaPuntosControl = bLPuntoControl.puntosControlDeUnTrayecto(paquete.IdTrayecto);
                BLPaquetePuntoControl bLPaquetePuntoControl = new BLPaquetePuntoControl();
                List<SPaquetePuntoControl> PaquetePuntosControl = bLPaquetePuntoControl.puntosControlDeUnPaquete(paquete.Id);
                dynamic respuesta = new ExpandoObject();
                respuesta.IdTrayecto = paquete.Id;
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

    }
}
