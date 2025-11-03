using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Bai03_Client : Form
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        public Bai03_Client()
        {
            InitializeComponent();
            btn_send.Enabled = false;
            btn_disconnect.Enabled = false;
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect("127.0.0.1", 8080);
                stream = tcpClient.GetStream();
                btn_connect.Enabled = false;
                btn_send.Enabled = true;
                btn_disconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to server: " + ex.Message);
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (stream == null) return;

            try
            {
                string message = rtb_message.Text;
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
                rtb_message.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending data: " + ex.Message);
            }
        }

        private void btn_disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (stream != null)
                    stream.Close();
                if (tcpClient != null)
                    tcpClient.Close();
                btn_connect.Enabled = true;
                btn_send.Enabled = false;
                btn_disconnect.Enabled = false;
                rtb_message.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error disconnecting: " + ex.Message);
            }
        }

        private void Bai03_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (stream != null)
                stream.Close();
            if (tcpClient != null)
                tcpClient.Close();
        }
    }
}
