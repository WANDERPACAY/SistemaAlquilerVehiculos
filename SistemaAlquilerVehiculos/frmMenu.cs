using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void AbrirFormularioMdi<T>() where T : Form, new()
        {
            string nombreForm = typeof(T).Name;
            Form frmExistente = Application.OpenForms[nombreForm];

            if (frmExistente != null)
            {
                frmExistente.BringToFront();
                frmExistente.WindowState = FormWindowState.Maximized;
                return;
            }

            T nuevoForm = new T();
            nuevoForm.MdiParent = this;
            nuevoForm.WindowState = FormWindowState.Maximized;
            nuevoForm.Show();
        }

        private void vehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmVehiculos>();
        }

        private void mantenimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmMantenimientos>();
        

        }

        private void xxdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmClientes>();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmContratos>();
        }

        private void devolucionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmDevoluciones>();
        }

        private void pagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmPagos>();
        }

        private void xdToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmConsultas>();
        }

        private void xdToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmReportes>();
        }

        private void xdToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmDashboard>();
        }
        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmUsuarios>();
        }

        private void bitacoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmBitacora>();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void salirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir del sistema?", "Confirmar salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel; // Cierra todo por completo
                this.Close();
            }
        }

        private void xdToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void galeriaVehiculosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioMdi<frmGaleriaVehiculos>();
        }
    }
    }
