using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLAgencia
    {
        private DALAgencia _dal;

        public BLAgencia()
        {
            _dal = new DALAgencia();
        }

        public SAgencia getAgencia(int id)
        {
            try
            {
                return _dal.getAgencia(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SAgencia> getAll()
        {
            return _dal.getAll();
        }

        public SAgencia addAgencia(SAgencia a)
        {
            try
            {
                return _dal.addAgencia(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SAgencia updateAgencia(SAgencia a)
        {
            try
            {
                return _dal.updateAgencia(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deleteAgencia(int id)
        {
            try
            {
                return _dal.deleteAgencia(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
