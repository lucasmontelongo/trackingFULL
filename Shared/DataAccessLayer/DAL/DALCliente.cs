using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALCliente
    {
        Conversiones _conv;

        public DALCliente()
        {
            _conv = new Conversiones();
        }

        public SCliente getCliente(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.Cliente.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<SCliente> getAll()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SCliente> cliente = new List<SCliente>();
                    en.Cliente.ToList().ForEach(x =>
                    {
                        if (x.borrado == false)
                        {
                            cliente.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return cliente;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SCliente addCliente(SCliente a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Cliente ag = en.Cliente.Add(_conv.entidadAModelo(a));
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SCliente updateCliente(SCliente a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Cliente ag = en.Cliente.Find(a.Id);
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

        public string deleteCliente(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Cliente a = en.Cliente.Find(id);
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
