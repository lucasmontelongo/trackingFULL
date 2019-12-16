using DataAccessLayer.DAL;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System.Diagnostics;
using RestSharp;
using Shared.Exceptions;

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

        public SUsuario addUsuario(SUsuario u)
        {
            try
            {
                SUsuario usuario = _dal.addUsuario(u);
                //BLEmail.confirmacionDeEmail(usuario.Email, getCodigoConfirmacionEmail(usuario.Id));
                return usuario;
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

        public SUsuario login(SUsuario u)
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

        public string getCodigoConfirmacionEmail(int id)
        {
            try
            {
                return _dal.getCodigoConfirmacionEmail(id);
            }
            catch (Exception)
            {
                throw;
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

        public SUsuario newAdmin(int id)
        {
            try
            {
                return _dal.newAdmin(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SUsuario getUsuarioByEmail(string email)
        {
            try
            {
                return _dal.getUsuarioByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> paquetesEnviados(string email) // el email es del que realizo la peticion
        {
            try
            {
                BLCliente bl = new BLCliente();
                SCliente c = bl.getClienteByEmail(email);
                if (c.Email != null)
                {
                    BLPaquete blp = new BLPaquete();
                    return blp.paquetesEnviados(c.Id);
                }
                throw new ECompartida("No tienes permisos suficientes para realizar esta accion");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SPaquete> paquetesRecibidos(string email) // el email es del que realizo la peticion
        {
            try
            {
                BLCliente bl = new BLCliente();
                SCliente c = bl.getClienteByEmail(email);
                if (c.Email != null)
                {
                    BLPaquete blp = new BLPaquete();
                    return blp.paquetesRecibidos(c.Id);
                }
                throw new ECompartida("No tienes permisos suficientes para realizar esta accion");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public SUsuario externalLogin(SUsuario login)
        {
            try
            {
                SUsuario u = _dal.getUsuarioByEmail(login.Email);
                if(u == null)
                {
                    u = _dal.addUsuarioExternalLogin(login);
                }
                return u;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
