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
    public partial class frmCuadre : Form
    {
        public string nombre;
        public string apellido;
        public DateTime fecha = DateTime.Now;
        public int dayInit;
        public int dayEnd;
        public int numClientes;
        public int numTrans;

        public frmCuadre()
        {
            InitializeComponent();
        }

        private void frmCuadre_Load(object sender, EventArgs e)
        {
            this.lblFecha.Text = fecha.ToString("yyyy/MM/dd | hh:mm:ss");
            this.txtNom.Text = nombre + " " + apellido;
            this.txtInit.Text = dayInit.ToString();
            this.txtEnd.Text = dayEnd.ToString();
            this.txtCli.Text = numClientes.ToString();
            this.txtTrans.Text = numTrans.ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
