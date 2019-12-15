using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Shared.Entities;

namespace ExternalServiceLayer
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        /*
                     Method = "GET",
            UriTemplate = "/Greeting/{value}",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]*/
        string Greeting(SAEData trayecto);

        // TODO: agregue aquí sus operaciones de servicio
    }
}
