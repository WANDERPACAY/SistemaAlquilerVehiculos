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

namespace SistemaAlquilerVehiculos
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtContrasena.Text))
            {
                MessageBox.Show("Por favor, ingrese el usuario y la contraseña.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string passHash = CalcularSHA256(txtContrasena.Text);

            try
            {
                using (SqlConnection con = Conexion.ObtenerConexion())
                {
                    string query = @"SELECT u.IdUsuario, u.NombreCompleto, u.Username, r.NombreRol, u.Activo 
                                     FROM USUARIO u 
                                     INNER JOIN ROL r ON u.IdRol = r.IdRol 
                                     WHERE u.Username = @usuario AND u.PasswordHash = @pass";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                        cmd.Parameters.AddWithValue("@pass", passHash);

                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                bool activo = reader.GetBoolean(4);
                                if (!activo)
                                {
                                    MessageBox.Show("El usuario se encuentra inactivo. Contacte al administrador.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                Sesion.IdUsuario = reader.GetInt32(0);
                                Sesion.NombreCompleto = reader.GetString(1);
                                Sesion.Username = reader.GetString(2);
                                Sesion.Rol = reader.GetString(3);

                                this.DialogResult = DialogResult.OK;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
