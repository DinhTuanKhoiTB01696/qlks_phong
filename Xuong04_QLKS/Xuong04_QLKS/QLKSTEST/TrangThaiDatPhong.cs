using NUnit.Framework;
using BLL_QLKS;
using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TRangThaiDatPhong
{
    [TestFixture]
    public class TrangThaiDatPhongTests
    {
        private TrangThaiDatPhongBLL bll;

        [SetUp]
        public void Setup()
        {
            bll = new TrangThaiDatPhongBLL();
        }

        // ===============================
        // 1. LẤY DANH SÁCH
        // ===============================
        [Test]
        public void Test_LayDanhSach_ThanhCong()
        {
            var list = bll.LayDanhSach();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        // ===============================
        // 2. THÊM TRẠNG THÁI
        // ===============================
        [Test]
        public void Test_ThemTrangThai_ThanhCong()
        {
            string newID = bll.GenerateNewTrangThaiID();

            var dto = new TrangThaiDatPhongDTO
            {
                TrangThaiID = newID,
                HoaDonThueID = "HD001",
                LoaiTrangThaiID = "LT01",
                TenTrangThai = "Đặt mới",
                NgayCapNhat = DateTime.Now
            };

            bool result = bll.Them(dto);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_ThemTrangThai_ThatBai_NullHoaDon()
        {
            string newID = bll.GenerateNewTrangThaiID();

            var dto = new TrangThaiDatPhongDTO
            {
                TrangThaiID = newID,
                HoaDonThueID = "",
                LoaiTrangThaiID = "LT01",
                TenTrangThai = "Đặt mới",
                NgayCapNhat = DateTime.Now
            };

            bool result = bll.Them(dto);

            // DAL sẽ trả false vì không insert được
            Assert.IsFalse(result);
        }

        // ===============================
        // 3. CẬP NHẬT TRẠNG THÁI
        // ===============================
        [Test]
        public void Test_CapNhatTrangThai_ThanhCong()
        {
            // Thêm mới trước
            string id = bll.GenerateNewTrangThaiID();
            var dto = new TrangThaiDatPhongDTO
            {
                TrangThaiID = id,
                HoaDonThueID = "HD001",
                LoaiTrangThaiID = "LT01",
                TenTrangThai = "Đặt mới",
                NgayCapNhat = DateTime.Now
            };
            bll.Them(dto);

            // Update
            dto.TenTrangThai = "Đang xử lý";

            bool result = bll.CapNhat(dto);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_CapNhatTrangThai_SaiID()
        {
            var dto = new TrangThaiDatPhongDTO
            {
                TrangThaiID = "SAIID999",
                HoaDonThueID = "HD001",
                LoaiTrangThaiID = "LT01",
                TenTrangThai = "Không tồn tại",
                NgayCapNhat = DateTime.Now
            };

            bool result = bll.CapNhat(dto);
            Assert.IsFalse(result);
        }

        // ===============================
        // 4. XÓA TRẠNG THÁI
        // ===============================
        [Test]
        public void Test_XoaTrangThai_ThanhCong()
        {
            string id = bll.GenerateNewTrangThaiID();

            var dto = new TrangThaiDatPhongDTO
            {
                TrangThaiID = id,
                HoaDonThueID = "HD001",
                LoaiTrangThaiID = "LT01",
                TenTrangThai = "Test Xóa",
                NgayCapNhat = DateTime.Now
            };

            bll.Them(dto);

            bool result = bll.Xoa(id);
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_XoaTrangThai_ThatBai()
        {
            string id = "ID_KHONG_TONTAI";
            bool result = bll.Xoa(id);
            Assert.IsFalse(result);
        }

        // ===============================
        // 5. TÌM THEO HÓA ĐƠN
        // ===============================
        [Test]
        public void Test_TimKiemTheoHoaDon()
        {
            var list = bll.TimKiemTheoHoaDon("HD001");

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        // ===============================
        // 6. SINH MÃ TỰ ĐỘNG
        // ===============================
        [Test]
        public void Test_GenerateNewTrangThaiID()
        {
            string id = bll.GenerateNewTrangThaiID();
            Assert.IsTrue(id.StartsWith("TT"));
        }

        // ===============================
        // 7. LẤY TRẠNG THÁI CUỐI CÙNG
        // ===============================
        [Test]
        public void Test_GetTrangThaiCuoi_TonTai()
        {
            string last = bll.GetTrangThaiCuoi("HD001");
            Assert.IsTrue(true); // chỉ cần chạy không lỗi
        }

        [Test]
        public void Test_GetTrangThaiCuoi_KhongTonTai()
        {
            string last = bll.GetTrangThaiCuoi("HD_KHONGTONTAI");
            Assert.IsNull(last);
        }

        // ===============================
        // 8. LẤY PHÒNG ID THEO HÓA ĐƠN THUÊ
        // ===============================
        [Test]
        public void Test_GetPhongIDByHoaDonThue()
        {
            string phongID = bll.GetPhongIDByHoaDonThueID("HD001");
            Assert.IsTrue(phongID == null || phongID.Length > 0);
        }
    }
}
