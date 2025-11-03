namespace Lab3
{
    partial class Bai06_Client
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
            lv_message = new ListView();
            lv_participant = new ListView();
            tb_name = new TextBox();
            btn_connect = new Button();
            tb_message = new TextBox();
            btn_send = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_sendFile = new Button();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // lv_message
            // 
            lv_message.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lv_message.Location = new Point(54, 54);
            lv_message.Name = "lv_message";
            lv_message.Size = new Size(1241, 635);
            lv_message.TabIndex = 0;
            lv_message.UseCompatibleStateImageBehavior = false;
            lv_message.View = View.List;
            // 
            // lv_participant
            // 
            lv_participant.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lv_participant.Location = new Point(1412, 230);
            lv_participant.Name = "lv_participant";
            lv_participant.Size = new Size(341, 686);
            lv_participant.TabIndex = 1;
            lv_participant.UseCompatibleStateImageBehavior = false;
            lv_participant.View = View.List;
            // 
            // tb_name
            // 
            tb_name.Font = new Font("Segoe UI", 14F);
            tb_name.Location = new Point(54, 793);
            tb_name.Name = "tb_name";
            tb_name.Size = new Size(537, 63);
            tb_name.TabIndex = 2;
            // 
            // btn_connect
            // 
            btn_connect.BackColor = Color.MistyRose;
            btn_connect.Font = new Font("Segoe UI", 14F);
            btn_connect.ForeColor = Color.Red;
            btn_connect.Location = new Point(800, 776);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(196, 80);
            btn_connect.TabIndex = 3;
            btn_connect.Text = "Connect";
            btn_connect.UseVisualStyleBackColor = false;
            btn_connect.Click += btn_connect_Click;
            // 
            // tb_message
            // 
            tb_message.Font = new Font("Segoe UI", 14F);
            tb_message.Location = new Point(54, 979);
            tb_message.Name = "tb_message";
            tb_message.Size = new Size(1007, 63);
            tb_message.TabIndex = 4;
            tb_message.TextChanged += tb_message_TextChanged;
            tb_message.KeyDown += tb_message_KeyDown;
            // 
            // btn_send
            // 
            btn_send.BackColor = Color.MistyRose;
            btn_send.Font = new Font("Segoe UI", 14F);
            btn_send.ForeColor = Color.Red;
            btn_send.Location = new Point(1192, 969);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(251, 73);
            btn_send.TabIndex = 5;
            btn_send.Text = "Send";
            btn_send.UseVisualStyleBackColor = false;
            btn_send.Click += btn_send_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(54, 900);
            label1.Name = "label1";
            label1.Size = new Size(189, 57);
            label1.TabIndex = 6;
            label1.Text = "Message";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14F);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(54, 712);
            label2.Name = "label2";
            label2.Size = new Size(223, 57);
            label2.TabIndex = 7;
            label2.Text = "Your name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(1412, 124);
            label3.Name = "label3";
            label3.Size = new Size(251, 65);
            label3.TabIndex = 8;
            label3.Text = "Participant";
            // 
            // btn_sendFile
            // 
            btn_sendFile.BackColor = Color.MistyRose;
            btn_sendFile.Enabled = false;
            btn_sendFile.Font = new Font("Segoe UI", 14F);
            btn_sendFile.ForeColor = Color.Red;
            btn_sendFile.Location = new Point(1081, 776);
            btn_sendFile.Name = "btn_sendFile";
            btn_sendFile.Size = new Size(234, 80);
            btn_sendFile.TabIndex = 9;
            btn_sendFile.Text = "SendFile";
            btn_sendFile.UseVisualStyleBackColor = false;
            btn_sendFile.Click += btn_sendFile_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // Bai06_Client
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(1782, 1080);
            Controls.Add(btn_sendFile);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btn_send);
            Controls.Add(tb_message);
            Controls.Add(btn_connect);
            Controls.Add(tb_name);
            Controls.Add(lv_participant);
            Controls.Add(lv_message);
            Name = "Bai06_Client";
            Text = "Bai06_Client";
            FormClosing += Bai06_Client_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lv_message;
        private ListView lv_participant;
        private TextBox tb_name;
        private Button btn_connect;
        private TextBox tb_message;
        private Button btn_send;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btn_sendFile;
        private OpenFileDialog openFileDialog1;
    }
}