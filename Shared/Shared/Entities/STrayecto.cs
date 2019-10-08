using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class STrayecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Version { get; set; }
        public bool Borrado { get; set; }

        //Coleccion de puntos de control que conforman el trayecto
        public List<SPuntoControl> ListaPuntosControl { get; set; }

        //Coleccion de paquetes que tiene el trayecto
        public List<SPaquete> ListaPaquetes { get; set; }
    }
}
