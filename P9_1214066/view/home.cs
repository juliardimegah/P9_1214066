using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P9_1214066.view
{
    public partial class home : Form
    {
        public home()
        {
            InitializeComponent();
        }

        private void dataMahasiswaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
