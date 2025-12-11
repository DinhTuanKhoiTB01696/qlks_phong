using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DAL_LoaiPhong
    {
        public List<LoaiPhong> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<LoaiPhong> list = new List<LoaiPhong>();
            try
            {
                DataTable table = DBUtil.Query(sql, args); // Sử dụng Dictionary

                foreach (DataRow row in table.Rows)
                {
                    LoaiPhong entity = new LoaiPhong();
                    entity.MaLoaiPhong = row["MaLoaiPhong"].ToString();
                    entity.TenLoaiPhong = row["TenLoaiPhong"].ToString();
                    entity.NgayTao = row["NgayTao"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["NgayTao"]);
                    entity.TrangThai = row["TrangThai"] == DBNull.Value ? false : Convert.ToBoolean(row["TrangThai"]);
                    entity.GhiChu = row["GhiChu"] == DBNull.Value ? string.Empty : row["GhiChu"].ToString();
                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<LoaiPhong> selectAll()
        {
            String sql = "SELECT * FROM LoaiPhong";
            return SelectBySql(sql, null);
        }

        public string generateMaLoaiPhong()
        {
            string prefix = "LP";
            string sql = "SELECT MAX(MaLoaiPhong) FROM LoaiPhong";
            object result = DBUtil.ScalarQuery(sql, null);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(2); // cắt bỏ 'LP'
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }
            return $"{prefix}001";
        }

        public void insertLoaiPhong(LoaiPhong loaiP)
        {
            try
            {
                string sql = @"INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, NgayTao, TrangThai, GhiChu) 
                               VALUES (@0, @1, @2, @3, @4)";
                var thamSo = new Dictionary<string, object>
                {
                    { "@0", loaiP.MaLoaiPhong },
                    { "@1", loaiP.TenLoaiPhong },
                    { "@2", loaiP.NgayTao },
                    { "@3", loaiP.TrangThai },
                    { "@4", loaiP.GhiChu }
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updateLoaiPhong(LoaiPhong loaiP)
        {
            try
            {
                string sql = @"UPDATE LoaiPhong 
                               SET TenLoaiPhong = @1, NgayTao = @2, TrangThai = @3, GhiChu = @4
                               WHERE MaLoaiPhong = @0";
                var thamSo = new Dictionary<string, object>
                {
                    { "@0", loaiP.MaLoaiPhong },
                    { "@1", loaiP.TenLoaiPhong },
                    { "@2", loaiP.NgayTao },
                    { "@3", loaiP.TrangThai },
                    { "@4", loaiP.GhiChu }
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e) { throw; }
        }

        public void deleteLoaiPhong(string maLP)
        {
            try
            {
                string sql = "DELETE FROM LoaiPhong WHERE MaLoaiPhong = @0";
                var thamSo = new Dictionary<string, object> { { "@0", maLP } };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
