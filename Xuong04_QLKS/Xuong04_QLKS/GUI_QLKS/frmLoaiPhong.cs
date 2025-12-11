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

namespace GUI_QLKS
{
    public partial class frmLoaiPhong : Form
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
            this.dtpNgayTao.ValueChanged += new System.EventHandler(this.dtpNgayTao_ValueChanged);

        }

        private void ClearForm()
        {
            btnThemLoaiP.Enabled = true;
            btnSuaLoaiP.Enabled = false;
            btnXoaLoaiP.Enabled = false;
            txtMaLoaiPhong.Clear();
            txtGhiChu.Clear();
            txtTenLoaiPhong.Clear();

            dtpNgayTao.MinDate = DateTime.Today;
            dtpNgayTao.MaxDate = DateTime.Today;
        }

        private void LoadDanhSachLoaiP()
        {
            BUS_LoaiPhong busLoaiP = new BUS_LoaiPhong();
            dgrDanhSachLoaiP.DataSource = null;
            dgrDanhSachLoaiP.DataSource = busLoaiP.GetLoaiPhongList();
            dgrDanhSachLoaiP.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
            dgrDanhSachLoaiP.Columns["TenLoaiPhong"].HeaderText = "Tên Loại Phòng";
            dgrDanhSachLoaiP.Columns["NgayTao"].HeaderText = "Ngày Tạo";
            dgrDanhSachLoaiP.Columns["TrangThai"].HeaderText = "Trạng Thái";
            dgrDanhSachLoaiP.Columns["GhiChu"].HeaderText = "Ghi Chú";

            dgrDanhSachLoaiP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        private void frmLoaiPhong_Resize(object sender, EventArgs e)
        {
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = 10;
        }

        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachLoaiP();

            dtpNgayTao.MinDate = DateTime.Today;
            dtpNgayTao.MaxDate = DateTime.Today;
        }



        private void btnThemLoaiP_Click_1(object sender, EventArgs e)
        {
            string maLoaiPhong = txtMaLoaiPhong.Text.Trim();
            string tenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            bool trangThai = rdbActive.Checked;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(tenLoaiPhong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin loại phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoaiPhong loaiP = new LoaiPhong
            {
                MaLoaiPhong = maLoaiPhong,
                TenLoaiPhong = tenLoaiPhong,
                NgayTao = ngayTao,
                TrangThai = trangThai,
                GhiChu = ghiChu
            };
            BUS_LoaiPhong bus = new BUS_LoaiPhong();
            string result = bus.InsertLoaiPhong(loaiP);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm thông tin thành công");
                ClearForm();
                LoadDanhSachLoaiP();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaLoaiP_Click_1(object sender, EventArgs e)
        {
            string maLoaiPhong = txtMaLoaiPhong.Text.Trim();
            string tenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();
            bool trangThai = rdbActive.Checked;

            if (string.IsNullOrEmpty(tenLoaiPhong))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin loại phòng.");
                return;
            }
            LoaiPhong loaiPhong = new LoaiPhong
            {
                MaLoaiPhong = maLoaiPhong,
                TenLoaiPhong = tenLoaiPhong,
                NgayTao = ngayTao,
                TrangThai = trangThai,
                GhiChu = ghiChu
            };
            BUS_LoaiPhong bus = new BUS_LoaiPhong();
            string result = bus.UpdateLoaiPhong(loaiPhong);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachLoaiP();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoaLoaiP_Click_1(object sender, EventArgs e)
        {
            string maLoaiPhong = txtMaLoaiPhong.Text.Trim();
            string tenLoaiPhong = txtTenLoaiPhong.Text.Trim();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();
            bool trangThai = rdbActive.Checked;

            if (string.IsNullOrEmpty(maLoaiPhong))
            {
                if (dgrDanhSachLoaiP.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgrDanhSachLoaiP.SelectedRows[0];
                    maLoaiPhong = selectedRow.Cells["MaLoaiPhong"].Value.ToString();
                    tenLoaiPhong = selectedRow.Cells["TenLoaiPhong"].Value.ToString();
                    ngayTao = Convert.ToDateTime(selectedRow.Cells["NgayTao"].Value);
                    trangThai = Convert.ToBoolean(selectedRow.Cells["TrangThai"].Value);
                    ghiChu = selectedRow.Cells["GhiChu"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thông tin loại phòng cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(maLoaiPhong))
            {
                MessageBox.Show("Vui lòng nhập mã loại phòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa loại phòng {maLoaiPhong} - {tenLoaiPhong}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUS_LoaiPhong bus = new BUS_LoaiPhong();
                string kq = bus.DeleteLoaiPhong(maLoaiPhong);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin loại phòng {maLoaiPhong} - {tenLoaiPhong} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachLoaiP();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMoiLoaiP_Click_1(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachLoaiP();
        }

        private void dgrDanhSachLoaiP_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgrDanhSachLoaiP.Rows[e.RowIndex];

            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaLoaiPhong.Text = row.Cells["MaLoaiPhong"].Value.ToString();
            txtTenLoaiPhong.Text = row.Cells["TenLoaiPhong"].Value.ToString();

            // ✅ Xử lý Ngày tạo trước khi gán vào dtp
            DateTime ngayTao = Convert.ToDateTime(row.Cells["NgayTao"].Value);
            if (ngayTao > dtpNgayTao.MaxDate)
            {
                dtpNgayTao.Value = dtpNgayTao.MaxDate; // Gán ngày hợp lệ
            }
            else if (ngayTao < dtpNgayTao.MinDate)
            {
                dtpNgayTao.Value = dtpNgayTao.MinDate;
            }
            else
            {
                dtpNgayTao.Value = ngayTao;
            }

            // Các dữ liệu còn lại
            rdbActive.Checked = Convert.ToBoolean(row.Cells["TrangThai"].Value);
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            // Bật nút "Sửa"
            btnThemLoaiP.Enabled = false;
            btnSuaLoaiP.Enabled = true;
            btnXoaLoaiP.Enabled = true;

            // Tắt chỉnh sửa mã thẻ
            txtMaLoaiPhong.Enabled = false;

        }

        private void btnTimKiemLoaiP_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiemLoaiP.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string timKiem = txtTimKiemLoaiP.Text.Trim();
            BUS_LoaiPhong busLoaiP = new BUS_LoaiPhong();
            List<LoaiPhong> ketQuaTimKiem = busLoaiP.GetLoaiPhongList()
                .Where(lp => lp.TenLoaiPhong.Contains(timKiem, StringComparison.OrdinalIgnoreCase) ||
                             lp.MaLoaiPhong.Contains(timKiem, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (ketQuaTimKiem.Count == 0)
            {
                MessageBox.Show("Không tìm thấy loại phòng nào phù hợp với từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgrDanhSachLoaiP.DataSource = ketQuaTimKiem;
                dgrDanhSachLoaiP.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
                dgrDanhSachLoaiP.Columns["TenLoaiPhong"].HeaderText = "Tên Loại Phòng";
                dgrDanhSachLoaiP.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                dgrDanhSachLoaiP.Columns["TrangThai"].HeaderText = "Trạng Thái";
                dgrDanhSachLoaiP.Columns["GhiChu"].HeaderText = "Ghi Chú";
                dgrDanhSachLoaiP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            txtTimKiemLoaiP.Clear();
        }

        private void dtpNgayTao_ValueChanged(object sender, EventArgs e)
        {
            //if (dtpNgayTao.Value.Date > DateTime.Today)
            //{
            //    MessageBox.Show("Ngày tạo không được vượt quá ngày hiện tại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dtpNgayTao.Value = DateTime.Today;
            //}
        }
    }
}
