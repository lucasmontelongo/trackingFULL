using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALPaquetePuntoControl
    {
        Conversiones _conv;

        public DALPaquetePuntoControl()
        {
            _conv = new Conversiones();
        }

        public SPaquetePuntoControl getPaquetePuntoControl(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.PaquetePuntoControl.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<SPaquetePuntoControl> getAll()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SPaquetePuntoControl> paquetePuntoControl = new List<SPaquetePuntoControl>();
                    en.PaquetePuntoControl.ToList().ForEach(x =>
                    {
                        if (x.borrado == false)
                        {
                            paquetePuntoControl.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return paquetePuntoControl;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SPaquetePuntoControl addPaquetePuntoControl(SPaquetePuntoControl a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    PaquetePuntoControl ag = en.PaquetePuntoControl.Add(_conv.entidadAModelo(a));
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SPaquetePuntoControl updatePaquetePuntoControl(SPaquetePuntoControl a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    PaquetePuntoControl ag = en.PaquetePuntoControl.Find(a.Id);
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

        public string deletePaquetePuntoControl(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    PaquetePuntoControl a = en.PaquetePuntoControl.Find(id);
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
