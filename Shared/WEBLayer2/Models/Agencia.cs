using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class Agencia
    {
        public int ? Id { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        [Display(Name = "Ubicación")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [Display(Name = "Envio a domicilio")]
        public bool EnvioDomicilio { get; set; } = false;

        [Required(ErrorMessage = "El campo es necesario")]
        [Display(Name = "Id empresa")]
        public int IdEmpresa { get; set; }

        public bool Borrado { get; set; } = false;
    }
}