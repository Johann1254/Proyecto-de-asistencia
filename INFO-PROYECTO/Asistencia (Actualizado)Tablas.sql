 --Crear tablas
 
 create table Administrador(
 idAdministrador int primary key not null, 
 Nombre_Administrador varchar(100) not null, 
 Apellido_Administrador varchar (100) not null, 
 Email_Administrador varchar(100) not null, 
 Contraseña_Administrador varchar(100) not null
 )

  create table Soporte_asistencia(
 idSoporte int primary key identity not null, 
 Fecha_Soporte varchar(100) not null, 
 Hora_Soporte varchar(100) not null,
 Formato_Soporte varchar(100) not null
 )

 create table Ficha(
 Numero_Ficha int primary key not null,
 idAdministrador int foreign key references Administrador 
 )

 create table Aprendiz (
 idAprendiz int identity primary key not null, 
 Nombres_Aprenidiz varchar(100) not null, 
 Apellidos_Aprendiz varchar(100) not null, 
 Email_Aprenidiz varchar(100) not null, 
 Tipo_Documento varchar(100) not null,
 Numero_Documento varchar(100) not null, 
 Contraseña varchar(100) not null,
 Estado bit default 1 not null, 
 Numero_Ficha int foreign key references Ficha,
 idAdministrador int foreign key references Administrador,
 idSoporte int foreign key references Soporte_asistencia

 )
 create table Instructor(
 idInstructor int primary key not null, 
 Nombre_Instructor varchar(100) not null, 
 Apellido_Instructor varchar(100) not null,
 Email_Instructor varchar(100) not null, 
 Contraseña_Instructor varchar(100) not null,
 Imagen_Qr text not null, 
 Numero_Ficha int foreign key references Ficha,
 idAdministrador int foreign key references Administrador 
 )

 create table Programa_Formacion(
 idPrograma int primary key not null,
 Nombre_Programa varchar(200) not null, 
 Duracion_Programa varchar(100) not null, 
 Numero_Ficha int foreign key references Ficha,
 idAdministrador int foreign key references Administrador,

 )
  create table Registro_Asistencias_QR(
 idAsistencia int primary key identity not null, 
 Fecha_Asistencia varchar(100) not null, 
 Hora_Asistencia varchar(100) not null, 
 Tipo_Asistencia varchar(100) not null,  
 idAprendiz int foreign key references Aprendiz 
 )

 create table Reporte(
 idReporte int primary key identity not null, 
 Fecha_Reporte varchar(100) not null, 
 Hora_Reporte varchar(100) not null,
 idAsistencia int foreign key references Registro_Asistencias_QR
 )

 
 create table Competencia(
 idCompetencia int primary key not null, 
 Nombre_Competencia varchar(200) not null, 
 Tipo_Competencias varchar(100) not null, 
 Duracion_Competencia varchar(100) not null,
 idAdministrador int foreign key references Administrador,
 idPrograma int foreign key references Programa_formacion
 )

