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
    public class TipoFacturasController : Controller
    {
        private PROLINSAEntities1 db = new PROLINSAEntities1();

        // GET: TipoFacturas
        public ActionResult Index()
        {
            return View(db.TipoFactura.ToList());
        }

        // GET: TipoFacturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFactura tipoFactura = db.TipoFactura.Find(id);
            if (tipoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tipoFactura);
        }

        // GET: TipoFacturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoFacturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipoFactura,Factura,Cotizacion")] TipoFactura tipoFactura)
        {
            if (ModelState.IsValid)
            {
                db.TipoFactura.Add(tipoFactura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoFactura);
        }

        // GET: TipoFacturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFactura tipoFactura = db.TipoFactura.Find(id);
            if (tipoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tipoFactura);
        }

        // POST: TipoFacturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipoFactura,Factura,Cotizacion")] TipoFactura tipoFactura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoFactura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoFactura);
        }

        // GET: TipoFacturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoFactura tipoFactura = db.TipoFactura.Find(id);
            if (tipoFactura == null)
            {
                return HttpNotFound();
            }
            return View(tipoFactura);
        }

        // POST: TipoFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoFactura tipoFactura = db.TipoFactura.Find(id);
            db.TipoFactura.Remove(tipoFactura);
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
