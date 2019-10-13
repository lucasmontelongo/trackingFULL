using DataAccessLayer.DAL;
using DataAccessLayer.Interfaces;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLUsuario
    {
        private IDALUsuario _dal;

        public BLUsuario()
        {
            _dal = new DALUsuario();
        }

        public string addUsuario(SUsuario u)
        {
            try
            {
                return _dal.addUsuario(u);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
