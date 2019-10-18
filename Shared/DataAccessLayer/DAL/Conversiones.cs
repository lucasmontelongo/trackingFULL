using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DAL
{
    public class Conversiones
    {
        //AGENCIA

        public SAgencia modeloAEntidad(Agencia a)
        {
            SAgencia agencia = new SAgencia()
            {
                Id = a.id,
                IdEmpresa = (int)a.idEmpresa,
                Nombre = a.nombre,
                Ubicacion = a.ubicacion,
                Borrado = (bool)a.borrado,
                EnvioDomicilio = (bool)a.envioDomicilio
            };
            return agencia;
        }

        public Agencia entidadAModelo(SAgencia a)
        {
            Agencia agencia = new Agencia()
            {
                id = a.Id,
                idEmpresa = a.IdEmpresa,
                nombre = a.Nombre,
                ubicacion = a.Ubicacion,
                borrado = a.Borrado,
                envioDomicilio = a.EnvioDomicilio

            };
            return agencia;
        }

        public Agencia entidadAModelo(SAgencia a, Agencia ag)
        {
            ag.id = a.Id;
            ag.idEmpresa = a.IdEmpresa;
            ag.nombre = a.Nombre;
            ag.ubicacion = a.Ubicacion;
            ag.borrado = a.Borrado;
            ag.envioDomicilio = a.EnvioDomicilio;
            return ag;
        }

        //USUARIO

        public SUsuario modeloAEntidad(Usuario u)
        {
            SUsuario usuario = new SUsuario()
            {
                Id = u.id,
                Email = u.email,
                Password = u.password,
                Rol = u.rol,
                Borrado = (bool)u.borrado
            };
            return usuario;
        }

        public Usuario entidadAModelo(SUsuario u)
        {
            Usuario usuario = new Usuario()
            {
                id = u.Id,
                email = u.Email,
                password = u.Password,
                rol = u.Rol,
                borrado = u.Borrado
            };
            return usuario;
        }

        public Usuario entidadAModelo(SUsuario u, Usuario us)
        {
            us.id = u.Id;
            us.email = u.Email;
            us.password = u.Password;
            us.rol = u.Rol;
            us.borrado = u.Borrado;
            return us;
        }

        //CLIENTE

        public SCliente modeloAEntidad(Cliente c)
        {
            SCliente cliente = new SCliente()
            {
                Id = c.id,
                Email = c.email,
                NombreCompleto = c.nombreCompleto,
                TipoDocumento = c.tipoDocumento,
                NumeroDocumento = c.numeroDocumento,
                Telefono = c.telefono,
                Borrado = (bool)c.borrado
            };
            return cliente;
        }

        public Cliente entidadAModelo(SCliente c)
        {
            Cliente cliente = new Cliente()
            {
                id = c.Id,
                email = c.Email,
                nombreCompleto = c.NombreCompleto,
                tipoDocumento = c.TipoDocumento,
                numeroDocumento = c.NumeroDocumento,
                telefono = c.Telefono,
                borrado = c.Borrado
            };
            return cliente;
        }

        public Cliente entidadAModelo(SCliente c, Cliente cl)
        {
            cl.id = c.Id;
            cl.email = c.Email;
            cl.nombreCompleto = c.NombreCompleto;
            cl.tipoDocumento = c.TipoDocumento;
            cl.numeroDocumento = c.NumeroDocumento;
            cl.telefono = c.Telefono;
            cl.borrado = c.Borrado;
            return cl;
        }

        //PAQUETE

        public SPaquete modeloAEntidad(Paquete p)
        {
            SPaquete paquete = new SPaquete()
            {
                Id = p.id,
                IdDestinatario = (int)p.idDestinatario,
                IdRemitente = (int)p.idRemitente,
                IdTrayecto = (int)p.IdTrayecto,
                Codigo = p.codigo,
                FechaIngreso = (DateTime)p.fechaIngreso,
                FechaEntrega = (DateTime)p.fechaEntrega,
                Borrado = (bool)p.borrado
            };
            return paquete;
        }

        public Paquete entidadAModelo(SPaquete p)
        {
            Paquete paquete = new Paquete()
            {
                id = p.Id,
                idDestinatario = p.IdDestinatario,
                idRemitente = p.IdRemitente,
                IdTrayecto = p.IdTrayecto,
                codigo = p.Codigo,
                fechaIngreso = p.FechaIngreso,
                fechaEntrega = p.FechaEntrega,
                borrado = p.Borrado
            };
            return paquete;
        }

        public Paquete entidadAModelo(SPaquete p, Paquete pa)
        {
            pa.id = p.Id;
            pa.idDestinatario = p.IdDestinatario;
            pa.idRemitente = p.IdRemitente;
            pa.IdTrayecto = p.IdTrayecto;
            pa.codigo = p.Codigo;
            pa.fechaIngreso = p.FechaIngreso;
            pa.fechaEntrega = p.FechaEntrega;
            pa.borrado = p.Borrado;
            return pa;
        }

        //TRAYECTO

        public STrayecto modeloAEntidad(Trayecto t)
        {
            STrayecto trayecto = new STrayecto()
            {
                Id = t.id,
                Nombre = t.nombre,
                Version = (int)t.version,
                Borrado = (bool)t.borrado,
                ListaPuntosControl = new List<SPuntoControl>()
            };
            t.PuntoControl.ToList().ForEach(x =>
            {
                trayecto.ListaPuntosControl.Add(modeloAEntidad(x));
            });
            return trayecto;
        }

        public Trayecto entidadAModelo(STrayecto t)
        {
            Trayecto trayecto = new Trayecto()
            {
                id = t.Id,
                nombre = t.Nombre,
                version = t.Version,
                borrado = t.Borrado
            };
            return trayecto;
        }

        public Trayecto entidadAModelo(STrayecto t, Trayecto tr)
        {
            tr.id = t.Id;
            tr.nombre = t.Nombre;
            tr.version = t.Version;
            tr.borrado = t.Borrado;

            bool nuevo = true;
            int id = 0;
            tr.PuntoControl.ToList().ForEach(x =>
            {
                nuevo = true;
                t.ListaPuntosControl.ToList().ForEach(y =>
                {
                    if (x.id == y.Id)
                    {
                        x = entidadAModelo(y);
                        nuevo = false;
                    }
                    id = y.Id;
                });
                if (nuevo)
                {
                    tr.PuntoControl.Add(entidadAModelo(t.ListaPuntosControl.First(y => y.Id == id)));
                }
            });

            return tr;
        }

        //PUNTO DE CONTROL

        public SPuntoControl modeloAEntidad(PuntoControl p)
        {
            SPuntoControl puntoControl = new SPuntoControl()
            {
                Id = p.id,
                IdAgencia = (int)p.idAgencia,
                IdTrayecto = (int)p.idTrayecto,
                Orden = (int)p.orden,
                Tiempo = (int)p.tiempo,
                Borrado = (bool)p.borrado
            };
            return puntoControl;
        }

        public PuntoControl entidadAModelo(SPuntoControl p)
        {
            PuntoControl puntoControl = new PuntoControl()
            {
                id = p.Id,
                idAgencia = p.IdAgencia,
                idTrayecto = p.IdTrayecto,
                orden = p.Orden,
                tiempo = p.Tiempo,
                borrado = p.Borrado
            };
            return puntoControl;
        }

        public PuntoControl entidadAModelo(SPuntoControl p, PuntoControl pc)
        {
            pc.id = p.Id;
            pc.idAgencia = p.IdAgencia;
            pc.idTrayecto = p.IdTrayecto;
            pc.orden = p.Orden;
            pc.tiempo = p.Tiempo;
            pc.borrado = p.Borrado;
            return pc;
        }

        //PAQUETE - PUNTO DE CONTROL

        public SPaquetePuntoControl modeloAEntidad(PaquetePuntoControl p)
        {
            SPaquetePuntoControl paquetePuntoControl = new SPaquetePuntoControl()
            {
                Id = p.id,
                IdPaquete = (int)p.idPaquete,
                IdEmpleado = (int)p.idEmpleado,
                IdPuntoControl = (int)p.idPuntoControl,
                FechaLlegada = (DateTime)p.fechaLlegada,
                Retraso = (int)p.retraso,
                Borrado = (bool)p.borrado
            };
            return paquetePuntoControl;
        }

        public PaquetePuntoControl entidadAModelo(SPaquetePuntoControl p)
        {
            PaquetePuntoControl paquetePuntoControl = new PaquetePuntoControl()
            {
                id = p.Id,
                idPaquete = p.IdPaquete,
                idEmpleado = p.IdEmpleado,
                idPuntoControl = p.IdPuntoControl,
                fechaLlegada = p.FechaLlegada,
                retraso = p.Retraso,
                borrado = p.Borrado
            };
            return paquetePuntoControl;
        }

        public PaquetePuntoControl entidadAModelo(SPaquetePuntoControl p, PaquetePuntoControl pc)
        {
            pc.id = p.Id;
            pc.idPaquete = p.IdPaquete;
            pc.idEmpleado = p.IdEmpleado;
            pc.idPuntoControl = p.IdPuntoControl;
            pc.fechaLlegada = p.FechaLlegada;
            pc.retraso = p.Retraso;
            pc.borrado = p.Borrado;
            return pc;
        }

        //EMPRESA

        public SEmpresa modeloAEntidad(Empresa e)
        {
            SEmpresa empresa = new SEmpresa()
            {
                Id = e.id,
                Nombre = e.nombre,
                Borrado = (bool)e.borrado
            };
            return empresa;
        }

        public Empresa entidadAModelo(SEmpresa e)
        {
            Empresa empresa = new Empresa()
            {
                id = e.Id,
                nombre = e.Nombre,
                borrado = e.Borrado
            };
            return empresa;
        }

        public Empresa entidadAModelo(SEmpresa e, Empresa em)
        {
            em.id = e.Id;
            em.nombre = e.Nombre;
            em.borrado = e.Borrado;
            return em;
        }

    }
}
