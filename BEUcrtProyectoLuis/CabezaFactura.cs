//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BEUcrtProyectoLuis
{
    using System;
    using System.Collections.Generic;
    
    public partial class CabezaFactura
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CabezaFactura()
        {
            this.CuerpoFactura = new HashSet<CuerpoFactura>();
        }
    
        public int cbf_id { get; set; }
        public int cln_id { get; set; }
        public Nullable<System.DateTime> cbf_dateOfCreated { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CuerpoFactura> CuerpoFactura { get; set; }
    }
}
