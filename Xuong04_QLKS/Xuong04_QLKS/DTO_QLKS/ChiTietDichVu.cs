using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class ChiTietDichVu
    {
        public string ChiTietDichVuID { get; set; }
        public string HoaDonThueID { get; set; }
        public string DichVuID { get; set; }
        public string LoaiDichVuID { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public string GhiChu { get; set; }


        public decimal ThanhTien
        {
            get { return SoLuong * DonGia; }
        }

        public string TenDichVu { get; set; }

    }
}