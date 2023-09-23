﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PROLINSAEntities1 : DbContext
    {
        public PROLINSAEntities1()
            : base("name=PROLINSAEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ActividadProveedor> ActividadProveedor { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<DetalleProducto> DetalleProducto { get; set; }
        public virtual DbSet<EMPLEADOS> EMPLEADOS { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<TipoFactura> TipoFactura { get; set; }
        public virtual DbSet<compras> compras { get; set; }
        public virtual DbSet<detallecompras> detallecompras { get; set; }
        public virtual DbSet<detallefactura> detallefactura { get; set; }
        public virtual DbSet<Facturas> Facturas { get; set; }
        public virtual DbSet<USUARIO> USUARIO { get; set; }
    
        public virtual ObjectResult<SP_BusquedaProveedor_Result> SP_BusquedaProveedor(string iDProveedor)
        {
            var iDProveedorParameter = iDProveedor != null ?
                new ObjectParameter("IDProveedor", iDProveedor) :
                new ObjectParameter("IDProveedor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_BusquedaProveedor_Result>("SP_BusquedaProveedor", iDProveedorParameter);
        }
    
        public virtual int SP_INSERTARCATEGORIAS(string nombre_Categoria, string descripcion_Categoria)
        {
            var nombre_CategoriaParameter = nombre_Categoria != null ?
                new ObjectParameter("Nombre_Categoria", nombre_Categoria) :
                new ObjectParameter("Nombre_Categoria", typeof(string));
    
            var descripcion_CategoriaParameter = descripcion_Categoria != null ?
                new ObjectParameter("Descripcion_Categoria", descripcion_Categoria) :
                new ObjectParameter("Descripcion_Categoria", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_INSERTARCATEGORIAS", nombre_CategoriaParameter, descripcion_CategoriaParameter);
        }
    
        public virtual int SP_InsertarProductos(Nullable<int> codigo, string nombreProducto, string descripcion, Nullable<decimal> precio_Compra, Nullable<decimal> precio_Venta, Nullable<int> stock, Nullable<int> idCategoria)
        {
            var codigoParameter = codigo.HasValue ?
                new ObjectParameter("Codigo", codigo) :
                new ObjectParameter("Codigo", typeof(int));
    
            var nombreProductoParameter = nombreProducto != null ?
                new ObjectParameter("NombreProducto", nombreProducto) :
                new ObjectParameter("NombreProducto", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            var precio_CompraParameter = precio_Compra.HasValue ?
                new ObjectParameter("Precio_Compra", precio_Compra) :
                new ObjectParameter("Precio_Compra", typeof(decimal));
    
            var precio_VentaParameter = precio_Venta.HasValue ?
                new ObjectParameter("Precio_Venta", precio_Venta) :
                new ObjectParameter("Precio_Venta", typeof(decimal));
    
            var stockParameter = stock.HasValue ?
                new ObjectParameter("Stock", stock) :
                new ObjectParameter("Stock", typeof(int));
    
            var idCategoriaParameter = idCategoria.HasValue ?
                new ObjectParameter("IdCategoria", idCategoria) :
                new ObjectParameter("IdCategoria", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_InsertarProductos", codigoParameter, nombreProductoParameter, descripcionParameter, precio_CompraParameter, precio_VentaParameter, stockParameter, idCategoriaParameter);
        }
    
        public virtual int sp_RegistrarUsuarios(string correo, string clave, ObjectParameter registrado, ObjectParameter mensaje)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var claveParameter = clave != null ?
                new ObjectParameter("Clave", clave) :
                new ObjectParameter("Clave", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_RegistrarUsuarios", correoParameter, claveParameter, registrado, mensaje);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_ValidarUsuario(string correo, string clave)
        {
            var correoParameter = correo != null ?
                new ObjectParameter("Correo", correo) :
                new ObjectParameter("Correo", typeof(string));
    
            var claveParameter = clave != null ?
                new ObjectParameter("Clave", clave) :
                new ObjectParameter("Clave", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_ValidarUsuario", correoParameter, claveParameter);
        }
    }
}
