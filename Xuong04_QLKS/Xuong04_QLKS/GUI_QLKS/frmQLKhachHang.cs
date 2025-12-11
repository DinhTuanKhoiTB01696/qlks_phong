using System.ComponentModel;
using System.Text.RegularExpressions;
using BLL_QLKS;
using DAL_QLKS;
using DTO_QLKS;

namespace GUI_QLKS
{
    public partial class frmQLKhachHang : Form
    {
        public frmQLKhachHang()
        {
            InitializeComponent();
        }

        private void frmQLKhachHang_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachKhachHang();
            LoadGioiTinh();

            //dtpNgayTao.MinDate = DateTime.Today;
            //dtpNgayTao.MaxDate = DateTime.Today;

            txtSoDienThoai.MaxLength = 11;
            txtCCCD.MaxLength = 12;

            DALKhachHang dal = new DALKhachHang();
            txtMaKhachHang.Text = dal.generateKhachHangID();

        }
        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtMaKhachHang.Clear();
            txtHoTen.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtCCCD.Clear();
            cboGioiTinh.SelectedIndex = -1;
            txtGhiChu.Clear();
            dtpNgayTao.Value = DateTime.Today;



            txtSoDienThoai.MaxLength = 11;
            txtCCCD.MaxLength = 12;

            txtMaKhachHang.Enabled = false;

        }
        private void LoadDanhSachKhachHang()
        {
            try
            {
                BUSKhachHang busKhachHang = new BUSKhachHang();
                dgrDanhSachKH.DataSource = null;
                dgrDanhSachKH.DataSource = busKhachHang.GetKhachHangList();
                dgrDanhSachKH.Columns["KhachHangID"].HeaderText = "Mã Khách Hàng";
                dgrDanhSachKH.Columns["HoTen"].HeaderText = "Họ Tên";
                dgrDanhSachKH.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                dgrDanhSachKH.Columns["GioiTinh"].HeaderText = "Giới Tính";
                dgrDanhSachKH.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                dgrDanhSachKH.Columns["CCCD"].HeaderText = "CCCD";
                dgrDanhSachKH.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                dgrDanhSachKH.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dgrDanhSachKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách Khách Hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadGioiTinh()
        {
            cboGioiTinh.Items.Clear();
            cboGioiTinh.Items.Add("Nam");
            cboGioiTinh.Items.Add("Nữ");
            cboGioiTinh.Items.Add("Khác");
            cboGioiTinh.SelectedIndex = -1;
        }






        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            string KhachHangID = txtMaKhachHang.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? string.Empty;
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(KhachHangID))
            {
                if (dgrDanhSachKH.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgrDanhSachKH.SelectedRows[0];
                    KhachHangID = selectedRow.Cells["KhachHangID"].Value.ToString();
                    hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                    diaChi = selectedRow.Cells["DiaChi"].Value.ToString();
                    gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                    soDienThoai = selectedRow.Cells["SoDienThoai"].Value.ToString();
                    cccd = selectedRow.Cells["CCCD"].Value.ToString();
                    ngayTao = Convert.ToDateTime(selectedRow.Cells["NgayTao"].Value);
                    ghiChu = selectedRow.Cells["GhiChu"].Value.ToString();

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (string.IsNullOrEmpty(KhachHangID))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BUSKhachHang bus = new BUSKhachHang();
                string kq = bus.DeleteKhachHang(KhachHangID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin khách hàng {KhachHangID} - {hoTen} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachKhachHang();
                    cboGioiTinh.SelectedIndex = -1;
                }

                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgrDanhSachKH_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgrDanhSachKH.Rows[e.RowIndex];


            txtMaKhachHang.Text = row.Cells["KhachHangID"].Value.ToString();
            txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
            cboGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value.ToString();
            txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
            txtCCCD.Text = row.Cells["CCCD"].Value.ToString();
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            txtMaKhachHang.Enabled = false;
        }






        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiemKhachHang.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                BUSKhachHang bus = new BUSKhachHang();
                string keyword = txtTimKiemKhachHang.Text.Trim();
                var result = bus.GetKhachHangList()
                    .Where(kH => kH.HoTen.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                             || kH.KhachHangID.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                if (result.Count > 0)
                {

                    dgrDanhSachKH.DataSource = result;
                    dgrDanhSachKH.Columns["KhachHangID"].HeaderText = "Mã Khách Hàng";
                    dgrDanhSachKH.Columns["HoTen"].HeaderText = "Họ Tên";
                    dgrDanhSachKH.Columns["DiaChi"].HeaderText = "Địa Chỉ";
                    dgrDanhSachKH.Columns["GioiTinh"].HeaderText = "Giới Tính";
                    dgrDanhSachKH.Columns["SoDienThoai"].HeaderText = "Số Điện Thoại";
                    dgrDanhSachKH.Columns["CCCD"].HeaderText = "CCCD";
                    dgrDanhSachKH.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                    dgrDanhSachKH.Columns["GhiChu"].HeaderText = "Ghi Chú";
                    dgrDanhSachKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào phù hợp với từ khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtTimKiemKhachHang.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Tạo BUS hoặc DAL để gọi hàm generate ID
            BUSKhachHang bus = new BUSKhachHang();

            // Sinh mã khách hàng tự động
            string KhachHangID = bus.GenerateKhachHangID();

            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? string.Empty;
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            DateTime ngayTao = DateTime.Today;

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDienThoai) ||
                string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(gioiTinh) || string.IsNullOrEmpty(cccd))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHang kH = new KhachHang
            {
                KhachHangID = KhachHangID,
                HoTen = hoTen,
                DiaChi = diaChi,
                GioiTinh = gioiTinh,
                SoDienThoai = soDienThoai,
                CCCD = cccd,
                NgayTao = ngayTao,
                GhiChu = ghiChu
            };

            string result = bus.InsertKhachHang(kH);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm mới khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDanhSachKhachHang();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string KhachHangID = txtMaKhachHang.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? string.Empty;
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(soDienThoai) || string.IsNullOrEmpty(diaChi) || string.IsNullOrEmpty(gioiTinh) || string.IsNullOrEmpty(cccd))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            KhachHang kH = new KhachHang
            {
                KhachHangID = KhachHangID,
                HoTen = hoTen,
                DiaChi = diaChi,
                GioiTinh = gioiTinh,
                SoDienThoai = soDienThoai,
                CCCD = cccd,
                NgayTao = ngayTao,
                GhiChu = ghiChu

            };
            BUSKhachHang bus = new BUSKhachHang();
            string result = bus.UpdateKhachHang(kH);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachKhachHang();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string KhachHangID = txtMaKhachHang.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string gioiTinh = cboGioiTinh.SelectedItem?.ToString() ?? string.Empty;
            string soDienThoai = txtSoDienThoai.Text.Trim();
            string cccd = txtCCCD.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(KhachHangID))
            {
                if (dgrDanhSachKH.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgrDanhSachKH.SelectedRows[0];
                    KhachHangID = selectedRow.Cells["KhachHangID"].Value.ToString();
                    hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                    diaChi = selectedRow.Cells["DiaChi"].Value.ToString();
                    gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                    soDienThoai = selectedRow.Cells["SoDienThoai"].Value.ToString();
                    cccd = selectedRow.Cells["CCCD"].Value.ToString();
                    ngayTao = Convert.ToDateTime(selectedRow.Cells["NgayTao"].Value);
                    ghiChu = selectedRow.Cells["GhiChu"].Value.ToString();

                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (string.IsNullOrEmpty(KhachHangID))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BUSKhachHang bus = new BUSKhachHang();
                string kq = bus.DeleteKhachHang(KhachHangID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin khách hàng {KhachHangID} - {hoTen} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachKhachHang();
                    cboGioiTinh.SelectedIndex = -1;
                }

                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachKhachHang();
            cboGioiTinh.SelectedIndex = -1;
        }

        private void dgrDanhSachKH_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgrDanhSachKH.Rows[e.RowIndex];

                // Khách hàng ID
                if (row.Cells["KhachHangID"].Value != null)
                    txtMaKhachHang.Text = row.Cells["KhachHangID"].Value.ToString();
                else
                    txtMaKhachHang.Text = "";

                // Các trường khác
                txtHoTen.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
                cboGioiTinh.SelectedItem = row.Cells["GioiTinh"].Value?.ToString() ?? "";
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
                txtCCCD.Text = row.Cells["CCCD"].Value?.ToString() ?? "";
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString() ?? "";

                //// Ngày tạo an toàn
                //if (row.Cells["NgayTao"].Value != null && DateTime.TryParse(row.Cells["NgayTao"].Value.ToString(), out DateTime ngayTao))
                //{
                //    if (ngayTao >= dtpNgayTao.MinDate && ngayTao <= dtpNgayTao.MaxDate)
                //        dtpNgayTao.Value = ngayTao;
                //    else
                //        dtpNgayTao.Value = DateTime.Now;
                //}
                //else
                //{
                //    dtpNgayTao.Value = DateTime.Now;
                //}

                btnThem.Enabled = false;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMaKhachHang.Enabled = false;
            }
        }



        private void txtHoTen_Leave(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            if (!string.IsNullOrEmpty(hoTen))
            {
                if (!Regex.IsMatch(hoTen, @"^[\p{L}\s]+$"))
                {
                    MessageBox.Show("Họ tên không được chứa số hoặc ký tự đặc biệt.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtSoDienThoai_Leave(object sender, EventArgs e)
        {
            string sdt = txtSoDienThoai.Text.Trim();
            if (!string.IsNullOrEmpty(sdt))
            {
                // Kiểm tra có chứa ký tự không phải số
                if (!Regex.IsMatch(sdt, @"^\d+$"))
                {
                    MessageBox.Show("Số điện thoại chỉ được chứa chữ số.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Nếu toàn số thì mới kiểm tra định dạng chuẩn
                else if (!Regex.IsMatch(sdt, @"^0\d{9,10}$"))
                {
                    MessageBox.Show("Số điện thoại phải bắt đầu bằng 0 và có 10–11 số.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtCCCD_Leave(object sender, EventArgs e)
        {
            string cccd = txtCCCD.Text.Trim();
            if (!string.IsNullOrEmpty(cccd))
            {
                if (!Regex.IsMatch(cccd, @"^\d+$"))
                {
                    MessageBox.Show("CCCD chỉ được chứa chữ số.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (!Regex.IsMatch(cccd, @"^\d{12}$"))
                {
                    MessageBox.Show("CCCD phải có đúng 12 số.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        
    }
}
