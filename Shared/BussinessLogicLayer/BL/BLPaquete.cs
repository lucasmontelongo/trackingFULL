using DataAccessLayer.DAL;
using QRCoder;
using Shared.Entities;
using Shared.Exceptions;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                    //string lista = "";
                    //t.ListaPuntosControl.ForEach(x =>
                    //{
                    //    lista += " " + x.Orden.ToString();
                    //});
                    //throw new ECompartida(lista);
                    if (ppcList.Count > 0)
                    {
                        if (t.ListaPuntosControl.Max(x => x.Orden) > ppcList.Max(y => t.ListaPuntosControl.First(z => z.Id == y.IdPuntoControl).Orden))
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

    }
}
