using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class PaqueteFiltroDTO
    {
        [Display (Name = "Fecha de Inicio")]
        public DateTime ? FechaInicio { get; set; }
        [Display(Name = "Fecha Final")]
        public DateTime ? FechaFinal { get; set; }
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Estado { get; set; }
    }
}
