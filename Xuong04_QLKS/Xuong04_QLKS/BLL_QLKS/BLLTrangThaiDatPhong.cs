using DAL_QLKS;
using DTO_QLKS;
using System.Collections.Generic;
using System.Data;

namespace BLL_QLKS
{
    public class TrangThaiDatPhongBLL
    {
        private TrangThaiDatPhongDAL dal = new TrangThaiDatPhongDAL();

        public List<TrangThaiDatPhongDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public bool Them(TrangThaiDatPhongDTO dto)
        {
            return dal.Them(dto);
        }

        public bool CapNhat(TrangThaiDatPhongDTO dto)
        {
            return dal.CapNhat(dto);
        }

        public bool Xoa(string id)
        {
            return dal.Xoa(id);
        }

        public List<TrangThaiDatPhongDTO> TimKiemTheoHoaDon(string hoaDonID)
        {
            return dal.TimKiemTheoHoaDon(hoaDonID);
        }
        public string GenerateNewTrangThaiID()
        {
            return dal.GenerateNewTrangThaiID();
        }


        public string GetPhongIDByHoaDonThueID(string hoaDonThueID)
        {
            string sql = "SELECT PhongID FROM HoaDonThue WHERE HoaDonThueID = @HoaDonThueID";
            var args = new Dictionary<string, object>
    {
        { "@HoaDonThueID", hoaDonThueID }
    };

            DataTable dt = DBUtil.Query(sql, args);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["PhongID"].ToString();

            return null;
        }


        public string GetTrangThaiCuoi(string hoaDonThueID)
        {
            var ds = LayDanhSach()
                .Where(x => x.HoaDonThueID == hoaDonThueID)
                .OrderByDescending(x => x.NgayCapNhat)
                .FirstOrDefault();

            return ds?.LoaiTrangThaiID;
        }


    }
}
