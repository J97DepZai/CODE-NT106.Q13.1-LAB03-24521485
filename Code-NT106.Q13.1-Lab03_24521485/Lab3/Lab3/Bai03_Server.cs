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
    public partial class Bai03_Server : Form
    {
        private TcpListener tcpServer;
        private Thread listenThread;
        public Bai03_Server()
        {
            InitializeComponent();
        }

        private void UpdateListView(string message)
        {
            if (lv_message.InvokeRequired)
            {
                lv_message.Invoke(new MethodInvoker(delegate
                {
                    lv_message.Items.Add(message);
                }));
            }
            else
            {
                lv_message.Items.Add(message);
            }
        }

        private void ListenThread()
        {
            try
            {
                tcpServer = new TcpListener(IPAddress.Any, 8080);
                tcpServer.Start();
                UpdateListView("Server started!");
                while (true)
                {
                    TcpClient client = tcpServer.AcceptTcpClient();
                    UpdateListView("Connection accepted from " + client.Client.RemoteEndPoint);

                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        UpdateListView("From client: " + message);
                    }

                    stream.Close();
                    client.Close();
                    UpdateListView("Client disconnected."); 
                }
            }
            catch (SocketException)
            {
            }
        }

        private void btn_listen_Click(object sender, EventArgs e)
        {
            listenThread = new Thread(new ThreadStart(ListenThread));
            listenThread.Start();
            btn_listen.Enabled = false;
        }

        private void Bai03_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (tcpServer != null)
                    tcpServer.Stop();
                if (listenThread != null && listenThread.IsAlive)
                    listenThread.Join(500);
            }
            catch { }
        }
    }
}
