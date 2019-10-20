using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SPuntoControl
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Tiempo { get; set; }
        public int ?IdAgencia { get; set; }
        public int IdTrayecto { get; set; }
        public bool Borrado { get; set; }
        public string Nombre { get; set; }

        //Coleccion de paquetes que han pasado por este punto de control
        public List<SPaquetePuntoControl> ListaPaquetePuntoControl { get; set; }
    }
}
