using Shared.Entities;
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

        public bool addPaquete(SPaquete a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {

                    Paquete ag = en.Paquete.Add(_conv.entidadAModelo(a));
                    en.SaveChanges();
                    return true;
                }
                catch (Exception)
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
    }
}
