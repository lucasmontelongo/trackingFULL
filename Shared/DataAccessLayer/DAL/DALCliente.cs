using Shared.Entities;
using Shared.Exceptions;
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

        public bool existe(SCliente c)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Cliente a = en.Cliente.FirstOrDefault(x => x.email == c.Email);
                    if (a != null)
                    {
                        throw new ECliente("Ya existe un cliente con ese email en el sistema");
                    }
                    if(en.Cliente.FirstOrDefault(x => x.numeroDocumento == c.NumeroDocumento) != null)
                    {
                        throw new ECliente("Ya existe un cliente con ese numero de documento en el sistema");
                    }
                    return false;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public SCliente getClienteByEmail(string email)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Cliente a = en.Cliente.FirstOrDefault(x => x.email == email);
                    if (a == null)
                    {
                        throw new ECliente("No existe ningun cliente con ese email en el sistema");
                    }
                    return _conv.modeloAEntidad(a);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

    }
}
