using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLKS
{
    public class DichVu
    {
        public string DichVuID { get; set; }
        public string HoaDonThueID { get; set; }
        public DateTime NgayTao { get; set; }
        public bool TrangThai { get; set; }
        public string GhiChu { get; set; }

    }
}
