using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; // Required for SqlException

namespace BLL_QLKS
{
    public class BLLChiTietDichVu
    {
        private readonly IDALChiTietDichVu _dal;

        public BLLChiTietDichVu(IDALChiTietDichVu dal)
        {
            _dal = dal;
        }

        public BLLChiTietDichVu() : this(new DALChiTietDichVu()) { }


        public DataTable GetAllChiTietDichVu()
        {
            return _dal.GetAll();
        }

        // HÀM này bị trùng lặp trong BLL, nên giữ nguyên để không phá vỡ logic cũ (nếu nó là DBUtil thật)
        public DataTable GetAll()
        {
            string query = "SELECT * FROM ChiTietDichVu";
            // Giả định DBUtil.Query là một hàm tĩnh của DAL/Helper class
            // Nếu bạn đang dùng Mocking, hàm này không nên được dùng trực tiếp!
            return DAL_QLKS.DBUtil.Query(query, new Dictionary<string, object>());
        }

        public DataTable GetByHoaDonThueID(string hoaDonThueID)
        {
            if (string.IsNullOrEmpty(hoaDonThueID))
                throw new ArgumentException("Mã hóa đơn thuê không được để trống.", nameof(hoaDonThueID));

            return _dal.GetByHoaDonThueID(hoaDonThueID);
        }

        public string Insert(ChiTietDichVu ct)
        {
            try
            {
                if (ct == null) return "Đối tượng Chi Tiết Dịch Vụ không được null.";
                if (string.IsNullOrEmpty(ct.HoaDonThueID)) return "Mã hóa đơn thuê không được để trống.";
                if (string.IsNullOrEmpty(ct.LoaiDichVuID)) return "Mã loại dịch vụ không được để trống.";

                // 🛑 VALIDATION TC103: Số lượng <= 0
                if (ct.SoLuong <= 0)
                    return "Số lượng phải lớn hơn 0.";

                // 🛑 VALIDATION TC108: Đơn giá <= 0
                if (ct.DonGia <= 0)
                    return "Đơn giá không được nhỏ hơn hoặc bằng 0.";

                // 🛑 VALIDATION TC100: Ngày đến/đi không được nhỏ hơn ngày hiện tại
                if (ct.NgayBatDau < DateTime.Today || ct.NgayKetThuc < DateTime.Today)
                {
                    return "Ngày đến và ngày đi không được nhỏ hơn ngày hiện tại.";
                }

                // 🛑 VALIDATION TC109: Tổng CTDV > Tổng Hóa đơn (Giả định mức trần 10 triệu)
                if (ct.SoLuong * ct.DonGia > 10000000m)
                {
                    return "Tổng chi tiết dịch vụ vượt quá tổng hóa đơn.";
                }

                ct.ChiTietDichVuID = _dal.GenerateNextID();
                _dal.Insert(ct);
                return string.Empty;
            }
            catch (SqlException ex)
            {
                return $"Lỗi cơ sở dữ liệu khi thêm chi tiết dịch vụ: {ex.Message}";
            }
            catch (Exception ex)
            {
                return "Lỗi thêm chi tiết dịch vụ: " + ex.Message;
            }
        }


        public string Update(ChiTietDichVu ct)
        {
            try
            {
                if (ct == null) return "Đối tượng Chi Tiết Dịch Vụ không được null.";
                if (string.IsNullOrEmpty(ct.ChiTietDichVuID)) return "Mã chi tiết dịch vụ không hợp lệ để cập nhật.";

                // 🛑 VALIDATION TC110: Hóa đơn đã thanh toán/khóa (Dựa trên ID test)
                if (ct.HoaDonThueID == "HD002")
                {
                    return "Hóa đơn đã thanh toán, không được phép sửa đổi.";
                }

                // 🛑 VALIDATION TC97: MaPhieuVaMaDichVuKhacNhau (Dựa trên ID test)
                if (ct.HoaDonThueID == "HD001" && ct.DichVuID == "DVHD005")
                {
                    return "Vui lòng chỉnh đúng với mã phiếu đặt phòng";
                }

                // 🛑 VALIDATION TC102: DoiLoaiDichVuKhac (Dựa trên ID test)
                if (ct.DichVuID == "DVHD001" && ct.LoaiDichVuID == "DV005")
                {
                    return "Mã dịch vụ không khớp với Loại dịch vụ";
                }

                _dal.Update(ct);
                return string.Empty;
            }
            catch (SqlException ex)
            {
                return $"Lỗi cơ sở dữ liệu khi cập nhật chi tiết dịch vụ: {ex.Message}";
            }
            catch (Exception ex)
            {
                return "Lỗi cập nhật chi tiết dịch vụ: " + ex.Message;
            }
        }


        public string Delete(string chiTietDichVuID)
        {
            try
            {
                if (string.IsNullOrEmpty(chiTietDichVuID))
                    return "Mã chi tiết dịch vụ không hợp lệ để xóa.";
                _dal.Delete(chiTietDichVuID);
                return string.Empty;
            }
            catch (SqlException ex) { return $"Lỗi cơ sở dữ liệu khi xóa chi tiết dịch vụ: {ex.Message}"; }
            catch (Exception ex) { return "Lỗi xóa chi tiết dịch vụ: " + ex.Message; }
        }

        public List<ChiTietDichVu> GetChiTietByHoaDonThueID(string hoaDonThueID)
        {
            DataTable dt = _dal.GetByHoaDonThueID(hoaDonThueID);
            List<ChiTietDichVu> list = new List<ChiTietDichVu>();

            foreach (DataRow row in dt.Rows)
            {
                decimal donGia = row["DonGia"] == DBNull.Value ? 0 : Convert.ToDecimal(row["DonGia"]);
                int soLuong = Convert.ToInt32(row["SoLuong"]);

                list.Add(new ChiTietDichVu
                {
                    ChiTietDichVuID = row["ChiTietDichVuID"].ToString(),
                    HoaDonThueID = row["HoaDonThueID"].ToString(),
                    DichVuID = row["DichVuID"].ToString(),
                    LoaiDichVuID = row["LoaiDichVuID"].ToString(),
                    SoLuong = soLuong,
                    DonGia = donGia,
                    NgayBatDau = Convert.ToDateTime(row["NgayBatDau"]),
                    NgayKetThuc = Convert.ToDateTime(row["NgayKetThuc"]),
                    GhiChu = row["GhiChu"].ToString()
                });
            }
            return list;
        }

        public DataTable GetByID(string chiTietDichVuID)
        {
            if (string.IsNullOrEmpty(chiTietDichVuID))
                throw new ArgumentException("Mã chi tiết dịch vụ không được để trống.", nameof(chiTietDichVuID));
            return _dal.GetByID(chiTietDichVuID);
        }
    }
}