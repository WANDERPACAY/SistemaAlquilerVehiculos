using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SistemaAlquilerVehiculos
{
    public partial class frmDashboard : Form
    {
        private Chart chartFlota;
        private Chart chartModelos;
        private Label lblDisponibles, lblVigentes, lblMorosos, lblIngresos;

        public frmDashboard()
        {
            InitializeComponent();
            this.Text = "Dashboard";
            this.Load += frmDashboard_Load;
            ConstruirControles();
        }

        private void ConstruirControles()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2, ColumnCount = 4 };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 90));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            for (int i = 0; i < 4; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));

            lblDisponibles = CrearIndicador("Disponibles");
            lblVigentes = CrearIndicador("Contratos vigentes");
            lblMorosos = CrearIndicador("Morosos");
            lblIngresos = CrearIndicador("Ingresos del mes");
            panel.Controls.Add(lblDisponibles, 0, 0);
            panel.Controls.Add(lblVigentes, 1, 0);
            panel.Controls.Add(lblMorosos, 2, 0);
            panel.Controls.Add(lblIngresos, 3, 0);

            chartFlota = new Chart { Dock = DockStyle.Fill };
            chartFlota.ChartAreas.Add(new ChartArea("a"));
            chartFlota.Legends.Add(new Legend("l"));
            chartFlota.Titles.Add("Utilización de la flota");

            chartModelos = new Chart { Dock = DockStyle.Fill };
            chartModelos.ChartAreas.Add(new ChartArea("a"));
            chartModelos.Titles.Add("Modelos más alquilados");

            panel.Controls.Add(chartFlota, 0, 1);
            panel.SetColumnSpan(chartFlota, 2);
            panel.Controls.Add(chartModelos, 2, 1);
            panel.SetColumnSpan(chartModelos, 2);

            this.Controls.Clear();
            this.Controls.Add(panel);
        }

        private Label CrearIndicador(string titulo)
        {
            return new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BorderStyle = BorderStyle.FixedSingle,
                Text = titulo + "\n0"
            };
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                CargarIndicadores();
                CargarDona();
                CargarBarras();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el dashboard: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private object Escalar(string sql)
        {
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                if (cn.State != ConnectionState.Open) cn.Open();
                return cmd.ExecuteScalar();
            }
        }

        private DataTable Tabla(string sql)
        {
            var dt = new DataTable();
            using (var cn = Conexion.ObtenerConexion())
            using (var da = new SqlDataAdapter(sql, cn))
            {
                da.Fill(dt);
            }
            return dt;
        }

        private void CargarIndicadores()
        {
            lblDisponibles.Text = "Disponibles\n" +
                Escalar("SELECT COUNT(*) FROM VEHICULO WHERE Estado='Disponible'");
            lblVigentes.Text = "Contratos vigentes\n" +
                Escalar("SELECT COUNT(*) FROM CONTRATO_ALQUILER WHERE EstadoContrato='Vigente'");
            lblMorosos.Text = "Morosos\n" +
                Escalar(@"SELECT COUNT(*) FROM CONTRATO_ALQUILER
                          WHERE EstadoContrato='Vigente' AND FechaRetornoReal IS NULL
                          AND FechaRetornoPactada < GETDATE()");
            object ing = Escalar(@"SELECT ISNULL(SUM(Monto),0) FROM PAGO_DETALLE
                                   WHERE TipoCobro <> 'Deposito'
                                   AND MONTH(FechaPago)=MONTH(GETDATE())
                                   AND YEAR(FechaPago)=YEAR(GETDATE())");
            lblIngresos.Text = "Ingresos del mes\nQ" + Convert.ToDecimal(ing).ToString("N2");
        }

        private void CargarDona()
        {
            DataTable dt = Tabla("SELECT Estado, COUNT(*) AS Total FROM VEHICULO GROUP BY Estado");
            chartFlota.Series.Clear();
            var s = new Series("Flota") { ChartType = SeriesChartType.Doughnut, IsValueShownAsLabel = true };
            foreach (DataRow r in dt.Rows)
                s.Points.AddXY(r["Estado"].ToString(), Convert.ToInt32(r["Total"]));
            chartFlota.Series.Add(s);
        }

        private void CargarBarras()
        {
            DataTable dt = Tabla(@"SELECT TOP 5 v.Marca + ' ' + v.Modelo AS Modelo, COUNT(*) AS Alquileres
                                   FROM CONTRATO_ALQUILER c
                                   INNER JOIN VEHICULO v ON v.IdVehiculo = c.IdVehiculo
                                   WHERE c.EstadoContrato <> 'Anulado'
                                   GROUP BY v.Marca, v.Modelo
                                   ORDER BY Alquileres DESC");
            chartModelos.Series.Clear();
            var s = new Series("Alquileres") { ChartType = SeriesChartType.Bar, IsValueShownAsLabel = true };
            foreach (DataRow r in dt.Rows)
                s.Points.AddXY(r["Modelo"].ToString(), Convert.ToInt32(r["Alquileres"]));
            chartModelos.Series.Add(s);
        }
    }
}