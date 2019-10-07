//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Paquete
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Paquete()
        {
            this.PaquetePuntoControl = new HashSet<PaquetePuntoControl>();
        }
    
        public int id { get; set; }
        public string codigo { get; set; }
        public Nullable<int> IdTrayecto { get; set; }
        public Nullable<int> idRemitente { get; set; }
        public Nullable<int> idDestinatario { get; set; }
        public Nullable<System.DateTime> fechaIngreso { get; set; }
        public Nullable<System.DateTime> fechaEntrega { get; set; }
        public Nullable<bool> borrado { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Cliente Cliente1 { get; set; }
        public virtual Trayecto Trayecto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaquetePuntoControl> PaquetePuntoControl { get; set; }
    }
}
