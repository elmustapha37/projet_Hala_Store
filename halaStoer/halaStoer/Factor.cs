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
    public partial class Factor : Form
    {

        string strcn = @"data source=DESKTOP-V2GONMU\SQLEXPRESS ; initial catalog=hala_store; integrated security=true";
        DataSet ds = new DataSet();
        SqlConnection cn = new SqlConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        public Factor()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produit pr = new Produit();
            pr.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Comand cm = new Comand();
            cm.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            categori ct = new categori();
            ct.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clientt cl = new clientt();
            cl.Show();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bntAnn_Click(object sender, EventArgs e)
        {
            textBoxRecherch.Text = "";
            dataGridView1.DataSource = null;
        }

        private void Factor_Load(object sender, EventArgs e)
        {
        }
        private void GetFactur()
        {
            SqlCommand selectCommand = new SqlCommand("select * from factur", cn);
            da.SelectCommand = selectCommand;
            da.Fill(ds, "factur");
            dataGridView1.DataSource = ds.Tables["factur"];
            //création de DeleteCommand
            SqlCommand deleteCommand = new SqlCommand("delete from factur where id_F=@id_F", cn);
            deleteCommand.Parameters.Add("@id_F", SqlDbType.Int, 0, "id_F");
            da.DeleteCommand = deleteCommand;
            //création de UpdateCommand
            SqlCommand updateCommand = new SqlCommand("update factur set Nomclient=@nomc,NomProduit=@nomp,Qnt=@Qnt,price=@price,total=@total"
                +" where id_F=@id", cn);
            updateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id_F");
            updateCommand.Parameters.Add("@nomc", SqlDbType.NVarChar, 50, "Nomclient");
            updateCommand.Parameters.Add("@nomp", SqlDbType.NVarChar, 50, "NomProduit");
            updateCommand.Parameters.Add("@Qnt", SqlDbType.Int, 0, "Qnt");
            updateCommand.Parameters.Add("@price", SqlDbType.Decimal, 0, "price");
            updateCommand.Parameters.Add("@total", SqlDbType.Decimal, 0, "total");
            da.UpdateCommand = updateCommand;
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            cn.ConnectionString = strcn;
            GetFactur();
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            da.Update(ds.Tables["factur"]);
        }

        private void btnRech_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(strcn))
            {
                SqlDataAdapter daCl = new SqlDataAdapter();
                daCl.SelectCommand = new SqlCommand();
                daCl.SelectCommand.Connection = cn;
                daCl.SelectCommand.CommandText = "Select * from factur where id_F=@idf";
                daCl.SelectCommand.Parameters.AddWithValue("@idf", textBoxRecherch.Text);
                DataSet ds = new DataSet();
                daCl.Fill(ds, "factur");
                dataGridView1.DataSource = ds.Tables["factur"];
            }
        }
    }
}
