using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.BL
{
    public class BLAdmin
    {
        private DALAdmin _dal;

        public BLAdmin()
        {
            _dal = new DALAdmin();
        }

        public bool add(string email)
        {
            try
            {
                //BLUsuario blUsuario = new BLUsuario();
                //SUsuario u = blUsuario.getUsuarioByEmail(email);
                return _dal.add(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
