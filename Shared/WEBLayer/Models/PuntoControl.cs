﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBLayer.Models
{
    public class PuntoControl
    {
        public int Id { get; set; }
        public int Orden { get; set; }
        public int Tiempo { get; set; }
        public int? IdAgencia { get; set; }
        public int IdTrayecto { get; set; }
        public bool Borrado { get; set; }
        public string Nombre { get; set; }
    }
}