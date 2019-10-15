using Shared.Entities;
using Shared.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class DALUsuario
    {
        Conversiones _conv;

        public DALUsuario()
        {
            _conv = new Conversiones();
        }

        public SUsuario getUsuario(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return _conv.modeloAEntidad(en.Usuario.Find(id));
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public List<SUsuario> getAll()
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    List<SUsuario> usuario = new List<SUsuario>();
                    en.Usuario.ToList().ForEach(x =>
                    {
                        if (x.borrado == false)
                        {
                            usuario.Add(_conv.modeloAEntidad(x));
                        }
                    });
                    return usuario;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public bool addUsuario(SUsuario u)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Usuario usuario = _conv.entidadAModelo(u);
                    usuario.codigoConfirmacion = Randoms.RandomString(100);
                    usuario.emailValido = false;
                    en.Usuario.Add(usuario);
                    en.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public SUsuario updateUsuario(SUsuario a)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Usuario ag = en.Usuario.Find(a.Id);
                    ag = _conv.entidadAModelo(a, ag);
                    en.SaveChanges();
                    return _conv.modeloAEntidad(ag);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public string deleteUsuario(int id)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Usuario a = en.Usuario.Find(id);
                    a.borrado = true;
                    en.SaveChanges();

                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
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

        public string getRol(string email)
        {
            using(trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    return en.Usuario.First(x => x.email == email).rol;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }

        public bool confirmarEmail(string email, string codigoConfirmacion)
        {
            using (trackingFULLEntities en = new trackingFULLEntities())
            {
                try
                {
                    Usuario u = en.Usuario.First(x => x.email == email);
                    if(u.codigoConfirmacion == codigoConfirmacion)
                    {
                        u.emailValido = true;
                        en.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}
