using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLPaquetePuntoControl
    {
        private DALPaquetePuntoControl _dal;

        public BLPaquetePuntoControl()
        {
            _dal = new DALPaquetePuntoControl();
        }

        public SPaquetePuntoControl getPaquetePuntoControl(int id)
        {
            try
            {
                return _dal.getPaquetePuntoControl(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquetePuntoControl> getAll()
        {
            return _dal.getAll();
        }

        public SPaquetePuntoControl addPaquetePuntoControl(SPaquetePuntoControl a)
        {
            try
            {
                return _dal.addPaquetePuntoControl(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPaquetePuntoControl updatePaquetePuntoControl(SPaquetePuntoControl a)
        {
            try
            {
                return _dal.updatePaquetePuntoControl(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deletePaquetePuntoControl(int id)
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

        public List<SPaquetePuntoControl> puntosControlDeUnPaquete(int id)
        {
            try
            {
                return _dal.getAllByPaquete(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
