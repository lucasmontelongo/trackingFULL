using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALTrayecto
    {
        Conversiones _conv;

        public DALTrayecto()
        {
            _conv = new Conversiones();
        }

        public STrayecto getTrayecto(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.Trayecto.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<STrayecto> getAll()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<STrayecto> trayecto = new List<STrayecto>();
                    en.Trayecto.ToList().ForEach(x =>
                    {
                        if (x.borrado == false)
                        {
                            trayecto.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return trayecto;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public STrayecto addTrayecto(STrayecto a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Trayecto ag = en.Trayecto.Add(_conv.entidadAModelo(a));
                    if (a.ListaPuntosControl != null)
                    {
                        a.ListaPuntosControl.ForEach(x =>
                        {
                            ag.PuntoControl.Add(_conv.entidadAModelo(x));
                        });
                    }
                    else
                    {
                        ag.PuntoControl.Add(new PuntoControl() { nombre = "Recibido en origen", orden = 1, tiempo = 0, borrado = false  });
                        ag.PuntoControl.Add(new PuntoControl() { nombre = "Esperando en origen", orden = 2, tiempo = 0, borrado = false });
                        ag.PuntoControl.Add(new PuntoControl() { nombre = "En viaje", orden = 3, tiempo = 0, borrado = false });
                        ag.PuntoControl.Add(new PuntoControl() { nombre = "Recibido en destino", orden = 4, tiempo = 0, borrado = false });
                        ag.PuntoControl.Add(new PuntoControl() { nombre = "Entregado al cliente", orden = 5, tiempo = 0, borrado = false });

                    }
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        public STrayecto updateTrayecto(STrayecto a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Trayecto ag = en.Trayecto.Find(a.Id);
                    ag = _conv.entidadAModelo(a, ag);
                    DALPuntoControl dalp = new DALPuntoControl();
                    if (a.ListaPuntosControl != null)
                    {
                        a.ListaPuntosControl.ToList().ForEach(x =>
                        {
                            if (x.Id > 0)
                            {
                                dalp.updatePuntoControl(x);
                            }
                            else
                            {
                                dalp.addPuntoControl(x);
                            }
                        });
                    }
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public string deleteTrayecto(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Trayecto a = en.Trayecto.Find(id);
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
        
        public int paquetesTransito(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    var total = en.Database
                      .SqlQuery<int>("Select COUNT(Paquete.id) from Trayecto left join Paquete on Paquete.IdTrayecto = Trayecto.id")
                      .ToList().First();
                    return total;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public bool isActive(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Trayecto a = en.Trayecto.Find(id);
                    if ((bool)a.borrado)
                    {
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
}
