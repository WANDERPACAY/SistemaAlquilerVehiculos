namespace SistemaAlquilerVehiculos
{
    partial class frmGaleriaVehiculos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.lblTipo = new System.Windows.Forms.Label();
            this.lblTransmision = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cmbFiltroTipo = new System.Windows.Forms.ComboBox();
            this.cmbFiltroTransmision = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnMostrarTodos = new System.Windows.Forms.Button();
            this.flpVehiculos = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(338, 46);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Galería de vehículos";
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(20, 75);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(270, 23);
            this.lblBuscar.TabIndex = 1;
            this.lblBuscar.Text = "Buscar por placa, marca o modelo";
            //
            // lblTipo
            //
            this.lblTipo.AutoSize = true;
            this.lblTipo.Location = new System.Drawing.Point(370, 75);
            this.lblTipo.Name = "lblTipo";
            this.lblTipo.Size = new System.Drawing.Size(135, 23);
            this.lblTipo.TabIndex = 2;
            this.lblTipo.Text = "Tipo de vehículo";
            //
            // lblTransmision
            //
            this.lblTransmision.AutoSize = true;
            this.lblTransmision.Location = new System.Drawing.Point(570, 75);
            this.lblTransmision.Name = "lblTransmision";
            this.lblTransmision.Size = new System.Drawing.Size(100, 23);
            this.lblTransmision.TabIndex = 3;
            this.lblTransmision.Text = "Transmisión";
            //
            // txtBuscar
            //
            this.txtBuscar.Location = new System.Drawing.Point(20, 100);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(330, 30);
            this.txtBuscar.TabIndex = 4;
            //
            // cmbFiltroTipo
            //
            this.cmbFiltroTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTipo.FormattingEnabled = true;
            this.cmbFiltroTipo.Location = new System.Drawing.Point(370, 100);
            this.cmbFiltroTipo.Name = "cmbFiltroTipo";
            this.cmbFiltroTipo.Size = new System.Drawing.Size(180, 31);
            this.cmbFiltroTipo.TabIndex = 5;
            //
            // cmbFiltroTransmision
            //
            this.cmbFiltroTransmision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroTransmision.FormattingEnabled = true;
            this.cmbFiltroTransmision.Location = new System.Drawing.Point(570, 100);
            this.cmbFiltroTransmision.Name = "cmbFiltroTransmision";
            this.cmbFiltroTransmision.Size = new System.Drawing.Size(180, 31);
            this.cmbFiltroTransmision.TabIndex = 6;
            //
            // btnBuscar
            //
            this.btnBuscar.Location = new System.Drawing.Point(770, 97);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 35);
            this.btnBuscar.TabIndex = 7;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            //
            // btnMostrarTodos
            //
            this.btnMostrarTodos.Location = new System.Drawing.Point(900, 97);
            this.btnMostrarTodos.Name = "btnMostrarTodos";
            this.btnMostrarTodos.Size = new System.Drawing.Size(150, 35);
            this.btnMostrarTodos.TabIndex = 8;
            this.btnMostrarTodos.Text = "Mostrar todos";
            this.btnMostrarTodos.UseVisualStyleBackColor = true;
            //
            // flpVehiculos
            //
            this.flpVehiculos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpVehiculos.AutoScroll = true;
            this.flpVehiculos.Location = new System.Drawing.Point(20, 155);
            this.flpVehiculos.Name = "flpVehiculos";
            this.flpVehiculos.Padding = new System.Windows.Forms.Padding(10);
            this.flpVehiculos.Size = new System.Drawing.Size(1030, 510);
            this.flpVehiculos.TabIndex = 9;
            //
            // lblTotal
            //
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(20, 680);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(163, 23);
            this.lblTotal.TabIndex = 10;
            this.lblTotal.Text = "Total de vehículos: 0";
            //
            // frmGaleriaVehiculos
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1195, 953);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.flpVehiculos);
            this.Controls.Add(this.btnMostrarTodos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.cmbFiltroTransmision);
            this.Controls.Add(this.cmbFiltroTipo);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblTransmision);
            this.Controls.Add(this.lblTipo);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmGaleriaVehiculos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Galería de vehículos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.Label lblTipo;
        private System.Windows.Forms.Label lblTransmision;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cmbFiltroTipo;
        private System.Windows.Forms.ComboBox cmbFiltroTransmision;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnMostrarTodos;
        private System.Windows.Forms.FlowLayoutPanel flpVehiculos;
        private System.Windows.Forms.Label lblTotal;
    }
}