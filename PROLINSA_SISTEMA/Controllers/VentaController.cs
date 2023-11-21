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
using Rotativa;

using PROLINSA_SISTEMA.Permisos;
using Microsoft.Ajax.Utilities;

namespace PROLINSA_SISTEMA.Controllers
{

    public class VentaController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1(); //Declaramos la coneccion a la base de datos para poder usarla
        SqlConnection conexion = new SqlConnection("server=LAPTOP-V4474JBK ; database=PROLINSA ; integrated security = true");

        // GET: Venta
        [ValidarSesion]
        public ActionResult Venta()
        {
            OrderView orderView = new OrderView();
            orderView.IDEMPLEADO = new List<FacturaMaestro>();
            orderView.IDCLIENTE = new List<FacturaMaestro>();
            orderView.IDTIPOFACTURA = new List<FacturaMaestro>();
            orderView.FechaA = new DateTime();
            orderView.TotalNETO = new decimal();

            orderView.idproductos = new List<FacturaDetalle>();
            orderView.Cantidad = new FacturaDetalle();
            orderView.Subtotal = new FacturaDetalle();
            orderView.Total = new FacturaDetalle();


            Session["OrderView"] = orderView;

            //declaramos una lista que contendra el Id de los empleados, pero cargara su nombre para efectos visuales
            var list1 = db.EMPLEADOS.ToList();
            ViewBag.ID_Empleados = new SelectList(list1, "ID_Empleados", "Nombre_Empleado");

            var list2 = db.Clientes.ToList();
            ViewBag.IdCliente = new SelectList(list2, "IdCliente", "NombreCliente");

            var list3 = db.TipoFactura.ToList();
            ViewBag.IdTipoFactura = new SelectList(list3, "IdTipoFactura", "Factura");

            return View(orderView);

        }

        [HttpPost]
        public ActionResult Venta(OrderView orderview)
        {

            orderview = Session["OrderView"] as OrderView;
            int empleadoid = int.Parse(Request["ID_Empleados"]);
            int clienteid = int.Parse(Request["IdCliente"]);
            int facturaid = int.Parse(Request["IdTipoFactura"]);
            /*  DateTime fechaventa = Convert.ToDateTime(Request["FechaA"]); */

            DateTime fechaventa;
            string fechaAString = Request["FechaA"];
            fechaventa = Convert.ToDateTime(fechaAString);


            /*  validamos si existe stock */

            if (orderview.idproductos == null || orderview.idproductos.Count == 0)
            {
                // Código a ejecutar si IdProducto es nulo o su conteo es cero
                ViewBag.carritovacio2 = "La lista de venta está vacía. Agrega productos antes de guardar la venta.";

                return View("Error");
            }








            Facturas newventas = new Facturas
            {
                IdEmpleados = empleadoid,
                IdCliente = clienteid,
                IdTipoFactura = facturaid,
                Fecha = fechaventa,
                //Total
        };
            db.Facturas.Add(newventas);
            db.SaveChanges();

            int ultimoidfactura = db.Facturas.ToList().Select(o => o.IdFactura).Max();

            Nullable<decimal> totalneto = 0;

            foreach(FacturaDetalle item in orderview.idproductos)
            {
                totalneto = totalneto + item.TOTAL; //Total de una facutura se suman todos los subtotales
                
                var detail = new detallefactura()
                {
                    IdDetalleFactura = ultimoidfactura,
                    IdProducto = item.IdProducto,
                    Cantidad = item.CANTIDAD,
                    Subtotal = item.Precio_Venta,
                    Total = item.TOTAL,

            };
                db.detallefactura.Add(detail);


                conexion.Open();//abrimos la coneccion a la base de datos
                SqlCommand comando1 = new SqlCommand("update Producto set Stock = Producto.Stock - @itemcantidadvendida where IdProducto = @idproducto", conexion); //declaramos una variable tipo sqlcommand para hacer un update a la ultima factura realizada con el total neto
                comando1.Parameters.Add("@itemcantidadvendida", SqlDbType.Int); //declaramos un parametro para la cantidad de articulos vendido
                comando1.Parameters.Add("@idproducto", SqlDbType.Int); //declaramos un parametro para saber cual fue el producto vendido

                comando1.Parameters["@itemcantidadvendida"].Value = item.CANTIDAD; //asignamos el valor del total
                comando1.Parameters["@idproducto"].Value = item.IdProducto; //asignamos el valor de la ultima factura

                comando1.ExecuteNonQuery();//ejecutamos nuestra consulta
                conexion.Close();//cerramos la coneccion a la base de datos

            }

            //Modulo para actualizar el total neto de la factura
            SqlCommand comando = new SqlCommand("update Facturas set Total = @totalneto where IdFactura = @ultimoidfactura", conexion); //declaramos una variable tipo sqlcommand para hacer un update a la ultima factura realizada con el total neto
            conexion.Open();//abrimos la coneccion a la base de datos

            comando.Parameters.Add("@totalneto", SqlDbType.Decimal); //declaramos un parametro para el total de la factura
            comando.Parameters.Add("@ultimoidfactura", SqlDbType.Int); //declaramos un parametro para saber cual fue la ultima factura generada

            comando.Parameters["@totalneto"].Value = totalneto; //asignamos el valor del total
            comando.Parameters["@ultimoidfactura"].Value = ultimoidfactura; //asignamos el valor de la ultima factura

            comando.ExecuteNonQuery();//ejecutamos nuestra consulta
            conexion.Close();//cerramos la coneccion a la base de datos

            db.SaveChanges();
            orderview = Session["OrderView"] as OrderView;


            var list1 = db.EMPLEADOS.ToList();
            ViewBag.ID_Empleados = new SelectList(list1, "ID_Empleados", "Nombre_Empleado");

            var list2 = db.Clientes.ToList();
            ViewBag.IdCliente = new SelectList(list2, "IdCliente", "NombreCliente");

            var list3 = db.TipoFactura.ToList();
            ViewBag.IdTipoFactura = new SelectList(list3, "IdTipoFactura", "Factura");
          

            return View(orderview);

        }

    









        [HttpGet]
        [ValidarSesion]
        public ActionResult AddProduct()
        {
            var listp = db.Producto.ToList();
            ViewBag.IdProducto = new SelectList(listp, "IdProducto", "NombreProducto");

            

            return View();
        }


        [HttpPost]
        [ValidarSesion]
        public ActionResult AddProduct(FacturaDetalle productorder)
        {
            var orderview = Session["OrderView"] as OrderView;
            var productid = int.Parse(Request["IdProducto"]); 
            var product = db.Producto.Find(productid);

            var stockactual = product.Stock;

            if (product.Stock <= 0)
            {
                ViewBag.MiDato = "Hola desde el controlador";
                return RedirectToAction("Venta");
            }

            productorder = new FacturaDetalle
            {
                IdProducto= product.IdProducto,
                NombreProducto = product.NombreProducto,
                Precio_Venta = product.Precio_Venta,
                CANTIDAD = int.Parse(Request["CANTIDAD"]),        
            };

            orderview.idproductos.Add(productorder);


            var list1 = db.EMPLEADOS.ToList();
            ViewBag.ID_Empleados = new SelectList(list1, "ID_Empleados", "Nombre_Empleado");

            var list2 = db.Clientes.ToList();
            ViewBag.IdCliente = new SelectList(list2, "IdCliente", "NombreCliente");

            var list3 = db.TipoFactura.ToList();
            ViewBag.IdTipoFactura = new SelectList(list3, "IdTipoFactura", "Factura");

            var listp = db.Producto.ToList();
            ViewBag.IdProducto = new SelectList(listp, "IdProducto", "NombreProducto");

            return View("Venta",orderview);
        }





        [HttpGet]
        [ValidarSesion]
        public ActionResult VerFacturas()
        {
            List<Facturas> facturas1 = db.Facturas.ToList();
            return View(facturas1);
        }


        [HttpGet]
        [ValidarSesion]
        public ActionResult _Detalleparcial(int Facturaid)
        {
            List<detallefactura> detalle = db.detallefactura.Where(m => m.IdDetalleFactura == Facturaid).ToList();

            return PartialView(detalle);
        }



        [HttpGet]
        [ValidarSesion]
        public ActionResult _VentasFacturacion(int Facturaid, DateTime fecha, decimal total, string cliente, string empleado, string tipo)
        {
            List<detallefactura> detalle = db.detallefactura.Where(m => m.IdDetalleFactura == Facturaid).ToList();
            ViewBag.fechaa = fecha;
            ViewBag.nFactura = Facturaid;
            ViewBag.totall = total;
            ViewBag.clientess = cliente;
            ViewBag.empleadoss = empleado;
            ViewBag.tipoo = tipo;
            return View(detalle);
        }

        [HttpGet]
        public ActionResult _VentasDescargar(int Facturaid, DateTime fecha, decimal total, string cliente, string empleado, string tipo)
        {
            

            List<detallefactura> detalle = db.detallefactura.Where(m => m.IdDetalleFactura == Facturaid).ToList();
            ViewBag.fechaa = fecha;
            ViewBag.nFactura = Facturaid;
            ViewBag.totall = total;
            ViewBag.clientess = cliente;
            ViewBag.empleadoss = empleado;
            ViewBag.tipoo = tipo;
            return View(detalle);
        }


        public ActionResult ImprimirFactura(int Facturaid, DateTime fecha, decimal total, string cliente, string empleado, string tipo)
        {
            
            return new ActionAsPdf("_VentasDescargar", new { Facturaid, fecha, total, cliente, empleado, tipo }) { FileName = "Factura.pdf" };
        }



    }
}