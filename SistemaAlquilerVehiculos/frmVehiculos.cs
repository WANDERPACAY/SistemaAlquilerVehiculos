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
using System.Globalization;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    public partial class frmVehiculos : Form
    {
        public frmVehiculos()
        {
            InitializeComponent();
            Load += (sender, e) => InicializarListado();
        }

        private readonly VehiculoDatos  vehiculoDatos = new VehiculoDatos(); private int idVehiculoSeleccionado = 0; private string rutaFotoActual = null;

        private void InicializarListado()
        {
            cmbFiltroTipo.Items.Clear();
            cmbFiltroTipo.Items.AddRange(
                new object[] { "Todos", "Sedán", "SUV", "Pickup" });

            cmbFiltroTransmision.Items.Clear();
            cmbFiltroTransmision.Items.AddRange(
                new object[] { "Todas", "Mecánica", "Automática" });

            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTransmision.SelectedIndex = 0;

            dgvVehiculos.AutoGenerateColumns = true;
            dgvVehiculos.ReadOnly = true;
            dgvVehiculos.AllowUserToAddRows = false;

            btnBuscar.Click += BuscarVehiculos;
            btnMostrarTodos.Click += MostrarTodosVehiculos;

            btnGuardar.Click += GuardarVehiculo;
            btnLimpiar.Click += LimpiarVehiculo;
            dgvVehiculos.CellClick += SeleccionarVehiculo;

            btnFoto.Click += SeleccionarFotoVehiculo;
            btnEliminar.Click += EliminarVehiculo;
            btnEditar.Click += EditarVehiculo;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            CargarVehiculos();
        }
        private void MostrarFotoVehiculo(string ruta)
        {
            Image nuevaImagen = FotosVehiculos.Cargar(ruta);

            Image anterior = picVehiculo.Image;
            picVehiculo.Image = nuevaImagen;

            if (anterior != null)
                anterior.Dispose();
        }

        private void SeleccionarFotoVehiculo(object sender, EventArgs e)
        {
            using (OpenFileDialog dialogo = new OpenFileDialog())
            {
                dialogo.Title = "Seleccionar fotografía del vehículo";
                dialogo.Filter =
                    "Imágenes JPG y PNG|*.jpg;*.jpeg;*.png";
                dialogo.Multiselect = false;

                if (dialogo.ShowDialog() != DialogResult.OK)
                    return;

                try
                {
                    string nuevaRuta = FotosVehiculos.Copiar(dialogo.FileName);

                    MostrarFotoVehiculo(nuevaRuta);
                    rutaFotoActual = nuevaRuta;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "No se pudo cargar la fotografía.\n" + ex.Message,
                        "Fotografía del vehículo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }
        private void EliminarVehiculo(object sender, EventArgs e)
        {
            if (idVehiculoSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Selecciona un vehículo del listado.",
                    "Eliminar vehículo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            DialogResult respuesta = MessageBox.Show(
                "¿Deseas eliminar el vehículo seleccionado?\n" +
                "Esta operación elimina su registro de la flota.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (respuesta != DialogResult.Yes)
                return;

            try
            {
                VehiculoLogica.Eliminar(
                    idVehiculoSeleccionado, Sesion.IdUsuario);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message, "Eliminación bloqueada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Vehículo eliminado correctamente.",
                "Eliminación completada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            MostrarTodosVehiculos(sender, e);
        }
        private void EditarVehiculo(object sender, EventArgs e)
        {
            if (idVehiculoSeleccionado <= 0)
            {
                MessageBox.Show(
                    "Selecciona un vehículo del listado.",
                    "Editar vehículo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Vehiculo vehiculo = LeerVehiculo();
                vehiculo.IdVehiculo = idVehiculoSeleccionado;

                VehiculoLogica.Editar(vehiculo, Sesion.IdUsuario);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message, "Revisa los datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message, "No se pudo editar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al editar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Vehículo actualizado correctamente.",
                "Edición completada",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            MostrarTodosVehiculos(sender, e);
        }
        private void SeleccionarVehiculo(
    object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow fila = dgvVehiculos.Rows[e.RowIndex];

            if (fila.IsNewRow)
                return;

            idVehiculoSeleccionado = Convert.ToInt32(
                fila.Cells["IdVehiculo"].Value);

            txtPlaca.Text = Convert.ToString(fila.Cells["Placa"].Value);
            txtMarca.Text = Convert.ToString(fila.Cells["Marca"].Value);
            txtModelo.Text = Convert.ToString(fila.Cells["Modelo"].Value);
            txtAnio.Text = Convert.ToString(fila.Cells["Anio"].Value);

            cmbTipoVehiculo.SelectedIndex = -1;
            cmbTransmision.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;

            cmbTipoVehiculo.SelectedItem =
                Convert.ToString(fila.Cells["TipoVehiculo"].Value);

            cmbTransmision.SelectedItem =
                Convert.ToString(fila.Cells["Transmision"].Value);

            cmbEstado.SelectedItem =
                Convert.ToString(fila.Cells["Estado"].Value);

            object tarifa = fila.Cells["TarifaDiaria"].Value;

            txtTarifaDiaria.Text =
                tarifa == null || tarifa == DBNull.Value
                ? ""
                : Convert.ToDecimal(tarifa).ToString(
                    "0.00", CultureInfo.InvariantCulture);

            txtKilometrajeActual.Text = Convert.ToString(
                fila.Cells["KilometrajeActual"].Value);

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;

            rutaFotoActual = Convert.ToString(
    fila.Cells["RutaFotografia"].Value);

            try
            {
                MostrarFotoVehiculo(rutaFotoActual);
            }
            catch (Exception ex)
            {
                // Retira la foto anterior si la del registro no puede abrirse.
                MostrarFotoVehiculo(null);

                MessageBox.Show(
                    "No se pudo mostrar la fotografía.\n" + ex.Message,
                    "Fotografía del vehículo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        private Vehiculo LeerVehiculo()
        {
            int anio;
            int kilometraje;
            decimal tarifa;

            string textoAnio = txtAnio.Text.Trim();

            if (textoAnio.Length != 4 ||
                !int.TryParse(
                    textoAnio,
                    NumberStyles.None,
                    CultureInfo.InvariantCulture,
                    out anio))
            {
                throw new ArgumentException(
                    "El año debe contener exactamente cuatro dígitos.");
            }

            if (!int.TryParse(txtKilometrajeActual.Text.Trim(), out kilometraje))
            {
                throw new ArgumentException(
                    "Ingresa un kilometraje entero válido.");
            }

            // Se utiliza punto decimal y no se permiten separadores de miles.
            if (!decimal.TryParse(
                txtTarifaDiaria.Text.Trim(),
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out tarifa))
            {
                throw new ArgumentException(
                    "Ingresa una tarifa válida. Ejemplo: 250.50, sin separadores de miles.");
            }

            return new Vehiculo
            {
                Placa = txtPlaca.Text,
                Marca = txtMarca.Text,
                Modelo = txtModelo.Text,
                Anio = anio,
                TipoVehiculo = cmbTipoVehiculo.SelectedItem == null
                    ? "" : cmbTipoVehiculo.SelectedItem.ToString(),
                Transmision = cmbTransmision.SelectedItem == null
                    ? "" : cmbTransmision.SelectedItem.ToString(),
                TarifaDiaria = tarifa,
                Estado = cmbEstado.SelectedItem == null
                    ? "" : cmbEstado.SelectedItem.ToString(),
                KilometrajeActual = kilometraje,
                RutaFotografia = rutaFotoActual
            };
        }

        private void GuardarVehiculo(object sender, EventArgs e)
        {
            int idVehiculo;

            try
            {
                Vehiculo vehiculo = LeerVehiculo();

                idVehiculo = VehiculoLogica.Guardar(
                    vehiculo, Sesion.IdUsuario);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message, "Revisa los datos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(
                    ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Error al guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(
                "Vehículo guardado correctamente. Código: " + idVehiculo,
                "Registro completado",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarCamposVehiculo();

            // Retirar los filtros permite ver el registro recién guardado.
            MostrarTodosVehiculos(sender, e);
        }

        private void LimpiarVehiculo(object sender, EventArgs e)
        {
            LimpiarCamposVehiculo();
        }

        private void LimpiarCamposVehiculo()
        {
            idVehiculoSeleccionado = 0;

            btnGuardar.Enabled = true;
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            txtPlaca.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtAnio.Clear();
            txtTarifaDiaria.Clear();
            txtKilometrajeActual.Clear();

            cmbTipoVehiculo.SelectedIndex = -1;
            cmbTransmision.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;

            dgvVehiculos.ClearSelection();
            txtPlaca.Focus();
            rutaFotoActual = null;
            MostrarFotoVehiculo(null);
        }
        

        private void CargarVehiculos()
        {
            try
            {
                string tipo = cmbFiltroTipo.SelectedIndex > 0
                    ? cmbFiltroTipo.SelectedItem.ToString()
                    : "";

                string transmision = cmbFiltroTransmision.SelectedIndex > 0
                    ? cmbFiltroTransmision.SelectedItem.ToString()
                    : "";

                var tabla = vehiculoDatos.Listar(
                    txtBuscar.Text, tipo, transmision);

                dgvVehiculos.DataSource = tabla;
                LimpiarCamposVehiculo();
                dgvVehiculos.Columns["IdVehiculo"].Visible = false;
                dgvVehiculos.Columns["RutaFotografia"].Visible = false;

                dgvVehiculos.Columns["Anio"].HeaderText = "Año";
                dgvVehiculos.Columns["TipoVehiculo"].HeaderText = "Tipo";
                dgvVehiculos.Columns["Transmision"].HeaderText = "Transmisión";
                dgvVehiculos.Columns["TarifaDiaria"].HeaderText = "Tarifa diaria (Q)";
                dgvVehiculos.Columns["KilometrajeActual"].HeaderText = "Kilometraje";

                dgvVehiculos.Columns["TarifaDiaria"]
                    .DefaultCellStyle.Format = "N2";

                foreach (DataGridViewColumn columna in dgvVehiculos.Columns)
                {
                    columna.SortMode = DataGridViewColumnSortMode.Automatic;
                }

                dgvVehiculos.ClearSelection();
                lblTotal.Text = "Total de vehículos: " + tabla.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message + Environment.NewLine +
                    ex.GetBaseException().Message,
                    "Consulta de vehículos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BuscarVehiculos(object sender, EventArgs e)
        {
            CargarVehiculos();
        }

        private void MostrarTodosVehiculos(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTransmision.SelectedIndex = 0;

            CargarVehiculos();
        }
    }
}
