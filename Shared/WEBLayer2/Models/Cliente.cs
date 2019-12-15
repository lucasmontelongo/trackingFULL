using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WEBLayer2.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(50, ErrorMessage = "No se puede ingresar más de 50 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        [Display(Name = "Nombre")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        [Display(Name = "Documento")]
        public string NumeroDocumento { get; set; }


        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(50, ErrorMessage = "No se puede ingresar más de 50 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        [Display(Name = "Tipo de Documento")]
        public string TipoDocumento { get; set; }

        public bool Borrado { get; set; } = false;
    }
}