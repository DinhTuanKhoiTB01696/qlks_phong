using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLKS;
using DAL_QLKS;
using DTO_QLKS;

namespace GUI_QLKS
{
    public partial class frmQLDichVu : Form
    {
        DALDatPhong dalDatPhong = new DALDatPhong();
        DALQLDichVu dalDichVu = new DALQLDichVu();
        BUSQLDichVu busDichVu = new BUSQLDichVu();

        private bool isProgrammaticallyChangingDate = false;
        private bool isDoubleClick = false;
        public frmQLDichVu()
        {
            InitializeComponent();
        }
        private void frmQLDichVu_Load(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachDichVu();
            LoadDatPhong();
            LoadMaDichVuMoi();
            this.dgvQLDichVu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQLDichVu_CellClick);


            dtpNgayTao.MinDate = DateTime.Today;
            dtpNgayTao.MaxDate = DateTime.Today;
        }
        private void LoadMaDichVuMoi()
        {
            txtDichVuID.Text = busDichVu.GenerateNextMaDichVu(); // hoặc gọi trực tiếp DAL nếu không dùng BUS
        }



        private void LoadDatPhong()
        {
            List<DatPhong> listDatPhong = dalDatPhong.selectAll();
            cboMaHoaDon.DataSource = listDatPhong;
            cboMaHoaDon.DisplayMember = "HoaDonThueID"; // Hiển thị mã hóa đơn thuê
            cboMaHoaDon.ValueMember = "HoaDonThueID"; // Giá trị của combobox là mã hóa đơn thuê
        }

        private void ClearForm()
        {
            txtDichVuID.Clear();
            cboMaHoaDon.Text = "";
            rdbDangThue.Checked = true; // mặc định là đang thuê
            txtGhiChu.Clear();
            btnThem_1.Enabled = true;
            btnSua_1.Enabled = true;
            btnXoa_1.Enabled = true;
            txtDichVuID.Enabled = true;

            dtpNgayTao.MinDate = DateTime.Today;
            dtpNgayTao.MaxDate = DateTime.Today;
            //dtpNgayTao.Value = DateTime.Today;
        }


        private void LoadDanhSachDichVu()
        {
            try
            {
                BUSQLDichVu bus = new BUSQLDichVu();

                // Lấy danh sách dịch vụ 1 lần
                var danhSachDichVu = bus.GetDichVuList();

                // Gán vào DataGridView
                dgvQLDichVu.DataSource = null;
                dgvQLDichVu.DataSource = danhSachDichVu;

                var columns = dgvQLDichVu.Columns;

                columns["DichVuID"].HeaderText = "Mã Dịch Vụ";
                columns["HoaDonThueID"].HeaderText = "Mã Hóa Đơn Thuê"; // ← THÊM DÒNG NÀY
                columns["NgayTao"].HeaderText = "Ngày Tạo";
                columns["TrangThai"].HeaderText = "Trạng Thái";
                columns["GhiChu"].HeaderText = "Ghi Chú";

                dgvQLDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Thiết lập AutoComplete cho txtDichVuID
                AutoCompleteStringCollection autoSource = new AutoCompleteStringCollection();
                foreach (var dv in danhSachDichVu)
                {
                    autoSource.Add(dv.DichVuID);
                }

                txtDichVuID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtDichVuID.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtDichVuID.AutoCompleteCustomSource = autoSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string dichVuID = txtDichVuID.Text.Trim();
            string hoaDonThueID = cboMaHoaDon.SelectedValue.ToString();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();

            // Lấy trạng thái từ radio button
            bool trangThai = rdbDangThue.Checked;

            // Kiểm tra dữ liệu đầu vào cơ bản
            if (string.IsNullOrEmpty(hoaDonThueID))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã dịch vụ và mã hóa đơn thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng Dịch Vụ
            DichVu dv = new DichVu
            {
                DichVuID = dichVuID,
                HoaDonThueID = hoaDonThueID,
                NgayTao = ngayTao,
                TrangThai = trangThai,
                GhiChu = ghiChu
            };

            // Gọi BUS để thêm
            BUSQLDichVu bus = new BUSQLDichVu();
            string result = bus.InsertDichVu(dv);

            // Thông báo kết quả
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm dịch vụ mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();            // Xóa form
                LoadDanhSachDichVu();     // Tải lại danh sách
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearForm();
            LoadDanhSachDichVu();
        }

        private void dgvQLDichVu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //isDoubleClick = true;
            //if (e.RowIndex >= 0)
            //{
            //    string maDichVu = dgvQLDichVu.Rows[e.RowIndex].Cells["MaDichVu"].Value.ToString();
            //    frmChiTietDichVu_2 frmChiTiet = new frmChiTietDichVu_2(maDichVu);
            //    frmChiTiet.ShowDialog();
            //}
            //isDoubleClick = false;
        }

        private void dgvQLDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (isDoubleClick) return; // nếu vừa double click thì bỏ qua click
            //if (e.RowIndex >= 0)
            //{
            //    string maDichVu = dgvQLDichVu.Rows[e.RowIndex].Cells["DichVuID"].Value.ToString();
            //    frmChiTietDichVu_2 frm = new frmChiTietDichVu_2(maDichVu);
            //    frm.ShowDialog();
            //}

        }
        private void dgvQLDichVu_Click(object sender, EventArgs e)
        {
            //if (isDoubleClick) return; // nếu vừa double click thì bỏ qua

            //if (dgvQLDichVu.CurrentRow != null)
            //{
            //    string maDichVu = dgvQLDichVu.CurrentRow.Cells["DichVuID"].Value.ToString();
            //    frmChiTietDichVu_2 frm = new frmChiTietDichVu_2(maDichVu);
            //    frm.ShowDialog();
            //}


            if (isDoubleClick) return;

            if (dgvQLDichVu.CurrentRow != null)
            {
                string hoaDonThueID = dgvQLDichVu.CurrentRow.Cells["HoaDonThueID"].Value.ToString();
                frmChiTietDichVu_2 frm = new frmChiTietDichVu_2(hoaDonThueID);
                frm.ShowDialog();
            }
        }

        private void btnThem_1_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string dichVuID = txtDichVuID.Text.Trim();
            string hoaDonThueID = cboMaHoaDon.SelectedValue.ToString();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();

            // Lấy trạng thái từ radio button
            bool trangThai = rdbDangThue.Checked;

            // Kiểm tra dữ liệu đầu vào cơ bản
            if (string.IsNullOrEmpty(hoaDonThueID))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã dịch vụ và mã hóa đơn thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng Dịch Vụ
            DichVu dv = new DichVu
            {
                DichVuID = dichVuID,
                HoaDonThueID = hoaDonThueID,
                NgayTao = ngayTao,
                TrangThai = trangThai,
                GhiChu = ghiChu
            };

            // Gọi BUS để thêm
            BUSQLDichVu bus = new BUSQLDichVu();
            string result = bus.InsertDichVu(dv);

            // Thông báo kết quả
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm dịch vụ mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();            // Xóa form
                LoadDanhSachDichVu();     // Tải lại danh sách
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_1_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các điều khiển
            string dichVuID = txtDichVuID.Text.Trim();
            string hoaDonThueID = cboMaHoaDon.SelectedValue.ToString();
            DateTime ngayTao = dtpNgayTao.Value.Date;
            string ghiChu = txtGhiChu.Text.Trim();

            // Lấy trạng thái từ radio button
            bool trangThai = rdbDangThue.Checked;

            // Kiểm tra dữ liệu đầu vào cơ bản
            if (string.IsNullOrEmpty(dichVuID) || string.IsNullOrEmpty(hoaDonThueID))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ mã dịch vụ và mã hóa đơn thuê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng Dịch Vụ
            DichVu dv = new DichVu
            {
                DichVuID = dichVuID,
                HoaDonThueID = hoaDonThueID,
                NgayTao = ngayTao,
                TrangThai = trangThai,
                GhiChu = ghiChu
            };

            // Gọi BUS để thêm
            BUSQLDichVu bus = new BUSQLDichVu();
            string result = bus.UpdateDichVu(dv);

            // Thông báo kết quả
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm dịch vụ mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();            // Xóa form
                LoadDanhSachDichVu();     // Tải lại danh sách
            }
            else
            {
                MessageBox.Show(result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_1_Click(object sender, EventArgs e)
        {
            string dichVuID = txtDichVuID.Text.Trim();

            // Nếu chưa nhập ID thì lấy từ dòng đang chọn trong DataGridView
            if (string.IsNullOrEmpty(dichVuID))
            {
                if (dgvQLDichVu.SelectedRows.Count > 0)
                {
                    DataGridViewRow selectedRow = dgvQLDichVu.SelectedRows[0];
                    dichVuID = selectedRow.Cells["DichVuID"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn dịch vụ cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Xác nhận xóa
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa dịch vụ này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                BUSQLDichVu bus = new BUSQLDichVu();
                string kq = bus.DeleteDichVu(dichVuID);

                if (string.IsNullOrEmpty(kq))
                {
                    MessageBox.Show($"Xóa dịch vụ {dichVuID} thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();         // Xóa form nhập
                    LoadDanhSachDichVu();  // Load lại danh sách
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
            LoadDanhSachDichVu();
            //LoadMaDichVuMoi();
        }


    }
}
