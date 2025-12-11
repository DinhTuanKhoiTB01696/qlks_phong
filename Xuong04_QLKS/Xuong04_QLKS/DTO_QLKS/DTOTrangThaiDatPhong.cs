using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class TrangThaiDatPhongDTO
    {
        public string TrangThaiID { get; set; }
        public string HoaDonThueID { get; set; }
        public string LoaiTrangThaiID { get; set; }
        public string TenTrangThai { get; set; }
        public DateTime NgayCapNhat { get; set; }

        public static Dictionary<string, string> ColumnHeaders => new Dictionary<string, string>
    {
        { "TrangThaiID", "Trạng thái" },
        { "HoaDonThueID", "Hóa đơn thuê" },
        { "LoaiTrangThaiID", "Loại trạng thái" },
        { "NgayCapNhat", "Ngày cập nhật" },
        { "TenTrangThai", "Tên trạng thái"   }
    };
    }

}
