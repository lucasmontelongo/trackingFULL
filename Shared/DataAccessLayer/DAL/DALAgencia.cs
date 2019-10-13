using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALAgencia
    {
        Conversiones _conv;

        public DALAgencia()
        {
            _conv = new Conversiones();
        }

        public List<SAgencia> getAll()
        {
            using(trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SAgencia> agencias = new List<SAgencia>();
                    en.Agencia.ToList().ForEach(x =>
                    {
                        agencias.Add(_conv.modeloAEntidad(x));
                    });
                    return agencias;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
