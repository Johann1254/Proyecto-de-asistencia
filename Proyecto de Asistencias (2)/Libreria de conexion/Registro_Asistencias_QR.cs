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
    
    public partial class Registro_Asistencias_QR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Registro_Asistencias_QR()
        {
            this.Reporte = new HashSet<Reporte>();
        }
    
        public int idAsistencia { get; set; }
        public string Fecha_Asistencia { get; set; }
        public string Hora_Asistencia { get; set; }
        public Nullable<bool> Tipo_Asistencia { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string ImagenQR { get; set; }
        public string Competencias { get; set; }
        public Nullable<int> Ficha { get; set; }
        public string Programa { get; set; }
        public Nullable<int> idInstructor { get; set; }
        public Nullable<int> idAprendiz { get; set; }
    
        public virtual Aprendiz Aprendiz { get; set; }
        public virtual Instructor Instructor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Reporte> Reporte { get; set; }
    }
}
