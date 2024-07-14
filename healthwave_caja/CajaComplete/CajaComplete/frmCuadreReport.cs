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
    public partial class frmCuadreReport : Form
    {
        public List<Cuadre> cuadres = new List<Cuadre>();
        public string nombre;
        public string apellido;
        public DateTime fecha = DateTime.Now;
        public int dayInit;
        public int dayEnd;
        public int numClientes;
        public int numTrans;
        public frmCuadreReport()
        {
            InitializeComponent();
        }

        private void frmCuadreReport_Load(object sender, EventArgs e)
        {
            cuadres.Add(new Cuadre()
            {
                Nombre = nombre + " " + apellido,
                Fecha = fecha,
                montoInicio = dayInit,
                montoFin = dayEnd,
                numClientes = numClientes,
                numTrans = numTrans
            });
            this.cuadreBindingSource.DataSource = cuadres;
            this.reportViewer1.RefreshReport();
        }
    }
}
