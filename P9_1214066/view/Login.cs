using MySql.Data.MySqlClient;
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



namespace P9_1214066.view
{
    public partial class Login : Form
    {
        CekLogin login = new CekLogin();
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;

        public Login()
        {
            server = "localhost";
            database = "ulbi";
            uid = "root";
            password = "";

            string connString;
            connString = "Server=localhost;Database=ulbi;Uid=root;Pwd=;";

            conn = new MySqlConnection(connString);

            InitializeComponent();
        }

        public bool IsLogin(string user, string pass)
        {
            string query = $"SELECT * FROM datalogin WHERE username = '{user}' AND password = '{pass}';";

            try
            {
                if (OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        reader.Close();
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        reader.Close();
                        conn.Close();
                        return false;
                    }
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }

        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Connection to server failed");
                        break;
                    case 1045:
                        MessageBox.Show("Server username or password is incorrect");
                        break;
                }
                return false;
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbNama.Text == "" || tbPassword.Text == "")
            {
                MessageBox.Show("Username dan Password tidak boleh kosong !!!!");
            }
            else
            {
                string username = tbNama.Text;
                string password = tbPassword.Text;

                bool status = login.cek_login(username, password);
                if (status)
                {
                    MessageBox.Show("Login Berhasil", "Berhasil");
                    home pform = new home();
                    pform.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Gagal Login", "Gagal");
                }
            }
            // string user = tbNama.Text;
            //string pass = tbPassword.Text;

            //if (IsLogin(user, pass))
            //{
            //  this.Hide();
            //home home = new home();
            //home.ShowDialog();

            //}
            //else if (user == "" && pass == "")
            //{
            // MessageBox.Show("Username dan Password tidak boleh kosong!!");
            //}
            //else if (user == "")
            //{
            //MessageBox.Show("Username tidak boleh kosong!!");
            //}
            //    else if (pass == "")
            //  {
            //        MessageBox.Show("Password tidak boleh kosong!!");
            //}
            //      else
            //    {
            //MessageBox.Show("GAGAL LOGIN!!", "Gagal", MessageBoxButtons.OK);
            //}
            //}
        }
    }
}
    