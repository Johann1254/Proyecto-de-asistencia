using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Libreria_de_conexion;
using Proyecto_de_Asistencias.Sesion;
namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class AprendizsController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities();

        // GET: Aprendizs
        public ActionResult Index()
        {
            var aprendiz = db.Aprendiz.Include(a => a.Administrador).Include(a => a.Soporte_asistencia).Include(a => a.Ficha);
            return View(aprendiz.ToList());
        }

        // GET: Aprendizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendiz.Find(id);
            if (aprendiz == null)
            {
                return HttpNotFound();
            }
            return View(aprendiz);
        }

        // GET: Aprendizs/Create
        public ActionResult Create()
        {
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador");
            ViewBag.idSoporte = new SelectList(db.Soporte_asistencia, "idSoporte", "Fecha_Soporte");
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha");
            return View();
        }

        // POST: Aprendizs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idAprendiz,Nombres_Aprenidiz,Apellidos_Aprendiz,Email_Aprenidiz,Tipo_Documento,Numero_Documento,Contraseña,Estado,Numero_Ficha,idAdministrador")] Aprendiz aprendiz)
        {
            if (ModelState.IsValid)
            {
                db.Aprendiz.Add(aprendiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", aprendiz.idAdministrador);
            //ViewBag.idSoporte = new SelectList(db.Soporte_asistencia, "idSoporte", "Fecha_Soporte", aprendiz.idSoporte);
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha", aprendiz.Numero_Ficha);
            return View(aprendiz);
        }

        // GET: Aprendizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendiz.Find(id);
            if (aprendiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", aprendiz.idAdministrador);
            //ViewBag.idSoporte = new SelectList(db.Soporte_asistencia, "idSoporte", "Fecha_Soporte", aprendiz.idSoporte);
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha", aprendiz.Numero_Ficha);
            return View(aprendiz);
        }

        // POST: Aprendizs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idAprendiz,Nombres_Aprenidiz,Apellidos_Aprendiz,Email_Aprenidiz,Tipo_Documento,Numero_Documento,Contraseña,Estado,Numero_Ficha,idAdministrador")] Aprendiz aprendiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aprendiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", aprendiz.idAdministrador);
            //ViewBag.idSoporte = new SelectList(db.Soporte_asistencia, "idSoporte", "Fecha_Soporte", aprendiz.idSoporte);
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha", aprendiz.Numero_Ficha);
            return View(aprendiz);
        }

        // GET: Aprendizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aprendiz aprendiz = db.Aprendiz.Find(id);
            if (aprendiz == null)
            {
                return HttpNotFound();
            }
            return View(aprendiz);
        }

        // POST: Aprendizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aprendiz aprendiz = db.Aprendiz.Find(id);
            db.Aprendiz.Remove(aprendiz);
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
