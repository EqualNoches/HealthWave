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
    public partial class frmRecibo : Form
    {
        public List<Transaccion> transacciones;
        public frmRecibo()
        {
            InitializeComponent();
            if (transacciones == null)
            {
                transacciones = new List<Transaccion>()
                {
                    new Transaccion(){
                        Id = 1,
                        FechaIngreso = DateTime.Now,
                        FechaPago = DateTime.Now,
                        Nombre = "Juan",
                        Apellido = "Perez",
                        Documento = "123456789",
                        Procedimiento = "Administracion de Penicilina",
                        Costo = 100.00f,
                        Metodo = "Efectivo",
                        InsertadoPor = "Admin",
                        Aseguradora = "ARS Reservas",
                        DescuentoAseguradora = 45f,
                        Pago = true
                    },
                    new Transaccion(){
                        Id = 1,
                        FechaIngreso = DateTime.Now,
                        FechaPago = DateTime.Now,
                        Nombre = "Juan",
                        Apellido = "Perez",
                        Documento = "123456789",
                        Procedimiento = "Traqueotomia",
                        Costo = 100.00f,
                        Metodo = "Efectivo",
                        InsertadoPor = "Admin",
                        Aseguradora = "ARS Reservas",
                        Pago = true
                    },
                };
            }
        }

        private void frmRecibo_Load(object sender, EventArgs e)
        {
            this.transaccionBindingSource.DataSource = this.transacciones;
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void transaccionBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
