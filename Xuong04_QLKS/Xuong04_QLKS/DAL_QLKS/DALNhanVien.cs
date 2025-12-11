using System;
using System.Collections.Generic;
using System.Data;
using DTO_QLKS;
using Microsoft.Data.SqlClient;

namespace DAL_QLKS
{
    public class DALNhanVien
    {
        public NhanVien getNhanVien(string email, string password)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            var thamSo = new Dictionary<string, object>
            {
                { "@0", email },
                { "@1", password }
            };
            return DBUtil.Value<NhanVien>(sql, thamSo);
        }

        public NhanVien? getNhanVien1(string email, string password)
        {
            string sql = "SELECT TOP 1 * FROM NhanVien WHERE Email=@0 AND MatKhau=@1";
            var thamSo = new Dictionary<string, object>
            {
                { "@0", email },
                { "@1", password }
            };

            DataTable table = DBUtil.Query(sql, thamSo);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                return new NhanVien
                {
                    MaNV = row["MaNV"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    Email = row["Email"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    VaiTro = row["VaiTro"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    TinhTrang = row["TinhTrang"] != DBNull.Value && Convert.ToBoolean(row["TinhTrang"])
                };
            }

            return null;
        }


        public NhanVien getNhanVienByEmail(string email)
        {
            string sql = "SELECT * FROM NhanVien WHERE Email = @0";
            var thamSo = new Dictionary<string, object>
    {
        { "@0", email }
    };

            DataTable table = DBUtil.Query(sql, thamSo);

            if (table.Rows.Count > 0)
            {
                DataRow row = table.Rows[0];
                return new NhanVien
                {
                    MaNV = row["MaNV"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    Email = row["Email"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    VaiTro = row["VaiTro"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    TinhTrang = row["TinhTrang"] != DBNull.Value && Convert.ToBoolean(row["TinhTrang"])
                };
            }

            return null;
        }


        public void ResetMatKhau(string mk, string email)
        {
            string sql = "UPDATE NhanVien SET MatKhau = @0 WHERE Email = @1";
            var thamSo = new Dictionary<string, object>
            {
                { "@0", mk },
                { "@1", email }
            };
            DBUtil.Update(sql, thamSo);
        }

        public List<NhanVien> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text)
        {
            List<NhanVien> list = new List<NhanVien>();
            DataTable table = DBUtil.Query(sql, args ?? new Dictionary<string, object>(), cmdType);

            foreach (DataRow row in table.Rows)
            {
                list.Add(new NhanVien
                {
                    MaNV = row["MaNV"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    GioiTinh = row["GioiTinh"].ToString(),
                    Email = row["Email"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    VaiTro = row["VaiTro"].ToString(),
                    TinhTrang = row["TinhTrang"] != DBNull.Value && Convert.ToBoolean(row["TinhTrang"])
                });
            }

            return list;
        }

        public List<NhanVien> selectAll()
        {
            string sql = @"SELECT MaNV, HoTen, GioiTinh, Email, MatKhau, DiaChi, VaiTro, TinhTrang FROM NhanVien";
            return SelectBySql(sql, new Dictionary<string, object>());
        }

        public NhanVien selectById(string id)
        {
            string sql = @"SELECT MaNV, HoTen, GioiTinh, Email, MatKhau, DiaChi, VaiTro, TinhTrang 
                           FROM NhanVien WHERE MaNV = @0";
            var thamSo = new Dictionary<string, object> { { "@0", id } };
            var list = SelectBySql(sql, thamSo);
            return list.Count > 0 ? list[0] : null;
        }

        public void insertNhanVien(NhanVien nv)
        {
            string sql = @"INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, Email, MatKhau, DiaChi, VaiTro, TinhTrang) 
                           VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            var thamSo = new Dictionary<string, object>
            {
                { "@0", nv.MaNV },
                { "@1", nv.HoTen },
                { "@2", nv.GioiTinh },
                { "@3", nv.Email },
                { "@4", nv.MatKhau },
                { "@5", nv.DiaChi },
                { "@6", nv.VaiTro },
                { "@7", nv.TinhTrang }
            };
            DBUtil.Update(sql, thamSo);
        }

        public void updateNhanVien(NhanVien nv)
        {
            string sql = @"UPDATE NhanVien 
                           SET HoTen = @1, Email = @2, MatKhau = @3, VaiTro = @4, TinhTrang = @5 
                           WHERE MaNV = @0";
            var thamSo = new Dictionary<string, object>
            {
                { "@0", nv.MaNV },
                { "@1", nv.HoTen },
                { "@2", nv.Email },
                { "@3", nv.MatKhau },
                { "@4", nv.VaiTro },
                { "@5", nv.TinhTrang }
            };
            DBUtil.Update(sql, thamSo);
        }

        public void deleteNhanVien(string maNv)
        {
            string sql = "DELETE FROM NhanVien WHERE MaNV = @0";
            var thamSo = new Dictionary<string, object> { { "@0", maNv } };
            DBUtil.Update(sql, thamSo);
        }

        public bool checkEmailExists(string email)
        {
            string sql = "SELECT COUNT(*) FROM NhanVien WHERE Email = @0";
            var thamSo = new Dictionary<string, object> { { "@0", email } };
            object result = DBUtil.ScalarQuery(sql, thamSo);
            return Convert.ToInt32(result) > 0;
        }

        public string generateMaNhanVien()
        {
            string prefix = "NV";
            string sql = "SELECT MAX(CAST(SUBSTRING(MaNV, 3, LEN(MaNV) - 2) AS INT)) FROM NhanVien";
            object result = DBUtil.ScalarQuery(sql, null);

            int newNumber = 1;
            if (result != null && result != DBNull.Value)
            {
                newNumber = Convert.ToInt32(result) + 1;
            }

            return $"{prefix}{newNumber:D3}";
        }



        // Trong DAL_QLKS/DALNhanVien.cs

    //    public NhanVien getNhanVien(string email, string password)
    //    {
    //        // BỔ SUNG ĐIỀU KIỆN TinhTrang = 1 (Hoạt động)
    //        string sql = "SELECT * FROM NhanVien WHERE Email=@0 AND MatKhau=@1 AND TinhTrang = 1";
    //        var thamSo = new Dictionary<string, object>
    //{
    //    { "@0", email },
    //    { "@1", password }
    //};

    //        // Lưu ý: Dù bạn dùng DBUtil.Value<NhanVien> ở BUS/DAL gốc, 
    //        // ta nên dùng logic mapping chi tiết để đảm bảo TinhTrang được xử lý đúng.
    //        DataTable table = DBUtil.Query(sql, thamSo);

    //        if (table.Rows.Count > 0)
    //        {
    //            DataRow row = table.Rows[0];
    //            return new NhanVien
    //            {
    //                MaNV = row["MaNV"].ToString(),
    //                HoTen = row["HoTen"].ToString(),
    //                // ... các thuộc tính khác ...
    //                VaiTro = row["VaiTro"].ToString(),
    //                TinhTrang = Convert.ToBoolean(row["TinhTrang"]) // Lấy TinhTrang
    //            };
    //        }
    //        return null;
    //    }

    }
}
