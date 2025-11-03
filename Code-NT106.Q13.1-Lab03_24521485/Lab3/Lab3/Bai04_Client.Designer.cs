namespace Lab3
{
    partial class Bai04_Client
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_book = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            clb_seat = new CheckedListBox();
            rtb_ketqua = new RichTextBox();
            tb_name = new TextBox();
            cbb_movie = new ComboBox();
            cbb_room = new ComboBox();
            btn_connect = new Button();
            SuspendLayout();
            // 
            // btn_book
            // 
            btn_book.BackColor = Color.SkyBlue;
            btn_book.Font = new Font("Segoe UI", 16F);
            btn_book.ForeColor = Color.Red;
            btn_book.Location = new Point(1007, 582);
            btn_book.Name = "btn_book";
            btn_book.Size = new Size(326, 114);
            btn_book.TabIndex = 0;
            btn_book.Text = "Đặt vé";
            btn_book.UseVisualStyleBackColor = false;
            btn_book.Click += btn_book_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(96, 187);
            label1.Name = "label1";
            label1.Size = new Size(367, 65);
            label1.TabIndex = 1;
            label1.Text = "Tên Khách Hàng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(530, 34);
            label2.Name = "label2";
            label2.Size = new Size(632, 65);
            label2.TabIndex = 2;
            label2.Text = "Quản Lí Phòng Vé Xem Phim";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(217, 325);
            label3.Name = "label3";
            label3.Size = new Size(220, 65);
            label3.TabIndex = 3;
            label3.Text = "Tên Phim";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 16F);
            label4.ForeColor = Color.Red;
            label4.Location = new Point(243, 462);
            label4.Name = "label4";
            label4.Size = new Size(165, 65);
            label4.TabIndex = 4;
            label4.Text = "Phòng";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 16F);
            label5.ForeColor = Color.Red;
            label5.Location = new Point(1555, 63);
            label5.Name = "label5";
            label5.Size = new Size(240, 65);
            label5.TabIndex = 5;
            label5.Text = "Thông Tin";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 16F);
            label6.ForeColor = Color.Red;
            label6.Location = new Point(641, 659);
            label6.Name = "label6";
            label6.Size = new Size(195, 65);
            label6.TabIndex = 6;
            label6.Text = "Đặt ghế";
            // 
            // clb_seat
            // 
            clb_seat.Font = new Font("Segoe UI", 16F);
            clb_seat.FormattingEnabled = true;
            clb_seat.Location = new Point(155, 759);
            clb_seat.Name = "clb_seat";
            clb_seat.Size = new Size(1277, 344);
            clb_seat.TabIndex = 7;
            clb_seat.ItemCheck += clb_seat_ItemCheck;
            clb_seat.SelectedIndexChanged += clb_seat_SelectedIndexChanged;
            // 
            // rtb_ketqua
            // 
            rtb_ketqua.BackColor = SystemColors.Info;
            rtb_ketqua.Font = new Font("Segoe UI", 16F);
            rtb_ketqua.ForeColor = SystemColors.WindowText;
            rtb_ketqua.Location = new Point(1383, 153);
            rtb_ketqua.Name = "rtb_ketqua";
            rtb_ketqua.Size = new Size(551, 571);
            rtb_ketqua.TabIndex = 8;
            rtb_ketqua.Text = "";
            // 
            // tb_name
            // 
            tb_name.Font = new Font("Segoe UI", 16F);
            tb_name.Location = new Point(558, 181);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(506, 71);
            tb_name.TabIndex = 9;
            // 
            // cbb_movie
            // 
            cbb_movie.Font = new Font("Segoe UI", 16F);
            cbb_movie.FormattingEnabled = true;
            cbb_movie.Location = new Point(558, 325);
            cbb_movie.Name = "cbb_movie";
            cbb_movie.Size = new Size(506, 73);
            cbb_movie.TabIndex = 10;
            cbb_movie.SelectedIndexChanged += cbb_movie_SelectedIndexChanged;
            // 
            // cbb_room
            // 
            cbb_room.Font = new Font("Segoe UI", 16F);
            cbb_room.FormattingEnabled = true;
            cbb_room.Location = new Point(558, 454);
            cbb_room.Name = "cbb_room";
            cbb_room.Size = new Size(506, 73);
            cbb_room.TabIndex = 11;
            cbb_room.SelectedIndexChanged += cbb_room_SelectedIndexChanged;
            // 
            // btn_connect
            // 
            btn_connect.BackColor = Color.SkyBlue;
            btn_connect.Font = new Font("Segoe UI", 16F);
            btn_connect.ForeColor = Color.Red;
            btn_connect.Location = new Point(117, 584);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(229, 112);
            btn_connect.TabIndex = 12;
            btn_connect.Text = "Connect";
            btn_connect.UseVisualStyleBackColor = false;
            btn_connect.Click += btn_connect_Click;
            // 
            // Bai04_Client
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MistyRose;
            ClientSize = new Size(1946, 1135);
            Controls.Add(btn_connect);
            Controls.Add(cbb_room);
            Controls.Add(cbb_movie);
            Controls.Add(tb_name);
            Controls.Add(rtb_ketqua);
            Controls.Add(clb_seat);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btn_book);
            Name = "Bai04_Client";
            Text = "Bai04_Client";
            FormClosing += Bai04_Client_FormClosing;
            Load += Bai04_Client_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_book;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private CheckedListBox clb_seat;
        private RichTextBox rtb_ketqua;
        private TextBox tb_name;
        private ComboBox cbb_movie;
        private ComboBox cbb_room;
        private Button btn_connect;
    }
}