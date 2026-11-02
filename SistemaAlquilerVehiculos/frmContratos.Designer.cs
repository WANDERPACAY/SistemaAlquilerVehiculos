namespace SistemaAlquilerVehiculos
{
    partial class frmContratos
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
            this.cmbClientes = new System.Windows.Forms.ComboBox();
            this.cmbVehiculos = new System.Windows.Forms.ComboBox();
            this.dtpSalida = new System.Windows.Forms.DateTimePicker();
            this.dtpRetorno = new System.Windows.Forms.DateTimePicker();
            this.txtTarifa = new System.Windows.Forms.TextBox();
            this.txtDias = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtDeposito = new System.Windows.Forms.TextBox();
            this.txtKmInicial = new System.Windows.Forms.TextBox();
            this.dgvContratos = new System.Windows.Forms.DataGridView();
            this.btnBuscarDisponibles = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnAprobar = new System.Windows.Forms.Button();
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnDevolucion = new System.Windows.Forms.Button();
            this.btnPdf = new System.Windows.Forms.Button();
            this.lblInformacion = new System.Windows.Forms.Label();
            this.txtNumeroContrato = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvContratos)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbClientes
            // 
            this.cmbClientes.BackColor = System.Drawing.Color.DarkGray;
            this.cmbClientes.FormattingEnabled = true;
            this.cmbClientes.Location = new System.Drawing.Point(34, 145);
            this.cmbClientes.Name = "cmbClientes";
            this.cmbClientes.Size = new System.Drawing.Size(584, 24);
            this.cmbClientes.TabIndex = 0;
            // 
            // cmbVehiculos
            // 
            this.cmbVehiculos.BackColor = System.Drawing.Color.DarkGray;
            this.cmbVehiculos.FormattingEnabled = true;
            this.cmbVehiculos.Location = new System.Drawing.Point(47, 212);
            this.cmbVehiculos.Name = "cmbVehiculos";
            this.cmbVehiculos.Size = new System.Drawing.Size(584, 24);
            this.cmbVehiculos.TabIndex = 1;
            // 
            // dtpSalida
            // 
            this.dtpSalida.CalendarTitleBackColor = System.Drawing.SystemColors.ControlText;
            this.dtpSalida.CalendarTitleForeColor = System.Drawing.Color.DarkGray;
            this.dtpSalida.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSalida.Location = new System.Drawing.Point(34, 287);
            this.dtpSalida.Name = "dtpSalida";
            this.dtpSalida.Size = new System.Drawing.Size(259, 22);
            this.dtpSalida.TabIndex = 2;
            // 
            // dtpRetorno
            // 
            this.dtpRetorno.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRetorno.Location = new System.Drawing.Point(361, 287);
            this.dtpRetorno.Name = "dtpRetorno";
            this.dtpRetorno.Size = new System.Drawing.Size(244, 22);
            this.dtpRetorno.TabIndex = 3;
            // 
            // txtTarifa
            // 
            this.txtTarifa.BackColor = System.Drawing.Color.DarkGray;
            this.txtTarifa.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarifa.Location = new System.Drawing.Point(361, 356);
            this.txtTarifa.Name = "txtTarifa";
            this.txtTarifa.Size = new System.Drawing.Size(244, 22);
            this.txtTarifa.TabIndex = 4;
            // 
            // txtDias
            // 
            this.txtDias.BackColor = System.Drawing.Color.DarkGray;
            this.txtDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDias.Location = new System.Drawing.Point(34, 356);
            this.txtDias.Name = "txtDias";
            this.txtDias.Size = new System.Drawing.Size(259, 22);
            this.txtDias.TabIndex = 5;
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.DarkGray;
            this.txtTotal.Location = new System.Drawing.Point(34, 481);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 22);
            this.txtTotal.TabIndex = 6;
            // 
            // txtDeposito
            // 
            this.txtDeposito.BackColor = System.Drawing.Color.DarkGray;
            this.txtDeposito.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeposito.Location = new System.Drawing.Point(364, 419);
            this.txtDeposito.Name = "txtDeposito";
            this.txtDeposito.Size = new System.Drawing.Size(244, 22);
            this.txtDeposito.TabIndex = 7;
            // 
            // txtKmInicial
            // 
            this.txtKmInicial.BackColor = System.Drawing.Color.DarkGray;
            this.txtKmInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKmInicial.Location = new System.Drawing.Point(34, 419);
            this.txtKmInicial.Name = "txtKmInicial";
            this.txtKmInicial.Size = new System.Drawing.Size(259, 22);
            this.txtKmInicial.TabIndex = 8;
            // 
            // dgvContratos
            // 
            this.dgvContratos.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dgvContratos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvContratos.Location = new System.Drawing.Point(38, 681);
            this.dgvContratos.Name = "dgvContratos";
            this.dgvContratos.RowHeadersWidth = 51;
            this.dgvContratos.RowTemplate.Height = 24;
            this.dgvContratos.Size = new System.Drawing.Size(593, 137);
            this.dgvContratos.TabIndex = 9;
            // 
            // btnBuscarDisponibles
            // 
            this.btnBuscarDisponibles.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnBuscarDisponibles.Location = new System.Drawing.Point(61, 533);
            this.btnBuscarDisponibles.Name = "btnBuscarDisponibles";
            this.btnBuscarDisponibles.Size = new System.Drawing.Size(201, 33);
            this.btnBuscarDisponibles.TabIndex = 10;
            this.btnBuscarDisponibles.Text = "Disponibles";
            this.btnBuscarDisponibles.UseVisualStyleBackColor = false;
            this.btnBuscarDisponibles.Click += new System.EventHandler(this.btnBuscarDisponibles_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Green;
            this.btnGuardar.Location = new System.Drawing.Point(361, 535);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(201, 31);
            this.btnGuardar.TabIndex = 11;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnAprobar
            // 
            this.btnAprobar.BackColor = System.Drawing.Color.Green;
            this.btnAprobar.Location = new System.Drawing.Point(61, 572);
            this.btnAprobar.Name = "btnAprobar";
            this.btnAprobar.Size = new System.Drawing.Size(201, 32);
            this.btnAprobar.TabIndex = 12;
            this.btnAprobar.Text = "Aprovar";
            this.btnAprobar.UseVisualStyleBackColor = false;
            this.btnAprobar.Click += new System.EventHandler(this.btnAprobar_Click);
            // 
            // btnAnular
            // 
            this.btnAnular.BackColor = System.Drawing.Color.Red;
            this.btnAnular.Location = new System.Drawing.Point(361, 573);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(201, 31);
            this.btnAnular.TabIndex = 13;
            this.btnAnular.Text = "Anular";
            this.btnAnular.UseVisualStyleBackColor = false;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnDevolucion
            // 
            this.btnDevolucion.BackColor = System.Drawing.Color.LightGray;
            this.btnDevolucion.Location = new System.Drawing.Point(61, 621);
            this.btnDevolucion.Name = "btnDevolucion";
            this.btnDevolucion.Size = new System.Drawing.Size(201, 33);
            this.btnDevolucion.TabIndex = 14;
            this.btnDevolucion.Text = "Devolución";
            this.btnDevolucion.UseVisualStyleBackColor = false;
            this.btnDevolucion.Click += new System.EventHandler(this.btnDevolucion_Click);
            // 
            // btnPdf
            // 
            this.btnPdf.BackColor = System.Drawing.Color.LightGray;
            this.btnPdf.Location = new System.Drawing.Point(361, 621);
            this.btnPdf.Name = "btnPdf";
            this.btnPdf.Size = new System.Drawing.Size(201, 33);
            this.btnPdf.TabIndex = 15;
            this.btnPdf.Text = "PDF";
            this.btnPdf.UseVisualStyleBackColor = false;
            this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
            // 
            // lblInformacion
            // 
            this.lblInformacion.AutoSize = true;
            this.lblInformacion.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformacion.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblInformacion.Location = new System.Drawing.Point(22, 13);
            this.lblInformacion.Name = "lblInformacion";
            this.lblInformacion.Size = new System.Drawing.Size(203, 19);
            this.lblInformacion.TabIndex = 16;
            this.lblInformacion.Text = "INFORMACIÓN DEL CONTRATO";
            // 
            // txtNumeroContrato
            // 
            this.txtNumeroContrato.Location = new System.Drawing.Point(34, 63);
            this.txtNumeroContrato.Name = "txtNumeroContrato";
            this.txtNumeroContrato.Size = new System.Drawing.Size(268, 22);
            this.txtNumeroContrato.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans TC", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 18;
            this.label1.Text = "Número De Contrato";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 19);
            this.label2.TabIndex = 19;
            this.label2.Text = "Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 19);
            this.label3.TabIndex = 20;
            this.label3.Text = "Vehículo Disponible";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 19);
            this.label4.TabIndex = 21;
            this.label4.Text = "Fecha De Salida";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(361, 268);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "Fecha De Retorno";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(34, 334);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 19);
            this.label6.TabIndex = 23;
            this.label6.Text = "Días De Alquiler";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(361, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 19);
            this.label7.TabIndex = 24;
            this.label7.Text = "Tarifa Diaria";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(34, 397);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 19);
            this.label8.TabIndex = 25;
            this.label8.Text = "Kilometraje Inicial";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(364, 397);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 19);
            this.label9.TabIndex = 26;
            this.label9.Text = "Depósito De Garantía";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(34, 462);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 19);
            this.label10.TabIndex = 27;
            this.label10.Text = "Total Estimado De Renta";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(34, 510);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 19);
            this.label11.TabIndex = 28;
            this.label11.Text = "ACCIONES";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Noto Sans JP", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(37, 662);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(180, 19);
            this.label12.TabIndex = 29;
            this.label12.Text = "CONTRATOS REGISTRADOS";
            // 
            // frmContratos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(935, 830);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNumeroContrato);
            this.Controls.Add(this.lblInformacion);
            this.Controls.Add(this.btnPdf);
            this.Controls.Add(this.btnDevolucion);
            this.Controls.Add(this.btnAnular);
            this.Controls.Add(this.btnAprobar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnBuscarDisponibles);
            this.Controls.Add(this.dgvContratos);
            this.Controls.Add(this.txtKmInicial);
            this.Controls.Add(this.txtDeposito);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtDias);
            this.Controls.Add(this.txtTarifa);
            this.Controls.Add(this.dtpRetorno);
            this.Controls.Add(this.dtpSalida);
            this.Controls.Add(this.cmbVehiculos);
            this.Controls.Add(this.cmbClientes);
            this.Name = "frmContratos";
            this.Text = "frmContratos";
            ((System.ComponentModel.ISupportInitialize)(this.dgvContratos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbClientes;
        private System.Windows.Forms.ComboBox cmbVehiculos;
        private System.Windows.Forms.DateTimePicker dtpSalida;
        private System.Windows.Forms.DateTimePicker dtpRetorno;
        private System.Windows.Forms.TextBox txtTarifa;
        private System.Windows.Forms.TextBox txtDias;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtDeposito;
        private System.Windows.Forms.TextBox txtKmInicial;
        private System.Windows.Forms.DataGridView dgvContratos;
        private System.Windows.Forms.Button btnBuscarDisponibles;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnAprobar;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.Button btnDevolucion;
        private System.Windows.Forms.Button btnPdf;
        private System.Windows.Forms.Label lblInformacion;
        private System.Windows.Forms.TextBox txtNumeroContrato;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
    }
}