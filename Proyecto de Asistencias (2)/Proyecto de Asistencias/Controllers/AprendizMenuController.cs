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
        public ActionResult RegistrarAsistencia(string fecha, string hora, int fichaId, string programa, string competencia, int instructorId, string uniqueId, string nombres, string apellidos)
        {
            using (var db = new AsistenciaEntities())
            {
                // Crear un nuevo registro de asistencia
                var asistencia = new Registro_Asistencias_QR
                {
                    Fecha_Asistencia = fecha,
                    Hora_Asistencia = hora,
                    Ficha = fichaId,
                    Programa = programa,
                    Competencias = competencia,
                    idInstructor = instructorId,
                    Nombres = nombres,
                    Apellidos = apellidos,
                    ImagenQR = uniqueId // O el valor que corresponda
                };

                // Agregar el nuevo registro a la base de datos
                db.Registro_Asistencias_QR.Add(asistencia);
                db.SaveChanges();
            }

            return Json(new { success = true });
        }
    }

}