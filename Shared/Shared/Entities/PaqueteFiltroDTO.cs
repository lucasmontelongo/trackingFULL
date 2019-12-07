using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class PaqueteFiltroDTO
    {
        public DateTime ? FechaInicio { get; set; }
        public DateTime ? FechaFinal { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Estado { get; set; }
    }
}
