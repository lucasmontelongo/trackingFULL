using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class PuntoControl
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Tiempo { get; set; }
        [Display(Name = "Agencia")]
        public int? IdAgencia { get; set; }
        public int IdTrayecto { get; set; }
        public bool Borrado { get; set; }
        public string Nombre { get; set; }
    }
}