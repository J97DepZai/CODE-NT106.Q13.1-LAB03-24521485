namespace Lab3
{
    partial class Bai02
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
            rtb_message = new RichTextBox();
            SuspendLayout();
            // 
            // btn_listen
            // 
            btn_listen.BackColor = Color.Cyan;
            btn_listen.Font = new Font("Segoe UI", 16F);
            btn_listen.ForeColor = Color.Red;
            btn_listen.Location = new Point(1348, 90);
            btn_listen.Name = "btn_listen";
            btn_listen.Size = new Size(377, 94);
            btn_listen.TabIndex = 0;
            btn_listen.Text = "Listen";
            btn_listen.UseVisualStyleBackColor = false;
            btn_listen.Click += btn_listen_Click;
            // 
            // rtb_message
            // 
            rtb_message.Font = new Font("Segoe UI", 16F);
            rtb_message.Location = new Point(68, 280);
            rtb_message.Name = "rtb_message";
            rtb_message.Size = new Size(1657, 786);
            rtb_message.TabIndex = 1;
            rtb_message.Text = "";
            rtb_message.TextChanged += rtb_message_TextChanged;
            // 
            // Bai02
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSalmon;
            ClientSize = new Size(1925, 1119);
            Controls.Add(rtb_message);
            Controls.Add(btn_listen);
            Name = "Bai02";
            Text = "Bai02";
            FormClosing += Bai02_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private Button btn_listen;
        private RichTextBox rtb_message;
    }
}