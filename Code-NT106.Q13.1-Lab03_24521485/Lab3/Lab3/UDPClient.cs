using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class UDPClient : Form
    {

        public UDPClient()
        {
            InitializeComponent();
        }

        private void UDPClient_Load(object sender, EventArgs e)
        {

        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            UdpClient udpClient = new UdpClient();
            try
            {
                string remoteIP = tb_remote.Text;
                int remotePort = int.Parse(tb_port.Text);
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);

                string message = rtb_message.Text;
                byte[] data = Encoding.UTF8.GetBytes(message);

                udpClient.Send(data, data.Length, serverEndPoint);
                rtb_message.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                udpClient.Close();
            }
        }
    }
}
