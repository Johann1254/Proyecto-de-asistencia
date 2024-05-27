using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class AprendizMenuController : Controller
    {
        // GET: AprendizMenu
        public ActionResult MenuprincipalAprendiz()
        {
            return View();
        }
        public ActionResult MostrarQR()
        {
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
    }
}