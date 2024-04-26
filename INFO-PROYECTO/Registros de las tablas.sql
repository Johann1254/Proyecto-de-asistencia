--Registros 
--Registro Admin
insert into Administrador(idAdministrador, Nombre_Administrador, Apellido_Administrador, Email_Administrador, Contraseña_Administrador)
values(142, 'Johan Estibenson', 'Avendaño Cabrera', 'johanavencabrera@gmail.com', '12345JA')

--Registro de Aprendiz
insert into Aprendiz(Nombres_Aprenidiz, Apellidos_Aprendiz, Email_Aprenidiz, Tipo_Documento, Numero_Documento, Contraseña, Estado, Numero_Ficha, idAdministrador, idSoporte)
values('Brayan David', 'Marchena Urbina', 'marchena1902@gmail.com', 'TI', '1081910761', '12345Br@', 1,2693606, 142,1 )

--Registro de Instructor 
insert into Instructor(idInstructor, Nombre_Instructor, Apellido_Instructor, Email_Instructor, Contraseña_Instructor, Imagen_Qr, Numero_Ficha, idAdministrador)
values(214, 'Jesus Andres', 'Marzal Barboza', 'jesusandres1409@gmail.com', 'losguerreroszgoku', 'jpg', 2693606, 142)

--Registro de Ficha 
insert into Ficha(Numero_Ficha, Jornada_Ficha,Fecha_inicio, Fecha_fin, Tipo_Ficha, idAdministrador)
values(2693606, 'Mixta','23/01/2023 ','24/04/2025', 'Cerrada', 142)

--Registro de Programa de Formacion
insert into Programa_Formacion(idPrograma, Nombre_Programa, Duracion_Programa,Numero_Ficha, idAdministrador)
values(321, 'Analisis y Desarrollo de Software', '21 meses',2693606,142)

--Registro de Competencia 
insert into Competencia(idCompetencia, Nombre_Competencia,Tipo_Competencias, Duracion_Competencia, idAdministrador, idPrograma)
values(251, 'Diseñar modelado de los requisitos funcionales', 'Tecnica', '3 meses', 142,321)

--Registro de Reporte 
insert into Reporte(Fecha_Reporte, Hora_Reporte, idAsistencia)
values('26/04/2024', '12:29:00 pm',3)

--Registro de Registro de Asistencias QR
insert into Registro_Asistencias_QR(Fecha_Asistencia, Hora_Asistencia, Tipo_Asistencia, idAprendiz)
values('26/04/2023', '12:32:00 pm', 'Asistio', 1)

--Registro de Soporte de Asistencias 
insert into Soporte_asistencia(Fecha_Soporte, Hora_Soporte, Formato_Soporte)
values('26/04/2023', '12:38:00 pm', 'pdf')


--Agregar un atributo a una tabla 
update Programa_Formacion set Numero_Ficha = 2693606
---------------------------------------------------
delete from Soporte_asistencia
where Soporte_asistencia.idSoporte = 2
-------------------------------------------

select * from Instructor
select * from Administrador
select * from Aprendiz
select * from Competencia
select * from Programa_Formacion
select * from Ficha
select * from Soporte_asistencia
select * from Registro_Asistencias_QR
select * from Reporte
-----Consultas 
--Obtener todos los datos de los aprendices con sus respectivas fichas
select Aprendiz.idAprendiz, Aprendiz.Nombres_Aprenidiz, Ficha.Numero_Ficha
from Aprendiz
INNER JOIN Ficha on Aprendiz.Numero_Ficha = Ficha.Numero_Ficha

--Obtener los instructores y los programas de formacion a los que estan asociados 
select Instructor.Nombre_Instructor, Programa_Formacion.Nombre_Programa
from Instructor
INNER JOIN Ficha on Instructor.Numero_Ficha = Ficha.Numero_Ficha
INNER JOIN Programa_Formacion on Ficha.Numero_Ficha = Programa_Formacion.Numero_Ficha

--Obtener los aprendices que han asistido a una sesion especifica 
select Aprendiz.idAprendiz, Aprendiz.Nombres_Aprenidiz, Registro_Asistencias_QR.Fecha_Asistencia
from Aprendiz
INNER JOIN Registro_Asistencias_QR on Aprendiz.idAprendiz = Registro_Asistencias_QR.idAprendiz
where Registro_Asistencias_QR.Fecha_Asistencia = '26/04/2023'

--Obtener los reportes generados para una asistencia especifica 
select Reporte.idReporte, Reporte.Fecha_Reporte, Registro_Asistencias_QR.Fecha_Asistencia
from Reporte 
INNER JOIN Registro_Asistencias_QR on Reporte.idAsistencia = Registro_Asistencias_QR.idAsistencia
where Registro_Asistencias_QR.Fecha_Asistencia = '26/04/2023'

--Obtener las competencias de un programa en especifico 
select Competencia.Nombre_Competencia, Programa_Formacion.Nombre_Programa
from Competencia
INNER JOIN Programa_Formacion on Competencia.idPrograma = Programa_Formacion.idPrograma
where Programa_Formacion.Nombre_Programa = 'Analisis y Desarrollo de Software'
-------------------------------------------------------------------------------------------------------
--Obtener los administradores y los programas de formacion que han creado
select Administrador.Nombre_Administrador, Programa_Formacion.Nombre_Programa
from Administrador
INNER JOIN Programa_Formacion on Administrador.idAdministrador = Programa_Formacion.idAdministrador

--Obtener los aprendices y el soporte de asistencia que han recibido
select Aprendiz.Nombres_Aprenidiz, Soporte_asistencia.Fecha_Soporte, Soporte_asistencia.Hora_Soporte
from Aprendiz
INNER JOIN Soporte_asistencia on Aprendiz.idSoporte = Soporte_asistencia.idSoporte

--Obtener los instructores y los administradores a los que estan asociados 
select Instructor.Nombre_Instructor, Administrador.Nombre_Administrador
from Instructor
INNER JOIN Administrador on Instructor.idAdministrador = Administrador.idAdministrador

--Obtener los aprendices y los reportes generados para su asistencia 
select Aprendiz.Nombres_Aprenidiz, Reporte.Fecha_Reporte, Reporte.Hora_Reporte
from Aprendiz
INNER JOIN Registro_Asistencias_QR on Aprendiz.idAprendiz = Registro_Asistencias_QR.idAprendiz
INNER JOIN Reporte on Registro_Asistencias_QR.idAsistencia = Reporte.idAsistencia

--Obtener los programas de formacion y las competencias asociadas 
select Programa_Formacion.Nombre_Programa, Competencia.Nombre_Competencia
from Programa_Formacion
INNER JOIN  Competencia on Programa_Formacion.idPrograma = Competencia.idPrograma