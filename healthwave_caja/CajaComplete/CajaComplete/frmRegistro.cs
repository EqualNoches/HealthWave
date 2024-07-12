using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CajaComplete
{
    public partial class frmRegistro : Form
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Documento { get; set; }

        public frmRegistro()
        {
            InitializeComponent();
        }

        public void clean()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDir.Clear();
            txtTel.Clear();
            txtCorreo.Clear();
            txtDoc.Clear();
            cmbTipoDoc.SelectedIndex = -1;
            rbnF.Checked = false;
            rbnM.Checked = false;
            rbnO.Checked = false;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            string nom = txtNombre.Text;
            string ape = txtApellido.Text;
            string dir = txtDir.Text;
            string tel = txtTel.Text;
            string email = txtCorreo.Text;
            string tipodoc = cmbTipoDoc.Text;
            string doc = txtDoc.Text;
            int sex = rbnF.Checked ? 1 : (rbnM.Checked ? 0 : (rbnO.Checked ? 2 : -1));
            
            if (checkFields(nom, ape, dir, tel, email, tipodoc, doc, sex))
            {
                MessageBox.Show("check is good, add stuff");
                using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    dict.Add("@nom", nom);
                    dict.Add("@ap", ape);
                    dict.Add("@dir", dir);
                    dict.Add("@tel", tel);
                    dict.Add("@cor", email);
                    dict.Add("@td", tipodoc);
                    dict.Add("@doc", doc);
                    dict.Add("@sex", sex);
                    dict.Add("@age", (int)nmuEdad.Value);
                    conn.SetNewTransaction("insertCliente", dict);
                    int result = (int)conn.ExecuteNonQuery();
                    if (result == 1)
                    {
                        MessageBox.Show("Registro exitoso");
                        clean();
                        this.Nombre = nom;
                        this.Apellido = ape;
                        this.Documento = doc;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Registro fallido");
                    }
                }
                
            } else
            {
                MessageBox.Show("Hay campos vacios, favor revisar", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private bool checkFields(string nom, string ape, string dir, string tel, string email, string tipodoc, string doc, int sex)
        {
            if (nom == "" || ape == "" || dir == "" || tel == "" || email == "" || tipodoc == "" || doc == "" || sex == -1)
            {
                return false;
            }
            return true;
        }
    }
}
