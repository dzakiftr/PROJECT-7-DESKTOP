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
    public partial class Dashboard : Form
    {

        // Define the CS_DROPSHADOW constant
        private const int CS_DROPSHADOW = 0x00020000;

        // Override the CreateParams property
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            Mahasiswa mmenu = new Mahasiswa();
            mmenu.TopLevel = false;
            mmenu.AutoScroll = true;
            mainPanel.Controls.Add(mmenu);
            mmenu.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mainPanel.Controls.Clear();
            KandidatMenu kd = new KandidatMenu();
            kd.TopLevel = false;
            kd.AutoScroll = true;
            mainPanel.Controls.Add(kd);
            kd.Show();
        }
    }
}
