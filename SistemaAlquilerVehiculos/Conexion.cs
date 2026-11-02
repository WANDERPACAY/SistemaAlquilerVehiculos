using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace SistemaAlquilerVehiculos
{
    internal class Conexion
    {
        private static readonly string cadena = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conexion = new SqlConnection(cadena);
            return conexion;
        }
    }

}
