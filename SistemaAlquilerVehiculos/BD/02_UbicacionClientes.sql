USE [SistemaAlquilerVehiculosInstalacionPrueba];
GO

-- Catálogo de departamentos.
IF OBJECT_ID(N'dbo.DEPARTAMENTO', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.DEPARTAMENTO
    (
        IdDepartamento INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_DEPARTAMENTO PRIMARY KEY,

        NombreDepartamento NVARCHAR(50) NOT NULL
            CONSTRAINT UQ_DEPARTAMENTO_Nombre UNIQUE
    );
END;
GO

-- Cada municipio pertenece a un departamento.
IF OBJECT_ID(N'dbo.MUNICIPIO', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.MUNICIPIO
    (
        IdMunicipio INT IDENTITY(1,1) NOT NULL
            CONSTRAINT PK_MUNICIPIO PRIMARY KEY,

        IdDepartamento INT NOT NULL,

        NombreMunicipio NVARCHAR(100) NOT NULL,

        CONSTRAINT FK_Municipio_Departamento
            FOREIGN KEY (IdDepartamento)
            REFERENCES dbo.DEPARTAMENTO(IdDepartamento),

        CONSTRAINT UQ_Municipio_Departamento_Nombre
            UNIQUE (IdDepartamento, NombreMunicipio)
    );
END;
GO

-- Nullable para conservar clientes que ya existan.
IF COL_LENGTH(N'dbo.CLIENTE', N'IdMunicipio') IS NULL
BEGIN
    ALTER TABLE dbo.CLIENTE
        ADD IdMunicipio INT NULL;
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.foreign_keys
    WHERE name = N'FK_Cliente_Municipio'
      AND parent_object_id = OBJECT_ID(N'dbo.CLIENTE')
)
BEGIN
    ALTER TABLE dbo.CLIENTE WITH CHECK
        ADD CONSTRAINT FK_Cliente_Municipio
        FOREIGN KEY (IdMunicipio)
        REFERENCES dbo.MUNICIPIO(IdMunicipio);
END;
GO

-- Verificar las tablas y la nueva columna.
SELECT name AS Tabla
FROM sys.tables
WHERE name IN ('DEPARTAMENTO', 'MUNICIPIO')
ORDER BY name;

SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo'
  AND TABLE_NAME = 'CLIENTE'
  AND COLUMN_NAME = 'IdMunicipio';
GO