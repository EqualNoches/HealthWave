namespace CajaComplete
{
    partial class frmPago
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
            this.btnEfectivo = new System.Windows.Forms.Button();
            this.btnTarjeta = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAseguradora = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudDescuento = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudDescuento)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEfectivo
            // 
            this.btnEfectivo.Location = new System.Drawing.Point(20, 254);
            this.btnEfectivo.Name = "btnEfectivo";
            this.btnEfectivo.Size = new System.Drawing.Size(75, 23);
            this.btnEfectivo.TabIndex = 0;
            this.btnEfectivo.Text = "Efectivo";
            this.btnEfectivo.UseVisualStyleBackColor = true;
            this.btnEfectivo.Click += new System.EventHandler(this.btnEfectivo_Click);
            // 
            // btnTarjeta
            // 
            this.btnTarjeta.Location = new System.Drawing.Point(110, 254);
            this.btnTarjeta.Name = "btnTarjeta";
            this.btnTarjeta.Size = new System.Drawing.Size(75, 23);
            this.btnTarjeta.TabIndex = 1;
            this.btnTarjeta.Text = "Tarjeta";
            this.btnTarjeta.UseVisualStyleBackColor = true;
            this.btnTarjeta.Click += new System.EventHandler(this.btnTarjeta_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Metodo de Pago";
            // 
            // txtAseguradora
            // 
            this.txtAseguradora.Location = new System.Drawing.Point(15, 138);
            this.txtAseguradora.Name = "txtAseguradora";
            this.txtAseguradora.Size = new System.Drawing.Size(100, 22);
            this.txtAseguradora.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Subtotal";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Location = new System.Drawing.Point(15, 75);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 22);
            this.txtSubtotal.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Aseguradora";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(173, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Descuento de Aseguradora";
            // 
            // nudDescuento
            // 
            this.nudDescuento.DecimalPlaces = 2;
            this.nudDescuento.Location = new System.Drawing.Point(15, 188);
            this.nudDescuento.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nudDescuento.Name = "nudDescuento";
            this.nudDescuento.Size = new System.Drawing.Size(120, 22);
            this.nudDescuento.TabIndex = 9;
            // 
            // frmPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 325);
            this.Controls.Add(this.nudDescuento);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAseguradora);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTarjeta);
            this.Controls.Add(this.btnEfectivo);
            this.Name = "frmPago";
            this.Text = "Pago";
            this.Load += new System.EventHandler(this.frmPago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDescuento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEfectivo;
        private System.Windows.Forms.Button btnTarjeta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAseguradora;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudDescuento;
    }
}