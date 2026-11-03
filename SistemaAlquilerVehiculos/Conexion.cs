using System;
using System.Configuration;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos
{
    internal class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadenaConexion =
                Environment.GetEnvironmentVariable(
                    "ALQUILER_VEHICULOS_CONEXION");

            if (string.IsNullOrWhiteSpace(cadenaConexion))
            {
                ConnectionStringSettings configuracion =
                    ConfigurationManager
                        .ConnectionStrings["ConexionBD"];

                if (configuracion == null ||
                    string.IsNullOrWhiteSpace(
                        configuracion.ConnectionString))
                {
                    throw new ConfigurationErrorsException(
                        "No se encontró la conexión ConexionBD "
                        + "en la configuración del programa.");
                }

                cadenaConexion = configuracion.ConnectionString;
            }

            return new SqlConnection(cadenaConexion);
        }
    }
}