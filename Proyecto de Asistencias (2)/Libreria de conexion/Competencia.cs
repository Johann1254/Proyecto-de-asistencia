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
    
    public partial class Competencia
    {
        public int idCompetencia { get; set; }
        public string Nombre_Competencia { get; set; }
        public string Tipo_Competencias { get; set; }
        public string Duracion_Competencia { get; set; }
        public Nullable<int> idAdministrador { get; set; }
        public Nullable<int> idPrograma { get; set; }
        public Nullable<int> idInstructor { get; set; }
    
        public virtual Administrador Administrador { get; set; }
        public virtual Instructor Instructor { get; set; }
        public virtual Programa_Formacion Programa_Formacion { get; set; }
    }
}
