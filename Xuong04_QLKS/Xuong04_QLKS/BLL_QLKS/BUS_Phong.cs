using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BUS_Phong
    {
        DAL_Phong dalPhong = new DAL_Phong();

        // ✅ Thêm phương thức đúng tên để tương thích với GUI
        public List<Phong> GetAllPhong()
        {
            return GetPhongList();
        }

        public List<Phong> GetPhongList()
        {
            return dalPhong.selectAll();
        }

        public Phong GetPhongById(string id)
        {
            return dalPhong.selectById(id);
        }

        public string InsertPhong(Phong phong)
        {
            try
            {
                phong.PhongID = dalPhong.generatePhongID();
                if (string.IsNullOrEmpty(phong.PhongID))
                {
                    return "Mã phòng không hợp lệ.";
                }
                dalPhong.insertPhong(phong);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdatePhong(Phong phong)
        {
            try
            {
                if (string.IsNullOrEmpty(phong.PhongID))
                {
                    return "Mã phòng không hợp lệ.";
                }
                dalPhong.updatePhong(phong);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeletePhong(string phongId)
        {
            try
            {
                dalPhong.deletePhong(phongId);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
        public List<Phong> GetPhongTheoTinhTrang(int tinhTrang)
        {
            return dalPhong.SelectByTinhTrang(tinhTrang);
        }


        // ✅ Cập nhật trạng thái phòng (TinhTrang) dựa vào HoaDonThueID
        public void CapNhatTinhTrangPhongTheoHoaDon(string hoaDonThueID, bool tinhTrang)
        {
            dalPhong.CapNhatTinhTrangPhongTheoHoaDon(hoaDonThueID, tinhTrang);
        }

        public void UpdateTinhTrangPhong(string phongID, bool tinhTrang)
        {
            dalPhong.CapNhatTinhTrang(phongID, tinhTrang);
        }



        public void CapNhatTrangThaiPhong(string phongID, bool trangThai)
        {
            Phong phong = dalPhong.selectById(phongID);
            if (phong != null)
            {
                phong.TinhTrang = trangThai;
                dalPhong.updatePhong(phong);
            }
        }
    }

}
