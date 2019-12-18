using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class PaqueteDTO
    {
        [Display(Name = "Id Trayecto")]

        public int IdTrayecto { get; set; }
        //public bool EnvioDomicilio { get; set; }
        public string Qr { get; set; }
        public Cliente Remitente { get; set; }
        public Cliente Destinatario { get; set; }
        public Trayecto Trayecto { get; set; }
        public List<PaquetePuntoControl> PaquetePuntoControl { get; set; }
    }
}