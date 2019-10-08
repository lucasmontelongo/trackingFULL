using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SCliente
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public bool Borrado { get; set; }

        //Coleccion de paquetes que ha enviado
        public List<SPaquete> ListaPaquetesEnviados { get; set; }

        //Coleccion de paquetes que ha recibido
        public List<SPaquete> ListaPaquetesRecibidos { get; set; }
    }
}
