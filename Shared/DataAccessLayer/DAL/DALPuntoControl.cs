using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALPuntoControl
    {
        Conversiones _conv;

        public DALPuntoControl()
        {
            _conv = new Conversiones();
        }

        public SPuntoControl getPuntoControl(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.PuntoControl.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<SPuntoControl> getAll()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SPuntoControl> puntoControl = new List<SPuntoControl>();
                    en.PuntoControl.ToList().ForEach(x =>
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

        public SPuntoControl addPuntoControl(SPuntoControl a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    PuntoControl ag = en.PuntoControl.Add(_conv.entidadAModelo(a));
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SPuntoControl updatePuntoControl(SPuntoControl a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    PuntoControl ag = en.PuntoControl.Find(a.Id);
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

        public string deletePuntoControl(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    PuntoControl a = en.PuntoControl.Find(id);
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

    }
}
