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
using DAL_QLKS;
using DTO_QLKS;
using Microsoft.IdentityModel.Tokens;


namespace GUI_QLKS
{
    public partial class frmQLNhanVien : Form
    {
        BUSNhanVien busNhanVien = new BUSNhanVien();
        DALNhanVien dal = new DALNhanVien();
        public frmQLNhanVien()
        {
            InitializeComponent();
        }


        private void frmQLNhanVien_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhanVien();
            txtMaNV.Text = dal.generateMaNhanVien();
        }
        private void LoadDanhSachNhanVien()
        {

            dgvNhanVien.DataSource = null;

            dgvNhanVien.DataSource = busNhanVien.GetNhanVienList();
            dgvNhanVien.Columns["MaNV"].HeaderText = "Mã Nhân Viên";
            dgvNhanVien.Columns["HoTen"].HeaderText = "Họ Tên";
            dgvNhanVien.Columns["Email"].HeaderText = "Email";
            dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvNhanVien.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dgvNhanVien.Columns["VaiTro"].HeaderText = "Vai Trò";
            dgvNhanVien.Columns["TinhTrangText"].HeaderText = "Trạng Thái";
            dgvNhanVien.Columns["TinhTrang"].Visible = false;
            dgvNhanVien.Columns["MatKhau"].Visible = true;

            dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ClearForm()
        {
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = true;
            txtMaNV.Clear();
            txtTenNhanVien.Clear();
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMatKhau.Clear();
            rdoHoatDong.Checked = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtTenNhanVien.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string diachi = txtDiaChi.Text.Trim();
            string gioitinh = cboGioiTinh.Text.Trim();
            string vaiTro = cboVaiTro.Text.Trim();


            bool trangThai;

            if (rdoHoatDong.Checked)
            {
                trangThai = true;
            }
            else
            {
                trangThai = false;
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(diachi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNV = maNV,
                HoTen = hoTen,
                Email = email,
                MatKhau = matKhau,
                DiaChi = diachi,
                GioiTinh = gioitinh,
                VaiTro = vaiTro,
                TinhTrang = trangThai
            };
            string result = busNhanVien.InsertNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm thông tin thành công");
                ClearForm();
                LoadDanhSachNhanVien();
                txtMaNV.Text = dal.generateMaNhanVien();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text.Trim();
            string hoTen = txtTenNhanVien.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();
            string diachi = txtDiaChi.Text.Trim();
            string gioitinh = cboGioiTinh.Text.Trim();
            string vaiTro = cboVaiTro.Text.Trim();
            bool trangThai;

            if (rdoHoatDong.Checked)
            {
                trangThai = true;
            }
            else
            {
                trangThai = false;
            }
            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(diachi))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin nhân viên.");
                return;
            }

            NhanVien nv = new NhanVien
            {
                MaNV = maNV,
                HoTen = hoTen,
                Email = email,
                MatKhau = matKhau,
                DiaChi = diachi,
                GioiTinh = gioitinh,
                VaiTro = vaiTro,
                TinhTrang = trangThai
            };
            string result = busNhanVien.UpdateNhanVien(nv);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật thông tin thành công");
                ClearForm();
                LoadDanhSachNhanVien();
            }
            else
            {
                MessageBox.Show(result);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string MaNV = txtMaNV.Text.Trim();
            string name = txtTenNhanVien.Text.Trim();
            if (string.IsNullOrEmpty(MaNV))
            {
                if (dgvNhanVien.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];
                    MaNV = selectedRow.Cells["MaNV"].Value.ToString();
                    name = selectedRow.Cells["HoTen"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (string.IsNullOrEmpty(MaNV))
            {
                MessageBox.Show("Xóa không thành công.");
                return;
            }

            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên {MaNV} - {name}?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                BUSNhanVien bus = new BUSNhanVien();
                string kq = bus.DeleteNhanVien(MaNV);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa thông tin nhân viên {MaNV} - {name} thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDanhSachNhanVien();
                }
                else
                {
                    MessageBox.Show(kq, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachNhanVien();
        }

        private void dgvNhanVien_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
            // Đổ dữ liệu vào các ô nhập liệu trên form
            txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
            txtTenNhanVien.Text = row.Cells["HoTen"].Value.ToString();
            txtEmail.Text = row.Cells["Email"].Value.ToString();
            txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();


            bool trangThai = Convert.ToBoolean(row.Cells["TinhTrang"].Value);
            if (trangThai == false)
            {
                rdoKhongHoatDong.Checked = true;
            }
            else
            {
                rdoHoatDong.Checked = true;
            }

            cboGioiTinh.SelectedIndex = cboGioiTinh.FindStringExact(row.Cells["GioiTinh"].Value.ToString());
            cboVaiTro.SelectedIndex = cboVaiTro.FindStringExact(row.Cells["VaiTro"].Value.ToString());

            // Bật nút "Sửa"
            btnThem.Enabled = false;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            // Tắt chỉnh sửa mã nhân viên
            txtMaNV.Enabled = false;
        }

        private void txtTenNhanVien_Leave(object sender, EventArgs e)
        {
            string hoTen = txtTenNhanVien.Text.Trim();

            // Chỉ kiểm tra nếu không rỗng
            if (!string.IsNullOrEmpty(hoTen))
            {
                // Chỉ cho phép chữ và khoảng trắng
                if (!System.Text.RegularExpressions.Regex.IsMatch(hoTen, @"^[\p{L}\s]+$"))
                {
                    MessageBox.Show("Họ tên không được chứa số hoặc ký tự đặc biệt.",
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();

            // Chỉ kiểm tra nếu không rỗng
            if (!string.IsNullOrEmpty(email))
            {
                // Kiểm tra email cơ bản
                if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Email không hợp lệ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Giữ focus lại mà không bị kẹt
                    this.BeginInvoke(new Action(() => txtEmail.Focus()));
                }
            }
        }
    }
}
