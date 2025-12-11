using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BUSThongKe
    {
        DALThongKe dalThongKe = new DALThongKe();

        public List<TKDoanhThuTheoLoaiPhong> getThongKeLoaiPhong(DateTime ngayBD, DateTime ngayKT)
        {
            return dalThongKe.GetDoanhThuTheoLoaiPhong(ngayBD, ngayKT);
        }


        public List<TKDoanhThuTheoNhanVien> getThongKeTheoNhanVien(DateTime ngayBD, DateTime ngayKT)
        {
            return dalThongKe.GetDoanhThuTheoNhanVien(ngayBD, ngayKT);
        }



    }

}
