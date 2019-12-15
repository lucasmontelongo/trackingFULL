using Shared.Entities;
using Shared.Exceptions;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALPaquete
    {
        Conversiones _conv;

        public DALPaquete()
        {
            _conv = new Conversiones();
        }

        public SPaquete getPaquete(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.Paquete.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<SPaquete> getAll()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SPaquete> puntoControl = new List<SPaquete>();
                    en.Paquete.ToList().ForEach(x =>
                    {
                        if (x.borrado == false)
                        {
                            puntoControl.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return puntoControl;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SPaquete addPaquete(SPaquete a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {

                    Paquete ag = en.Paquete.Add(_conv.entidadAModelo(a));
                    en.SaveChanges();
                    SPaquete p = _conv.modeloAEntidad(ag);
                    en.Domicilio.Add(new Domicilio() { idPaquete = (int)p.Id, envio = false });
                    en.SaveChanges();
                    return p;
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public SPaquete updatePaquete(SPaquete a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Paquete ag = en.Paquete.Find(a.Id);
                    ag = _conv.entidadAModelo(a, ag);
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public string deletePaquete(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Paquete a = en.Paquete.Find(id);
                    a.borrado = true;
                    en.SaveChanges();

                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<SPaquete> paquetesEnviados(int idCliente)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SPaquete> paqEnviados = new List<SPaquete>();
                    en.Paquete.ToList().ForEach(x =>
                    {
                        if(x.idRemitente == idCliente)
                        {
                            paqEnviados.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return paqEnviados;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<SPaquete> paquetesRecibidos(int idCliente)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SPaquete> paqEnviados = new List<SPaquete>();
                    en.Paquete.ToList().ForEach(x =>
                    {
                        if (x.idDestinatario == idCliente)
                        {
                            paqEnviados.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return paqEnviados;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool updateEnvioDomicilio(SDomicilio d)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Domicilio dom = en.Domicilio.Find(d.IdPaquete);
                    if (dom != null)
                    {
                        dom.envio = d.Envio;
                        dom.hora = d.Hora;
                        en.SaveChanges();
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

        public SPaquete consultaEstado(int idCliente, string codigo)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Paquete dom = en.Paquete.Where(x => x.idDestinatario == idCliente && x.codigoConfirmacion == codigo).FirstOrDefault();
                    if (dom != null)
                    {
                        return _conv.modeloAEntidad(dom);
                    }
                    throw new ECompartida("No se encontro ningun paquete para tu usuario con el codigo dado");
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
