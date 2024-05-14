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
    public class InstructorsController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities();

        // GET: Instructors
        public ActionResult Index()
        {
            var instructor = db.Instructor.Include(i => i.Administrador).Include(i => i.Ficha);
            return View(instructor.ToList());
        }

        // GET: Instructors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // GET: Instructors/Create
        public ActionResult Create()
        {
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador");
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha");
            return View();
        }

        // POST: Instructors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idInstructor,Nombre_Instructor,Apellido_Instructor,Email_Instructor,Contraseña_Instructor,Imagen_Qr,Numero_Ficha,idAdministrador")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Instructor.Add(instructor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", instructor.idAdministrador);
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha", instructor.Numero_Ficha);
            return View(instructor);
        }

        // GET: Instructors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", instructor.idAdministrador);
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha", instructor.Numero_Ficha);
            return View(instructor);
        }

        // POST: Instructors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idInstructor,Nombre_Instructor,Apellido_Instructor,Email_Instructor,Contraseña_Instructor,Imagen_Qr,Numero_Ficha,idAdministrador")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instructor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", instructor.idAdministrador);
            ViewBag.Numero_Ficha = new SelectList(db.Ficha, "Numero_Ficha", "Numero_Ficha", instructor.Numero_Ficha);
            return View(instructor);
        }

        // GET: Instructors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Instructor instructor = db.Instructor.Find(id);
            if (instructor == null)
            {
                return HttpNotFound();
            }
            return View(instructor);
        }

        // POST: Instructors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Instructor instructor = db.Instructor.Find(id);
            db.Instructor.Remove(instructor);
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
