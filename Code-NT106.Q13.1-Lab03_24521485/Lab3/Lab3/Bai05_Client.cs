using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Bai05_Client : Form
    {

        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private string myNickname;
        private const string RandomDelimiter = "RANDOM_RESULT:";
        public Bai05_Client()
        {
            InitializeComponent();
            this.FormClosing += Bai05_Client_FormClosing;

        }

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            if (client != null && client.Connected) return;

            string potentialNickname = tb_newname.Text.Trim();

            if (string.IsNullOrEmpty(potentialNickname))
            {
                MessageBox.Show("Vui lòng nhập Tên người dùng vào ô Tên người đăng nhập trước khi Connect!");
                return;
            }

            try
            {
                client = new TcpClient();
                client.Connect(IPAddress.Parse("127.0.0.1"), 8080);

                writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true };
                reader = new StreamReader(client.GetStream(), Encoding.UTF8);

                writer.WriteLine(potentialNickname);
                myNickname = potentialNickname;

                btn_connect.Enabled = false;

                Thread receiveThread = new Thread(ReceiveMessages);
                receiveThread.IsBackground = true;
                receiveThread.Start();

                MessageBox.Show($"Kết nối thành công với Nickname: {myNickname}", "Thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối Server: {ex.Message}", "Lỗi");
                client?.Close();
                client = null;
            }
        }

        private void Disconnect()
        {
            if (client != null)
            {
                writer?.Close();
                reader?.Close();
                client.Close();
            }
        }

        private void Bai05_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Vui lòng Connect trước khi thêm món ăn.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMon = tb_newmon.Text.Trim();
            string tenNCC = tb_namencc.Text.Trim();
            string hinhAnh = tb_tenfileanh.Text.Trim();

            if (string.IsNullOrEmpty(tenMon) || string.IsNullOrEmpty(tenNCC) || string.IsNullOrEmpty(hinhAnh))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên món ăn, Tên người cung cấp và chọn ảnh!");
                return;
            }

            string insertCommand = $"INSERT:{tenMon}|{hinhAnh}|{tenNCC}";
            writer.WriteLine(insertCommand);


            tb_newmon.Clear();
            tb_tenfileanh.Clear();
            tb_namencc.Clear(); 
        }

        private void btn_mon_Click(object sender, EventArgs e)
        {
            if (CheckConnection())
            {
                writer.WriteLine("RANDOM_MINE:");
            }
        }

        private void btn_monmn_Click(object sender, EventArgs e)
        {
            if (CheckConnection())
            {
                writer.WriteLine("RANDOM_COMMUNITY:");
            }
        }

        private bool CheckConnection()
        {
            if (client == null || !client.Connected)
            {
                MessageBox.Show("Vui lòng bấm 'Connect' trước khi thực hiện chức năng!");
                return false;
            }
            return true;
        }

        private void ReceiveMessages()
        {
            try
            {
                string serverMessage;
                while ((serverMessage = reader.ReadLine()) != null)
                {
                    this.Invoke(new Action(() => ProcessServerMessage(serverMessage)));
                }
            }
            catch (Exception)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Mất kết nối với Server.");
                    btn_connect.Enabled = true;
                }));
            }
        }

        private void ProcessServerMessage(string message)
        {
            if (message.StartsWith(RandomDelimiter))
            {
                string result = message.Substring(RandomDelimiter.Length);
                var parts = result.Split('|');

                if (parts.Length >= 3)
                {
                    tb_kquamon.Text = parts[0];
                    tb_kqname.Text = parts[2];
                    string tenFileAnh = parts[1];

                    try
                    {
                        string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", tenFileAnh);

                        if (File.Exists(imagePath))
                        {
                            pb_hinhanh.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            pb_hinhanh.Image = null;
                            MessageBox.Show($"Không tìm thấy file ảnh: {tenFileAnh}", "Thiếu ảnh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        pb_hinhanh.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch (Exception ex)
                    {
                        pb_hinhanh.Image = null;
                        MessageBox.Show($"Lỗi hiển thị ảnh: {ex.Message}", "Lỗi");
                    }

                    MessageBox.Show($"Tìm thấy món ăn: {parts[0]}", "Kết quả Random", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                }
            }
            else if (message.StartsWith("SUCCESS:"))
            {
                string successMsg = message.Substring("SUCCESS:".Length);
                MessageBox.Show(successMsg, "Thêm món ăn thành công");
            }
            else if (message.StartsWith("ERROR:"))
            {
                string errorMsg = message.Substring("ERROR:".Length);
                MessageBox.Show(errorMsg, "Lỗi Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_hinhanh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string tenFileAnh = Path.GetFileName(openFileDialog.FileName);
                tb_tenfileanh.Text = tenFileAnh;
            }
        }
    }
}
