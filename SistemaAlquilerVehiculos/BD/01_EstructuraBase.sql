USE [master];
GO

IF DB_ID(N'SistemaAlquilerVehiculosInstalacionPrueba') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [SistemaAlquilerVehiculosInstalacionPrueba];');
END;
GO

USE [SistemaAlquilerVehiculosInstalacionPrueba];
GO

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET XACT_ABORT ON;

IF EXISTS (
    SELECT 1
    FROM sys.tables
    WHERE is_ms_shipped = 0
)
BEGIN
    THROW 50001,
        'La base de prueba ya contiene tablas. No se realizaron cambios.',
        1;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    CREATE TABLE dbo.ROL
    (
        IdRol INT IDENTITY(1,1) NOT NULL,
        NombreRol VARCHAR(50) NOT NULL,
        Descripcion VARCHAR(200) NULL,

        CONSTRAINT PK_ROL
            PRIMARY KEY CLUSTERED (IdRol)
    );

    CREATE TABLE dbo.USUARIO
    (
        IdUsuario INT IDENTITY(1,1) NOT NULL,
        NombreCompleto VARCHAR(100) NOT NULL,
        Username VARCHAR(50) NOT NULL,
        PasswordHash VARCHAR(255) NOT NULL,
        Activo BIT NULL
            CONSTRAINT DF_USUARIO_Activo DEFAULT (1),
        IdRol INT NOT NULL,

        CONSTRAINT PK_USUARIO
            PRIMARY KEY CLUSTERED (IdUsuario),

        CONSTRAINT FK_Usuario_Rol
            FOREIGN KEY (IdRol)
            REFERENCES dbo.ROL (IdRol)
    );

    CREATE TABLE dbo.CLIENTE
    (
        IdCliente INT IDENTITY(1,1) NOT NULL,
        DPI VARCHAR(20) NOT NULL,
        Nombres VARCHAR(50) NOT NULL,
        Apellidos VARCHAR(50) NOT NULL,
        Telefono VARCHAR(20) NULL,
        Direccion VARCHAR(150) NULL,
        NumeroLicencia VARCHAR(30) NULL,
        TipoLicencia VARCHAR(10) NULL,
        FechaVencLicencia DATE NULL,
        FotoLicenciaRuta VARCHAR(255) NULL,

        CONSTRAINT PK_CLIENTE
            PRIMARY KEY CLUSTERED (IdCliente)
    );

    -- IdMunicipio y su relación se agregan mediante
    -- 02_UbicacionClientes.sql.

    CREATE TABLE dbo.VEHICULO
    (
        IdVehiculo INT IDENTITY(1,1) NOT NULL,
        Placa VARCHAR(20) NOT NULL,
        Marca VARCHAR(50) NOT NULL,
        Modelo VARCHAR(50) NOT NULL,
        Anio INT NULL,
        TipoVehiculo VARCHAR(30) NULL,
        Transmision VARCHAR(20) NULL,
        TarifaDiaria DECIMAL(18,2) NOT NULL,
        Estado VARCHAR(30) NULL,
        RutaFotografia VARCHAR(255) NULL,
        KilometrajeActual INT NULL,

        CONSTRAINT PK_VEHICULO
            PRIMARY KEY CLUSTERED (IdVehiculo)
    );

    CREATE TABLE dbo.CONTRATO_ALQUILER
    (
        IdContrato INT IDENTITY(1,1) NOT NULL,
        NumeroContrato VARCHAR(30) NOT NULL,
        IdCliente INT NOT NULL,
        IdVehiculo INT NOT NULL,
        IdUsuario INT NOT NULL,
        FechaSalida DATETIME NOT NULL,
        FechaRetornoPactada DATETIME NOT NULL,
        FechaRetornoReal DATETIME NULL,
        KilometrajeInicial INT NULL,
        KilometrajeFinal INT NULL,
        DepositoGarantia DECIMAL(18,2) NULL,
        EstadoContrato VARCHAR(30) NULL,

        CONSTRAINT PK_CONTRATO_ALQUILER
            PRIMARY KEY CLUSTERED (IdContrato),

        CONSTRAINT FK_Contrato_Cliente
            FOREIGN KEY (IdCliente)
            REFERENCES dbo.CLIENTE (IdCliente),

        CONSTRAINT FK_Contrato_Vehiculo
            FOREIGN KEY (IdVehiculo)
            REFERENCES dbo.VEHICULO (IdVehiculo),

        CONSTRAINT FK_Contrato_Usuario
            FOREIGN KEY (IdUsuario)
            REFERENCES dbo.USUARIO (IdUsuario)
    );

    CREATE TABLE dbo.PAGO_DETALLE
    (
        IdPago INT IDENTITY(1,1) NOT NULL,
        IdContrato INT NOT NULL,
        FechaPago DATETIME NOT NULL,
        Monto DECIMAL(18,2) NOT NULL,
        TipoCobro VARCHAR(50) NULL,
        MetodoPago VARCHAR(30) NULL,
        Observaciones VARCHAR(255) NULL,

        CONSTRAINT PK_PAGO_DETALLE
            PRIMARY KEY CLUSTERED (IdPago),

        CONSTRAINT FK_Pago_Contrato
            FOREIGN KEY (IdContrato)
            REFERENCES dbo.CONTRATO_ALQUILER (IdContrato)
    );

    CREATE TABLE dbo.MANTENIMIENTO
    (
        IdMantenimiento INT IDENTITY(1,1) NOT NULL,
        IdVehiculo INT NOT NULL,
        FechaProgramada DATE NULL,
        FechaRealizada DATE NULL,
        KilometrajeServicio INT NULL,
        Descripcion VARCHAR(255) NULL,
        Costo DECIMAL(18,2) NULL,
        EstadoMantenimiento VARCHAR(30) NULL,

        CONSTRAINT PK_MANTENIMIENTO
            PRIMARY KEY CLUSTERED (IdMantenimiento),

        CONSTRAINT FK_Mantenimiento_Vehiculo
            FOREIGN KEY (IdVehiculo)
            REFERENCES dbo.VEHICULO (IdVehiculo)
    );

    CREATE TABLE dbo.BITACORA
    (
        IdBitacora INT IDENTITY(1,1) NOT NULL,
        FechaOperacion DATETIME NOT NULL,
        TipoOperacion VARCHAR(50) NOT NULL,
        Tabla VARCHAR(50) NOT NULL,
        IdRegistro INT NULL,
        IdUsuario INT NULL,
        Detalle VARCHAR(255) NULL,

        CONSTRAINT PK_BITACORA
            PRIMARY KEY CLUSTERED (IdBitacora)
    );

    COMMIT TRANSACTION;

    SELECT
        DB_NAME() AS BaseDeDatos,
        N'Estructura base creada correctamente.' AS Resultado;

    SELECT
        SCHEMA_NAME(schema_id) AS Esquema,
        name AS Tabla
    FROM sys.tables
    WHERE is_ms_shipped = 0
    ORDER BY name;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;