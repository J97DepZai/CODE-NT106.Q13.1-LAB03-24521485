using Microsoft.Data.Sqlite;
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
    public partial class Bai05_Server : Form
    {

        private TcpListener listener;
        private Dictionary<string, TcpClient> clientList = new Dictionary<string, TcpClient>();
        private readonly object lockObj = new object();
        private const string RandomDelimiter = "RANDOM_RESULT:";
        public Bai05_Server()
        {
            InitializeComponent();
            this.FormClosing += Bai05_Server_FormClosing;

            DataSQLite.InitializeDatabase();
        }

        private void btn_listen_Click(object sender, EventArgs e)
        {
            if (listener == null)
            {
                StartServer(8080);
                if (sender is Button btn) btn.Text = "Stop";
            }
            else
            {
                StopServer();
                if (sender is Button btn) btn.Text = "Listen";
            }
        }


        public void StartServer(int port = 8080)
        {
            if (listener != null) return;

            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                UpdateLog($"Server đang chạy trên 127.0.0.1:{port}");

                Thread listenThread = new Thread(ListenForClients);
                listenThread.IsBackground = true;
                listenThread.Start();

                MessageBox.Show($"Server đã bắt đầu lắng nghe trên 127.0.0.1:{port}", "Server ON", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UpdateLog($"Lỗi khởi động Server: {ex.Message}");
                MessageBox.Show($"Lỗi khởi động Server: {ex.Message}", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listener = null;
            }
        }

        public void StopServer()
        {
            if (listener != null)
            {
                listener.Stop();
                listener = null;
                UpdateLog("Server đã dừng.");

                lock (lockObj)
                {
                    foreach (var client in clientList.Values) client.Close();
                    clientList.Clear();
                }
            }
        }

        private void Bai05_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopServer();
        }


        private void ListenForClients()
        {
            try
            {
                while (listener != null)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.IsBackground = true;
                    clientThread.Start();
                }
            }
            catch (SocketException) { UpdateLog("Server đã dừng lắng nghe."); }
            catch (Exception ex) { UpdateLog($"Lỗi ListenForClients: {ex.Message}"); }
        }

        private void HandleClient(TcpClient client)
        {
            string nickname = "";
            try
            {
                using (var reader = new StreamReader(client.GetStream(), Encoding.UTF8))
                using (var writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true })
                {
                    nickname = reader.ReadLine();
                    if (string.IsNullOrEmpty(nickname)) return;

                    lock (lockObj) clientList.Add(nickname, client);
                    UpdateLog($"[{nickname}] đã tham gia.");

                    string message;
                    while ((message = reader.ReadLine()) != null)
                    {
                        if (message.StartsWith("RANDOM_MINE:")) ProcessRandomFoodRequest(nickname, nickname);
                        else if (message.StartsWith("RANDOM_COMMUNITY:")) ProcessRandomFoodRequest(nickname, "EXTERNAL_FOOD");
                        else if (message.StartsWith("INSERT:"))
                        {
                            string data = message.Substring(7);
                            ProcessInsertFoodRequest(nickname, data);
                        }
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                if (!string.IsNullOrEmpty(nickname))
                {
                    lock (lockObj) clientList.Remove(nickname);
                    UpdateLog($"[{nickname}] đã rời nhóm.");
                }
                client.Close();
            }
        }

        private void ProcessRandomFoodRequest(string senderNickname, string targetNickname)
        {
            string sql = "";
            string resultMessage = "ERROR:Không tìm thấy món ăn phù hợp.";
            string connectionString = DataSQLite.ConnectionString;

            if (targetNickname.Equals("EXTERNAL_FOOD"))
            {
                sql = "SELECT m.TenMonAn, m.HinhAnh, n.HoTen FROM MonAn m " +
                      "JOIN NguoiDung n ON m.IDNCC = n.IDNCC " +
                      "WHERE n.IDNCC > 1 ORDER BY RANDOM() LIMIT 1";
            }
            else
            {
                sql = "SELECT m.TenMonAn, m.HinhAnh, n.HoTen FROM MonAn m " +
                      "JOIN NguoiDung n ON m.IDNCC = n.IDNCC " +
                      $"WHERE n.HoTen = '{targetNickname.Replace("'", "''")}' ORDER BY RANDOM() LIMIT 1";
            }

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new SqliteCommand(sql, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string tenMon = reader.GetString(0);
                                string hinhAnh = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                string ncc = reader.GetString(2);
                                resultMessage = $"{RandomDelimiter}{tenMon}|{hinhAnh}|{ncc}";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultMessage = $"ERROR:Lỗi truy vấn CSDL: {ex.Message}";
                UpdateLog($"Lỗi CSDL: {ex.Message}");
            }

            SendMessageToUser(senderNickname, resultMessage, isRaw: true);
        }

        private void ProcessInsertFoodRequest(string senderNickname, string data)
        {
            var parts = data.Split('|');
            if (parts.Length < 3)
            {
                SendMessageToUser(senderNickname, "ERROR: Dữ liệu món ăn không đầy đủ.", isRaw: true);
                return;
            }

            string tenMon = parts[0];
            string hinhAnh = parts[1];
            string tenNCC = parts[2];

            string resultMessage;
            string connectionString = DataSQLite.ConnectionString;

            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    SqliteTransaction transaction = connection.BeginTransaction();

                    long idncc;
                    var findCmd = new SqliteCommand("SELECT IDNCC FROM NguoiDung WHERE HoTen = @HoTen", connection, transaction);
                    findCmd.Parameters.AddWithValue("@HoTen", tenNCC);
                    var result = findCmd.ExecuteScalar();

                    if (result != null)
                    {
                        idncc = (long)result;
                    }
                    else
                    {
                        var insertUserCmd = new SqliteCommand("INSERT INTO NguoiDung (HoTen, QuyenHan) VALUES (@HoTen, 'User'); SELECT last_insert_rowid();", connection, transaction);
                        insertUserCmd.Parameters.AddWithValue("@HoTen", tenNCC);
                        idncc = (long)insertUserCmd.ExecuteScalar();
                    }

                    var insertFoodCmd = new SqliteCommand("INSERT INTO MonAn (TenMonAn, HinhAnh, IDNCC) VALUES (@TenMonAn, @HinhAnh, @IDNCC)", connection, transaction);
                    insertFoodCmd.Parameters.AddWithValue("@TenMonAn", tenMon);
                    insertFoodCmd.Parameters.AddWithValue("@HinhAnh", hinhAnh);
                    insertFoodCmd.Parameters.AddWithValue("@IDNCC", idncc);

                    insertFoodCmd.ExecuteNonQuery();

                    transaction.Commit();

                    resultMessage = $"SUCCESS: Đã thêm món '{tenMon}' do {tenNCC} cung cấp.";
                    UpdateLog($"[{senderNickname}] đã thêm món: {tenMon}");
                }
            }
            catch (Exception ex)
            {
                resultMessage = $"ERROR: Lỗi CSDL khi thêm món: {ex.Message}";
                UpdateLog($"Lỗi CSDL INSERT: {ex.Message}");
            }

            SendMessageToUser(senderNickname, resultMessage, isRaw: true);
        }

        public void UpdateLog(string text)
        {
            if (lv_message != null)
            {
                if (lv_message.InvokeRequired)
                {
                    lv_message.Invoke(new Action<string>(UpdateLog), text);
                }
                else
                {
                    string logEntry = $"[{DateTime.Now.ToShortTimeString()}] {text}";
                    ListViewItem item = new ListViewItem(logEntry);
                    lv_message.Items.Add(item);

                    lv_message.EnsureVisible(lv_message.Items.Count - 1);
                }
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now.ToShortTimeString()}] {text}");
            }
        }

        private void SendMessageToUser(string targetNickname, string message, bool isRaw = false)
        {
            lock (lockObj)
            {
                if (clientList.TryGetValue(targetNickname, out TcpClient client))
                {
                    try
                    {
                        var writer = new StreamWriter(client.GetStream(), Encoding.UTF8) { AutoFlush = true };
                        writer.WriteLine(message);
                    }
                    catch (Exception ex)
                    {
                        UpdateLog($"Lỗi gửi tin nhắn tới {targetNickname}: {ex.Message}");
                    }
                }
            }
        }
    }
}
