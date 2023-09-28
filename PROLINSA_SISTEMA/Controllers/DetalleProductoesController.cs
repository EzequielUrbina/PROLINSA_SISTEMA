using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROLINSA_SISTEMA;

using PROLINSA_SISTEMA.Permisos;

namespace PROLINSA_SISTEMA.Controllers
{
    [ValidarSesion]
    public class DetalleProductoesController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1();

        // GET: DetalleProductoes
        public ActionResult Index()
        {
            var detalleProducto = db.DetalleProducto.Include(d => d.Producto).Include(d => d.Proveedores);
            return View(detalleProducto.ToList());
        }

        // GET: DetalleProductoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleProducto);
        }

        // GET: DetalleProductoes/Create
        public ActionResult Create()
        {
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto");
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "NombreProveedor");
            return View();
        }

        // POST: DetalleProductoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProducto,NombreProveedor,PaisProveedor,TelefonoProveedor,IdProveedor,FechaCompra")] DetalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.DetalleProducto.Add(detalleProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detalleProducto.IdProducto);
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "NombreProveedor", detalleProducto.IdProveedor);
            return View(detalleProducto);
        }

        // GET: DetalleProductoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detalleProducto.IdProducto);
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "NombreProveedor", detalleProducto.IdProveedor);
            return View(detalleProducto);
        }

        // POST: DetalleProductoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProducto,NombreProveedor,PaisProveedor,TelefonoProveedor,IdProveedor,FechaCompra")] DetalleProducto detalleProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "NombreProducto", detalleProducto.IdProducto);
            ViewBag.IdProveedor = new SelectList(db.Proveedores, "IdProveedor", "NombreProveedor", detalleProducto.IdProveedor);
            return View(detalleProducto);
        }

        // GET: DetalleProductoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            if (detalleProducto == null)
            {
                return HttpNotFound();
            }
            return View(detalleProducto);
        }

        // POST: DetalleProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleProducto detalleProducto = db.DetalleProducto.Find(id);
            db.DetalleProducto.Remove(detalleProducto);
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
