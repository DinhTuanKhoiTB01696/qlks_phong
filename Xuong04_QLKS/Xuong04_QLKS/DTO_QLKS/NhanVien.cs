using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class NhanVien
    {
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public string VaiTro { get; set; }
        public bool TinhTrang { get; set; }

        public string GioiTinhText => GioiTinh?.ToLower() == "nam" ? "Nam" :
                                      GioiTinh?.ToLower() == "nữ" ? "Nữ" : "Khác";
        
        public string TinhTrangText => TinhTrang ? "Đang Hoạt Động" : "Tạm Ngưng";
    }
}
