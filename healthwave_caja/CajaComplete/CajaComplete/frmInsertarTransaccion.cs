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
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Procedimiento = txtProc.Text;
            this.Costo = (float)nmuCosto.Value;
            this.Close();

        }
    }
}
