namespace Lab3
{
    partial class Bai06_Server
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
            btn_listen.BackColor = Color.Honeydew;
            btn_listen.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_listen.ForeColor = Color.Red;
            btn_listen.Location = new Point(1370, 71);
            btn_listen.Name = "btn_listen";
            btn_listen.Size = new Size(433, 101);
            btn_listen.TabIndex = 0;
            btn_listen.Text = "Listen";
            btn_listen.UseVisualStyleBackColor = false;
            btn_listen.Click += btn_listen_Click;
            // 
            // lv_message
            // 
            lv_message.Location = new Point(38, 230);
            lv_message.Name = "lv_message";
            lv_message.Size = new Size(1765, 885);
            lv_message.TabIndex = 1;
            lv_message.UseCompatibleStateImageBehavior = false;
            lv_message.View = View.List;
            // 
            // Bai06_Server
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSalmon;
            ClientSize = new Size(1895, 1177);
            Controls.Add(lv_message);
            Controls.Add(btn_listen);
            Name = "Bai06_Server";
            Text = "Bai06_Server";
            FormClosing += Bai06_Server_FormClosing;
            Load += Bai06_Server_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btn_listen;
        private ListView lv_message;
    }
}