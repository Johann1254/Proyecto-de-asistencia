//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Libreria_de_conexion
{
    using System;
    using System.Collections.Generic;
    
    public partial class Instructor
    {
        public int idInstructor { get; set; }
        public string Nombre_Instructor { get; set; }
        public string Apellido_Instructor { get; set; }
        public string Email_Instructor { get; set; }
        public string Contraseña_Instructor { get; set; }
        public string Imagen_Qr { get; set; }
        public Nullable<int> Numero_Ficha { get; set; }
        public Nullable<int> idAdministrador { get; set; }
    
        public virtual Administrador Administrador { get; set; }
        public virtual Ficha Ficha { get; set; }
    }
}
