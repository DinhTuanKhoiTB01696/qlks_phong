using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL_QLKS;
using DAL_QLKS;
using DTO_QLKS;

namespace GUI_QLKS
{
    public partial class frmTrangThaiDatPhong : Form
    {
        public frmTrangThaiDatPhong()
        {
            InitializeComponent();
            LoadComboBoxes();
            LoadData();
            this.dgvTrangThaiDatPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTrangThaiDatPhong_CellClick);
            TrangThaiDatPhongDAL dal = new TrangThaiDatPhongDAL();
            txtTrangThaiID.Text = dal.GenerateNewTrangThaiID();
            txtTrangThaiID.Enabled = false;
        }
        private TrangThaiDatPhongBLL bll = new TrangThaiDatPhongBLL();
        private LoaiTrangThaiDatPhongBLL loaiTrangThaiBLL = new LoaiTrangThaiDatPhongBLL();
        BUS_Phong busPhong = new BUS_Phong();

        private void LoadComboBoxes()
        {
            var listLoai = loaiTrangThaiBLL.LayDanhSach();
            cboLoaiTrangThaiID.DataSource = listLoai;
            cboLoaiTrangThaiID.DisplayMember = "LoaiTrangThaiID";   // ✅ tên trạng thái hiển thị
            cboLoaiTrangThaiID.ValueMember = "LoaiTrangThaiID";

            cboTenTrangThai.DataSource = listLoai; 
            cboTenTrangThai.DisplayMember = "TenTrangThai"; 
            cboTenTrangThai.ValueMember = "TenTrangThai";

            var listHoaDon = bll.LayDanhSach();
            cboHoaDonThueID.DataSource = listHoaDon;
            cboHoaDonThueID.DisplayMember = "HoaDonThueID"; 
            cboHoaDonThueID.ValueMember = "HoaDonThueID";
        }
        private void LoadData()
        {
            dgvTrangThaiDatPhong.DataSource = bll.LayDanhSach();
            dgvTrangThaiDatPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTrangThaiDatPhong.ReadOnly = true;
            dgvTrangThaiDatPhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvTrangThaiDatPhong.Columns["TrangThaiID"].HeaderText = "Trạng thái";
            dgvTrangThaiDatPhong.Columns["HoaDonThueID"].HeaderText = "Hóa đơn thuê";
            dgvTrangThaiDatPhong.Columns["LoaiTrangThaiID"].HeaderText = "Loại trạng thái";
            dgvTrangThaiDatPhong.Columns["TenTrangThai"].HeaderText = "Tên trạng thái";
            dgvTrangThaiDatPhong.Columns["NgayCapNhat"].HeaderText = "Ngày cập nhật";
        }



        private string TaoMaTrangThaiMoi()
        {
            string prefix = "TT";
            int maxID = 0;

            // Giả sử bạn đang có DataGridView hiển thị dữ liệu
            foreach (DataGridViewRow row in dgvTrangThaiDatPhong.Rows)
            {
                if (row.Cells["TrangThaiID"].Value != null)
                {
                    string id = row.Cells["TrangThaiID"].Value.ToString();
                    if (id.StartsWith(prefix))
                    {
                        string numberPart = id.Substring(prefix.Length).Trim();
                        if (int.TryParse(numberPart, out int num))
                        {
                            if (num > maxID)
                                maxID = num;
                        }
                    }
                }
            }

            return prefix + (maxID + 1).ToString("D3"); // ví dụ: TT001, TT002
        }
        
        private string TaoMaHoaDonMoi()
        {
            var usedNumbers = dgvTrangThaiDatPhong.Rows
                .Cast<DataGridViewRow>()
                .Select(r => r.Cells["HoaDonThueID"].Value?.ToString())
                .Where(val => !string.IsNullOrEmpty(val) && val.StartsWith("HD"))
                .Select(val =>
                {
                    if (int.TryParse(val.Substring(2), out int num))
                        return num;
                    return -1;
                })
                .Where(n => n > 0)
                .OrderBy(n => n)
                .ToList();

            int newNumber = 1;
            foreach (var n in usedNumbers)
            {
                if (n == newNumber)
                    newNumber++;
                else
                    break;
            }
            return $"HD{newNumber:D3}";
        }

        

        private void btnSua2_Click(object sender, EventArgs e)
        {
            var dto = new TrangThaiDatPhongDTO
            {
                TrangThaiID = txtTrangThaiID.Text.Trim(),
                HoaDonThueID = cboHoaDonThueID.SelectedValue?.ToString(),
                LoaiTrangThaiID = cboLoaiTrangThaiID.SelectedValue?.ToString(),
                NgayCapNhat = dtpNgayCapNhat.Value
            };

            if (string.IsNullOrEmpty(dto.HoaDonThueID) || string.IsNullOrEmpty(dto.LoaiTrangThaiID))
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Hóa Đơn Thuê và Trạng Thái!");
                return;
            }

            if (bll.CapNhat(dto))
            {
                string loaiTrangThaiText = cboTenTrangThai.Text.Trim().ToLower();

                // Nếu là trạng thái "Hủy" thì cập nhật phòng thành "Trống"
                if (loaiTrangThaiText.Contains("hủy") || loaiTrangThaiText.Contains("huỷ"))
                {
                    var busDatPhong = new BUSDatPhong();
                    var datPhong = busDatPhong.GetDatPhongByID(dto.HoaDonThueID);

                    if (datPhong != null)
                    {
                        var phongID = datPhong.PhongID;

                        var busPhong = new BUS_Phong();
                        // Giả sử TinhTrang = false là "Trống"
                        busPhong.UpdateTinhTrangPhong(phongID, false);
                    }
                }

                MessageBox.Show("Cập nhật thành công!");
                LoadData();
                LamMoi();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }


        private void CapNhatPhongVeTrangThaiTrong(string hoaDonThueID)
        {
            var bllDatPhong = new BUSDatPhong(); // sử dụng lớp xử lý DatPhong đúng
            var datPhong = bllDatPhong.GetDatPhongByID(hoaDonThueID);

            if (datPhong != null)
            {
                string phongID = datPhong.PhongID;
                var bllPhong = new BUS_Phong();
                bllPhong.UpdateTinhTrangPhong(phongID, false); // false = trống
            }
        }




        private void btnMoi2_Click(object sender, EventArgs e)
        {
            LamMoi();
        }
        private void LamMoi()
        {
            cboHoaDonThueID.SelectedIndex = -1;
            cboLoaiTrangThaiID.SelectedIndex = 0;
            dtpNgayCapNhat.Value = DateTime.Now;
            cboTenTrangThai.SelectedIndex = -1;
        }

        private void btnTim2_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiemHoaDonID.Text.Trim();
            dgvTrangThaiDatPhong.DataSource = bll.TimKiemTheoHoaDon(keyword);
        }

        private void dgvTrangThaiDatPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTrangThaiDatPhong.Rows[e.RowIndex];

                txtTrangThaiID.Text = row.Cells["TrangThaiID"].Value?.ToString();
                cboHoaDonThueID.SelectedValue = row.Cells["HoaDonThueID"].Value?.ToString();
                cboLoaiTrangThaiID.SelectedValue = row.Cells["LoaiTrangThaiID"].Value?.ToString();
                cboTenTrangThai.SelectedValue = row.Cells["TenTrangThai"].Value?.ToString();
                dtpNgayCapNhat.Value = Convert.ToDateTime(row.Cells["NgayCapNhat"].Value);
            }
        }

        private void cboLoaiTrangThaiID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTenTrangThai.SelectedItem != null)
            {
                var selected = (LoaiTrangThaiDatPhongDTO)cboTenTrangThai.SelectedItem; cboLoaiTrangThaiID.Text = selected.LoaiTrangThaiID;
            }
        }

        private void cboTenTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboTenTrangThai.SelectedItem != null)
            {
                var selected = (LoaiTrangThaiDatPhongDTO)cboTenTrangThai.SelectedItem;
                cboLoaiTrangThaiID.Text = selected.LoaiTrangThaiID;
            }
        }
    }
}
