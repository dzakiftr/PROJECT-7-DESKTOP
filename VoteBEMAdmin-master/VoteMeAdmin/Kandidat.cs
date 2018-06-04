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
using System.IO;
using System.Drawing.Imaging;

namespace VoteMeAdmin
{
    public partial class Kandidat : Form
    {
        string targetGambar;
        Koneksi konn = new Koneksi();

        public void Simpan()
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
                    string sql = "insert into Kandidat Values(@Nomor, @NIM, @Visi, @Misi, @Foto)";
                    SqlCommand com = new SqlCommand(sql, conn);
                    using (com)
                    {
                        com.Parameters.AddWithValue("@Nomor", txtNomor.Text);
                        com.Parameters.AddWithValue("@NIM", txtNIM.Text);
                        com.Parameters.AddWithValue("@Visi", txtVisi.Text);
                        com.Parameters.AddWithValue("@Misi", txtMisi.Text);
                        com.Parameters.AddWithValue("@Foto", image);
                        //SqlParameter foto = new SqlParameter("@Foto", SqlDbType.VarBinary, image.Length, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, image);
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


        public void Hapus()
        {
            try
            {
                SqlConnection conn = konn.getConnection();
                conn.Open();
                string sql = "DELETE FROM Kandidat WHERE Nomor='" + txtNomor.Text + "'";

                SqlCommand com = new SqlCommand(sql, conn);
                com.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Berhasil");
            }
            catch (Exception e)
            {
                MessageBox.Show("gagal" + e);
            }
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
                    using(com)
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

        public Kandidat()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var fileName = openFileDialog1.FileName;
                txtFoto.Text = fileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool val = checkExist(txtNIM.Text);

            if (val != false)
            {
                Simpan();
                Autogen();
            }
            else
                MessageBox.Show("Kandidat dengan NIM: {0}, Telah ada!", txtNIM.Text);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        public void Autogen()
        {
            SqlConnection conn = konn.getConnection();
            int ID = 0;
            using (conn)
            {
                conn.Open();

                string cmd = "Select TOP 1 Nomor From Kandidat order by Nomor desc";
                SqlCommand sqcomm = new SqlCommand(cmd, conn);

                using (sqcomm)
                {
                    SqlDataReader sda = sqcomm.ExecuteReader();
                    sda.Read();

                    if (sda.HasRows)
                    {
                        int cur = sda.GetInt32(0);
                        ID = cur + 1;
                    }
                    else
                    {
                        ID = 1;
                    }
                }

                conn.Close();
            }
            txtNomor.Text = ID.ToString();
        }

        public bool checkExist(string NIM)
        {
            SqlConnection conn = konn.getConnection();
            using (conn)
            {
                conn.Open();

                string cmd = "Select NIM From Kandidat where NIM = "+NIM;
                SqlCommand sqcomm = new SqlCommand(cmd, conn);
                using (sqcomm)
                {
                    SqlDataReader sda = sqcomm.ExecuteReader();
                    sda.Read();
                    if (sda.HasRows)
                        return false;
                    else
                        return true;
                }
                conn.Close();
            }
        }

        private void txtFoto_TextChanged(object sender, EventArgs e)
        {
            string path = txtFoto.Text;
            pictureBox1.Image = Image.FromFile(@path);
        }

        private void Kandidat_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'voteBEMDataSet1.Mahasiswa' table. You can move, or remove it, as needed.
            this.mahasiswaTableAdapter.Fill(this.voteBEMDataSet1.Mahasiswa);

            Autogen();
        }

        private void mahasiswaBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void searchByNameToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.mahasiswaTableAdapter.SearchByName(this.voteBEMDataSet1.Mahasiswa, nameToolStripTextBox.Text);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.mahasiswaTableAdapter.Fill(this.voteBEMDataSet1.Mahasiswa);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                txtNIM.Text = row.Cells[0].Value.ToString();
            }
        }
    }
}
