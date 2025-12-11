using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DALKhachHang
    {
        public List<KhachHang> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<KhachHang> list = new List<KhachHang>();
            try
            {
                DataTable table = DBUtil.Query(sql, args);

                foreach (DataRow row in table.Rows)
                {
                    KhachHang entity = new KhachHang();
                    entity.KhachHangID = row["KhachHangID"].ToString();
                    entity.HoTen = row["HoTen"].ToString();
                    entity.DiaChi = row["DiaChi"].ToString();
                    entity.GioiTinh = row["GioiTinh"].ToString();
                    entity.SoDienThoai = row["SoDienThoai"].ToString();
                    entity.CCCD = row["CCCD"].ToString();

                    entity.NgayTao = row["NgayTao"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(row["NgayTao"]);

                    entity.GhiChu = row["GhiChu"]?.ToString() ?? "";

                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<KhachHang> selectAll()
        {
            string sql = "SELECT * FROM KhachHang";
            return SelectBySql(sql, null);
        }

        public KhachHang selectById(string id)
        {
            string sql = "SELECT * FROM KhachHang WHERE KhachHangID = @0";
            var thamSo = new Dictionary<string, object> { { "@0", id } };
            List<KhachHang> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertKhachHang(KhachHang kH)
        {
            try
            {
                string sql = @"INSERT INTO KhachHang (KhachHangID, HoTen, DiaChi, GioiTinh, SoDienThoai, CCCD, NgayTao, GhiChu) 
                               VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
                var thamSo = new Dictionary<string, object>
                {
                    { "@0", kH.KhachHangID },
                    { "@1", kH.HoTen },
                    { "@2", kH.DiaChi },
                    { "@3", kH.GioiTinh },
                    { "@4", kH.SoDienThoai },
                    { "@5", kH.CCCD },
                    { "@6", kH.NgayTao },
                    { "@7", kH.GhiChu }
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updateKhachHang(KhachHang kH)
        {
            try
            {
                string sql = @"UPDATE KhachHang
                               SET HoTen = @1, DiaChi = @2, GioiTinh = @3, SoDienThoai = @4, CCCD = @5, NgayTao = @6, GhiChu = @7
                               WHERE KhachHangID = @0";
                var thamSo = new Dictionary<string, object>
                {
                    { "@0", kH.KhachHangID },
                    { "@1", kH.HoTen },
                    { "@2", kH.DiaChi },
                    { "@3", kH.GioiTinh },
                    { "@4", kH.SoDienThoai },
                    { "@5", kH.CCCD },
                    { "@6", kH.NgayTao },
                    { "@7", kH.GhiChu }
                };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void deleteKhachHang(string KhachHangID)
        {
            try
            {
                string sql = "DELETE FROM KhachHang WHERE KhachHangID = @0";
                var thamSo = new Dictionary<string, object> { { "@0", KhachHangID } };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string generateKhachHangID()
        {
            string prefix = "KH";
            string sql = "SELECT MAX(KhachHangID) FROM KhachHang";
            object result = DBUtil.ScalarQuery(sql, null);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(2);
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }

            return $"{prefix}001";
        }
    }
}
