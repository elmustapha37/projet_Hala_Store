using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace halaStoer
{
    public partial class login : Form
    {
        string cnx = @"data source=DESKTOP-V2GONMU\SQLEXPRESS ; initial catalog=hala_store; integrated security=true";

        public login()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnFP_Click(object sender, EventArgs e)
        {
            textBoxUser.Text = "";
            textBoxPss.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
            textBoxPss.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPss.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            ad.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUser.Text == "" || textBoxPss.Text == "")
            {
                MessageBox.Show("Entre your user name & password !!");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection(cnx))
                {
                    SqlCommand cmd = new SqlCommand("select * from client where cin='"+textBoxUser.Text+"' and pasword='"+textBoxPss.Text+"'", cn);
                    cn.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    int a = ds.Tables[0].Rows.Count;
                    if (a == 0)
                    {
                        MessageBox.Show("Wrong Username or password..");
                    }
                    else{
                        Vent vn = new Vent();
                        vn.Show();
                        this.Hide();
                    }
                }
            }
           
        }
    }
}
