using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SAERespuesta
    {
        public SUsuario usuario { get; set; }
        public STrayecto trayecto { get; set; }
        public SPaquete paquete { get; set; }
    }
}
