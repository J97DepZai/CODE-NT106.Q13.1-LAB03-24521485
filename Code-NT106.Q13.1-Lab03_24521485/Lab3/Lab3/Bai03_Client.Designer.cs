namespace Lab3
{
    partial class Bai03_Client
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
            rtb_message = new RichTextBox();
            btn_connect = new Button();
            btn_send = new Button();
            btn_disconnect = new Button();
            SuspendLayout();
            // 
            // rtb_message
            // 
            rtb_message.Font = new Font("Segoe UI", 16F);
            rtb_message.Location = new Point(39, 108);
            rtb_message.Name = "rtb_message";
            rtb_message.Size = new Size(1310, 499);
            rtb_message.TabIndex = 0;
            rtb_message.Text = "";
            // 
            // btn_connect
            // 
            btn_connect.BackColor = Color.DeepSkyBlue;
            btn_connect.Font = new Font("Segoe UI", 16F);
            btn_connect.ForeColor = Color.Red;
            btn_connect.Location = new Point(1439, 192);
            btn_connect.Name = "btn_connect";
            btn_connect.Size = new Size(334, 94);
            btn_connect.TabIndex = 1;
            btn_connect.Text = "Connect";
            btn_connect.UseVisualStyleBackColor = false;
            btn_connect.Click += btn_connect_Click;
            // 
            // btn_send
            // 
            btn_send.BackColor = Color.DeepSkyBlue;
            btn_send.Font = new Font("Segoe UI", 16F);
            btn_send.ForeColor = Color.Red;
            btn_send.Location = new Point(1439, 355);
            btn_send.Name = "btn_send";
            btn_send.Size = new Size(334, 94);
            btn_send.TabIndex = 2;
            btn_send.Text = "Send";
            btn_send.UseVisualStyleBackColor = false;
            btn_send.Click += btn_send_Click;
            // 
            // btn_disconnect
            // 
            btn_disconnect.BackColor = Color.DeepSkyBlue;
            btn_disconnect.Font = new Font("Segoe UI", 16F);
            btn_disconnect.ForeColor = Color.Red;
            btn_disconnect.Location = new Point(1439, 513);
            btn_disconnect.Name = "btn_disconnect";
            btn_disconnect.Size = new Size(334, 94);
            btn_disconnect.TabIndex = 3;
            btn_disconnect.Text = "Disconnect";
            btn_disconnect.UseVisualStyleBackColor = false;
            btn_disconnect.Click += btn_disconnect_Click;
            // 
            // Bai03_Client
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Pink;
            ClientSize = new Size(1935, 1124);
            Controls.Add(btn_disconnect);
            Controls.Add(btn_send);
            Controls.Add(btn_connect);
            Controls.Add(rtb_message);
            Name = "Bai03_Client";
            Text = "Bai03_Client";
            FormClosing += Bai03_Client_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtb_message;
        private Button btn_connect;
        private Button btn_send;
        private Button btn_disconnect;
    }
}