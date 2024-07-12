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

namespace CajaComplete
{
    public partial class frmLoginCliente : Form
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }
        public frmLoginCliente()
        {
            InitializeComponent();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtNom.Text == "" || txtAp.Text == "" || txtDoc.Text == "")
            {
                MessageBox.Show("Hay campos vacios, favor revisar", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlParameter output = new SqlParameter("@existe", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Output
                };
                using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
                {
                    conn.SetNewTransaction("clienteExiste", new Dictionary<string, object> { { "@nom", txtNom.Text }, { "@ap", txtAp.Text }, { "@doc", txtDoc.Text } });
                    conn.cmd.Parameters.Add(output);
                    conn.ExecuteNonQuery();
                }
                if ((bool)output.Value)
                {
                    this.Nombre = txtNom.Text;
                    this.Apellido = txtAp.Text;
                    this.Documento = txtDoc.Text;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El cliente no existe en la base de datos", "Cliente no existe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
