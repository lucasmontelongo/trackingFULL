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
            UriTemplate = "/Greeting",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        string Greeting(SEATrayecto trayecto);

        // TODO: agregue aquí sus operaciones de servicio
    }
}
