# Instalación de la base de datos



## Requisitos



- Visual Studio con desarrollo de escritorio .NET.

- Herramientas de compilación para .NET Framework 4.7.2.

- SQL Server Express LocalDB, instancia (LocalDB)\MSSQLLocalDB.



## Crear la base



Conectarse a (LocalDB)\MSSQLLocalDB desde el editor SQL.

Ejecutar completos estos archivos, en orden:



1. 01_EstructuraBase.sql

2. 02_UbicacionClientes.sql

3. 03_CatalogoUbicacionInicial.sql

4. 04_DatosIniciales.sql



Los cuatro archivos apuntan a:

SistemaAlquilerVehiculosInstalacionPrueba



El script 01 crea la base si no existe y se detiene si ya contiene

tablas. No es necesario repetirlo después de una instalación correcta.



Resultado esperado:

- Diez tablas.

- Dos departamentos y seis municipios.

- Roles Administrador y Empleado.

- Usuario admin activo.



El catálogo de ubicaciones es inicial y no cubre toda Guatemala.

La instalación no incluye clientes, vehículos ni contratos de ejemplo.



## Configurar la aplicación



Conexion.cs utiliza primero la variable de entorno:

ALQUILER_VEHICULOS_CONEXION



Si no está definida, utiliza ConexionBD de App.config.



Para utilizar la base instalada, ejecutar en PowerShell:



[Environment]::SetEnvironmentVariable(

    "ALQUILER_VEHICULOS_CONEXION",

    "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SistemaAlquilerVehiculosInstalacionPrueba;Integrated Security=True;Connect Timeout=30;",

    "User"

)



Cerrar todas las ventanas de Visual Studio y volver a abrirlo desde

Inicio para que reciba la variable actualizada.



Cada integrante configura su conexión en su propia computadora.

Quien ya utiliza otra base puede conservar su configuración personal.



## Acceso inicial de prueba



Usuario: admin

Contraseña: 123456



Estas credenciales son para la demostración académica.

Cambiar la contraseña antes de utilizar datos reales.



## Fotografías



Las imágenes se guardan en carpetas junto al ejecutable:



- Fotos\Vehiculos

- Fotos\Licencias



En ejecución Debug, normalmente están dentro de:

SistemaAlquilerVehiculos\bin\Debug\Fotos



Los scripts guardan la estructura de la base, no las fotografías.

En una instalación nueva, seleccionar imágenes desde los formularios.



Para trasladar registros existentes con sus imágenes, trasladar

también las carpetas Fotos conservando los nombres y rutas relativas.



Respaldar la carpeta Fotos antes de limpiar o eliminar bin.

Usar imágenes de licencia ficticias para las pruebas compartidas.



## Comprobación



Compilar e iniciar sesión.

Abrir Clientes, Vehículos y Galería.

Las listas estarán vacías al comenzar.

Comprobar departamentos y municipios en Clientes.

Registrar datos de prueba para comprobar las operaciones.
