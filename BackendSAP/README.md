# Servicio Comunitario-API para el Servicio de Asesoramiento Psicol�gico de la UCV

Desarrollo de una API REST para una p�gina web para consultas y asesoramiento psicol�gico.

## Herramintas utilizadas

- .NET Core 7.0 
- SQL Server Express y Microsoft SQL Server Management Studio 20

## Ejecuci�n local

### Base de datos

Primeramente, al iniciar Microsoft SQL Server Management Studio, se debe conectar con un servidor usando la opci�n de "SQL Server Authentication", marcando el usuario y su contrase�a.

Para hacer esto, primero se debe crear el login del usuario. Primero se inicia con "Windows Authetication" usando la instancia "localhost\SQLEXPRESS", como se muestra en la siguiente imagen:

![Par�metros para Windows Authentication](BackendSAP/../Images/WindowAuthentication.png)

Luego, se crea el usuario con la contrase�a y los siguientes par�metros:

![Par�metros para crear usuario](BackendSAP/../Images/createLogin1.png)
![Rol del usuario en el servidor](BackendSAP/../Images/serverRole.png)
![Permisos de usuario](BackendSAP/../Images/permisosUsuario.png)

Posteriormente, con click derecho al nombre del servidor, se selecciona la opci�n de "Properties" y se cambia el modo de autenticai�n a "SQL Server and Windows Authetication mode", como se muestra a continuaci�n:

![Cambiar modo de autenticaci�n](BackendSAP/../Images/authenticationMode.png)

Finalmente, se reinicia el servidor con la opci�n "restart" y se conecta con el usuario creado.

![Conexi�n con usuario creado](BackendSAP/../Images/sqlServerAuthentication1.png)

Por �ltimo, al generar la conexi�n, se crea la base de datos y se le coloca el nombre de preferencia.

### Dependencias

Antes de ejecutar las migraciones y dem�s funcionalidades, es necesario descargar todas las dependencias que aparecen en la siguiente imagen:

![Dependencias del Proyecto](BackendSAP/../Images/dependencias.png)

Estos paquetes se obtienen en la secci�n de "Herramientas", justamente en la opci�n "Administrar paquetes NuGet", se selecciona la opci�n "Administrar paquetes NuGet para la soluci�n..." y a paritr de ah� se descarga cada una de las dependencias con sus versiones exactas.

Nota: Es importante que las versiones sean iguales en cada una de las dependencias mostradas en la imagen anterior, en caso contrario, el proyeco puede fallar.

### Archivo de configuraci�n

El archivo appsentings.json debe tener el siguiente formato para realizar la conexi�n con la base de datos y generar la clave secreta para los tokens:

```bash

{
    "ApiSettings": {
        "Secreta": "LoQueQuierasColocarSiempreYCuandoTengaNumerosYLetras12345-="
    },
    "ConnectionStrings": {
        "ConexionSql": "Server=localhost\\SQLEXPRESS;Database=nombre_bd;User ID=nombre_usuario;Password=clave_bd;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*"
}

```

### Migraciones

1. Primeramente, en la secci�n de "Herramientas", justamente en la opci�n "Administrar paquetes NuGet", se selecciona la opci�n "Consola del Admninistrador de Paquetes", y en la consola desplegada se debe escribir el comando:

```bash
update-database

```

2. Si todos los pasos anteriores se realizan de forma correcta, la consola mostrar� un mensaje de �xito y la base de datos ya deber�a tener todas las tablas creadas.

![Migraci�n](BackendSAP/../Images/migracion.png)

![Modelo E-R](BackendSAP/../Images/modeloER.png)

### Ejecuci�n

Al terminar con los pasos anteriores, se ejecuta desde el framework la API, el cual deber�a aparecer de la siguiente manera:

![Vista API](BackendSAP/../Images/vistaAPI.png)

Se debe tener en cuenta las siguientes condiciones para ejecutar los endpoints:

1. Para iniciar, se deben crear algunos estados y ciudades de Venezuela para crer usuarios.

2. Hay algunos endpoints que, para ser ejecutados, se debe registrar un usuario e iniciar sesi�n (en ese orden). Se debe recordar que existen tres roles de usuario en esta aplicaci�n:

2.1) admin: Control total para registrar, actualizar y borrar en cualquier controlador presente en la API. �l es quien se encarga de confirmar si un psic�logo es qui�n dice ser (verificado) y de registrar los trastornos psicol�gicos existentes.
2.2) psicologo: Puede registrarse, iniciar sesi�n y actualizar su perfil, colocando su informaci�n de contacto y profesional para recibir consultas de cualquier persona. Tambi�n pueden calificar a otros psic�logos y vincular alg�n trastorno, indicando su especialidad para tratar dicho problema.
2.3) usuario: Puede registrarse, iniciar sesi�n y actualizar su perfil, al igual que calificar a alg�n psic�logo si as� lo desea.

Al registrar un usuario y e inicar sesi�n, se debe mostrar un output con el token asociado a ese usuario (el cual tiene una vigencia de 24 horas):

![Output Login](BackendSAP/../Images/output.png)

3. Al iniciar sesi�n y obtener el token, se debe habilitar la autorizaci�n para tener acceso a ciertos endpoints (dependiendo de tu rol como usuario), y se debe marcar de la siguiente forma:

![Autorizaci�n](BackendSAP/../Images/autorizar.png)

Nota: Para que la autoriazai�n no falle, se debe colocar la palabra "Bearer" seguido del token, como se muestra en la imagen anterior.

4. Ya despu�s de eso, es posible ejecutar gran parte de los endpoints. El siguiente ejemplo muestra el output de la creaci�n de un trastorno, el cual solo lo puede ejecutar un admin:

![Creaci�n Trastorno](BackendSAP/../Images/output2.png)

Si se ejecutara este endpoint con otro usuario de un rol distinto a "admin", la API generar�a un error 403 por no tener autorizaci�n.

## Autores

- ### [@Andr�s L�pez](https://github.com/andreselc) [![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/andres-lopez-644338281/)


