using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SekreterGiris form1 = new SekreterGiris();
            {
                form1.StartPosition = FormStartPosition.CenterScreen;
                form1.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            DoktorGiris form1 = new DoktorGiris();
            {
                form1.StartPosition = FormStartPosition.CenterScreen;
                form1.Show();
            }
        }
    }
}
