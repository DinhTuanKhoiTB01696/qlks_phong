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
    public class DAL_Phong
    {
        public List<Phong> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<Phong> list = new List<Phong>();
            try
            {
                DataTable table = DBUtil.Query(sql, args); // đúng kiểu trả về
                foreach (DataRow row in table.Rows)
                {
                    Phong entity = new Phong();
                    entity.PhongID = row["PhongID"].ToString();
                    entity.TenPhong = row["TenPhong"].ToString();
                    entity.MaLoaiPhong = row["MaLoaiPhong"].ToString();
                    entity.GiaPhong = Convert.ToDecimal(row["GiaPhong"]);
                    entity.NgayTao = Convert.ToDateTime(row["NgayTao"]);
                    entity.TinhTrang = Convert.ToBoolean(row["TinhTrang"]);
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

        public List<Phong> selectAll()
        {
            String sql = "SELECT * FROM Phong";
            return SelectBySql(sql, null);
        }

        public Phong selectById(string id)
        {
            String sql = "SELECT * FROM Phong WHERE PhongID = @0";
            var args = new Dictionary<string, object> { { "@0", id } };
            List<Phong> list = SelectBySql(sql, args);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertPhong(Phong p)
        {
            try
            {
                string sql = @"INSERT INTO Phong (PhongID, TenPhong, MaLoaiPhong, GiaPhong, NgayTao, TinhTrang, GhiChu) 
                               VALUES (@0, @1, @2, @3, @4, @5)";
                var args = new Dictionary<string, object>
                {
                    { "@0", p.PhongID },
                    { "@1", p.TenPhong },
                    { "@2", p.MaLoaiPhong },
                    { "@3", p.GiaPhong },
                    { "@4", p.NgayTao },
                    { "@5", p.TinhTrang },
                    { "@6", p.GhiChu }
                };
                DBUtil.Update(sql, args);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void updatePhong(Phong p)
        {
            try
            {
                string sql = @"UPDATE Phong 
                               SET TenPhong = @1, GiaPhong = @2, MaLoaiPhong = @3, NgayTao = @4, TinhTrang = @5, GhiChu = @6
                               WHERE PhongID = @0";
                var args = new Dictionary<string, object>
                {
                    { "@0", p.PhongID },
                    { "@1", p.TenPhong },
                    { "@2", p.MaLoaiPhong },
                    { "@3", p.GiaPhong },
                    { "@4", p.NgayTao },
                    { "@5", p.TinhTrang },
                    { "@6", p.GhiChu }
                };
                DBUtil.Update(sql, args);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void deletePhong(string maP)
        {
            try
            {
                string sql = "DELETE FROM Phong WHERE PhongID = @0";
                var args = new Dictionary<string, object> { { "@0", maP } };
                DBUtil.Update(sql, args);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string generatePhongID()
        {
            string prefix = "P";
            string sql = "SELECT MAX(PhongID) FROM Phong";
            object result = DBUtil.ScalarQuery(sql, null);
            if (result != null && result.ToString().StartsWith(prefix))
            {
                string maxCode = result.ToString().Substring(1); // chỉ lấy sau ký tự 'P'
                int newNumber = int.Parse(maxCode) + 1;
                return $"{prefix}{newNumber:D3}";
            }
            return $"{prefix}001";
        }
        public List<Phong> SelectByTinhTrang(int tinhTrang)
        {
            string sql = "SELECT * FROM Phong WHERE TinhTrang = @0";
            var args = new Dictionary<string, object>
                {
                    { "@0", tinhTrang }
                };
            return SelectBySql(sql, args);
        }



        public void CapNhatTinhTrangPhongTheoHoaDon(string hoaDonThueID, bool tinhTrang)
        {
            string sql = @"
            UPDATE Phong
            SET TinhTrang = @TinhTrang
            WHERE PhongID = (
                SELECT PhongID FROM HoaDonThue WHERE HoaDonThueID = @HoaDonThueID
            )";

            var parameters = new Dictionary<string, object>
        {
            { "@TinhTrang", tinhTrang },
            { "@HoaDonThueID", hoaDonThueID }
        };

            DBUtil.Update(sql, parameters);
        }



        public void CapNhatTinhTrang(string phongID, bool tinhTrang)
        {
            string sql = @"UPDATE Phong SET TinhTrang = @0 WHERE PhongID = @1";
            var args = new Dictionary<string, object>
    {
        { "@0", tinhTrang },
        { "@1", phongID }
    };
            DBUtil.Update(sql, args);
        }



    }
}
