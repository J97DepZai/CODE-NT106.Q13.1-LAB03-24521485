namespace Lab3
{
    partial class Bai05
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
            btn_server = new Button();
            btn_client = new Button();
            SuspendLayout();
            // 
            // btn_server
            // 
            btn_server.BackColor = Color.Bisque;
            btn_server.Font = new Font("Segoe UI", 16F);
            btn_server.ForeColor = Color.Red;
            btn_server.Location = new Point(580, 118);
            btn_server.Name = "btn_server";
            btn_server.Size = new Size(647, 155);
            btn_server.TabIndex = 0;
            btn_server.Text = "Server";
            btn_server.UseVisualStyleBackColor = false;
            btn_server.Click += btn_server_Click;
            // 
            // btn_client
            // 
            btn_client.BackColor = Color.Bisque;
            btn_client.Font = new Font("Segoe UI", 16F);
            btn_client.ForeColor = Color.Red;
            btn_client.Location = new Point(593, 506);
            btn_client.Name = "btn_client";
            btn_client.Size = new Size(647, 155);
            btn_client.TabIndex = 1;
            btn_client.Text = "Client";
            btn_client.UseVisualStyleBackColor = false;
            btn_client.Click += btn_client_Click;
            // 
            // Bai05
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ScrollBar;
            ClientSize = new Size(1832, 1166);
            Controls.Add(btn_client);
            Controls.Add(btn_server);
            Name = "Bai05";
            Text = "Bai05";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_server;
        private Button btn_client;
    }
}