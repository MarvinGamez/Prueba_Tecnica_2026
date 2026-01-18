## Descripción

Este repositorio contiene dos proyectos desarrollados como prueba tecnica profesional:

1. **LoginWebApp (.NET 8)**  
   Aplicacion web moderna para autenticacion de usuarios con login y registro.  
   - Utiliza **.NET 8** por su rendimiento, soporte a largo plazo y características modernas de desarrollo web.  
   - Conectada a SQL Server LocalDB **(localdb)\MSSQLLocalDB** para la base de datos de usuarios

2. **UsersCRUD.WinForms (Framework 4.8)**  
   Aplicacion de escritorio para gestion de usuarios (CRUD) 
   - Desarrollada con **Windows Forms y .NET Framework 4.8** por su estabilidad y compatibilidad probada en entornos corporativos  
   - Utiliza la misma base de datos que LoginWebApp, aprovechando procedimientos almacenados para operaciones seguras y eficientes

## Requisitos Previos

- Visual Studio
- .NET 8 SDK (para LoginWebApp)
- .NET 4.8 (para UsersCRUD.WinForms)
- SQL Server LocalDB **(localdb)\MSSQLLocalDB**

## Pasos para ejecutar cada proyecto

### LoginWebApp

1. Abrir **LoginWebApp.sln** en Visual Studio
2. Restaurar paquetes NuGet (clic derecho en la solucion y luego clic en "Restore NuGet Packages")
3. Ejecutar la aplicación.
4. La aplicación se abrira en el navegador usar los datos de prueba de la base de datos para login
	-El usuario con el que contara la base de datos es "admin" y su contraseña "admin2026"
	-Con esas credenciales podremos ingresar y comprobar el correcto funcionameno inmediatamente.
  
### UsersCRUD.WinForms

1. Abrir **UsersCRUD.WinForms.sln** en Visual Studio.
2. Ejecutar la aplicación.
3. La aplicación permite agregar, editar, eliminar y listar usuarios utilizando los procedimientos almacenados de la base de datos


## Base de Datos

Ambos proyectos utilizan la misma base de datos en SQL Server LocalDB **(localdb)\MSSQLLocalDB**

Para restaurar la base de datos y los procedimientos almacenados:

1. Abrir SQL Server Management Studio
2. Conectarse a **(localdb)\MSSQLLocalDB**
3. Ejecutar el script SQL ubicado en la carpeta llamada Scripts "UsersCRUDNET"
4. Este script cuenta con la base de datos, su tabla User, procedientos almacenados y su registro de administrador!
