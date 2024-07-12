namespace CajaComplete
{
    partial class frmInsertarTransaccion
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
            this.txtProc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nmuCosto = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInsertar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmuCosto)).BeginInit();
            this.SuspendLayout();
            // 
            // txtProc
            // 
            this.txtProc.Location = new System.Drawing.Point(135, 25);
            this.txtProc.Multiline = true;
            this.txtProc.Name = "txtProc";
            this.txtProc.Size = new System.Drawing.Size(300, 225);
            this.txtProc.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Procedimiento";
            // 
            // nmuCosto
            // 
            this.nmuCosto.DecimalPlaces = 2;
            this.nmuCosto.Location = new System.Drawing.Point(135, 257);
            this.nmuCosto.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.nmuCosto.Name = "nmuCosto";
            this.nmuCosto.Size = new System.Drawing.Size(120, 22);
            this.nmuCosto.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Costo";
            // 
            // btnInsertar
            // 
            this.btnInsertar.Location = new System.Drawing.Point(220, 300);
            this.btnInsertar.Name = "btnInsertar";
            this.btnInsertar.Size = new System.Drawing.Size(118, 47);
            this.btnInsertar.TabIndex = 4;
            this.btnInsertar.Text = "Insertar";
            this.btnInsertar.UseVisualStyleBackColor = true;
            this.btnInsertar.Click += new System.EventHandler(this.btnInsertar_Click);
            // 
            // frmInsertarTransaccion
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 380);
            this.Controls.Add(this.btnInsertar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nmuCosto);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtProc);
            this.Name = "frmInsertarTransaccion";
            this.Text = "Insertar Transaccion";
            ((System.ComponentModel.ISupportInitialize)(this.nmuCosto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtProc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nmuCosto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnInsertar;
    }
}