using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class EnvioDomicilioDTO
    {
        public int IdPaquete { get; set; }
        public bool Envio { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? Hora { get; set; }
    }
}