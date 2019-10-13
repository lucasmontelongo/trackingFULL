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

        public List<SAgencia> getAll()
        {
            return _dal.getAll();
        }

        public void addAgencia()
        {
            throw new NotImplementedException();
        }
    }
}
