using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria_de_conexion;
using Proyecto_de_Asistencias.Sesion;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class AprendizMenuController : Controller
    {
        AsistenciaEntities db = new AsistenciaEntities();
        // GET: AprendizMenu
        public ActionResult Subirarchivo()
        {
            return View();
        }
        public ActionResult Archivosoporte()
        {
            return View();
        }

        public ActionResult MenuprincipalAprendiz()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario; // Pasa el ID del usuario a la vista
            return View();
        }

        public ActionResult Consultarasistencias()
        {
            int idUsuario = (int)Session["Usuarios"];

            using (var db = new AsistenciaEntities())
            {
                // Obtener las asistencias del aprendiz desde la base de datos
                var asistencias = db.Registro_Asistencias_QR
                                    .Where(a => a.idAprendiz == idUsuario)
                                    .ToList();

                // Pasar las asistencias a la vista mediante ViewBag o un modelo de vista
                ViewBag.Asistencias = asistencias;
            }

            return View();
        }
        public ActionResult Consultarinasistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario; // Pasa el ID del usuario a la vista
            return View();
        }

        public ActionResult MostrarQR()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario; // Pasa el ID del usuario a la vista
            return View();
        }

        public ActionResult FormularioAsistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario; // Pasa el ID del usuario a la vista
            return View();
        }

        public ActionResult ObtenerImagenQR()
        {
            string base64String = Session["QrCodeBase64"] as string;
            if (!string.IsNullOrEmpty(base64String))
            {
                byte[] qrCodeImage = Convert.FromBase64String(base64String);
                return File(qrCodeImage, "image/png");
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult VerificarDisponibilidadQR()
        {
            string base64String = Session["QrCodeBase64"] as string;
            bool qrDisponible = !string.IsNullOrEmpty(base64String);
            return Json(new { disponible = qrDisponible }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult RegistrarAsistencia(string fecha, int fichaId, string nameprog, string namecompe, string hora, int instructorId, string uniqueId, int aprendizId)
        {
            // Verificar si ya se ha registrado la asistencia con el mismo uniqueId y aprendizId
            var asistenciaExistente = db.Registro_Asistencias_QR.Any(a => a.idAprendiz == aprendizId && a.Fecha_Asistencia == fecha && a.Hora_Asistencia == hora);

            if (asistenciaExistente)
            {
                return Json(new { success = false, message = "La asistencia ya ha sido registrada." });
            }

            // Obtener los detalles del aprendiz para completar el registro
            var aprendiz = db.Aprendiz.FirstOrDefault(a => a.idAprendiz == aprendizId);
            if (aprendiz == null)
            {
                return Json(new { success = false, message = "Aprendiz no encontrado." });
            }

            // Registrar la asistencia
            Registro_Asistencias_QR nuevaAsistencia = new Registro_Asistencias_QR
            {
                Fecha_Asistencia = fecha,
                Hora_Asistencia = hora,
                Tipo_Asistencia = true,
                Nombres = aprendiz.Nombres_Aprenidiz,
                Apellidos = aprendiz.Apellidos_Aprendiz,
                ImagenQR = uniqueId,
                Competencias = namecompe,
                Ficha = fichaId,
                Programa = nameprog,
                idInstructor = instructorId,
                idAprendiz = aprendizId
            };

            db.Registro_Asistencias_QR.Add(nuevaAsistencia);
            db.SaveChanges();

            return Json(new { success = true, message = "Asistencia registrada exitosamente." });

        }

    }
}