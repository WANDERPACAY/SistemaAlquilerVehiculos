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
            string dirBase = AppDomain.CurrentDomain.BaseDirectory;

            DirectoryInfo dir = new DirectoryInfo(dirBase);
            if (dir.Name.Equals("Debug", StringComparison.OrdinalIgnoreCase) ||
                dir.Name.Equals("Release", StringComparison.OrdinalIgnoreCase))
            {
                dirBase = dir.Parent.Parent.FullName;
            }

            string rutaDb = Path.Combine(dirBase, "Database1.mdf");

            string cadenaConexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={rutaDb};Integrated Security=True;Connect Timeout=30";

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            return conexion;
        }
    }
}