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
    public class detallefacturasController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1();

        // GET: detallefacturas
        public ActionResult Index()
        {
            var detallefactura = db.detallefactura.Include(d => d.Facturas).Include(d => d.Producto);
            return View(detallefactura.ToList());
        }

        // GET: detallefacturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallefactura detallefactura = db.detallefactura.Find(id);
            if (detallefactura == null)
            {
                return HttpNotFound();
            }
            return View(detallefactura);
        }

        // GET: detallefacturas/Create
        public ActionResult Create()
        {
            ViewBag.IdDetalleFactura = new SelectList(db.Facturas, "IdFactura", "IdFactura");
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto");
            return View();
        }

        // POST: detallefacturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdFactura,IdDetalleFactura,IdProducto,Cantidad,Subtotal,Total,Totalporfactura")] detallefactura detallefactura)
        {
            if (ModelState.IsValid)
            {
                db.detallefactura.Add(detallefactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDetalleFactura = new SelectList(db.Facturas, "IdFactura", "IdFactura", detallefactura.IdDetalleFactura);
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detallefactura.IdProducto);
            return View(detallefactura);
        }

        // GET: detallefacturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallefactura detallefactura = db.detallefactura.Find(id);
            if (detallefactura == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDetalleFactura = new SelectList(db.Facturas, "IdFactura", "IdFactura", detallefactura.IdDetalleFactura);
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detallefactura.IdProducto);
            return View(detallefactura);
        }

        // POST: detallefacturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdFactura,IdDetalleFactura,IdProducto,Cantidad,Subtotal,Total,Totalporfactura")] detallefactura detallefactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detallefactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDetalleFactura = new SelectList(db.Facturas, "IdFactura", "IdFactura", detallefactura.IdDetalleFactura);
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detallefactura.IdProducto);
            return View(detallefactura);
        }

        // GET: detallefacturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            detallefactura detallefactura = db.detallefactura.Find(id);
            if (detallefactura == null)
            {
                return HttpNotFound();
            }
            return View(detallefactura);
        }

        // POST: detallefacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            detallefactura detallefactura = db.detallefactura.Find(id);
            db.detallefactura.Remove(detallefactura);
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
