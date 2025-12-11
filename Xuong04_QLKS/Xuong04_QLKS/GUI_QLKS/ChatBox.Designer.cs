namespace GUI_QLKS
{
    partial class ChatBox
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            txtCauHoi = new Guna.UI2.WinForms.Guna2TextBox();
            rtbChatLog = new RichTextBox();
            guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            btnGui = new Guna.UI2.WinForms.Guna2Button();
            lblTieuDe = new Guna.UI2.WinForms.Guna2HtmlLabel();
            lblThoiGian = new Guna.UI2.WinForms.Guna2HtmlLabel();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // txtCauHoi
            // 
            txtCauHoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCauHoi.CustomizableEdges = customizableEdges5;
            txtCauHoi.DefaultText = "";
            txtCauHoi.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            txtCauHoi.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            txtCauHoi.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            txtCauHoi.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            txtCauHoi.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCauHoi.Font = new Font("Segoe UI", 9F);
            txtCauHoi.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            txtCauHoi.Location = new Point(120, 510);
            txtCauHoi.Margin = new Padding(3, 4, 3, 4);
            txtCauHoi.Name = "txtCauHoi";
            txtCauHoi.PasswordChar = '\0';
            txtCauHoi.PlaceholderText = "";
            txtCauHoi.SelectedText = "";
            txtCauHoi.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtCauHoi.Size = new Size(444, 39);
            txtCauHoi.TabIndex = 0;
            // 
            // rtbChatLog
            // 
            rtbChatLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbChatLog.Location = new Point(50, 81);
            rtbChatLog.Name = "rtbChatLog";
            rtbChatLog.ReadOnly = true;
            rtbChatLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbChatLog.Size = new Size(993, 398);
            rtbChatLog.TabIndex = 3;
            rtbChatLog.Text = "";
            // 
            // guna2HtmlLabel2
            // 
            guna2HtmlLabel2.BackColor = Color.Transparent;
            guna2HtmlLabel2.Location = new Point(63, 510);
            guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            guna2HtmlLabel2.Size = new Size(31, 22);
            guna2HtmlLabel2.TabIndex = 4;
            guna2HtmlLabel2.Text = "Bạn:";
            // 
            // btnGui
            // 
            btnGui.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnGui.CustomizableEdges = customizableEdges7;
            btnGui.DisabledState.BorderColor = Color.DarkGray;
            btnGui.DisabledState.CustomBorderColor = Color.DarkGray;
            btnGui.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnGui.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnGui.Font = new Font("Segoe UI", 9F);
            btnGui.ForeColor = Color.White;
            btnGui.Location = new Point(585, 510);
            btnGui.Name = "btnGui";
            btnGui.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnGui.Size = new Size(146, 39);
            btnGui.TabIndex = 5;
            btnGui.Text = "Gửi";
            btnGui.Click += btnGui_Click;
            // 
            // lblTieuDe
            // 
            lblTieuDe.BackColor = Color.Transparent;
            lblTieuDe.Location = new Point(391, 26);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(213, 22);
            lblTieuDe.TabIndex = 6;
            lblTieuDe.Text = "Hộp Thoại AI Hỗ Trợ Khách Sạn";
            // 
            // lblThoiGian
            // 
            lblThoiGian.BackColor = Color.Transparent;
            lblThoiGian.Location = new Point(63, 573);
            lblThoiGian.Name = "lblThoiGian";
            lblThoiGian.Size = new Size(68, 22);
            lblThoiGian.TabIndex = 7;
            lblThoiGian.Text = "Thời gian:";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            // 
            // ChatBox
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1083, 607);
            Controls.Add(lblThoiGian);
            Controls.Add(lblTieuDe);
            Controls.Add(btnGui);
            Controls.Add(guna2HtmlLabel2);
            Controls.Add(rtbChatLog);
            Controls.Add(txtCauHoi);
            Name = "ChatBox";
            Text = "ChatBox";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2TextBox txtCauHoi;
        private RichTextBox rtbChatLog;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2Button btnGui;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTieuDe;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblThoiGian;
        private System.Windows.Forms.Timer timer1;
    }
}