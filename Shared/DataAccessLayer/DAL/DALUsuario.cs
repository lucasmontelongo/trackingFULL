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

        public string login(SUsuario u)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    SUsuario usuario = _conv.modeloAEntidad(en.Usuario.First(x => x.email == u.Email));
                    if (usuario != null)
                    {
                        if (usuario.Password == u.Password) // Deberiamos hashear la pass y aca llamariamos a una funcion que compruebe si esta bien o no, por ahora queda asi
                        {
                            return "OK";
                        }
                        else
                        {
                            return "La contrase;a no coincide";
                        }
                    }
                    return "El usuario no existe";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
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
