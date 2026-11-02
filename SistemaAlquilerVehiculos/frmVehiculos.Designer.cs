namespace SistemaAlquilerVehiculos
{
    partial class frmVehiculos
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
            this.grpDatosVehiculo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.txtAnio = new System.Windows.Forms.TextBox();
            this.txtKilometrajeActual = new System.Windows.Forms.TextBox();
            this.txtTarifaDiaria = new System.Windows.Forms.TextBox();
            this.cmbTipoVehiculo = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.cmbTransmision = new System.Windows.Forms.ComboBox();
            this.grpDatosVehiculo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(307, 41);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de vehículos";
            // 
            // grpDatosVehiculo
            // 
            this.grpDatosVehiculo.Controls.Add(this.cmbTransmision);
            this.grpDatosVehiculo.Controls.Add(this.cmbEstado);
            this.grpDatosVehiculo.Controls.Add(this.cmbTipoVehiculo);
            this.grpDatosVehiculo.Controls.Add(this.txtTarifaDiaria);
            this.grpDatosVehiculo.Controls.Add(this.txtKilometrajeActual);
            this.grpDatosVehiculo.Controls.Add(this.txtAnio);
            this.grpDatosVehiculo.Controls.Add(this.txtModelo);
            this.grpDatosVehiculo.Controls.Add(this.txtMarca);
            this.grpDatosVehiculo.Controls.Add(this.txtPlaca);
            this.grpDatosVehiculo.Controls.Add(this.label9);
            this.grpDatosVehiculo.Controls.Add(this.label8);
            this.grpDatosVehiculo.Controls.Add(this.label7);
            this.grpDatosVehiculo.Controls.Add(this.label6);
            this.grpDatosVehiculo.Controls.Add(this.label5);
            this.grpDatosVehiculo.Controls.Add(this.label4);
            this.grpDatosVehiculo.Controls.Add(this.label3);
            this.grpDatosVehiculo.Controls.Add(this.label2);
            this.grpDatosVehiculo.Controls.Add(this.label1);
            this.grpDatosVehiculo.Location = new System.Drawing.Point(20, 65);
            this.grpDatosVehiculo.Name = "grpDatosVehiculo";
            this.grpDatosVehiculo.Size = new System.Drawing.Size(760, 230);
            this.grpDatosVehiculo.TabIndex = 1;
            this.grpDatosVehiculo.TabStop = false;
            this.grpDatosVehiculo.Text = "Datos del vehículo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Placa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Marca";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(510, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Modelo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Año";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tipo de vehículo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(510, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Transmisión";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 23);
            this.label7.TabIndex = 6;
            this.label7.Text = "Tarifa diaria (Q)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(265, 160);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 23);
            this.label8.TabIndex = 7;
            this.label8.Text = "Estado";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(510, 160);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 23);
            this.label9.TabIndex = 8;
            this.label9.Text = "Kilometraje actual";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(20, 55);
            this.txtPlaca.MaxLength = 20;
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(220, 30);
            this.txtPlaca.TabIndex = 9;
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(265, 55);
            this.txtMarca.MaxLength = 50;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(220, 30);
            this.txtMarca.TabIndex = 10;
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(510, 55);
            this.txtModelo.MaxLength = 50;
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(220, 30);
            this.txtModelo.TabIndex = 11;
            // 
            // txtAnio
            // 
            this.txtAnio.Location = new System.Drawing.Point(20, 120);
            this.txtAnio.MaxLength = 4;
            this.txtAnio.Name = "txtAnio";
            this.txtAnio.Size = new System.Drawing.Size(220, 30);
            this.txtAnio.TabIndex = 12;
            // 
            // txtKilometrajeActual
            // 
            this.txtKilometrajeActual.Location = new System.Drawing.Point(510, 185);
            this.txtKilometrajeActual.Name = "txtKilometrajeActual";
            this.txtKilometrajeActual.Size = new System.Drawing.Size(220, 30);
            this.txtKilometrajeActual.TabIndex = 13;
            // 
            // txtTarifaDiaria
            // 
            this.txtTarifaDiaria.Location = new System.Drawing.Point(20, 185);
            this.txtTarifaDiaria.Name = "txtTarifaDiaria";
            this.txtTarifaDiaria.Size = new System.Drawing.Size(220, 30);
            this.txtTarifaDiaria.TabIndex = 14;
            // 
            // cmbTipoVehiculo
            // 
            this.cmbTipoVehiculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoVehiculo.FormattingEnabled = true;
            this.cmbTipoVehiculo.Items.AddRange(new object[] {
            "Sedán",
            "SUV",
            "Pickup"});
            this.cmbTipoVehiculo.Location = new System.Drawing.Point(265, 120);
            this.cmbTipoVehiculo.Name = "cmbTipoVehiculo";
            this.cmbTipoVehiculo.Size = new System.Drawing.Size(220, 31);
            this.cmbTipoVehiculo.TabIndex = 15;
            // 
            // cmbEstado
            // 
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "Disponible",
            "Rentado",
            "En Taller"});
            this.cmbEstado.Location = new System.Drawing.Point(265, 185);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(220, 31);
            this.cmbEstado.TabIndex = 16;
            // 
            // cmbTransmision
            // 
            this.cmbTransmision.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransmision.FormattingEnabled = true;
            this.cmbTransmision.Items.AddRange(new object[] {
            "Mecánica",
            "Automática"});
            this.cmbTransmision.Location = new System.Drawing.Point(510, 120);
            this.cmbTransmision.Name = "cmbTransmision";
            this.cmbTransmision.Size = new System.Drawing.Size(220, 31);
            this.cmbTransmision.TabIndex = 17;
            // 
            // frmVehiculos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1082, 603);
            this.Controls.Add(this.grpDatosVehiculo);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmVehiculos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de vehículos";
            this.grpDatosVehiculo.ResumeLayout(false);
            this.grpDatosVehiculo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatosVehiculo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTarifaDiaria;
        private System.Windows.Forms.TextBox txtKilometrajeActual;
        private System.Windows.Forms.TextBox txtAnio;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.ComboBox cmbTransmision;
        private System.Windows.Forms.ComboBox cmbEstado;
        private System.Windows.Forms.ComboBox cmbTipoVehiculo;
    }
}