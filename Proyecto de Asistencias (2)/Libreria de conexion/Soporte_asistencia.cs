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
    
    public partial class Soporte_asistencia
    {
        public int idSoporte { get; set; }
        public string Fecha_Soporte { get; set; }
        public string Hora_Soporte { get; set; }
        public Nullable<int> idAprendiz { get; set; }
    
        public virtual Aprendiz Aprendiz { get; set; }
    }
}
