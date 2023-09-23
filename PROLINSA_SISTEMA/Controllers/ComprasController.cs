using PROLINSA_SISTEMA.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using PROLINSA_SISTEMA;
using System.Data.SqlClient;

namespace PROLINSA_SISTEMA.Controllers
{
    public class ComprasController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1(); //Declaramos la coneccion a la base de datos para poder usarla
        SqlConnection conexion = new SqlConnection("server=PC ; database=PROLINSA ; integrated security = true");


        // GET: Compras
        public ActionResult Compras()
        {
            ComprasView comprasView = new ComprasView();
            comprasView.IDPROVEEDOR = new List<Proveedores>();
            comprasView.FechaA = new DateTime();

            comprasView.IdProducto = new List<detallecompraas>();
            comprasView.CANTIDAD = new int();
            comprasView.Subtotal = new FacturaDetalle();
            comprasView.Total = new FacturaDetalle();


            Session["OrderView"] = comprasView;

            //declaramos una lista que contendra el Id de los proveedores, pero cargara su nombre para efectos visuales
            var listt1 = db.Proveedores.ToList();
            ViewBag.IdProveedor = new SelectList(listt1, "IdProveedor", "NombreProveedor");

            return View(comprasView);
        }

        [HttpPost]
        public ActionResult Compras(ComprasView comprasView)
        {
            comprasView = Session["OrderView"] as ComprasView;
            int idproveedor = int.Parse(Request["IDPROVEEDOR"]);
            DateTime fechacompra = Convert.ToDateTime(Request["FechaA"]);

            compras newcompras = new compras
            {
                Idproveedor = idproveedor,
                Fecha = fechacompra
            };
            db.compras.Add(newcompras);
            db.SaveChanges();

            int ultimoidcompra = db.compras.ToList().Select(o => o.Idcompra).Max();//sacamos el ultimo id de la tabla compras

            Nullable<decimal> totalneto = 0;

            foreach (detallecompraas item in comprasView.IdProducto)
            {
                totalneto = totalneto + item.TOTAL; //Total de una facutura se suman todos los subtotales

                var detail = new detallecompras()
                {
                    IdDetalleCompras = ultimoidcompra,
                    IdProducto = item.IdProducto,
                    Cantidad = item.CANTIDAD,
                    Subtotal = item.Precio_Compra,
                    Total = item.TOTAL
                };
                db.detallecompras.Add(detail);

                conexion.Open();//abrimos la coneccion a la base de datos
                SqlCommand comando2 = new SqlCommand("update Producto set Stock = Producto.Stock + @itemcantidadcomprada where IdProducto = @idproducto", conexion); //declaramos una variable tipo sqlcommand para hacer un update a la ultima factura realizada con el total neto
                comando2.Parameters.Add("@itemcantidadcomprada", SqlDbType.Int); //declaramos un parametro para la cantidad de articulos vendido
                comando2.Parameters.Add("@idproducto", SqlDbType.Int); //declaramos un parametro para saber cual fue el producto vendido

                comando2.Parameters["@itemcantidadcomprada"].Value = item.CANTIDAD; //asignamos el valor del total
                comando2.Parameters["@idproducto"].Value = item.IdProducto; //asignamos el valor de la ultima factura

                comando2.ExecuteNonQuery();//ejecutamos nuestra consulta
                conexion.Close();//cerramos la coneccion a la base de datos
            }

            //Modulo para actualizar el total neto de la compra
            SqlCommand comando = new SqlCommand("update compras set Total = @totalneto where Idcompra = @ultimoidcompra", conexion); //declaramos una variable tipo sqlcommand para hacer un update a la ultima factura realizada con el total neto
            conexion.Open();//abrimos la coneccion a la base de datos

            comando.Parameters.Add("@totalneto", SqlDbType.Decimal); //declaramos un parametro para el total de la factura
            comando.Parameters.Add("@ultimoidcompra", SqlDbType.Int); //declaramos un parametro para saber cual fue la ultima factura generada

            comando.Parameters["@totalneto"].Value = totalneto; //asignamos el valor del total
            comando.Parameters["@ultimoidcompra"].Value = ultimoidcompra; //asignamos el valor de la ultima factura

            comando.ExecuteNonQuery();//ejecutamos nuestra consulta
            conexion.Close();//cerramos la coneccion a la base de datos


            db.SaveChanges();
            comprasView = Session["OrderView"] as ComprasView;

            var listt1 = db.Proveedores.ToList();
            ViewBag.IdProveedor = new SelectList(listt1, "IdProveedor", "NombreProveedor");

            return View(comprasView);

        }


















        [HttpGet]
        public ActionResult ProductosAdd()
        {
            var listp = db.Producto.ToList();
            ViewBag.IdProducto = new SelectList(listp, "IdProducto", "NombreProducto");

            return View();
        }

        [HttpPost]
        public ActionResult ProductosAdd(detallecompraas productorderr)
        {
            var comprasView = Session["OrderView"] as ComprasView;
            var productid = int.Parse(Request["IdProducto"]);
            var product = db.Producto.Find(productid);

            productorderr = new detallecompraas
            {
                IdProducto = product.IdProducto,
                NombreProducto = product.NombreProducto,
                CANTIDAD = int.Parse(Request["CANTIDAD"]),
                Precio_Compra = product.Precio_Compra
               
            };

            comprasView.IdProducto.Add(productorderr);//revisar esta linia en el video


            var listt1 = db.Proveedores.ToList();
            ViewBag.IdProveedor = new SelectList(listt1, "IdProveedor", "NombreProveedor");

            var listp = db.Producto.ToList();
            ViewBag.IdProducto = new SelectList(listp, "IdProducto", "NombreProducto");

            return View("Compras", comprasView);
        }



        [HttpGet]
        public ActionResult VerCompras()
        {
            List<compras> compras1 = db.compras.ToList();
            return View(compras1);
        }

        [HttpGet]
        public ActionResult _Detalleparcialcompras(int Facturaid)
        {
            List<detallecompras> detalle = db.detallecompras.Where(m => m.IdDetalleCompras == Facturaid).ToList();

            return PartialView(detalle);
        }






    }
}