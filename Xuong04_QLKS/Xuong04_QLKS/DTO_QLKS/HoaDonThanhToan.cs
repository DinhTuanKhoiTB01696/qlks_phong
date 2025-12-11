using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class HoaDonThanhToan
    {
        public string HoaDonID { get; set; }              // Mã Hóa Đơn Thanh Toán
        public string HoaDonThueID { get; set; }          // Mã Hóa Đơn Thuê (DatPhong.HoaDonThueID)
        public DateTime NgayThanhToan { get; set; }       // Ngày thanh toán
        public string PhuongThucThanhToan { get; set; }   // Phương thức thanh toán (tiền mặt, CK, momo...)
        public string GhiChu { get; set; }                // Ghi chú (nếu có)
        public int TrangThai { get; set; }
        public HoaDonThanhToan() { }

        public HoaDonThanhToan(string hoaDonID, string hoaDonThueID, DateTime ngayThanhToan, string phuongThuc, string ghiChu, int trangThai)
        {
            HoaDonID = hoaDonID;
            HoaDonThueID = hoaDonThueID;
            NgayThanhToan = ngayThanhToan;
            PhuongThucThanhToan = phuongThuc;
            GhiChu = ghiChu;
            TrangThai = trangThai;
        }
    }
}
