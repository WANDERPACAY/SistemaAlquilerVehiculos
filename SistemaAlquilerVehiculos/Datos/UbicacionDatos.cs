using System;
using System.Data;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos.Datos
{
    public class UbicacionDatos
    {
        public DataTable ListarDepartamentos()
        {
            const string sql = @"
                SELECT IdDepartamento, NombreDepartamento
                FROM dbo.DEPARTAMENTO
                ORDER BY NombreDepartamento;";

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataTable tabla = new DataTable();

                    conexion.Open();
                    adaptador.Fill(tabla);

                    return tabla;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "No se pudieron consultar los departamentos.", ex);
            }
        }

        public DataTable ListarMunicipios(int idDepartamento)
        {
            const string sql = @"
                SELECT IdMunicipio, NombreMunicipio
                FROM dbo.MUNICIPIO
                WHERE IdDepartamento = @IdDepartamento
                ORDER BY NombreMunicipio;";

            try
            {
                using (SqlConnection conexion = Conexion.ObtenerConexion())
                using (SqlCommand comando = new SqlCommand(sql, conexion))
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    comando.Parameters.Add(
                        "@IdDepartamento", SqlDbType.Int
                    ).Value = idDepartamento;

                    DataTable tabla = new DataTable();

                    conexion.Open();
                    adaptador.Fill(tabla);

                    return tabla;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "No se pudieron consultar los municipios.", ex);
            }
        }
    }
}