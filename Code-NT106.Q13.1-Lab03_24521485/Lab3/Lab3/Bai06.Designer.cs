namespace Lab3
{
    partial class Bai06
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
            btn_server.BackColor = Color.MistyRose;
            btn_server.Font = new Font("Segoe UI", 16F);
            btn_server.ForeColor = Color.Red;
            btn_server.Location = new Point(379, 190);
            btn_server.Name = "btn_server";
            btn_server.Size = new Size(752, 170);
            btn_server.TabIndex = 0;
            btn_server.Text = "Server";
            btn_server.UseVisualStyleBackColor = false;
            btn_server.Click += btn_server_Click;
            // 
            // btn_client
            // 
            btn_client.BackColor = Color.MistyRose;
            btn_client.Font = new Font("Segoe UI", 16F);
            btn_client.ForeColor = Color.Red;
            btn_client.Location = new Point(379, 584);
            btn_client.Name = "btn_client";
            btn_client.Size = new Size(752, 170);
            btn_client.TabIndex = 1;
            btn_client.Text = "Client";
            btn_client.UseVisualStyleBackColor = false;
            btn_client.Click += btn_client_Click;
            // 
            // Bai06
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Crimson;
            ClientSize = new Size(1695, 1192);
            Controls.Add(btn_client);
            Controls.Add(btn_server);
            Name = "Bai06";
            Text = "Bai06";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_server;
        private Button btn_client;
    }
}