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
            List<string> ret = new List<string>();
            if (ListaPuntosControlAgencia == null)
            {
                return "Error no se recibieron puntos de control";
            }
            this.ListaPuntosControlAgencia.ForEach(x =>
            {
                if (x.IdAgencia == null && x.Agencia == null) faltaDatosAgencia++;
                if ((x.IdTrayecto != null && Id != null && x.IdTrayecto != Id) || (x.IdTrayecto != null && Id == null)) errorDatosTrayecto++;
            });


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


            List<SPuntoControl> actu = t.ListaPuntosControl.OrderBy(o => o.Orden).ToList();
            List<SEAPuntoControlAgencia> news = ListaPuntosControlAgencia.OrderBy(o => o.Orden).ToList();
            if (actu.Count != news.Count) return true;

            for (int i = 0; i < actu.Count; i++)
            {
                if (actu[i].Id != news[i].Id) return true;
                if (actu[i].Orden != news[i].Orden) return true;
                if (actu[i].IdAgencia != news[i].IdAgencia) return true;
                if (actu[i].Tiempo != news[i].Tiempo) return true;
                if (actu[i].Borrado != news[i].Borrado) return true;
                if (actu[i].Nombre != news[i].Nombre) return true;
            }
            return false;

        }
  
    }
}
