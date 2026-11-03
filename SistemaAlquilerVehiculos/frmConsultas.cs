using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaAlquilerVehiculos
{
    public partial class frmConsultas : Form
    {
        private DataGridView dgvFechas, dgvGroup, dgvHaving, dgvSub, dgvPers;
        private DateTimePicker dtpDesdeF, dtpDesdeP, dtpHastaP;
        private TextBox txtMinimo;

        public frmConsultas()
        {
            InitializeComponent();
            this.Text = "Consultas";
            ConstruirControles();
        }

        private DataGridView NuevaGrilla(TabPage tab, out FlowLayoutPanel barra)
        {
            barra = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(5) };
            var g = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            tab.Controls.Add(g);      // primero el Fill
            tab.Controls.Add(barra);  // luego el Top
            return g;
        }

        private Label Etiqueta(string t)
        {
            return new Label { Text = t, AutoSize = true, Margin = new Padding(5, 8, 5, 0) };
        }

        private DateTimePicker NuevoDtp(DateTime valor)
        {
            return new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 110, Value = valor };
        }

        private Button NuevoBoton(string t, EventHandler h)
        {
            var b = new Button { Text = t, AutoSize = true, Margin = new Padding(10, 3, 5, 0) };
            b.Click += h;
            return b;
        }

        private void ConstruirControles()
        {
            var tabs = new TabControl { Dock = DockStyle.Fill };
            FlowLayoutPanel barra;

            // 1. Fechas
            var t1 = new TabPage("Fechas");
            dgvFechas = NuevaGrilla(t1, out barra);
            dtpDesdeF = NuevoDtp(DateTime.Today.AddYears(-1));
            barra.Controls.Add(Etiqueta("Salida desde:"));
            barra.Controls.Add(dtpDesdeF);
            barra.Controls.Add(NuevoBoton("Consultar", btnFechas_Click));

            // 2. Group By
            var t2 = new TabPage("Group By");
            dgvGroup = NuevaGrilla(t2, out barra);
            barra.Controls.Add(NuevoBoton("Total por cliente", btnGroup_Click));

            // 3. Having
            var t3 = new TabPage("Having");
            dgvHaving = NuevaGrilla(t3, out barra);
            txtMinimo = new TextBox { Width = 90, Text = "1000" };
            barra.Controls.Add(Etiqueta("Total mayor a (Q):"));
            barra.Controls.Add(txtMinimo);
            barra.Controls.Add(NuevoBoton("Consultar", btnHaving_Click));

            // 4. Subconsultas
            var t4 = new TabPage("Subconsultas");
            dgvSub = NuevaGrilla(t4, out barra);
            barra.Controls.Add(NuevoBoton("Pagos sobre el promedio", btnSub_Click));

            // 5. Personalizada
            var t5 = new TabPage("Personalizada");
            dgvPers = NuevaGrilla(t5, out barra);
            dtpDesdeP = NuevoDtp(DateTime.Today.AddYears(-1));
            dtpHastaP = NuevoDtp(DateTime.Today);
            barra.Controls.Add(Etiqueta("Desde:"));
            barra.Controls.Add(dtpDesdeP);
            barra.Controls.Add(Etiqueta("Hasta:"));
            barra.Controls.Add(dtpHastaP);
            barra.Controls.Add(NuevoBoton("Clientes que más pagaron", btnPers_Click));

            tabs.TabPages.AddRange(new[] { t1, t2, t3, t4, t5 });
            this.Controls.Clear();
            this.Controls.Add(tabs);
        }

        // ---------- Acceso a datos ----------
        private void Ejecutar(DataGridView g, string sql, params SqlParameter[] ps)
        {
            try
            {
                var dt = new DataTable();
                using (var cn = Conexion.ObtenerConexion())
                using (var cmd = new SqlCommand(sql, cn))
                {
                    if (ps != null) cmd.Parameters.AddRange(ps);
                    using (var da = new SqlDataAdapter(cmd)) da.Fill(dt);
                }
                g.DataSource = dt;
                if (dt.Rows.Count == 0)
                    MessageBox.Show("No se encontraron resultados.", "Consulta",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- Eventos ----------
        private void btnFechas_Click(object sender, EventArgs e)
        {
            Ejecutar(dgvFechas,
                @"SELECT c.NumeroContrato, cl.Nombres + ' ' + cl.Apellidos AS Cliente,
                         v.Placa, c.FechaSalida, c.EstadoContrato
                  FROM CONTRATO_ALQUILER c
                  INNER JOIN CLIENTE cl ON cl.IdCliente = c.IdCliente
                  INNER JOIN VEHICULO v ON v.IdVehiculo = c.IdVehiculo
                  WHERE c.FechaSalida >= @desde
                  ORDER BY c.FechaSalida DESC",
                new SqlParameter("@desde", dtpDesdeF.Value.Date));
        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            Ejecutar(dgvGroup,
                @"SELECT cl.Nombres + ' ' + cl.Apellidos AS Cliente,
                         SUM(p.Monto) AS TotalPagado, COUNT(DISTINCT c.IdContrato) AS Contratos
                  FROM CLIENTE cl
                  INNER JOIN CONTRATO_ALQUILER c ON c.IdCliente = cl.IdCliente
                  INNER JOIN PAGO_DETALLE p ON p.IdContrato = c.IdContrato
                  WHERE p.TipoCobro <> 'Deposito'
                  GROUP BY cl.Nombres, cl.Apellidos");
        }

        private void btnHaving_Click(object sender, EventArgs e)
        {
            decimal minimo;
            if (!decimal.TryParse(txtMinimo.Text, out minimo) || minimo < 0)
            {
                MessageBox.Show("Ingrese un monto numérico válido (no negativo).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ejecutar(dgvHaving,
                @"SELECT cl.Nombres + ' ' + cl.Apellidos AS Cliente,
                         SUM(p.Monto) AS TotalPagado, COUNT(DISTINCT c.IdContrato) AS Contratos
                  FROM CLIENTE cl
                  INNER JOIN CONTRATO_ALQUILER c ON c.IdCliente = cl.IdCliente
                  INNER JOIN PAGO_DETALLE p ON p.IdContrato = c.IdContrato
                  WHERE p.TipoCobro <> 'Deposito'
                  GROUP BY cl.Nombres, cl.Apellidos
                  HAVING SUM(p.Monto) > @minimo",
                new SqlParameter("@minimo", minimo));
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            Ejecutar(dgvSub,
                @"SELECT IdPago, IdContrato, Monto, FechaPago FROM PAGO_DETALLE
                  WHERE TipoCobro <> 'Deposito'
                  AND Monto > (SELECT AVG(Monto) FROM PAGO_DETALLE WHERE TipoCobro <> 'Deposito')
                  ORDER BY Monto DESC");
        }

        private void btnPers_Click(object sender, EventArgs e)
        {
            if (dtpHastaP.Value.Date < dtpDesdeP.Value.Date)
            {
                MessageBox.Show("La fecha final no puede ser anterior a la inicial.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Ejecutar(dgvPers,
                @"SELECT cl.Nombres + ' ' + cl.Apellidos AS Cliente, SUM(p.Monto) AS TotalPagado
                  FROM CLIENTE cl
                  INNER JOIN CONTRATO_ALQUILER c ON c.IdCliente = cl.IdCliente
                  INNER JOIN PAGO_DETALLE p ON p.IdContrato = c.IdContrato
                  WHERE p.TipoCobro <> 'Deposito' AND p.FechaPago BETWEEN @desde AND @hasta
                  GROUP BY cl.Nombres, cl.Apellidos
                  ORDER BY TotalPagado DESC",
                new SqlParameter("@desde", dtpDesdeP.Value.Date),
                new SqlParameter("@hasta", dtpHastaP.Value.Date));
        }
    }
}