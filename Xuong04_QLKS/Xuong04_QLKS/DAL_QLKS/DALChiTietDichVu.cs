using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DTO_QLKS;

namespace DAL_QLKS
{
    public class DALChiTietDichVu : IDALChiTietDichVu
    {
        //public DataTable GetAll()
        //{
        //    string sql = @"
        //SELECT 
        //    ctdv.ChiTietDichVuID,
        //    ctdv.HoaDonThueID,
        //    ctdv.DichVuID,
        //    ctdv.LoaiDichVuID,
        //    ldv.TenDichVu AS TenLoaiDichVu,
        //    ctdv.SoLuong,
        //    ctdv.NgayBatDau,
        //    ctdv.NgayKetThuc,
        //    ctdv.GhiChu
        //FROM ChiTietDichVu ctdv
        //JOIN LoaiDichVu ldv 
        //    ON ctdv.LoaiDichVuID = ldv.LoaiDichVuID";

        //    var thamSo = new Dictionary<string, object>(); // dictionary rỗng vì không có tham số lọc
        //    return DBUtil.Query(sql, thamSo, CommandType.Text);
        //}


        public DataTable GetAll()
        {
            string sql = @"
        SELECT 
            ctdv.ChiTietDichVuID,
            ctdv.HoaDonThueID,
            ctdv.DichVuID,
            ctdv.LoaiDichVuID,
            ldv.TenDichVu AS Tên_Dịch_Vụ,
            ctdv.SoLuong,
            ctdv.NgayBatDau,
            ctdv.NgayKetThuc,
            ctdv.GhiChu
        FROM ChiTietDichVu ctdv
        JOIN LoaiDichVu ldv 
            ON ctdv.LoaiDichVuID = ldv.LoaiDichVuID";

            // Không có tham số nên truyền Dictionary rỗng
            return DBUtil.Query(sql, new Dictionary<string, object>(), CommandType.Text);
        }





        public DataTable GetByHoaDonThueID(string hoaDonThueID)
        {
            string sql = @"SELECT ctdv.*, ldv.TenDichVu, ldv.GiaDichVu AS DonGia, ldv.DonViTinh 
                           FROM ChiTietDichVu ctdv 
                           JOIN LoaiDichVu ldv ON ctdv.LoaiDichVuID = ldv.LoaiDichVuID 
                           WHERE ctdv.HoaDonThueID = @0";

            var thamSo = new Dictionary<string, object> { { "@0", hoaDonThueID } };
            return DBUtil.Query(sql, thamSo, CommandType.Text);
        }


        public DataTable GetByID(string chiTietDichVuID)
        {
            string sql = @"SELECT ctdv.ChiTietDichVuID, ctdv.HoaDonThueID, ctdv.DichVuID,
                          ctdv.LoaiDichVuID, ctdv.SoLuong, ctdv.NgayBatDau,
                          ctdv.NgayKetThuc, ctdv.GhiChu
                   FROM ChiTietDichVu ctdv 
                   WHERE ctdv.ChiTietDichVuID = @0";

            var thamSo = new Dictionary<string, object> { { "@0", chiTietDichVuID } };
            return DBUtil.Query(sql, thamSo, CommandType.Text);
        }





        public string GenerateNextID()
        {
            string prefix = "CTDV";  // Đổi từ CDV sang CTDV
            string sql = "SELECT MAX(CAST(SUBSTRING(ChiTietDichVuID, 5, LEN(ChiTietDichVuID)) AS INT)) " +
                         "FROM ChiTietDichVu WHERE ChiTietDichVuID LIKE 'CTDV%'";

            object result = DBUtil.ScalarQuery(sql, null);
            int nextNumber = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) + 1 : 1;

            return $"{prefix}{nextNumber:D3}";  // Ví dụ: CTDV001, CTDV002
        }

        public void Insert(ChiTietDichVu ct)
        {
            string sql = @"INSERT INTO ChiTietDichVu 
                           (ChiTietDichVuID, HoaDonThueID, DichVuID, LoaiDichVuID, SoLuong, NgayBatDau, NgayKetThuc, GhiChu) 
                           VALUES (@0, @1, @2, @3, @4, @5, @6, @7)";
            var thamSo = new Dictionary<string, object>
            {
                { "@0", ct.ChiTietDichVuID },
                { "@1", ct.HoaDonThueID },
                { "@2", ct.DichVuID },
                { "@3", ct.LoaiDichVuID },
                { "@4", ct.SoLuong },
                { "@5", ct.NgayBatDau },
                { "@6", ct.NgayKetThuc },
                { "@7", ct.GhiChu }
            };
            DBUtil.Update(sql, thamSo);
        }

        public void Update(ChiTietDichVu ct)
        {
            string sql = @"UPDATE ChiTietDichVu 
                           SET HoaDonThueID = @1, DichVuID = @2, LoaiDichVuID = @3, 
                               SoLuong = @4, NgayBatDau = @5, NgayKetThuc = @6, GhiChu = @7 
                           WHERE ChiTietDichVuID = @0";

            var thamSo = new Dictionary<string, object>
            {
                { "@0", ct.ChiTietDichVuID },
                { "@1", ct.HoaDonThueID },
                { "@2", ct.DichVuID },
                { "@3", ct.LoaiDichVuID },
                { "@4", ct.SoLuong },
                { "@5", ct.NgayBatDau },
                { "@6", ct.NgayKetThuc },
                { "@7", ct.GhiChu }
            };
            DBUtil.Update(sql, thamSo);
        }

        public void Delete(string id)
        {
            string sql = "DELETE FROM ChiTietDichVu WHERE ChiTietDichVuID = @0";
            var thamSo = new Dictionary<string, object> { { "@0", id } };
            DBUtil.Update(sql, thamSo);
        }
    }
}
