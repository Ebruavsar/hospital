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
            label17.Text = Giristen_Alinan_Doktor_Kimlik;
            label13.Text = DateTime.Now.ToShortDateString();
            DataTable dt = doktorRandevu.goruntule(dateTimePicker1.Value,label17.Text);
            dataGridView1.DataSource = dt;
        }

        private void goruntule_Click(object sender, EventArgs e)
        {
            DataTable dt = doktorRandevu.goruntule(dateTimePicker1.Value, label17.Text);
            dataGridView1.DataSource = dt;
        }
    }
}
