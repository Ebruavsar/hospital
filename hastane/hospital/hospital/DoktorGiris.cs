using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace hospital
{
    public partial class DoktorGiris : Form
    {
        public DoktorGiris()
        {
            InitializeComponent();
        }
        public BusinessLayer.Giris doktor = new BusinessLayer.Giris();
        private void Giris_Click(object sender, EventArgs e)
        {
            string kimlik = textBox1.Text;
            string sifre = textBox2.Text;
            bool loggedIn = doktor.DoktorGiris(kimlik, sifre);
            if (loggedIn)
            {
                // Giriş başarılı ise yapılacak işlemler
                this.Hide();
                Doktor doktor = new Doktor();
                {
                    doktor.StartPosition = FormStartPosition.CenterParent;
                    doktor.Giristen_Alinan_Doktor_Kimlik = kimlik;
                    doktor.Show();
                }
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
