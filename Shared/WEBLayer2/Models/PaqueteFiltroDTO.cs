using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class PaqueteFiltroDTO
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaFinal { get; set; }

        [MaxLength(50)]
        public string Remitente { get; set; }
        
        public string Destinatario { get; set; }
        
        public string Estado { get; set; }
    }
}