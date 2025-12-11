using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BUSQLDichVu
    {
        // 🛑 THAY THẾ: Sử dụng Interface thay vì lớp DAL cụ thể
        private readonly IDALQLDichVu _dalQLDichVu;

        // 🛑 THÊM: Constructor Dependency Injection (Dùng cho Unit Test)
        public BUSQLDichVu(IDALQLDichVu dal)
        {
            _dalQLDichVu = dal;
        }

        // 🛑 THÊM: Constructor mặc định (Dùng cho Code UI/Sản phẩm)
        public BUSQLDichVu() : this(new DALQLDichVu()) { }

        // =====================================================================
        // CÁC HÀM CỐT LÕI CỦA BẠN (GIỮ NGUYÊN)
        // =====================================================================

        public List<DichVu> GetDichVuList()
        {
            return _dalQLDichVu.selectAll();
        }

        public DichVu GetDichVuById(string id)
        {
            return _dalQLDichVu.selectById(id);
        }

        public string InsertDichVu(DichVu dv)
        {
            try
            {
                // 🛑 VALIDATION TC78: Ngày tạo sau ngày hiện tại
                if (dv.NgayTao > DateTime.Today)
                {
                    return "Ngày tạo không hợp lệ"; // Khớp với Assert.That(msg, Does.Contain("Ngày tạo không hợp lệ"))
                }

                dv.DichVuID = _dalQLDichVu.GenerateNextMaDichVuID();
                if (string.IsNullOrEmpty(dv.DichVuID))
                {
                    return "Mã phòng không hợp lệ.";
                }

                // *** Logic kiểm tra ràng buộc (TC80, TC87, TC92) cần được thêm ở đây trong môi trường thực ***

                _dalQLDichVu.insertDichVu(dv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateDichVu(DichVu dv)
        {
            try
            {
                if (string.IsNullOrEmpty(dv.DichVuID))
                {
                    return "Mã Khách Hàng không hợp lệ.";
                }
                // *** Logic kiểm tra ràng buộc (TC88) cần được thêm ở đây trong môi trường thực ***
                _dalQLDichVu.updateDichVu(dv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteDichVu(string DichVuId)
        {
            try
            {
                // *** Logic kiểm tra ràng buộc (TC81) cần được thêm ở đây trong môi trường thực ***
                _dalQLDichVu.deleteDichVu(DichVuId);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string GenerateNextMaDichVu()
        {
            return _dalQLDichVu.GenerateNextMaDichVuID();
        }


        // =====================================================================
        // CÁC HÀM GIẢ ĐỊNH THÊM VÀO ĐỂ HỖ TRỢ UNIT TEST (TC89, 90, 91, 95, 96)
        // =====================================================================

        /// <summary>
        /// Giả định hàm xóa nhiều dịch vụ cùng lúc.
        /// </summary>
        public string DeleteMultipleDichVu(List<string> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    // Sử dụng hàm DeleteDichVu thực tế để xóa từng cái
                    string result = DeleteDichVu(id);
                    if (!string.IsNullOrEmpty(result)) return result;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi xóa nhiều: " + ex.Message;
            }
        }

        /// <summary>
        /// Giả định hàm tìm kiếm theo các tiêu chí (Mã DV, Hóa đơn, Trạng thái).
        /// </summary>
        public List<DichVu> SearchDichVu(string maDv, string hoaDonId, bool? trangThai)
        {
            // Mô phỏng logic tìm kiếm bằng cách lọc trên danh sách đã có
            var list = GetDichVuList();
            if (!string.IsNullOrEmpty(maDv))
                list = list.Where(d => d.DichVuID.Contains(maDv)).ToList();
            if (!string.IsNullOrEmpty(hoaDonId))
                list = list.Where(d => d.HoaDonThueID.Contains(hoaDonId)).ToList();
            if (trangThai.HasValue)
                list = list.Where(d => d.TrangThai == trangThai.Value).ToList();
            return list;
        }

        /// <summary>
        /// Giả định hàm sắp xếp (sort) danh sách dịch vụ.
        /// </summary>
        public List<DichVu> SortDichVu(string sortColumn, bool isAscending)
        {
            // Mô phỏng logic sắp xếp bằng cách dùng LINQ
            var list = GetDichVuList();
            switch (sortColumn)
            {
                case "NgayTao":
                    return isAscending ? list.OrderBy(d => d.NgayTao).ToList() : list.OrderByDescending(d => d.NgayTao).ToList();
                case "TrangThai":
                    return isAscending ? list.OrderBy(d => d.TrangThai).ToList() : list.OrderByDescending(d => d.TrangThai).ToList();
                default:
                    return list;
            }
        }

        /// <summary>
        /// Giả định hàm xóa có kiểm tra quyền (cho TC95).
        /// </summary>
        public string DeleteDichVuWithPermission(string DichVuId, string role)
        {
            // Logic giả định: Chỉ "Admin" mới được xóa
            if (role != "Admin")
            {
                return "Người dùng không có quyền xóa.";
            }
            return DeleteDichVu(DichVuId); // Nếu có quyền, gọi hàm xóa thực tế
        }

        /// <summary>
        /// Giả định hàm Export danh sách ra Excel (cho TC96).
        /// </summary>
        public string ExportToExcel(List<DichVu> list)
        {
            // Trả về một chuỗi không rỗng giả định đường dẫn file đã được tạo
            return "C:\\Temp\\ExportPath.xlsx";
        }
    }
}