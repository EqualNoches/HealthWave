namespace CajaComplete
{
    partial class frmTransacciones
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
            this.dgvTransacciones = new System.Windows.Forms.DataGridView();
            this.btnFilter = new System.Windows.Forms.Button();
            this.cmbFilter = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnPay = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMonto = new System.Windows.Forms.Label();
            this.lblTel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDoc = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblNom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransacciones)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTransacciones
            // 
            this.dgvTransacciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransacciones.Location = new System.Drawing.Point(30, 111);
            this.dgvTransacciones.Name = "dgvTransacciones";
            this.dgvTransacciones.ReadOnly = true;
            this.dgvTransacciones.RowHeadersWidth = 51;
            this.dgvTransacciones.RowTemplate.Height = 24;
            this.dgvTransacciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransacciones.Size = new System.Drawing.Size(845, 257);
            this.dgvTransacciones.TabIndex = 0;
            this.dgvTransacciones.SelectionChanged += new System.EventHandler(this.dgvTransacciones_SelectionChanged);
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(699, 374);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(97, 41);
            this.btnFilter.TabIndex = 1;
            this.btnFilter.Text = "Buscar";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cmbFilter
            // 
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.Items.AddRange(new object[] {
            "Procedimiento",
            "No Pago",
            "Insertado Por",
            "Fecha de Ingreso"});
            this.cmbFilter.Location = new System.Drawing.Point(30, 374);
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(157, 24);
            this.cmbFilter.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(205, 374);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(468, 22);
            this.txtSearch.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(268, 402);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(119, 39);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Agregar";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnPay
            // 
            this.btnPay.Location = new System.Drawing.Point(436, 402);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(100, 39);
            this.btnPay.TabIndex = 5;
            this.btnPay.Text = "Pagar";
            this.btnPay.UseVisualStyleBackColor = true;
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMonto);
            this.groupBox1.Controls.Add(this.lblTel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lblAge);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblDoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblNom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(86, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 65);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informacion";
            // 
            // lblMonto
            // 
            this.lblMonto.AutoSize = true;
            this.lblMonto.Location = new System.Drawing.Point(574, 31);
            this.lblMonto.Name = "lblMonto";
            this.lblMonto.Size = new System.Drawing.Size(44, 16);
            this.lblMonto.TabIndex = 9;
            this.lblMonto.Text = "Total: ";
            // 
            // lblTel
            // 
            this.lblTel.AutoSize = true;
            this.lblTel.Location = new System.Drawing.Point(468, 43);
            this.lblTel.Name = "lblTel";
            this.lblTel.Size = new System.Drawing.Size(15, 16);
            this.lblTel.TabIndex = 7;
            this.lblTel.Text = "d";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(468, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 16);
            this.label8.TabIndex = 6;
            this.label8.Text = "Telefono";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(327, 43);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(14, 16);
            this.lblAge.TabIndex = 5;
            this.lblAge.Text = "c";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(327, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "Edad";
            // 
            // lblDoc
            // 
            this.lblDoc.AutoSize = true;
            this.lblDoc.Location = new System.Drawing.Point(167, 43);
            this.lblDoc.Name = "lblDoc";
            this.lblDoc.Size = new System.Drawing.Size(15, 16);
            this.lblDoc.TabIndex = 3;
            this.lblDoc.Text = "b";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Documento";
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(17, 43);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(15, 16);
            this.lblNom.TabIndex = 1;
            this.lblNom.Text = "a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre";
            // 
            // frmTransacciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(930, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.dgvTransacciones);
            this.Name = "frmTransacciones";
            this.Text = "Transacciones";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransacciones)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTransacciones;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.ComboBox cmbFilter;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnPay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMonto;
        private System.Windows.Forms.Label lblTel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblNom;
    }
}