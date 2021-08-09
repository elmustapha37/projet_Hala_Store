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
     public partial class categori : Form
    {
        string cnx = @"data source=DESKTOP-V2GONMU\SQLEXPRESS ; initial catalog=hala_store; integrated security=true";
        BindingSource bs = new BindingSource();
        public categori()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Produit pr = new Produit();
            pr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clientt ctt = new clientt();
            ctt.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRech_Click(object sender, EventArgs e)
        {
            int pos = bs.Find("id_c", textBoxRecherch.Text);
            if (pos >= 0)
            {
                bs.Position = pos;
            }
            else
            {
                labelInfo.Text = "la Cateegory n'pas existe ...!";
            }
        }

        private void categori_Load(object sender, EventArgs e)
        {
            getData();
            setBata();
        }
        private void getData()
        {
            SqlConnection cn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand("select * from category", cn);
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
            textBoxIdCat.DataBindings.Add("Text", bs, "id_c", true);
            textBoxNom.DataBindings.Add("Text", bs, "nom_c", true);
        }

        private void bntAnn_Click(object sender, EventArgs e)
        {
            textBoxIdCat.Text = "";
            textBoxNom.Text = "";
            labelInfo.Text = "Annuler is success ...";
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
                SqlCommand cmd = new SqlCommand("select * from category", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                cn.Close();
                labelInfo.Text = "Afficher is Success...";
            }
        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            if (textBoxIdCat.Text == "" || textBoxNom.Text == "")
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
                    SqlCommand cmd = new SqlCommand("Delete category where id_c=@id_c", cn);
                    cmd.Parameters.AddWithValue("@id_c", textBoxIdCat.Text);
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
            if (textBoxIdCat.Text == "" || textBoxNom.Text == "")
            {
                labelInfo.Text = "information is vide ... !";
            }
            else
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(cnx))
                    {
                        SqlCommand cmd = new SqlCommand("update category set nom_c=@nom_c where id_c=@id_c", cn);
                        cmd.Parameters.AddWithValue("@nom_c", textBoxNom.Text);
                        cmd.Parameters.AddWithValue("@id_c", textBoxIdCat.Text);
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

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnx))
                {
                    SqlCommand cmd = new SqlCommand("insert into category (id_c,nom_c) values(@id_c,@nom_c)", cn);
                    cmd.Parameters.AddWithValue("@id_c", textBoxIdCat.Text);
                    cmd.Parameters.AddWithValue("@nom_c", textBoxNom.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            Comand cm = new Comand();
            cm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Factor f = new Factor();
            f.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }
    }
}
