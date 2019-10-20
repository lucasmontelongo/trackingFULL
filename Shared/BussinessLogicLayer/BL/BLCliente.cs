using DataAccessLayer.DAL;
using Shared.Entities;
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
                return _dal.addCliente(a);
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
    }
}
