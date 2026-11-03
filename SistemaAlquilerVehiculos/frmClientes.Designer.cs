namespace SistemaAlquilerVehiculos
{
    partial class frmClientes
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
            this.grpDatosCliente = new System.Windows.Forms.GroupBox();
            this.txtNombres = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDPI = new System.Windows.Forms.TextBox();
            this.txtApellidos = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbMunicipio = new System.Windows.Forms.ComboBox();
            this.cmbDepartamento = new System.Windows.Forms.ComboBox();
            this.grpLicencia = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtNumeroLicencia = new System.Windows.Forms.TextBox();
            this.cmbTipoLicencia = new System.Windows.Forms.ComboBox();
            this.dtpVencimientoLicencia = new System.Windows.Forms.DateTimePicker();
            this.picLicencia = new System.Windows.Forms.PictureBox();
            this.btnFotoLicencia = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.lblBuscar = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnMostrarTodos = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.grpDatosCliente.SuspendLayout();
            this.grpLicencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLicencia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitulo
            //
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(283, 41);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestión de clientes";
            //
            // grpDatosCliente
            //
            this.grpDatosCliente.Controls.Add(this.cmbDepartamento);
            this.grpDatosCliente.Controls.Add(this.cmbMunicipio);
            this.grpDatosCliente.Controls.Add(this.label7);
            this.grpDatosCliente.Controls.Add(this.label6);
            this.grpDatosCliente.Controls.Add(this.txtDireccion);
            this.grpDatosCliente.Controls.Add(this.txtApellidos);
            this.grpDatosCliente.Controls.Add(this.txtDPI);
            this.grpDatosCliente.Controls.Add(this.txtTelefono);
            this.grpDatosCliente.Controls.Add(this.label5);
            this.grpDatosCliente.Controls.Add(this.label4);
            this.grpDatosCliente.Controls.Add(this.label3);
            this.grpDatosCliente.Controls.Add(this.label2);
            this.grpDatosCliente.Controls.Add(this.label1);
            this.grpDatosCliente.Controls.Add(this.txtNombres);
            this.grpDatosCliente.Location = new System.Drawing.Point(20, 65);
            this.grpDatosCliente.Name = "grpDatosCliente";
            this.grpDatosCliente.Size = new System.Drawing.Size(760, 250);
            this.grpDatosCliente.TabIndex = 1;
            this.grpDatosCliente.TabStop = false;
            this.grpDatosCliente.Text = "Datos del cliente";
            //
            // txtNombres
            //
            this.txtNombres.Location = new System.Drawing.Point(265, 55);
            this.txtNombres.MaxLength = 50;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(220, 30);
            this.txtNombres.TabIndex = 0;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "DPI";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombres";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(510, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Apellidos";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 23);
            this.label4.TabIndex = 4;
            this.label4.Text = "Teléfono";
            //
            // label5
            //
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Dirección";
            //
            // txtTelefono
            //
            this.txtTelefono.Location = new System.Drawing.Point(20, 125);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(220, 30);
            this.txtTelefono.TabIndex = 6;
            //
            // txtDPI
            //
            this.txtDPI.Location = new System.Drawing.Point(20, 55);
            this.txtDPI.MaxLength = 13;
            this.txtDPI.Name = "txtDPI";
            this.txtDPI.Size = new System.Drawing.Size(220, 30);
            this.txtDPI.TabIndex = 7;
            //
            // txtApellidos
            //
            this.txtApellidos.Location = new System.Drawing.Point(510, 55);
            this.txtApellidos.MaxLength = 50;
            this.txtApellidos.Name = "txtApellidos";
            this.txtApellidos.Size = new System.Drawing.Size(220, 30);
            this.txtApellidos.TabIndex = 8;
            //
            // txtDireccion
            //
            this.txtDireccion.Location = new System.Drawing.Point(265, 125);
            this.txtDireccion.MaxLength = 150;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(465, 30);
            this.txtDireccion.TabIndex = 9;
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Departamento";
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(390, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 23);
            this.label7.TabIndex = 11;
            this.label7.Text = "Municipio";
            //
            // cmbMunicipio
            //
            this.cmbMunicipio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMunicipio.FormattingEnabled = true;
            this.cmbMunicipio.Location = new System.Drawing.Point(390, 195);
            this.cmbMunicipio.Name = "cmbMunicipio";
            this.cmbMunicipio.Size = new System.Drawing.Size(340, 31);
            this.cmbMunicipio.TabIndex = 12;
            //
            // cmbDepartamento
            //
            this.cmbDepartamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartamento.FormattingEnabled = true;
            this.cmbDepartamento.Location = new System.Drawing.Point(20, 195);
            this.cmbDepartamento.Name = "cmbDepartamento";
            this.cmbDepartamento.Size = new System.Drawing.Size(340, 31);
            this.cmbDepartamento.TabIndex = 13;
            //
            // grpLicencia
            //
            this.grpLicencia.Controls.Add(this.dtpVencimientoLicencia);
            this.grpLicencia.Controls.Add(this.cmbTipoLicencia);
            this.grpLicencia.Controls.Add(this.txtNumeroLicencia);
            this.grpLicencia.Controls.Add(this.label10);
            this.grpLicencia.Controls.Add(this.label9);
            this.grpLicencia.Controls.Add(this.label8);
            this.grpLicencia.Location = new System.Drawing.Point(20, 325);
            this.grpLicencia.Name = "grpLicencia";
            this.grpLicencia.Size = new System.Drawing.Size(760, 115);
            this.grpLicencia.TabIndex = 2;
            this.grpLicencia.TabStop = false;
            this.grpLicencia.Text = "Datos de la licencia";
            //
            // label8
            //
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(158, 23);
            this.label8.TabIndex = 2;
            this.label8.Text = "Número de licencia";
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(265, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(128, 23);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tipo de licencia";
            //
            // label10
            //
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(440, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(176, 23);
            this.label10.TabIndex = 4;
            this.label10.Text = "Fecha de vencimiento";
            //
            // txtNumeroLicencia
            //
            this.txtNumeroLicencia.Location = new System.Drawing.Point(20, 50);
            this.txtNumeroLicencia.MaxLength = 30;
            this.txtNumeroLicencia.Name = "txtNumeroLicencia";
            this.txtNumeroLicencia.Size = new System.Drawing.Size(220, 30);
            this.txtNumeroLicencia.TabIndex = 5;
            //
            // cmbTipoLicencia
            //
            this.cmbTipoLicencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoLicencia.FormattingEnabled = true;
            this.cmbTipoLicencia.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "M",
            "E"});
            this.cmbTipoLicencia.Location = new System.Drawing.Point(265, 50);
            this.cmbTipoLicencia.Name = "cmbTipoLicencia";
            this.cmbTipoLicencia.Size = new System.Drawing.Size(150, 31);
            this.cmbTipoLicencia.TabIndex = 14;
            //
            // dtpVencimientoLicencia
            //
            this.dtpVencimientoLicencia.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVencimientoLicencia.Location = new System.Drawing.Point(440, 50);
            this.dtpVencimientoLicencia.Name = "dtpVencimientoLicencia";
            this.dtpVencimientoLicencia.Size = new System.Drawing.Size(290, 30);
            this.dtpVencimientoLicencia.TabIndex = 15;
            //
            // picLicencia
            //
            this.picLicencia.BackColor = System.Drawing.Color.White;
            this.picLicencia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLicencia.Location = new System.Drawing.Point(800, 80);
            this.picLicencia.Name = "picLicencia";
            this.picLicencia.Size = new System.Drawing.Size(250, 180);
            this.picLicencia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLicencia.TabIndex = 3;
            this.picLicencia.TabStop = false;
            //
            // btnFotoLicencia
            //
            this.btnFotoLicencia.Location = new System.Drawing.Point(800, 270);
            this.btnFotoLicencia.Name = "btnFotoLicencia";
            this.btnFotoLicencia.Size = new System.Drawing.Size(250, 40);
            this.btnFotoLicencia.TabIndex = 4;
            this.btnFotoLicencia.Text = "Seleccionar foto de licencia";
            this.btnFotoLicencia.UseVisualStyleBackColor = true;
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(20, 455);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(130, 35);
            this.btnGuardar.TabIndex = 5;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            //
            // btnEditar
            //
            this.btnEditar.Enabled = false;
            this.btnEditar.Location = new System.Drawing.Point(165, 455);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(130, 35);
            this.btnEditar.TabIndex = 6;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            //
            // btnEliminar
            //
            this.btnEliminar.Enabled = false;
            this.btnEliminar.Location = new System.Drawing.Point(310, 455);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(130, 35);
            this.btnEliminar.TabIndex = 7;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            //
            // btnLimpiar
            //
            this.btnLimpiar.Location = new System.Drawing.Point(455, 455);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(130, 35);
            this.btnLimpiar.TabIndex = 8;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            //
            // lblBuscar
            //
            this.lblBuscar.AutoSize = true;
            this.lblBuscar.Location = new System.Drawing.Point(20, 505);
            this.lblBuscar.Name = "lblBuscar";
            this.lblBuscar.Size = new System.Drawing.Size(286, 23);
            this.lblBuscar.TabIndex = 9;
            this.lblBuscar.Text = "Buscar por DPI, nombres o apellidos";
            //
            // txtBuscar
            //
            this.txtBuscar.Location = new System.Drawing.Point(20, 530);
            this.txtBuscar.MaxLength = 100;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(450, 30);
            this.txtBuscar.TabIndex = 10;
            //
            // btnBuscar
            //
            this.btnBuscar.Location = new System.Drawing.Point(485, 528);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(120, 35);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            //
            // btnMostrarTodos
            //
            this.btnMostrarTodos.Location = new System.Drawing.Point(620, 528);
            this.btnMostrarTodos.Name = "btnMostrarTodos";
            this.btnMostrarTodos.Size = new System.Drawing.Size(160, 35);
            this.btnMostrarTodos.TabIndex = 12;
            this.btnMostrarTodos.Text = "Mostrar todos";
            this.btnMostrarTodos.UseVisualStyleBackColor = true;
            //
            // dgvClientes
            //
            this.dgvClientes.AllowUserToAddRows = false;
            this.dgvClientes.AllowUserToDeleteRows = false;
            this.dgvClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvClientes.BackgroundColor = System.Drawing.Color.White;
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(20, 575);
            this.dgvClientes.MultiSelect = false;
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.ReadOnly = true;
            this.dgvClientes.RowHeadersVisible = false;
            this.dgvClientes.RowHeadersWidth = 51;
            this.dgvClientes.RowTemplate.Height = 24;
            this.dgvClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvClientes.Size = new System.Drawing.Size(1030, 95);
            this.dgvClientes.TabIndex = 13;
            //
            // lblTotal
            //
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(20, 680);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(150, 23);
            this.lblTotal.TabIndex = 14;
            this.lblTotal.Text = "Total de clientes: 0";
            //
            // frmClientes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1080, 710);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.btnMostrarTodos);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.lblBuscar);
            this.Controls.Add(this.btnLimpiar);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnFotoLicencia);
            this.Controls.Add(this.picLicencia);
            this.Controls.Add(this.grpLicencia);
            this.Controls.Add(this.grpDatosCliente);
            this.Controls.Add(this.lblTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestión de clientes";
            this.grpDatosCliente.ResumeLayout(false);
            this.grpDatosCliente.PerformLayout();
            this.grpLicencia.ResumeLayout(false);
            this.grpLicencia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLicencia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.GroupBox grpDatosCliente;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtApellidos;
        private System.Windows.Forms.TextBox txtDPI;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombres;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbDepartamento;
        private System.Windows.Forms.ComboBox cmbMunicipio;
        private System.Windows.Forms.GroupBox grpLicencia;
        private System.Windows.Forms.DateTimePicker dtpVencimientoLicencia;
        private System.Windows.Forms.ComboBox cmbTipoLicencia;
        private System.Windows.Forms.TextBox txtNumeroLicencia;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox picLicencia;
        private System.Windows.Forms.Button btnFotoLicencia;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnMostrarTodos;
        private System.Windows.Forms.DataGridView dgvClientes;
        private System.Windows.Forms.Label lblTotal;
    }
}