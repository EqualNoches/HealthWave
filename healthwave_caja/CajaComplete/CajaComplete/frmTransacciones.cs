using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace CajaComplete
{
    public partial class frmTransacciones : Form
    {
        int montoCobrado = 0;
        public int transaccionesCobradas = 0;
        public bool clienteAtendido = false;
        Dictionary<string, string> filters = new Dictionary<string, string> { { "Procedimiento", "Procedimiento LIKE " }, { "Insertado Por", "InsertadoPor LIKE " }, {"Fecha de Ingreso", "CONVERT(FechaIngreso, 'System.String') LIKE " }, { "No Pago", "Pago = 0"} };
        public int MontoCobrado { get { return montoCobrado; } set { montoCobrado = value; } }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public int Edad {  get; set; }
        public int Sexo { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public frmTransacciones()
        {
            InitializeComponent();
            
        }

        public void Filter(string query)
        {
            DataView dv = this.dgvTransacciones.DataSource as DataView;
            dv.RowFilter = query;
            this.dgvTransacciones.DataSource = dv;
        }

        public void getClientInfo()
        {
            this.dgvTransacciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransacciones.AllowUserToAddRows = false;
            DataTable dt = new DataTable();
            using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
            {
                conn.SetNewTransaction("fetchCliente", new Dictionary<string, object> { { "@nom", this.Nombre }, { "@ap", this.Apellido }, { "@doc", this.Documento } });
                SqlDataReader data = conn.ExecuteReader();                
                dt.Load(data);
                data.Close();
            }

            DataRow row = dt.Rows[0];
            this.Edad = (int)row["Edad"];
            this.Sexo = (int)row["Sexo"];
            this.Direccion = (string)row["Direccion"];
            this.Correo = (string)row["Correo"];
            this.Telefono = (string)row["Telefono"];

            this.lblNom.Text = this.Nombre + " " + this.Apellido;
            this.lblDoc.Text = this.Documento;
            this.lblAge.Text = this.Edad.ToString();
            this.lblTel.Text = this.Telefono;

            this.refreshData();
            this.dgvTransacciones.Columns[0].Visible = false;
            this.dgvTransacciones.Columns[1].Visible = false;
            this.dgvTransacciones.Columns[2].Visible = false;
        }

        private void refreshData()
        {
            using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
            {
                conn.SetNewTransaction("fetchTransacciones", new Dictionary<string, object> { { "@nom", this.Nombre }, { "@ap", this.Apellido }, { "@doc", this.Documento } });
                SqlDataReader data = conn.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(data);
                data.Close();
                DataView dv = new DataView(dt);
                this.dgvTransacciones.DataSource = dv;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string proc;
            float cost;
            bool insert = false;
            using (frmInsertarTransaccion frm = new frmInsertarTransaccion())
            {
                result = frm.ShowDialog();
                proc = frm.Procedimiento;
                cost = frm.Costo;
            }
            if (result == DialogResult.OK)
            {
                using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object> {
                        { "@nom", this.Nombre },
                        { "@ap", this.Apellido },
                        { "@doc", this.Documento },
                        { "@proc", proc},
                        { "@cost", cost },
                        { "@ip", "Employee" }
                    };
                    conn.SetNewTransaction("insertTransaccion", parameters);
                    int? success = conn.ExecuteNonQuery();
                    if (success != 1)
                    {
                        // handle an error
                        MessageBox.Show("Error al insertar transaccion");
                    }
                    else
                    {
                        insert = true;
                    }
                }
                if (insert)
                {
                    this.refreshData();
                }
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (this.dgvTransacciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una transaccion para pagar");
                return;
            }
            
            // Compile all the selected payments, and get their id and cost
            List<int> ids = new List<int>();
            List<double> costs = new List<double>();
            foreach (DataGridViewRow row in this.dgvTransacciones.SelectedRows)
            {
                ids.Add((int)row.Cells[0].Value);
                costs.Add((double)row.Cells[4].Value); // check if this is actually the correct value.
                MessageBox.Show(costs.Sum().ToString());
            }
            frmPago p = new frmPago();
            p.ids = ids;
            p.costs = costs;
            p.ShowDialog();
            if (p.DialogResult == DialogResult.OK)
            {
                this.refreshData();
                montoCobrado += p.montoCobrado;
                transaccionesCobradas += p.n_cobrado;
                clienteAtendido = true;
                frmRecibo r = new frmRecibo();
                r.transacciones = p.createTransactions();
                r.ShowDialog();
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            if (cmbFilter.Text == "")
            {
                this.refreshData();
                return;
            }

            string query = "";
            if (cmbFilter.Text != "No Pago")
            {
                if (txtSearch.Text == "")
                {
                    MessageBox.Show("Favor de llenar el campo de busqueda.", "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                query += $"'%{txtSearch.Text}%'";
            }
            query = this.filters[cmbFilter.Text] + query;
            this.Filter(query);
        }

        private void dgvTransacciones_SelectionChanged(object sender, EventArgs e)
        {
            double sum = 0;
            foreach(DataGridViewRow row in this.dgvTransacciones.SelectedRows)
            {
                if (row.Cells[4].Value != null)
                    sum += (double)row.Cells[4].Value;
            }
            this.lblMonto.Text = sum.ToString();
        }
    }
}
