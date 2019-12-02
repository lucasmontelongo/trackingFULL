using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class PaqueteDTO
    {
        public int IdTrayecto { get; set; }
        public bool EnvioDomicilio { get; set; }
        public string Qr { get; set; }
        public Cliente Remitente { get; set; }
        public Cliente Destinatario { get; set; }
        public Trayecto Trayecto { get; set; }
        public List<PaquetePuntoControl> PaquetePuntoControl { get; set; }
    }
}