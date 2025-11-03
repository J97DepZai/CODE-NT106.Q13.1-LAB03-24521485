using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Bai05 : Form
    {
        public Bai05()
        {
            InitializeComponent();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            Bai05_Server f = new Bai05_Server();
            f.Show();
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            Bai05_Client f = new Bai05_Client();
            f.Show();
        }
    }
}
