using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class EtiquetaDTO
    {
        public string Destinatario { get; set; }
        public string Remitente { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Qr { get; set; }
    }
}