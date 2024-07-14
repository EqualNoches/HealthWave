using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CajaComplete
{
    public partial class frmInsertarTransaccion : Form
    {
        public string Procedimiento { get; set; }
        public float Costo { get; set; }

        public frmInsertarTransaccion()
        {
            InitializeComponent();
            CoreInterface coreInterface = new CoreInterface();
            this.dataGridView1.DataSource = coreInterface.GetServicios().Select(u => new { u.nombre, u.costo }).ToList();

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            DataGridViewRow selected = this.dataGridView1.SelectedRows[0];
            this.DialogResult = DialogResult.OK;
            this.Procedimiento = selected.Cells[0].Value.ToString();
            this.Costo = float.Parse(selected.Cells[1].Value.ToString());
            this.Close();

        }
    }
}
