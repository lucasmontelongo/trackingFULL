using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class Paquete
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoConfirmacion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int IdTrayecto { get; set; }
        public int IdRemitente { get; set; }
        public int IdDestinatario { get; set; }
        public bool Borrado { get; set; }
    }
}