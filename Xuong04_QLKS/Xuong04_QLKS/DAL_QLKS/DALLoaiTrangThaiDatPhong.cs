using System;
using System.Collections.Generic;
using DTO_QLKS;

namespace DAL_QLKS
{
    public class LoaiTrangThaiDatPhongDAL
    {
        public List<LoaiTrangThaiDatPhongDTO> LayDanhSach()
        {
            string query = "SELECT * FROM LoaiTrangThaiDatPhong";
            return DBUtil.Select<LoaiTrangThaiDatPhongDTO>(query, new Dictionary<string, object>());
        }

        public bool Them(LoaiTrangThaiDatPhongDTO trangThai)
        {
            string query = "INSERT INTO LoaiTrangThaiDatPhong (LoaiTrangThaiID, TenTrangThai) VALUES (@LoaiTrangThaiID, @TenTrangThai)";
            var parameters = new Dictionary<string, object>
        {
            { "@LoaiTrangThaiID", trangThai.LoaiTrangThaiID },
            { "@TenTrangThai", trangThai.TenTrangThai }
        };
            return DBUtil.Update(query, parameters) > 0;
        }

        public bool CapNhat(LoaiTrangThaiDatPhongDTO trangThai)
        {
            string query = "UPDATE LoaiTrangThaiDatPhong SET TenTrangThai = @TenTrangThai WHERE LoaiTrangThaiID = @LoaiTrangThaiID";
            var parameters = new Dictionary<string, object>
        {
            { "@LoaiTrangThaiID", trangThai.LoaiTrangThaiID },
            { "@TenTrangThai", trangThai.TenTrangThai }
        };
            return DBUtil.Update(query, parameters) > 0;
        }

        public bool Xoa(string id)
        {
            string query = "DELETE FROM LoaiTrangThaiDatPhong WHERE LoaiTrangThaiID = @LoaiTrangThaiID";
            var parameters = new Dictionary<string, object>
        {
            { "@LoaiTrangThaiID", id }
        };
            return DBUtil.Update(query, parameters) > 0;
        }

        public LoaiTrangThaiDatPhongDTO LayTheoID(string id)
        {
            string query = "SELECT * FROM LoaiTrangThaiDatPhong WHERE LoaiTrangThaiID = @LoaiTrangThaiID";
            var parameters = new Dictionary<string, object>
        {
            { "@LoaiTrangThaiID", id }
        };
            return DBUtil.SelectOne<LoaiTrangThaiDatPhongDTO>(query, parameters);
        }



        public List<LoaiTrangThaiDatPhongDTO> LayTrangThaiThanhToan()
        {
            string query = "SELECT * FROM LoaiTrangThaiDatPhong WHERE TenTrangThai IN (N'Đã thanh toán', N'Chưa thanh toán')";
            return DBUtil.Select<LoaiTrangThaiDatPhongDTO>(query, new Dictionary<string, object>());
        }

    }

}

