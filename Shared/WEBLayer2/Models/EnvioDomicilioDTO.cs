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
        public String Hora { get; set; } = DateTime.Now.ToString();

    }
}