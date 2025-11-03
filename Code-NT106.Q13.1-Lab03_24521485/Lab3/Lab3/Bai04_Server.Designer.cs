namespace Lab3
{
    partial class Bai04_Server
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
            btn_listen = new Button();
            lv_message = new ListView();
            SuspendLayout();
            // 
            // btn_listen
            // 
            btn_listen.BackColor = Color.Aquamarine;
            btn_listen.Font = new Font("Segoe UI", 16F);
            btn_listen.ForeColor = Color.Red;
            btn_listen.Location = new Point(1305, 69);
            btn_listen.Name = "btn_listen";
            btn_listen.Size = new Size(429, 126);
            btn_listen.TabIndex = 0;
            btn_listen.Text = "Listen";
            btn_listen.UseVisualStyleBackColor = false;
            btn_listen.Click += btn_listen_Click;
            // 
            // lv_message
            // 
            lv_message.Font = new Font("Segoe UI", 16F);
            lv_message.Location = new Point(66, 246);
            lv_message.Name = "lv_message";
            lv_message.Size = new Size(1668, 857);
            lv_message.TabIndex = 1;
            lv_message.UseCompatibleStateImageBehavior = false;
            lv_message.View = View.List;
            // 
            // Bai04_Server
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1822, 1149);
            Controls.Add(lv_message);
            Controls.Add(btn_listen);
            Name = "Bai04_Server";
            Text = "Bai04_Server";
            FormClosing += Bai04_Server_FormClosing;
            Load += Bai04_Server_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btn_listen;
        private ListView lv_message;
    }
}