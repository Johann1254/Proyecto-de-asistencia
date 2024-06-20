using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria_de_conexion;
using Proyecto_de_Asistencias.Sesion;

namespace Proyecto_de_Asistencias.Controllers
{
    [Validar_sesion]
    public class AprendizMenuController : Controller
    {
        string conexion = "data source = JAMB\\SQLEXPRESS; initial catalog = Asistencia; integrated security = true; multipleactiveresultsets=true;";


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
            var idAprend = (int)Session["Aprendiz"];
            using (var db = new AsistenciaEntities())
            {

                var aprendices = db.Registro_Asistencias_QR.Where(a => a.idAprendiz == idAprend).Where(a => a.Tipo_Asistencia == true).
                    ToList();
                ViewBag.Aprendiz = aprendices;

            }

            return View(); 
        }
        public ActionResult Consultarinasistencias()
        {
            var idAprend = (int)Session["Aprendiz"];
            using (var db = new AsistenciaEntities())
            {

                var aprendices = db.Registro_Asistencias_QR.Where(a => a.idAprendiz == idAprend).Where(a => a.Tipo_Asistencia == false).
                    ToList();
                db.Registro_Asistencias_QR.Where(a => a.Tipo_Asistencia == false); ViewBag.Aprendiz = aprendices;

            }

            return View(); 
        }
        public ActionResult Archivosoporte()
        {
            return View(); 
        }
        public ActionResult VistaQR()
        {
            return View(); 
        }
        [HttpPost]
        public ActionResult Cargar(HttpPostedFileBase file, int? idAsistencia)
        {
            try
            {
                // Verificar que se haya seleccionado un archivo
                if (file == null || file.ContentLength == 0)
                {
                    TempData["ErrorMessage"] = "Por favor, seleccione un archivo a subir";
                    return RedirectToAction("SubirArchivo");
                }

                // Verificar que se haya ingresado un ID de inasistencia válido
                if (idAsistencia == null || idAsistencia <= 0)
                {
                    TempData["AlertMessage"] = "Por favor, ingrese un ID de inasistencia válido";
                    return RedirectToAction("SubirArchivo");
                }

                // Obtener la extensión del archivo que se ha cargado
                string Extension = Path.GetExtension(file.FileName);

                // Obtener el id del Aprendiz que inició sesión 
                var idAprendiz = (int)Session["Aprendiz"];

                // Verificar si la inasistencia existe y no ha pasado el tiempo límite
                if (!VerificarInasistenciaValida(idAsistencia.Value))
                {
                    TempData["AlertMessage"] = "La inasistencia no es válida o ha pasado el tiempo límite para subir el archivo.";
                    return RedirectToAction("SubirArchivo");
                }

                // Verificar si el ID de la inasistencia coincide con los registros existentes
                if (!VerificarExistenciaInasistencia(idAsistencia.Value))
                {
                    TempData["AlertMessage"] = "El ID de la inasistencia no coincide con los registros existentes.";
                    return RedirectToAction("SubirArchivo");
                }

                // Verificar si ya existe un archivo de soporte para esta inasistencia
                if (!VerificarArchivoSoporte(idAsistencia.Value))
                {
                    TempData["AlertMessage"] = "Ya se ha subido un archivo de soporte para esta inasistencia.";
                    return RedirectToAction("SubirArchivo");
                }

                // Crear un MemoryStream para copiar el contenido del archivo subido
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] data = ms.ToArray();

                    // Insertar el archivo en la base de datos
                    using (SqlConnection connection = new SqlConnection(conexion))
                    {
                        SqlCommand cmd = new SqlCommand(@"INSERT INTO Soporte_asistencia (Archivo_soporte, idAprendiz, Extension, idAsistencia) 
                        VALUES (@Archivo_soporte, @idAprendiz, @Extension, @idAsistencia);", connection);

                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Archivo_soporte", data);
                        cmd.Parameters.AddWithValue("@idAprendiz", idAprendiz);
                        cmd.Parameters.AddWithValue("@Extension", Extension);
                        cmd.Parameters.AddWithValue("@idAsistencia", idAsistencia.Value);

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        TempData["Mensaje"] = "Archivo Subido Correctamente";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AlertMessage"] = "Error al subir el archivo: " + ex.Message;
            }

            return RedirectToAction("SubirArchivo");
        }

        // Método para verificar si la inasistencia es válida y no ha pasado el tiempo límite
        private bool VerificarInasistenciaValida(int idAsistencia)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) 
                                                  FROM Registro_Asistencias_QR
                                                  WHERE idAsistencia = @idAsistencia 
                                                  AND Fecha_Asistencia >= DATEADD(day, -3, GETDATE())", connection);
                    cmd.Parameters.AddWithValue("@idAsistencia", idAsistencia);
                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Método para verificar si ya existe un archivo de soporte para la inasistencia
        private bool VerificarArchivoSoporte(int idAsistencia)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) 
                                                  FROM Soporte_asistencia 
                                                  WHERE idAsistencia = @idAsistencia", connection);
                    cmd.Parameters.AddWithValue("@idAsistencia", idAsistencia);
                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count == 0; // Retorna true si no hay archivos de soporte para esta inasistencia
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        // Método para verificar si el ID de la inasistencia existe en la base de datos
        private bool VerificarExistenciaInasistencia(int idAsistencia)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) 
                                                  FROM Registro_Asistencias_QR
                                                  WHERE idAsistencia = @idAsistencia", connection);
                    cmd.Parameters.AddWithValue("@idAsistencia", idAsistencia);
                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count == 1; // Retorna true si el ID de la inasistencia existe en la base de datos
                }
            }
            catch (Exception)
            {
                return false;
            }
        }




    }
}