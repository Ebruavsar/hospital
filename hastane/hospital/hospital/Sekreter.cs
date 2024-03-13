using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace hospital
{
    public partial class Sekreter : Form
    {
        public BusinessLayer.DoktorIslemleri DoktorIslemleri = new DoktorIslemleri();
        public BusinessLayer.SekreterIslemleri SekreterIslemleri = new SekreterIslemleri();
        public BusinessLayer.HastaIslemleri HastaIslemleri = new HastaIslemleri();
        public BusinessLayer.RandevuIslemleri RandevuIslemleri=new RandevuIslemleri();
        public string Giristen_Alinan_Sekreter_kimlik = "";
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();


        public Sekreter()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            //doktor işlemleri
            DataTable tablo = DoktorIslemleri.goruntule(comboBox2.Text, textBox7.Text);
            dataGridView1.DataSource = tablo; //presentation
            textBox5.Text = Giristen_Alinan_Sekreter_kimlik;

            //sekreter işlemleri
            DataTable ta = SekreterIslemleri.goruntule(comboBox3.Text, textBox13.Text);
            dataGridView2.DataSource = ta; //presentation
            //hasta işlemleri
            DataTable tab = HastaIslemleri.goruntule(comboBox4.Text, textBox20.Text);
            dataGridView3.DataSource = tab; //presentation

            //randevu işlemleri

            DataTable tabl = RandevuIslemleri.poliklinikDoktorGoruntule(comboBox5.Text);
            dataGridView4.DataSource = tabl;


            /*----------------------------------------------------------------------------*/
        }

        //doktor ekle,sil,güncelle-----------------------------------------------------------------------------------------------------
        public void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.ResetText();
            textBox4.Clear();
            //textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            comboBox2.ResetText();
        }
        private void ekle_Click(object sender, EventArgs e)
        {
            try
            {
                // TC kimlik numarasının geçerliliğini kontrol et
                if (!IsTCKimlikValid(textBox1.Text))
                {
                    throw new Exception("Geçersiz TC kimlik numarası");
                }

                // Telefon numarasının geçerliliğini kontrol et
                string formattedPhoneNumber = FormatPhoneNumber(textBox4.Text);
               

                DoktorIslemleri.ekle(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, formattedPhoneNumber, textBox5.Text, textBox6.Text);
                MessageBox.Show("Başarıyla Eklendi");
                temizle();
                listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            
        }

        private void listele_Click(object sender, EventArgs e)
        {
            DataTable tablo = DoktorIslemleri.goruntule(comboBox2.Text,textBox7.Text);
            dataGridView1.DataSource = tablo;
            
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = selectedRow.Cells[0].Value.ToString();
                textBox2.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[2].Value.ToString();
                comboBox1.Text = selectedRow.Cells[3].Value.ToString();
                textBox4.Text = selectedRow.Cells[4].Value.ToString();
                textBox6.Text = selectedRow.Cells[5].Value.ToString();

            }
        }
        private void sil_Click(object sender, EventArgs e)
        {
            try
            {
                DoktorIslemleri.sil(textBox1.Text);
                MessageBox.Show("Silme işlemi gerçekleşti");
                listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle();
        }
        private void guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                // TC kimlik numarasının geçerliliğini kontrol et
                if (!IsTCKimlikValid(textBox1.Text))
                {
                    throw new Exception("Geçersiz TC kimlik numarası");
                }

                // Telefon numarasının geçerliliğini kontrol et
                string formattedPhoneNumber = FormatPhoneNumber(textBox4.Text);

                DoktorIslemleri.guncelle(textBox1.Text, textBox2.Text, textBox3.Text, comboBox1.Text, formattedPhoneNumber, textBox5.Text, textBox6.Text);
                MessageBox.Show("Güncellendi");
                listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle();
        }
        //sekreter ekle,sil,güncelle---------------------------------------------------------------------------------------
        public void temizle2()
        {
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            comboBox3.ResetText();
            textBox13.Clear();
        }
        private void s_listele_Click(object sender, EventArgs e)
        {
            DataTable tablo = SekreterIslemleri.goruntule(comboBox3.Text, textBox13.Text);
            dataGridView2.DataSource = tablo;
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];


                textBox8.Text = selectedRow.Cells[0].Value.ToString();
                textBox9.Text = selectedRow.Cells[1].Value.ToString();
                textBox10.Text = selectedRow.Cells[2].Value.ToString();
                textBox11.Text = selectedRow.Cells[3].Value.ToString();
                textBox12.Text = selectedRow.Cells[4].Value.ToString();

            }
        }
        private void s_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTCKimlikValid(textBox8.Text))
                {
                    throw new Exception("Geçersiz TC kimlik numarası");
                }
                string formattedPhoneNumber = FormatPhoneNumber(textBox11.Text);
                SekreterIslemleri.ekle(textBox8.Text, textBox9.Text, textBox10.Text, formattedPhoneNumber, textBox12.Text);
                MessageBox.Show("Başarıyla Eklendi");
                s_listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle2();
        }
        private void s_sil_Click(object sender, EventArgs e)
        {
            try
            {
                SekreterIslemleri.sil(textBox8.Text);
                MessageBox.Show("Silme işlemi gerçekleşti");
                s_listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle2();
        }
        private void s_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTCKimlikValid(textBox8.Text))
                {
                    throw new Exception("Geçersiz TC kimlik numarası");
                }
                string formattedPhoneNumber = FormatPhoneNumber(textBox11.Text);
                SekreterIslemleri.guncelle(textBox8.Text, textBox9.Text, textBox10.Text, formattedPhoneNumber, textBox12.Text);
                MessageBox.Show("Güncellendi");
                s_listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle2();
        }
        //hasta ekle,sil,güncelle---------------------------------------------------------------------------------------------------------------------
        public void temizle3()
        {
            textBox14.Clear();
            textBox15.Clear();
            textBox16.Clear();
            textBox17.Clear();
            comboBox6.ResetText();
            textBox19.Clear();
            comboBox4.ResetText();
            textBox20.Clear();
        }
        private void h_listele_Click(object sender, EventArgs e)
        {
            DataTable tablo = HastaIslemleri.goruntule(comboBox4.Text, textBox20.Text);
            dataGridView3.DataSource = tablo;
            temizle3();
        }
        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView3.Rows[e.RowIndex];

                textBox14.Text = selectedRow.Cells[0].Value.ToString();
                textBox15.Text = selectedRow.Cells[1].Value.ToString();
                textBox16.Text = selectedRow.Cells[2].Value.ToString();
                textBox17.Text = selectedRow.Cells[3].Value.ToString();
                dateTimePicker1.Text = selectedRow.Cells[4].Value.ToString();
                comboBox6.Text = selectedRow.Cells[5].Value.ToString();
                textBox19.Text = selectedRow.Cells[6].Value.ToString();
            }
        }
        private void h_ekle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTCKimlikValid(textBox14.Text))
                {
                    throw new Exception("Geçersiz TC kimlik numarası");
                }
                string formattedPhoneNumber = FormatPhoneNumber(textBox17.Text);
                if (!IsEmailValid(textBox19.Text))
                {
                    throw new Exception("Geçersiz e-posta adresi");
                }

                HastaIslemleri.ekle(textBox14.Text, textBox15.Text, textBox16.Text, formattedPhoneNumber,  dateTimePicker1.Value, comboBox6.Text, textBox19.Text);
                MessageBox.Show("Başarıyla Eklendi");
                h_listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle3();
        }
        private void h_sil_Click(object sender, EventArgs e)
        {
            try
            {
                HastaIslemleri.sil(textBox14.Text);
                MessageBox.Show("Silme işlemi gerçekleşti");
                h_listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle3();
        }
        private void h_guncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsTCKimlikValid(textBox14.Text))
                {
                    throw new Exception("Geçersiz TC kimlik numarası");
                }
                string formattedPhoneNumber = FormatPhoneNumber(textBox17.Text);
                if (!IsEmailValid(textBox19.Text))
                {
                    throw new Exception("Geçersiz e-posta adresi");
                }
                HastaIslemleri.guncelle(textBox14.Text, textBox15.Text, textBox16.Text, formattedPhoneNumber, dateTimePicker1.Value, comboBox6.Text, textBox19.Text);
                MessageBox.Show("Güncellendi");
                h_listele_Click(sender, e);
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
            temizle3();
        }

        //randevu----------------------------------------------------------------------------------------

        private void olustur_Click(object sender, EventArgs e)
        {
            bool kayıt = RandevuIslemleri.RandevuPlanla(textBox18.Text, textBox21.Text, dateTimePicker2.Value, comboBox7.Text);
            if (kayıt)
            {
                MessageBox.Show("Randevu başarıyla oluşturuldu.");
            }
            else
            {
                MessageBox.Show("Seçilen saatte doktorun randevusu bulunmaktadır. Lütfen başka bir saat seçiniz.");
            }
            r_listele_Click(sender, e);
        }

        private void goruntule_Click(object sender, EventArgs e)
        {
            label28.Text = RandevuIslemleri.HastaGoruntuleAd(textBox21.Text);

            DataTable tabl = RandevuIslemleri.poliklinikDoktorGoruntule(comboBox5.Text);
            dataGridView4.DataSource = tabl;
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView4.Rows[e.RowIndex];

                textBox18.Text = selectedRow.Cells[0].Value.ToString();
            }
        }

        private void r_listele_Click(object sender, EventArgs e)
        {
            DataTable tabl = RandevuIslemleri.DoktorRandevuGoruntule(textBox18.Text, dateTimePicker2.Value);
            dataGridView5.DataSource = tabl;
        }

        //hata mesajları için------------------------------------------------------------------------------------------------------------
        private bool IsTCKimlikValid(string tcKimlik)
        {
            // TC kimlik numarası 11 haneli olmalıdır ve sadece rakamları içermelidir
            if (tcKimlik.Length != 11 || !tcKimlik.All(char.IsDigit))
            {
                return false;
            }

            return true;
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            // Telefon numarasından sadece rakamları al
            string digitsOnly = new string(phoneNumber.Where(char.IsDigit).ToArray());

            // Eğer telefon numarası 10 haneli değilse veya rakamları içermiyorsa, hata vermeden devam et
            if (digitsOnly.Length != 10)
            {
                // 10 haneden azsa, ekleme yapamayız, hatayı işlemi devam ettirmeden fırlat
                throw new ArgumentException("Telefon numarası 10 haneli olmalıdır.");
            }

            // Telefon numarasını istediğiniz formata dönüştür: (XXX) XXX-XXXX
            string formattedPhoneNumber = string.Format("({0}) {1}-{2}",
                digitsOnly.Substring(0, 3),
                digitsOnly.Substring(3, 3),
                digitsOnly.Substring(6));

            return formattedPhoneNumber;
        }



        private bool IsEmailValid(string email)
        {
            try
            {
                // E-posta adresi için bir regular expression (düzenli ifade) kullanarak doğrulama yapın
                string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
                // Örnek bir regular expression, basit bir doğrulamadır. Daha karmaşık bir doğrulama gerekebilir.
                // Bu örnekte, e-posta adresinin alfanumerik, @ işareti, nokta (.) ve tire (-) karakterlerini içermesi beklenir.
                // Daha karmaşık doğrulama senaryoları için regular expression'ları daha genişletmek gerekebilir.

                // Regular expression kullanarak e-posta adresini kontrol edin
                if (Regex.IsMatch(email, pattern))
                {
                    return true; // Eğer e-posta adresi geçerliyse true döndürün
                }
                else
                {
                    return false; // Geçerli değilse false döndürün
                }
            }
            catch (Exception)
            {
                return false; // Herhangi bir hata durumunda false döndürün
            }
        }
    }
}