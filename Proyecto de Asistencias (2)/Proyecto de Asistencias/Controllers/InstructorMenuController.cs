using System;
using System.Linq;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion; // Asegúrate de tener la referencia correcta a tu modelo
using QRCoder;
using System.Drawing;
using System.IO;
using Libreria_de_conexion;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class InstructorMenuController : Controller
    {
        private AsistenciaEntities db = new AsistenciaEntities(); // Usar tu contexto existente

        public ActionResult MenuprincipalInstructor()
        {
            return View();
        }

        public ActionResult VistaAsistencias()
        {
            return View();
        }

        public ActionResult VistaInasistencias()
        {
            return View();
        }

        public ActionResult VistacrearUsuarios()
        {
            return View();
        }

        public ActionResult ConsultarFichas()
        {
            return View();
        }

        public ActionResult VistaconsultarInasistencias()
        {
            return View();
        }

        public ActionResult Listadofichas()
        {
            return View();
        }

        public ActionResult ListadoAprendiCES()
        {
            return View();
        }

        public ActionResult ListadoProgramas()
        {
            return View();
        }

        public ActionResult ListadoCompetencias()
        {
            return View();
        }

        public ActionResult reporte()
        {
            return View();
        }

        public ActionResult QrAsistencias()
        {
            // Obtener las fichas desde la base de datos
            var fichas = db.Ficha.Select(f => new SelectListItem
            {
                Value = f.Numero_Ficha.ToString(),
                Text = f.Numero_Ficha.ToString()
            }).ToList();

            ViewBag.Fichas = fichas;
            return View();
        }

        public ActionResult InasistenciasAprendiz()
        {
            return View();
        }

        public ActionResult ConsultarProgramas()
        {
            return View();
        }

        public ActionResult ConsultarCompetencias()
        {
            return View();
        }

        public ActionResult FormularioAsistencias()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ObtenerCodigoQR(string fecha, int fichaId)
        {
            // Obtener la ficha desde la base de datos
            var ficha = db.Ficha.FirstOrDefault(f => f.Numero_Ficha == fichaId);
            if (ficha == null)
            {
                return HttpNotFound();
            }

            // Generar un ID único
            string uniqueId = Guid.NewGuid().ToString();

            // URL de la vista a la que quieres que el QR redirija, incluyendo fecha, ficha e ID único
            string url = Url.Action("FormularioAsistencias", "InstructorMenu", new { fecha = fecha, ficha = ficha.Numero_Ficha, id = uniqueId }, Request.Url.Scheme);

            // Crear instancia de QRCodeGenerator
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Crear Bitmap del código QR
            Bitmap qrCodeImage = qrCode.GetGraphic(5);

            // Convertir Bitmap a arreglo de bytes
            using (MemoryStream stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteArray = stream.ToArray();

                // Convertir el arreglo de bytes a una cadena base64
                string base64String = Convert.ToBase64String(byteArray);
                return Json(base64String, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
