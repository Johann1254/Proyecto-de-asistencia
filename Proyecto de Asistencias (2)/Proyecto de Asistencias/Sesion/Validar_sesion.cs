using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_de_Asistencias.Sesion
{
    // Clase Validar_sesion que hereda de la clase ActionFilterAttribute 
    public class Validar_sesion : ActionFilterAttribute
    {
        // Método que se ejecuta antes de que se ejecute la acción del controlador
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Comprueba si las variables de sesión "Administrador", "Aprendiz" e "Instructor" son nulas
            if (HttpContext.Current.Session["Administrador"] == null && HttpContext.Current.Session["Aprendiz"] == null && HttpContext.Current.Session["Instructor"] == null)
            {
                // Si todas las variables de sesión son nulas, redirige al usuario a la página de inicio de sesión
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            // Llama al método OnActionExecuting de la clase base
            base.OnActionExecuting(filterContext);
        }

    }
}