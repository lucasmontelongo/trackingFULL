using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALAdmin
    {
        Conversiones _conv;

        public DALAdmin()
        {
            _conv = new Conversiones();
        }

        public bool add(string email, string role)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Usuario u = en.Usuario.FirstOrDefault(x => x.email == email);
                    if (u != null)
                    {
                        u.rol = role;
                        en.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<EstadisticaDTO> trayectoPaquete()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    var estadisticas = en.Database
                       .SqlQuery<EstadisticaDTO>("Select Trayecto.nombre as x, COUNT(Paquete.id) as y from Trayecto left join Paquete on Paquete.fechaIngreso = Paquete.fechaEntrega and Paquete.IdTrayecto = Trayecto.id group by Trayecto.nombre")
                       .ToList();
                    return estadisticas; 
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<EstadisticaDTO> paquetesIngresadosEntregados(string opcion)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    string q = "";
                    if (opcion == "ingresado")
                    {
                        q = "Select CONVERT(varchar, CAST(Paquete.fechaIngreso as date), 1) as x, COUNT(Paquete.id) as y from Paquete where Paquete.fechaIngreso >= DATEADD(DAY, -7, GETDATE()) group by CAST(Paquete.fechaIngreso as date)";
                    }
                    else
                    {
                        q = "Select CONVERT(varchar, CAST(Paquete.fechaEntrega as date), 1) as x, COUNT(Paquete.id) as y from Paquete where Paquete.fechaEntrega >= DATEADD(DAY, -7, GETDATE()) group by CAST(Paquete.fechaEntrega as date)";
                    }
                    var estadisticas = en.Database
                       .SqlQuery<EstadisticaDTO>(q)
                       .ToList();
                    return estadisticas;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

    }
}
