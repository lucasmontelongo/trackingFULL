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

        public static response changeStatePack(string uri , string IdPaquete, string IdEmpleado, string sTocken, string scode)
        {
            try
            {
                RestClient client = null;
                RestRequest request = null;
                if (scode != "")
                {
                    request = new RestRequest(Method.GET);
                    client = new RestClient("http://trackingfullapi2019.azurewebsites.net/" + uri + "?codigo=" + scode + "&IdPaquete=" + IdPaquete + "");
                } else
                {
                    request = new RestRequest(Method.POST);
                    client = new RestClient("http://trackingfullapi2019.azurewebsites.net/" + uri);
                    request.AddParameter("application/json", "{\"Id\":\"" + IdPaquete + "\"}", ParameterType.RequestBody);
                }

                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + sTocken);
                IRestResponse res = client.Execute(request);
                return new response()
                {
                    body = res.Content,
                    status = res.StatusCode.ToString() == "OK"
                };
            }
            catch (Exception e)
            {
                return new response()
                {
                    body = e.Message,
                    status = true
                }; ;
            }
        }

    }
}
