using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class TimPhongTrong
    {
        public string PhongID { get; set; }          // <- Đúng kiểu
        public string TenPhong { get; set; }
        public string LoaiPhong { get; set; }
        public string TinhTrang { get; set; }
        public decimal GiaTien { get; set; }
    }
}
