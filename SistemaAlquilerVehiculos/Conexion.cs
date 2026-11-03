using System;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos
{
    internal class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadenaConexion =
              @"Data Source=(LocalDB)\MSSQLLocalDB;" +
              "Initial Catalog=SistemaAlquilerVehiculosPablo;" +
              "Integrated Security=True;" +
              "Connect Timeout=30;";

            return new SqlConnection(cadenaConexion);
        }
    }
}