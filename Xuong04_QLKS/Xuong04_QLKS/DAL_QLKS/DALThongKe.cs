using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DALThongKe
    {
        public List<TKDoanhThuTheoLoaiPhong> GetDoanhThuTheoLoaiPhong(DateTime tuNgay, DateTime denNgay)
        {
            string sql = "TKDoanhThuTheoLoaiPhong";
            var thamSo = new Dictionary<string, object>
            {
                { "@TuNgay", tuNgay },
                { "@DenNgay", denNgay }
            };

            DataTable dt = DBUtil.Query(sql, thamSo, CommandType.StoredProcedure);
            List<TKDoanhThuTheoLoaiPhong> result = new List<TKDoanhThuTheoLoaiPhong>();

            foreach (DataRow row in dt.Rows)
            {
                TKDoanhThuTheoLoaiPhong tk = new TKDoanhThuTheoLoaiPhong
                {
                    MaLoaiPhong = row["MaLoaiPhong"].ToString(),
                    TenLoaiPhong = row["TenLoaiPhong"].ToString(),

                    // Đã sửa tên cột ở đây
                    SoLuongDat = Convert.ToInt32(row["SoPhieuThue"]),

                    TongDoanhThu = Convert.ToDecimal(row["TongDoanhThu"])
                };
                result.Add(tk);
            }
            return result;
        }



        public List<TKDoanhThuTheoNhanVien> GetDoanhThuTheoNhanVien(DateTime tuNgay, DateTime denNgay)
        {
            string sql = "TKDoanhThuTheoNhanVien";
            var thamSo = new Dictionary<string, object>
    {
        { "@TuNgay", tuNgay },
        { "@DenNgay", denNgay }
    };

            DataTable dt = DBUtil.Query(sql, thamSo, CommandType.StoredProcedure);
            List<TKDoanhThuTheoNhanVien> result = new List<TKDoanhThuTheoNhanVien>();

            foreach (DataRow row in dt.Rows)
            {
                TKDoanhThuTheoNhanVien tk = new TKDoanhThuTheoNhanVien
                {
                    MaNV = row["MaNV"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    SoPhieuThue = Convert.ToInt32(row["SoPhieuThue"]),
                    TongDoanhThu = Convert.ToDecimal(row["TongDoanhThu"])
                };
                result.Add(tk);
            }
            return result;
        }


    }
}
