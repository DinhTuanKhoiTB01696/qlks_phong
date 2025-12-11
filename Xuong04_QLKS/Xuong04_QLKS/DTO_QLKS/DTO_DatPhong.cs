using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class DatPhong
    {
        public string HoaDonThueID { get; set; }
        public string KhachHangID { get; set; }
        public string PhongID { get; set; }
        public DateTime NgayDen { get; set; }
        public DateTime NgayDi { get; set; }
        public string MaNV { get; set; }
        public string GhiChu { get; set; }
        //public string LoaiTrangThaiID { get; set; }
        //public string TenTrangThai { get; set; }
        //public int TrangThai { get; set; } // "Đã thanh toán", "Chưa thanh toán"


    }
}
