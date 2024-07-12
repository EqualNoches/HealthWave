namespace CajaComplete
{
    partial class frmMain
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMonto = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExistente = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnCuadre = new System.Windows.Forms.Button();
            this.txtClientesAtendidos = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTrans = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCerrar.Location = new System.Drawing.Point(65, 181);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(145, 98);
            this.btnCerrar.TabIndex = 0;
            this.btnCerrar.Text = "Cerrar Sesion";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTrans);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtClientesAtendidos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMonto);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 189);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion";
            // 
            // txtMonto
            // 
            this.txtMonto.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtMonto.Location = new System.Drawing.Point(6, 48);
            this.txtMonto.Name = "txtMonto";
            this.txtMonto.ReadOnly = true;
            this.txtMonto.Size = new System.Drawing.Size(148, 22);
            this.txtMonto.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Monto Actual en Caja";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnExistente);
            this.groupBox2.Controls.Add(this.btnCerrar);
            this.groupBox2.Location = new System.Drawing.Point(202, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 300);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Transacciones";
            // 
            // btnExistente
            // 
            this.btnExistente.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExistente.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExistente.Location = new System.Drawing.Point(15, 36);
            this.btnExistente.Name = "btnExistente";
            this.btnExistente.Size = new System.Drawing.Size(236, 113);
            this.btnExistente.TabIndex = 2;
            this.btnExistente.Text = "Abrir cuenta de Cliente";
            this.btnExistente.UseVisualStyleBackColor = false;
            this.btnExistente.Click += new System.EventHandler(this.btnExistente_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNuevo.Location = new System.Drawing.Point(510, 231);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(112, 27);
            this.btnNuevo.TabIndex = 1;
            this.btnNuevo.Text = "Usuario Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnCuadre
            // 
            this.btnCuadre.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCuadre.Location = new System.Drawing.Point(492, 127);
            this.btnCuadre.Name = "btnCuadre";
            this.btnCuadre.Size = new System.Drawing.Size(145, 98);
            this.btnCuadre.TabIndex = 3;
            this.btnCuadre.Text = "Abrir Cuadre";
            this.btnCuadre.UseVisualStyleBackColor = false;
            this.btnCuadre.Click += new System.EventHandler(this.btnCuadre_Click);
            // 
            // txtClientesAtendidos
            // 
            this.txtClientesAtendidos.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtClientesAtendidos.Location = new System.Drawing.Point(6, 102);
            this.txtClientesAtendidos.Name = "txtClientesAtendidos";
            this.txtClientesAtendidos.ReadOnly = true;
            this.txtClientesAtendidos.Size = new System.Drawing.Size(148, 22);
            this.txtClientesAtendidos.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Clientes Atendidos";
            // 
            // txtTrans
            // 
            this.txtTrans.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtTrans.Location = new System.Drawing.Point(6, 154);
            this.txtTrans.Name = "txtTrans";
            this.txtTrans.ReadOnly = true;
            this.txtTrans.Size = new System.Drawing.Size(148, 22);
            this.txtTrans.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Transacciones Hechas";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CajaComplete.Properties.Resources.WhatsApp_Image_2024_06_28_at_3_401;
            this.pictureBox1.Location = new System.Drawing.Point(226, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 87);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(649, 430);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCuadre);
            this.Controls.Add(this.btnNuevo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "frmMain";
            this.Text = "Pantalla Principal";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtMonto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExistente;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.TextBox txtTrans;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtClientesAtendidos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCuadre;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

