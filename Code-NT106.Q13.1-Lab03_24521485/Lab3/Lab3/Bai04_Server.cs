using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;
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
using static Lab3.Bai04_Server;

namespace Lab3
{
    public partial class Bai04_Server : Form
    {

        private List<Movie> movies = new List<Movie>();
        private Dictionary<string, HashSet<string>> gheDaMuaTheoPhimPhong = new Dictionary<string, HashSet<string>>();

        private Dictionary<string, HashSet<int>> phongDaMuaTheoKhach = new Dictionary<string, HashSet<int>>();

        public enum Seat { GheVot, GheThuong, GheVip }

        private Dictionary<string, Seat> seatmap = new Dictionary<string, Seat>()
        {
            {"A1",Seat.GheVot }, {"A5",Seat.GheVot }, {"C1",Seat.GheVot }, {"C5",Seat.GheVot},
            {"A2",Seat.GheThuong}, {"A3",Seat.GheThuong }, {"A4",Seat.GheThuong },
            {"C2",Seat.GheThuong }, {"C3",Seat.GheThuong }, {"C4",Seat.GheThuong },
            {"B1",Seat.GheVip }, {"B2",Seat.GheVip }, {"B3",Seat.GheVip }, {"B4",Seat.GheVip }, {"B5",Seat.GheVip }
        };

        public class Movie
        {
            public string name { get; set; }
            public double price { get; set; }
            public List<int> room { get; set; }
        }

        private TcpListener tcpServer;
        private Thread listenThread;
        private readonly object _lock = new object();
        public Bai04_Server()
        {
            InitializeComponent();
        }

        private void btn_listen_Click(object sender, EventArgs e)
        {
            listenThread = new Thread(new ThreadStart(ListenForClients));
            listenThread.Start();
            btn_listen.Enabled = false;
        }



        private void Bai04_Server_Load(object sender, EventArgs e)
        {
            TaiPhimTuFile("input5.txt");
            if (movies.Count == 0)
                UpdateLog("LỖI: Không tìm thấy file input5.txt.");
            else
                UpdateLog($"Đã tải {movies.Count} phim từ input5.txt.");

        }

        private void ListenForClients()
        {
            try
            {
                tcpServer = new TcpListener(IPAddress.Any, 8080);
                tcpServer.Start();
                UpdateLog("Server bắt đầu lắng nghe trên cổng 8080...");

                while (true)
                {
                    TcpClient client = tcpServer.AcceptTcpClient();
                    UpdateLog($"Client mới kết nối từ {client.Client.RemoteEndPoint}");
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                    clientThread.Start(client);
                }
            }
            catch (SocketException) {}
        }

        private void HandleClientComm(object clientObj)
        {
            TcpClient client = (TcpClient)clientObj;
            NetworkStream stream = client.GetStream();
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            try
            {
                while (true)
                {
                    string command = sr.ReadLine();
                    if (command == null) break;

                    string[] parts = command.Split('|');
                    string response = "";

                    switch (parts[0])
                    {
                        case "GET_MOVIES":
                            response = "MOVIES|" + string.Join(";", movies.Select(m => m.name)); // Dùng ;
                            sw.WriteLine(response);
                            break;

                        case "GET_ROOMS":
                            {
                                string movieName = parts[1];
                                Movie movie = movies.Find(m => m.name == movieName);
                                if (movie != null)
                                    response = "ROOMS|" + string.Join(";", movie.room); // Dùng ;
                                else
                                    response = "ERROR|Movie not found";
                                sw.WriteLine(response);
                                break;
                            }

                        case "GET_SEATS":
                            {
                                string reqMovieName = parts[1];
                                string reqRoom = parts[2]; 
                                string key = $"{reqMovieName}_{reqRoom}"; 

                                List<string> seatStatusList = new List<string>();

                                lock (_lock)
                                {
                                    HashSet<string> gheDaMua = gheDaMuaTheoPhimPhong.ContainsKey(key)
                                        ? gheDaMuaTheoPhimPhong[key]
                                        : new HashSet<string>();

                                    for (char r = 'A'; r <= 'C'; r++)
                                        for (int c = 1; c <= 5; c++)
                                        {
                                            string ghe = $"{r}{c}";
                                            if (gheDaMua.Contains(ghe)) 
                                                seatStatusList.Add($"{ghe} (Đã mua)");
                                            else
                                                seatStatusList.Add(ghe);
                                        }
                                }
                                response = "SEATS|" + string.Join(";", seatStatusList);
                                sw.WriteLine(response);
                                break;
                            } 

                        case "BOOK":
                            {
                                response = ProcessBooking(parts); 
                                sw.WriteLine(response);
                                break;
                            }
                    }
                }
            }
            catch (Exception) {  }
            finally
            {
                UpdateLog($"Client {client.Client.RemoteEndPoint} đã ngắt kết nối.");
                client.Close();
            }
        }

        private string ProcessBooking(string[] parts)
        {
            string movieName = parts[1];
            string room = parts[2];
            string customerName = parts[3];
            List<string> gheChon = parts[4].Split(',').ToList();
            string key = $"{movieName}_{room}";

            Movie movie = movies.Find(m => m.name == movieName);
            if (movie == null) return "BOOK_FAIL|Phim không tồn tại.";

            lock (_lock)
            {
                if (!gheDaMuaTheoPhimPhong.ContainsKey(key))
                    gheDaMuaTheoPhimPhong[key] = new HashSet<string>();

                var gheDaMua = gheDaMuaTheoPhimPhong[key];
                var gheBiTrung = gheChon.Where(g => gheDaMua.Contains(g)).ToList();
                if (gheBiTrung.Any())
                {
                    return $"BOOK_FAIL|Ghế {string.Join(", ", gheBiTrung)} đã được mua. Vui lòng chọn lại.";
                }

                int soPhong = int.Parse(room);
                if (!phongDaMuaTheoKhach.ContainsKey(customerName))
                    phongDaMuaTheoKhach[customerName] = new HashSet<int>();

                if (!phongDaMuaTheoKhach[customerName].Contains(soPhong) &&
                     phongDaMuaTheoKhach[customerName].Count >= 2)
                {
                    return "BOOK_FAIL|Bạn chỉ được chọn tối đa 2 phòng chiếu khác nhau!";
                }

                double tongTien = 0;
                foreach (var ghe in gheChon)
                {
                    double gia = movie.price;
                    if (seatmap.ContainsKey(ghe)) 
                    {
                        if (seatmap[ghe] == Seat.GheVot) gia *= 0.25;
                        else if (seatmap[ghe] == Seat.GheVip) gia *= 2;
                    }
                    tongTien += gia;
                }

                foreach (var ghe in gheChon)
                    gheDaMua.Add(ghe);

                phongDaMuaTheoKhach[customerName].Add(soPhong);

                UpdateLog($"ĐÃ BÁN: {customerName} mua {gheChon.Count} vé ({string.Join(", ", gheChon)}) phim {movieName} (P{room}). TỔNG TIỀN: {tongTien:N0} đ");

                string receipt = $"Khách hàng: {customerName}\n" +
                                 $"Phim: {movieName}\n" +
                                 $"Phòng chiếu: {room}\n" +
                                 $"Ghế: {string.Join(", ", gheChon)}\n" +
                                 $"Tổng tiền: {tongTien:N0} đ";

                return $"BOOK_SUCCESS|{receipt}";
            }
        }

        private void TaiPhimTuFile(string path)
        {
            if (!File.Exists(path))
            {
                UpdateLog($"Lỗi: Không tìm thấy file {path}");
                return;
            }

            string[] lines = File.ReadAllLines(path, Encoding.UTF8);
            for (int i = 0; i < lines.Length;)
            {
                string ten = lines[i++];
                double gia = double.Parse(lines[i++]);
                var phongList = new List<int>();

                while (i < lines.Length && int.TryParse(lines[i], out int phong))
                    phongList.Add(int.Parse(lines[i++]));

                movies.Add(new Movie { name = ten, price = gia, room = phongList });
            }
        }

        private void UpdateLog(string message)
        {
            if (lv_message.InvokeRequired)
            {
                lv_message.Invoke(new MethodInvoker(delegate { lv_message.Items.Add(message); }));
            }
            else
            {
                lv_message.Items.Add(message);
            }
        }

        private void Bai04_Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { if (tcpServer != null) tcpServer.Stop(); } catch { }
        }
    }
}
