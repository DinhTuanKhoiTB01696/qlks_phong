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
    public class BUSNhanVien
    {
        DALNhanVien dalNhanVien = new DALNhanVien();

        public NhanVien DangNhap(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            return dalNhanVien.getNhanVien(username, password);
        }

        public List<NhanVien> GetNhanVienList()
        {
            return dalNhanVien.selectAll();
        }

        // ✅ Thêm phương thức này để tránh lỗi CS1061
        public List<NhanVien> GetAll()
        {
            return GetNhanVienList();
        }

        public bool ResetMatKhau(string email, string mk)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(mk))
                {
                    return false;
                }
                dalNhanVien.ResetMatKhau(mk, email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string InsertNhanVien(NhanVien nv)
        {
            try
            {
                nv.MaNV = dalNhanVien.generateMaNhanVien();
                if (string.IsNullOrEmpty(nv.MaNV))
                {
                    return "Mã nhân viên không hợp lệ.";
                }
                if (dalNhanVien.checkEmailExists(nv.Email))
                {
                    return "Email đã tồn tại.";
                }
                dalNhanVien.insertNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string UpdateNhanVien(NhanVien nv)
        {
            try
            {
                if (string.IsNullOrEmpty(nv.MaNV))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.updateNhanVien(nv);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        public string DeleteNhanVien(string maNV)
        {
            try
            {
                if (string.IsNullOrEmpty(maNV))
                {
                    return "Mã nhân viên không hợp lệ.";
                }

                dalNhanVien.deleteNhanVien(maNV);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }



        public NhanVien GetNhanVienByEmail(string email)
        {
            return dalNhanVien.getNhanVienByEmail(email);
        }


        //// Trong BLL_QLKS/BUSNhanVien.cs

        //public NhanVien DangNhap(string username, string password)
        //{
        //    // Bổ sung xử lý Trim()
        //    string trimmedUsername = username?.Trim();
        //    string trimmedPassword = password?.Trim();

        //    if (string.IsNullOrEmpty(trimmedUsername) || string.IsNullOrEmpty(trimmedPassword))
        //    {
        //        return null; // Xử lý TC04, 05, 06, 07, 08
        //    }

        //    // GỌI DAL VỚI GIÁ TRỊ ĐÃ ĐƯỢC XỬ LÝ
        //    return dalNhanVien.getNhanVien(trimmedUsername, trimmedPassword);
        //}
    }

}
