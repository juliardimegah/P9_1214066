using MySql.Data.MySqlClient;
using P9_1214066.controller;
using P9_1214066.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace P9_1214066.view
{
    public partial class nilai : Form
    {
        koneksi koneksi = new koneksi();
        M_Nilai m_nli = new M_Nilai();
        string id;
       
        public void GetDataMhs()
        {
            koneksi.OpenConnection();
            MySqlDataReader reader = koneksi.reader("SELECT * FROM t_mahasiswa");
            while (reader.Read())
            {
                cbNPM.Items.Add(reader.GetString("npm"));
            }
            reader.Close();
            koneksi.CloseConnection();
        }

        public void GetNamaMahasiswa()
        {
            koneksi.OpenConnection();
            MySqlDataReader reader = koneksi.reader("SELECT nama FROM t_mahasiswa WHERE npm = '" + cbNPM.Text + "'");
            while (reader.Read())
            {
                tbNama.Text = reader.GetString(0);
            }
            reader.Close();
            koneksi.CloseConnection();
        }
        public void Tampil()
        {
            //Query DB
            datamahasiswa.DataSource = koneksi.ShowData("SELECT id_nilai, matkul, kategori, t_nilai.npm, nama, nilai FROM t_nilai JOIN t_mahasiswa ON t_mahasiswa.npm = t_nilai.npm ");

            datamahasiswa.Columns[0].HeaderText = "ID";
            datamahasiswa.Columns[1].HeaderText = "Matkul";
            datamahasiswa.Columns[2].HeaderText = "Kategori";
            datamahasiswa.Columns[3].HeaderText = "NPM";
            datamahasiswa.Columns[4].HeaderText = "Nama";
            datamahasiswa.Columns[5].HeaderText = "Nilai";
        }

        public nilai()
        {
            
            InitializeComponent();
        }



        private void nilai_Load(object sender, EventArgs e)
        {
            Tampil();
            GetDataMhs();
        }

        private void datamahasiswa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cbMatkul.SelectedIndex = -1;
            cbKategori.SelectedIndex = -1;
            cbNPM.SelectedIndex = -1;
            tbNilai.Text = "";
            tbNama.Text = "";

            Tampil();
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {

            if (cbMatkul.SelectedIndex == -1 || cbKategori.SelectedIndex == -1 || cbNPM.SelectedIndex == -1 || tbNilai.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Nilai nli = new Nilai();
                m_nli.Matkul = cbMatkul.Text;
                m_nli.Kategori = cbKategori.Text;
                m_nli.Npm = cbNPM.Text;
                m_nli.Nilai = tbNilai.Text;

                nli.Insert(m_nli);

                cbKategori.SelectedIndex = -1;
                cbMatkul.SelectedIndex = -1;
                cbNPM.SelectedIndex = -1;
                tbNilai.Text = "";

                Tampil();
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (cbMatkul.SelectedIndex == -1 || cbKategori.SelectedIndex == -1 || cbNPM.SelectedIndex == -1 || tbNilai.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Nilai nli = new Nilai();
                m_nli.Matkul = cbMatkul.Text;
                m_nli.Kategori = cbKategori.Text;
                m_nli.Npm = cbNPM.Text;
                m_nli.Nilai = tbNilai.Text;

                nli.Update(m_nli, id);
                cbKategori.SelectedIndex = -1;
                cbMatkul.SelectedIndex = -1;
                cbNPM.SelectedIndex = -1;
                tbNilai.Text = "";

                Tampil();
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            DialogResult pesan = MessageBox.Show("Apakah yakin akan menghapus data ini?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pesan == DialogResult.Yes)
            {
                Nilai nli = new Nilai();
                nli.Delete(id);
                Tampil();

                cbKategori.SelectedIndex = -1;
                cbMatkul.SelectedIndex = -1;
                cbNPM.SelectedIndex = -1;
                tbNilai.Text = "";
            }
        }

        private void tbCariData_TextChanged(object sender, EventArgs e)
        {
                datamahasiswa.DataSource = koneksi.ShowData("SELECT id_nilai, matkul, kategori, t_nilai.npm, nama, nilai, FROM t_nilai JOIN t_mahasiswa on t_mahasiswa.npm = t_nilai.npm" +
                    "WHERE t_nilai.npm LIKE '%' '" + tbCariData.Text + "' '%' OR matkul LIKE '%' '" + tbCariData.Text + "' '%'");
        }

        private void datamahasiswa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = datamahasiswa.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbMatkul.Text = datamahasiswa.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbKategori.Text = datamahasiswa.Rows[e.RowIndex].Cells[2].Value.ToString();
            cbNPM.Text = datamahasiswa.Rows[e.RowIndex].Cells[3].Value.ToString();
            tbNilai.Text = datamahasiswa.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void cbNPM_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetNamaMahasiswa();
        }
    }
}