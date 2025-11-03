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
    public partial class Bai02 : Form
    {

        private Thread listenThread;
        private Socket listenerSocket;

        public Bai02()
        {
            InitializeComponent();
        }

        private void btn_listen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            listenThread = new Thread(new ThreadStart(StartUnsafeThread));
            listenThread.Start();
            btn_listen.Enabled = false;
        }

        private void StartUnsafeThread()
        {
            int bytesReceived = 0;

            byte[] recv = new byte[1]; 
            Socket clientSocket;
            listenerSocket = new Socket(
                AddressFamily.InterNetwork, 
                SocketType.Stream,          
                ProtocolType.Tcp           
            );

            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Any, 8080);
            try
            {
                listenerSocket.Bind(ipepServer);
                listenerSocket.Listen(10);
                rtb_message.AppendText("Telnet running on 127.0.0.1 8080\n");
                while (true)
                {
                    clientSocket = listenerSocket.Accept(); 
                    rtb_message.AppendText("Client connected from: " + clientSocket.RemoteEndPoint + "\n");
                    while ((bytesReceived = clientSocket.Receive(recv)) > 0)
                    {
                        string message = Encoding.UTF8.GetString(recv, 0, bytesReceived);
                        rtb_message.AppendText(message);
                        rtb_message.ScrollToCaret();
                    }

                    clientSocket.Close();
                    rtb_message.AppendText("Client disconnected.\n");
                }
            }
            catch (SocketException)
            {
                
            }
        }

        private void rtb_message_TextChanged(object sender, EventArgs e)
        {

        }

        private void Bai02_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (listenerSocket != null)
                {
                    listenerSocket.Close();
                }
                if (listenThread != null && listenThread.IsAlive)
                {
                    listenThread.Join(500);
                }
            }
            catch { }
        }
    }
}
