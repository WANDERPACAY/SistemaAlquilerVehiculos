using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    public partial class frmBitacora : Form
    {
        public frmBitacora()
        {
            InitializeComponent();
        }

        private void frmBitacora_Load(object sender, EventArgs e)
        {
            if (cmbModulo.Items.Count > 0)
                cmbModulo.SelectedIndex = 0;

            CargarBitacora();
        }
        private void CargarBitacora()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "SELECT TOP 100 FechaOperacion, TipoOperacion, Tabla, IdRegistro, Detalle FROM BITACORA ORDER BY FechaOperacion DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBitacora.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la bitácora: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    // Consulta base con filtro obligatorio de fechas
                    string query = @"SELECT FechaOperacion, TipoOperacion, Tabla, IdRegistro, Detalle 
                                     FROM BITACORA 
                                     WHERE FechaOperacion >= @fechaInicio AND FechaOperacion < DATEADD(day, 1, @fechaFin)";

                    // Si seleccionaron un módulo específico (y no es "Todos"), filtramos por la columna Tabla
                    bool filtrarPorModulo = cmbModulo.SelectedItem != null && cmbModulo.SelectedItem.ToString() != "Todos";

                    if (filtrarPorModulo)
                    {
                        query += " AND Tabla = @modulo";
                    }

                    query += " ORDER BY FechaOperacion DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add("@fechaInicio", SqlDbType.DateTime).Value = dtpInicio.Value.Date;
                        cmd.Parameters.Add("@fechaFin", SqlDbType.DateTime).Value = dtpFin.Value.Date;

                        if (filtrarPorModulo)
                        {
                            cmd.Parameters.AddWithValue("@modulo", cmbModulo.SelectedItem.ToString());
                        }

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvBitacora.DataSource = dt;

                        if (dt.Rows.Count == 0)
                        {
                            MessageBox.Show("No se encontraron registros de bitácora con los filtros seleccionados.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar la bitácora: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
