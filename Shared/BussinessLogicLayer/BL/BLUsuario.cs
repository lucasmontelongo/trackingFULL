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
        private DALUsuario _dal;

        public BLUsuario()
        {
            _dal = new DALUsuario();
        }

        public string login(SUsuario u)
        {
            return _dal.login(u);
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

        public string getRol(string email)
        {
            try
            {
                return _dal.getRol(email);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
    }
}
