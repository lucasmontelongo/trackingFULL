using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Shared.Utilidades;
using Shared.Exceptions;
using Newtonsoft.Json;
using RestSharp;

using Shared.Entities;

namespace ExternalServiceLayer
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string Greeting(SAEData trayecto)
        {

            var client = new RestClient(Direcciones.ApiRest + "completo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(trayecto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}
