using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Shared.Exceptions;

namespace BussinessLogicLayer.BL
{
    static public class BLEmail
    {
        static private RestRequest setRequest(string metodo)
        {
            try
            {
                var request = new RestRequest();

                if (metodo == "post")
                {
                    request = new RestRequest(Method.POST);
                }
                else if(metodo == "get")
                {
                    request = new RestRequest(Method.GET);
                }
                else if(metodo == "put")
                {
                    request = new RestRequest(Method.PUT);
                }
                else if(metodo == "delete")
                {
                    request = new RestRequest(Method.DELETE);
                }
                else
                {
                    throw new ECompartida("El metodo indicado no es valido");
                }
                
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                request.AddHeader("api-key", "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");

                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public string confirmacionDeEmail(string email, string codigoConfirmacion)
        {
            try
            {
                var client = new RestClient("https://api.sendinblue.com/v3/smtp/email");
                var request = setRequest("post");
                request.AddParameter("application/json", "{\"sender\":{\"email\":\"tuxlukz@gmail.com\"},\"to\":[{\"email\":\"" + email + "\"}],\"replyTo\":{\"email\":\"tuxlukz@gmail.com\"},\"templateId\":1,\"params\":{\"LINKCONFIRMACION\":\"http://localhost:52952/api/auth/confirmaremail?email=" + email + "&codigoConfirmacion=" + codigoConfirmacion + "\"}}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
