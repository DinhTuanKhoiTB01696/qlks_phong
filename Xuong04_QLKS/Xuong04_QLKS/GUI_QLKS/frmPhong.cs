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
    public partial class frmPhong : Form
    {
        public frmPhong()
        {
            InitializeComponent();
        }

        private void frmPhong_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadLoaiPhong();
            LoadDanhSachPhong();
            cboLoaiPhong.SelectedIndex = -1;

            //dtpNgayTao.MinDate = DateTimePicker.MinimumDateTime;
            //dtpNgayTao.MaxDate = DateTimePicker.MaximumDateTime;

            //dtpNgayTao.MinDate = DateTime.Today;
            //dtpNgayTao.MaxDate = DateTime.Today;

            dtpNgayTao.Value = DateTime.Today;
        }


        private void ClearForm()
        {
            btnThemP.Enabled = true;
            btnSuaP.Enabled = false;
            btnXoaP.Enabled = false;
            txtPhongID.Clear();
            txtTenPhong.Clear();
            dtpNgayTao.Value = DateTime.Now;
            txtGhiChu.Clear();
            txtGiaPhong.Clear();
            rdbActive.Checked = true;
            rdbDeActive.Checked = false;


            //dtpNgayTao.MinDate = DateTime.Today;
            //dtpNgayTao.MaxDate = DateTime.Today;
        }

        private void LoadLoaiPhong()
        {
            try
            {
                BUS_LoaiPhong busLoaiPhong = new BUS_LoaiPhong();
                List<LoaiPhong> dsLoai = busLoaiPhong.GetLoaiPhongList();
                cboLoaiPhong.DataSource = dsLoai;
                cboLoaiPhong.ValueMember = "MaLoaiPhong";
                cboLoaiPhong.DisplayMember = "TenLoaiPhong";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại phòng" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDanhSachPhong()
        {
            try
            {
                BUS_Phong busPhong = new BUS_Phong();
                dgrDanhSachP.DataSource = null;
                dgrDanhSachP.DataSource = busPhong.GetPhongList();
                dgrDanhSachP.Columns["PhongID"].HeaderText = "Mã Phòng";
                dgrDanhSachP.Columns["TenPhong"].HeaderText = "Tên Phòng";
                dgrDanhSachP.Columns["MaLoaiPhong"].HeaderText = "Mã Loại";
                dgrDanhSachP.Columns["GiaPhong"].HeaderText = "Giá Phòng";
                dgrDanhSachP.Columns["GiaPhong"].DefaultCellStyle.Format = "N0";
                dgrDanhSachP.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                dgrDanhSachP.Columns["TinhTrang"].HeaderText = "Tình Trạng";
                dgrDanhSachP.Columns["GhiChu"].HeaderText = "Ghi Chú";

                dgrDanhSachP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnThemP_Click_1(object sender, EventArgs e)
        {
            dtpNgayTao.Value = DateTime.Today;

            // Kiểm tra giá trị nhập vào txtGiaPhong
            decimal giaPhong = 0;
            string giaPhongText = txtGiaPhong.Text.Trim();

            if (!string.IsNullOrWhiteSpace(giaPhongText))
            {
                if (!decimal.TryParse(giaPhongText, out giaPhong))
                {
                    MessageBox.Show("Giá phòng phải là số hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Định dạng lại hiển thị giá phòng sau khi kiểm tra
            txtGiaPhong.Text = giaPhong.ToString("N0");

            string phongID = txtPhongID.Text.Trim();
            string tenPhong = txtTenPhong.Text.Trim();
            string maLoaiPhong = cboLoaiPhong.SelectedValue?.ToString() ?? "";

            DateTime ngayTao = DateTime.Today;
            bool tinhTrang = rdbActive.Checked;
            string ghiChu = txtGhiChu.Text.Trim();

            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(tenPhong) || string.IsNullOrEmpty(maLoaiPhong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Khởi tạo đối tượng phòng
            Phong phong = new Phong
            {
                PhongID = phongID,
                TenPhong = tenPhong,
                //MaLoaiPhong = maLoaiPhong,
                GiaPhong = giaPhong,
                NgayTao = ngayTao,
                TinhTrang = tinhTrang,
                GhiChu = ghiChu
            };

            // Gọi BUS xử lý
            BUS_Phong bus = new BUS_Phong();
            string result = bus.InsertPhong(phong);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm mới phòng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadDanhSachPhong();
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaP_Click_1(object sender, EventArgs e)
        {
            string phongID = txtPhongID.Text.Trim();
            string tenPhong = txtTenPhong.Text.Trim();
            string maLoaiPhong = cboLoaiPhong.SelectedValue.ToString();
            decimal giaPhong = txtGiaPhong.Text.Trim() == "" ? 0 : decimal.Parse(txtGiaPhong.Text.Trim());
            txtGiaPhong.Text = giaPhong.ToString("N0");
            DateTime ngayTao = dtpNgayTao.Value.Date;
            bool tinhTrang = rdbActive.Checked;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(tenPhong) || string.IsNullOrEmpty(giaPhong.ToString())) /*|| string.IsNullOrEmpty(maLoaiPhong))*/
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Phong phong = new Phong
            {
                PhongID = phongID,
                TenPhong = tenPhong,
                MaLoaiPhong = maLoaiPhong,
                GiaPhong = giaPhong,
                NgayTao = ngayTao,
                TinhTrang = tinhTrang,
                GhiChu = ghiChu
            };
            BUS_Phong bus = new BUS_Phong();
            string result = bus.UpdatePhong(phong);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachPhong();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoaP_Click_1(object sender, EventArgs e)
        {
            string phongID = txtPhongID.Text.Trim();
            string tenPhong = txtTenPhong.Text.Trim();
            string maLoaiPhong = cboLoaiPhong.SelectedValue.ToString();
            decimal giaPhong = txtGiaPhong.Text.Trim() == "" ? 0 : decimal.Parse(txtGiaPhong.Text.Trim());
            txtGiaPhong.Text = giaPhong.ToString("N0");
            DateTime ngayTao = dtpNgayTao.Value.Date;
            bool tinhTrang = rdbActive.Checked;
            string ghiChu = txtGhiChu.Text.Trim();

            if (string.IsNullOrEmpty(phongID))
            {
                if (dgrDanhSachP.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgrDanhSachP.SelectedRows[0];
                    phongID = selectedRow.Cells["PhongID"].Value.ToString();
                    tenPhong = selectedRow.Cells["TenPhong"].Value.ToString();
                    maLoaiPhong = selectedRow.Cells["MaLoaiPhong"].Value.ToString();
                    giaPhong = Convert.ToDecimal(selectedRow.Cells["GiaPhong"].Value);
                    ngayTao = Convert.ToDateTime(selectedRow.Cells["NgayTao"].Value);
                    tinhTrang = Convert.ToBoolean(selectedRow.Cells["TinhTrang"].Value);
                    ghiChu = selectedRow.Cells["GhiChu"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn phòng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (string.IsNullOrEmpty(phongID))
            {
                MessageBox.Show("Vui lòng nhập ID phòng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phòng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BUS_Phong bus = new BUS_Phong();
                string kq = bus.DeletePhong(phongID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin loại phòng {phongID} - {tenPhong} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachPhong();
                    cboLoaiPhong.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMoiP_Click_1(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachPhong();
            cboLoaiPhong.SelectedIndex = -1;
        }

        private void btnTimKiemP_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiemP.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                BUS_Phong bus = new BUS_Phong();
                string keyword = txtTimKiemP.Text.Trim();
                var result = bus.GetPhongList()
                    .Where(p => p.TenPhong.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
                             || p.PhongID.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();
                if (result.Count > 0)
                {
                    dgrDanhSachP.DataSource = result;
                    dgrDanhSachP.Columns["PhongID"].HeaderText = "Mã Phòng";
                    dgrDanhSachP.Columns["TenPhong"].HeaderText = "Tên Phòng";
                    dgrDanhSachP.Columns["MaLoaiPhong"].HeaderText = "Mã Loại Phòng";
                    dgrDanhSachP.Columns["GiaPhong"].HeaderText = "Giá Phòng";
                    dgrDanhSachP.Columns["GiaPhong"].DefaultCellStyle.Format = "N0";
                    dgrDanhSachP.Columns["NgayTao"].HeaderText = "Ngày Tạo";
                    dgrDanhSachP.Columns["TinhTrang"].HeaderText = "Tình Trạng";
                    dgrDanhSachP.Columns["GhiChu"].HeaderText = "Ghi Chú";
                    dgrDanhSachP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phòng nào phù hợp với từ khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                txtTimKiemP.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgrDanhSachP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgrDanhSachP.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtPhongID.Text = row.Cells["PhongID"].Value.ToString();
            txtTenPhong.Text = row.Cells["TenPhong"].Value.ToString();
            cboLoaiPhong.SelectedValue = row.Cells["MaLoaiPhong"].Value.ToString();
            txtGiaPhong.Text = Convert.ToDecimal(row.Cells["GiaPhong"].Value).ToString("N0");
            dtpNgayTao.Value = Convert.ToDateTime(row.Cells["NgayTao"].Value);

            rdbActive.Checked = Convert.ToBoolean(row.Cells["TinhTrang"].Value);
            txtGhiChu.Text = row.Cells["GhiChu"].Value.ToString();

            // Bật nút "Sửa"
            btnThemP.Enabled = false;
            btnSuaP.Enabled = true;
            btnXoaP.Enabled = true;
            // Tắt chỉnh sửa mã thẻ
            txtPhongID.Enabled = false;
        }

        private void dtpNgayTao_ValueChanged(object sender, EventArgs e)
        {
        }
    }
}



