# API para el Servicio de Asesoramiento Psicológico de la UCV

Desarrollo de una API REST para una página web para consultas y asesoramiento psicológico.

## Herramintas utilizadas

- .NET Core 7.0 
- SQL Server Express y Microsoft SQL Server Management Studio 20

## Ejecución local

### Base de datos

Primeramente, al iniciar Microsoft SQL Server Management Studio, se debe conectar con un servidor
usando la opción de "SQL Server Authentication", marcando el usuario y su contraseña.

Para hacer esto, primero se debe crear el login del usuario. Primero se inicia con "Windows Authetication"
usando la instancia "localhost\SQLEXPRESS", como se muestra en la siguiente imagen:

![Parámetros para Windows Authentication](BackendSAP/Images/WindowAuthentication.png)

Luego, se crea el usuario con la contraseña y los siguientes parámetros:

![Parámetros para crear usuario](BackendSAP/Images/createLogin.png)
![Rol del usuario en el servidor](BackendSAP/Images/serverRole.png)
![Permisos de usuario](BackendSAP/Images/serverRole.png)

Posteriormente, con click derecho al nombre del servidor, se selecciona la opción de "Properties" y
y se cambia el modo de autenticaión a "SQL Server and Windows Authetication mode", como se muestra a continuación:

![Cambiar modo de autenticación](BackendSAP/Images/authenticationMode.png)

Finalmente, se reinicia el servidor con la opción "restart" y se conecta con el usuario creado.

![Conexión con usuario creado](BackendSAP/Images/sqlServerAuthentication.png)




