using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SAEData
    {
        public SUsuario usuario { get; set; }
        public SEATrayecto trayecto { get; set; }
        public SAEPaquete paquete { get; set; }
    }
}
