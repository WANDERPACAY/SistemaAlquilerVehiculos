USE [SistemaAlquilerVehiculosPablo];
GO

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    -- Agregar departamentos sin duplicarlos.
    INSERT INTO dbo.DEPARTAMENTO (NombreDepartamento)
    SELECT origen.Nombre
    FROM
    (
        VALUES
            (N'Alta Verapaz'),
            (N'Baja Verapaz')
    ) AS origen(Nombre)
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.DEPARTAMENTO AS d
        WHERE d.NombreDepartamento = origen.Nombre
    );

    -- Buscar el departamento por nombre, sin asumir su ID.
    INSERT INTO dbo.MUNICIPIO
        (IdDepartamento, NombreMunicipio)
    SELECT d.IdDepartamento, origen.Municipio
    FROM
    (
        VALUES
            (N'Alta Verapaz', N'Cobán'),
            (N'Alta Verapaz', N'San Pedro Carchá'),
            (N'Alta Verapaz', N'San Juan Chamelco'),
            (N'Baja Verapaz', N'Salamá'),
            (N'Baja Verapaz', N'Rabinal'),
            (N'Baja Verapaz', N'Cubulco')
    ) AS origen(Departamento, Municipio)
    INNER JOIN dbo.DEPARTAMENTO AS d
        ON d.NombreDepartamento = origen.Departamento
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.MUNICIPIO AS m
        WHERE m.IdDepartamento = d.IdDepartamento
          AND m.NombreMunicipio = origen.Municipio
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO

SELECT
    d.NombreDepartamento AS Departamento,
    m.NombreMunicipio AS Municipio
FROM dbo.DEPARTAMENTO AS d
INNER JOIN dbo.MUNICIPIO AS m
    ON m.IdDepartamento = d.IdDepartamento
ORDER BY d.NombreDepartamento, m.NombreMunicipio;
GO