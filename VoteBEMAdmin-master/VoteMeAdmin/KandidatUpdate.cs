using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoteMeAdmin
{
    public partial class KandidatUpdate : Form
    {

        Koneksi konn = new Koneksi();
        public KandidatUpdate()
        {
            InitializeComponent();
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void KandidatUpdate_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'voteBEMDataSet.Kandidat' table. You can move, or remove it, as needed.
            this.kandidatTableAdapter.Fill(this.voteBEMDataSet.Kandidat);

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                txtFoto.Text = fileName;
            }
        }

        private void txtFoto_TextChanged(object sender, EventArgs e)
        {
            string path = txtFoto.Text;
            pictureBox1.Image = Image.FromFile(@path);
        }

        public void Updating()
        {
            try
            {
                FileStream fs = new FileStream(txtFoto.Text, FileMode.Open, FileAccess.Read);
                byte[] image = new byte[fs.Length];
                fs.Read(image, 0, Convert.ToInt32(fs.Length));

                SqlConnection conn = konn.getConnection();
                using (conn)
                {
                    conn.Open();
                    string sql = "UPDATE Kandidat SET NIM= @NIM, Visi= @Visi, Misi= @Misi, Foto= @Foto WHERE Nomor= @Nomor";

                    SqlCommand com = new SqlCommand(sql, conn);
                    using (com)
                    {
                        com.Parameters.AddWithValue("@Nomor", txtNomor.Text);
                        com.Parameters.AddWithValue("@NIM", txtNIM.Text);
                        com.Parameters.AddWithValue("@Visi", txtVisi.Text);
                        com.Parameters.AddWithValue("@Misi", txtMisi.Text);
                        com.Parameters.AddWithValue("@Foto", image);
                        com.ExecuteNonQuery();
                    }

                    conn.Close();

                    MessageBox.Show("Berhasil");
                }


            }
            catch (Exception e)
            {
                MessageBox.Show("gagal" + e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Updating();
        }

        public void Hapus()
        {
            try
            {
                SqlConnection conn = konn.getConnection();
                conn.Open();
                string sql = "DELETE FROM Kandidat WHERE Nomor='" + txtNomor.Text + "'";

                SqlCommand com = new SqlCommand(sql, conn);
                com.ExecuteNonQuery();
                MessageBox.Show("Data dihapus");
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Gagal menghapus data: \n" + e);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dia = MessageBox.Show("Apakah Anda yakin akan menghapus data ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dia == DialogResult.Yes)
                Hapus();
        }
    }
}
