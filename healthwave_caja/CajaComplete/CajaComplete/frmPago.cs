using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;

namespace CajaComplete
{
    public partial class frmPago : Form
    {
        public List<int> ids = new List<int>();
        public List<double> costs = new List<double>();
        double sum = 0;
        public int montoCobrado = 0;
        public int n_cobrado = 0;
        public frmPago()
        {
            InitializeComponent();
        }

        public List<Transaccion> createTransactions()
        {
            List<Transaccion> transacciones = new List<Transaccion>();
            foreach (int id in ids)
            {
                using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
                {
                    conn.SetNewTransaction("getTransactionById", new Dictionary<string, object> { { "@id", id } });
                    SqlDataReader reader = conn.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            transacciones.Add(new Transaccion()
                            {
                                Id = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Documento = reader.GetString(2),
                                Procedimiento = reader.GetString(3),
                                Costo = reader.GetFloat(4),
                                Pago = reader.GetBoolean(5),
                                Metodo = reader.GetString(6),
                                FechaIngreso = reader.GetDateTime(7),
                                FechaPago = reader.GetDateTime(8),
                                Aseguradora = reader.GetString(9),
                                InsertadoPor = reader.GetString(10),
                                Apellido = reader.GetString(11),
                                DescuentoAseguradora = reader.GetFloat(12)

                            });
                        }
                    }
                }
            }
            return transacciones;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnEfectivo_Click(object sender, EventArgs e)
        {
            string result = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el monto a pagar", "Pago en efectivo", "0", 0, 0);
            if (!float.TryParse(result, out float monto))
            {
                MessageBox.Show("Monto inválido");
                btnEfectivo_Click(sender, e);
                return;
            }
            if (monto > sum)
            {
                MessageBox.Show($"Favor devolver ${monto-sum} al cliente.", "Devuelta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } else if (monto < sum)
            {
                MessageBox.Show("Monto insuficiente");
                btnEfectivo_Click(sender, e);
                return;
            }
            if (!payTransactions("Efectivo"))
            {
                MessageBox.Show("[DEV]: reminder to put a cache backup and ping. We can do that directly in the Transact module tbh.");
                return;
            }
            montoCobrado += (int)Math.Round(monto);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool payTransactions(string metodo)
        {
            try
            {
                foreach(int id in ids)
                {
                    using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
                    {
                        conn.SetNewTransaction("payTransaction", new Dictionary<string, object> { { "@id", id }, { "@aseguradora", txtAseguradora.Text.IsNullOrEmpty() ? "" : txtAseguradora.Text }, { "@descuento", nudDescuento.Value }, { "@metodo", metodo } });
                        conn.ExecuteNonQuery();
                    }
                }
                n_cobrado += ids.Count;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al pagar la factura: " + ex.Message);
                return false;
            }
        }
        private void frmPago_Load(object sender, EventArgs e)
        {
            sum = costs.Sum();
            txtSubtotal.Text = sum.ToString();
        }

        private void btnTarjeta_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Favor pagar en terminal de tarjeta", "Pago con tarjeta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            if (!payTransactions("Tarjeta"))
            {
                MessageBox.Show("[DEV]: reminder to put a cache backup and ping. We can do that directly in the Transact module tbh.");
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
