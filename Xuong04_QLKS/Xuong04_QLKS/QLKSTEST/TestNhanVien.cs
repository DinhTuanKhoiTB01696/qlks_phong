using NUnit.Framework;
using BLL_QLKS;
using DTO_QLKS;
using DAL_QLKS;
using System.Collections.Generic;

namespace TestProject_NhanVien
{
    [TestFixture]
    public class NhanVienTests
    {
        private BUSNhanVien bus;
        private DALNhanVien dal;

        [SetUp]
        public void Setup()
        {
            bus = new BUSNhanVien();
            dal = new DALNhanVien();
        }

        // ==========================
        // 1. TEST ĐĂNG NHẬP
        // ==========================
        [Test]
        public void Test_DangNhap_ThanhCong()
        {
            var result = bus.DangNhap("admin@gmail.com", "123");
            Assert.IsNotNull(result);
        }

        [Test]
        public void Test_DangNhap_ThatBai()
        {
            var result = bus.DangNhap("saiemail@gmail.com", "wrong");
            Assert.IsNull(result);
        }

        // ==========================
        // 2. THÊM NHÂN VIÊN
        // ==========================
        [Test]
        public void Test_ThemNhanVien_ThanhCong()
        {
            string newEmail = "test_nv_" + System.Guid.NewGuid() + "@gmail.com";

            NhanVien nv = new NhanVien
            {
                MaNV = "",
                HoTen = "Test Nhân viên",
                Email = newEmail,
                GioiTinh = "Nam",
                DiaChi = "HCM",
                VaiTro = "Lễ tân",
                MatKhau = "123",
                TinhTrang = true
            };

            string result = bus.InsertNhanVien(nv);

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [Test]
        public void Test_ThemNhanVien_TrungEmail()
        {
            string email = "email_trung@gmail.com";

            // chèn trước 1 nhân viên để tạo email trùng
            NhanVien nv1 = new NhanVien
            {
                MaNV = "",
                HoTen = "NV1",
                Email = email,
                GioiTinh = "Nam",
                DiaChi = "HCM",
                VaiTro = "Lễ tân",
                MatKhau = "123",
                TinhTrang = true
            };
            bus.InsertNhanVien(nv1);

            // test case chính
            NhanVien nv2 = new NhanVien
            {
                MaNV = "",
                HoTen = "NV2",
                Email = email,
                GioiTinh = "Nữ",
                DiaChi = "HN",
                VaiTro = "Quản lý",
                MatKhau = "456",
                TinhTrang = true
            };

            string result = bus.InsertNhanVien(nv2);
            Assert.AreEqual("Email đã tồn tại.", result);
        }

        // ==========================
        // 3. SỬA NHÂN VIÊN
        // ==========================
        [Test]
        public void Test_SuaNhanVien_ThanhCong()
        {
            // tạo trước dữ liệu
            string email = "update_test" + System.Guid.NewGuid() + "@gmail.com";

            NhanVien nv = new NhanVien
            {
                MaNV = "",
                HoTen = "Test Update",
                Email = email,
                GioiTinh = "Nam",
                DiaChi = "HCM",
                VaiTro = "Lễ tân",
                MatKhau = "123",
                TinhTrang = true
            };
            bus.InsertNhanVien(nv);

            // lấy lại nhân viên để sửa
            var nvUpdate = bus.GetNhanVienByEmail(email);
            nvUpdate.HoTen = "Tên mới sau update";

            string result = bus.UpdateNhanVien(nvUpdate);
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [Test]
        public void Test_SuaNhanVien_MaRong()
        {
            NhanVien nv = new NhanVien
            {
                MaNV = "",
                HoTen = "Sai",
                Email = "aaa@gmail.com",
                MatKhau = "123",
                DiaChi = "HCM",
                GioiTinh = "Nam",
                VaiTro = "Lễ tân",
                TinhTrang = true
            };

            string result = bus.UpdateNhanVien(nv);
            Assert.AreEqual("Mã nhân viên không hợp lệ.", result);
        }

        // ==========================
        // 4. XÓA NHÂN VIÊN
        // ==========================
        [Test]
        public void Test_XoaNhanVien_ThanhCong()
        {
            string email = "delete_nv_" + System.Guid.NewGuid() + "@gmail.com";

            NhanVien nv = new NhanVien
            {
                MaNV = "",
                HoTen = "Delete NV",
                Email = email,
                GioiTinh = "Nam",
                DiaChi = "HN",
                VaiTro = "Lễ tân",
                MatKhau = "123",
                TinhTrang = true
            };
            bus.InsertNhanVien(nv);

            var nvDel = bus.GetNhanVienByEmail(email);
            string result = bus.DeleteNhanVien(nvDel.MaNV);

            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [Test]
        public void Test_XoaNhanVien_MaRong()
        {
            string result = bus.DeleteNhanVien("");
            Assert.AreEqual("Mã nhân viên không hợp lệ.", result);
        }

        // ==========================
        // 5. LẤY DANH SÁCH
        // ==========================
        [Test]
        public void Test_LayDanhSach_ThanhCong()
        {
            List<NhanVien> list = bus.GetNhanVienList();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        // ==========================
        // 6. RESET MẬT KHẨU
        // ==========================
        [Test]
        public void Test_ResetMatKhau()
        {
            bool result = bus.ResetMatKhau("admin@gmail.com", "123456");
            Assert.IsTrue(result);
        }
    }
}
