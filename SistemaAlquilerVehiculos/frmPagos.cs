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
    public partial class frmPagos : Form
    {
        private string conexionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AlquilerDeAutos.mdf;Integrated Security=True;";
        public frmPagos()
        {
            InitializeComponent();
        }
        private void frmPagos_Load(object sender, EventArgs e)
        {
            CargarContratos();
            CargarComboTipoCobro();
            CargarComboMetodoPago();
        }
        private void CargarContratos()
        {
            using (SqlConnection con = new SqlConnection(conexionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT IdContrato, NumeroContrato FROM CONTRATO_ALQUILER WHERE EstadoContrato != 'Anulado'";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbContrato.DataSource = dt;
                    cmbContrato.DisplayMember = "NumeroContrato";
                    cmbContrato.ValueMember = "IdContrato";
                    cmbContrato.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar contratos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void CargarComboTipoCobro()
        {
            cmbTipoCobro.Items.Clear();
            cmbTipoCobro.Items.Add("Anticipo / Depósito");
            cmbTipoCobro.Items.Add("Renta Total");
            cmbTipoCobro.Items.Add("Mora / Penalización");
            cmbTipoCobro.Items.Add("Daños / Reparaciones");
        }
        private void CargarComboMetodoPago()
        {
            cmbMetodoPago.Items.Clear();
            cmbMetodoPago.Items.Add("Efectivo");
            cmbMetodoPago.Items.Add("Tarjeta de Crédito/Débito");
            cmbMetodoPago.Items.Add("Transferencia Bancaria");
        }
        private void CargarHistorialPagos(int idContrato)
        {
            using (SqlConnection con = new SqlConnection(conexionString))
            {
                try
                {
                    con.Open();
                    string query = @"SELECT IdPago, FechaPago, Monto, TipoCobro, MetodoPago, Observaciones 
                                    FROM PAGO_DETALLE 
                                    WHERE IdContrato = @IdContrato 
                                    ORDER BY FechaPago DESC";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdContrato", idContrato);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPagos.DataSource = dt;

                    
                    decimal total = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        total += Convert.ToDecimal(row["Monto"]);
                    }
                    lblTotal.Text = "Total Cobrado: Q" + total.ToString("N2");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el historial de pagos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void cmbContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbContrato.SelectedValue != null && int.TryParse(cmbContrato.SelectedValue.ToString(), out int idContrato))
            {
                CargarHistorialPagos(idContrato);
            }
        }
        private void btnRegistrarPago_Click(object sender, EventArgs e)
        {
            if (cmbContrato.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un contrato válido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(txtMonto.Text, out decimal monto) || monto <= 0)
            {
                MessageBox.Show("Ingrese un monto válido mayor a 0.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection con = new SqlConnection(conexionString))
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO PAGO_DETALLE (IdContrato, FechaPago, Monto, TipoCobro, MetodoPago, Observaciones) 
                                    VALUES (@IdContrato, @FechaPago, @Monto, @TipoCobro, @MetodoPago, @Observaciones)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IdContrato", Convert.ToInt32(cmbContrato.SelectedValue));
                    cmd.Parameters.AddWithValue("@FechaPago", dtpFechaPago.Value);
                    cmd.Parameters.AddWithValue("@Monto", monto);
                    cmd.Parameters.AddWithValue("@TipoCobro", cmbTipoCobro.Text);
                    cmd.Parameters.AddWithValue("@MetodoPago", cmbMetodoPago.Text);
                    cmd.Parameters.AddWithValue("@Observaciones", txtObservaciones.Text.Trim());

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Pago registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    CargarHistorialPagos(Convert.ToInt32(cmbContrato.SelectedValue));
                    LimpiarCamposEntrada();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPagos.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un pago del historial para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPago = Convert.ToInt32(dgvPagos.CurrentRow.Cells["IdPago"].Value);

            if (MessageBox.Show("¿Está seguro de eliminar este registro de pago?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    try
                    {
                        con.Open();
                        string query = "DELETE FROM PAGO_DETALLE WHERE IdPago = @IdPago";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@IdPago", idPago);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Registro de pago eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarHistorialPagos(Convert.ToInt32(cmbContrato.SelectedValue));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al eliminar pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            cmbContrato.SelectedIndex = -1;
            LimpiarCamposEntrada();
            dgvPagos.DataSource = null;
            lblTotal.Text = "Total Cobrado: Q0.00";
        }

        private void LimpiarCamposEntrada()
        {
            cmbTipoCobro.SelectedIndex = -1;
            cmbMetodoPago.SelectedIndex = -1;
            dtpFechaPago.Value = DateTime.Now;
            txtMonto.Clear();
            txtObservaciones.Clear();
        }
    }
}

        
    


