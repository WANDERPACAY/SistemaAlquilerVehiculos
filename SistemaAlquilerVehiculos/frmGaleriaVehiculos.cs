using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SistemaAlquilerVehiculos.Datos;
using SistemaAlquilerVehiculos.Utilidades;

namespace SistemaAlquilerVehiculos
{
    public partial class frmGaleriaVehiculos : Form
    {
        private readonly VehiculoDatos vehiculoDatos =
            new VehiculoDatos();

        public frmGaleriaVehiculos()
        {
            InitializeComponent();
            Resize += AjustarDistribucion;
            Shown += AjustarDistribucion;

            Load += InicializarGaleria;
            btnBuscar.Click += BuscarVehiculos;
            btnMostrarTodos.Click += MostrarTodosVehiculos;
            txtBuscar.KeyDown += BuscarConEnter;

            FormClosed += delegate
            {
                LimpiarTarjetas();
            };
        }

        private void InicializarGaleria(object sender, EventArgs e)
        {
            cmbFiltroTipo.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbFiltroTransmision.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbFiltroTipo.Items.Clear();
            cmbFiltroTipo.Items.AddRange(new object[]
            {
                "Todos",
                "Sedán",
                "SUV",
                "Pickup"
            });

            cmbFiltroTransmision.Items.Clear();
            cmbFiltroTransmision.Items.AddRange(new object[]
            {
                "Todos",
                "Mecánica",
                "Automática"
            });

            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTransmision.SelectedIndex = 0;

            flpVehiculos.AutoScroll = true;
            flpVehiculos.WrapContents = true;
            flpVehiculos.FlowDirection =
                FlowDirection.LeftToRight;

            CargarGaleria();
        }
        private void AjustarDistribucion(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                return;

            int margen = 20;

            lblTotal.Left = flpVehiculos.Left;
            lblTotal.Top = Math.Max(
                margen,
                ClientSize.Height - lblTotal.Height - margen);

            flpVehiculos.Width = Math.Max(
                0,
                ClientSize.Width - flpVehiculos.Left - margen);

            flpVehiculos.Height = Math.Max(
                0,
                lblTotal.Top - flpVehiculos.Top - margen);
        }
        private void BuscarVehiculos(object sender, EventArgs e)
        {
            CargarGaleria();
        }

        private void MostrarTodosVehiculos(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            cmbFiltroTipo.SelectedIndex = 0;
            cmbFiltroTransmision.SelectedIndex = 0;

            CargarGaleria();
        }

        private void BuscarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            e.SuppressKeyPress = true;
            CargarGaleria();
        }

        private void CargarGaleria()
        {
            flpVehiculos.SuspendLayout();

            try
            {
                string tipo = cmbFiltroTipo.SelectedIndex > 0
                    ? cmbFiltroTipo.SelectedItem.ToString()
                    : "";

                string transmision =
                    cmbFiltroTransmision.SelectedIndex > 0
                    ? cmbFiltroTransmision.SelectedItem.ToString()
                    : "";

                using (DataTable tabla = vehiculoDatos.Listar(
                    txtBuscar.Text,
                    tipo,
                    transmision))
                {
                    LimpiarTarjetas();

                    foreach (DataRow fila in tabla.Rows)
                    {
                        flpVehiculos.Controls.Add(
                            CrearTarjeta(fila));
                    }

                    lblTotal.Text =
                        "Total de vehículos: " + tabla.Rows.Count;

                    if (tabla.Rows.Count == 0)
                    {
                        Label aviso = new Label
                        {
                            AutoSize = true,
                            Text = "No se encontraron vehículos "
                                + "con esos filtros.",
                            ForeColor = Color.DimGray,
                            Margin = new Padding(15)
                        };

                        flpVehiculos.Controls.Add(aviso);
                    }
                }

                flpVehiculos.AutoScrollPosition = Point.Empty;
            }
            catch (Exception ex)
            {
                LimpiarTarjetas();
                lblTotal.Text = "No se pudo cargar la galería.";

                MessageBox.Show(
                    ex.Message + Environment.NewLine
                    + ex.GetBaseException().Message,
                    "Error al cargar vehículos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                flpVehiculos.ResumeLayout(true);
            }
        }

        private Panel CrearTarjeta(DataRow fila)
        {
            Panel tarjeta = new Panel
            {
                Size = new Size(310, 385),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(8)
            };

            // Si falla la creación, libera lo que ya se haya creado.
            try
            {
                PictureBox foto = new PictureBox
                {
                    Location = new Point(10, 10),
                    Size = new Size(288, 170),
                    BackColor = Color.WhiteSmoke,
                    SizeMode = PictureBoxSizeMode.Zoom
                };

                // PictureBox no libera automáticamente su Image.
                foto.Disposed += delegate
                {
                    Image imagen = foto.Image;
                    foto.Image = null;

                    if (imagen != null)
                        imagen.Dispose();
                };

                tarjeta.Controls.Add(foto);

                string textoFoto = "Sin fotografía";

                try
                {
                    foto.Image = FotosVehiculos.Cargar(
                        Convert.ToString(fila["RutaFotografia"]));
                }
                catch (IOException)
                {
                    textoFoto = "Fotografía no disponible";
                }
                catch (ArgumentException)
                {
                    textoFoto = "Fotografía no válida";
                }
                catch (ExternalException)
                {
                    textoFoto = "No se pudo leer la fotografía";
                }
                catch (UnauthorizedAccessException)
                {
                    textoFoto = "Sin acceso a la fotografía";
                }

                if (foto.Image == null)
                {
                    Label avisoFoto = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = textoFoto,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.DimGray
                    };

                    foto.Controls.Add(avisoFoto);
                }

                Label titulo = AgregarEtiqueta(
                    tarjeta,
                    Convert.ToString(fila["Marca"]) + " "
                        + Convert.ToString(fila["Modelo"]),
                    190,
                    30);

                titulo.ForeColor = Color.MidnightBlue;

                AgregarEtiqueta(
                    tarjeta,
                    "Placa: " + Convert.ToString(fila["Placa"]),
                    225,
                    25);

                AgregarEtiqueta(
                    tarjeta,
                    "Año: " + Convert.ToString(fila["Anio"])
                        + "  |  "
                        + Convert.ToString(fila["TipoVehiculo"]),
                    255,
                    25);

                AgregarEtiqueta(
                    tarjeta,
                    "Transmisión: "
                        + Convert.ToString(fila["Transmision"]),
                    285,
                    25);

                Label tarifa = AgregarEtiqueta(
                    tarjeta,
                    "Q "
                        + Convert.ToDecimal(
                            fila["TarifaDiaria"]).ToString("N2")
                        + " por día",
                    315,
                    25);

                tarifa.ForeColor = Color.DarkBlue;

                string estado = Convert.ToString(fila["Estado"]);

                Label etiquetaEstado = AgregarEtiqueta(
                    tarjeta,
                    "Estado: " + estado,
                    345,
                    25);

                etiquetaEstado.ForeColor =
                    estado == "Disponible"
                    ? Color.DarkGreen
                    : Color.DarkOrange;

                return tarjeta;
            }
            catch
            {
                tarjeta.Dispose();
                throw;
            }
        }

        private Label AgregarEtiqueta(
            Panel tarjeta,
            string texto,
            int posicionY,
            int alto)
        {
            Label etiqueta = new Label
            {
                Text = texto,
                AutoSize = false,
                AutoEllipsis = true,
                Location = new Point(10, posicionY),
                Size = new Size(288, alto),
                TextAlign = ContentAlignment.MiddleLeft,
                UseMnemonic = false
            };

            tarjeta.Controls.Add(etiqueta);
            return etiqueta;
        }

        private void LimpiarTarjetas()
        {
            while (flpVehiculos.Controls.Count > 0)
            {
                Control control = flpVehiculos.Controls[0];

                flpVehiculos.Controls.Remove(control);
                control.Dispose();
            }
        }
    }
}