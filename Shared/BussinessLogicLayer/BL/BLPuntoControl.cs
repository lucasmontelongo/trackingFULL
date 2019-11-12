using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLPuntoControl
    {
        private DALPuntoControl _dal;

        public BLPuntoControl()
        {
            _dal = new DALPuntoControl();
        }

        public SPuntoControl getPuntoControl(int id)
        {
            try
            {
                return _dal.getPuntoControl(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPuntoControl> getAll()
        {
            return _dal.getAll();
        }

        public SPuntoControl addPuntoControl(SPuntoControl a)
        {
            try
            {
                return _dal.addPuntoControl(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SPuntoControl updatePuntoControl(SPuntoControl a)
        {
            try
            {
                return _dal.updatePuntoControl(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deletePuntoControl(int id)
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

        public List<SPuntoControl> puntosControlDeUnTrayecto(int id)
        {
            try
            {
                return _dal.puntosControlDeUnTrayecto(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
