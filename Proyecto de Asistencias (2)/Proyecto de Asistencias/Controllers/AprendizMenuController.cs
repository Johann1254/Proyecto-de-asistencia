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
        // GET: AprendizMenu
        public ActionResult MenuprincipalAprendiz()
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
        public ActionResult RegistrarAsistencia(string fecha, string hora, int fichaId, string programa, string competencia, int instructorId, string uniqueId, string nombres, string apellidos, int aprendizId)
        {
            using (var db = new AsistenciaEntities())
            {
                // Obtener el aprendiz de la base de datos usando el ID del aprendiz
                var aprendiz = db.Aprendiz.Find(aprendizId);

                // Validar que el aprendiz existe y que los nombres y apellidos coinciden
                if (aprendiz == null || aprendiz.Nombres_Aprenidiz != nombres || aprendiz.Apellidos_Aprendiz != apellidos)
                {
                    return Json(new { success = false, message = "Datos de aprendiz incorrectos." });
                }

                // Verificar si ya existe un registro de asistencia para el mismo aprendiz en la misma fecha y hora
                bool existeRegistro = db.Registro_Asistencias_QR.Any(a => a.Fecha_Asistencia == fecha && a.Hora_Asistencia == hora && a.idAprendiz == aprendizId);

                if (existeRegistro)
                {
                    return Json(new { success = false, message = "Ya existe un registro de asistencia para este aprendiz en esta fecha y hora." });
                }

                // Crear un nuevo registro de asistencia
                var asistencia = new Registro_Asistencias_QR
                {
                    Fecha_Asistencia = fecha,
                    Hora_Asistencia = hora,
                    Ficha = fichaId,
                    Programa = programa,
                    Competencias = competencia,
                    idInstructor = instructorId,
                    idAprendiz = aprendizId,
                    Nombres = nombres,
                    Apellidos = apellidos,
                    ImagenQR = uniqueId, // O el valor que corresponda
                    Tipo_Asistencia = true // Establecer el tipo de asistencia como "asistió" (true)
                };

                // Agregar el nuevo registro a la base de datos
                db.Registro_Asistencias_QR.Add(asistencia);
                db.SaveChanges();
            }

            return Json(new { success = true, message = "Registro de asistencia exitoso." });
        }
    }

}