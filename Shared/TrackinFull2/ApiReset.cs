using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

using RestSharp;

namespace TrackinFull2
{
    public class ApiReset
    {
        public ApiReset() { }

        private async Task<T> get<T>(String URL)
        {
            HttpClient _http = new HttpClient();
            var res = await _http.GetAsync(URL);
  
            var sJSON = await res.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(sJSON);

        }
        public static async void login(string Email, string Password, MainPage main)
        {
            try
            {
                var client = new RestClient("https://apirestlayer20191105065734.azurewebsites.net/api/auth/login");
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", "{\"Email\":\"" + Email + "\",\"Password\":\"" + Password + "\"}", ParameterType.RequestBody);
                client.ExecuteAsync(request, (res) =>
                {
                    
                    if (res.StatusCode.ToString() == "OK")
                    {

                        var usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(res.Content);

                        if (usuario != null)
                        {
//                            main.initUser(usuario);
                        }
                        else
                        {
  //                          main.msg("Error", "Algo a fallado", "OK");
                        }
                    }
                    else
                    {
    //                    main.msg("Error", (string)Newtonsoft.Json.JsonConvert.SerializeObject(res), "OK");
                    }
                });

            }
            catch (Exception)
            {
      //          main.msg("Error", "algo salio re mal", "OK");
            }
        }

        public static Boolean changeStatePack(string uri , string IdPaquete, string IdEmpleado, string sTocken, string scode)
        {
            try
            {
                var client = new RestClient("http://trackingfullapi2019.azurewebsites.net/api" + uri);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", sTocken);
                string param = "\"IdPaquete\":\"" + IdPaquete + "\",\"IdEmpleado\":\"" + IdEmpleado + "\"";
                if (scode != null)
                {
                    param += ",\"codigo\":\"" + scode + "\"";
                }
                request.AddParameter("application/json", "{" + param + "}", ParameterType.RequestBody);

                IRestResponse res = client.Execute(request);
                return (res.StatusCode.ToString() == "OK");
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
