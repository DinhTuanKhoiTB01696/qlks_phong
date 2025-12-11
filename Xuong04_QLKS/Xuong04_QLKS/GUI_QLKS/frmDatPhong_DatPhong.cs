using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLKS;
using DAL_QLKS;
using Guna.UI2.WinForms;
using BLL_QLKS;
using static BLL_QLKS.Class1;
using System.Runtime.Intrinsics.Arm;

namespace Xuong02_QLKS
{
    public partial class frmDatPhong_DatPhong : Form
    {
        private TrangThaiDatPhongBLL bll = new TrangThaiDatPhongBLL();
        private LoaiTrangThaiDatPhongBLL loaiTrangThaiBLL = new LoaiTrangThaiDatPhongBLL();
        private BUS_Phong bllPhong = new BUS_Phong();
        private BUSKhachHang bllKH = new BUSKhachHang();
        private BUSNhanVien bllNV = new BUSNhanVien();
        private BUSDatPhong bllDatPhong = new BUSDatPhong();
        private BLLHoaDonThanhToan bllHoaDonThanhToan = new BLLHoaDonThanhToan();
        private string phongID;
        public frmDatPhong_DatPhong()
        {
            InitializeComponent();
            LoadDatPhong();
            LoadComboBox();
            LoadDanhSachPhongTrong();
            cboTenPhong.DataSource = bllPhong.GetAllPhong();
            cboTenPhong.DisplayMember = "TenPhong";  // Hiển thị Tên phòng
            cboTenPhong.ValueMember = "PhongID";

            dtpNgayDi.MinDate = DateTime.Today;
            dtpNgayDen.MinDate = DateTime.Today;


            // Cập nhật tình trạng phòng
            bllPhong.UpdateTinhTrangPhong(phongID, true);
            dgvDatPhong.DataSource = bllDatPhong.GetAllList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDatPhong();
            LoadComboBox();
            string newTrangThaiID = bll.GenerateNewTrangThaiID();
            cboPhongID.SelectedIndexChanged += cboPhongID_SelectedIndexChanged;

        }
        public class DatPhongView
        {
            public string HoaDonThueID { get; set; }
            public string PhongID { get; set; }
            public string TenPhong { get; set; }
            public string KhachHangID { get; set; }
            public DateTime NgayDen { get; set; }
            public DateTime NgayDi { get; set; }
            public string MaNV { get; set; }
            public string GhiChu { get; set; }
            //public string LoaiTrangThaiID { get; set; } 
            //public int TrangThai { get; set; }
        }
        private void SetNewHoaDonThueID()
        {
            DALDatPhong dal = new DALDatPhong();
            string newID = dal.generateHoaDonThueID();
            txtHoaDonThueID.Text = newID;
        }
        private void LoadComboBox()
        {
            cboPhongID.DataSource = bllPhong.GetAllPhong();
            cboPhongID.DisplayMember = "PhongID";
            cboPhongID.ValueMember = "PhongID";

            cboKhachHangID.DataSource = bllKH.GetAll();
            cboKhachHangID.DisplayMember = "HoTen";
            cboKhachHangID.ValueMember = "KhachHangID";

            cboMaNV.DataSource = bllNV.GetAll();
            cboMaNV.DisplayMember = "HoTen";
            cboMaNV.ValueMember = "MaNV";
        }

        private void ClearForm()
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cboPhongID.SelectedIndex = -1;
            cboMaNV.SelectedIndex = -1;
            dtpNgayDen.Value = DateTime.Now;
            dtpNgayDi.Value = DateTime.Now;
            cboKhachHangID.SelectedIndex = -1;
            txtGhiChu.Clear();
            txtHoaDonThueID.Clear();
            SetNewHoaDonThueID();
            cboTenPhong.Enabled = false;
        }



        private void LoadDatPhong()
        {
            dgvDatPhong.DataSource = null;

            var datPhongs = bllDatPhong.GetDatPhongList();
            var danhSachPhong = bllPhong.GetAllPhong();
            var danhSachTrangThai = loaiTrangThaiBLL.LayDanhSach();
            var danhSachKhachHang = bllKH.GetAll();
            var danhSachNhanVien = bllNV.GetAll();

            var viewList = datPhongs.Select(dp =>
            {
                //var trangThai = danhSachTrangThai.FirstOrDefault(t => t.LoaiTrangThaiID == dp.LoaiTrangThaiID);
                var tenKhach = danhSachKhachHang.FirstOrDefault(kh => kh.KhachHangID == dp.KhachHangID)?.HoTen ?? "";
                var tenNhanVien = danhSachNhanVien.FirstOrDefault(nv => nv.MaNV == dp.MaNV)?.HoTen ?? "";

                return new
                {
                    HoaDonThueID = dp.HoaDonThueID,
                    PhongID = dp.PhongID,
                    TenPhong = danhSachPhong.FirstOrDefault(p => p.PhongID == dp.PhongID)?.TenPhong ?? "",
                    KhachHangID = dp.KhachHangID, // giữ ID để combobox dùng
                    TenKhachHang = tenKhach,      // cột hiển thị tên
                    NgayDen = dp.NgayDen,
                    NgayDi = dp.NgayDi,
                    MaNV = dp.MaNV,               // giữ ID để combobox dùng
                    TenNhanVien = tenNhanVien,    // cột hiển thị tên
                    GhiChu = dp.GhiChu,
                    //LoaiTrangThaiID = dp.LoaiTrangThaiID,
                    //TenTrangThai = trangThai?.TenTrangThai ??
                    //               (string.IsNullOrEmpty(dp.LoaiTrangThaiID) ? "Chưa thanh toán" : $"Không rõ (ID: {dp.LoaiTrangThaiID})")
                };
            }).ToList();

            dgvDatPhong.DataSource = viewList;

            // Ẩn ID, chỉ hiện tên
            dgvDatPhong.Columns["KhachHangID"].Visible = false;
            dgvDatPhong.Columns["MaNV"].Visible = false;
            //dgvDatPhong.Columns["LoaiTrangThaiID"].Visible = false;

            // Tiêu đề
            dgvDatPhong.Columns["HoaDonThueID"].HeaderText = "Mã hóa đơn";
            dgvDatPhong.Columns["PhongID"].HeaderText = "Mã phòng";
            dgvDatPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
            dgvDatPhong.Columns["TenKhachHang"].HeaderText = "Tên khách hàng";
            dgvDatPhong.Columns["NgayDen"].HeaderText = "Ngày đến";
            dgvDatPhong.Columns["NgayDi"].HeaderText = "Ngày đi";
            dgvDatPhong.Columns["TenNhanVien"].HeaderText = "Tên nhân viên";
            dgvDatPhong.Columns["GhiChu"].HeaderText = "Ghi chú";
            //dgvDatPhong.Columns["TenTrangThai"].HeaderText = "Trạng thái";

            dgvDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }




        private void LoadDanhSachPhongTrong()
        {
            BLL_TimPhongTrong bll = new BLL_TimPhongTrong();
            var danhSachPhong = bll.LayPhongTrong();

            if (danhSachPhong == null || danhSachPhong.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu phòng trống hoặc có lỗi trong quá trình lấy dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvDanhSachPhong.DataSource = null;
                return;
            }

            dgvDanhSachPhong.DataSource = danhSachPhong;

            // Nếu danh sách có nhưng bị thiếu cột, việc đặt tên cột sẽ lỗi
            if (dgvDanhSachPhong.Columns["PhongID"] != null)
                dgvDanhSachPhong.Columns["PhongID"].HeaderText = "Mã phòng";
            if (dgvDanhSachPhong.Columns["TenPhong"] != null)
                dgvDanhSachPhong.Columns["TenPhong"].HeaderText = "Tên phòng";
            if (dgvDanhSachPhong.Columns["LoaiPhong"] != null)
                dgvDanhSachPhong.Columns["LoaiPhong"].HeaderText = "Loại phòng";
            if (dgvDanhSachPhong.Columns["TinhTrang"] != null)
                dgvDanhSachPhong.Columns["TinhTrang"].HeaderText = "Tình trạng";
            if (dgvDanhSachPhong.Columns["GiaTien"] != null)
                dgvDanhSachPhong.Columns["GiaTien"].HeaderText = "Giá tiền";

            dgvDanhSachPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }





        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemKhachHang.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadDatPhong(); // Load lại toàn bộ nếu không nhập gì
                return;
            }

            var datPhongs = bllDatPhong.GetDatPhongList()
                .Where(dp => dp.PhongID.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                          || dp.KhachHangID.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToList();

            var danhSachPhong = bllPhong.GetAllPhong();

            var viewList = datPhongs.Select(dp => new DatPhongView
            {
                HoaDonThueID = dp.HoaDonThueID,
                PhongID = dp.PhongID,
                TenPhong = danhSachPhong
                              .FirstOrDefault(p => p.PhongID?.Trim() == dp.PhongID?.Trim())
                              ?.TenPhong ?? "[Không tìm thấy]",
                KhachHangID = dp.KhachHangID,
                NgayDen = dp.NgayDen,
                NgayDi = dp.NgayDi,
                MaNV = dp.MaNV,
                GhiChu = dp.GhiChu
            }).ToList();

            dgvDatPhong.DataSource = viewList;

            // (Tuỳ chọn) Nếu bạn cần đặt lại tiêu đề hiển thị:
            dgvDatPhong.Columns["HoaDonThueID"].HeaderText = "Mã Hoá Đơn";
            dgvDatPhong.Columns["PhongID"].HeaderText = "Mã Phòng";
            dgvDatPhong.Columns["TenPhong"].HeaderText = "Tên Phòng";
            dgvDatPhong.Columns["KhachHangID"].HeaderText = "Mã KH";
            dgvDatPhong.Columns["NgayDen"].HeaderText = "Ngày Đến";
            dgvDatPhong.Columns["NgayDi"].HeaderText = "Ngày Đi";
            dgvDatPhong.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dgvDatPhong.Columns["GhiChu"].HeaderText = "Ghi Chú";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Lấy thông tin cần thiết
            string hoadonID = txtHoaDonThueID.Text.Trim();
            string phongId = cboPhongID.SelectedValue?.ToString();
            string maNV = cboMaNV.SelectedValue?.ToString();
            string khachhangID = cboKhachHangID.SelectedValue?.ToString();
            string ghiChu = txtGhiChu.Text.Trim();
            DateTime ngayDen = dtpNgayDen.Value.Date;
            DateTime ngayDi = dtpNgayDi.Value.Date;

            // Nếu người dùng chưa nhập hoặc chọn hóa đơn, thử lấy từ dòng đang chọn
            if (string.IsNullOrEmpty(hoadonID))
            {
                if (dgvDatPhong.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvDatPhong.SelectedRows[0];

                    hoadonID = selectedRow.Cells["HoaDonThueID"].Value.ToString();
                    phongId = selectedRow.Cells["PhongID"].Value.ToString();
                    maNV = selectedRow.Cells["MaNV"].Value.ToString();
                    khachhangID = selectedRow.Cells["KhachHangID"].Value.ToString();
                    ngayDen = Convert.ToDateTime(selectedRow.Cells["NgayDen"].Value);
                    ngayDi = Convert.ToDateTime(selectedRow.Cells["NgayDi"].Value);
                    ghiChu = selectedRow.Cells["GhiChu"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn đơn đặt phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Kiểm tra lại
            if (string.IsNullOrEmpty(hoadonID))
            {
                MessageBox.Show("Không tìm thấy ID hóa đơn để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn hủy đơn đặt phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // Gọi BUS xử lý
                BUSDatPhong bus = new BUSDatPhong();
                string kq = bus.DeleteDatPhong(hoadonID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa đơn đặt phòng thành công!\nMã hóa đơn: {hoadonID}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();     // Làm mới giao diện
                    LoadDatPhong();  // Load lại danh sách
                }
                else
                {
                    MessageBox.Show("Lỗi khi xóa đặt phòng: " + kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string phongId = cboPhongID.SelectedValue?.ToString();
            DateTime ngayDen = dtpNgayDen.Value.Date;
            DateTime ngayDi = dtpNgayDi.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();
            string maNV = cboMaNV.SelectedValue?.ToString();
            string hoadonID = txtHoaDonThueID.Text.Trim();
            string khachhangID = cboKhachHangID.SelectedValue?.ToString();


            if (string.IsNullOrEmpty(phongId) || string.IsNullOrEmpty(khachhangID) || string.IsNullOrEmpty(hoadonID))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sửa đặt phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var datPhongCu = bllDatPhong.GetDatPhongByID(hoadonID);
            bool thayDoiNgayHoacPhong = phongId != datPhongCu.PhongID
            || ngayDen != datPhongCu.NgayDen
            || ngayDi != datPhongCu.NgayDi;


            DatPhong kH = new DatPhong
            {
                KhachHangID = khachhangID,
                PhongID = phongId,
                MaNV = maNV,
                NgayDen = ngayDen,
                NgayDi = ngayDi,
                GhiChu = ghiChu,
                HoaDonThueID = hoadonID,

            };

            BUSDatPhong bus = new BUSDatPhong();
            string result = bus.UpdateDatPhong(kH);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDatPhong();
            }
            else
            {
                MessageBox.Show(result);
            }
            //Kiểm tra trùng thời gian(trừ chính nó)
                if (thayDoiNgayHoacPhong)
            {
                var datPhongs = bllDatPhong.GetDatPhongList();
                bool isPhongTrung = datPhongs.Any(dp =>
                    dp.PhongID == phongId &&
                    dp.HoaDonThueID != hoadonID &&
                    !(dp.NgayDi <= ngayDen || dp.NgayDen >= ngayDi)
                );
                if (isPhongTrung)
                {
                    MessageBox.Show("Phòng này đã được đặt trong khoảng thời gian này!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }


        //if (thayDoiNgayHoacPhong)
        //{
        //    // Kiểm tra logic ngày
        //    if (ngayDen >= ngayDi)
        //    {
        //        MessageBox.Show("Ngày đến phải nhỏ hơn ngày đi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }
        //    if (ngayDen < DateTime.Today)
        //    {
        //        MessageBox.Show("Ngày đến không được nhỏ hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;   
        //    }

        //}
        //    // Kiểm tra trùng thời gian (trừ chính nó)
        //    if (thayDoiNgayHoacPhong)
        //    {
        //        var datPhongs = bllDatPhong.GetDatPhongList();
        //        bool isPhongTrung = datPhongs.Any(dp =>
        //            dp.PhongID == phongId &&
        //            dp.HoaDonThueID != hoadonID &&
        //            !(dp.NgayDi <= ngayDen || dp.NgayDen >= ngayDi)
        //        );
        //        if (isPhongTrung)
        //        {
        //            MessageBox.Show("Phòng này đã được đặt trong khoảng thời gian này!",
        //                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }


        private void btnMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDatPhong();
        }



        private void dgvDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDatPhong.Rows[e.RowIndex];

                // Gán dữ liệu cơ bản
                txtHoaDonThueID.Text = row.Cells["HoaDonThueID"].Value.ToString();

                // Gán SelectedValue để hiển thị đúng DisplayMember
                cboKhachHangID.SelectedValue = row.Cells["KhachHangID"].Value.ToString();
                cboPhongID.SelectedValue = row.Cells["PhongID"].Value.ToString();
                cboMaNV.SelectedValue = row.Cells["MaNV"].Value.ToString();

                // Nếu combo TenPhong đã có dữ liệu thì mới gán SelectedValue
                if (cboTenPhong.DataSource != null)
                {
                    cboTenPhong.SelectedValue = row.Cells["PhongID"].Value.ToString();
                }

                // Ngày đến / ngày đi
                DateTime ngayDen = Convert.ToDateTime(row.Cells["NgayDen"].Value);
                dtpNgayDen.MinDate = DateTimePicker.MinimumDateTime;
                dtpNgayDen.Value = ngayDen;

                DateTime ngayDi = Convert.ToDateTime(row.Cells["NgayDi"].Value);
                dtpNgayDi.MinDate = DateTimePicker.MinimumDateTime;
                dtpNgayDi.Value = ngayDi;

                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";

                // Gán radio thanh toán
                //if (row.Cells["ThanhToan"].Value != DBNull.Value)
                //{
                //    bool daThanhToan = Convert.ToInt32(row.Cells["ThanhToan"].Value) == 1;
                //    rdoDaThanhToan.Checked = daThanhToan;
                //    rdoChuaThanhToan.Checked = !daThanhToan;
                //}

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
            }
        }



        private void cboPhongID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhongID.SelectedValue != null)
            {
                string phongID = cboPhongID.SelectedValue.ToString();
                var phong = bllPhong.GetPhongById(phongID);

                if (phong != null)
                {
                    cboTenPhong.Text = phong.TenPhong;
                }
                else
                {
                    cboTenPhong.Text = "";
                }
            }
        }



        private void btnThemDatPhong_Click(object sender, EventArgs e)
        {
            string phongId = cboPhongID.SelectedValue?.ToString();
            string khachhangID = cboKhachHangID.SelectedValue?.ToString();
            string maNV = cboMaNV.SelectedValue?.ToString();
            DateTime ngayDen = dtpNgayDen.Value.Date;
            DateTime ngayDi = dtpNgayDi.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();
            string hoadonID = txtHoaDonThueID.Text.Trim();


            if (string.IsNullOrEmpty(phongId) || string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(khachhangID))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đặt phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ngayDen >= ngayDi)
            {
                MessageBox.Show("Ngày đến phải nhỏ hơn ngày đi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ngayDen < DateTime.Today)
            {
                MessageBox.Show("Ngày đến không được nhỏ hơn ngày hiện tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            BUSDatPhong bus = new BUSDatPhong();
            bool isPhongDaDat = bus.KiemTraPhongDaDuocDat_1(phongId, ngayDen, ngayDi);

            if (isPhongDaDat)
            {
                MessageBox.Show("Phòng này đã được đặt trong khoảng thời gian này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatPhong datPhong = new DatPhong
            {
                HoaDonThueID = hoadonID,
                KhachHangID = khachhangID,
                PhongID = phongId,
                NgayDen = ngayDen,
                NgayDi = ngayDi,
                MaNV = maNV,
                GhiChu = ghiChu
            };

            string result = bus.InsertDatPhong(datPhong);

            if (string.IsNullOrEmpty(result))
            {
                TrangThaiDatPhongBLL bllTrangThai = new TrangThaiDatPhongBLL();
                string newTrangThaiID = bllTrangThai.GenerateNewTrangThaiID();

               

                TrangThaiDatPhongDTO trangThai = new TrangThaiDatPhongDTO
                {
                    TrangThaiID = newTrangThaiID,
                    HoaDonThueID = hoadonID,
            
                    NgayCapNhat = DateTime.Now
                };

                bllTrangThai.Them(trangThai);

                MessageBox.Show("Thêm mới đặt phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset form
                ClearForm();
                LoadDatPhong();

                // Cập nhật danh sách phòng trống
                BLL_TimPhongTrong bllTimPhong = new BLL_TimPhongTrong();
                var danhSachPhongTrong = bllTimPhong.TimPhongTheoNgay(ngayDen, ngayDi);
                dgvDanhSachPhong.DataSource = danhSachPhongTrong;
            }
        }

        private void cboTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPhongID.SelectedValue == null)
                return;

            string phongID = cboPhongID.SelectedValue.ToString();

            // Debug để kiểm tra có đúng Mã phòng không
            // MessageBox.Show("Đang lấy Tên phòng cho: " + phongID);

            Phong phong = bllPhong.GetPhongById(phongID);

            if (phong != null)
                cboTenPhong.Text = phong.TenPhong;
            else
                cboTenPhong.Text = ""; // không có phòng
        }

        private void dtpNgayDi_ValueChanged(object sender, EventArgs e)
        {
            // Nếu ngày đi nhỏ hơn hôm nay thì tự động đặt lại = hôm nay
            if (dtpNgayDi.Value.Date < DateTime.Today)
            {
                dtpNgayDi.Value = DateTime.Today;
            }

            // Nếu ngày đi nhỏ hơn ngày đến thì đặt lại = ngày đến
            if (dtpNgayDi.Value.Date < dtpNgayDen.Value.Date)
            {
                dtpNgayDi.Value = dtpNgayDen.Value.Date;
            }
        }

        public bool KiemTraPhongDaDuocDat(string maPhong, DateTime ngayDen, DateTime ngayDi)
        {
            var danhSachDatPhong = bllDatPhong.GetDatPhongConHieuLuc(); // ✅ Chỉ lấy đơn chưa hủy

            foreach (var datPhong in danhSachDatPhong)
            {
                string maPhongDaDat = datPhong.PhongID;
                DateTime ngayDenDaDat = datPhong.NgayDen;
                DateTime ngayDiDaDat = datPhong.NgayDi;

                if (maPhongDaDat == maPhong)
                {
                    if (ngayDen >= ngayDiDaDat)
                        continue;

                    if (ngayDi <= ngayDenDaDat)
                        continue;

                    return true; // Có giao nhau → phòng đã được đặt
                }
            }

            return false; // Không có đặt trùng
        }


        private void btnTimPhong_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpNgayDen.Value.Date;
            DateTime denNgay = dtpNgayDi.Value.Date;

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Ngày đến không được lớn hơn ngày đi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var bll = new BLL_TimPhongTrong();
            var danhSach = bll.TimPhongTheoNgay(tuNgay, denNgay);

            dgvDanhSachPhong.DataSource = danhSach;
        }


        private void CapNhatTrangThaiPhongSauDat(string phongID)
        {
            var bllPhong = new BUS_Phong();
            bllPhong.UpdateTinhTrangPhong(phongID, true); // true = đã được đặt
        }
        




        //private void rdoChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdoChuaThanhToan.Checked && !string.IsNullOrEmpty(txtHoaDonThueID.Text))
        //    {
        //        bllHoaDonThanhToan.CapNhatTrangThai(txtHoaDonThueID.Text, 0);
        //        LoadDatPhong();
        //    }
        //}

        //private void rdoDaThanhToan_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (rdoDaThanhToan.Checked && !string.IsNullOrEmpty(txtHoaDonThueID.Text))
        //    {
        //        bllHoaDonThanhToan.CapNhatTrangThai(txtHoaDonThueID.Text, 1);
        //        LoadDatPhong();
        //    }
        //}
    }

}
