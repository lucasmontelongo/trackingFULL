using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALAgencia
    {
        Conversiones _conv;

        public DALAgencia()
        {
            _conv = new Conversiones();
        }

        public SAgencia getAgencia(int id)
        {
            using(trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.Agencia.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<SAgencia> getAll()
        {
            using(trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SAgencia> agencias = new List<SAgencia>();
                    en.Agencia.ToList().ForEach(x =>
                    {
                        if (x.borrado == false)
                        {
                            agencias.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return agencias;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SAgencia addAgencia(SAgencia a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Agencia ag = en.Agencia.Add(_conv.entidadAModelo(a));
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SAgencia updateAgencia(SAgencia a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Agencia ag = en.Agencia.Find(a.Id);
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

        public string deleteAgencia(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Agencia a = en.Agencia.Find(id);
                    a.borrado = true;
                    en.SaveChanges(); // Que pasa con las demas entidades que tienen a esta agencia como FK????

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
