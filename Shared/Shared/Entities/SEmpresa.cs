using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SEmpresa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Borrado { get; set; }

        //Coleccion de agencias que tiene la empresa
        public List<SAgencia> ListaAgencias { get; set; }
    }
}
