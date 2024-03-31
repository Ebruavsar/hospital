using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using BusinessLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ZedGraph;
using System.Runtime.InteropServices.ComTypes;


namespace hospital
{
    public partial class Doktor : Form
    {
        public BusinessLayer.DoktorRandevu doktorRandevu = new DoktorRandevu();
        public string Giristen_Alinan_Doktor_Kimlik = "";
        
        public Doktor()
        {
            InitializeComponent();
        }

        private void Doktor_Load(object sender, EventArgs e)
        {
            label13.Text = Giristen_Alinan_Doktor_Kimlik;
            DataTable dt = doktorRandevu.goruntule(dateTimePicker1.Value,label13.Text);
            dataGridView1.DataSource = dt;
            dateTimePicker1.Value = DateTime.Now;
            DataTable tablo = doktorRandevu.eski_gorusmeler(comboBox1.Text, textBox1.Text);
            dataGridView2.DataSource = tablo;

        }

        private void goruntule_Click(object sender, EventArgs e)
        {
            DataTable dt = doktorRandevu.goruntule(dateTimePicker1.Value, label13.Text);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                label7.Text = selectedRow.Cells[0].Value.ToString();
                label8.Text = selectedRow.Cells[4].Value.ToString();
                label9.Text = selectedRow.Cells[5].Value.ToString();
                label10.Text = selectedRow.Cells[6].Value.ToString();
                label11.Text = selectedRow.Cells[7].Value.ToString();
                label12.Text = selectedRow.Cells[3].Value.ToString();
            }
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            
            try
            {
                doktorRandevu.ekle(label13.Text,label7.Text,richTextBox1.Text,richTextBox2.Text);
                MessageBox.Show("Eklendi");
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void pdf_Click(object sender, EventArgs e)
        {
            try
            {
                // Font tanımlaması yapın
                BaseFont font = BaseFont.CreateFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font turkishFont = new iTextSharp.text.Font(font, 12, iTextSharp.text.Font.NORMAL);

                // Seçilen hastanın bilgilerini alın
                string hastaBilgileri = $"Ad: {label8.Text}\nSoyad: {label9.Text}\nDoğum Tarihi: {label10.Text}\nE-posta: {label11.Text}";

                // RichTextBox'lerdeki verileri birleştirin
                string birlesikMetin = $"Görüşler: {richTextBox1.Text}" + Environment.NewLine + $"Tahlil Sonuçları: {richTextBox2.Text}";

                // PDF tablosu oluşturun
                PdfPTable pdfTablosu = new PdfPTable(1); // Sadece bir sütun var
                pdfTablosu.DefaultCell.Padding = 3;
                pdfTablosu.WidthPercentage = 100;
                pdfTablosu.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTablosu.DefaultCell.BorderWidth = 1;

                // Hastanın bilgilerini PDF'e ekleyin
                PdfPCell hastaBilgisiHucresi = new PdfPCell(new Phrase("Hasta Bilgileri", turkishFont));
                pdfTablosu.AddCell(hastaBilgisiHucresi);
                PdfPCell hastaBilgisiIcerikHucresi = new PdfPCell(new Phrase(hastaBilgileri, turkishFont));
                pdfTablosu.AddCell(hastaBilgisiIcerikHucresi);

                // RichTextBox'lerdeki verileri PDF'e ekleyin
                PdfPCell birlesikMetinHucresi = new PdfPCell(new Phrase("Ek Bilgiler", turkishFont));
                pdfTablosu.AddCell(birlesikMetinHucresi);
                PdfPCell birlesikMetinIcerikHucresi = new PdfPCell(new Phrase(birlesikMetin, turkishFont));
                pdfTablosu.AddCell(birlesikMetinIcerikHucresi);

                // PDF dosyasını kaydedin
                SaveFileDialog pdfkaydetme = new SaveFileDialog();
                pdfkaydetme.Filter = "PDF Dosyaları|*.pdf";
                pdfkaydetme.Title = "PDF Olarak Kaydet";
                if (pdfkaydetme.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream stream = new FileStream(pdfkaydetme.FileName, FileMode.Create))
                    {
                        iTextSharp.text.Document pdfDosya = new iTextSharp.text.Document(PageSize.A4, 5f, 5f, 11f, 10f);
                        iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDosya, stream);
                        pdfDosya.Open();
                        pdfDosya.Add(pdfTablosu);
                        pdfDosya.Close();
                        stream.Close();
                        MessageBox.Show("PDF dosyası başarıyla oluşturuldu!\n" + "Dosya Konumu: " + pdfkaydetme.FileName, "İşlem Tamam");
                    }
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }

        }

        private void listele_Click(object sender, EventArgs e)
        {
            DataTable tablo = doktorRandevu.eski_gorusmeler(comboBox1.Text, textBox1.Text);
            dataGridView2.DataSource = tablo;
        }

        


    }
}
