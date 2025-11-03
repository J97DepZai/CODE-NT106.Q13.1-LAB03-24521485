namespace Lab3
{
    partial class UDPServer
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
            label1 = new Label();
            tb_port = new TextBox();
            btn_listen = new Button();
            label2 = new Label();
            lv_message = new ListView();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F);
            label1.ForeColor = Color.Red;
            label1.Location = new Point(145, 86);
            label1.Name = "label1";
            label1.Size = new Size(114, 65);
            label1.TabIndex = 0;
            label1.Text = "Port";
            // 
            // tb_port
            // 
            tb_port.Font = new Font("Segoe UI", 16F);
            tb_port.ForeColor = SystemColors.ActiveCaptionText;
            tb_port.Location = new Point(340, 80);
            tb_port.Name = "tb_port";
            tb_port.Size = new Size(530, 71);
            tb_port.TabIndex = 1;
            // 
            // btn_listen
            // 
            btn_listen.BackColor = Color.Gainsboro;
            btn_listen.Font = new Font("Segoe UI", 16F);
            btn_listen.ForeColor = Color.Red;
            btn_listen.Location = new Point(1297, 70);
            btn_listen.Name = "btn_listen";
            btn_listen.Size = new Size(329, 81);
            btn_listen.TabIndex = 2;
            btn_listen.Text = "Listen";
            btn_listen.UseVisualStyleBackColor = false;
            btn_listen.Click += btn_listen_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.ForeColor = Color.Red;
            label2.Location = new Point(145, 288);
            label2.Name = "label2";
            label2.Size = new Size(432, 65);
            label2.TabIndex = 3;
            label2.Text = "Received messages";
            // 
            // lv_message
            // 
            lv_message.Font = new Font("Segoe UI", 16F);
            lv_message.Location = new Point(145, 431);
            lv_message.Name = "lv_message";
            lv_message.Size = new Size(1592, 629);
            lv_message.TabIndex = 4;
            lv_message.UseCompatibleStateImageBehavior = false;
            lv_message.View = View.List;
            // 
            // UDPServer
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Bisque;
            ClientSize = new Size(1840, 1112);
            Controls.Add(lv_message);
            Controls.Add(label2);
            Controls.Add(btn_listen);
            Controls.Add(tb_port);
            Controls.Add(label1);
            Name = "UDPServer";
            Text = "UDPServer";
            FormClosing += UDPServer_FormClosing;
            Load += UDPServer_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tb_port;
        private Button btn_listen;
        private Label label2;
        private ListView lv_message;
    }
}