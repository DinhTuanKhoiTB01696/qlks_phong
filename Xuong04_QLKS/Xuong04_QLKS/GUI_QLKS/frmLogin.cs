using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLKS;
using DTO_QLKS;
using UTIL_QLKS;

namespace GUI_QLKS
{
    public partial class frmLogin : Form
    {
        BUSNhanVien BUSNhanVien = new BUSNhanVien();
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            NhanVien nv = BUSNhanVien.DangNhap(username, password);
            if (nv == null)
            {
                MessageBox.Show(this, "Tài khoản hoặc mật khẩu không chính xác");
            }
            else
            {
                if (nv.TinhTrang == false)
                {
                    MessageBox.Show(this, "Tài khoản đang tạm khóa, vui lòng liên hệ QTV!!!");
                    return;
                }

                // ✅ FIX: Gán thông tin nhân viên đang đăng nhập
                AuthUtil.user = nv;

                frmMainForm main = new frmMainForm();
                main.Show();
                this.Hide();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn muốn thoát khỏi chương trình", "Thoát",
                           MessageBoxButtons.YesNo,
                           MessageBoxIcon.Question
                           );
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
