using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DALLoaiDichVu
    {
        public List<LoaiDichVu> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<LoaiDichVu> list = new List<LoaiDichVu>();
            try
            {
                DataTable table = DBUtil.Query(sql, args);

                foreach (DataRow row in table.Rows)
                {
                    LoaiDichVu entity = new LoaiDichVu();
                    entity.LoaiDichVuID = row["LoaiDichVuID"].ToString();
                    entity.TenDichVu = row["TenDichVu"].ToString();
                    entity.GiaDichVu = Convert.ToDecimal(row["GiaDichVu"]);
                    entity.DonViTinh = row["DonViTinh"].ToString();
                    entity.NgayTao = Convert.ToDateTime(row["NgayTao"]);
                    entity.TrangThai = Convert.ToBoolean(row["TrangThai"]);
                    entity.GhiChu = row["GhiChu"].ToString();

                    list.Add(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public List<LoaiDichVu> selectAll()
        {
            string sql = "SELECT * FROM LoaiDichVu";
            return SelectBySql(sql, null);
        }

        public LoaiDichVu selectById(string id)
        {
            string sql = "SELECT * FROM LoaiDichVu WHERE LoaiDichVuID = @0";
            var thamSo = new Dictionary<string, object> { { "@0", id } };
            List<LoaiDichVu> list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertLoaiDichVu(LoaiDichVu ldv)
        {
            try
            {
                string sql = @"INSERT INTO LoaiDichVu 
                (LoaiDichVuID, TenDichVu, GiaDichVu, DonViTinh, NgayTao, TrangThai, GhiChu) 
                VALUES (@0, @1, @2, @3, @4, @5, @6)";

                var thamSo = new Dictionary<string, object>
                {
                    { "@0", ldv.LoaiDichVuID },
                    { "@1", ldv.TenDichVu },
                    { "@2", ldv.GiaDichVu },
                    { "@3", ldv.DonViTinh },
                    { "@4", DateTime.Now },
                    { "@5", ldv.TrangThai },
                    { "@6", ldv.GhiChu }
                };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updateLoaiDichVu(LoaiDichVu ldv)
        {
            try
            {
                string sql = @"UPDATE LoaiDichVu
                SET TenDichVu = @1, GiaDichVu = @2, DonViTinh = @3, NgayTao = @4, TrangThai = @5, GhiChu = @6
                WHERE LoaiDichVuID = @0";

                var thamSo = new Dictionary<string, object>
                {
                    { "@0", ldv.LoaiDichVuID },
                    { "@1", ldv.TenDichVu },
                    { "@2", ldv.GiaDichVu },
                    { "@3", ldv.DonViTinh },
                    { "@4", ldv.NgayTao },
                    { "@5", ldv.TrangThai },
                    { "@6", ldv.GhiChu }
                };

                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void deleteLoaiDichVu(string loaiDichVuID)
        {
            try
            {
                string sql = "DELETE FROM LoaiDichVu WHERE LoaiDichVuID = @0";
                var thamSo = new Dictionary<string, object> { { "@0", loaiDichVuID } };
                DBUtil.Update(sql, thamSo);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string generateLoaiDichVuID()
        {
            string prefix = "DV";
            string sql = "SELECT MAX(LoaiDichVuID) FROM LoaiDichVu";
            object result = DBUtil.ScalarQuery(sql, null);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(prefix.Length);
                if (int.TryParse(maxCode, out int number))
                {
                    int newNumber = number + 1;
                    return $"{prefix}{newNumber:D3}";
                }
            }
            return $"{prefix}001";
        }
    }
}
