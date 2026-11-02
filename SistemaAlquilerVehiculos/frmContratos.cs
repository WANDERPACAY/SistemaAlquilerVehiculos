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
using System.Linq.Expressions;

namespace SistemaAlquilerVehiculos
{
    public partial class frmContratos : Form
    {
        private string conexionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\HP\Source\Repos\SistemaAlquilerVehiculos\SistemaAlquilerVehiculos\AlquilerDeAutos.mdf;Integrated Security = True";
        public frmContratos()
        {
            InitializeComponent();
        }
        private void LimpiarCampos()
        {
            txtNumeroContrato.Clear();
            cmbClientes.SelectedIndex = -1;
            cmbVehiculos.SelectedIndex = -1;
            dtpSalida.Value = DateTime.Now;
            dtpRetorno.Value = DateTime.Now;
            txtDias.Clear();
            txtTarifa.Clear();
            txtKmInicial.Clear();
            txtDeposito.Clear();
            txtTotal.Clear();
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text))
            {
                MessageBox.Show("Ingrese o seleccione un número de contrato para aprobar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection con = new SqlConnection(conexionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();

                try
                {

                    string updateContrato = "UPDATE CONTRATO_ALQUILER SET EstadoContrato = 'Activo' WHERE NumeroContrato = @NumeroContrato";
                    SqlCommand cmdContrato = new SqlCommand(updateContrato, con, transaction);
                    cmdContrato.Parameters.AddWithValue("@NumeroContrato", txtNumeroContrato.Text.Trim());
                    cmdContrato.ExecuteNonQuery();


                    string updateVehiculo = "UPDATE VEHICULO SET Estado = 'Alquilado' WHERE IdVehiculo = @IdVehiculo";
                    SqlCommand cmdVehiculo = new SqlCommand(updateVehiculo, con, transaction);
                    cmdVehiculo.Parameters.AddWithValue("@IdVehiculo", Convert.ToInt32(cmbVehiculos.SelectedValue));
                    cmdVehiculo.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Contrato aprobado y vehículo marcado como Alquilado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error al aprobar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnBuscarDisponibles_Click(object sender, EventArgs e)
        {
            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT * FROM VEHICULO WHERE Estado = 'Disponible'";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conexion);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cmbVehiculos.DataSource = dt;
                    cmbVehiculos.DisplayMember = "VehiculoInfo";
                    cmbVehiculos.ValueMember = "IdVehiculo";

                    MessageBox.Show("Vehiculos Disponibles Cargados Correcrtamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los vehículos disponibles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text) || cmbClientes.SelectedValue == null || cmbVehiculos.SelectedValue == null)
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection con = new SqlConnection(conexionString))
            {
                try
                {
                    con.Open();
                    string query = @"INSERT INTO CONTRATO_ALQUILER 
                            (NumeroContrato, IdCliente, IdVehiculo, IdUsuario, FechaSalida, FechaRetornoPactada, KilometrajeInicial, DepositoGarantia, EstadoContrato) 
                            VALUES 
                            (@NumeroContrato, @IdCliente, @IdVehiculo, @IdUsuario, @FechaSalida, @FechaRetornoPactada, @KilometrajeInicial, @DepositoGarantia, 'Pendiente')";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@NumeroContrato", txtNumeroContrato.Text.Trim());
                    cmd.Parameters.AddWithValue("@IdCliente", Convert.ToInt32(cmbClientes.SelectedValue));
                    cmd.Parameters.AddWithValue("@IdVehiculo", Convert.ToInt32(cmbVehiculos.SelectedValue));
                    cmd.Parameters.AddWithValue("@IdUsuario", 1);
                    cmd.Parameters.AddWithValue("@FechaSalida", dtpSalida.Value);
                    cmd.Parameters.AddWithValue("@FechaRetornoPactada", dtpRetorno.Value);
                    cmd.Parameters.AddWithValue("@KilometrajeInicial", Convert.ToInt32(txtKmInicial.Text));
                    cmd.Parameters.AddWithValue("@DepositoGarantia", Convert.ToDecimal(txtDeposito.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Contrato guardado en estado 'Pendiente'.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text)) return;

            if (MessageBox.Show("¿Está seguro de anular este contrato?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection con = new SqlConnection(conexionString))
                {
                    try
                    {
                        con.Open();
                        string query = "UPDATE CONTRATO_ALQUILER SET EstadoContrato = 'Anulado' WHERE NumeroContrato = @NumeroContrato";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@NumeroContrato", txtNumeroContrato.Text.Trim());
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("El contrato ha sido anulado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al anular contrato: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text)) return;

    using (SqlConnection con = new SqlConnection(conexionString))
    {
        con.Open();
        SqlTransaction transaction = con.BeginTransaction();

        try
        {
            
            string updateContrato = @"UPDATE CONTRATO_ALQUILER 
                                    SET FechaRetornoReal = @FechaRetornoReal, 
                                        KilometrajeFinal = @KmFinal, 
                                        EstadoContrato = 'Finalizado' 
                                    WHERE NumeroContrato = @NumeroContrato";

            SqlCommand cmdContrato = new SqlCommand(updateContrato, con, transaction);
            cmdContrato.Parameters.AddWithValue("@FechaRetornoReal", DateTime.Now);
            cmdContrato.Parameters.AddWithValue("@KmFinal", Convert.ToInt32(txtKmInicial.Text)); 
            cmdContrato.Parameters.AddWithValue("@NumeroContrato", txtNumeroContrato.Text.Trim());
            cmdContrato.ExecuteNonQuery();

            
            string updateVehiculo = "UPDATE VEHICULO SET Estado = 'Disponible', KilometrajeActual = @KmFinal WHERE IdVehiculo = @IdVehiculo";
            SqlCommand cmdVehiculo = new SqlCommand(updateVehiculo, con, transaction);
            cmdVehiculo.Parameters.AddWithValue("@KmFinal", Convert.ToInt32(txtKmInicial.Text));
            cmdVehiculo.Parameters.AddWithValue("@IdVehiculo", Convert.ToInt32(cmbVehiculos.SelectedValue));
            cmdVehiculo.ExecuteNonQuery();

            transaction.Commit();
            MessageBox.Show("Devolución registrada con éxito. El vehículo vuelve a estar disponible.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarCampos();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            MessageBox.Show("Error al registrar devolución: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
 if (string.IsNullOrWhiteSpace(txtNumeroContrato.Text))
    {
        MessageBox.Show("Seleccione o ingrese un contrato para generar el reporte.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    MessageBox.Show($"Generando reporte PDF para el contrato {txtNumeroContrato.Text}...", "Exportar a PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
    
}
        }
    }



