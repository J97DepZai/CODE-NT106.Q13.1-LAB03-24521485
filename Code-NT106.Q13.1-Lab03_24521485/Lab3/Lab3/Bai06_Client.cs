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
    public partial class Bai06_Client : Form
    {
        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private Task receiveTask;
        private string myNickname = "";

        private const string IP_ADDRESS = "127.0.0.1";
        private const int PORT = 8080;
        public Bai06_Client()
        {
            InitializeComponent();
            this.FormClosing += Bai06_Client_FormClosing;
            this.tb_message.KeyDown += tb_message_KeyDown;
        }

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tb_name.Text))
            {
                MessageBox.Show("Vui long nhap ten!");
                return;
            }

            try
            {
                client = new TcpClient();
                await client.ConnectAsync(IP_ADDRESS, PORT);

                NetworkStream stream = client.GetStream();
                reader = new StreamReader(stream, Encoding.UTF8, true, 10 * 1024 * 1024);
                writer = new StreamWriter(stream) { AutoFlush = true };


                btn_connect.Enabled = false;
                tb_name.Enabled = false;
                btn_send.Enabled = true;
                btn_sendFile.Enabled = true;
                tb_message.Enabled = true;

                writer.WriteLine(tb_name.Text);

                receiveTask = Task.Run(() => ReceiveMessages());

                btn_connect.Enabled = false;
                tb_name.Enabled = false;
                tb_message.Enabled = true;
                btn_send.Enabled = true;
                UpdateLog("Da ket noi thanh cong.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi ket noi: " + ex.Message);
                UpdateLog("Khong the ket noi den server.");
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected && !string.IsNullOrEmpty(tb_message.Text))
            {
                try
                {
                    string message = tb_message.Text;

                    UpdateLog($"You: {message}");

                    writer.WriteLine(message);
                    tb_message.Clear();
                }
                catch (Exception ex)
                {
                    UpdateLog($"Khong gui duoc tin nhan: {ex.Message}");
                }
            }
        }

        private void ProcessServerMessage(string message)
        {
            if (message.StartsWith("FILE:"))
            {
                HandleIncomingFile(message);
                return; 
            }

            if (message.StartsWith("MSG:"))
            {
                string rawMessage = message.Substring(4);
               
                string myLeaveMessage = $"{myNickname} left the group";
                if (rawMessage.Contains(myLeaveMessage)) 
                {
                    UpdateLog("You left the group");
                    return;
                }

                UpdateLog(rawMessage);
            }

            else if (message.StartsWith("LIST:"))
            {
                UpdateParticipantList(message.Substring(5));
            }
            else if (message.StartsWith("ERROR:"))
            {
                MessageBox.Show(message.Substring(6), "Loi tu Server");
                ResetUIForDisconnect(true);
            }
        }
        private void HandleIncomingFile(string message)
        {
            try
            {
                var parts = message.Split(new char[] { ':' }, 4); 
                string sender = parts[1];
                string fileName = parts[2];
                string base64data = parts[3];

                byte[] fileBytes = Convert.FromBase64String(base64data);

                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ChatDownloads");
                Directory.CreateDirectory(downloadsPath);
                string uniqueFileName = $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}_{fileName}";
                string fullPath = Path.Combine(downloadsPath, uniqueFileName);

                File.WriteAllBytes(fullPath, fileBytes);

                UpdateLog($"{sender} đã gửi file/ảnh: {fileName}");
            }
            catch (Exception ex)
            {
                UpdateLog($"Lỗi khi nhận file: {ex.Message}");
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

        private void ReceiveMessages()
        {
            try
            {
                string message;
                while ((message = reader.ReadLine()) != null)
                {
                    this.Invoke(new Action(() => ProcessServerMessage(message)));
                }
            }
            catch
            {
                this.Invoke(new Action(() =>
                {
                    UpdateLog("you left the group.");
                    ResetUIForDisconnect(false);
                }));
            }
        }
        private void UpdateParticipantList(string userListCsv)
        {
            if (lv_participant.InvokeRequired)
            {
                lv_participant.Invoke(new Action<string>(UpdateParticipantList), userListCsv);
            }
            else
            {
                lv_participant.Items.Clear();
                string[] users = userListCsv.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string user in users)
                {
                    lv_participant.Items.Add(user);
                }
            }
        }
        private void ResetUIForDisconnect(bool isError)
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }
            btn_connect.Enabled = true;
            tb_name.Enabled = true;
            tb_message.Enabled = false;
            btn_send.Enabled = false;
            lv_participant.Items.Clear();
            btn_sendFile.Enabled = false;
            myNickname = "";
        }
        private void tb_message_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_send_Click(sender, e);
            }
        }

        private void Bai06_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null && client.Connected)
            {
                try
                {
                    writer.Close();
                    reader.Close();
                    client.Close();
                }
                catch { }
            }
        }

        private void btn_sendFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog1.FileName;
                    string fileName = Path.GetFileName(filePath); 

                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    string base64String = Convert.ToBase64String(fileBytes);

                    writer.WriteLine($"FILE:{fileName}:{base64String}");

                    UpdateLog($"You: đã gửi file/ảnh: {fileName}...");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi gửi file: {ex.Message}");
                }
            }
        }
    }
}
