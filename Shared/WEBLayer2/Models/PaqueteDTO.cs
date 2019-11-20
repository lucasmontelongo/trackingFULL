using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class PaqueteDTO
    {
        public int IdTrayecto { get; set; }
        public string RemitenteEmail { get; set; }
        public string RemitenteNombreCompleto { get; set; }
        public string RemitenteTelefono { get; set; }
        public string RemitenteNumeroDocumento { get; set; }
        public string RemitenteTipoDocumento { get; set; }
        public string DestinatarioEmail { get; set; }
        public string DestinatarioNombreCompleto { get; set; }
        public string DestinatarioTelefono { get; set; }
    }
}