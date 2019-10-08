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
        SAgencia modeloAEntidad(Agencia a)
        {
            SAgencia agencia = new SAgencia()
            {
                Id = a.id,
                IdEmpresa = (int)a.idEmpresa,
                Nombre = a.nombre,
                Ubicacion = a.ubicacion,
                Borrado = (bool)a.borrado,
                EnvioDomicilio = (bool)a.envioDomicilio,
                ListaPuntoControl = null
            };
            return agencia;
        }

        Agencia entidadAModelo(SAgencia a)
        {
            Agencia agencia = new Agencia()
            {
                id = a.Id,
                idEmpresa = a.IdEmpresa,
                
            };
            return null;
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
                        agencias.Add(modeloAEntidad(x));
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
