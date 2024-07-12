using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Drawing;

namespace CajaComplete
{
    public partial class frmMain : Form
    {
        public int inicio_de_dia;
        public int montoDia = 10_000;
        int num_clientes = 0;
        int num_transacciones = 0;
        public string employee_name;
        public string employee_sur;
        public string employee_doc;

        public frmMain()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(233, 233, 233); // this should be pink-ish
            this.txtClientesAtendidos.Text = num_clientes.ToString();
            this.txtTrans.Text = num_transacciones.ToString();

        }

        private void openTransForm(string nombre, string apellido, string documento)
        {
            using (frmTransacciones trs = new frmTransacciones())
            {
                trs.Nombre = nombre;
                trs.Apellido = apellido;
                trs.Documento = documento;
                trs.Empleado = employee_name + " " + employee_sur;
                trs.getClientInfo();
                trs.StartPosition = FormStartPosition.CenterScreen;
                this.Hide();
                DialogResult result = trs.ShowDialog();
                montoDia += trs.MontoCobrado;
                num_transacciones += trs.transaccionesCobradas;
                if (trs.clienteAtendido)
                    num_clientes++;
                this.txtClientesAtendidos.Text = num_clientes.ToString();
                this.txtTrans.Text = num_transacciones.ToString();
                txtMonto.Text = montoDia.ToString();
                this.Show();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string nom, ape, doc;
            using (frmRegistro frm = new frmRegistro())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                result = frm.ShowDialog();
                nom = frm.Nombre;
                ape = frm.Apellido;
                doc = frm.Documento;
            }
            if (result == DialogResult.OK)
            {
                
                // The idea now is to hide the current form, open the transaction form, and when that's exited we open this form again.
                openTransForm(nom, ape, doc);
            }
        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
            this.montoDia += getAmount("depositar");
            txtMonto.Text = montoDia.ToString();
        }

        private int getAmount(string action)
        {
            string result = Interaction.InputBox($"Ingrese el monto a {action}:", "Modificar Monto en Caja", "0");
            if (int.TryParse(result, out int amount))
            {
                return amount;
            } else
            {
                MessageBox.Show("Monto invalido");
                return getAmount(action);
            }
        }

        private void btnRetiro_Click(object sender, EventArgs e)
        {
            this.montoDia -= getAmount("retirar");
            txtMonto.Text = montoDia.ToString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Cuadre();
            this.SignOut();
            

        }

        private void btnExistente_Click(object sender, EventArgs e)
        {
            DialogResult result;
            string nom, ape, doc;
            using (frmLoginCliente frm = new frmLoginCliente())
            {
                frm.StartPosition = FormStartPosition.CenterScreen;
                result = frm.ShowDialog();
                nom = frm.Nombre;
                ape = frm.Apellido;
                doc = frm.Documento;
            }
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Registro exitoso");

                // The idea now is to hide the current form, open the transaction form, and when that's exited we open this form again.
                openTransForm(nom, ape, doc);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SignOut()
        {
            using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\HealthWave\\healthwave_caja\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True;MultipleActiveResultSets=True"))
            {
                conn.SetNewTransaction("signOut", new Dictionary<string, object> { { "@nom", employee_name }, { "@ap", employee_sur}, { "@doc", employee_doc}, { "@n_cli", num_clientes }, { "@n_trs", num_transacciones }, { "@dia_init", inicio_de_dia }, { "@dia_fin", montoDia } });
                conn.ExecuteNonQuery();
            }
            this.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtMonto.Text = montoDia.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCuadre_Click(object sender, EventArgs e)
        {
            this.Cuadre();
        }

        private void Cuadre()
        {
            string nom = employee_name;
            string doc = employee_doc;
            string sur = employee_sur;
            int dayInit = inicio_de_dia;
            int dayEnd = montoDia;
            DateTime current = DateTime.Now;
            int numClientes = num_clientes;
            int numTrans = num_transacciones;
            frmCuadre c = new frmCuadre()
            {
                nombre = employee_name,
                apellido = employee_sur,
                dayInit = inicio_de_dia,
                dayEnd = montoDia,
                numClientes = num_clientes,
                numTrans = num_transacciones
            };
            c.ShowDialog();
        }
    }
}
