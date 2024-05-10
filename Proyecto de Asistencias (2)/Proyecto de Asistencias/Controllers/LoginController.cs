﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Libreria_de_conexion;
using System.Data;
using System.Data.SqlClient; 

namespace Proyecto_de_Asistencias.Controllers
{
    public class LoginController : Controller
    {
        AsistenciaEntities Ae = new AsistenciaEntities();
        string cadena = "data source = DESKTOP-LSQ95PM; initial catalog = Asistencia; integrated security = true; multipleactiveresultsets=true;";
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Iniciar_sesion(Administrador usuario)
        {
            string tipoUsuario;
            int idUsuario;

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Email", usuario.Email_Administrador);
                cmd.Parameters.AddWithValue("Contraseña", usuario.Contraseña_Administrador);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        tipoUsuario = reader.GetString(0);
                        idUsuario = reader.GetInt32(1);
                    }
                    else
                    {
                        ViewData["mensaje"] = "Usuario no encontrado";
                        return View();
                    }
                }
            }

            switch (tipoUsuario)
            {
                case "Administrador":
                    Session["Administrador"] = idUsuario;
                    return RedirectToAction("Index", "Home"); // Redireccionar a la vista de administrador
                case "Aprendiz":
                    Session["Aprendiz"] = idUsuario;
                    return RedirectToAction("MenuprincipalAprendiz", "AprendizMenu"); // Redireccionar a la vista de aprendiz
                case "Instructor":
                    Session["Instructor"] = idUsuario;
                    return RedirectToAction("MenuprincipalInstructor", "InstructorMenu"); // Redireccionar a la vista de instructor
                default:
                    ViewData["mensaje"] = "Tipo de usuario no reconocido";
                    return View();
            }
        }

    }
}