﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class trackingFULLEntities : DbContext
    {
        public trackingFULLEntities()
            : base("name=trackingFULLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agencia> Agencia { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Paquete> Paquete { get; set; }
        public virtual DbSet<PaquetePuntoControl> PaquetePuntoControl { get; set; }
        public virtual DbSet<PuntoControl> PuntoControl { get; set; }
        public virtual DbSet<Trayecto> Trayecto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Domicilio5> Domicilio5 { get; set; }
    }
}
