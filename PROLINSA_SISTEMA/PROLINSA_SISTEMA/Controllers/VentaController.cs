﻿using PROLINSA_SISTEMA.Models;
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

namespace PROLINSA_SISTEMA.Controllers
{
    public class VentaController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1(); //Declaramos la coneccion a la base de datos para poder usarla


        // GET: Venta
        public ActionResult Venta()
        {
            OrderView orderView = new OrderView();
            orderView.IDEMPLEADO = new List<FacturaMaestro>();
            orderView.IDCLIENTE = new List<FacturaMaestro>();
            orderView.IDTIPOFACTURA = new List<FacturaMaestro>();
            orderView.FechaA = new DateTime();

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
            DateTime fechaventa = Convert.ToDateTime(Request["FechaA"]);

            Facturas newventas = new Facturas
            {
                IdEmpleados= empleadoid, 
                IdCliente = clienteid,
                IdTipoFactura = facturaid,
                Fecha = fechaventa,
                //Total = 
            };
            db.Facturas.Add(newventas);
            db.SaveChanges();

            int ultimoidfactura = db.Facturas.ToList().Select(o => o.IdFactura).Max();

            foreach(FacturaDetalle item in orderview.idproductos)
            {
                var detail = new detallefactura()
                {
                    IdDetalleFactura = ultimoidfactura,
                    IdProducto = item.IdProducto,
                    Cantidad = item.CANTIDAD,
                    Subtotal = item.Precio_Venta,
                    Total = item.TOTAL,      
                    
                };
                db.detallefactura.Add(detail);
                
            }


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
        public ActionResult AddProduct()
        {
            var listp = db.Producto.ToList();
            ViewBag.IdProducto = new SelectList(listp, "IdProducto", "NombreProducto");

            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(FacturaDetalle productorder)
        {
            var orderview = Session["OrderView"] as OrderView;
            var productid = int.Parse(Request["IdProducto"]); 
            var product = db.Producto.Find(productid);

            productorder = new FacturaDetalle
            {
                IdProducto= product.IdProducto,
                NombreProducto = product.NombreProducto,
                Precio_Venta = product.Precio_Venta,
                CANTIDAD = int.Parse(Request["CANTIDAD"]),        
            };

            orderview.idproductos.Add(productorder);//revisar esta linia en el video


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
        public ActionResult VerFacturas()
        {
            List<Facturas> facturas1 = db.Facturas.ToList();
            return View(facturas1);
        }


        [HttpGet]
        public ActionResult _Detalleparcial(int Facturaid)
        {
            List<detallefactura> detalle = db.detallefactura.Where(m => m.IdDetalleFactura == Facturaid).ToList();

            return PartialView(detalle);
        }





    }
}