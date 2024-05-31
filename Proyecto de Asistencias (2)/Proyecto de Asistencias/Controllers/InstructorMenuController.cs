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

namespace Proyecto_de_Asistencias.Controllers
{
    public class InstructorMenuController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities();

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
    }
}
