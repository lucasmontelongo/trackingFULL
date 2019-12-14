using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Entities
{
    public class SEATrayecto : STrayecto
    {
        public  List<SEAPuntoControlAgencia> ListaPuntosControlAgencia { get; set; }
        public string validasionCrearAgencias()
        {
            int faltaDatosAgencia = 0;
            int errorDatosTrayecto = 0;
            this.ListaPuntosControlAgencia.ForEach(x =>
            {
                if (x.IdAgencia == null && x.Agencia == null) faltaDatosAgencia++;
                if ((x.IdTrayecto != null && Id != null && x.IdTrayecto != Id) || (x.IdTrayecto != null && Id == null)) errorDatosTrayecto++;
            });
            List<string> ret = new List<string>();


            if (faltaDatosAgencia != 0)
            {
                ret.Add("Fallo el analizis en los datos de " + faltaDatosAgencia + " agencias");
            }
            if (errorDatosTrayecto != 0)
            {
                ret.Add("Error, se recibieron " + faltaDatosAgencia + " ids de trayecto que no coinciden con el trayecto actual");
            }


            return string.Join<string>(", ", ret);
        }
        public bool compara(STrayecto t)
        {

            if (t.Nombre != Nombre) return true;
            if (t.Version != Version) return true;
            if (t.Borrado != Borrado) return true;
            if (t.ListaPuntosControl.Count != this.ListaPuntosControl.Count) return true;
            for (int i = 0; i < t.ListaPuntosControl.Count; i++)
            {
                if (t.ListaPuntosControl[i].Id != ListaPuntosControl[i].Id) return true;
                if (t.ListaPuntosControl[i].Orden != ListaPuntosControl[i].Orden) return true;
                if (t.ListaPuntosControl[i].IdAgencia != ListaPuntosControl[i].IdAgencia) return true;
                if (t.ListaPuntosControl[i].Tiempo != ListaPuntosControl[i].Tiempo) return true;
                if (t.ListaPuntosControl[i].Borrado != ListaPuntosControl[i].Borrado) return true;
                if (t.ListaPuntosControl[i].Nombre != ListaPuntosControl[i].Nombre) return true;
            }
            return false;

        }
  
    }
}
