﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AsistenciaEntities : DbContext
    {
        public AsistenciaEntities()
            : base("name=AsistenciaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Administrador> Administrador { get; set; }
        public virtual DbSet<Aprendiz> Aprendiz { get; set; }
        public virtual DbSet<Competencia> Competencia { get; set; }
        public virtual DbSet<Ficha> Ficha { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        public virtual DbSet<Programa_Formacion> Programa_Formacion { get; set; }
        public virtual DbSet<Registro_Asistencias_QR> Registro_Asistencias_QR { get; set; }
        public virtual DbSet<Reporte> Reporte { get; set; }
        public virtual DbSet<Soporte_asistencia> Soporte_asistencia { get; set; }
    }
}