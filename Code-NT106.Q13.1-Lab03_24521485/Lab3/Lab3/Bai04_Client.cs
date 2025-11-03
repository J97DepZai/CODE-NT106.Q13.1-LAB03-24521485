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
using System.IO;

namespace Lab3
{
    public partial class Bai04_Client : Form
    {

        private TcpClient tcpClient;
        private StreamReader sr;
        private StreamWriter sw;
        public Bai04_Client()
        {
            InitializeComponent();
            ToggleControls(false); 
        }

        private void ToggleControls(bool enabled)
        {
            tb_name.Enabled = enabled;
            cbb_movie.Enabled = enabled;
            cbb_room.Enabled = enabled;
            clb_seat.Enabled = enabled;
            btn_book.Enabled = enabled;
        }

        private async void btn_book_Click(object sender, EventArgs e)
        {
            string customerName = tb_name.Text.Trim();
            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Vui lòng nhập họ tên!"); return;
            }
            if (clb_seat.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ghế!"); return;
            }
            if (sw == null) return;

            List<string> gheChon = new List<string>();
            foreach (var item in clb_seat.CheckedItems)
            {
                string seat = item.ToString().Split(' ')[0];
                gheChon.Add(seat);
            }
            if (gheChon.Count == 0)
            {
                MessageBox.Show("Bạn không thể chọn ghế đã được mua!"); return;
            }

            string movieName = cbb_movie.SelectedItem.ToString();
            string room = cbb_room.SelectedItem.ToString();
            string gheChonStr = string.Join(",", gheChon);

            try
            {
                await sw.WriteLineAsync($"BOOK|{movieName}|{room}|{customerName}|{gheChonStr}");
                string response = await sr.ReadLineAsync(); 

                if (response == null) throw new Exception("Server ngắt kết nối.");
                string[] parts = response.Split(new[] { '|' }, 2);

                if (parts[0] == "BOOK_SUCCESS")
                {
                    MessageBox.Show("Đặt vé thành công!");
                    rtb_ketqua.Text = parts[1].Replace("\\n", "\n"); 
                }
                else if (parts[0] == "BOOK_FAIL")
                {
                    MessageBox.Show(parts[1], "Lỗi Đặt Vé"); 
                }
                await RefreshSeatList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đặt vé: " + ex.Message);
            }

        }

        private async void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync("127.0.0.1", 8080);

                NetworkStream stream = tcpClient.GetStream();
                sr = new StreamReader(stream, Encoding.UTF8);
                sw = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

                await sw.WriteLineAsync("GET_MOVIES");
                string response = await sr.ReadLineAsync();

                if (response == null) throw new Exception("Server ngắt kết nối.");
                string[] parts = response.Split('|');

                if (parts[0] == "MOVIES")
                {
                    string[] movieNames = parts[1].Split(';');
                    cbb_movie.Items.Clear();
                    cbb_movie.Items.AddRange(movieNames);
                    if (cbb_movie.Items.Count > 0)
                        cbb_movie.SelectedIndex = 0; 

                    ToggleControls(true);
                    btn_connect.Enabled = false;
                    rtb_ketqua.Text = "Đã kết nối server! Vui lòng chọn phim.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối server: " + ex.Message);
            }

        }

        private void clb_seat_SelectedIndexChanged(object sender, EventArgs e)
        {
         
        }
        private async void cbb_movie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sw == null) return;
            string movieName = cbb_movie.SelectedItem.ToString();
            try
            {
                await sw.WriteLineAsync($"GET_ROOMS|{movieName}");
                string response = await sr.ReadLineAsync();

                if (response == null) throw new Exception("Server ngắt kết nối.");
                string[] parts = response.Split('|');

                if (parts[0] == "ROOMS")
                {
                    string[] roomNumbers = parts[1].Split(';');
                    cbb_room.Items.Clear();
                    cbb_room.Items.AddRange(roomNumbers);
                    if (cbb_room.Items.Count > 0)
                        cbb_room.SelectedIndex = 0; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách phòng: " + ex.Message);
            }
        }
        private async void cbb_room_SelectedIndexChanged(object sender, EventArgs e)
        {
            await RefreshSeatList();
        }


        private async Task RefreshSeatList()
        {
            if (sw == null || cbb_movie.SelectedItem == null || cbb_room.SelectedItem == null) return;
            string movieName = cbb_movie.SelectedItem.ToString();
            string room = cbb_room.SelectedItem.ToString();
            try
            {
                await sw.WriteLineAsync($"GET_SEATS|{movieName}|{room}");
                string response = await sr.ReadLineAsync();

                if (response == null) throw new Exception("Server ngắt kết nối.");
                string[] parts = response.Split('|');

                if (parts[0] == "SEATS")
                {
                    string[] seatStatusList = parts[1].Split(';');

                    clb_seat.ItemCheck -= clb_seat_ItemCheck;

                    clb_seat.Items.Clear();
                    foreach (string seatStatus in seatStatusList)
                    {
                        clb_seat.Items.Add(seatStatus);
                    }

                    for (int i = 0; i < clb_seat.Items.Count; i++)
                    {
                        clb_seat.SetItemCheckState(i, CheckState.Unchecked);
                    }

                    clb_seat.ItemCheck += clb_seat_ItemCheck; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy danh sách ghế: " + ex.Message);
            }
        }



        private void clb_seat_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (clb_seat.Items[e.Index].ToString().Contains("(Đã mua)") &&
                e.NewValue == CheckState.Checked)
            {
                e.NewValue = CheckState.Unchecked;
                MessageBox.Show("Ghế này đã được mua! Vui lòng chọn ghế khác");
            }
        }
        private void Bai04_Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (sr != null) sr.Close();
                if (sw != null) sw.Close();
                if (tcpClient != null) tcpClient.Close();
            }
            catch { }
        }

        private void Bai04_Client_Load(object sender, EventArgs e)
        {

        }
    }
}
