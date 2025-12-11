using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLKS
{
    public partial class ChatBox : Form
    {
        public ChatBox()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            txtCauHoi.KeyDown += TxtCauHoi_KeyDown;
            timer1.Tick += Timer1_Tick;
            timer1.Start();
        }
        
        
        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            lblThoiGian.Text = "Thời gian: " + DateTime.Now.ToString("hh:mm:ss tt");
        }

        private void TxtCauHoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Không xuống dòng
                GuiCauHoi();
            }
        }
        private void btnGui_Click(object sender, EventArgs e)
        {
            GuiCauHoi();
        }
        private void GuiCauHoi()
        {
            string cauHoi = txtCauHoi.Text.Trim();
            if (string.IsNullOrEmpty(cauHoi))
                return;

            rtbChatLog.AppendText("Bạn: " + cauHoi + "\n");

            string traLoi = TraLoiGiaLap(cauHoi);
            rtbChatLog.AppendText("AI: " + traLoi + "\n\n");

            txtCauHoi.Clear();
            txtCauHoi.Focus();

            // Tự động cuộn
            rtbChatLog.SelectionStart = rtbChatLog.Text.Length;
            rtbChatLog.ScrollToCaret();
        }

        private string TraLoiGiaLap(string cauHoi)
        {
            string lower = cauHoi.ToLower();
            if (lower.Contains("check-in"))
                return "Giờ check-in là từ 14:00 đến 22:00 mỗi ngày.";
            else if (lower.Contains("check-out"))
                return "Giờ check-out là trước 12:00 trưa.";
            else if (lower.Contains("spa"))
                return "Dịch vụ spa hoạt động từ 8h đến 21h mỗi ngày.";
            else if (lower.Contains("wifi"))
                return "Khách sạn cung cấp Wi-Fi miễn phí toàn khuôn viên.";
            else
                return "Cảm ơn bạn. Tôi sẽ chuyển câu hỏi đến nhân viên hỗ trợ.";
        }
    }
}
