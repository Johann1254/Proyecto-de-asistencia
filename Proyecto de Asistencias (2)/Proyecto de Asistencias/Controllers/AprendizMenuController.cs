using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion;
using System.IO;
using Libreria_de_conexion;
using System.Data; 
using System.Data.SqlClient; 

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
        public ActionResult Subirarchivo(string nombre, HttpPostedFileBase adjunto)
        {
            string[] tiposPermitidos = { ".jpg", ".png", ".pdf" };
            if (adjunto != null && adjunto.ContentLength > 0)
            {
                string extension = Path.GetExtension(adjunto.FileName).ToLower();
                if (tiposPermitidos != null && !tiposPermitidos.Contains(extension))
                {
                    //throw new ArgumentException("Tipo de archivo no permitido.");
                    TempData["Mensaje"] = "Tipo de archivo no permitido";
                }
                string dirSubida = Path.Combine/*(HttpContext.Current.Server.MapPath*/(@"C:\Users\USER\otros");
                string archivo = Path.Combine(dirSubida, Path.GetFileName(adjunto.FileName));

                // Create the directory if it doesn't exist
                if (!Directory.Exists(dirSubida))
                {
                    Directory.CreateDirectory(dirSubida);
                }

                // Save the uploaded file
                adjunto.SaveAs(archivo);

                // Do something with the uploaded file
                // ...
            }

            return View();
        }
      
     

        private AsistenciaEntities db = new AsistenciaEntities();

        public ActionResult Consultarasistencias()
        {

            return View(); 
        }
        public ActionResult Consultarinasistencias()
        {
            var idAprend = (int)Session["Aprendiz"]; 
            using (var db = new AsistenciaEntities())
            {

                var aprendices = db.Registro_Asistencias_QR.Where(a => a.Tipo_Asistencia == false).
                    ToList();

                db.Registro_Asistencias_QR.Where(s => s.idAprendiz == idAprend).ToList(); 


                ViewBag.Aprendiz = aprendices;

            }
            return View(); 
        }
       
    }
}