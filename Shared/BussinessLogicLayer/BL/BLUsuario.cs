using DataAccessLayer.DAL;
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

        public SUsuario getUsuario(int id)
        {
            try
            {
                return _dal.getUsuario(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SUsuario> getAll()
        {
            return _dal.getAll();
        }

        public bool addUsuario(SUsuario u)
        {
            try
            {
                return _dal.addUsuario(u);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SUsuario updateUsuario(SUsuario a)
        {
            try
            {
                return _dal.updateUsuario(a);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string deleteUsuario(int id)
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

        public string login(SUsuario u)
        {
            return _dal.login(u);
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

        public bool confirmarEmail(string email, string codigoConfirmacion)
        {
            try
            {
                return _dal.confirmarEmail(email, codigoConfirmacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
