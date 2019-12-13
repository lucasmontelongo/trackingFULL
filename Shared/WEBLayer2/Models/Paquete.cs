using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBLayer2.Models
{
    public class Paquete
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoConfirmacion { get; set; }
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entrega")]
        public DateTime FechaEntrega { get; set; }
        [Display(Name = "Id trayecto")]
        public int IdTrayecto { get; set; }
        [Display(Name = "Id remitente")]
        public int IdRemitente { get; set; }
        [Display(Name = "Id destinatario")]
        public int IdDestinatario { get; set; }
        public bool Borrado { get; set; }
    }
}