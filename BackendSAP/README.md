# API para el Servicio de Asesoramiento Psicol�gico de la UCV

Desarrollo de una API REST para una p�gina web para consultas y asesoramiento psicol�gico.

## Herramintas utilizadas

- .NET Core 7.0 
- SQL Server Express y Microsoft SQL Server Management Studio 20

## Ejecuci�n local

### Base de datos

Primeramente, al iniciar Microsoft SQL Server Management Studio, se debe conectar con un servidor
usando la opci�n de "SQL Server Authentication", marcando el usuario y su contrase�a.

Para hacer esto, primero se debe crear el login del usuario. Primero se inicia con "Windows Authetication"
usando la instancia "localhost\SQLEXPRESS", como se muestra en la siguiente imagen:

![Par�metros para Windows Authentication](BackendSAP/Images/WindowAuthentication.png)

Luego, se crea el usuario con la contrase�a y los siguientes par�metros:

![Par�metros para crear usuario](BackendSAP/Images/createLogin.png)
![Rol del usuario en el servidor](BackendSAP/Images/serverRole.png)
![Permisos de usuario](BackendSAP/Images/serverRole.png)

Posteriormente, con click derecho al nombre del servidor, se selecciona la opci�n de "Properties" y
y se cambia el modo de autenticai�n a "SQL Server and Windows Authetication mode", como se muestra a continuaci�n:

![Cambiar modo de autenticaci�n](BackendSAP/Images/authenticationMode.png)

Finalmente, se reinicia el servidor con la opci�n "restart" y se conecta con el usuario creado.

![Conexi�n con usuario creado](BackendSAP/Images/sqlServerAuthentication.png)




