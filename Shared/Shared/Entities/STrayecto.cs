using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class STrayecto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int Version { get; set; }
        public bool Borrado { get; set; }

        //Coleccion de puntos de control que conforman el trayecto
        public List<SPuntoControl> ListaPuntosControl { get; set; }

        //Coleccion de paquetes que tiene el trayecto
        public List<SPaquete> ListaPaquetes { get; set; }

        public bool validation()
        {
            try
            {
                if (this.Nombre == null || this.Nombre == "" || this.Borrado || this.ListaPuntosControl == null || this.ListaPuntosControl.Count < 2)
                {
                    throw new ETrayecto("Algun parametro invalido"); //esto deberiamos hacer una validacion para cada campo y eso para devolver el mensaje correcto de lo que falla, pero bueno por ahora queda asi
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
