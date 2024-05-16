using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion;
using Libreria_de_conexion;
using System.Data.SqlClient;
using System.Data;
using Model.models; 
namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class InstructorMenuController : Controller
    {
        Aprendiz apr = new Aprendiz(); 
  
        
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


        public ActionResult Consultar_Aprendices()
        {
            string nombre = apr.Nombres_Aprenidiz;
            string apellido = apr.Apellidos_Aprendiz;

            List<Aprendiz> aprendices = new List<Aprendiz>();

            string query = "select Nombres_Aprenidiz, Apellido_Aprenidiz from Aprendiz";

            SqlCommand cmd = new SqlCommand(query);
           
            SqlDataAdapter reader = cmd.ExecuteReader(); 

            while (reader.Read())
            {
                Aprendiz.Add(new Aprendiz
                {
                    Nombres_Aprenidiz = reader["Nombre_Aprenidz"].ToString(),
                    Apellidos_Aprendiz = reader["Apellido_Aprenidiz"].ToString()
                }); 
            }

            return View(aprendices); 

        }
    }

   
}