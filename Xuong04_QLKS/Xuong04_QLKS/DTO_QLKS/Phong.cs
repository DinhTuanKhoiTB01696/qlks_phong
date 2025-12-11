using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class Phong
    {
        public string PhongID { get; set; }
        public string TenPhong { get; set; }
        public string MaLoaiPhong { get; set; }
        public decimal GiaPhong { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TinhTrang { get; set; }
        public string GhiChu { get; set; }
    }
}
