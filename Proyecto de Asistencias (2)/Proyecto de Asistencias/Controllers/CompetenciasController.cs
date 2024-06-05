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
    public class CompetenciasController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities();

        // GET: Competencias
        public ActionResult Index(string buscar)
        {
            var competencia = db.Competencia.Include(c => c.Administrador)
                                            .Include(c => c.Instructor)
                                            .Include(c => c.Programa_Formacion);

            if (!String.IsNullOrEmpty(buscar))
            {
                // Asumiendo que quieres buscar por un campo 'Nombre' en la entidad 'Competencia'
                competencia = competencia.Where(c => c.Nombre_Competencia.Contains(buscar));
            }

            return View(competencia.ToList());
        }


        // GET: Competencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competencia competencia = db.Competencia.Find(id);
            if (competencia == null)
            {
                return HttpNotFound();
            }
            return View(competencia);
        }

        // GET: Competencias/Create
        public ActionResult Create()
        {
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador");
            ViewBag.idInstructor = new SelectList(db.Instructor, "idInstructor", "idInstructor");
            ViewBag.idPrograma = new SelectList(db.Programa_Formacion, "idPrograma", "idPrograma");
            return View();
        }

        // POST: Competencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idCompetencia,Nombre_Competencia,Tipo_Competencias,Duracion_Competencia,idAdministrador,idPrograma,idInstructor")] Competencia competencia)
        {
            if (ModelState.IsValid)
            {
                db.Competencia.Add(competencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", competencia.idAdministrador);
            ViewBag.idInstructor = new SelectList(db.Instructor, "idInstructor", "idInstructor", competencia.idInstructor);
            ViewBag.idPrograma = new SelectList(db.Programa_Formacion, "idPrograma", "idPrograma", competencia.idPrograma);
            return View(competencia);
        }

        // GET: Competencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competencia competencia = db.Competencia.Find(id);
            if (competencia == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", competencia.idAdministrador);
            ViewBag.idInstructor = new SelectList(db.Instructor, "idInstructor", "idInstructor", competencia.idInstructor);
            ViewBag.idPrograma = new SelectList(db.Programa_Formacion, "idPrograma", "idPrograma", competencia.idPrograma);
            return View(competencia);
        }

        // POST: Competencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idCompetencia,Nombre_Competencia,Tipo_Competencias,Duracion_Competencia,idAdministrador,idPrograma,idInstructor")] Competencia competencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(competencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAdministrador = new SelectList(db.Administrador, "idAdministrador", "idAdministrador", competencia.idAdministrador);
            ViewBag.idInstructor = new SelectList(db.Instructor, "idInstructor", "idInstructor", competencia.idInstructor);
            ViewBag.idPrograma = new SelectList(db.Programa_Formacion, "idPrograma", "idPrograma", competencia.idPrograma);
            return View(competencia);
        }

        // GET: Competencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Competencia competencia = db.Competencia.Find(id);
            if (competencia == null)
            {
                return HttpNotFound();
            }
            return View(competencia);
        }

        // POST: Competencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Competencia competencia = db.Competencia.Find(id);
            db.Competencia.Remove(competencia);
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
