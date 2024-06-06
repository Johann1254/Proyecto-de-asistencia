using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto_de_Asistencias.Sesion;
using Libreria_de_conexion;
using System.Data.SqlClient;
using System.Data;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class InstructorMenuController : Controller
    {
        Aprendiz apr = new Aprendiz();
        string cadena = "data source = DESKTOP-67BA9OT\\SQLEXPRESS; initial catalog = Asistencia; integrated security = true; multipleactiveresultsets=true;";

       
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
        private AsistenciaEntities db = new AsistenciaEntities(); 
        public ActionResult ListadoAprendices()
        {

            using (var db = new AsistenciaEntities())
            {

                var aprendices = db.Aprendiz.ToList(); 


               ViewBag.Aprendiz = aprendices;

            }
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
        public ActionResult ConsultarAprendices()
        {
            return View(); 
        }
       

        public ActionResult ConsultarA(int NumeroFicha, int IdCompetencia, int IdPrograma)
        {
            List<Aprendiz> aprendices = new List<Aprendiz>();

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                // Crea un comando SQL para ejecutar el procedimiento almacenado sp_ValidarUsuario 
                SqlCommand cmd = new SqlCommand("ConsultarAprendiz", cn);
                cmd.Parameters.AddWithValue("NumeroFicha", NumeroFicha);
                cmd.Parameters.AddWithValue("IdPrograma", IdPrograma);
                cmd.Parameters.AddWithValue("IdCompetencia", IdCompetencia);
                cmd.CommandType = CommandType.StoredProcedure;

                // Abre la conexión a la base de datos
                cn.Open();


                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Aprendiz aprendiz = new Aprendiz
                        {
                            Nombres_Aprenidiz = dr["Nombres_Aprenidiz"].ToString(),
                            Apellidos_Aprendiz = dr["Apellidos_Aprendiz"].ToString(),
                            Email_Aprenidiz = dr["Email_Aprenidiz"].ToString()
                        };
                        aprendices.Add(aprendiz);



                    }
                }

            }
            return View("ListadoAprendices", aprendices);
        }
        
    }

   
}