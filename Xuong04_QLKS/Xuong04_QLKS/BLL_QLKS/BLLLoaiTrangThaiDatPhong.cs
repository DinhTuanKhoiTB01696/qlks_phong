using System.Collections.Generic;
using DTO_QLKS;
using DAL_QLKS;

namespace BLL_QLKS
{
    public class LoaiTrangThaiDatPhongBLL
    {
        private LoaiTrangThaiDatPhongDAL dal = new LoaiTrangThaiDatPhongDAL();

        public List<LoaiTrangThaiDatPhongDTO> LayDanhSach()
        {
            return dal.LayDanhSach();
        }

        public bool Them(LoaiTrangThaiDatPhongDTO dto)
        {
            return dal.Them(dto);
        }

        public bool CapNhat(LoaiTrangThaiDatPhongDTO dto)
        {
            return dal.CapNhat(dto);
        }

        public bool Xoa(string id)
        {
            return dal.Xoa(id);
        }


        public List<LoaiTrangThaiDatPhongDTO> LayTrangThaiThanhToan()
        {
            return dal.LayTrangThaiThanhToan();
        }

    }
}
