using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROLINSA_SISTEMA;

namespace PROLINSA_SISTEMA.Controllers
{
    public class FacturasController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1();

        // GET: Facturas
        public ActionResult Index()
        {
            var facturas = db.Facturas.Include(f => f.Clientes).Include(f => f.EMPLEADOS).Include(f => f.TipoFactura);
            return View(facturas.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "NombreCliente");
            ViewBag.IdEmpleados = new SelectList(db.EMPLEADOS, "ID_Empleados", "Nombre_Empleado");
            ViewBag.IdTipoFactura = new SelectList(db.TipoFactura, "IdTipoFactura", "Factura");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFactura,IdEmpleados,IdCliente,IdTipoFactura,Fecha")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Facturas.Add(facturas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "NombreCliente", facturas.IdCliente);
            ViewBag.IdEmpleados = new SelectList(db.EMPLEADOS, "ID_Empleados", "Nombre_Empleado", facturas.IdEmpleados);
            ViewBag.IdTipoFactura = new SelectList(db.TipoFactura, "IdTipoFactura", "Factura", facturas.IdTipoFactura);
            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "NombreCliente", facturas.IdCliente);
            ViewBag.IdEmpleados = new SelectList(db.EMPLEADOS, "ID_Empleados", "Nombre_Empleado", facturas.IdEmpleados);
            ViewBag.IdTipoFactura = new SelectList(db.TipoFactura, "IdTipoFactura", "Factura", facturas.IdTipoFactura);
            return View(facturas);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFactura,IdEmpleados,IdCliente,IdTipoFactura,Fecha")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCliente = new SelectList(db.Clientes, "IdCliente", "NombreCliente", facturas.IdCliente);
            ViewBag.IdEmpleados = new SelectList(db.EMPLEADOS, "ID_Empleados", "Nombre_Empleado", facturas.IdEmpleados);
            ViewBag.IdTipoFactura = new SelectList(db.TipoFactura, "IdTipoFactura", "Factura", facturas.IdTipoFactura);
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = db.Facturas.Find(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Facturas facturas = db.Facturas.Find(id);
            db.Facturas.Remove(facturas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
