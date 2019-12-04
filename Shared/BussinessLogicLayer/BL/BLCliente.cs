using DataAccessLayer.DAL;
using Shared.Entities;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLCliente
    {
        private DALCliente _dal;

        public BLCliente()
        {
            _dal = new DALCliente();
        }

        public SCliente getCliente(int id)
        {
            try
            {
                return _dal.getCliente(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SCliente> getAll()
        {
            return _dal.getAll();
        }

        public SCliente addCliente(SCliente a)
        {
            try
            {
                if (validacion(a))
                {
                    return _dal.addCliente(a);
                }
                throw new ECliente("Algun error raro del carajo");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SCliente updateCliente(SCliente a)
        {
            try
            {
                return _dal.updateCliente(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deleteCliente(int id)
        {
            try
            {
                return _dal.deleteCliente(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool validacion(SCliente c)
        {
            try
            {
                if (!_dal.existe(c))
                {
                    if (c.NumeroDocumento != null)
                    {
                        if (c.TipoDocumento != "CI" && c.TipoDocumento != "Pasaporte")
                        {
                            throw new ECliente("El tipo de documento indicado no es valido");
                        }
                    }
                    return true;
                }
                throw new ECliente("El email o el numero de documento ya esta registrado en el sistema");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SCliente getClienteByEmail(string email)
        {
            try
            {
                return _dal.getClienteByEmail(email);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<SCliente> nuevoPaquete(List<SCliente> clientes)
        {
            List<SCliente> clientesRespuesta = new List<SCliente>();
            try
            {
                clientes.ForEach(x =>
                {
                    x.Borrado = false;
                    x.Id = 0;
                    if (!_dal.existe(x))
                    {
                        clientesRespuesta.Add(_dal.addCliente(x));
                    }
                    else
                    {
                        SCliente c = _dal.getClienteByEmail(x.Email);
                        c.NombreCompleto = x.NombreCompleto;
                        c.Telefono = x.Telefono;
                        c.Borrado = x.Borrado;
                        if (x.TipoDocumento != null)
                        {
                            if (x.TipoDocumento == "CI" || x.NumeroDocumento == "Pasaporte")
                            {
                                c.NumeroDocumento = x.NumeroDocumento;
                                c.TipoDocumento = x.TipoDocumento;
                            }
                            else
                            {
                                throw new ECliente("El tipo de documento indicado no es valido");
                            }
                        }
                        clientesRespuesta.Add(_dal.updateCliente(c));
                    }
                });
                return clientesRespuesta;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
