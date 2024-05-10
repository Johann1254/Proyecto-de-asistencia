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
    
    public partial class Ficha
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ficha()
        {
            this.Aprendiz = new HashSet<Aprendiz>();
            this.Instructor = new HashSet<Instructor>();
            this.Programa_Formacion = new HashSet<Programa_Formacion>();
        }
    
        public int Numero_Ficha { get; set; }
        public string Jornada_Ficha { get; set; }
        public string Fecha_inicio { get; set; }
        public string Fecha_fin { get; set; }
        public string Tipo_Ficha { get; set; }
        public Nullable<int> idAdministrador { get; set; }
    
        public virtual Administrador Administrador { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aprendiz> Aprendiz { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Instructor> Instructor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programa_Formacion> Programa_Formacion { get; set; }
    }
}