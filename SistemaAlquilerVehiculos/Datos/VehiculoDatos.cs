using System;
using System.Data;
using System.Data.SqlClient;
using SistemaAlquilerVehiculos.Entidades;

namespace SistemaAlquilerVehiculos.Datos
{
    public class VehiculoDatos
    {
        public DataTable Listar(
            string buscar = "",
            string tipo = "",
            string transmision = "")
        {
            const string sql = @"
                SELECT IdVehiculo, Placa, Marca, Modelo, Anio,
                       TipoVehiculo, Transmision, TarifaDiaria,
                       Estado, RutaFotografia, KilometrajeActual
                FROM dbo.VEHICULO
                WHERE
                    (@Buscar = ''
                     OR Placa LIKE '%' + @Buscar + '%'
                     OR Marca LIKE '%' + @Buscar + '%'
                     OR Modelo LIKE '%' + @Buscar + '%')
                    AND (@Tipo = '' OR TipoVehiculo = @Tipo)
                    AND (@Transmision = '' OR Transmision = @Transmision)
                ORDER BY Placa;";

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.Add(
                        "@Buscar", SqlDbType.VarChar, 100
                    ).Value = (buscar ?? "").Trim();

                    comando.Parameters.Add(
                        "@Tipo", SqlDbType.VarChar, 30
                    ).Value = (tipo ?? "").Trim();

                    comando.Parameters.Add(
                        "@Transmision", SqlDbType.VarChar, 20
                    ).Value = (transmision ?? "").Trim();

                    using (SqlDataAdapter adaptador =
                        new SqlDataAdapter(comando))
                    {
                        DataTable tabla = new DataTable();

                        conexion.Open();
                        adaptador.Fill(tabla);

                        return tabla;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "No se pudo consultar la lista de vehículos.", ex);
            }
        }
        public void Eliminar(int idVehiculo, int idUsuario)
        {
            if (idVehiculo <= 0)
                throw new ArgumentException("Selecciona un vehículo.");

            if (idUsuario <= 0)
                throw new InvalidOperationException(
                    "Debes iniciar sesión para eliminar vehículos.");

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlTransaction transaccion =
                        conexion.BeginTransaction(IsolationLevel.Serializable))
                    {
                        string placa;

                        const string sqlVehiculo = @"
                    SELECT Placa
                    FROM dbo.VEHICULO WITH (UPDLOCK, HOLDLOCK)
                    WHERE IdVehiculo = @Id;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlVehiculo, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@Id", SqlDbType.Int).Value = idVehiculo;

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null || resultado == DBNull.Value)
                                throw new InvalidOperationException(
                                    "El vehículo ya no existe. Actualiza el listado.");

                            placa = Convert.ToString(resultado);
                        }

                        const string sqlRelaciones = @"
                    SELECT
                        CASE WHEN EXISTS (
                            SELECT 1 FROM dbo.CONTRATO_ALQUILER
                            WHERE IdVehiculo = @Id
                        ) THEN 1 ELSE 0 END AS TieneContratos,
                        CASE WHEN EXISTS (
                            SELECT 1 FROM dbo.MANTENIMIENTO
                            WHERE IdVehiculo = @Id
                        ) THEN 1 ELSE 0 END AS TieneMantenimientos;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlRelaciones, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@Id", SqlDbType.Int).Value = idVehiculo;

                            using (SqlDataReader lector = comando.ExecuteReader())
                            {
                                lector.Read();

                                if (lector.GetInt32(0) == 1)
                                    throw new InvalidOperationException(
                                        "No se puede eliminar: el vehículo tiene contratos registrados.");

                                if (lector.GetInt32(1) == 1)
                                    throw new InvalidOperationException(
                                        "No se puede eliminar: el vehículo tiene mantenimientos registrados.");
                            }
                        }

                        using (SqlCommand comando = new SqlCommand(
                            "DELETE FROM dbo.VEHICULO WHERE IdVehiculo = @Id;",
                            conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@Id", SqlDbType.Int).Value = idVehiculo;

                            if (comando.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException(
                                    "No se pudo eliminar el vehículo seleccionado.");
                        }

                        const string sqlBitacora = @"
                    INSERT INTO dbo.BITACORA
                    (
                        FechaOperacion, TipoOperacion, Tabla,
                        IdRegistro, IdUsuario, Detalle
                    )
                    VALUES
                    (
                        GETDATE(), 'DELETE', 'VEHICULO',
                        @Id, @Usuario, @Detalle
                    );";

                        using (SqlCommand comando = new SqlCommand(
                            sqlBitacora, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@Id", SqlDbType.Int).Value = idVehiculo;

                            comando.Parameters.Add(
                                "@Usuario", SqlDbType.Int).Value = idUsuario;

                            comando.Parameters.Add(
                                "@Detalle", SqlDbType.VarChar, 255
                            ).Value = "Eliminación de vehículo con placa " + placa;

                            comando.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    throw new InvalidOperationException(
                        "No se puede eliminar: existen registros relacionados.",
                        ex);

                throw new Exception(
                    "No se pudo eliminar el vehículo y registrar su bitácora.",
                    ex);
            }
        }
        public int Guardar(Vehiculo vehiculo, int idUsuario)
        {
            if (idUsuario <= 0)
                throw new InvalidOperationException(
                    "Debes iniciar sesión para guardar vehículos.");

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlTransaction transaccion =
                        conexion.BeginTransaction(IsolationLevel.Serializable))
                    {
                        // La transacción se revierte al salir sin Commit.
                        const string sqlDuplicado = @"
                    SELECT COUNT(*)
                    FROM dbo.VEHICULO WITH (UPDLOCK, HOLDLOCK)
                    WHERE Placa = @Placa;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlDuplicado, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@Placa", SqlDbType.VarChar, 20
                            ).Value = vehiculo.Placa;

                            int cantidad = Convert.ToInt32(
                                comando.ExecuteScalar());

                            if (cantidad > 0)
                                throw new InvalidOperationException(
                                    "Ya existe un vehículo con esa placa.");
                        }

                        const string sqlInsertar = @"
                    INSERT INTO dbo.VEHICULO
                    (
                        Placa, Marca, Modelo, Anio, TipoVehiculo,
                        Transmision, TarifaDiaria, Estado,
                        RutaFotografia, KilometrajeActual
                    )
                    OUTPUT INSERTED.IdVehiculo
                    VALUES
                    (
                        @Placa, @Marca, @Modelo, @Anio, @TipoVehiculo,
                        @Transmision, @TarifaDiaria, @Estado,
                        @RutaFotografia, @KilometrajeActual
                    );";

                        int idVehiculo;

                        using (SqlCommand comando = new SqlCommand(
                            sqlInsertar, conexion, transaccion))
                        {
                            AgregarParametros(comando, vehiculo);

                            idVehiculo = Convert.ToInt32(
                                comando.ExecuteScalar());
                        }

                        const string sqlBitacora = @"
                    INSERT INTO dbo.BITACORA
                    (
                        FechaOperacion, TipoOperacion, Tabla,
                        IdRegistro, IdUsuario, Detalle
                    )
                    VALUES
                    (
                        GETDATE(), 'INSERT', 'VEHICULO',
                        @IdRegistro, @IdUsuario, @Detalle
                    );";

                        using (SqlCommand comando = new SqlCommand(
                            sqlBitacora, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdRegistro", SqlDbType.Int
                            ).Value = idVehiculo;

                            comando.Parameters.Add(
                                "@IdUsuario", SqlDbType.Int
                            ).Value = idUsuario;

                            comando.Parameters.Add(
                                "@Detalle", SqlDbType.VarChar, 255
                            ).Value = "Registro de vehículo con placa "
                                + vehiculo.Placa;

                            comando.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                        return idVehiculo;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new InvalidOperationException(
                        "El registro contiene un valor que ya existe en la base.",
                        ex);

                throw new Exception(
                    "No se pudo guardar el vehículo y su bitácora.", ex);
            }
        }
        public void Editar(Vehiculo vehiculo, int idUsuario)
        {
            if (idUsuario <= 0)
                throw new InvalidOperationException(
                    "Debes iniciar sesión para editar vehículos.");

            if (vehiculo.IdVehiculo <= 0)
                throw new ArgumentException(
                    "Selecciona el vehículo que deseas editar.");

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlTransaction transaccion =
                        conexion.BeginTransaction(IsolationLevel.Serializable))
                    {
                        const string sqlDuplicado = @"
                    SELECT COUNT(*)
                    FROM dbo.VEHICULO WITH (UPDLOCK, HOLDLOCK)
                    WHERE Placa = @Placa
                      AND IdVehiculo <> @IdVehiculo;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlDuplicado, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@Placa", SqlDbType.VarChar, 20
                            ).Value = vehiculo.Placa;

                            comando.Parameters.Add(
                                "@IdVehiculo", SqlDbType.Int
                            ).Value = vehiculo.IdVehiculo;

                            if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
                                throw new InvalidOperationException(
                                    "Otro vehículo ya utiliza esa placa.");
                        }

                        const string sqlActualizar = @"
                    UPDATE dbo.VEHICULO
                    SET Placa = @Placa,
                        Marca = @Marca,
                        Modelo = @Modelo,
                        Anio = @Anio,
                        TipoVehiculo = @TipoVehiculo,
                        Transmision = @Transmision,
                        TarifaDiaria = @TarifaDiaria,
                        Estado = @Estado,
RutaFotografia = @RutaFotografia,
KilometrajeActual = @KilometrajeActual
                    WHERE IdVehiculo = @IdVehiculo;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlActualizar, conexion, transaccion))
                        {
                            AgregarParametros(comando, vehiculo);

                            comando.Parameters.Add(
                                "@IdVehiculo", SqlDbType.Int
                            ).Value = vehiculo.IdVehiculo;

                            if (comando.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException(
                                    "El vehículo ya no existe. Actualiza el listado.");
                        }

                        const string sqlBitacora = @"
                    INSERT INTO dbo.BITACORA
                    (
                        FechaOperacion, TipoOperacion, Tabla,
                        IdRegistro, IdUsuario, Detalle
                    )
                    VALUES
                    (
                        GETDATE(), 'UPDATE', 'VEHICULO',
                        @IdRegistro, @IdUsuario, @Detalle
                    );";

                        using (SqlCommand comando = new SqlCommand(
                            sqlBitacora, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdRegistro", SqlDbType.Int
                            ).Value = vehiculo.IdVehiculo;

                            comando.Parameters.Add(
                                "@IdUsuario", SqlDbType.Int
                            ).Value = idUsuario;

                            comando.Parameters.Add(
                                "@Detalle", SqlDbType.VarChar, 255
                            ).Value = "Edición de vehículo con placa "
                                + vehiculo.Placa;

                            comando.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new InvalidOperationException(
                        "El registro contiene un valor que ya existe en la base.",
                        ex);

                throw new Exception(
                    "No se pudo editar el vehículo y su bitácora.", ex);
            }
        }
        private static void AgregarParametros(
            SqlCommand comando, Vehiculo vehiculo)
        {
            comando.Parameters.Add(
                "@Placa", SqlDbType.VarChar, 20
            ).Value = vehiculo.Placa;

            comando.Parameters.Add(
                "@Marca", SqlDbType.VarChar, 50
            ).Value = vehiculo.Marca;

            comando.Parameters.Add(
                "@Modelo", SqlDbType.VarChar, 50
            ).Value = vehiculo.Modelo;

            comando.Parameters.Add(
                "@Anio", SqlDbType.Int
            ).Value = vehiculo.Anio;

            comando.Parameters.Add(
                "@TipoVehiculo", SqlDbType.VarChar, 30
            ).Value = vehiculo.TipoVehiculo;

            comando.Parameters.Add(
                "@Transmision", SqlDbType.VarChar, 20
            ).Value = vehiculo.Transmision;

            SqlParameter tarifa = comando.Parameters.Add(
                "@TarifaDiaria", SqlDbType.Decimal);

            tarifa.Precision = 18;
            tarifa.Scale = 2;
            tarifa.Value = vehiculo.TarifaDiaria;

            comando.Parameters.Add(
                "@Estado", SqlDbType.VarChar, 30
            ).Value = vehiculo.Estado;

            comando.Parameters.Add(
                "@RutaFotografia", SqlDbType.VarChar, 255
            ).Value = string.IsNullOrWhiteSpace(vehiculo.RutaFotografia)
                ? (object)DBNull.Value
                : vehiculo.RutaFotografia;

            comando.Parameters.Add(
                "@KilometrajeActual", SqlDbType.Int
            ).Value = vehiculo.KilometrajeActual;
        }
    }
    }
