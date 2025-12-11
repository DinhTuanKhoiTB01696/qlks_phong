using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;

namespace BLL_QLKS
{
    public class BLLHoaDonThanhToan
    {
        private DALHoaDonThanhToan dal = new DALHoaDonThanhToan();

        private DALHoaDonThanhToan _dalHoaDonThanhToan = new DALHoaDonThanhToan();

        public List<DatPhong> GetAllDatPhongChuaThanhToan()
        {
            return dal.GetAllDatPhongChuaThanhToan();
        }

        public List<HoaDonThanhToan> GetAll()
        {
            return dal.GetAll();
        }

        public HoaDonThanhToan GetByID(string id)
        {
            return dal.GetByID(id);
        }

        public string Insert(HoaDonThanhToan entity)
        {
            try
            {
                dal.Insert(entity);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi thêm hóa đơn thanh toán: " + ex.Message;
            }
        }

        public string Update(HoaDonThanhToan entity)
        {
            try
            {
                dal.Update(entity);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi cập nhật hóa đơn thanh toán: " + ex.Message;
            }
        }

        public string Delete(string id)
        {
            try
            {
                dal.Delete(id);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi xóa hóa đơn thanh toán: " + ex.Message;
            }
        }

        // ✅ THÊM PHƯƠNG THỨC NÀY ĐỂ KHÔNG BỊ LỖI KHI GỌI ThemHoaDon(...)
        public bool ThemHoaDon(HoaDonThanhToan hoaDon)
        {
            try
            {
                dal.Insert(hoaDon);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public string TaoMaHoaDonMoi()
        {
            return dal.GenerateNewHoaDonID();
        }


        public List<HoaDonThanhToan> GetByTrangThai(int trangThai)
        {
            return dal.GetByTrangThai(trangThai);
        }


        public bool CapNhatHoaDon(HoaDonThanhToan hd)
        {
            return dal.CapNhat(hd);
        }


        public bool CapNhatTrangThai(string hoaDonID, int trangThai)
        {
            if (string.IsNullOrEmpty(hoaDonID))
                return false;

            return dal.CapNhatTrangThai(hoaDonID, trangThai);
        }

    }
}

