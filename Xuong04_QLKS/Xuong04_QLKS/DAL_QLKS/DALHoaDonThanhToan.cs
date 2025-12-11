using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO_QLKS;

namespace DAL_QLKS
{
    public class DALHoaDonThanhToan
    {
        public List<HoaDonThanhToan> GetAll()
        {
            string sql = "SELECT HoaDonID, HoaDonThueID, NgayThanhToan, PhuongThucThanhToan, GhiChu, TrangThai FROM HoaDonThanhToan";
            return DBUtil.Select<HoaDonThanhToan>(sql, null);
        }


        public List<DatPhong> GetAllDatPhongChuaThanhToan()
        {
            string query = @"
        SELECT * FROM DatPhong
        WHERE HoaDonThueID NOT IN 
              (SELECT HoaDonThueID FROM HoaDonThanhToan WHERE TrangThai = 1)";
            return DBUtil.ExecuteQuery<DatPhong>(query);
        }



        public HoaDonThanhToan GetByID(string hoaDonID)
        {
            string sql = "SELECT * FROM HoaDonThanhToan WHERE HoaDonID = @HoaDonID";
            var args = new Dictionary<string, object> { { "HoaDonID", hoaDonID } };
            return DBUtil.SelectOne<HoaDonThanhToan>(sql, args);
        }

        public void Insert(HoaDonThanhToan entity)
        {
            string sql = @"
            INSERT INTO HoaDonThanhToan 
                (HoaDonID, HoaDonThueID, NgayThanhToan, PhuongThucThanhToan, GhiChu)
            VALUES 
                (@HoaDonID, @HoaDonThueID, @NgayThanhToan, @PhuongThucThanhToan, @GhiChu)";

            var args = new Dictionary<string, object>
        {
            { "HoaDonID", entity.HoaDonID },
            { "HoaDonThueID", entity.HoaDonThueID },
            { "NgayThanhToan", entity.NgayThanhToan },
            { "PhuongThucThanhToan", entity.PhuongThucThanhToan },
            { "GhiChu", entity.GhiChu }
        };

            DBUtil.Update(sql, args);
        }

        public void Update(HoaDonThanhToan entity)
        {
            string sql = @"
            UPDATE HoaDonThanhToan
            SET HoaDonThueID = @HoaDonThueID,
                NgayThanhToan = @NgayThanhToan,
                PhuongThucThanhToan = @PhuongThucThanhToan,
                GhiChu = @GhiChu
            WHERE HoaDonID = @HoaDonID";

            var args = new Dictionary<string, object>
        {
            { "HoaDonID", entity.HoaDonID },
            { "HoaDonThueID", entity.HoaDonThueID },
            { "NgayThanhToan", entity.NgayThanhToan },
            { "PhuongThucThanhToan", entity.PhuongThucThanhToan },
            { "GhiChu", entity.GhiChu }
        };

            DBUtil.Update(sql, args);
        }

        public void Delete(string hoaDonID)
        {
            string sql = "DELETE FROM HoaDonThanhToan WHERE HoaDonID = @HoaDonID";
            var args = new Dictionary<string, object> { { "HoaDonID", hoaDonID } };
            DBUtil.Update(sql, args);
        }



        public string GenerateNewHoaDonID()
        {
            // Chỉ tìm các mã bắt đầu bằng HDTT
            string sql = "SELECT MAX(HoaDonID) FROM HoaDonThanhToan WHERE HoaDonID LIKE 'HDTT%'";
            object result = DBUtil.ScalarQuery(sql, null);

            string lastID = result?.ToString()?.Trim();

            if (string.IsNullOrEmpty(lastID))
            {
                return "HDTT001"; // Nếu chưa có mã nào
            }

            // Lấy phần số sau 'HDTT'
            string numberPart = lastID.Substring(4); // bỏ 'HDTT'
            if (!int.TryParse(numberPart, out int number))
            {
                number = 0;
            }

            number++; // Tăng mã

            return "HDTT" + number.ToString("D3"); // Ví dụ: HDTT006
        }



        public List<HoaDonThanhToan> GetByTrangThai(int trangThai)
        {
            string sql = "SELECT * FROM HoaDonThanhToan WHERE TrangThai = @TrangThai";
            var args = new Dictionary<string, object> { { "TrangThai", trangThai } };
            return DBUtil.Select<HoaDonThanhToan>(sql, args);
        }


        public bool CapNhat(HoaDonThanhToan hd)
        {
            string sql = @"UPDATE HoaDonThanhToan
                   SET HoaDonThueID = @1, NgayThanhToan = @2, 
                       PhuongThucThanhToan = @3, GhiChu = @4, TrangThai = @5
                   WHERE HoaDonID = @0";

            var param = new Dictionary<string, object>
    {
        {"@0", hd.HoaDonID},
        {"@1", hd.HoaDonThueID},
        {"@2", hd.NgayThanhToan},
        {"@3", hd.PhuongThucThanhToan},
        {"@4", hd.GhiChu},
        {"@5", hd.TrangThai}
    };

            return DBUtil.Update(sql, param) > 0;
        }


        public bool CapNhatTrangThai(string hoaDonThueID, int trangThai)
        {
            string query = "UPDATE HoaDonThanhToan SET TrangThai = @TrangThai WHERE HoaDonThueID = @HoaDonThueID";

            var parameters = new Dictionary<string, object>
    {
        { "@TrangThai", trangThai },
        { "@HoaDonThueID", hoaDonThueID }
    };

            return DBUtil.Update(query, parameters, CommandType.Text) > 0;
        }
        public HoaDonThanhToan selectById(string id)
        {
            return GetByID(id);
        }

    }


}
