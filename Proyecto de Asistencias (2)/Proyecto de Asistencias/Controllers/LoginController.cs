// Espacio de importacion de las librerias a utilizar 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria_de_conexion;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_de_Asistencias.Controllers
{
    // Clase LoginController que hereda de la clase Controller de ASP.NET MVC
    public class LoginController : Controller
    {
        // Instancia de AsistenciaEntities para interactuar con la base de datos
        AsistenciaEntities Ae = new AsistenciaEntities();

        // Cadena de conexión a la base de datos
        string cadena = "data source = JAMB\\SQLEXPRESS; initial catalog = Asistencia; integrated security = true; multipleactiveresultsets=true;";

        // Método para manejar la solicitud POST(enviar datos) para iniciar sesión
        [HttpPost]
        //  La instancia ("usuario")  de la tabla Administrador funciona como una variable general que permitira acceder a los email y contraseña de los roles. 
        public ActionResult Iniciar_sesion(Administrador usuario)
        {
            // Variables para almacenar el tipo de usuario y el ID del usuario
            string tipoUsuario;
            int idUsuario;
            bool? estado = null;  // Utiliza nullable para estado
            string nombreUsuario = "";
            string apellidoUsuario = "";

            // Se establece una conexión a la base de datos
            using (SqlConnection cn = new SqlConnection(cadena))
            {

                // Crea un comando SQL para ejecutar el procedimiento almacenado sp_ValidarUsuario 
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Email", usuario.Email_Administrador);
                cmd.Parameters.AddWithValue("Contraseña", usuario.Contraseña_Administrador);
                cmd.CommandType = CommandType.StoredProcedure;

                // Abre la conexión a la base de datos
                cn.Open();

                // Ejecuta el comando y obtiene los resultados
                using (SqlDataReader reader = cmd.ExecuteReader())
                {

                    // Si el usuario es válido, obtiene el tipo de usuario y el ID del usuario
                    if (reader.HasRows)
                    {
                        reader.Read();
                        tipoUsuario = reader.GetString(0);
                        // si no se cuentra ningun tipo de usuario se muestre un mensaje de usuario no existente
                        if (tipoUsuario == "0")
                        {
                            ViewData["Mensaje"] = "USUARIO NO EXISTENTE";
                            return View("Login");
                        }
                        // si el correo esta registrado en la BD pero la contraseña no coincide muestre el mensaje de CONTRASEÑA INCORRECTA
                        if (tipoUsuario == "-1")
                        {
                            ViewData["Mensaje"] = "CONTRASEÑA INCORRECTA";
                            return View("Login");
                        }
                        // si el aprendiz su estado esta como 0 que indica retirado no permita acceder a la vista del aprendiz y muestre el mensaje de aprendiz retirado
                        idUsuario = reader.GetInt32(1);
                        if (tipoUsuario == "Aprendiz")
                        {
                            nombreUsuario = reader.GetString(3);
                            apellidoUsuario = reader.GetString(4);
                            estado = reader.GetBoolean(2);  // Lee el estado solo si es Aprendiz
                            if (estado == false)
                            {
                                ViewData["Mensaje"] = "APRENDIZ RETIRADO";
                                return View("Login");
                            }
                            
                        }
                        else if (tipoUsuario == "Instructor")
                        {
                            nombreUsuario = reader.GetString(3); // Index 3 para nombre
                            apellidoUsuario = reader.GetString(4); // Index 4 para apellido
                        }

                    }
                    // Si no hay resultados, devuelve a la vista login
                    else
                    {
                        return View("Login");
                    }
                }
            }

            // Dependiendo del tipo de usuario, establece una variable de sesión y redirige al usuario a la vista correspondiente
            //!El switch sirve para evaluar una expresion que en este caso es ("tipoUsuario") para luego seleccionar una de las ramas que en este caso son las "case" basadas en su valor!

            switch (tipoUsuario)
            {
                // Si tipoUsuario es "Administrador"
                case "Administrador":
                    Session["Administrador"] = idUsuario;
                    return RedirectToAction("Index", "Home"); // Redireccionar a la vista de administrador

                // Si tipoUsuario es "Aprendiz",
                case "Aprendiz":
                    Session["Aprendiz"] = idUsuario;
                    Session["NombreUsuario"] = nombreUsuario;
                    Session["ApellidoUsuario"] = apellidoUsuario;
                    return RedirectToAction("MenuprincipalAprendiz", "AprendizMenu"); // Redireccionar a la vista de aprendiz

                // Si tipoUsuario es "Instructor",
                case "Instructor":
                    Session["Instructor"] = idUsuario;
                    Session["NombreUsuario"] = nombreUsuario;
                    Session["ApellidoUsuario"] = apellidoUsuario;
                    return RedirectToAction("MenuprincipalInstructor", "InstructorMenu"); // Redireccionar a la vista de instructor

                default:
                    return View();
            }
        }
        // Método para manejar la solicitud GET(obtener datos) para la página de inicio de sesión
        public ActionResult Login()
        {
            // Devuelve la vista de inicio de sesión
            return View();
        }
    }
}