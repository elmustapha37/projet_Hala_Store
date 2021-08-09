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
   
    public partial class clientt : Form
    {
        hala_storeEntities context = new hala_storeEntities();
        public clientt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Produit pr = new Produit();
            pr.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            categori ct = new categori();
            ct.Show();
            this.Hide();
        }

        private void client_Load(object sender, EventArgs e)
        {
        }
        private void getData()
        {
            using (hala_storeEntities context = new hala_storeEntities())
            {
                List<client> clients = context.clients.ToList<client>();
                dataGridView1.DataSource = clients;
            }   
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            int idcl = int.Parse(textBoxIdC.Text);
            using (hala_storeEntities context = new hala_storeEntities())
            {
                var cl = context.clients.FirstOrDefault(c => c.idC == idcl);
                if (cl != null)
                {
                    labelInfo.Text = "déje un client qui a la clé!!!";
                    return;
                }
                client clnt = new client();
                clnt.idC = idcl;
                clnt.NomC = textBoxNom.Text;
                clnt.cin = textBoxCin.Text;
                clnt.pasword = textBoxPsw.Text;
                context.clients.Add(clnt);
                context.SaveChanges();
                labelInfo.Text = "Ajouter is success ..!!";
            }
        }

        private void btnMod_Click(object sender, EventArgs e)
        {
            int idcl = int.Parse(textBoxIdC.Text);
            using (hala_storeEntities context = new hala_storeEntities())
            {
                var cl = context.clients.FirstOrDefault(c => c.idC == idcl);
                if (cl != null)
                {
                    cl.idC = idcl;
                    cl.NomC = textBoxNom.Text;
                    cl.cin = textBoxCin.Text;
                    cl.pasword = textBoxPsw.Text;
                    context.SaveChanges();
                    labelInfo.Text = "client modifie ..!!";
                }
                else
                {
                    labelInfo.Text = "client n'exist pas ..!!";
                }
            }
        }
        private void btnSup_Click_1(object sender, EventArgs e)
        {
            int idcl = int.Parse(textBoxIdC.Text);
            using (hala_storeEntities context = new hala_storeEntities())
            {
                var cl = context.clients.FirstOrDefault(c => c.idC == idcl);
                if (cl != null)
                {
                    context.clients.Remove(cl);
                    context.SaveChanges();
                    labelInfo.Text = "Client supprimer...!!!";
                }
                else
                {
                    labelInfo.Text = "client n'excet pas ..!!";
                }
            }
        }

        private void btnAfficher_Click(object sender, EventArgs e)
        {
            getData();
        }

        private void bntAnn_Click(object sender, EventArgs e)
        {
            textBoxIdC.Text = "";
            textBoxCin.Text = "";
            textBoxPsw.Text = "";
            textBoxNom.Text = "";
            textBoxRecherch.Text = "";
            labelInfo.Text = "Annuler is Success...";
        }

        private void btnRech_Click(object sender, EventArgs e)
        {
            int idc = int.Parse(textBoxRecherch.Text);
            using(hala_storeEntities context = new hala_storeEntities()){
                var clients = from cl in context.clients
                              where cl.idC == idc
                              select new { id_c = cl.idC, nom = cl.NomC, cin = cl.cin, pasword = cl.pasword };
                dataGridView1.DataSource = clients.ToList();
            }
        }

        private void btnCmd_Click(object sender, EventArgs e)
        {
            Comand cm = new Comand();
            cm.Show();
            this.Hide();
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
