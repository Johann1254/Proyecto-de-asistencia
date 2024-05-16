using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion;
using System.Data.SqlClient;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int Instructores;
            string Conexion = "data source=DESKTOP-67BA9OT\\SQLEXPRESS;initial catalog=Asistencia;integrated security=True;multipleactiveresultsets=True;";

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                string query = " select count(*) from Instructor";

                SqlCommand comando = new SqlCommand(query, connection);
                connection.Open();
                Instructores = (int)comando.ExecuteScalar();

            }
            ViewBag.Total_Instructores = Instructores;
            int Aprendices;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                string query = "select count(*) from Aprendiz";
                connection.Open();
                SqlCommand comando = new SqlCommand(query, connection);
                Aprendices = (int)comando.ExecuteScalar();
            }
            ViewBag.Total_Aprendices = Aprendices;

            int Fichas;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                string query = "select count(*) from Ficha";
                connection.Open();
                SqlCommand comando = new SqlCommand(query, connection);
                Fichas = (int)comando.ExecuteScalar();
            }
            ViewBag.Total_Fichas = Fichas;

            int Programas;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                string query = "select count(*) from Programa_Formacion";
                connection.Open();
                SqlCommand comando = new SqlCommand(query, connection);
                Programas = (int)comando.ExecuteScalar();
            }
            ViewBag.Total_Programas = Programas;
            
            int Competencias;

            using (SqlConnection connection = new SqlConnection(Conexion))
            {
                string query = "select count(*) from Competencia";
                connection.Open();
                SqlCommand comando = new SqlCommand(query, connection);
                Competencias = (int)comando.ExecuteScalar();
            }
            ViewBag.Total_Competencias = Competencias;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CerrarSesion()
        {
            Session["Administrador"] = null;
            return RedirectToAction("Login", "Login"); //Redireccionar a la vista 
        }
    }
}