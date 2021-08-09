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
    public partial class Comand : Form
    {
        string cnx = @"data source=DESKTOP-V2GONMU\SQLEXPRESS ; initial catalog=hala_store; integrated security=true";
        BindingSource bs = new BindingSource();
        public Comand()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clientt cl = new clientt();
            cl.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produit p = new Produit();
            p.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            categori ca = new categori();
            ca.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bntAnn_Click(object sender, EventArgs e)
        {
            textBoxIdCmd.Text = "";
            textBoxNc.Text = "";
            textBoxNomP.Text = "";
            textBoxPrice.Text = "";
            textBoxQnt.Text = "";
            textBoxTotal.Text = "";
            labelInfo.Text = "Annuler is Success ...";
        }

        private void btnRech_Click(object sender, EventArgs e)
        {
            int pos = bs.Find("idCmd", textBoxRecherch.Text);
            if (pos >= 0)
            {
                bs.Position = pos;
            }
            else
            {
                labelInfo.Text = "le Command n'pas existe ...!";
            }
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            if (textBoxIdCmd.Text == "" || textBoxNc.Text == "" || textBoxNomP.Text == "" || textBoxTotal.Text == "" || textBoxPrice.Text == "" || textBoxQnt.Text == "")
            {
                labelInfo.Text = "information is vide ... !";
            }
            else
            {
                SupprimerClient();
            }
        }
        private void SupprimerClient()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnx))
                {
                    SqlCommand cmd = new SqlCommand("Delete command where idCmd=@idCmd", cn);
                    cmd.Parameters.AddWithValue("@idCmd", textBoxIdCmd.Text);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    labelInfo.Text = "Supprimer is Success...";
                }
            }
            catch (Exception ex)
            {
                labelInfo.Text = ex.Message;
            }
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            if (textBoxIdCmd.Text == "" || textBoxNc.Text == "" || textBoxNomP.Text == "" || textBoxTotal.Text == "" || textBoxPrice.Text == "" || textBoxQnt.Text == "")
            {
                labelInfo.Text = "information is vide ... !";
            }
             else
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(cnx))
                    {
                        SqlCommand cmd = new SqlCommand("update command set Nomclient=@Nomclient,NomProduit=@NomProduit,price=@price,Qnt=@Qnt,total=@total where idCmd=@idCmd", cn);
                        cmd.Parameters.AddWithValue("@Nomclient", textBoxNc.Text);
                        cmd.Parameters.AddWithValue("@NomProduit", textBoxNomP.Text);
                        cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                        cmd.Parameters.AddWithValue("@Qnt", textBoxQnt.Text);
                        cmd.Parameters.AddWithValue("@total", textBoxTotal.Text);
                        cmd.Parameters.AddWithValue("@idCmd", textBoxIdCmd.Text);
                        cn.Open();
                        int c = cmd.ExecuteNonQuery();
                        labelInfo.Text = "Modification is Success...";
                        bs.EndEdit();
                    }
                }
                catch (Exception ex)
                {
                    labelInfo.Text = ex.Message;
                }
            }
        }

        private void Command_Load(object sender, EventArgs e)
        {
            getData();
            setBata();
        }
        private void getData()
        {
            SqlConnection cn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand("select * from command", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            bs.DataSource = dt;
            //dv = dt.DefaultView;
            //dataGridView1.DataSource = dv;
        }
        private void setBata()
        {
            textBoxIdCmd.DataBindings.Add("Text", bs, "idCmd", true);
            textBoxNomP.DataBindings.Add("Text", bs, "NomProduit", true);
            textBoxNc.DataBindings.Add("Text", bs, "Nomclient", true);
            textBoxQnt.DataBindings.Add("Text", bs, "Qnt", true);
            textBoxPrice.DataBindings.Add("Text", bs, "price", true);
            textBoxTotal.DataBindings.Add("Text", bs, "total", true);
        }

        private void btnsecond_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void btnfirst_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                SqlCommand cmd = new SqlCommand("select * from command", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                cn.Close();
                labelInfo.Text = "Afficher is Success...";
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            DateTime DateCmd = DateTime.Now;
            try
            {
                using (SqlConnection cn = new SqlConnection(cnx))
                {
                    SqlCommand cmd = new SqlCommand("insert into command (idCmd,Nomclient,NomProduit,Qnt,price,total,Date_Cmd) values(@idCmd,@NomC,@NomP,@Qnt,@price,@total,@Date_Cmd)", cn);
                    cmd.Parameters.AddWithValue("@idCmd", textBoxIdCmd.Text);
                    cmd.Parameters.AddWithValue("@NomC", textBoxNc.Text);
                    cmd.Parameters.AddWithValue("@NomP", textBoxNomP.Text);
                    cmd.Parameters.AddWithValue("@Qnt", textBoxQnt.Text);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@total", textBoxTotal.Text);
                    cmd.Parameters.AddWithValue("@Date_Cmd", DateCmd);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    labelInfo.Text = "Ajouter is Success...";
                    bs.EndEdit();
                }
            }
            catch (Exception ex)
            {
                labelInfo.Text = ex.Message;
            }
        }

        private void textBoxRecherch_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBoxNc_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            home h = new home();
            h.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Factor f = new Factor();
            f.Show();
            this.Hide();
        }
    }
}
