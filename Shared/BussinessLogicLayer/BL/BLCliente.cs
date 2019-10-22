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
                _dal.existe(c);
                if(c.TipoDocumento != "CI" && c.TipoDocumento != "Pasaporte")
                {
                    throw new ECliente("El tipo de documento indicado no es valido");
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
