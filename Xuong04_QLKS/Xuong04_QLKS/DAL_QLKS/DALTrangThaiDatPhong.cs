using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class TrangThaiDatPhongDAL
    {
        public List<TrangThaiDatPhongDTO> LayDanhSach()
        {
            string sql = @"
        SELECT 
            ttdp.TrangThaiID,
            ttdp.HoaDonThueID,
            ttdp.LoaiTrangThaiID,
            lttdp.TenTrangThai,
            ttdp.NgayCapNhat
        FROM TrangThaiDatPhong ttdp
        JOIN LoaiTrangThaiDatPhong lttdp 
            ON ttdp.LoaiTrangThaiID = lttdp.LoaiTrangThaiID";

            return DBUtil.Select<TrangThaiDatPhongDTO>(sql, null);
        }

        public bool Them(TrangThaiDatPhongDTO dto)
        {
            string query = "INSERT INTO TrangThaiDatPhong (TrangThaiID, HoaDonThueID, LoaiTrangThaiID, NgayCapNhat) " +
                           "VALUES (@TrangThaiID, @HoaDonThueID, @LoaiTrangThaiID, @NgayCapNhat)";
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@TrangThaiID", dto.TrangThaiID },
                    { "@HoaDonThueID", dto.HoaDonThueID },
                    { "@LoaiTrangThaiID", dto.LoaiTrangThaiID },
                    { "@NgayCapNhat", dto.NgayCapNhat }
                };

                DBUtil.Update(query, parameters);
                return true;
            }
            catch { return false; }
        }

        public bool CapNhat(TrangThaiDatPhongDTO dto)
        {
            string query = "UPDATE TrangThaiDatPhong SET HoaDonThueID = @HoaDonThueID, LoaiTrangThaiID = @LoaiTrangThaiID, " +
                           "NgayCapNhat = @NgayCapNhat WHERE TrangThaiID = @TrangThaiID";
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@HoaDonThueID", dto.HoaDonThueID },
                    { "@LoaiTrangThaiID", dto.LoaiTrangThaiID },
                    { "@NgayCapNhat", dto.NgayCapNhat },
                    { "@TrangThaiID", dto.TrangThaiID }
                };

                DBUtil.Update(query, parameters);
                return true;
            }
            catch { return false; }
        }

        public bool Xoa(string id)
        {
            string query = "DELETE FROM TrangThaiDatPhong WHERE TrangThaiID = @TrangThaiID";
            try
            {
                var parameters = new Dictionary<string, object>
                {
                    { "@TrangThaiID", id }
                };

                DBUtil.Update(query, parameters);
                return true;
            }
            catch { return false; }
        }

        public List<TrangThaiDatPhongDTO> TimKiemTheoHoaDon(string hoaDonID)
        {
            string query = "SELECT * FROM TrangThaiDatPhong WHERE HoaDonThueID LIKE @HoaDonID";
            var parameters = new Dictionary<string, object>
            {
                { "@HoaDonID", "%" + hoaDonID + "%" }
            };

            return DBUtil.QueryList<TrangThaiDatPhongDTO>(query, parameters);
        }


        public string GenerateNewTrangThaiID()
        {
            DAL_QLKS.TrangThaiDatPhongDAL dal = new DAL_QLKS.TrangThaiDatPhongDAL();
            string prefix = "TTDP";
            string sql = "SELECT MAX(TrangThaiID) FROM TrangThaiDatPhong";
            var args = new Dictionary<string, object>();
            object result = DBUtil.ScalarQuery(sql, args);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(prefix.Length);
                if (int.TryParse(maxCode, out int number))
                {
                    return $"{prefix}{(number + 1):D3}";
                }
            }
            return $"{prefix}001";
        }


    }
}
