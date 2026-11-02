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
            // Obtenemos la ruta base del directorio del proyecto (subiendo desde bin\Debug si es necesario)
            string dirBase = AppDomain.CurrentDomain.BaseDirectory;

            // Si estamos corriendo desde bin\Debug o bin\Release, subimos dos niveles para apuntar a la raíz del proyecto
            DirectoryInfo dir = new DirectoryInfo(dirBase);
            if (dir.Name.Equals("Debug", StringComparison.OrdinalIgnoreCase) ||
                dir.Name.Equals("Release", StringComparison.OrdinalIgnoreCase))
            {
                dirBase = dir.Parent.Parent.FullName;
            }

            string rutaDb = Path.Combine(dirBase, "Database1.mdf");

            // Cadena robusta usando LocalDB con un nombre de instancia explícito para evitar bloqueos
            string cadenaConexion = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={rutaDb};Integrated Security=True;Connect Timeout=30";

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            return conexion;
        }
    }
}