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
    public partial class frmDoanhThuPhong : Form
    {
        public frmDoanhThuPhong()
        {
            InitializeComponent();
        }

        private void btnThongKe_Click_1(object sender, EventArgs e)
        {
            DateTime bd = dtpTuNgay.Value.Date;
            DateTime kt = dtpDenNgay.Value.Date;

            if (kt < bd)
            {
                MessageBox.Show("Ngày kết thúc không được nhỏ hơn ngày bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BUSThongKe busThongKe = new BUSThongKe();
            List<TKDoanhThuTheoLoaiPhong> result = busThongKe.getThongKeLoaiPhong(bd, kt);

            string maLoaiPhong = cbxPhong.SelectedValue?.ToString();
            if (!string.IsNullOrEmpty(maLoaiPhong))
                result = result.Where(x => x.MaLoaiPhong == maLoaiPhong).ToList();

            dgrDanhSachThongKe.DataSource = result;
        }





        private void frmDoanhThuNhanVien_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }
        private void frmDoanhThuPhong_Load(object sender, EventArgs e)
        {
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTuNgay.Value = firstDayOfMonth;

            dtpDenNgay.MinDate = dtpTuNgay.Value.Date;
            dtpDenNgay.MaxDate = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;

            dtpTuNgay.ValueChanged += dtpTuNgay_ValueChanged;

            LoadLoaiPhong();
            btnThongKe_Click_1(sender, e);
        }


        private void LoadLoaiPhong()
        {
            try
            {
                BUS_LoaiPhong bus = new BUS_LoaiPhong();
                List<LoaiPhong> ds = bus.GetLoaiPhongList();
                ds.Insert(0, new LoaiPhong() { MaLoaiPhong = string.Empty, TenLoaiPhong = "--Tất Cả--" });

                cbxPhong.DataSource = ds;
                cbxPhong.ValueMember = "MaLoaiPhong";
                cbxPhong.DisplayMember = "TenLoaiPhong";

                cbxPhong.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load loại phòng: " + ex.Message);
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

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime homNay = DateTime.Today;

            // Nếu "Từ Ngày" > hôm nay thì báo lỗi và trả lại giá trị cũ
            if (tuNgay > homNay)
            {
                MessageBox.Show("Không được chọn ngày trong tương lai!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpTuNgay.Value = homNay;
                tuNgay = homNay;
            }

            // Đặt giới hạn cho "Đến Ngày"
            dtpDenNgay.MaxDate = homNay;
            dtpDenNgay.MinDate = tuNgay;

            // Nếu "Đến Ngày" hiện tại không hợp lệ thì set lại
            if (dtpDenNgay.Value < tuNgay || dtpDenNgay.Value > homNay)
            {
                dtpDenNgay.Value = tuNgay;
            }
        }
    }
}

