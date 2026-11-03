using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SistemaAlquilerVehiculos
{
    public partial class frmAnalisis : Form
    {
        private Chart chartIngresos, chartTipos;
        private DataGridView dgvDatos;
        private Label lblMorosos, lblConclusion;

        public frmAnalisis()
        {
            InitializeComponent();
            this.Text = "Análisis de datos";
            ConstruirControles();
            this.Load += frmAnalisis_Load;
        }

        private void ConstruirControles()
        {
            var panel = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 3, ColumnCount = 2 };
            panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 55));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 45));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            lblMorosos = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Text = "Contratos morosos: 0%"
            };
            panel.Controls.Add(lblMorosos, 0, 0);
            panel.SetColumnSpan(lblMorosos, 2);

            chartIngresos = new Chart { Dock = DockStyle.Fill };
            chartIngresos.ChartAreas.Add(new ChartArea("a"));
            chartIngresos.Titles.Add("Ingresos por mes (Q)");
            panel.Controls.Add(chartIngresos, 0, 1);

            chartTipos = new Chart { Dock = DockStyle.Fill };
            chartTipos.ChartAreas.Add(new ChartArea("a"));
            chartTipos.Titles.Add("Alquileres por tipo de vehículo");
            panel.Controls.Add(chartTipos, 1, 1);

            dgvDatos = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            panel.Controls.Add(dgvDatos, 0, 2);

            lblConclusion = new Label
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Padding = new Padding(10),
                Text = "Conclusión:"
            };
            panel.Controls.Add(lblConclusion, 1, 2);

            this.Controls.Clear();
            this.Controls.Add(panel);
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

        private void frmAnalisis_Load(object sender, EventArgs e)
        {
            try
            {
                CargarMorosos();
                CargarIngresos();
                CargarTipos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar el análisis: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMorosos()
        {
            DataTable dt = Tabla(@"SELECT COUNT(*) AS Total,
                SUM(CASE WHEN EstadoContrato='Vigente' AND FechaRetornoReal IS NULL
                         AND FechaRetornoPactada < GETDATE() THEN 1 ELSE 0 END) AS Morosos
                FROM CONTRATO_ALQUILER WHERE EstadoContrato <> 'Anulado'");
            int total = Convert.ToInt32(dt.Rows[0]["Total"]);
            int mor = dt.Rows[0]["Morosos"] == DBNull.Value ? 0 : Convert.ToInt32(dt.Rows[0]["Morosos"]);
            double pct = total == 0 ? 0 : mor * 100.0 / total;
            lblMorosos.Text = string.Format("Contratos morosos: {0} de {1} ({2:N1}%)", mor, total, pct);
        }

        private void CargarIngresos()
        {
            DataTable dt = Tabla(@"SELECT YEAR(FechaPago) AS Anio, MONTH(FechaPago) AS Mes,
                                          SUM(Monto) AS Ingresos
                                   FROM PAGO_DETALLE
                                   WHERE TipoCobro <> 'Deposito'
                                   GROUP BY YEAR(FechaPago), MONTH(FechaPago)
                                   ORDER BY Anio, Mes");
            chartIngresos.Series.Clear();
            var s = new Series("Ingresos") { ChartType = SeriesChartType.Column, IsValueShownAsLabel = true };
            foreach (DataRow r in dt.Rows)
                s.Points.AddXY(r["Mes"] + "/" + r["Anio"], Convert.ToDecimal(r["Ingresos"]));
            chartIngresos.Series.Add(s);

            if (dt.Rows.Count > 0)
            {
                DataRow mejor = dt.Rows[0];
                foreach (DataRow r in dt.Rows)
                    if (Convert.ToDecimal(r["Ingresos"]) > Convert.ToDecimal(mejor["Ingresos"])) mejor = r;
                lblConclusion.Text = string.Format("Conclusión:\nEl mes con más ingresos fue {0}/{1} con Q{2:N2}.",
                    mejor["Mes"], mejor["Anio"], Convert.ToDecimal(mejor["Ingresos"]));
            }
        }

        private void CargarTipos()
        {
            DataTable dt = Tabla(@"SELECT v.Tipo, COUNT(*) AS Alquileres,
                                          COUNT(DISTINCT v.IdVehiculo) AS Vehiculos
                                   FROM CONTRATO_ALQUILER c
                                   INNER JOIN VEHICULO v ON v.IdVehiculo = c.IdVehiculo
                                   WHERE c.EstadoContrato <> 'Anulado'
                                   GROUP BY v.Tipo
                                   ORDER BY Alquileres DESC");
            dgvDatos.DataSource = dt;
            chartTipos.Series.Clear();
            var s = new Series("Alquileres") { ChartType = SeriesChartType.Pie, IsValueShownAsLabel = true };
            foreach (DataRow r in dt.Rows)
                s.Points.AddXY(r["Tipo"].ToString(), Convert.ToInt32(r["Alquileres"]));
            chartTipos.Series.Add(s);

            if (dt.Rows.Count > 0)
                lblConclusion.Text += string.Format("\nEl tipo de vehículo más alquilado es {0} ({1} alquileres).",
                    dt.Rows[0]["Tipo"], dt.Rows[0]["Alquileres"]);
        }
    }
}