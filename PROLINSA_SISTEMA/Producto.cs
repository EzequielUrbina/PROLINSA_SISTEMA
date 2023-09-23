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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.detallecompras = new HashSet<detallecompras>();
            this.detallefactura = new HashSet<detallefactura>();
        }
    
        public int IdProducto { get; set; }
        public Nullable<int> Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public Nullable<decimal> Precio_Compra { get; set; }
        public Nullable<decimal> Precio_Venta { get; set; }
        public Nullable<int> Stock { get; set; }
        public Nullable<int> IdCategoria { get; set; }
    
        public virtual Categorias Categorias { get; set; }
        public virtual DetalleProducto DetalleProducto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallecompras> detallecompras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<detallefactura> detallefactura { get; set; }
    }
}
