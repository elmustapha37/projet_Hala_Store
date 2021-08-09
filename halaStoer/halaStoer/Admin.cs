using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace halaStoer
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBoxPss.UseSystemPasswordChar = false;
            }
            else
            {
                textBoxPss.UseSystemPasswordChar = true;
            }
        }

        private void btnFP_Click(object sender, EventArgs e)
        {
            textBoxPss.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            login lg = new login();
            lg.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (textBoxPss.Text == "")
            {
                labelInfo.Text = "Entre your password";
            }
            else {
                if (textBoxPss.Text == "1998") { 
                home hm = new home();
                hm.Show();
                this.Hide();
                }
                else
                {
                    labelInfo.Text = "your password is failed";
                }
            }
        }
    }
}
