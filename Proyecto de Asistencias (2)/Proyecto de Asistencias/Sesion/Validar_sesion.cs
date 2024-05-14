using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_de_Asistencias.Sesion
{
    public class Validar_sesion:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Administrador"] == null && HttpContext.Current.Session["Aprendiz"] == null && HttpContext.Current.Session["Instructor"] == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}