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

        [MaxLength(5, ErrorMessage = "No se puede ingresar más de 5 caracteres")]
        public string CodigoConfirmacion { get; set; }


        [Required(ErrorMessage = "El campo es necesario")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de ingreso")]
        public DateTime FechaIngreso { get; set; }


        [Required(ErrorMessage = "El campo es necesario")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de entrega")]
        public DateTime FechaEntrega { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [Display(Name = "Id trayecto")]
        public int IdTrayecto { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [Display(Name = "Id remitente")]
        public int IdRemitente { get; set; }

        [Required(ErrorMessage = "El campo es necesario")]
        [Display(Name = "Id destinatario")]
        public int IdDestinatario { get; set; }

        public bool Borrado { get; set; }
    }
}