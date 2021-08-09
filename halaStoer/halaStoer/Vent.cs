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
    public partial class Vent : Form
    {
        string cnx = @"data source=DESKTOP-V2GONMU\SQLEXPRESS ; initial catalog=hala_store; integrated security=true";
        public Vent()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            login lg = new login();
            lg.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Vent_Load(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                cn.Open();
                string query = "select nomP,price,brand,stock from produit";
                SqlDataAdapter da = new SqlDataAdapter(query, cn);
                SqlCommandBuilder cb = new SqlCommandBuilder(da);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                cn.Close();
            }
        }
        int n = 0;
        double sum = 0;
        private void btnMod_Click(object sender, EventArgs e)
        {
            if (textBoxQnt.Text == "" || textBoxPrice1.Text == "" || textBoxNomPr.Text=="")
            {
                labelInfo.Text = "Entre Quantite ou Price Svp ...!";
            }
            else
            {
                double total = Double.Parse(textBoxQnt.Text) * Double.Parse(textBoxPrice1.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(dataGridFact);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = textBoxNomC.Text;
                newRow.Cells[2].Value = textBoxNomPr.Text;
                newRow.Cells[3].Value = textBoxPrice1.Text;
                newRow.Cells[4].Value = textBoxQnt.Text;
                newRow.Cells[5].Value = total;
                dataGridFact.Rows.Add(newRow);
                labelInfo.Text = "Facteur is Success ...!";
                n++;
                sum = sum + total;
                labelSum.Text = " " + sum + " DH";
                AjouteFac();
            }
        }
        private void AjouteFac()
        {
            decimal t = decimal.Parse(textBoxQnt.Text) * decimal.Parse(textBoxPrice1.Text);
            using (hala_storeEntities1 context = new hala_storeEntities1())
            {
                List<factur> facturs = context.facturs.ToList<factur>();
                int idf = facturs.Count() + 1;
                var cl = context.facturs.FirstOrDefault(c => c.id_F == idf);
                if (cl != null)
                {
                    labelInfo.Text = "déje un facture qui a la clé!!!";
                    return;
                }
                factur ft = new factur();
                ft.id_F = idf;
                ft.Nomclient = textBoxNomC.Text;
                ft.NomProduit = textBoxNomPr.Text;
                ft.price = decimal.Parse(textBoxPrice1.Text);
                ft.Qnt = int.Parse(textBoxQnt.Text);
                ft.total =t;
                context.facturs.Add(ft);
                context.SaveChanges();
                labelInfo.Text = "Ajouter is success ..!!";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxNomC.Text = "";
            textBoxNomPr.Text = "";
            textBoxPrice1.Text = "";
            textBoxQnt.Text = "";
        }

        private void dataGridFact_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxPrice1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxNomPr.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
