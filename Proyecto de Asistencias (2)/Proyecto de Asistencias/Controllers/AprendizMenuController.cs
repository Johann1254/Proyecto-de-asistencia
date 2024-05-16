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
        public ActionResult Subirarchivo()
        {
            return View(); 
        }
        public ActionResult Consultarasistencias()
        {
            return View(); 
        }
        public ActionResult Consultarinasistencias()
        {
            return View(); 
        }
        public ActionResult Archivosoporte()
        {
            return View(); 
        }
    }
}