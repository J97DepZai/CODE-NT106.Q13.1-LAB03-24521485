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
    public partial class UDPServer : Form
    {
        private UdpClient udpServer;
        private CancellationTokenSource cts;
        public UDPServer()
        {
            InitializeComponent();
        }

        private void UDPServer_Load(object sender, EventArgs e)
        {

        }

        private async Task StartListening(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    UdpReceiveResult result = await udpServer.ReceiveAsync();
                    if (token.IsCancellationRequested) break;

                    byte[] receivedData = result.Buffer;
                    IPEndPoint remoteEP = result.RemoteEndPoint;
                    string message = Encoding.UTF8.GetString(receivedData);
                    string displayMessage = remoteEP.Address.ToString() + ":" + remoteEP.Port + ": " + message;
                    lv_message.Items.Add(displayMessage);
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (SocketException)
                {
                    if (!token.IsCancellationRequested)
                        continue;
                    break;
                }
            }
        }

        private async void btn_listen_Click(object sender, EventArgs e)
        {
            try
            {
                int port = int.Parse(tb_port.Text);
                udpServer = new UdpClient(port);
                cts = new CancellationTokenSource();
                MessageBox.Show("Server đang lắng nghe trên cổng " + port, "Thông báo Server");
                btn_listen.Enabled = false;
                await StartListening(cts.Token);
            }
            catch (OperationCanceledException)
            {
                lv_message.Items.Add("Server đã dừng.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                if (udpServer != null) udpServer.Close();
                btn_listen.Enabled = true;
            }
        }

        private void UDPServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cts != null)
            {
                cts.Cancel(); 

                if (udpServer != null)
                    udpServer.Close(); 

                cts.Dispose();
            }
            else if (udpServer != null)
            {
                udpServer.Close(); 
            }
        }
    }
}
