namespace CajaComplete
{
    partial class frmRegistro
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
            this.label1 = new System.Windows.Forms.Label();
            this.rbnF = new System.Windows.Forms.RadioButton();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbnM = new System.Windows.Forms.RadioButton();
            this.rbnO = new System.Windows.Forms.RadioButton();
            this.txtApellido = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDoc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nmuEdad = new System.Windows.Forms.NumericUpDown();
            this.cmbTipoDoc = new System.Windows.Forms.ComboBox();
            this.btnInsertar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEdad)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // rbnF
            // 
            this.rbnF.AutoSize = true;
            this.rbnF.Location = new System.Drawing.Point(6, 21);
            this.rbnF.Name = "rbnF";
            this.rbnF.Size = new System.Drawing.Size(36, 20);
            this.rbnF.TabIndex = 1;
            this.rbnF.TabStop = true;
            this.rbnF.Text = "F";
            this.rbnF.UseVisualStyleBackColor = true;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(115, 48);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(180, 22);
            this.txtNombre.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbnO);
            this.groupBox1.Controls.Add(this.rbnM);
            this.groupBox1.Controls.Add(this.rbnF);
            this.groupBox1.Location = new System.Drawing.Point(431, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sexo";
            // 
            // rbnM
            // 
            this.rbnM.AutoSize = true;
            this.rbnM.Location = new System.Drawing.Point(48, 21);
            this.rbnM.Name = "rbnM";
            this.rbnM.Size = new System.Drawing.Size(39, 20);
            this.rbnM.TabIndex = 2;
            this.rbnM.TabStop = true;
            this.rbnM.Text = "M";
            this.rbnM.UseVisualStyleBackColor = true;
            // 
            // rbnO
            // 
            this.rbnO.AutoSize = true;
            this.rbnO.Location = new System.Drawing.Point(93, 21);
            this.rbnO.Name = "rbnO";
            this.rbnO.Size = new System.Drawing.Size(53, 20);
            this.rbnO.TabIndex = 3;
            this.rbnO.TabStop = true;
            this.rbnO.Text = "Otro";
            this.rbnO.UseVisualStyleBackColor = true;
            // 
            // txtApellido
            // 
            this.txtApellido.Location = new System.Drawing.Point(115, 88);
            this.txtApellido.Name = "txtApellido";
            this.txtApellido.Size = new System.Drawing.Size(180, 22);
            this.txtApellido.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Apellido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tipo de Documento";
            // 
            // txtDoc
            // 
            this.txtDoc.Location = new System.Drawing.Point(115, 179);
            this.txtDoc.Name = "txtDoc";
            this.txtDoc.Size = new System.Drawing.Size(180, 22);
            this.txtDoc.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Documento";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(62, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Edad";
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(431, 131);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(180, 22);
            this.txtDir.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Direccion";
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(431, 176);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(180, 22);
            this.txtCorreo.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(378, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Correo";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(431, 218);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(180, 22);
            this.txtTel.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(365, 221);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Telefono";
            // 
            // nmuEdad
            // 
            this.nmuEdad.Location = new System.Drawing.Point(115, 219);
            this.nmuEdad.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nmuEdad.Name = "nmuEdad";
            this.nmuEdad.Size = new System.Drawing.Size(120, 22);
            this.nmuEdad.TabIndex = 18;
            // 
            // cmbTipoDoc
            // 
            this.cmbTipoDoc.FormattingEnabled = true;
            this.cmbTipoDoc.Items.AddRange(new object[] {
            "Cedula",
            "Pasaporte"});
            this.cmbTipoDoc.Location = new System.Drawing.Point(115, 141);
            this.cmbTipoDoc.Name = "cmbTipoDoc";
            this.cmbTipoDoc.Size = new System.Drawing.Size(180, 24);
            this.cmbTipoDoc.TabIndex = 19;
            // 
            // btnInsertar
            // 
            this.btnInsertar.Location = new System.Drawing.Point(298, 309);
            this.btnInsertar.Name = "btnInsertar";
            this.btnInsertar.Size = new System.Drawing.Size(103, 23);
            this.btnInsertar.TabIndex = 20;
            this.btnInsertar.Text = "Insertar";
            this.btnInsertar.UseVisualStyleBackColor = true;
            this.btnInsertar.Click += new System.EventHandler(this.btnInsertar_Click);
            // 
            // frmRegistro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 395);
            this.Controls.Add(this.btnInsertar);
            this.Controls.Add(this.cmbTipoDoc);
            this.Controls.Add(this.nmuEdad);
            this.Controls.Add(this.txtTel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDir);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtDoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtApellido);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.Name = "frmRegistro";
            this.Text = "Nuevo Cliente";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmuEdad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbnF;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbnO;
        private System.Windows.Forms.RadioButton rbnM;
        private System.Windows.Forms.TextBox txtApellido;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nmuEdad;
        private System.Windows.Forms.ComboBox cmbTipoDoc;
        private System.Windows.Forms.Button btnInsertar;
    }
}