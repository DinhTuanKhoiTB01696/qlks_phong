using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UTIL_QLKS;
using Xuong02_QLKS;

namespace GUI_QLKS
{
    public partial class frmMainForm : Form
    {
        public frmMainForm()
        {
            InitializeComponent();
        }
        private Form currentFormChild;
        private void openChildForm(Form formChild)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
            }
            currentFormChild = formChild;
            formChild.TopLevel = false;
            formChild.FormBorderStyle = FormBorderStyle.None;
            formChild.Dock = DockStyle.Fill;
            pnMain.Controls.Add(formChild);
            pnMain.Tag = formChild;
            formChild.BringToFront();
            formChild.Show();


        }

        private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing)
            {
                //e.Cancel = true;
                Application.Exit();
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            danhMucToolStripMenuItem.Visible = false;
            datPhongToolStripMenuItem.Visible = false;
            nhanVienToolStripMenuItem.Visible = false;
            khachHangToolStripMenuItem.Visible = false;

            this.Hide();
            AuthUtil.user = null;
            frmLogin login = new frmLogin();
            login.Show();
        }

        private void loaitrangThaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDatPhong_DatPhong());
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            //openChildForm(new frmTrangThaiDatPhong());
        }
        private void VaiTroNhanVien()
        {
            danhMucToolStripMenuItem.Visible = false;
            datPhongToolStripMenuItem.Visible = false;
            nhanVienToolStripMenuItem.Visible = false;
            khachHangToolStripMenuItem.Visible = false;
        }

        private void nhanVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQLNhanVien());
        }

        private void phongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmPhong());
        }

        private void khachHangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQLKhachHang());
        }

        private void quảnLýDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmQLDichVu());
        }

        private void doimatKhauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmResetPassword());
        }

        private void chiTiếtDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmChiTietDichVu_2());
        }

        private void phòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDoanhThuPhong());
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmDoanhThuNhanVien());
        }

        private void loạiDịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmLoaiDichVu());
        }

        private void trangThaiĐatPhongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTrangThaiDatPhong());
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmThanhToanHoaDon());
        }

        private void loạiPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new frmLoaiPhong());
        }
    }
}
