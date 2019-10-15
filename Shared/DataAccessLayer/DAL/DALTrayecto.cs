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
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
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

    }
}
