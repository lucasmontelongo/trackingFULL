﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SPaquetePuntoControl
    {
        public int Id { get; set; }
        public DateTime FechaLlegada { get; set; }
        public int Retraso { get; set; }
        public int IdPaquete { get; set; }
        public int IdPuntoControl { get; set; }
        public int IdEmpleado { get; set; }
        public bool Borrado { get; set; }
    }
}
