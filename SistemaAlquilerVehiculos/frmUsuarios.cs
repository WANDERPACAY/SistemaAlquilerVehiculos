using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SistemaAlquilerVehiculos;

namespace SistemaAlquilerVehiculos
{
    public partial class frmUsuarios : Form
    {
        private int idUsuarioSeleccionado = 0;
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            CargarRolesComboBox();
        }
        private void CargarUsuarios()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = @"SELECT u.IdUsuario, u.NombreCompleto, u.Username, r.NombreRol, 
                                     CASE WHEN u.Activo = 1 THEN 'Sí' ELSE 'No' END AS Activo, u.IdRol
                                     FROM USUARIO u
                                     INNER JOIN ROL r ON u.IdRol = r.IdRol";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUsuarios.DataSource = dt;

                    if (dgvUsuarios.Columns["IdRol"] != null)
                        dgvUsuarios.Columns["IdRol"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los usuarios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarRolesComboBox()
        {
            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = "SELECT IdRol, NombreRol FROM ROL";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbRol.DataSource = dt;
                    cmbRol.DisplayMember = "NombreRol";
                    cmbRol.ValueMember = "IdRol";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
        string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Por favor, complete los campos obligatorios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int activo = chkActivo.Checked ? 1 : 0;

            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    con.Open();


                    if (idUsuarioSeleccionado == 0)
                    {
                        if (string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            MessageBox.Show("La contraseña es obligatoria para nuevos usuarios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string passHash = CalcularSHA256(txtPassword.Text.Trim());
                        string queryInsert = "INSERT INTO USUARIO (NombreCompleto, Username, PasswordHash, Activo, IdRol) VALUES (@nombre, @user, @pass, @activo, @rol)";

                        using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                        {
                            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                            cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                            cmd.Parameters.AddWithValue("@pass", passHash);
                            cmd.Parameters.AddWithValue("@activo", activo);
                            cmd.Parameters.AddWithValue("@rol", cmbRol.SelectedValue);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    else
                    {
                        string queryUpdate = "";

                        if (string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            queryUpdate = "UPDATE USUARIO SET NombreCompleto = @nombre, Username = @user, Activo = @activo, IdRol = @rol WHERE IdUsuario = @id";
                            using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                            {
                                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                                cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                                cmd.Parameters.AddWithValue("@activo", activo);
                                cmd.Parameters.AddWithValue("@rol", cmbRol.SelectedValue);
                                cmd.Parameters.AddWithValue("@id", idUsuarioSeleccionado);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else 
                        {
                            string passHash = CalcularSHA256(txtPassword.Text.Trim());
                            queryUpdate = "UPDATE USUARIO SET NombreCompleto = @nombre, Username = @user, PasswordHash = @pass, Activo = @activo, IdRol = @rol WHERE IdUsuario = @id";
                            using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                            {
                                cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                                cmd.Parameters.AddWithValue("@user", txtUsername.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", passHash);
                                cmd.Parameters.AddWithValue("@activo", activo);
                                cmd.Parameters.AddWithValue("@rol", cmbRol.SelectedValue);
                                cmd.Parameters.AddWithValue("@id", idUsuarioSeleccionado);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    LimpiarFormulario();
                    CargarUsuarios();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idUsuarioSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un usuario de la tabla para eliminar o desactivar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult resultado = MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = Conexion.ObtenerConexion())
                    {
                        string query = "DELETE FROM USUARIO WHERE IdUsuario = @id";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@id", idUsuarioSeleccionado);
                            con.Open();
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Usuario eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpiarFormulario();
                            CargarUsuarios();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se puede eliminar el usuario porque tiene contratos o registros relacionados en el sistema.", "Restricción de Integridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUsuarios.Rows[e.RowIndex];
                idUsuarioSeleccionado = Convert.ToInt32(row.Cells["IdUsuario"].Value);
                txtNombre.Text = row.Cells["NombreCompleto"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                cmbRol.SelectedValue = row.Cells["IdRol"].Value;
                chkActivo.Checked = row.Cells["Activo"].Value.ToString() == "Sí";
                txtPassword.Clear();
            }
        }
        private string CalcularSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void LimpiarFormulario()
        {
            idUsuarioSeleccionado = 0;
            txtNombre.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtNombre.Focus();
        }

    }
}
