using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO_QLKS;
using BLL_QLKS;
using DAL_QLKS;


namespace GUI_QLKS
{
    public partial class frmThanhToanHoaDon : Form
    {

        private BLLHoaDonThanhToan _bllHoaDon = new BLLHoaDonThanhToan();
        private BUSDatPhong _bllDatPhong = new BUSDatPhong();
        private BLLChiTietDichVu _bllChiTietDV = new BLLChiTietDichVu();
        private string currentMode = "add";
        private string phongID;
        public frmThanhToanHoaDon()
        {
            InitializeComponent();
            LoadHoaDonThue();
            LoadPhuongThucThanhToan();
            LoadDanhSachHoaDonThanhToan();
            cbxHoaDonThueID.SelectedIndexChanged += cbxHoaDonThueID_SelectedIndexChanged_1;

            // Chỉ cho chọn ngày hôm nay
            dtpNgayTT.MinDate = DateTime.Today;
            dtpNgayTT.MaxDate = DateTime.Today;
            dtpNgayTT.Value = DateTime.Today;

            txtHoaDonID.Text = _bllHoaDon.TaoMaHoaDonMoi();
            txtTenKH.Enabled = false;
            txtPhong.Enabled = false;
            txtGiaPhong.Enabled = false;
            dtpTuNgay.Enabled = false;
            dtpDenNgay.Enabled = false;

            this.ActiveControl = btnLuu;

            var bllPhong = new BUS_Phong();
            bllPhong.UpdateTinhTrangPhong(phongID, false);
        }



        private void LoadHoaDonThue()
        {
            try
            {
                var ds = _bllHoaDon.GetAllDatPhongChuaThanhToan();

                if (ds == null || ds.Count == 0)
                {
                    MessageBox.Show("Không có hóa đơn thuê nào chưa thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbxHoaDonThueID.DataSource = null;
                    return;
                }

                cbxHoaDonThueID.DataSource = ds;
                cbxHoaDonThueID.DisplayMember = "HoaDonThueID";
                cbxHoaDonThueID.ValueMember = "HoaDonThueID";
                cbxHoaDonThueID.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load hóa đơn thuê: " + ex.Message);
            }
        }


        private void LoadPhuongThucThanhToan()
        {
            cbxPTTT.Items.Clear();
            cbxPTTT.Items.AddRange(new object[] { "Tiền mặt", "Chuyển khoản", "Momo", "ZaloPay", "Visa" });
            cbxPTTT.SelectedIndex = 0;
        }


        private void LoadDanhSachHoaDonThanhToan()
        {
            try
            {
                var dsHoaDon = _bllHoaDon.GetAll();

                if (dsHoaDon == null || dsHoaDon.Count == 0)
                {
                    dgvHoaDonThanhToan.DataSource = null;
                    return;
                }

                // Xóa cấu hình cũ
                dgvHoaDonThanhToan.AutoGenerateColumns = false;
                dgvHoaDonThanhToan.Columns.Clear();

                // Gán data source
                dgvHoaDonThanhToan.DataSource = dsHoaDon;

                // Cột Mã HĐ
                dgvHoaDonThanhToan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "HoaDonID",
                    Name = "HoaDonID",     // 👈 Thêm Name
                    HeaderText = "Mã HĐ",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                // Cột Mã HĐ Thuê
                dgvHoaDonThanhToan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "HoaDonThueID",
                    Name = "HoaDonThueID",   // 👈 Thêm Name
                    HeaderText = "Mã HĐ Thuê",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                // Cột Ngày thanh toán
                dgvHoaDonThanhToan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NgayThanhToan",
                    Name = "NgayThanhToan",
                    HeaderText = "Ngày Thanh Toán",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                    DefaultCellStyle = { Format = "dd/MM/yyyy" }
                });

                // Cột Phương thức thanh toán
                dgvHoaDonThanhToan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "PhuongThucThanhToan",
                    Name = "PhuongThucThanhToan",
                    HeaderText = "Phương Thức TT",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                // Cột Ghi chú
                dgvHoaDonThanhToan.Columns.Add(new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "GhiChu",
                    Name = "GhiChu",
                    HeaderText = "Ghi Chú",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                });

                // Cột Trạng Thái (checkbox)
                var colTrangThai = new DataGridViewCheckBoxColumn
                {
                    DataPropertyName = "TrangThai",
                    Name = "TrangThai",  // 👈 Thêm Name
                    HeaderText = "Trạng Thái",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
                };
                dgvHoaDonThanhToan.Columns.Add(colTrangThai);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu hóa đơn thanh toán: " + ex.Message);
            }


        }




        private void LoadChiTietDichVu(string hoaDonThueID)
        {
            var dsCTDV = _bllChiTietDV.GetChiTietByHoaDonThueID(hoaDonThueID);

            dgvChiTietDichVu.AutoGenerateColumns = false;
            dgvChiTietDichVu.Columns.Clear();

            if (dsCTDV == null || dsCTDV.Count == 0)
            {
                dgvChiTietDichVu.DataSource = null;
                lblTienDV.Text = "0";
                lblTongTien.Text = lblTienPhong.Text; // chỉ có tiền phòng
                return;
            }

            // Gán DataSource
            dgvChiTietDichVu.DataSource = dsCTDV;

            // Thêm cột
            dgvChiTietDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ChiTietDichVuID", Name = "ChiTietDichVuID", HeaderText = "Mã CTDV" });
            dgvChiTietDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DichVuID", Name = "DichVuID", HeaderText = "Mã Dịch Vụ" });
            dgvChiTietDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoaiDichVuID", Name = "LoaiDichVuID", HeaderText = "Loại DV" });
            dgvChiTietDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoLuong", Name = "SoLuong", HeaderText = "Số Lượng" });
            dgvChiTietDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DonGia", Name = "DonGia", HeaderText = "Đơn Giá" });
            dgvChiTietDichVu.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThanhTien", Name = "ThanhTien", HeaderText = "Thành Tiền" });

            dgvChiTietDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tính tổng tiền dịch vụ
            decimal tongDV = dsCTDV.Sum(d => d.ThanhTien);
            lblTienDV.Text = tongDV.ToString("N0");

            // Tính tổng thanh toán = tiền phòng + tiền dịch vụ
            decimal tienPhong = 0;
            decimal.TryParse(lblTienPhong.Text, out tienPhong);
            lblTongTien.Text = (tienPhong + tongDV).ToString("N0");
        }








        private BUSDatPhong busDatPhong = new BUSDatPhong();

        private void LoadThongTinDatPhong(string hoaDonThueID)
        {
            var datPhongView = busDatPhong.GetDatPhongViewByID(hoaDonThueID);
            if (datPhongView != null)
            {
                txtTenKH.Text = datPhongView.TenKH;
                txtPhong.Text = datPhongView.TenPhong;
                txtGiaPhong.Text = datPhongView.GiaPhong.ToString("N0");
                dtpTuNgay.Value = datPhongView.NgayDen;
                dtpDenNgay.Value = datPhongView.NgayDi;

                int soNgay = (int)(datPhongView.NgayDi - datPhongView.NgayDen).TotalDays;
                if (soNgay <= 0) soNgay = 1;

                lblTienPhong.Text = (soNgay * datPhongView.GiaPhong).ToString("N0");
            }
        }
        private void cbxHoaDonThueID_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbxHoaDonThueID.SelectedIndex < 0) return;

            string hoaDonThueID = cbxHoaDonThueID.SelectedValue?.ToString();
            if (string.IsNullOrEmpty(hoaDonThueID)) return;

            // Load thông tin phòng + tổng tiền như trước...
            LoadThongTinDatPhong(hoaDonThueID);

            // Load chi tiết dịch vụ vào dgvChiTietDichVu (KHÔNG còn dgvDichVu)
            LoadChiTietDichVu(hoaDonThueID);
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            // --- Kiểm tra dữ liệu nhập ---
            if (string.IsNullOrWhiteSpace(txtHoaDonID.Text))
            {
                MessageBox.Show("Vui lòng nhập mã hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbxHoaDonThueID.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn thuê!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rdoDaThanhToan.Checked && !rdoChuaThanhToan.Checked)
            {
                MessageBox.Show("Vui lòng chọn trạng thái thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- Tạo đối tượng hóa đơn ---
            HoaDonThanhToan hd = new HoaDonThanhToan
            {
                HoaDonID = txtHoaDonID.Text.Trim(),
                HoaDonThueID = cbxHoaDonThueID.SelectedValue.ToString(),
                NgayThanhToan = dtpNgayTT.Value,
                PhuongThucThanhToan = cbxPTTT.Text,
                GhiChu = txtGhiChu.Text.Trim(),
                TrangThai = rdoDaThanhToan.Checked ? 1 : 0
            };

            bool result = false;

            try
            {
                if (currentMode == "add")
                {
                    result = _bllHoaDon.ThemHoaDon(hd);
                }
                else
                {
                    result = _bllHoaDon.CapNhatHoaDon(hd);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu hóa đơn: " + ex.Message);
                return;
            }

            if (result)
            {
                MessageBox.Show("Lưu hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // --- Load lại danh sách từ DB ---
                LoadDanhSachHoaDonThanhToan();

                // Sau khi lưu thành công thì luôn đưa về chế độ add và tạo mã mới
                currentMode = "add";
                btnLamMoi.PerformClick();
            }
            else
            {
                MessageBox.Show("Lưu hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in chưa được triển khai!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // TODO: In hoặc xuất báo cáo
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            //// Reset form về trạng thái thêm mới
            //txtHoaDonID.Text = _bllHoaDon.TaoMaHoaDonMoi();
            //cbxHoaDonThueID.SelectedIndex = -1;
            //txtGhiChu.Clear();
            //dtpNgayTT.Value = DateTime.Today;
            //cbxPTTT.SelectedIndex = 0;
            //txtTenKH.Clear();
            //txtPhong.Clear();
            //dtpTuNgay.Value = DateTime.Today;
            //dtpDenNgay.Value = DateTime.Today;
            //txtGiaPhong.Clear();
            //lblTienPhong.Text = "0";
            //lblTienDV.Text = "0";
            //lblTongTien.Text = "0";
            //dgvChiTietDichVu.DataSource = null;

            //// Chỉ tại đây mới reset chế độ về add
            //currentMode = "add";



            // Nếu đang ở chế độ thêm mới thì tạo mã mới
            if (currentMode == "add")
            {
                txtHoaDonID.Text = _bllHoaDon.TaoMaHoaDonMoi();
            }

            cbxHoaDonThueID.SelectedIndex = -1;
            txtGhiChu.Clear();
            dtpNgayTT.Value = DateTime.Today;
            cbxPTTT.SelectedIndex = 0;
            txtTenKH.Clear();
            txtPhong.Clear();
            dtpTuNgay.Value = DateTime.Today;
            dtpDenNgay.Value = DateTime.Today;
            txtGiaPhong.Clear();
            lblTienPhong.Text = "0";
            lblTienDV.Text = "0";
            lblTongTien.Text = "0";
            dgvChiTietDichVu.DataSource = null;

            // Luôn đưa về trạng thái thêm mới khi bấm nút này
            currentMode = "add";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void dgvDichVu_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null)
            {
                dgvDichVu.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = e.Value.ToString();
            }
        }

        private void dgvHoaDonThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHoaDonThanhToan.Rows[e.RowIndex];

                // Mã hóa đơn
                txtHoaDonID.Text = row.Cells["HoaDonID"]?.Value?.ToString();

                // Gán SelectedValue cho combobox (nếu có binding)
                if (row.Cells["HoaDonThueID"]?.Value != null)
                {
                    try
                    {
                        cbxHoaDonThueID.SelectedValue = row.Cells["HoaDonThueID"].Value;
                    }
                    catch
                    {
                        // Trường hợp không tìm thấy giá trị trong combobox
                        cbxHoaDonThueID.SelectedIndex = -1;
                    }
                }
                else
                {
                    cbxHoaDonThueID.SelectedIndex = -1;
                }

                // Ngày thanh toán
                if (DateTime.TryParse(row.Cells["NgayThanhToan"]?.Value?.ToString(), out DateTime ngayThanhToan))
                {
                    if (ngayThanhToan < dtpNgayTT.MinDate || ngayThanhToan > dtpNgayTT.MaxDate)
                        dtpNgayTT.Value = DateTime.Today;
                    else
                        dtpNgayTT.Value = ngayThanhToan;
                }
                else
                {
                    dtpNgayTT.Value = DateTime.Today;
                }

                // Phương thức thanh toán
                cbxPTTT.Text = row.Cells["PhuongThucThanhToan"]?.Value?.ToString();

                // Ghi chú
                txtGhiChu.Text = row.Cells["GhiChu"]?.Value?.ToString();

                // Chuyển sang chế độ cập nhật
                currentMode = "update";

                // Load thông tin chi tiết
                string hoaDonThueID = row.Cells["HoaDonThueID"]?.Value?.ToString();
                if (!string.IsNullOrEmpty(hoaDonThueID))
                {
                    LoadThongTinDatPhong(hoaDonThueID);
                    LoadChiTietDichVu(hoaDonThueID);
                }
            }
        }

        private void rdoDaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoDaThanhToan.Checked && currentMode == "update")
            {
                _bllHoaDon.CapNhatTrangThai(txtHoaDonID.Text, 1);
                LoadDanhSachHoaDonThanhToan();
            }
        }

        private void rdoChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoChuaThanhToan.Checked && currentMode == "update")
            {
                _bllHoaDon.CapNhatTrangThai(txtHoaDonID.Text, 0);
                LoadDanhSachHoaDonThanhToan();
            }
        }

        private void dgvChiTietDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvChiTietDichVu.Rows[e.RowIndex].Cells["ChiTietDichVuID"].Value != null)
            {
                string maCTDV = dgvChiTietDichVu.Rows[e.RowIndex].Cells["ChiTietDichVuID"].Value.ToString();
                MessageBox.Show($"Chi tiết dịch vụ: {maCTDV}");
            }
        }

        private void dtpNgayTT_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;

            // Nếu ngày chọn khác hôm nay thì tự động set lại hôm nay
            if (dtpNgayTT.Value.Date != today)
            {
                MessageBox.Show("Ngày thanh toán phải là ngày hôm nay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayTT.Value = today;
            }
        }

        private void txtTenKH_Enter(object sender, EventArgs e)
        {
            btnLuu.Focus();
        }


        private void CapNhatTrangThaiPhongSauTra(string phongID)
        {
            var bllPhong = new BUS_Phong();
            bllPhong.UpdateTinhTrangPhong(phongID, false); // false = trả phòng
        }


    }
}
