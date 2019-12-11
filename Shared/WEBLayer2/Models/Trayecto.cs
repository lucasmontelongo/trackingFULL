using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WEBLayer2.Models
{
    public class Trayecto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [MaxLength(50, ErrorMessage = "No se puede ingresar más de 50 caracteres")]
        [MinLength(5, ErrorMessage = "Debe ingresar al meno 5 caracteres")]
        public string Nombre { get; set; }
        public int Version { get; set; }
        public bool Borrado { get; set; }
        
        public List<PuntoControl> ListaPuntosControl { get; set; }
    }
}