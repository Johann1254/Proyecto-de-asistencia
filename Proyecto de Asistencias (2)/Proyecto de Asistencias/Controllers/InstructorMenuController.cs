using System;
using System.Linq;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion; // Asegúrate de tener la referencia correcta a tu modelo
using QRCoder;
using System.Drawing;
using System.IO;
using Libreria_de_conexion;
using System.Drawing.Imaging;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class InstructorMenuController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities();

        public ActionResult MenuprincipalInstructor() => View();
        public ActionResult VistaAsistencias() => View();
        public ActionResult VistaInasistencias() => View();
        public ActionResult VistacrearUsuarios() => View();
        public ActionResult ConsultarFichas() => View();
        public ActionResult VistaconsultarInasistencias() => View();
        public ActionResult Listadofichas() => View();
        public ActionResult ListadoAprendiCES() => View();
        public ActionResult ListadoProgramas() => View();
        public ActionResult ListadoCompetencias() => View();
        public ActionResult reporte() => View();

        public ActionResult QrAsistencias()
        {
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

        public ActionResult InasistenciasAprendiz() => View();
        public ActionResult ConsultarProgramas() => View();
        public ActionResult ConsultarCompetencias() => View();
        public ActionResult FormularioAsistencias() => View();

        [HttpGet]
        public ActionResult ObtenerCodigoQR(string fecha, int fichaId, string namecompe, string nameprog)
        {
            var ficha = db.Ficha.FirstOrDefault(f => f.Numero_Ficha == fichaId);
            var competencia = db.Competencia.FirstOrDefault(f => f.Nombre_Competencia == namecompe);
            var programa = db.Programa_Formacion.FirstOrDefault(f => f.Nombre_Programa == nameprog);

            if (ficha == null || competencia == null || programa == null)
            {
                return HttpNotFound();
            }

            string uniqueId = Guid.NewGuid().ToString();
            string url = Url.Action("FormularioAsistencias", "InstructorMenu", new { fecha, fichaId, nameprog, namecompe, id = uniqueId }, Request.Url.Scheme);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(3);

            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                byte[] byteArray = stream.ToArray();
                string base64String = Convert.ToBase64String(byteArray);

                // Almacenar en la sesión
                Session["QrCodeBase64"] = base64String;
                Session["Fecha"] = fecha;
                Session["FichaId"] = fichaId;
                Session["Competencia"] = namecompe;
                Session["Programa"] = nameprog;
                return Json(base64String, JsonRequestBehavior.AllowGet);


            }
        }
        [HttpPost]
        public ActionResult EliminarCodigoQR()
        {
            // Eliminar el QR de la sesión
            Session.Remove("QrCodeBase64");
            Session.Remove("Fecha");
            Session.Remove("FichaId");
            Session.Remove("Competencia");
            Session.Remove("Programa");

            return Json(new { success = true });
        }
    }
}
