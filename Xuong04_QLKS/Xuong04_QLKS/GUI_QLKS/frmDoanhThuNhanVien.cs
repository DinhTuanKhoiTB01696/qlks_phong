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


namespace GUI_QLKS
{
    public partial class frmDoanhThuNhanVien : Form
    {
        public frmDoanhThuNhanVien()
        {
            InitializeComponent();
        }

        //private void btnThongKe_Click(object sender, EventArgs e)
        //{

        //}


        private void frmDoanhThuNhanVien_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }

        //private void frmDoanhThuNhanVien_Load(object sender, EventArgs e)
        //{

        //}
        private void LoadNhanVien()
        {
            try
            {
                BUSNhanVien busNhanVien = new BUSNhanVien();
                List<NhanVien> dsNhanVien = busNhanVien.GetNhanVienList();
                dsNhanVien.Insert(0, new NhanVien() { MaNV = string.Empty, HoTen = "--Tất Cả--" });
                cbxNhanVien.DataSource = dsNhanVien;
                cbxNhanVien.ValueMember = "MaNV";
                cbxNhanVien.DisplayMember = "HoTen";

                cbxNhanVien.SelectedIndex = 0; // Đặt mặc định một mục được chọn
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmDoanhThuNhanVien_Load_1(object sender, EventArgs e)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTuNgay.Value = firstDayOfMonth;

            // Gắn giới hạn cho dtpDenNgay
            dtpDenNgay.MinDate = dtpTuNgay.Value.Date;
            dtpDenNgay.MaxDate = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            // Gắn sự kiện
            dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;
            dtpDenNgay.ValueChanged += dtpDenNgay_ValueChanged;

            LoadNhanVien();
            btnThongKe_Click_1(sender, e);
        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            DateTime bd = dtpTuNgay.Value.Date;
            DateTime kt = dtpDenNgay.Value.Date;

            BUSThongKe busThongKe = new BUSThongKe();
            List<TKDoanhThuTheoNhanVien> result = busThongKe.getThongKeTheoNhanVien(bd, kt);
            dgrDanhSachThongKe.DataSource = result;
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime homNay = DateTime.Today;

            // Nếu "Từ Ngày" > hôm nay → thông báo và set lại
            if (tuNgay > homNay)
            {
                MessageBox.Show("Không được chọn ngày trong tương lai!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTuNgay.Value = homNay;
                tuNgay = homNay;
            }

            // Cập nhật giới hạn cho "Đến Ngày"
            dtpDenNgay.MaxDate = homNay;
            dtpDenNgay.MinDate = tuNgay;

            // Nếu "Đến Ngày" không nằm trong khoảng cho phép → sửa lại
            if (dtpDenNgay.Value < tuNgay || dtpDenNgay.Value > homNay)
            {
                dtpDenNgay.Value = tuNgay;
            }
        }

        private void dtpDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDenNgay.Value < dtpTuNgay.Value)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDenNgay.Value = dtpTuNgay.Value;
            }
        }
    }
}
