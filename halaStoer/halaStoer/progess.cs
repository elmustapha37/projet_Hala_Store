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
    public partial class progess : Form
    {
        public progess()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar.Increment(3);
            if (progressBar.Value == 100)
            {
                this.Hide();
                login lg = new login();
                lg.Show();
                timer1.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void progressBar_Click(object sender, EventArgs e)
        {
            progressBar.ForeColor = Color.Orange;

        }
    }
}
