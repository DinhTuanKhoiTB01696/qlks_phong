using BLL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DAL_QLKS; // 💡 Thêm để dùng generateLoaiDichVuID()

namespace GUI_QLKS
{
    public partial class frmLoaiDichVu : Form
    {
        public frmLoaiDichVu()
        {
            InitializeComponent();
        }

        private void frmLoaiDichVu_Load(object sender, EventArgs e)
        {
            LoadDanhSachLoaiDichVu();
            ClearForm(); // ✅ Gọi ClearForm khi load
            txtMaDichVu.ReadOnly = true; // ✅ Không cho sửa mã
        }

        private void LoadDanhSachLoaiDichVu()
        {
            try
            {
                BUSLoaiDichVu busLoaiDV = new BUSLoaiDichVu();
                dgvLoaiDichVu.DataSource = null;
                dgvLoaiDichVu.DataSource = busLoaiDV.GetLoaiDichVuList();

                var columns = dgvLoaiDichVu.Columns;
                if (columns["LoaiDichVuID"] != null) columns["LoaiDichVuID"].HeaderText = "Mã Loại Dịch Vụ";
                if (columns["TenDichVu"] != null) columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
                if (columns["GiaDichVu"] != null) columns["GiaDichVu"].HeaderText = "Giá Dịch Vụ";
                if (columns["DonViTinh"] != null) columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
                if (columns["NgayTao"] != null) columns["NgayTao"].HeaderText = "Ngày Tạo";
                if (columns["TrangThai"] != null) columns["TrangThai"].HeaderText = "Trạng Thái";
                if (columns["GhiChu"] != null) columns["GhiChu"].HeaderText = "Ghi Chú";

                dgvLoaiDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void ClearForm()
        {
            DALLoaiDichVu dal = new DALLoaiDichVu();
            txtMaDichVu.Text = dal.generateLoaiDichVuID(); // ✅ Sinh mã mới

            txtTenDV.Clear();
            txtGiaDV.Clear();
            txtDonVi.Clear();
            txtGhiChu.Clear();
            rdoHoatDong.Checked = true;
            rdoKhongHoatDong.Checked = false;

            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            dgvLoaiDichVu.ClearSelection();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string loaiDichVuID = txtMaDichVu.Text.Trim(); // đã tự sinh
            string tenDichVu = txtTenDV.Text.Trim();
            string giaDichVuText = txtGiaDV.Text.Trim();
            string donViTinh = txtDonVi.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            bool trangThai = rdoHoatDong.Checked;

            if (!decimal.TryParse(giaDichVuText, out decimal giaDichVu))
            {
                MessageBox.Show("Giá dịch vụ không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoaiDichVu ldv = new LoaiDichVu
            {
                LoaiDichVuID = loaiDichVuID,
                TenDichVu = tenDichVu,
                GiaDichVu = giaDichVu,
                DonViTinh = donViTinh,
                TrangThai = trangThai,
                GhiChu = ghiChu,
                NgayTao = DateTime.Now
            };

            BUSLoaiDichVu bus = new BUSLoaiDichVu();
            string result = bus.InsertLoaiDichVu(ldv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm mới loại dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachLoaiDichVu();
                ClearForm(); // ✅ Reset sau khi thêm
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string loaiDichVuID = txtMaDichVu.Text.Trim();
            string tenDichVu = txtTenDV.Text.Trim();
            string giaDichVuText = txtGiaDV.Text.Trim();
            string donViTinh = txtDonVi.Text.Trim();
            string ghiChu = txtGhiChu.Text.Trim();
            bool trangThai = rdoHoatDong.Checked;

            if (!decimal.TryParse(giaDichVuText, out decimal giaDichVu))
            {
                MessageBox.Show("Giá dịch vụ không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoaiDichVu ldv = new LoaiDichVu
            {
                LoaiDichVuID = loaiDichVuID,
                TenDichVu = tenDichVu,
                GiaDichVu = giaDichVu,
                DonViTinh = donViTinh,
                TrangThai = trangThai,
                GhiChu = ghiChu,
                NgayTao = DateTime.Now
            };

            BUSLoaiDichVu bus = new BUSLoaiDichVu();
            string result = bus.UpdateLoaiDichVu(ldv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật loại dịch vụ thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSachLoaiDichVu();
                ClearForm();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string loaiDichVuID = txtMaDichVu.Text.Trim();

            if (string.IsNullOrEmpty(loaiDichVuID))
            {
                MessageBox.Show("Vui lòng chọn loại dịch vụ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại dịch vụ này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BUSLoaiDichVu bus = new BUSLoaiDichVu();
                string kq = bus.DeleteLoaiDichVu(loaiDichVuID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa loại dịch vụ {loaiDichVuID} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachLoaiDichVu();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachLoaiDichVu();
            ClearForm();
        }

        private void dgvLoaiDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLoaiDichVu.Rows[e.RowIndex];

                txtMaDichVu.Text = row.Cells["LoaiDichVuID"].Value?.ToString();
                txtTenDV.Text = row.Cells["TenDichVu"].Value?.ToString();
                txtGiaDV.Text = row.Cells["GiaDichVu"].Value?.ToString();
                txtDonVi.Text = row.Cells["DonViTinh"].Value?.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();

                if (bool.TryParse(row.Cells["TrangThai"].Value?.ToString(), out bool trangThai))
                {
                    rdoHoatDong.Checked = trangThai;
                    rdoKhongHoatDong.Checked = !trangThai;
                }
                else
                {
                    rdoHoatDong.Checked = false;
                    rdoKhongHoatDong.Checked = true;
                }

                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false; // Không cho thêm trùng
            }
        }
    }
}
