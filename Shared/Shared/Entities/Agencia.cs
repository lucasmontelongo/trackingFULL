using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class Agencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public bool EnvioDomicilio { get; set; }
        public int IdEmpresa { get; set; }
        public bool Borrado { get; set; }
    }
}
