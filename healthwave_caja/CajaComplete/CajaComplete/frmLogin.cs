using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CajaComplete
{
    public partial class frmLogin : Form
    {
        frmMain main = new frmMain();
        public frmLogin()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(233, 233, 233); // this should be pink-ish
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (login(txtUsuario.Text, txtClave.Text))
            {
                
                using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
                {
                    conn.SetNewTransaction("getLatest");
                    object r = conn.ExecuteScalar();
                    if (r != null)
                    {
                        main.inicio_de_dia = Convert.ToInt32(r);
                    }
                    else
                    {
                        main.inicio_de_dia = 10_000;
                    }
                }
                main.StartPosition = FormStartPosition.CenterScreen;
                main.FormClosed += (s, args) => this.Close();
                this.Hide();
                main.Show();

            }
            else
            {
                MessageBox.Show("Usuario o clave incorrectos");
            }
            
        }

        private bool login(string user, string pass)
        {
            // Here we get the employee data and assign it to main form IF IT'S TRUE.
            bool got_data = false;
            using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True"))
            {
                conn.SetNewTransaction("getEmployee", new Dictionary<string, object> { { "@nom", user }, { "@pass", pass } });
                SqlDataReader reader = conn.ExecuteReader();
                got_data = reader.HasRows;
                while (reader.Read()) {
                    main.employee_name = user;
                    main.employee_sur = reader.GetString(2);
                    main.employee_doc = reader.GetString(3);
                }
                reader.Close();
            }
            return got_data;
        }
    }
}
