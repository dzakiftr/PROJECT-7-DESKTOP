using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoteMeAdmin
{
    public partial class KandidatMenu : Form
    {
        public KandidatMenu()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void KandidatMenu_Load(object sender, EventArgs e)
        {
            mainPanel2.Controls.Clear();
            Kandidat kd = new Kandidat();
            kd.TopLevel = false;
            kd.AutoScroll = true;
            mainPanel2.Controls.Add(kd);
            kd.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainPanel2.Controls.Clear();
            Kandidat kd = new Kandidat();
            kd.TopLevel = false;
            kd.AutoScroll = true;
            mainPanel2.Controls.Add(kd);
            kd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainPanel2.Controls.Clear();
            KandidatUpdate kd = new KandidatUpdate();
            kd.TopLevel = false;
            kd.AutoScroll = true;
            mainPanel2.Controls.Add(kd);
            kd.Show();
        }
    }
}
