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

        public string pruebaEmail()
        {
            Configuration.Default.AddApiKey("api-key", "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");
            var apiInstance = new SMTPApi();
            var templateId = 1;  // long? | Id of the template
            var sendEmail = new SendEmail(emailTo:  new List<string>() { "tuxlukz@gmail.com" }); // SendEmail |
            try
            {
                // Get your account informations, plans and credits details
                apiInstance.SendTemplate(templateId, sendEmail);
                return "pepe";
            }
            catch (Exception)
            {
                throw;
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

    }
}
