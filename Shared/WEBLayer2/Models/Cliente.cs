using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public bool Borrado { get; set; }
    }
}