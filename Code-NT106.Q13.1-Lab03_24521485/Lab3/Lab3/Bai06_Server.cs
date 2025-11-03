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
    public partial class Bai06_Server : Form
    {
        private TcpListener serverListener;
        private static Dictionary<string, TcpClient> clientList = new Dictionary<string, TcpClient>();
        private static readonly object lockObj = new object();

        private const string IP_ADDRESS = "127.0.0.1";
        private const int PORT = 8080;


        public Bai06_Server()
        {
            InitializeComponent();
            this.FormClosing += Bai06_Server_FormClosing;
        }

        private async void btn_listen_Click(object sender, EventArgs e)
        {
            if (serverListener != null && serverListener.Server.IsBound)
            {
                UpdateLog("Server da bat dau lang nghe roi.");
                return;
            }

            try
            {
                IPAddress ip = IPAddress.Parse(IP_ADDRESS);
                serverListener = new TcpListener(ip, PORT);
                serverListener.Start();

                UpdateLog($"Server da khoi dong tai {IP_ADDRESS}:{PORT}");
                btn_listen.Enabled = false;
                await Task.Run(() => ListenForClients());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Loi khoi dong server: {ex.Message}", "Loi");
                UpdateLog($"Loi khoi dong server: {ex.Message}");
                btn_listen.Enabled = true;
            }
        }

        private void ListenForClients()
        {
            while (true)
            {
                try
                {
                    TcpClient client = serverListener.AcceptTcpClient();

                    string clientEndPoint = client.Client.RemoteEndPoint.ToString();
                    UpdateLog($"New client connected from {clientEndPoint}");

                    Task.Run(() => HandleClient(client));
                }
                catch
                {
                    UpdateLog("Server da ngung lang nghe.");
                    break;
                }
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            StreamReader reader = new StreamReader(stream, Encoding.UTF8, true, 10 * 1024 * 1024);

            StreamWriter writer = new StreamWriter(stream) { AutoFlush = true };
            string nickname = null;

            try
            {
                nickname = reader.ReadLine();

                lock (lockObj)
                {
                    if (string.IsNullOrEmpty(nickname) || clientList.ContainsKey(nickname))
                    {
                        writer.WriteLine("ERROR:Ten da ton tai hoac khong hop le.");
                        client.Close();
                        return;
                    }

                    clientList.Add(nickname, client);
                }

                BroadcastMessage($"I'm {nickname}, nice too meet you ", excludedUser: null);
                BroadcastUserList();

                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    if (message.StartsWith("FILE:"))
                    {
                        UpdateLog($"{nickname} đang gửi một file/ảnh...");

                        var parts = message.Split(new char[] { ':' }, 3);
                        string fileName = parts[1];
                        string base64data = parts[2];

                        BroadcastMessage($"FILE:{nickname}:{fileName}:{base64data}", excludedUser: nickname, isRaw: true);
                    }
                    else if (message.StartsWith("@"))
                    {
                        ProcessPrivateMessage(nickname, message);
                    }

                    else
                    {
                        string logMessage = $"{nickname}: {message}";
                        UpdateLog(logMessage);

                        string clientMessage = $"{nickname}: {message}";
                        BroadcastMessage(clientMessage, excludedUser: nickname);
                    }
                }
            }
            catch { }
            finally
            {
                if (nickname != null)
                {
                    lock (lockObj)
                    {
                        clientList.Remove(nickname);
                    }

                    string leftMessage_Log = $"{nickname} left the group";
                    UpdateLog(leftMessage_Log);

                    string leftMessage_Client = $"{nickname} left the group";
                    BroadcastMessage(leftMessage_Client, excludedUser: null);

                    BroadcastUserList();
                }
                client.Close();
            }
        }


        private void BroadcastMessage(string message, string excludedUser = null, bool isRaw = false)
        {

            lock (lockObj)
            {
                foreach (var entry in clientList)
                {
                    string user = entry.Key;
                    TcpClient client = entry.Value;

                    if (excludedUser == null || user != excludedUser)
                    {
                        try
                        {
                            StreamWriter writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

                            string finalMessage = isRaw ? message : $"MSG:{message}";

                            writer.WriteLine(finalMessage);
                        }
                        catch { }
                    }
                }
            }
        }

        private void BroadcastUserList()
        {
            string userList;
            lock (lockObj)
            {
                userList = string.Join(",", clientList.Keys);
            }

            lock (lockObj)
            {
                foreach (TcpClient c in clientList.Values)
                {
                    try
                    {
                        StreamWriter writer = new StreamWriter(c.GetStream()) { AutoFlush = true };
                        writer.WriteLine($"LIST:{userList}");
                    }
                    catch { }
                }
            }
        }
        private void ProcessPrivateMessage(string sender, string message)
        {
            TcpClient targetClient = null;
            TcpClient senderClient = null;

            try
            {
                int colonIndex = message.IndexOf(':');

                if (colonIndex <= 1)
                {
                    throw new Exception("Cú pháp không hợp lệ.");
                }

                string targetUser = message.Substring(1, colonIndex - 1).Trim();

                string privateMsg = message.Substring(colonIndex + 1).Trim();

                lock (lockObj)
                {
                    clientList.TryGetValue(targetUser, out targetClient);
                    clientList.TryGetValue(sender, out senderClient);
                }

                if (targetClient != null && senderClient != null)
                {
                    StreamWriter targetWriter = new StreamWriter(targetClient.GetStream()) { AutoFlush = true };
                    targetWriter.WriteLine($"MSG:(Rieng tu {sender}): {privateMsg}");

                    StreamWriter senderWriter = new StreamWriter(senderClient.GetStream()) { AutoFlush = true };
                    senderWriter.WriteLine($"MSG:(Ban -> {targetUser}): {privateMsg}");
                }
                else if (senderClient != null)
                {
                    StreamWriter senderWriter = new StreamWriter(senderClient.GetStream()) { AutoFlush = true };
                    senderWriter.WriteLine($"MSG: Khong tim thay nguoi dung '{targetUser}'.");
                }
            }
            catch
            {
                lock (lockObj)
                {
                    clientList.TryGetValue(sender, out senderClient);
                }

                if (senderClient != null)
                {
                    StreamWriter senderWriter = new StreamWriter(senderClient.GetStream()) { AutoFlush = true };
                    senderWriter.WriteLine("MSG: Cu phap tin nhan rieng khong dung. (Dung: @Ten: Noi dung)");
                }
            }
        }

        private void UpdateLog(string text)
        {
            if (lv_message.InvokeRequired)
            {
                lv_message.Invoke(new Action<string>(UpdateLog), text);
            }
            else
            {
                lv_message.Items.Add(text);
                if (lv_message.Items.Count > 0)
                {
                    lv_message.EnsureVisible(lv_message.Items.Count - 1);
                }
            }
        }
        private void Bai06_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serverListener != null)
            {
                serverListener.Stop();
                lock (lockObj)
                {
                    foreach (var clientPair in clientList)
                    {
                        clientPair.Value.Close();
                    }
                }
            }
        }

        private void Bai06_Server_Load(object sender, EventArgs e)
        {

        }
    }
}
