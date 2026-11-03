USE [SistemaAlquilerVehiculosInstalacionPrueba];
GO

SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    -- Crear los roles si todavía no existen.
    IF NOT EXISTS (
        SELECT 1 FROM dbo.ROL
        WHERE NombreRol = 'Administrador'
    )
    BEGIN
        INSERT INTO dbo.ROL (NombreRol, Descripcion)
        VALUES ('Administrador', 'Control total del sistema');
    END;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.ROL
        WHERE NombreRol = 'Empleado'
    )
    BEGIN
        INSERT INTO dbo.ROL (NombreRol, Descripcion)
        VALUES ('Empleado', 'Operaciones de alquiler');
    END;

    -- Obtener el rol por nombre, sin asumir su ID.
    DECLARE @IdAdministrador INT;

    SELECT @IdAdministrador = IdRol
    FROM dbo.ROL
    WHERE NombreRol = 'Administrador';

    -- Credenciales para la instalación de prueba:
    -- Usuario: admin
    -- Contraseña: 123456
    DECLARE @PasswordHash VARCHAR(64);

    SET @PasswordHash = LOWER(
        CONVERT(
            VARCHAR(64),
            HASHBYTES('SHA2_256', '123456'),
            2
        )
    );

    -- Si ya existe, conservar su contraseña y configuración.
    IF NOT EXISTS (
        SELECT 1 FROM dbo.USUARIO
        WHERE Username = 'admin'
    )
    BEGIN
        INSERT INTO dbo.USUARIO
        (
            NombreCompleto,
            Username,
            PasswordHash,
            Activo,
            IdRol
        )
        VALUES
        (
            'Administrador del sistema',
            'admin',
            @PasswordHash,
            1,
            @IdAdministrador
        );
    END;

    COMMIT TRANSACTION;

    SELECT
        u.IdUsuario,
        u.NombreCompleto,
        u.Username,
        u.Activo,
        r.NombreRol
    FROM dbo.USUARIO AS u
    INNER JOIN dbo.ROL AS r ON r.IdRol = u.IdRol
    WHERE u.Username = 'admin';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO