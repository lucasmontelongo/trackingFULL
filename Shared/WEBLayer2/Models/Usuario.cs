using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WEBLayer2.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(250, ErrorMessage = "No se puede ingresar más de 250 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        public string Password { get; set; }

        public string Rol { get; set; }

        public bool Borrado { get; set; }

        public string Token { get; set; }
    }
}