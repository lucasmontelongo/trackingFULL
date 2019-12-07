using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer.Models
{
    public class PaquetePuntoControl
    {
        public int Id { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int Retraso { get; set; }
        public int IdPaquete { get; set; }
        public int IdPuntoControl { get; set; }
        public int IdEmpleado { get; set; }
        public bool Borrado { get; set; }
    }
}