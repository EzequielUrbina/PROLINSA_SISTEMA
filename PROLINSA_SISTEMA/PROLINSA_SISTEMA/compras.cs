//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PROLINSA_SISTEMA
{
    using System;
    using System.Collections.Generic;
    
    public partial class compras
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public compras()
        {
            this.detallecompras = new HashSet<detallecompras>();
        }
    
        public int Idcompra { get; set; }
        public Nullable<int> Idproveedor { get; set; }
        public DateTime Fecha { get; set; }
    
        public virtual Proveedores Proveedores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallecompras> detallecompras { get; set; }
    }
}
