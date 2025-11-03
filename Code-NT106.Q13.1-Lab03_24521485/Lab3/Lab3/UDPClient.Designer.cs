namespace Lab3
{
    partial class UDPClient
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
            tb_remote = new TextBox();
            tb_port = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btn_send = new Button();
            rtb_message = new RichTextBox();
            SuspendLayout();
            // 
            // tb_remote
            // 
            tb_remote.Font = new Font("Segoe UI", 16F);
            tb_remote.ForeColor = Color.Black;
            tb_remote.Location = new Point(87, 182);
            tb_remote.Name = "tb_remote";
            tb_remote.Size = new Size(591, 71);
            tb_remote.TabIndex = 0;
            // 
            // tb_port
            // 
            tb_port.Font = new Font("Segoe UI", 16F);
            tb_port.ForeColor = Color.Black;
            tb_port.Location = new Point(1023, 182);
            tb_port.Name = "tb_port";
            tb_port.Size = new Size(510, 71);
            tb_port.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(87, 75);
            label1.Name = "label1";
            label1.Size = new Size(348, 65);
            label1.TabIndex = 2;
            label1.Text = "IP Remote host";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(1023, 75);
            label2.Name = "label2";
            label2.Size = new Size(114, 65);
            label2.TabIndex = 3;
            label2.Text = "Port";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16F);
            label3.ForeColor = Color.Red;
            label3.Location = new Point(87, 394);
            label3.Name = "label3";
            label3.Size = new Size(213, 65);
            label3.TabIndex = 4;
            label3.Text = "Message";
            // 
            // btn_send
            // 
            btn_send.BackColor = SystemColors.ActiveBorder;
            btn_send.Font = new Font("Segoe UI", 16F);
            btn_send.ForeColor = Color.Red;
            btn_send.Location = new Point(87, 952);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(394, 80);
            btn_send.TabIndex = 6;
            btn_send.Text = "Send";
            btn_send.UseVisualStyleBackColor = false;
            btn_send.Click += btn_send_Click;
            // 
            // rtb_message
            // 
            rtb_message.Font = new Font("Segoe UI", 16F);
            rtb_message.ForeColor = SystemColors.ActiveCaptionText;
            rtb_message.Location = new Point(87, 488);
            rtb_message.Name = "rtb_message";
            rtb_message.Size = new Size(1519, 381);
            rtb_message.TabIndex = 7;
            rtb_message.Text = "";
            // 
            // UDPClient
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Bisque;
            ClientSize = new Size(1723, 1094);
            Controls.Add(rtb_message);
            Controls.Add(btn_send);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(tb_port);
            Controls.Add(tb_remote);
            Name = "UDPClient";
            Text = "UDPClient";
            Load += UDPClient_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tb_remote;
        private TextBox tb_port;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btn_send;
        private RichTextBox rtb_message;
    }
}