using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class Trayecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Version { get; set; }
        public bool Borrado { get; set; }
        
        public List<PuntoControl> ListaPuntosControl { get; set; }
    }
}