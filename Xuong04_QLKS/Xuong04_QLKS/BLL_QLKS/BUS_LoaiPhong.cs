using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BUS_LoaiPhong
    {
        DAL_LoaiPhong dalLoaiPhong = new DAL_LoaiPhong();

        public List<LoaiPhong> GetLoaiPhongList()
        {
            return dalLoaiPhong.selectAll();
        }

        public string InsertLoaiPhong(LoaiPhong loaiP)
        {
            try
            {
                loaiP.MaLoaiPhong = dalLoaiPhong.generateMaLoaiPhong();
                if (string.IsNullOrEmpty(loaiP.MaLoaiPhong))
                {
                    return "Mã loại phòng không hợp lệ.";
                }

                dalLoaiPhong.insertLoaiPhong(loaiP);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Thêm mới không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateLoaiPhong(LoaiPhong loaiP)
        {
            try
            {
                if (string.IsNullOrEmpty(loaiP.MaLoaiPhong))
                {
                    return "Mã loại phòng không hợp lệ.";
                }

                dalLoaiPhong.updateLoaiPhong(loaiP);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Cập nhật không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteLoaiPhong(string maloaiP)
        {
            try
            {
                if (string.IsNullOrEmpty(maloaiP))
                {
                    return "Mã loại phòng không hợp lệ.";
                }

                dalLoaiPhong.deleteLoaiPhong(maloaiP);
                return string.Empty;
            }
            catch (Exception ex)
            {
                //return "Xóa không thành công.";
                return "Lỗi: " + ex.Message;
            }
        }
    }
}
