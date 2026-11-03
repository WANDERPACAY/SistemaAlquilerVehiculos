using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAlquilerVehiculos.Datos;
using SistemaAlquilerVehiculos.Entidades;
using SistemaAlquilerVehiculos.Logica;
using SistemaAlquilerVehiculos.Utilidades;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    public partial class frmClientes : Form
    {
        private readonly UbicacionDatos ubicacionDatos = new UbicacionDatos();
        private readonly ClienteDatos clienteDatos = new ClienteDatos();
        private string rutaFotoLicenciaOrigen = null;
        private string rutaFotoLicenciaGuardada = null;
        private int idClienteSeleccionado = 0;

        private bool cargandoUbicacion = false;
        public frmClientes()
        {
            InitializeComponent();
            Load += InicializarClientes;
            cmbDepartamento.SelectedValueChanged += CambiarDepartamento;
            btnBuscar.Click += BuscarClientes;
            btnMostrarTodos.Click += MostrarTodosClientes;
            btnFotoLicencia.Click += SeleccionarFotoLicencia;
            btnGuardar.Click += GuardarCliente;
            btnLimpiar.Click += LimpiarCliente;
            dgvClientes.CellClick += SeleccionarCliente;
            btnEditar.Click += EditarCliente;
            btnEliminar.Click += EliminarCliente;

            // Permitir representar registros antiguos sin fecha.
            dtpVencimientoLicencia.ShowCheckBox = true;
            dtpVencimientoLicencia.Checked = true;

            FormClosed += (sender, e) =>
            {
                Image anterior = picLicencia.Image;
                picLicencia.Image = null;

                if (anterior != null)
                    anterior.Dispose();
            };
        }
        private void InicializarClientes(object sender, EventArgs e)
        {
            cmbMunicipio.Enabled = false;
            CargarClientes();
            try
            {
                cargandoUbicacion = true;

                cmbDepartamento.DisplayMember = "NombreDepartamento";
                cmbDepartamento.ValueMember = "IdDepartamento";
                cmbDepartamento.DataSource =
                    ubicacionDatos.ListarDepartamentos();

                cmbDepartamento.SelectedIndex = -1;
                cmbMunicipio.DataSource = null;
            }
            catch (Exception ex)
            {
                cmbDepartamento.DataSource = null;
                cmbMunicipio.DataSource = null;

                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al cargar departamentos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                cargandoUbicacion = false;
            }
        }
        private void EditarCliente(object sender, EventArgs e)
        {
            if (idClienteSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Selecciona un cliente del listado.",
                    "Editar cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                Cliente cliente = LeerCliente();
                cliente.IdCliente = idClienteSeleccionado;

                ClienteLogica.Validar(cliente);

                if (Sesion.IdUsuario <= 0)
                    throw new InvalidOperationException(
                        "Debes iniciar sesión para editar clientes.");

                if (cliente.FechaVencLicencia.Value.Date < DateTime.Today)
                {
                    DialogResult respuesta = MessageBox.Show(
                        "La licencia está vencida. El cliente podrá actualizarse, " +
                        "pero no debe realizar alquileres hasta renovarla.\n\n" +
                        "¿Deseas continuar?",
                        "Licencia vencida",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    if (respuesta != DialogResult.Yes)
                        return;
                }

                // Si no se eligió otra foto, conservar la ruta existente.
                if (string.IsNullOrWhiteSpace(rutaFotoLicenciaGuardada))
                {
                    rutaFotoLicenciaGuardada =
                        FotosLicencias.Copiar(rutaFotoLicenciaOrigen);
                }

                cliente.FotoLicenciaRuta = rutaFotoLicenciaGuardada;

                ClienteLogica.Editar(cliente, Sesion.IdUsuario);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Revisa los datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "No se pudo editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al editar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Cliente actualizado correctamente.",
                "Edición exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtBuscar.Clear();
            CargarClientes();
        }
        private Cliente LeerCliente()
        {
            return new Cliente
            {
                DPI = txtDPI.Text,
                Nombres = txtNombres.Text,
                Apellidos = txtApellidos.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDireccion.Text,

                IdMunicipio = cmbMunicipio.SelectedValue is int
                    ? (int?)cmbMunicipio.SelectedValue
                    : null,

                NumeroLicencia = txtNumeroLicencia.Text,

                TipoLicencia = cmbTipoLicencia.SelectedItem == null
                    ? ""
                    : cmbTipoLicencia.SelectedItem.ToString(),

                FechaVencLicencia = dtpVencimientoLicencia.Checked
    ? (DateTime?)dtpVencimientoLicencia.Value.Date
    : null,

                FotoLicenciaRuta = rutaFotoLicenciaGuardada ??
                    (string.IsNullOrWhiteSpace(rutaFotoLicenciaOrigen)
                        ? null
                        : "pendiente")
            };
        }

        private void GuardarCliente(object sender, EventArgs e)
        {
            if (idClienteSeleccionado > 0)
            {
                MessageBox.Show(
                    "Pulsa Limpiar para registrar un cliente nuevo.",
                    "Cliente seleccionado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }
            try
            {
                Cliente cliente = LeerCliente();

                // Validar antes de copiar la fotografía.
                ClienteLogica.Validar(cliente);

                if (Sesion.IdUsuario <= 0)
                    throw new InvalidOperationException(
                        "Debes iniciar sesión para registrar clientes.");

                if (cliente.FechaVencLicencia.Value.Date < DateTime.Today)
                {
                    DialogResult respuesta = MessageBox.Show(
                        "La licencia está vencida. El cliente podrá registrarse, " +
                        "pero no debe realizar alquileres hasta renovarla.\n\n" +
                        "¿Deseas continuar con el registro?",
                        "Licencia vencida",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);

                    if (respuesta != DialogResult.Yes)
                        return;
                }

                if (string.IsNullOrWhiteSpace(rutaFotoLicenciaGuardada))
                {
                    rutaFotoLicenciaGuardada =
                        FotosLicencias.Copiar(rutaFotoLicenciaOrigen);
                }

                cliente.FotoLicenciaRuta = rutaFotoLicenciaGuardada;

                ClienteLogica.Guardar(cliente, Sesion.IdUsuario);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Revisa los datos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "No se pudo guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Cliente registrado correctamente.",
                "Registro exitoso",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarCamposCliente();
            txtBuscar.Clear();
            CargarClientes();
        }

        private void LimpiarCliente(object sender, EventArgs e)
        {
            LimpiarCamposCliente();
        }

        private void LimpiarCamposCliente()
        {
            idClienteSeleccionado = 0;
            txtDPI.Clear();
            txtNombres.Clear();
            txtApellidos.Clear();
            txtTelefono.Clear();
            txtDireccion.Clear();
            txtNumeroLicencia.Clear();

            cmbDepartamento.SelectedIndex = -1;
            cmbMunicipio.DataSource = null;
            cmbMunicipio.Enabled = false;
            cmbTipoLicencia.SelectedIndex = -1;

            dtpVencimientoLicencia.Value = DateTime.Today;
            dtpVencimientoLicencia.Checked = true;

            rutaFotoLicenciaOrigen = null;
            rutaFotoLicenciaGuardada = null;

            Image anterior = picLicencia.Image;
            picLicencia.Image = null;

            if (anterior != null)
                anterior.Dispose();

            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;

            dgvClientes.ClearSelection();
            txtDPI.Focus();
        }
        private void SeleccionarCliente(
    object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];

            if (fila.IsNewRow)
                return;

            // Limpiar cualquier fotografía o selección anterior.
            LimpiarCamposCliente();

            idClienteSeleccionado =
                Convert.ToInt32(fila.Cells["IdCliente"].Value);

            txtDPI.Text = Convert.ToString(fila.Cells["DPI"].Value);
            txtNombres.Text = Convert.ToString(fila.Cells["Nombres"].Value);
            txtApellidos.Text = Convert.ToString(fila.Cells["Apellidos"].Value);
            txtTelefono.Text = Convert.ToString(fila.Cells["Telefono"].Value);
            txtDireccion.Text = Convert.ToString(fila.Cells["Direccion"].Value);

            txtNumeroLicencia.Text =
                Convert.ToString(fila.Cells["NumeroLicencia"].Value);

            cmbTipoLicencia.SelectedItem =
                Convert.ToString(fila.Cells["TipoLicencia"].Value);

            if (fila.Cells["IdDepartamento"].Value != DBNull.Value)
            {
                // El evento carga los municipios del departamento.
                cmbDepartamento.SelectedValue =
                    Convert.ToInt32(fila.Cells["IdDepartamento"].Value);

                if (fila.Cells["IdMunicipio"].Value != DBNull.Value)
                {
                    cmbMunicipio.SelectedValue =
                        Convert.ToInt32(fila.Cells["IdMunicipio"].Value);
                }
            }

            object fecha = fila.Cells["FechaVencLicencia"].Value;

            if (fecha != DBNull.Value && fecha != null)
            {
                dtpVencimientoLicencia.Value = Convert.ToDateTime(fecha);
                dtpVencimientoLicencia.Checked = true;
            }
            else
            {
                dtpVencimientoLicencia.Checked = false;
            }

            rutaFotoLicenciaGuardada =
                Convert.ToString(fila.Cells["FotoLicenciaRuta"].Value);

            try
            {
                picLicencia.Image =
                    FotosLicencias.Cargar(rutaFotoLicenciaGuardada);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Los datos se cargaron, pero no se pudo abrir " +
                    "la fotografía: " + ex.Message,
                    "Fotografía de licencia",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            fila.Selected = true;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;

            btnEliminar.Enabled = true;
        }
        private void EliminarCliente(object sender, EventArgs e)
        {
            if (idClienteSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Selecciona un cliente del listado.",
                    "Eliminar cliente",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el cliente seleccionado?\n\n" +
                "Esta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                ClienteLogica.Eliminar(
                    idClienteSeleccionado, Sesion.IdUsuario);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Revisa la selección",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Eliminación bloqueada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al eliminar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Cliente eliminado correctamente.",
                "Eliminación exitosa",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            txtBuscar.Clear();
            CargarClientes();
        }
        private void SeleccionarFotoLicencia(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccionar fotografía de la licencia";
                dialogo.Filter =
                    "Imágenes JPG y PNG|*.jpg;*.jpeg;*.png";
                dialogo.CheckFileExists = true;
                dialogo.Multiselect = false;

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    Image nueva = FotosLicencias.Cargar(dialogo.FileName);
                    Image anterior = picLicencia.Image;

                    picLicencia.Image = nueva;
                    rutaFotoLicenciaOrigen = dialogo.FileName;

                    if (anterior != null)
                        anterior.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo abrir la imagen: " + ex.Message,
                        "Fotografía de licencia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
        private void CargarClientes()
        {
            try
            {
                DataTable tabla = clienteDatos.Listar(txtBuscar.Text);

                dgvClientes.AutoGenerateColumns = true;
                dgvClientes.DataSource = tabla;

                // Conservar estos datos para seleccionar y editar después.
                dgvClientes.Columns["IdCliente"].Visible = false;
                dgvClientes.Columns["IdDepartamento"].Visible = false;
                dgvClientes.Columns["IdMunicipio"].Visible = false;
                dgvClientes.Columns["FotoLicenciaRuta"].Visible = false;

                dgvClientes.Columns["Telefono"].HeaderText = "Teléfono";
                dgvClientes.Columns["Direccion"].HeaderText = "Dirección";
                dgvClientes.Columns["NumeroLicencia"].HeaderText =
                    "Número de licencia";
                dgvClientes.Columns["TipoLicencia"].HeaderText =
                    "Tipo de licencia";
                dgvClientes.Columns["FechaVencLicencia"].HeaderText =
                    "Vencimiento";

                dgvClientes.Columns["FechaVencLicencia"]
                    .DefaultCellStyle.Format = "dd/MM/yyyy";

                foreach (DataGridViewColumn columna in dgvClientes.Columns)
                {
                    columna.SortMode = DataGridViewColumnSortMode.Automatic;
                }

                LimpiarCamposCliente();

                lblTotal.Text = "Total de clientes: " + tabla.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al cargar clientes",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BuscarClientes(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void MostrarTodosClientes(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarClientes();
        }
        private void CambiarDepartamento(object sender, EventArgs e)
        {
            if (cargandoUbicacion)
                return;

            // Descarta el municipio del departamento anterior.
            cmbMunicipio.DataSource = null;
            cmbMunicipio.Enabled = false;

            if (!(cmbDepartamento.SelectedValue is int))
                return;

            int idDepartamento = (int)cmbDepartamento.SelectedValue;

            try
            {
                DataTable municipios =
                    ubicacionDatos.ListarMunicipios(idDepartamento);

                cmbMunicipio.DisplayMember = "NombreMunicipio";
                cmbMunicipio.ValueMember = "IdMunicipio";
                cmbMunicipio.DataSource = municipios;
                cmbMunicipio.SelectedIndex = -1;

                cmbMunicipio.Enabled = municipios.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                cmbMunicipio.DataSource = null;
                cmbMunicipio.Enabled = false;

                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al cargar municipios",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
