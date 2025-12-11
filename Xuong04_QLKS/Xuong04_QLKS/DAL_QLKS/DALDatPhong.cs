using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DTO_QLKS;

namespace DAL_QLKS
{
    public class DALDatPhong : IDALDatPhong
    {
        // code DAL của bạn để nguyên không đổi


        public List<DatPhong> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<DatPhong> list = new List<DatPhong>();
            try
            {
                DataTable dt = DBUtil.Query(sql, args, cmdType);
                foreach (DataRow row in dt.Rows)
                {
                    DatPhong entity = new DatPhong
                    {
                        HoaDonThueID = row["HoaDonThueID"].ToString(),
                        KhachHangID = row["KhachHangID"].ToString(),
                        PhongID = row["PhongID"].ToString(),
                        NgayDen = Convert.ToDateTime(row["NgayDen"]),
                        NgayDi = Convert.ToDateTime(row["NgayDi"]),
                        MaNV = row["MaNV"].ToString(),
                        GhiChu = row["GhiChu"].ToString(),
                    };
                    list.Add(entity);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy dữ liệu đặt phòng: " + ex.Message, ex);
            }
            return list;
        }



        public List<DatPhong> selectAll()
        {
            string sql = @"
        SELECT dp.*
        FROM DatPhong dp
        LEFT JOIN (
            SELECT HoaDonThueID
            FROM TrangThaiDatPhong
            WHERE NgayCapNhat = (
                SELECT MAX(NgayCapNhat)
                FROM TrangThaiDatPhong AS innerT
                WHERE innerT.HoaDonThueID = TrangThaiDatPhong.HoaDonThueID
            )
        ) ttdp ON dp.HoaDonThueID = ttdp.HoaDonThueID";

            return SelectBySql(sql, new Dictionary<string, object>());
        }


        public DatPhong selectById(string id)
        {
            string sql = "SELECT * FROM DatPhong WHERE HoaDonThueID = @HoaDonThueID";
            var args = new Dictionary<string, object>
            {
                { "@HoaDonThueID", id }
            };
            var list = SelectBySql(sql, args);
            return list.Count > 0 ? list[0] : null;
        }

        public bool insertDatPhong(DatPhong kH)
        {
            string sql = @"
        INSERT INTO DatPhong (HoaDonThueID, KhachHangID, PhongID, NgayDen, NgayDi, MaNV, GhiChu)
        VALUES (@HoaDonThueID, @KhachHangID, @PhongID, @NgayDen, @NgayDi, @MaNV, @GhiChu)";

            var args = new Dictionary<string, object>
    {
        { "@HoaDonThueID", kH.HoaDonThueID },
        { "@KhachHangID", kH.KhachHangID },
        { "@PhongID", kH.PhongID },
        { "@NgayDen", kH.NgayDen },
        { "@NgayDi", kH.NgayDi },
        { "@MaNV", kH.MaNV },
        { "@GhiChu", kH.GhiChu },
    };

            int rowsAffected = DBUtil.Update(sql, args);
            return rowsAffected > 0;
        }



        public void updateDatPhong(DatPhong kH)
        {
            string sql = @"
    UPDATE DatPhong
    SET KhachHangID = @KhachHangID,
        PhongID = @PhongID,
        NgayDen = @NgayDen,
        NgayDi = @NgayDi,
        MaNV = @MaNV,
        GhiChu = @GhiChu
    WHERE HoaDonThueID = @HoaDonThueID";

            var args = new Dictionary<string, object>
{
    { "@HoaDonThueID", kH.HoaDonThueID },
    { "@KhachHangID", kH.KhachHangID },
    { "@PhongID", kH.PhongID },
    { "@NgayDen", kH.NgayDen },
    { "@NgayDi", kH.NgayDi },
    { "@MaNV", kH.MaNV },
    { "@GhiChu", kH.GhiChu },
};

            DBUtil.Update(sql, args);
        }

        public void deleteDatPhong(string hoaDonThueID)
        {
            string sql = "DELETE FROM DatPhong WHERE HoaDonThueID = @HoaDonThueID";
            var args = new Dictionary<string, object>
            {
                { "@HoaDonThueID", hoaDonThueID }
            };
            DBUtil.Update(sql, args);
        }

        public string generateHoaDonThueID()
        {
            string sql = "SELECT MAX(HoaDonThueID) FROM DatPhong WHERE HoaDonThueID LIKE 'HD%'";
            var args = new Dictionary<string, object>();
            object result = DBUtil.ScalarQuery(sql, args);

            int number = 0;

            if (result != null && result.ToString().StartsWith("HD"))
            {
                string code = result.ToString();
                string numPart = code.Substring(2); // Bỏ "HD"
                int.TryParse(numPart, out number);
            }

            return $"HD{(number + 1):D3}";
        }




        public List<DatPhong> GetDatPhongByPhongID(string phongID)
        {
            List<DatPhong> list = new List<DatPhong>();
            using (SqlConnection con = new SqlConnection(DBUtil.connString))
            {
                string query = "SELECT * FROM DatPhong WHERE PhongID = @PhongID AND TrangThai != N'Hủy'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PhongID", phongID);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DatPhong dp = new DatPhong
                    {
                        HoaDonThueID = reader["HoaDonThueID"].ToString(),
                        KhachHangID = reader["KhachHangID"].ToString(),
                        PhongID = reader["PhongID"].ToString(),
                        NgayDen = Convert.ToDateTime(reader["NgayDen"]),
                        NgayDi = Convert.ToDateTime(reader["NgayDi"]),
                        MaNV = reader["MaNV"].ToString(),
                        GhiChu = reader["GhiChu"]?.ToString() // tránh lỗi null
                    };
                    list.Add(dp);
                }
                reader.Close();
            }
            return list;
        }






        public DatPhongView GetThongTinDatPhongChiTiet(string hoaDonThueID)
        {
            string sql = @"
        SELECT dp.HoaDonThueID, kh.HoTen AS TenKH, p.TenPhong, p.GiaPhong, dp.NgayDen, dp.NgayDi
        FROM DatPhong dp
        JOIN KhachHang kh ON dp.KhachHangID = kh.KhachHangID
        JOIN Phong p ON dp.PhongID = p.PhongID
        WHERE dp.HoaDonThueID = @HoaDonThueID";

            var args = new Dictionary<string, object>
    {
        { "@HoaDonThueID", hoaDonThueID }
    };

            DataTable dt = DBUtil.Query(sql, args);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                return new DatPhongView
                {
                    HoaDonThueID = row["HoaDonThueID"].ToString(),
                    TenKH = row["TenKH"].ToString(),
                    TenPhong = row["TenPhong"].ToString(),
                    GiaPhong = Convert.ToDecimal(row["GiaPhong"]),
                    NgayDen = Convert.ToDateTime(row["NgayDen"]),
                    NgayDi = Convert.ToDateTime(row["NgayDi"])
                };
            }
            return null;
        }




        public void CapNhatTinhTrangHuy(string hoaDonThueID)
        {
            string sql = @"
        INSERT INTO TrangThaiDatPhong (HoaDonThueID, LoaiTrangThaiID, NgayCapNhat)
        VALUES (@HoaDonThueID, 'TT002', GETDATE())";

            var args = new Dictionary<string, object>
    {
        { "@HoaDonThueID", hoaDonThueID }
    };

            DBUtil.Update(sql, args);
        }






        public bool KiemTraPhongDaDuocDat(string phongID, DateTime ngayDen, DateTime ngayDi)
        {
            string sql = @"
        SELECT COUNT(*)
        FROM DatPhong dp
        JOIN TrangThaiDatPhong ttdp ON dp.HoaDonThueID = ttdp.HoaDonThueID
        JOIN (
            SELECT HoaDonThueID, MAX(NgayCapNhat) AS NgayMoiNhat
            FROM TrangThaiDatPhong
            GROUP BY HoaDonThueID
        ) latest ON latest.HoaDonThueID = ttdp.HoaDonThueID AND latest.NgayMoiNhat = ttdp.NgayCapNhat
        WHERE dp.PhongID = @PhongID
          AND ttdp.LoaiTrangThaiID != 'TT002' -- bỏ những đơn đã hủy
          AND NOT (dp.NgayDi <= @NgayDen OR dp.NgayDen >= @NgayDi)";

            var args = new Dictionary<string, object>
    {
        { "@PhongID", phongID },
        { "@NgayDen", ngayDen },
        { "@NgayDi", ngayDi }
    };

            object result = DBUtil.ScalarQuery(sql, args);
            int count = Convert.ToInt32(result);
            return count > 0;
        }




        public bool KiemTraPhongDaDuocDat_SP(string phongID, DateTime ngayDen, DateTime ngayDi)
        {
            string sql = "sp_KiemTraPhongDaDuocDat";
            var args = new Dictionary<string, object>
    {
        { "@PhongID", phongID },
        { "@NgayDen", ngayDen },
        { "@NgayDi", ngayDi }
    };
            object result = DBUtil.ScalarQuery(sql, args, CommandType.StoredProcedure);
            return Convert.ToInt32(result) > 0;
        }




    }
}
