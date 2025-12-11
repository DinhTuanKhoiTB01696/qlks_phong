using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DAL_TimPhongTrong
    {
        public List<TimPhongTrong> LayDanhSachPhongTrong()
        {
            string sql = @"
                SELECT 
                    P.PhongID, 
                    P.TenPhong, 
                    LP.TenLoaiPhong AS LoaiPhong, 
                    N'Trống' AS TinhTrang, -- chỉ lấy phòng trống nên cố định luôn
                    P.GiaPhong AS GiaTien
                FROM Phong P
                JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
                WHERE P.TinhTrang = 0";

            try
            {
                return DBUtil.ExecuteQuery<TimPhongTrong>(sql);
            }
            catch (Exception ex)
            {
                // Ghi log hoặc xử lý lỗi phù hợp
                Console.WriteLine("Lỗi lấy danh sách phòng trống: " + ex.Message);
                return new List<TimPhongTrong>();
            }

        }

        public List<TimPhongTrong> TimPhongTheoNgay(DateTime tuNgay, DateTime denNgay)
        {
            string sql = @"
    SELECT 
        P.PhongID, 
        P.TenPhong, 
        LP.TenLoaiPhong AS LoaiPhong, 
        N'Trống' AS TinhTrang,
        P.GiaPhong AS GiaTien
    FROM Phong P
    JOIN LoaiPhong LP ON P.MaLoaiPhong = LP.MaLoaiPhong
    WHERE P.TinhTrang = 0
      AND P.PhongID NOT IN (
        SELECT dp.PhongID
        FROM DatPhong dp
        JOIN TrangThaiDatPhong ttdp ON dp.HoaDonThueID = ttdp.HoaDonThueID
        JOIN LoaiTrangThaiDatPhong lttdp ON ttdp.LoaiTrangThaiID = lttdp.LoaiTrangThaiID
        WHERE 
            (dp.NgayDen <= @DenNgay AND dp.NgayDi >= @TuNgay)
            AND lttdp.TenTrangThai != N'Hủy'
            AND ttdp.NgayCapNhat = (
                SELECT MAX(tt2.NgayCapNhat)
                FROM TrangThaiDatPhong tt2
                WHERE tt2.HoaDonThueID = dp.HoaDonThueID
            )
    )";

            var args = new Dictionary<string, object>
    {
        { "@TuNgay", tuNgay },
        { "@DenNgay", denNgay }
    };

            try
            {
                return DBUtil.ExecuteQuery<TimPhongTrong>(sql, args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm phòng theo ngày: " + ex.Message);
                return new List<TimPhongTrong>();
            }
        }


    }
}


