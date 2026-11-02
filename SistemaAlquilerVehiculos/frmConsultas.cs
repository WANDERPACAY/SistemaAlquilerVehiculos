using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SistemaAlquilerVehiculos
{
    public partial class frmConsultas : Form
    {
        private string conexionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AlquilerDeAutos.mdf;Integrated Security=True;";
        public frmConsultas()
        {
            InitializeComponent();
        }
        private void frmConsultas_Load(object sender, EventArgs e)
        {
            CargarEstadosContrato();
            CargarClientes();

           
            dtpDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpHasta.Value = DateTime.Now;
        }
private void CargarEstadosContrato()
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Todos");
            cmbEstado.Items.Add("Pendiente");
            cmbEstado.Items.Add("Activo");
            cmbEstado.Items.Add("Finalizado");
            cmbEstado.Items.Add("Anulado");
            cmbEstado.SelectedIndex = 0;
        }
        private void CargarClientes()
        {
            using (SqlConnection con = new SqlConnection(conexionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT IdCliente, CONCAT(Nombres, ' ', Apellidos, ' (DPI: ', DPI, ')') AS NombreCliente FROM CLIENTE";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    
                    DataRow row = dt.NewRow();
                    row["IdCliente"] = 0;
                    row["NombreCliente"] = "-- Todos los Clientes --";
                    dt.Rows.InsertAt(row, 0);

                    cmbCliente.DataSource = dt;
                    cmbCliente.DisplayMember = "NombreCliente";
                    cmbCliente.ValueMember = "IdCliente";
                    cmbCliente.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la lista de clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnConsultarContrato_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conexionString))
            {
                try
                {
                    con.Open();

                   
                    string query = @"SELECT 
                                        C.IdContrato,
                                        C.NumeroContrato AS [Nº Contrato],
                                        CONCAT(CL.Nombres, ' ', CL.Apellidos) AS [Cliente],
                                        CONCAT(V.Marca, ' ', V.Modelo, ' (', V.Placa, ')') AS [Vehículo],
                                        C.FechaSalida AS [Fecha Salida],
                                        C.FechaRetornoPactada AS [Fecha Retorno],
                                        C.DepositoGarantia AS [Depósito],
                                        C.EstadoContrato AS [Estado]
                                     FROM CONTRATO_ALQUILER C
                                     INNER JOIN CLIENTE CL ON C.IdCliente = CL.IdCliente
                                     INNER JOIN VEHICULO V ON C.IdVehiculo = V.IdVehiculo
                                     WHERE C.FechaSalida >= @FechaDesde AND C.FechaSalida <= @FechaHasta";

                    
                    if (cmbEstado.SelectedItem != null && cmbEstado.SelectedItem.ToString() != "Todos")
                    {
                        query += " AND C.EstadoContrato = @EstadoContrato";
                    }

                   
                    if (cmbCliente.SelectedValue != null && Convert.ToInt32(cmbCliente.SelectedValue) > 0)
                    {
                        query += " AND C.IdCliente = @IdCliente";
                    }

                    query += " ORDER BY C.FechaSalida DESC";

                    SqlCommand cmd = new SqlCommand(query, con);

                    
                    cmd.Parameters.AddWithValue("@FechaDesde", dtpDesde.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaHasta", dtpHasta.Value.Date.AddDays(1).AddTicks(-1));

                    if (cmbEstado.SelectedItem != null && cmbEstado.SelectedItem.ToString() != "Todos")
                    {
                        cmd.Parameters.AddWithValue("@EstadoContrato", cmbEstado.SelectedItem.ToString());
                    }

                    if (cmbCliente.SelectedValue != null && Convert.ToInt32(cmbCliente.SelectedValue) > 0)
                    {
                        cmd.Parameters.AddWithValue("@IdCliente", Convert.ToInt32(cmbCliente.SelectedValue));
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvContratos.DataSource = dt;

                   
                    lblTotal.Text = $"Total de Contratos Encontrados: {dt.Rows.Count}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al ejecutar la consulta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}
