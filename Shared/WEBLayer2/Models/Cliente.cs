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
        public string Email { get; set; }
        [Display(Name = "Nombre")]
        public string NombreCompleto { get; set; }
        [Display(Name = "Teléfono")]

        public string Telefono { get; set; }
        [Display(Name = "Documento")]
        public string NumeroDocumento { get; set; }
        [Display(Name = "Tipo de Documento")]
        public string TipoDocumento { get; set; }
        public bool Borrado { get; set; }
    }
}