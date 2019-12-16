using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SAEPaquete : SPaquete
    {
        public SCliente Remitente { get; set; }
        public SCliente Destinatario { get; set; }
        public bool? adelanta { get; set; }
        public bool? atrasa { get; set; }
        public bool? entrega { get; set; }
        public string code { get; set; }


        public string validacion()
        {
            string ret = "";
            if (Remitente == null && IdRemitente == null)
            {
                ret = "Error en paquete, falta el remitente";
            }
            else if (Destinatario == null && IdDestinatario == null)
            {
                ret = "Error en paquete, falta el destinatario";
            }
            return ret;
        }
    }
}
