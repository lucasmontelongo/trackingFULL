using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SUsuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }
        public bool Borrado { get; set; }

        //Esta coleccion seria para los usuarios con rol FUNCIONARIO
        public List<SPaquetePuntoControl> ListaPaquetePuntoControl { get; set; }
    }
}
