using P9_1214066.controller;
using P9_1214066.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P9_1214066
{
    public partial class Form1 : Form
    {
        koneksi koneksi = new koneksi();
        M_mahasiswa m_mhs = new M_mahasiswa();
        string id;

        public void Tampil()
        {
            //Query DB
            datamahasiswa.DataSource = koneksi.ShowData("SELECT * FROM t_mahasiswa");

            //mengubah nama kolom tabel
            datamahasiswa.Columns[0].HeaderText = "NPM";
            datamahasiswa.Columns[1].HeaderText = "Nama";
            datamahasiswa.Columns[2].HeaderText = "Angkatan";
            datamahasiswa.Columns[3].HeaderText = "Alamat";
            datamahasiswa.Columns[4].HeaderText = "Email";
            datamahasiswa.Columns[5].HeaderText = "No HP";
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tampil();
        }

        private void datamahasiswa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (npm.Text == "" || nama.Text == "" || angkatan.SelectedIndex == -1 || alamat.Text == "" || email.Text == "" || nohp.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Mahasiswa mhs = new Mahasiswa();
                m_mhs.Npm = npm.Text;
                m_mhs.Nama = nama.Text;
                m_mhs.Angkatan = angkatan.Text;
                m_mhs.Alamat = alamat.Text;
                m_mhs.Email = email.Text;
                m_mhs.Nohp = nohp.Text;

                mhs.Insert(m_mhs);

                npm.Text = "";
                nama.Text = "";
                angkatan.SelectedIndex = -1;
                alamat.Text = "";
                email.Text = "";
                nohp.Text = "";

                Tampil();

            }
        }

        private void datamahasiswa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = datamahasiswa.Rows[e.RowIndex].Cells[0].Value.ToString();
            npm.Text = datamahasiswa.Rows[e.RowIndex].Cells[0].Value.ToString();
            nama.Text = datamahasiswa.Rows[e.RowIndex].Cells[1].Value.ToString();
            angkatan.Text = datamahasiswa.Rows[e.RowIndex].Cells[2].Value.ToString();
            alamat.Text = datamahasiswa.Rows[e.RowIndex].Cells[3].Value.ToString();
            email.Text = datamahasiswa.Rows[e.RowIndex].Cells[4].Value.ToString();
            nohp.Text = datamahasiswa.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (npm.Text == "" || nama.Text == "" || angkatan.SelectedIndex == -1 || alamat.Text == "" || email.Text == "" || nohp.Text == "")
            {
                MessageBox.Show("Data tidak boleh kosong", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Mahasiswa mhs = new Mahasiswa();
                m_mhs.Npm = npm.Text;
                m_mhs.Nama = nama.Text;
                m_mhs.Angkatan = angkatan.Text;
                m_mhs.Alamat = alamat.Text;
                m_mhs.Email = email.Text;
                m_mhs.Nohp = nohp.Text;

                mhs.Update(m_mhs, id);

                npm.Text = "";
                nama.Text = "";
                angkatan.SelectedIndex = -1;
                alamat.Text = "";
                email.Text = "";
                nohp.Text = "";

                Tampil();

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Tampil();

            npm.Text = "";
            nama.Text = "";
            angkatan.SelectedIndex = -1;
            alamat.Text = "";
            email.Text = "";
            nohp.Text = "";
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            DialogResult pesan = MessageBox.Show("Apakah yakin akan menghapus data ini?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (pesan == DialogResult.Yes)
            {
                Mahasiswa mhs = new Mahasiswa();
                mhs.Delete(id);
                Tampil();

                npm.Text = "";
                nama.Text = "";
                angkatan.SelectedIndex = -1;
                alamat.Text = "";
                email.Text = "";
                nohp.Text = "";
            }
        }

        private void tbCariData_TextChanged(object sender, EventArgs e)
        {
            datamahasiswa.DataSource = koneksi.ShowData("SELECT * FROM t_mahasiswa WHERE npm LIKE '%' '" + tbCariData.Text + "' '%' OR nama LIKE '%' '"+ tbCariData.Text + "' '%'");
        }
    }
}
