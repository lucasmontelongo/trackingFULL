using DataAccessLayer.Interfaces;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALUsuario : IDALUsuario
    {
        Conversiones _conv;

        public DALUsuario()
        {
            _conv = new Conversiones();
        }

        public string addUsuario(SUsuario u)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    en.Usuario.Add(_conv.entidadAModelo(u));
                    en.SaveChanges();
                    return "OK";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
    }
}
