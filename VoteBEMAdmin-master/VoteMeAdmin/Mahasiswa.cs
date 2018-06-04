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
using System.Globalization;

namespace VoteMeAdmin
{
    public partial class Mahasiswa : Form
    {

        Koneksi konn = new Koneksi();
        public void Simpan()
        {
            dtpTanggal.CustomFormat = "yyyy-MM-dd";
            try
            {
                SqlConnection conn = konn.getConnection();
                conn.Open();
                string sql = "insert into Mahasiswa Values(@Nama, @Tempat, @Tgl, @Jur, @Pro, @Pass)";
                SqlCommand com = new SqlCommand(sql, conn);
                com.Parameters.AddWithValue("@Nama", txtNama.Text);
                com.Parameters.AddWithValue("@Tempat", txtTempatLahir.Text);
                com.Parameters.AddWithValue("@Tgl", dtpTanggal.Text);
                com.Parameters.AddWithValue("@Jur", cbJurusan.SelectedValue);
                com.Parameters.AddWithValue("@Pro", cbProdi.SelectedItem);
                com.Parameters.AddWithValue("@Pass", txtPassword.Text);

                com.ExecuteNonQuery();
                MessageBox.Show("Data berhasil dimasukkan");
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Gagal memasukkan data: /n " + e);
            } 
        }

        public void Autogen()
        {
            SqlConnection conn = konn.getConnection();
            int ID = 0;
            using (conn)
            {
                conn.Open();

                string cmd = "Select TOP 1 NIM From Mahasiswa order by NIM desc";
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
                        ID = 1810001;
                    }
                }

                    conn.Close();
            }
            txtNIM.Text = ID.ToString();
        }

        public void Hapus()
        {
            try
            {
                SqlConnection conn = konn.getConnection();
                conn.Open();
                string sql = "DELETE FROM Mahasiswa WHERE NIM='" + txtNIM.Text + "'";

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

        public void Updating()
        {
            dtpTanggal.CustomFormat = "yyyy-MM-dd";
            try
            {
                SqlConnection conn = konn.getConnection();
                conn.Open();
                string sql = "UPDATE Mahasiswa SET Nama = @Nama, Tempat_Lahir = @Tempat, Tanggal_Lahir = @Tgl, Jurusan = @Jur, Prodi = @Pro, Password = @Pass WHERE NIM = @NIM";
                SqlCommand com = new SqlCommand(sql, conn);
                com.Parameters.AddWithValue("@Nama", txtNama.Text);
                com.Parameters.AddWithValue("@Tempat", txtTempatLahir.Text);
                com.Parameters.AddWithValue("@Tgl", dtpTanggal.Text);
                com.Parameters.AddWithValue("@Jur", cbJurusan.SelectedValue);
                com.Parameters.AddWithValue("@Pro", cbProdi.SelectedItem);
                com.Parameters.AddWithValue("@Pass", txtPassword.Text);
                com.Parameters.AddWithValue("@NIM", txtNIM.Text);
                com.ExecuteNonQuery();
                MessageBox.Show("Data diperbarui!");
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Gagal memperbarui data: \n" + e);
            }
            
        }

        public Mahasiswa()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Simpan();
            this.mahasiswaTableAdapter.Fill(this.voteBEMDataSet.Mahasiswa);
            clear();
            Autogen();
        }

        private void Mahasiswa_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'voteBEMDataSet.Prodi' table. You can move, or remove it, as needed.
            this.prodiTableAdapter.Fill(this.voteBEMDataSet.Prodi);
            // TODO: This line of code loads data into the 'voteBEMDataSet.Jurusan' table. You can move, or remove it, as needed.
            this.jurusanTableAdapter.Fill(this.voteBEMDataSet.Jurusan);
            // TODO: This line of code loads data into the 'voteBEMDataSet.Mahasiswa' table. You can move, or remove it, as needed.
            this.mahasiswaTableAdapter.Fill(this.voteBEMDataSet.Mahasiswa);

            Autogen();
            getProdi();
            btnDel.Enabled = false;
            btnUpdt.Enabled = false;
            btnSave.Enabled = true;

        }

        private void getIDbyJurToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void cbJurusan_SelectedIndexChanged(object sender, EventArgs e)
        {
            getProdi();
        }

        public void getProdi()
        {
            cbProdi.Items.Clear();
            SqlConnection conn = konn.getConnection();
            using (conn)
            {
                conn.Open();
                string command = "Select ID_prodi from Prodi where ID_jur = '" + cbJurusan.SelectedValue + "'";
                SqlCommand scomm = new SqlCommand(command, conn);

                using (scomm)
                {
                    SqlDataReader sdr = scomm.ExecuteReader();
                    while (sdr.Read())
                    {
                        string prodi = sdr["ID_Prodi"].ToString();
                        cbProdi.Items.Add(prodi);
                    }
                }

                conn.Close();
            }
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.mahasiswaTableAdapter.FillBy(this.voteBEMDataSet.Mahasiswa, ((int)(System.Convert.ChangeType(nIMToolStripTextBox.Text, typeof(int)))));
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        public void clear()
        {
            txtNama.Clear();
            txtPassword.Clear();
            txtTempatLahir.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Autogen();
            clear();
            btnDel.Enabled = false;
            btnUpdt.Enabled = false;
            btnSave.Enabled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = false;
            btnUpdt.Enabled = true;
            btnDel.Enabled = true;
            foreach(DataGridViewRow row in dataGridView1.SelectedRows)
            {
                txtNIM.Text = row.Cells[0].Value.ToString();
                txtNama.Text = row.Cells[1].Value.ToString();
                txtTempatLahir.Text = row.Cells[2].Value.ToString();
                cbJurusan.SelectedValue = row.Cells[4].Value.ToString();
                cbProdi.SelectedItem = row.Cells[5].Value.ToString();
                string date = row.Cells[3].Value.ToString();
                DateTime DOB = Convert.ToDateTime(date);
                dtpTanggal.Value = DOB;
                txtPassword.Text = row.Cells[6].Value.ToString();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult dia = MessageBox.Show("Apakah Anda yakin akan menghapus data ini?", "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(dia == DialogResult.Yes)
            {
                Hapus();
                this.mahasiswaTableAdapter.Fill(this.voteBEMDataSet.Mahasiswa);
                clear();
                Autogen();

                btnDel.Enabled = false;
                btnUpdt.Enabled = false;
                btnSave.Enabled = true;
            }
                 
        }

        private void btnUpdt_Click(object sender, EventArgs e)
        {
            Updating();
            this.mahasiswaTableAdapter.Fill(this.voteBEMDataSet.Mahasiswa);
            clear();
            Autogen();

            btnDel.Enabled = false;
            btnUpdt.Enabled = false;
            btnSave.Enabled = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
