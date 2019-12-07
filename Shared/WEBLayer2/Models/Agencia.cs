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
        public string Nombre { get; set; }
        [Display(Name = "Ubicación")]

        public string Ubicacion { get; set; }
        [Display(Name = "Envio a domicilio")]

        public bool EnvioDomicilio { get; set; }
        [Display(Name = "Id empresa")]

        public int IdEmpresa { get; set; }
        public bool Borrado { get; set; }
    }
}