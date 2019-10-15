using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLPaquete
    {
        private DALPaquete _dal;

        public BLPaquete()
        {
            _dal = new DALPaquete();
        }

        public SPaquete getPaquete(int id)
        {
            try
            {
                return _dal.getPaquete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> getAll()
        {
            return _dal.getAll();
        }

        public SPaquete addPaquete(SPaquete a)
        {
            try
            {
                return _dal.addPaquete(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPaquete updatePaquete(SPaquete a)
        {
            try
            {
                return _dal.updatePaquete(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deletePaquete(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
