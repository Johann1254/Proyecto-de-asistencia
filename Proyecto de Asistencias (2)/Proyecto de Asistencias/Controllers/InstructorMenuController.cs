using System;
using System.Linq;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion; // Asegúrate de tener la referencia correcta a tu modelo
using QRCoder;
using System.Drawing;
using System.IO;
using Libreria_de_conexion;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
namespace Proyecto_de_Asistencias.Controllers
{
    public class InstructorMenuController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities();

        Aprendiz apr = new Aprendiz(); 
  
        
        // GET: InstructorMenu
        public ActionResult MenuprincipalInstructor()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult VistaAsistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult VistaInasistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult VistacrearUsuarios()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult ConsultarFichas()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult VistaconsultarInasistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult Listadofichas()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult ListadoAprendiCES()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult ListadoProgramas()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult ListadoCompetencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult reporte()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult InasistenciasAprendiz()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult ConsultarProgramas()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult ConsultarCompetencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult FormularioAsistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;
            return View();
        }

        public ActionResult QrAsistencias()
        {
            int idUsuario = (int)Session["Usuarios"];
            ViewBag.IdUsuario = idUsuario;

            ViewBag.Fichas = db.Ficha.Select(f => new SelectListItem
            {
                Value = f.Numero_Ficha.ToString(),
                Text = f.Numero_Ficha.ToString()
            }).ToList();

            ViewBag.Programa_Formacion = db.Programa_Formacion.Select(f => new SelectListItem
            {
                Value = f.Nombre_Programa.ToString(),
                Text = f.Nombre_Programa.ToString()
            }).ToList();

            ViewBag.Competencia = db.Competencia.Select(f => new SelectListItem
            {
                Value = f.Nombre_Competencia.ToString(),
                Text = f.Nombre_Competencia.ToString()
            }).ToList();

            return View();
        }

        public ActionResult RegistroInasistencia()
        {
            // Obtiene todas las fichas de la base de datos y las convierte en una lista de elementos SelectListItem.
            // Estos elementos se utilizan para llenar un menú desplegable en la vista.
            ViewBag.Fichas = db.Ficha.Select(f => new SelectListItem
            {
                Value = f.Numero_Ficha.ToString(),
                Text = f.Numero_Ficha.ToString()
            }).ToList();

            ViewBag.Programa_Formacion = db.Programa_Formacion.Select(f => new SelectListItem
            {
                Value = f.Nombre_Programa.ToString(),
                Text = f.Nombre_Programa.ToString()
            }).ToList();

            ViewBag.Competencia = db.Competencia.Select(f => new SelectListItem
            {
                Value = f.Nombre_Competencia.ToString(),
                Text = f.Nombre_Competencia.ToString()
            }).ToList();

            return View();
        }

        [HttpGet]
        public ActionResult ObtenerCodigoQR(string fecha, int fichaId, string namecompe, string nameprog, int instructorId)
        {
            var ficha = db.Ficha.FirstOrDefault(f => f.Numero_Ficha == fichaId);
            var competencia = db.Competencia.FirstOrDefault(f => f.Nombre_Competencia == namecompe);
            var programa = db.Programa_Formacion.FirstOrDefault(f => f.Nombre_Programa == nameprog);

            if (ficha == null || competencia == null || programa == null)
            {
                return HttpNotFound();
            }

            // Crear un ID único para cada aprendiz
            var aprendices = db.Aprendiz.Where(a => a.Numero_Ficha == fichaId).ToList();
            var qrs = new List<object>();

            foreach (var aprendiz in aprendices)
            {
                string uniqueId = Guid.NewGuid().ToString();

                // Crear la URL del QR para el aprendiz
                string url = Url.Action("RegistrarAsistencia", "AprendizMenu",
                    new { fecha, fichaId, nameprog, namecompe, hora = DateTime.Now.ToString("HH:mm:ss"), instructorId, uniqueId, aprendizId = aprendiz.idAprendiz },
                    Request.Url.Scheme);

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(3);

                using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, ImageFormat.Png);
                    byte[] byteArray = stream.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                    qrs.Add(base64String);
                }
            }

            // Almacenar los QRs en la sesión
            Session["QrCodes"] = qrs;
            Session["Fecha"] = fecha;
            Session["Hora"] = DateTime.Now.ToString("HH:mm:ss");
            Session["FichaId"] = fichaId;
            Session["Competencia"] = namecompe;
            Session["Programa"] = nameprog;
            Session["InstructorId"] = instructorId;

            return Json(qrs, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult EliminarCodigoQR()
        {
            // Marcar que el QR ha sido eliminado
            Session["QrEliminado"] = true;

            // Eliminar el QR de la sesión
            Session.Remove("QrCodes");
            Session.Remove("Fecha");
            Session.Remove("Hora");
            Session.Remove("FichaId");
            Session.Remove("Competencia");
            Session.Remove("Programa");

            return Json(new { success = true });
        }


        public ActionResult GetProgramasPorFicha(int fichaId)
        {

            // Este método se encarga de obtener los programas asociados a una ficha específica.
            // Obtiene todas las fichas de la base de datos que coinciden con el id de la ficha proporcionado.
            // Luego, selecciona el id y el nombre del programa asociado a cada ficha.
            // Finalmente, elimina los duplicados y convierte el resultado en una lista.
            var programas = db.Ficha
                              .Where(f => f.Numero_Ficha == fichaId)
                              .Select(f => new {
                                  f.Programa_Formacion.idPrograma,
                                  f.Programa_Formacion.Nombre_Programa
                              })
                              .Distinct()
                              .ToList();

            // Retorna los programas como un objeto JSON.
            return Json(programas, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCompetenciasPorPrograma(int programaId)
        {
            // Este método se encarga de obtener las competencias asociadas a un programa específico.
            // Obtiene todas las competencias de la base de datos que coinciden con el id del programa proporcionado.
            // Luego, selecciona el id y el nombre de cada competencia.
            // Finalmente, convierte el resultado en una lista.
            var competencias = db.Competencia
                                 .Where(c => c.idPrograma == programaId)
                                 .Select(c => new { c.idCompetencia, c.Nombre_Competencia })
                                 .ToList();
            // Retorna las competencias como un objeto JSON.
            return Json(competencias, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAprendicesPorFicha(int fichaId)
        {
            // Este método se encarga de obtener los aprendices asociados a una ficha específica.
            // Obtiene todos los aprendices de la base de datos que coinciden con el número de ficha proporcionado.
            // Luego, selecciona el id y el nombre completo (nombres y apellidos) de cada aprendiz.
            // Finalmente, convierte el resultado en una lista.
            var aprendices = db.Aprendiz
                .Where(a => a.Numero_Ficha == fichaId)
                .Select(a => new
                {
                    a.idAprendiz,
                    NombreCompleto = a.Nombres_Aprenidiz + " " + a.Apellidos_Aprendiz
                })
                .ToList();
            // Retorna los aprendices como un objeto JSON.
            return Json(aprendices, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RegistrarInasistencia(int fichaId, int programaId, int competenciaId, int aprendizId, string fecha, string hora)
        {
            try
            {
                // Obtener el id del instructor desde la sesión
                var idInstructor = Session["Instructor"];
                // Si el instructor no está autenticado, se devuelve un mensaje de error
                if (idInstructor == null)
                {
                    return Json(new { success = false, message = "Instructor no autenticado" });
                }

                // Busca al aprendiz en la base de datos usando su id
                var aprendiz = db.Aprendiz.FirstOrDefault(a => a.idAprendiz == aprendizId);
                // Si el aprendiz no se encuentra, se devuelve un mensaje de error
                if (aprendiz == null)
                {
                    return Json(new { success = false, message = "Aprendiz no encontrado" });
                }

                // Crea un nuevo registro de asistencia
                Registro_Asistencias_QR registro = new Registro_Asistencias_QR
                {
                    Fecha_Asistencia = fecha, // Fecha de la asistencia
                    Hora_Asistencia = hora, // Hora de la asistencia
                    Tipo_Asistencia = false, // Inasistencia
                    Ficha = fichaId, // Id de la ficha
                    Programa = db.Programa_Formacion.Where(p => p.idPrograma == programaId).Select(p => p.Nombre_Programa).FirstOrDefault(), // Nombre del programa
                    Competencias = competenciaId.ToString(), // Id de la competencia convertido a string
                    idAprendiz = aprendizId, // Id del aprendiz
                    Nombres = aprendiz.Nombres_Aprenidiz, // Nombres del aprendiz
                    Apellidos = aprendiz.Apellidos_Aprendiz, // Apellidos del aprendiz
                    idInstructor = (int)idInstructor // Id del instructor
                };

                // Agrega el registro a la base de datos
                db.Registro_Asistencias_QR.Add(registro);
                // Guarda los cambios en la base de datos
                db.SaveChanges();

                // Si todo sale bien, se devuelve un mensaje de éxito
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Si ocurre un error, se devuelve un mensaje con el error
                return Json(new { success = false, message = ex.Message });
            }
        }
        public ActionResult ConsultarInasistencias()
        {
            // Este método se encarga de preparar la vista para consultar inasistencias.
            // Obtiene todas las fichas de la base de datos y las convierte en una lista de elementos SelectListItem.
            // Estos elementos se utilizan para llenar un menú desplegable en la vista.
            ViewBag.Fichas = db.Ficha.Select(f => new SelectListItem
            {
                Value = f.Numero_Ficha.ToString(),
                Text = f.Numero_Ficha.ToString()
            }).ToList();

            // Retorna la vista.
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerInasistencias(int? fichaId, int? programaId, int? competenciaId)
        {
            // Este método se encarga de obtener las inasistencias basándose en los parámetros proporcionados.
            // Los parámetros pueden ser null, en cuyo caso no se utilizan para filtrar las inasistencias.

            // Obtiene el id del instructor de la sesión.
            var idInstructor = (int?)Session["Instructor"];

            // Si el id del instructor es null, significa que el instructor no está autenticado.
            // En este caso, se retorna un objeto JSON con un mensaje de error.
            if (idInstructor == null)
            {
                return Json(new { success = false, message = "Instructor no autenticado" }, JsonRequestBehavior.AllowGet);
            }

            // Obtiene las inasistencias de la base de datos que coinciden con los parámetros proporcionados.
            var inasistencias = db.Registro_Asistencias_QR
                                  // La función .Where() se utiliza para filtrar los registros de asistencia.
                                  // Si no se proporcionó un fichaId, entonces esta condición es verdadera por defecto. 
                                  // Si se proporcionó un fichaId, entonces el Ficha del registro debe ser igual a fichaId.Value.
                                  .Where(r => (!fichaId.HasValue || r.Ficha == fichaId.Value) &&
                                              // igaul a la condición anterior, pero para el programaId. 
                                              // Si se proporcionó un programaId, entonces el Programa del registro debe ser igual al
                                              // Nombre_Programa del programa con el idPrograma igual a programaId.Value.
                                              (!programaId.HasValue || r.Programa == db.Programa_Formacion
                                                                           .Where(p => p.idPrograma == programaId.Value)
                                                                           .Select(p => p.Nombre_Programa)
                                                                           .FirstOrDefault()) &&
                                              // igual a las condiciones anteriores, pero para el competenciaId. 
                                              // Si se proporcionó un competenciaId, entonces las Competencias del registro deben ser iguales a competenciaId.Value.ToString().
                                              (!competenciaId.HasValue || r.Competencias == competenciaId.Value.ToString()) &&
                                              // El idInstructor del registro debe ser igual al idInstructor obtenido de la sesión.
                                              r.idInstructor == idInstructor &&
                                              // El Tipo_Asistencia del registro debe ser false, lo que indica que es una inasistencia.
                                              r.Tipo_Asistencia == false)
                                  // La función .Select() se utiliza para seleccionar ciertos campos de cada registro que cumple con las condiciones. 
                                  // En este caso, se seleccionan los campos Nombres, Apellidos, Fecha_Asistencia y Ficha.
                                  .Select(r => new
                                  {
                                      Nombre = r.Nombres,
                                      Apellidos = r.Apellidos,
                                      Fecha = r.Fecha_Asistencia,
                                      Ficha = r.Ficha
                                  })
                                  // Finalmente, la función .ToList() se utiliza para convertir el resultado en una lista.
                                  .ToList();

            // Retorna las inasistencias como un objeto JSON.
            return Json(inasistencias, JsonRequestBehavior.AllowGet);
        }







    }
}
