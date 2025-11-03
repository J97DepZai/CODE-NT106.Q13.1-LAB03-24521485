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
    public partial class Bai01 : Form
    {
        public Bai01()
        {
            InitializeComponent();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            UDPServer f = new UDPServer();
            f.Show();
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
        }

        private void btn_client_Click_1(object sender, EventArgs e)
        {
            UDPClient f = new UDPClient();
            f.Show();
        }
    }
}
