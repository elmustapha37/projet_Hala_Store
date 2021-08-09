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
    public partial class Produit : Form
    {
        string cnx = @"data source=DESKTOP-V2GONMU\SQLEXPRESS ; initial catalog=hala_store; integrated security=true";
        BindingSource bs = new BindingSource();
        public Produit()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clientt cl = new clientt();
            cl.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            categori ct = new categori();
            ct.Show();
            this.Hide();
        }

        private void Produit_Load(object sender, EventArgs e)
        {
            getData();
            RemplieCombo();
            setBata();
        }
        private void getData()
        {
            SqlConnection cn = new SqlConnection(cnx);
            SqlCommand cmd = new SqlCommand("select * from produit", cn);
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
            textBoxIdP.DataBindings.Add("Text", bs, "idP", true);
            textBoxNomP.DataBindings.Add("Text", bs, "NomP", true);
            textBoxStock.DataBindings.Add("Text", bs, "stock", true);
            textBoxBrand.DataBindings.Add("Text", bs, "brand", true);
            textBoxPrice.DataBindings.Add("Text", bs, "price", true);
            comboBoxC.DataBindings.Add("SelectedValue", bs, "id_c", true);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(cnx))
                {
                    SqlCommand cmd = new SqlCommand("insert into produit (idP,NomP,stock,price,brand,id_c) values(@idP,@NomP,@stock,@price,@brand,@id_c)", cn);
                    cmd.Parameters.AddWithValue("@idP", textBoxIdP.Text);
                    cmd.Parameters.AddWithValue("@NomP", textBoxNomP.Text);
                    cmd.Parameters.AddWithValue("@stock", textBoxStock.Text);
                    cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                    cmd.Parameters.AddWithValue("@brand", textBoxBrand.Text);
                    cmd.Parameters.AddWithValue("@id_c", comboBoxC.SelectedValue);
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
        private void RemplieCombo()
        {
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                SqlCommand cmd = new SqlCommand("select id_c,nom_c from category", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                comboBoxC.DataSource=dt;
                comboBoxC.DisplayMember="nom_c";
                comboBoxC.ValueMember="id_c";
            }
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnx))
            {
                SqlCommand cmd = new SqlCommand("select * from produit", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
                cn.Close();
                labelInfo.Text = "Afficher is Success...";
            }
        }

        private void bntAnn_Click(object sender, EventArgs e)
        {
            textBoxIdP.Text="";
            textBoxNomP.Text = "";
            textBoxStock.Text = "";
            textBoxBrand.Text = "";
            textBoxPrice.Text = "";
            textBoxRecherch.Text = "";
            labelInfo.Text = "Annuler is success ...";

        }

        private void btnSup_Click(object sender, EventArgs e)
        {
            if (textBoxIdP.Text == "" || textBoxNomP.Text == "" || textBoxStock.Text == "" || textBoxBrand.Text == "" || textBoxPrice.Text == "")
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
                    SqlCommand cmd = new SqlCommand("Delete produit where idP=@idP", cn);
                    cmd.Parameters.AddWithValue("@idP", textBoxIdP.Text);
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
            if (textBoxIdP.Text == "" || textBoxNomP.Text == "" || textBoxStock.Text == "" || textBoxBrand.Text == "" || textBoxPrice.Text == "")
            {
                labelInfo.Text = "information is vide ... !";
            }
            else
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(cnx))
                    {
                        SqlCommand cmd = new SqlCommand("update produit set NomP=@nomp,price=@price,stock=@stock,brand=@brand,id_c=@id_c where idP=@idP", cn);
                        cmd.Parameters.AddWithValue("@nomp", textBoxNomP.Text);
                        cmd.Parameters.AddWithValue("@price", textBoxPrice.Text);
                        cmd.Parameters.AddWithValue("@stock", textBoxStock.Text);
                        cmd.Parameters.AddWithValue("@brand", textBoxBrand.Text);
                        cmd.Parameters.AddWithValue("@id_c", comboBoxC.SelectedValue);
                        cmd.Parameters.AddWithValue("@idP", textBoxIdP.Text);
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

        private void btnRech_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(cnx)) 
            {
                string q = "select * from produit where idP=@idpr";
                SqlCommand cmd = new SqlCommand(q, cn);
                cn.Open();
                cmd.Parameters.AddWithValue("@idpr", textBoxRecherch.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dr;
            }
            //int pos = bs.Find("idP", textBoxRecherch.Text);
            //if (pos >= 0)
            //{
            //    bs.Position = pos;
            //}
            //else
            //{
            //    labelInfo.Text = "le Produit n'pas existe ...!";
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Comand cm = new Comand();
            cm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
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
