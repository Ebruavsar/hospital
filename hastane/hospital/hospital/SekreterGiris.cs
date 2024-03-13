using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;


namespace hospital
{
    public partial class SekreterGiris : Form
    {
        public SekreterGiris()
        {
            InitializeComponent();
        }
        public BusinessLayer.Giris sekreter= new BusinessLayer.Giris();
        private void Giris_Click(object sender, EventArgs e)
        {
            string kimlik = textBox1.Text;
            string sifre = textBox2.Text;
            bool loggedIn = sekreter.SekreterGiris(kimlik, sifre);
            if (loggedIn)
            {
                // Giriş başarılı ise yapılacak işlemler
                this.Hide();

                Sekreter sekreter = new Sekreter();
                sekreter.Giristen_Alinan_Sekreter_kimlik = kimlik;
                sekreter.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış!");
            }

        }

        private void Geri_Click(object sender, EventArgs e)
        {
            this.Hide();
            Giris giris = new Giris();
            {
                giris.StartPosition = FormStartPosition.CenterScreen;
                giris.Show();
            }
        }
    }
}
