using System;
using System.Data;
using System.Data.SqlClient;
using SistemaAlquilerVehiculos.Entidades;

namespace SistemaAlquilerVehiculos.Datos
{
    public class ClienteDatos
    {
        public DataTable Listar(string buscar = "")
        {
            const string sql = @"
                SELECT
                    c.IdCliente,
                    c.DPI,
                    c.Nombres,
                    c.Apellidos,
                    c.Telefono,
                    c.Direccion,
                    d.IdDepartamento,
                    c.IdMunicipio,
                    d.NombreDepartamento AS Departamento,
                    m.NombreMunicipio AS Municipio,
                    c.NumeroLicencia,
                    c.TipoLicencia,
                    c.FechaVencLicencia,
                    c.FotoLicenciaRuta
                FROM dbo.CLIENTE AS c
                LEFT JOIN dbo.MUNICIPIO AS m
                    ON m.IdMunicipio = c.IdMunicipio
                LEFT JOIN dbo.DEPARTAMENTO AS d
                    ON d.IdDepartamento = m.IdDepartamento
                WHERE
                    @Buscar = ''
                    OR c.DPI LIKE '%' + @Buscar + '%'
                    OR c.Nombres LIKE '%' + @Buscar + '%'
                    OR c.Apellidos LIKE '%' + @Buscar + '%'
                ORDER BY c.Apellidos, c.Nombres, c.IdCliente;";

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    comando.Parameters.Add(
                        "@Buscar", SqlDbType.VarChar, 100
                    ).Value = (buscar ?? "").Trim();

                    DataTable tabla = new DataTable();

                    conexion.Open();
                    adaptador.Fill(tabla);

                    return tabla;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "No se pudieron consultar los clientes.", ex);
            }
        }
        public int Guardar(Cliente cliente, int idUsuario)
        {
            if (cliente == null)
                throw new ArgumentException("No se recibieron datos del cliente.");

            if (idUsuario <= 0)
                throw new InvalidOperationException(
                    "Debes iniciar sesión para registrar clientes.");

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
                    FROM dbo.CLIENTE WITH (UPDLOCK, HOLDLOCK)
                    WHERE DPI = @DPI;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlDuplicado, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@DPI", SqlDbType.VarChar, 20
                            ).Value = cliente.DPI;

                            if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
                                throw new InvalidOperationException(
                                    "Ya existe un cliente con ese DPI.");
                        }

                        const string sqlInsertar = @"
                    INSERT INTO dbo.CLIENTE
                    (
                        DPI, Nombres, Apellidos, Telefono, Direccion,
                        IdMunicipio, NumeroLicencia, TipoLicencia,
                        FechaVencLicencia, FotoLicenciaRuta
                    )
                    OUTPUT INSERTED.IdCliente
                    VALUES
                    (
                        @DPI, @Nombres, @Apellidos, @Telefono, @Direccion,
                        @IdMunicipio, @NumeroLicencia, @TipoLicencia,
                        @FechaVencLicencia, @FotoLicenciaRuta
                    );";

                        int idCliente;

                        using (SqlCommand comando = new SqlCommand(
                            sqlInsertar, conexion, transaccion))
                        {
                            AgregarParametros(comando, cliente);
                            idCliente = Convert.ToInt32(comando.ExecuteScalar());
                        }

                        const string sqlBitacora = @"
                    INSERT INTO dbo.BITACORA
                    (
                        FechaOperacion, TipoOperacion, Tabla,
                        IdRegistro, IdUsuario, Detalle
                    )
                    VALUES
                    (
                        GETDATE(), 'INSERT', 'CLIENTE',
                        @IdRegistro, @IdUsuario, @Detalle
                    );";

                        using (SqlCommand comando = new SqlCommand(
                            sqlBitacora, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdRegistro", SqlDbType.Int
                            ).Value = idCliente;

                            comando.Parameters.Add(
                                "@IdUsuario", SqlDbType.Int
                            ).Value = idUsuario;

                            comando.Parameters.Add(
                                "@Detalle", SqlDbType.VarChar, 255
                            ).Value = "Registro de cliente: " +
                                cliente.Nombres + " " + cliente.Apellidos;

                            comando.ExecuteNonQuery();
                        }

                        transaccion.Commit();
                        return idCliente;
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                    throw new InvalidOperationException(
                        "El registro contiene un valor duplicado.", ex);

                if (ex.Number == 547)
                    throw new InvalidOperationException(
                        "El municipio seleccionado ya no está disponible. " +
                        "Vuelve a seleccionarlo.", ex);

                throw new Exception(
                    "No se pudo guardar el cliente.", ex);
            }
        }
        public void Editar(Cliente cliente, int idUsuario)
        {
            if (cliente == null || cliente.IdCliente <= 0)
                throw new ArgumentException(
                    "Selecciona un cliente válido para editar.");

            if (idUsuario <= 0)
                throw new InvalidOperationException(
                    "Debes iniciar sesión para editar clientes.");

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
                    FROM dbo.CLIENTE WITH (UPDLOCK, HOLDLOCK)
                    WHERE DPI = @DPI
                      AND IdCliente <> @IdCliente;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlDuplicado, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@DPI", SqlDbType.VarChar, 20
                            ).Value = cliente.DPI;

                            comando.Parameters.Add(
                                "@IdCliente", SqlDbType.Int
                            ).Value = cliente.IdCliente;

                            if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
                                throw new InvalidOperationException(
                                    "Otro cliente ya utiliza ese DPI.");
                        }

                        const string sqlActualizar = @"
                    UPDATE dbo.CLIENTE
                    SET
                        DPI = @DPI,
                        Nombres = @Nombres,
                        Apellidos = @Apellidos,
                        Telefono = @Telefono,
                        Direccion = @Direccion,
                        IdMunicipio = @IdMunicipio,
                        NumeroLicencia = @NumeroLicencia,
                        TipoLicencia = @TipoLicencia,
                        FechaVencLicencia = @FechaVencLicencia,
                        FotoLicenciaRuta = @FotoLicenciaRuta
                    WHERE IdCliente = @IdCliente;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlActualizar, conexion, transaccion))
                        {
                            AgregarParametros(comando, cliente);

                            comando.Parameters.Add(
                                "@IdCliente", SqlDbType.Int
                            ).Value = cliente.IdCliente;

                            if (comando.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException(
                                    "El cliente ya no existe. Actualiza el listado.");
                        }

                        const string sqlBitacora = @"
                    INSERT INTO dbo.BITACORA
                    (
                        FechaOperacion, TipoOperacion, Tabla,
                        IdRegistro, IdUsuario, Detalle
                    )
                    VALUES
                    (
                        GETDATE(), 'UPDATE', 'CLIENTE',
                        @IdRegistro, @IdUsuario, @Detalle
                    );";

                        using (SqlCommand comando = new SqlCommand(
                            sqlBitacora, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdRegistro", SqlDbType.Int
                            ).Value = cliente.IdCliente;

                            comando.Parameters.Add(
                                "@IdUsuario", SqlDbType.Int
                            ).Value = idUsuario;

                            comando.Parameters.Add(
                                "@Detalle", SqlDbType.VarChar, 255
                            ).Value = "Edición de cliente: " +
                                cliente.Nombres + " " + cliente.Apellidos;

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
                        "El registro contiene un valor duplicado.", ex);

                if (ex.Number == 547)
                    throw new InvalidOperationException(
                        "El municipio seleccionado ya no está disponible. " +
                        "Vuelve a seleccionarlo.", ex);

                throw new Exception(
                    "No se pudo editar el cliente.", ex);
            }
        }
        public void Eliminar(int idCliente, int idUsuario)
        {
            if (idCliente <= 0)
                throw new ArgumentException(
                    "Selecciona un cliente válido para eliminar.");

            if (idUsuario <= 0)
                throw new InvalidOperationException(
                    "Debes iniciar sesión para eliminar clientes.");

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                {
                    conexion.Open();

                    using (SqlTransaction transaccion =
                        conexion.BeginTransaction(IsolationLevel.Serializable))
                    {
                        const string sqlCliente = @"
                    SELECT Nombres + ' ' + Apellidos
                    FROM dbo.CLIENTE WITH (UPDLOCK, HOLDLOCK)
                    WHERE IdCliente = @IdCliente;";

                        string nombreCliente;

                        using (SqlCommand comando = new SqlCommand(
                            sqlCliente, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdCliente", SqlDbType.Int
                            ).Value = idCliente;

                            object resultado = comando.ExecuteScalar();

                            if (resultado == null || resultado == DBNull.Value)
                                throw new InvalidOperationException(
                                    "El cliente ya no existe. Actualiza el listado.");

                            nombreCliente = Convert.ToString(resultado);
                        }

                        const string sqlContratos = @"
                    SELECT COUNT(*)
                    FROM dbo.CONTRATO_ALQUILER
                    WHERE IdCliente = @IdCliente;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlContratos, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdCliente", SqlDbType.Int
                            ).Value = idCliente;

                            if (Convert.ToInt32(comando.ExecuteScalar()) > 0)
                                throw new InvalidOperationException(
                                    "No se puede eliminar: el cliente tiene " +
                                    "contratos registrados.");
                        }

                        const string sqlEliminar = @"
                    DELETE FROM dbo.CLIENTE
                    WHERE IdCliente = @IdCliente;";

                        using (SqlCommand comando = new SqlCommand(
                            sqlEliminar, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdCliente", SqlDbType.Int
                            ).Value = idCliente;

                            if (comando.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException(
                                    "El cliente ya no existe. Actualiza el listado.");
                        }

                        const string sqlBitacora = @"
                    INSERT INTO dbo.BITACORA
                    (
                        FechaOperacion, TipoOperacion, Tabla,
                        IdRegistro, IdUsuario, Detalle
                    )
                    VALUES
                    (
                        GETDATE(), 'DELETE', 'CLIENTE',
                        @IdRegistro, @IdUsuario, @Detalle
                    );";

                        using (SqlCommand comando = new SqlCommand(
                            sqlBitacora, conexion, transaccion))
                        {
                            comando.Parameters.Add(
                                "@IdRegistro", SqlDbType.Int
                            ).Value = idCliente;

                            comando.Parameters.Add(
                                "@IdUsuario", SqlDbType.Int
                            ).Value = idUsuario;

                            comando.Parameters.Add(
                                "@Detalle", SqlDbType.VarChar, 255
                            ).Value = "Eliminación de cliente: " + nombreCliente;

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
                        "No se puede eliminar: el cliente tiene " +
                        "registros relacionados.", ex);

                throw new Exception(
                    "No se pudo eliminar el cliente.", ex);
            }
        }
        private static void AgregarParametros(
            SqlCommand comando, Cliente cliente)
        {
            comando.Parameters.Add(
                "@DPI", SqlDbType.VarChar, 20
            ).Value = cliente.DPI;

            comando.Parameters.Add(
                "@Nombres", SqlDbType.VarChar, 50
            ).Value = cliente.Nombres;

            comando.Parameters.Add(
                "@Apellidos", SqlDbType.VarChar, 50
            ).Value = cliente.Apellidos;

            comando.Parameters.Add(
                "@Telefono", SqlDbType.VarChar, 20
            ).Value = string.IsNullOrWhiteSpace(cliente.Telefono)
                ? (object)DBNull.Value : cliente.Telefono;

            comando.Parameters.Add(
                "@Direccion", SqlDbType.VarChar, 150
            ).Value = string.IsNullOrWhiteSpace(cliente.Direccion)
                ? (object)DBNull.Value : cliente.Direccion;

            comando.Parameters.Add(
                "@IdMunicipio", SqlDbType.Int
            ).Value = cliente.IdMunicipio.HasValue
                ? (object)cliente.IdMunicipio.Value : DBNull.Value;

            comando.Parameters.Add(
                "@NumeroLicencia", SqlDbType.VarChar, 30
            ).Value = cliente.NumeroLicencia;

            comando.Parameters.Add(
                "@TipoLicencia", SqlDbType.VarChar, 10
            ).Value = cliente.TipoLicencia;

            comando.Parameters.Add(
                "@FechaVencLicencia", SqlDbType.Date
            ).Value = cliente.FechaVencLicencia.HasValue
                ? (object)cliente.FechaVencLicencia.Value.Date : DBNull.Value;

            comando.Parameters.Add(
                "@FotoLicenciaRuta", SqlDbType.VarChar, 255
            ).Value = cliente.FotoLicenciaRuta;
        }
    }
}