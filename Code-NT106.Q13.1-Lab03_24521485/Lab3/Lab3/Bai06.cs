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
    public partial class Bai06 : Form
    {
        public Bai06()
        {
            InitializeComponent();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            Bai06_Server f = new Bai06_Server();
            f.Show();
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            Bai06_Client f = new Bai06_Client();
            f.Show();
        }
    }
}
