using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SEATrayecto : STrayecto
    {
        new public  List<SEAPuntoControlAgencia>  ListaPuntosControl { get; set; }
        public string validasionCrearAgencias()
        {
            int faltaDatosAgencia = 0;
            int errorDatosTrayecto = 0;
            ListaPuntosControl.ForEach(x =>
            {
                if (x.Id == null && x.Agencia == null) faltaDatosAgencia++;
                if ((x.IdTrayecto != null && Id != null && x.IdTrayecto != Id) || (x.IdTrayecto != null && Id == null)) errorDatosTrayecto++;
            });
            List<string> ret = new List<string>();


            if (faltaDatosAgencia == 0)
            {
                ret.Add("Fallo el analizis en los datos de " + faltaDatosAgencia + " agencias");
            }
            if (errorDatosTrayecto == 0)
            {
                ret.Add("Error, se recibieron " + faltaDatosAgencia + " ids de trayecto que no coinciden con el trayecto actual");
            }


            return string.Join<string>(", ", ret);
        }
    }
}
