using System;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
            try
            {
                if (login(txtUsuario.Text, txtClave.Text))
                {

                    using (ConnHandlingTransaction conn = new ConnHandlingTransaction("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\super\\source\\repos\\HealthWave\\healthwave_caja\\CajaComplete\\CajaComplete\\PayDB.mdf;Integrated Security=True;MultipleActiveResultSets=True"))
                    {
                        conn.SetNewTransaction("getLatest");
                        object r = conn.ExecuteScalar();
                        main.inicio_de_dia = main.montoDia = Convert.ToInt32(r);
                       
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
            } catch (TimeoutException)
            {
                return;
            }
        }

        private bool login(string user, string pass)
        {
            CoreInterface core = new CoreInterface();
            Usuario usuario = core.login(user, pass);
            if (usuario == null)
            {
                return false;
            }
            else
            {
                main.employee_name = usuario.nombre;
                main.employee_sur = usuario.apellido;
                main.employee_doc = usuario.codigoDocumento;
                return true;
            }
        }
    }
}
