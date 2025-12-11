using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLKS;
using DTO_QLKS;



namespace BLL_QLKS
{
    public class BUSLoaiDichVu
    {
        private DALLoaiDichVu dalLoaiDichVu = new DALLoaiDichVu();

        // ❌ Bỏ static để gọi qua instance
        public List<LoaiDichVu> GetAll()
        {
            return dalLoaiDichVu.selectAll();
        }

        public List<LoaiDichVu> GetLoaiDichVuList()
        {
            return dalLoaiDichVu.selectAll();
        }

        public LoaiDichVu GetKhachHangById(string id)
        {
            return dalLoaiDichVu.selectById(id);
        }

        public string InsertLoaiDichVu(LoaiDichVu ldv)
        {
            try
            {
                ldv.LoaiDichVuID = dalLoaiDichVu.generateLoaiDichVuID();
                if (string.IsNullOrEmpty(ldv.LoaiDichVuID))
                {
                    return "Mã phòng không hợp lệ.";
                }
                dalLoaiDichVu.insertLoaiDichVu(ldv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateLoaiDichVu(LoaiDichVu ldv)
        {
            try
            {
                if (string.IsNullOrEmpty(ldv.LoaiDichVuID))
                {
                    return "Mã Khách Hàng không hợp lệ.";
                }
                dalLoaiDichVu.updateLoaiDichVu(ldv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteLoaiDichVu(string KhachHangId)
        {
            try
            {
                dalLoaiDichVu.deleteLoaiDichVu(KhachHangId);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }
    }

}

