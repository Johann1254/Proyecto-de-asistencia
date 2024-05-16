using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion;
using QRCoder;
using System.Drawing;
using System.IO;
namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class InstructorMenuController : Controller
    {
        // GET: InstructorMenu
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
        public ActionResult ObtenerCodigoQR(string fecha, string ficha)
        {
            // Generar un ID único
            string uniqueId = Guid.NewGuid().ToString();

            // URL de la vista a la que quieres que el QR redirija, incluyendo fecha, ficha e ID único
            string url = Url.Action("FormularioAsistencias", "InstructorMenu", new { fecha = fecha, ficha = ficha, id = uniqueId }, Request.Url.Scheme);

            // Crear instancia de QRCodeGenerator
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            // Crear Bitmap del código QR
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

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