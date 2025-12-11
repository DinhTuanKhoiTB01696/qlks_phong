using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class LoaiDichVu
    {
        public string LoaiDichVuID { get; set; }
        public string TenDichVu { get; set; }
        public decimal GiaDichVu { get; set; }
        public string DonViTinh { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }
    }

}
