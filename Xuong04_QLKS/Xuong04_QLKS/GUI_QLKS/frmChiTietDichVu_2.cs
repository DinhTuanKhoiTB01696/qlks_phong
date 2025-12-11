using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BLL_QLKS;
using DTO_QLKS;
using DAL_QLKS;
using System.Collections.Generic;

namespace GUI_QLKS
{
    public partial class frmChiTietDichVu_2 : Form
    {
        private BLLChiTietDichVu _bllChiTietDichVu = new BLLChiTietDichVu();
        private BUSLoaiDichVu _bllLoaiDichVu = new BUSLoaiDichVu();
        private System.Collections.Generic.List<DTO_QLKS.ChiTietDichVu> _listChiTietDichVu = new System.Collections.Generic.List<DTO_QLKS.ChiTietDichVu>();
        private bool isHoaDonThueActive = false;
        private string _maDichVu;
        public frmChiTietDichVu_2()
        {
            InitializeComponent();
            ////dgvChiTietDichVu_CellClick_1.CellClick += dgvChiTietDichVu_CellClick_1;
            //dgvChiTietDichVu.CellEndEdit += dgvChiTietDichVu_CellEndEdit;
            //dgvLoaiDichVu.CellDoubleClick += dgvLoaiDichVu_CellDoubleClick;

            //_maDichVu = maDichVu;
            //LoadChiTietDichVu(_maDichVu);

            //dgvChiTietDichVu.CellEndEdit += dgvChiTietDichVu_CellEndEdit;
            //dgvLoaiDichVu.CellDoubleClick += dgvLoaiDichVu_CellDoubleClick;
        }

        public frmChiTietDichVu_2(string maDichVu) : this()
        {
            //_maDichVu = maDichVu;
            //LoadChiTietDichVu(_maDichVu);
        }

        private void frmChiTietDichVu_2_Load(object sender, EventArgs e)
        {

            // 1. Load dữ liệu cho các combobox trước
            LoadComboBoxData();
            LoadLoaiDichVu();
            LoadDichVu();
            CheckHoaDonThueStatus();

            // 2. Load DataGridView
            LoadChiTietDichVu(_maDichVu); // Nếu _maDichVu null thì load tất cả

            // 3. Nếu có mã dịch vụ, đổ dữ liệu vào form
            if (!string.IsNullOrEmpty(_maDichVu))
            {
                FillChiTietDichVuToForm(_maDichVu);
            }
            else
            {
                ClearFormChiTietDichVu();
            }

            // 4. Chọn hàng đầu tiên và hiển thị vào form
            if (dgvChiTietDichVu.Rows.Count > 0)
            {
                dgvChiTietDichVu.Rows[0].Selected = true;
                dgvChiTietDichVu_CellClick_1(dgvChiTietDichVu, new DataGridViewCellEventArgs(0, 0));
            }

            // 5. Khóa những control không cho sửa ID
            txtChiTietID.Enabled = false;
            cboHoaDonThue.Enabled = true;
            cboDichVuID.Enabled = true;

        }


        private void FillChiTietDichVuToForm(string maChiTiet)
        {
            DataTable dt = _bllChiTietDichVu.GetByID(maChiTiet); // lấy 1 bảng

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0]; // lấy dòng đầu tiên

                cboHoaDonThue.SelectedValue = row["HoaDonThueID"].ToString();
                cboDichVuID.SelectedValue = row["DichVuID"].ToString();
                cboLoaiDichVuID.SelectedValue = row["LoaiDichVuID"].ToString();
                nudSoLuong.Value = Convert.ToInt32(row["SoLuong"]);
                dtpBatDau.Value = Convert.ToDateTime(row["NgayBatDau"]);
                dtpKetThuc.Value = Convert.ToDateTime(row["NgayKetThuc"]);
                txtGhiChu.Text = row["GhiChu"].ToString();
            }
        }



        private void CheckHoaDonThueStatus()
        {

            if (isHoaDonThueActive)
            {
                btnThemCT.Enabled = false;
                btnSuaCT.Enabled = false;
                btnXoaCT.Enabled = false;
                //btnLen.Enabled = false;
                //btnXuong.Enabled = false;
                dgvChiTietDichVu.ReadOnly = true;

            }
            else
            {
                btnThemCT.Enabled = true;
                btnSuaCT.Enabled = true;
                btnXoaCT.Enabled = true;
                //btnLen.Enabled = true;
                //btnXuong.Enabled = true;
                dgvChiTietDichVu.ReadOnly = false;
                if (dgvChiTietDichVu.Columns.Contains("SoLuong"))
                {
                    dgvChiTietDichVu.Columns["SoLuong"].ReadOnly = false;
                }

            }
        }

        //private void LoadChiTietDichVu()
        //{
        //    try
        //    {
        //        DataTable dt = _bllChiTietDichVu.GetAll();
        //        _listChiTietDichVu = dt.AsEnumerable()
        //                            .Select(row => new DTO_QLKS.ChiTietDichVu
        //                            {
        //                                ChiTietDichVuID = row.Field<string>("ChiTietDichVuID"),
        //                                HoaDonThueID = row.Field<string>("HoaDonThueID"),
        //                                DichVuID = row.Field<string>("DichVuID"),
        //                                LoaiDichVuID = row.Field<string>("LoaiDichVuID"),
        //                                SoLuong = row.Field<int>("SoLuong"),
        //                                NgayBatDau = row.Field<DateTime>("NgayBatDau"),
        //                                NgayKetThuc = row.Field<DateTime>("NgayKetThuc"),
        //                                GhiChu = row.Field<string>("GhiChu")
        //                            }).ToList();

        //        dgvChiTietDichVu.DataSource = dt;
        //        dgvChiTietDichVu.Columns["ChiTietDichVuID"].HeaderText = "Mã CT Dịch Vụ";
        //        dgvChiTietDichVu.Columns["HoaDonThueID"].HeaderText = "Mã Hóa Đơn Thuê";
        //        dgvChiTietDichVu.Columns["DichVuID"].HeaderText = "Mã Dịch Vụ";
        //        dgvChiTietDichVu.Columns["LoaiDichVuID"].Visible = false; // Ẩn mã loại dịch vụ

        //        dgvChiTietDichVu.DataSource = _bllChiTietDichVu.GetAllChiTietDichVu();
        //        dgvChiTietDichVu.Columns["TenDichVu"].HeaderText = "Tên dịch vụ";

        //        dgvChiTietDichVu.Columns["SoLuong"].HeaderText = "Số Lượng";
        //        dgvChiTietDichVu.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
        //        dgvChiTietDichVu.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
        //        dgvChiTietDichVu.Columns["GhiChu"].HeaderText = "Ghi Chú";

        //        if (dgvChiTietDichVu.Columns.Contains("TenDichVu"))
        //        {
        //            dgvChiTietDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
        //        }
        //        if (dgvChiTietDichVu.Columns.Contains("GiaDichVu"))
        //        {
        //            dgvChiTietDichVu.Columns["GiaDichVu"].HeaderText = "Giá Dịch Vụ";
        //        }
        //        if (dgvChiTietDichVu.Columns.Contains("DonViTinh"))
        //        {
        //            dgvChiTietDichVu.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
        //        }

        //        dgvChiTietDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        //        if (dgvChiTietDichVu.Columns.Contains("SoLuong"))
        //        {
        //            dgvChiTietDichVu.Columns["SoLuong"].ReadOnly = isHoaDonThueActive;
        //        }


        //        if (dgvChiTietDichVu.Columns.Contains("DichVuID"))
        //        {
        //            dgvChiTietDichVu.Columns["DichVuID"].Visible = false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi khi tải dữ liệu chi tiết dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}




        private void LoadChiTietDichVu(string maDichVu = null)
        {
            try
            {
                DataTable dt = _bllChiTietDichVu.GetAllChiTietDichVu();

                if (!string.IsNullOrEmpty(maDichVu))
                {
                    dt = dt.AsEnumerable()
                        .Where(r => r.Field<string>("DichVuID") == maDichVu)
                        .CopyToDataTable();
                }

                dgvChiTietDichVu.DataSource = dt;

                dgvChiTietDichVu.Columns["ChiTietDichVuID"].HeaderText = "Mã CT Dịch Vụ";
                dgvChiTietDichVu.Columns["HoaDonThueID"].HeaderText = "Mã Hóa Đơn Thuê";
                dgvChiTietDichVu.Columns["SoLuong"].HeaderText = "Số Lượng";
                dgvChiTietDichVu.Columns["NgayBatDau"].HeaderText = "Ngày Bắt Đầu";
                dgvChiTietDichVu.Columns["NgayKetThuc"].HeaderText = "Ngày Kết Thúc";
                dgvChiTietDichVu.Columns["GhiChu"].HeaderText = "Ghi Chú";

                dgvChiTietDichVu.Columns["LoaiDichVuID"].Visible = true;
                dgvChiTietDichVu.Columns["DichVuID"].Visible = true;

                dgvChiTietDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu chi tiết dịch vụ: {ex.Message}");
            }
        }






        private void LoadComboBoxData()
        {
            try
            {
                // Load Hóa đơn thuê (từ bảng DatPhong)
                var hoaDonData = DBUtil.Query(
                    "SELECT DISTINCT HoaDonThueID FROM DatPhong ORDER BY HoaDonThueID",
                    new Dictionary<string, object>()
                );
                if (hoaDonData != null)
                {
                    cboHoaDonThue.DataSource = hoaDonData;
                    cboHoaDonThue.DisplayMember = "HoaDonThueID";
                    cboHoaDonThue.ValueMember = "HoaDonThueID";
                    cboHoaDonThue.SelectedIndex = -1;
                }

                // Load Dịch vụ (JOIN để lấy tên từ LoaiDichVu)
                var dichVuData = DBUtil.Query(
                    "SELECT dv.DichVuID, ldv.TenDichVu FROM DichVu dv " +
                    "JOIN LoaiDichVu ldv ON dv.LoaiDichVuID = ldv.LoaiDichVuID " +
                    "ORDER BY ldv.TenDichVu",
                    new Dictionary<string, object>()
                );
                if (dichVuData != null)
                {
                    cboDichVuID.DataSource = dichVuData;
                    cboDichVuID.DisplayMember = "TenDichVu";
                    cboDichVuID.ValueMember = "DichVuID";
                    cboDichVuID.SelectedIndex = -1;
                }

                // Load Loại dịch vụ
                var loaiDichVuData = DBUtil.Query(
                    "SELECT LoaiDichVuID, TenDichVu FROM LoaiDichVu ORDER BY TenDichVu",
                    new Dictionary<string, object>()
                );
                if (loaiDichVuData != null)
                {
                    cboLoaiDichVuID.DataSource = loaiDichVuData;
                    cboLoaiDichVuID.DisplayMember = "TenDichVu";
                    cboLoaiDichVuID.ValueMember = "LoaiDichVuID";
                    cboLoaiDichVuID.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load ComboBox: " + ex.Message);
            }
        }


        private void ClearFormChiTietDichVu(string hoaDonThueID = null, string dichVuID = null)
        {
            DALChiTietDichVu dal = new DALChiTietDichVu();
            txtChiTietID.Text = dal.GenerateNextID();

            // Nếu có giá trị giữ lại thì set, ngược lại reset về -1
            if (!string.IsNullOrEmpty(hoaDonThueID))
                cboHoaDonThue.SelectedValue = hoaDonThueID;
            else
                cboHoaDonThue.SelectedIndex = -1;

            if (!string.IsNullOrEmpty(dichVuID))
                cboDichVuID.SelectedValue = dichVuID;
            else
                cboDichVuID.SelectedIndex = -1;

            cboLoaiDichVuID.SelectedIndex = -1;
            nudSoLuong.Value = 1;
            txtGhiChu.Clear();
            dtpBatDau.Value = DateTime.Now;
            dtpKetThuc.Value = DateTime.Now;

            txtChiTietID.Enabled = false;
            cboHoaDonThue.Enabled = true; // khóa luôn mã phiếu
            cboDichVuID.Enabled = true;   // khóa luôn mã dịch vụ
            cboLoaiDichVuID.Enabled = true;
            nudSoLuong.Enabled = true;
            dtpBatDau.Enabled = true;
            dtpKetThuc.Enabled = true;
            txtGhiChu.Enabled = true;

            //btnThemCT.Enabled = true;
            //btnSuaCT.Enabled = false;
            //btnXoaCT.Enabled = false;

            dgvChiTietDichVu.ClearSelection();
        }


        private void LoadDichVu()
        {
            try
            {
                BUSQLDichVu busQLDichVu = new BUSQLDichVu();
                cboDichVuID.DataSource = busQLDichVu.GetDichVuList();  // ✅ Sửa tại đây
                cboDichVuID.DisplayMember = "DichVuID";                 // hoặc "TenDichVu" nếu có
                cboDichVuID.ValueMember = "DichVuID";
                cboDichVuID.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void LoadLoaiDichVu()
        {
            try
            {
                BUSLoaiDichVu busLoaiDichVu = new BUSLoaiDichVu(); // ✅ tạo instance
                dgvLoaiDichVu.DataSource = busLoaiDichVu.GetAll(); // ✅ gọi non-static method

                dgvLoaiDichVu.Columns["LoaiDichVuID"].HeaderText = "Mã Loại DV";
                dgvLoaiDichVu.Columns["TenDichVu"].HeaderText = "Tên Dịch Vụ";
                dgvLoaiDichVu.Columns["GiaDichVu"].HeaderText = "Giá Dịch Vụ";
                dgvLoaiDichVu.Columns["DonViTinh"].HeaderText = "Đơn Vị Tính";
                dgvLoaiDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách loại dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (isHoaDonThueActive)
            {
                MessageBox.Show("Hóa đơn đã được thanh toán hoặc khóa. Không thể thêm dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ChiTietDichVu ct = new ChiTietDichVu()
            {
                HoaDonThueID = cboHoaDonThue.SelectedValue?.ToString(),
                DichVuID = cboDichVuID.SelectedValue?.ToString(),
                LoaiDichVuID = cboLoaiDichVuID.SelectedValue?.ToString(),
                SoLuong = (int)nudSoLuong.Value,
                NgayBatDau = dtpBatDau.Value,
                NgayKetThuc = dtpKetThuc.Value,
                GhiChu = txtGhiChu.Text
            };

            string result = _bllChiTietDichVu.Insert(ct);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Thêm chi tiết dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload theo mã dịch vụ hiện tại
                LoadChiTietDichVu(_maDichVu);

                // Chọn dòng vừa thêm
                if (dgvChiTietDichVu.Rows.Count > 0)
                {
                    int newIndex = dgvChiTietDichVu.Rows.Count - 1;
                    dgvChiTietDichVu.Rows[newIndex].Selected = true;
                    dgvChiTietDichVu_CellClick_1(dgvChiTietDichVu, new DataGridViewCellEventArgs(0, newIndex));
                }
            }
            else
            {
                MessageBox.Show($"Lỗi: {result}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            if (isHoaDonThueActive)
            {
                MessageBox.Show("Hóa đơn đã được thanh toán hoặc khóa. Không thể sửa dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtChiTietID.Text))
            {
                MessageBox.Show("Vui lòng chọn chi tiết dịch vụ cần sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ChiTietDichVu ct = new ChiTietDichVu()
            {
                ChiTietDichVuID = txtChiTietID.Text,
                HoaDonThueID = cboHoaDonThue.SelectedValue?.ToString(),
                DichVuID = cboDichVuID.SelectedValue?.ToString(),
                LoaiDichVuID = cboLoaiDichVuID.SelectedValue?.ToString(),
                SoLuong = (int)nudSoLuong.Value,
                NgayBatDau = dtpBatDau.Value,
                NgayKetThuc = dtpKetThuc.Value,
                GhiChu = txtGhiChu.Text
            };

            string result = _bllChiTietDichVu.Update(ct);
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Cập nhật chi tiết dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Giữ vị trí dòng đang chọn
                int currentIndex = dgvChiTietDichVu.CurrentCell.RowIndex;

                // Reload
                LoadChiTietDichVu(_maDichVu);

                // Chọn lại dòng cũ
                if (currentIndex >= 0 && currentIndex < dgvChiTietDichVu.Rows.Count)
                {
                    dgvChiTietDichVu.Rows[currentIndex].Selected = true;
                    dgvChiTietDichVu_CellClick_1(dgvChiTietDichVu, new DataGridViewCellEventArgs(0, currentIndex));
                }
            }
            else
            {
                MessageBox.Show($"Lỗi: {result}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (isHoaDonThueActive)
            {
                MessageBox.Show("Hóa đơn đã được thanh toán hoặc khóa. Không thể xóa dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(txtChiTietID.Text))
            {
                MessageBox.Show("Vui lòng chọn chi tiết dịch vụ cần xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa chi tiết dịch vụ '{txtChiTietID.Text}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                string result = _bllChiTietDichVu.Delete(txtChiTietID.Text);
                if (string.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Xóa chi tiết dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload
                    LoadChiTietDichVu(_maDichVu);

                    // Nếu còn dữ liệu thì chọn lại dòng đầu
                    if (dgvChiTietDichVu.Rows.Count > 0)
                    {
                        dgvChiTietDichVu.Rows[0].Selected = true;
                        dgvChiTietDichVu_CellClick_1(dgvChiTietDichVu, new DataGridViewCellEventArgs(0, 0));
                    }
                    else
                    {
                        ClearFormChiTietDichVu();
                    }
                }
                else
                {
                    MessageBox.Show($"Lỗi: {result}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnMoiCT_Click(object sender, EventArgs e)
        {
            string maHoaDonThue = null;
            string maDichVuDangChon = null;

            if (dgvChiTietDichVu.CurrentRow != null)
            {
                maHoaDonThue = dgvChiTietDichVu.CurrentRow.Cells["HoaDonThueID"].Value.ToString();
                maDichVuDangChon = dgvChiTietDichVu.CurrentRow.Cells["DichVuID"].Value.ToString();
            }

            // Reset nhưng vẫn giữ 2 mã cố định
            ClearFormChiTietDichVu(maHoaDonThue, maDichVuDangChon);

            // Load lại danh sách chi tiết của dịch vụ đó
            LoadChiTietDichVu(maDichVuDangChon);

            //*LoadComboBoxData();
        }


        private void SetComboBoxValue(ComboBox comboBox, string columnName, DataGridViewRow row)
        {
            object cellValue = row.Cells[columnName]?.Value;

            if (cellValue != null)
            {
                // Nếu ComboBox đã có ValueMember thì chọn trực tiếp
                if (!string.IsNullOrEmpty(comboBox.ValueMember))
                {
                    comboBox.SelectedValue = cellValue;
                    return;
                }

                // Nếu không có ValueMember thì tìm theo DisplayMember hoặc chuỗi
                foreach (var item in comboBox.Items)
                {
                    if (item != null && item.ToString() == cellValue.ToString())
                    {
                        comboBox.SelectedItem = item;
                        return;
                    }
                }
            }

            comboBox.SelectedIndex = -1; // Không tìm thấy thì reset
        }




        private void dgvChiTietDichVu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (isHoaDonThueActive)
            {
                ShowMessage("Hóa đơn đã được thanh toán hoặc khóa. Không thể chỉnh sửa số lượng.");
                LoadChiTietDichVu();
                return;
            }

            if (e.RowIndex >= 0 && dgvChiTietDichVu.Columns[e.ColumnIndex].Name == "SoLuong")
            {
                var row = dgvChiTietDichVu.Rows[e.RowIndex];
                string id = row.Cells["ChiTietDichVuID"].Value?.ToString();
                ChiTietDichVu ct = _listChiTietDichVu.FirstOrDefault(x => x.ChiTietDichVuID == id);

                if (ct == null)
                {
                    ShowMessage("Không tìm thấy chi tiết dịch vụ để cập nhật.", "Lỗi", MessageBoxIcon.Error);
                    LoadChiTietDichVu();
                    return;
                }

                int oldValue = ct.SoLuong;

                if (int.TryParse(row.Cells["SoLuong"].Value?.ToString(), out int newValue))
                {
                    if (newValue <= 0)
                    {
                        ShowMessage("Số lượng phải lớn hơn 0. Để xóa, hãy dùng nút xóa.", "Lỗi nhập liệu", MessageBoxIcon.Warning);
                        row.Cells["SoLuong"].Value = oldValue;
                        return;
                    }

                    ct.SoLuong = newValue;
                    string result = _bllChiTietDichVu.Update(ct);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ShowMessage($"Cập nhật số lượng thất bại: {result}", "Lỗi", MessageBoxIcon.Error);
                        row.Cells["SoLuong"].Value = oldValue;
                    }
                    else
                    {
                        ShowMessage("Cập nhật số lượng thành công.");
                        LoadChiTietDichVu();
                    }
                }
                else
                {
                    ShowMessage("Vui lòng nhập số hợp lệ cho số lượng!", "Lỗi", MessageBoxIcon.Warning);
                    row.Cells["SoLuong"].Value = oldValue;
                }
            }
        }

        private void dgvLoaiDichVu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isHoaDonThueActive)
            {
                MessageBox.Show("Hóa đơn đã được thanh toán hoặc khóa. Không thể thêm dịch vụ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLoaiDichVu.Rows[e.RowIndex];

                string loaiDichVuID = row.Cells["LoaiDichVuID"].Value?.ToString();
                string tenDichVu = row.Cells["TenDichVu"].Value?.ToString();
                decimal giaDichVu = Convert.ToDecimal(row.Cells["GiaDichVu"].Value);

                string hoaDonThueIDHienTai = cboHoaDonThue.SelectedValue?.ToString();
                if (string.IsNullOrEmpty(hoaDonThueIDHienTai))
                {
                    MessageBox.Show("Vui lòng chọn một Hóa đơn thuê trước khi thêm dịch vụ.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                TransferLoaiDichVuToChiTiet(loaiDichVuID, tenDichVu, hoaDonThueIDHienTai);
            }
        }

        private void TransferLoaiDichVuToChiTiet(string loaiDichVuID, string tenDichVu, string hoaDonThueID, int soLuong = 1)
        {
            if (isHoaDonThueActive)
                return;

            if (string.IsNullOrEmpty(hoaDonThueID) || string.IsNullOrEmpty(loaiDichVuID))
            {
                ShowMessage("Thiếu thông tin Hóa đơn thuê hoặc Loại dịch vụ.", "Lỗi", MessageBoxIcon.Error);
                return;
            }

            var existing = _listChiTietDichVu.FirstOrDefault(x =>
                x.HoaDonThueID == hoaDonThueID && x.LoaiDichVuID == loaiDichVuID);

            string result = string.Empty;

            if (existing != null)
            {
                existing.SoLuong += soLuong;
                result = _bllChiTietDichVu.Update(existing);
            }
            else
            {
                var newCt = new ChiTietDichVu
                {
                    HoaDonThueID = hoaDonThueID,
                    LoaiDichVuID = loaiDichVuID,
                    DichVuID = loaiDichVuID,
                    SoLuong = soLuong,
                    NgayBatDau = DateTime.Now,
                    NgayKetThuc = DateTime.Now,
                    GhiChu = $"Dịch vụ {tenDichVu}"
                };

                result = _bllChiTietDichVu.Insert(newCt);
            }

            if (!string.IsNullOrEmpty(result))
                ShowMessage($"Thêm/cập nhật dịch vụ không thành công: {result}", "Lỗi", MessageBoxIcon.Error);
            else
                LoadChiTietDichVu();
        }

        private void btnLen_Click(object sender, EventArgs e)
        {
            CapNhatSoLuongDichVu(1);
        }

        private void btnXuong_Click(object sender, EventArgs e)
        {
            CapNhatSoLuongDichVu(-1);
        }





        // Thông báo dùng chung
        private void ShowMessage(string message, string title = "Thông báo", MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        // Kiểm tra ngày hợp lệ
        private bool ValidateDateRange()
        {
            if (dtpBatDau.Value > dtpKetThuc.Value)
            {
                ShowMessage("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.", "Lỗi ngày", MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // Cập nhật số lượng dịch vụ theo delta
        private void CapNhatSoLuongDichVu(int delta)
        {
            if (isHoaDonThueActive)
            {
                ShowMessage("Hóa đơn đã được thanh toán hoặc khóa. Không thể chỉnh sửa số lượng.");
                return;
            }

            if (dgvChiTietDichVu.CurrentRow != null)
            {
                DataGridViewRow currentRow = dgvChiTietDichVu.CurrentRow;
                string chiTietDichVuID = currentRow.Cells["ChiTietDichVuID"].Value?.ToString();

                if (string.IsNullOrEmpty(chiTietDichVuID))
                {
                    ShowMessage("Không tìm thấy ID chi tiết dịch vụ.", "Lỗi", MessageBoxIcon.Error);
                    return;
                }

                ChiTietDichVu? ct = _listChiTietDichVu.FirstOrDefault(x => x.ChiTietDichVuID == chiTietDichVuID);

                if (ct != null)
                {
                    if (ct.SoLuong == 1 && delta == -1)
                    {
                        DialogResult confirm = MessageBox.Show(
                            $"Số lượng đang là 1. Bạn có muốn xóa dịch vụ '{currentRow.Cells["TenDichVu"].Value}'?",
                            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (confirm == DialogResult.Yes)
                        {
                            string result = _bllChiTietDichVu.Delete(chiTietDichVuID);
                            if (string.IsNullOrEmpty(result))
                                ShowMessage("Đã xóa dịch vụ.");
                            else
                                ShowMessage($"Lỗi khi xóa dịch vụ: {result}", "Lỗi", MessageBoxIcon.Error);

                            LoadChiTietDichVu();
                        }
                        return;
                    }

                    ct.SoLuong += delta;
                    if (ct.SoLuong <= 0) ct.SoLuong = 1;

                    string updateResult = _bllChiTietDichVu.Update(ct);
                    if (!string.IsNullOrEmpty(updateResult))
                        ShowMessage($"Cập nhật số lượng thất bại: {updateResult}", "Lỗi", MessageBoxIcon.Error);
                    else
                        ShowMessage("Cập nhật số lượng thành công.");

                    LoadChiTietDichVu();
                }
            }
            else
            {
                ShowMessage("Vui lòng chọn một dịch vụ.", "Cảnh báo", MessageBoxIcon.Warning);
            }
        }



        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            //string chiTietDichVuID = txtTimKiem.Text.Trim();

            //if (string.IsNullOrEmpty(chiTietDichVuID))
            //{
            //    MessageBox.Show("Vui lòng nhập mã chi tiết dịch vụ để tìm kiếm.");
            //    return;
            //}

            //// Lấy dữ liệu từ BLL (đảm bảo bạn có phương thức GetByID)
            //DataTable ds = _bllChiTietDichVu.GetByID(chiTietDichVuID);

            //if (ds == null || ds.Rows.Count == 0)
            //{
            //    MessageBox.Show("Không tìm thấy dữ liệu cho mã chi tiết dịch vụ này.");
            //    //dgvChiTietDichVu.DataSource = null;
            //    return;
            //}

            //// Tạo bảng để hiển thị
            //DataTable table = new DataTable();
            //table.Columns.Add("ChiTietDichVuID", typeof(string));
            //table.Columns.Add("HoaDonThueID", typeof(string));
            //table.Columns.Add("DichVuID", typeof(string));
            //table.Columns.Add("LoaiDichVuID", typeof(string));
            //table.Columns.Add("SoLuong", typeof(int));
            //table.Columns.Add("NgayBatDau", typeof(DateTime));
            //table.Columns.Add("NgayKetThuc", typeof(DateTime));
            //table.Columns.Add("GhiChu", typeof(string));

            //// Đổ dữ liệu vào bảng
            //foreach (DataRow row in ds.Rows)
            //{
            //    table.Rows.Add(
            //        row["ChiTietDichVuID"].ToString(),
            //        row["HoaDonThueID"].ToString(),
            //        row["DichVuID"].ToString(),
            //        row["LoaiDichVuID"].ToString(),
            //        Convert.ToInt32(row["SoLuong"]),
            //        Convert.ToDateTime(row["NgayBatDau"]),
            //        Convert.ToDateTime(row["NgayKetThuc"]),
            //        row["GhiChu"].ToString()
            //    );
            //}

            //// Gán vào DataGridView
            //dgvChiTietDichVu.DataSource = table;
        }


        private void dgvChiTietDichVu_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvChiTietDichVu.Rows[e.RowIndex];

                // Đổ dữ liệu vào các control
                txtChiTietID.Text = row.Cells["ChiTietDichVuID"].Value?.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value?.ToString();
                SetComboBoxValue(cboHoaDonThue, "HoaDonThueID", row);
                SetComboBoxValue(cboDichVuID, "DichVuID", row);
                SetComboBoxValue(cboLoaiDichVuID, "LoaiDichVuID", row);

                if (int.TryParse(row.Cells["SoLuong"].Value?.ToString(), out int soLuong))
                    nudSoLuong.Value = soLuong;

                if (DateTime.TryParse(row.Cells["NgayBatDau"].Value?.ToString(), out DateTime batDau))
                    dtpBatDau.Value = batDau;

                if (DateTime.TryParse(row.Cells["NgayKetThuc"].Value?.ToString(), out DateTime ketThuc))
                    dtpKetThuc.Value = ketThuc;


                //dgvChiTietDichVu.DataSource = _bllChiTietDichVu.GetAll();
                //dgvChiTietDichVu.Columns["TenDichVu"].HeaderText = "Tên dịch vụ";

                //dgvChiTietDichVu.DataSource = _bllChiTietDichVu.GetAll();

                //if (dgvChiTietDichVu.Columns["TenDichVu"] != null)
                //{
                //    dgvChiTietDichVu.Columns["TenDichVu"].HeaderText = "Tên dịch vụ";
                //}


                // Vô hiệu hóa các control để không sửa được
                //txtChiTietID.Enabled = false;
                //cboHoaDonThue.Enabled = false;
                //cboDichVuID.Enabled = false;
                //cboLoaiDichVuID.Enabled = false;
                //nudSoLuong.Enabled = false;
                //dtpBatDau.Enabled = false;
                //dtpKetThuc.Enabled = false;
                //txtGhiChu.Enabled = false;

                // Chuyển trạng thái nút
                //btnThemCT.Enabled = true;
                //btnSuaCT.Enabled = false;
                //btnXoaCT.Enabled = false;
            }
        }


    }
}