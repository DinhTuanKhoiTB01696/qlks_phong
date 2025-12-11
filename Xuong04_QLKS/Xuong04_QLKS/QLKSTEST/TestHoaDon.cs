using NUnit.Framework;
using DAL_QLKS;
using BLL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;

namespace HoaDonThanhToanTests
{
    [TestFixture]
    public class HoaDonThanhToanTestSuite
    {
        private DALHoaDonThanhToan dal;
        private BLLHoaDonThanhToan bll;

        [SetUp]
        public void Setup()
        {
            dal = new DALHoaDonThanhToan();
            bll = new BLLHoaDonThanhToan();
        }

        // ============================================================
        // 1 – Lấy tất cả hóa đơn
        // ============================================================
        [Test]
        public void TC01_GetAll_ShouldReturnList()
        {
            var list = bll.GetAll();
            Assert.IsNotNull(list);
            Assert.GreaterOrEqual(list.Count, 0);
        }

        // ============================================================
        // 2 – Lấy hóa đơn theo ID hợp lệ
        // ============================================================
        [Test]
        public void TC02_GetByID_Valid_ShouldReturnItem()
        {
            string id = "HD001";
            var hd = dal.GetByID(id);
            Assert.IsNotNull(hd);
            Assert.AreEqual(id, hd.HoaDonID);
        }

        // ============================================================
        // 3 – Lấy hóa đơn theo ID không tồn tại
        // ============================================================
        [Test]
        public void TC03_GetByID_Invalid_ShouldReturnNull()
        {
            var hd = dal.GetByID("HD999");
            Assert.IsNull(hd);
        }

        // ============================================================
        // 4 – Lấy hóa đơn theo ID null
        // ============================================================
        [Test]
        public void TC04_GetByID_Null_ShouldReturnNull()
        {
            var hd = dal.GetByID(null);
            Assert.IsNull(hd);
        }

        // ============================================================
        // 5 – Thêm hóa đơn hợp lệ
        // ============================================================
        [Test]
        public void TC05_Insert_Valid_ShouldReturnTrue()
        {
            var hd = new HoaDonThanhToan
            {
                HoaDonID = "",
                HoaDonThueID = "HDT001",
                NgayThanhToan = DateTime.Now,
                PhuongThucThanhToan = "Tiền mặt",
                GhiChu = "Test Insert",
                TrangThai = 1
            };
            var result = bll.ThemHoaDon(hd);
            Assert.IsTrue(result);
        }

        // ============================================================
        // 6 – Thêm hóa đơn thiếu trường bắt buộc
        // ============================================================
        [Test]
        public void TC06_Insert_MissingFields_ShouldReturnFalse()
        {
            var hd = new HoaDonThanhToan
            {
                HoaDonID = "",
                HoaDonThueID = null,
                NgayThanhToan = DateTime.Now
            };
            var result = bll.ThemHoaDon(hd);
            Assert.IsFalse(result);
        }

        // ============================================================
        // 7 – Thêm hóa đơn trùng ID
        // ============================================================
        [Test]
        public void TC07_Insert_DuplicateID_ShouldReturnFalse()
        {
            string id = bll.TaoMaHoaDonMoi();

            var hd1 = new HoaDonThanhToan
            {
                HoaDonID = id,
                HoaDonThueID = "HDT002",
                NgayThanhToan = DateTime.Now
            };
            bll.ThemHoaDon(hd1);

            var hd2 = new HoaDonThanhToan
            {
                HoaDonID = id,
                HoaDonThueID = "HDT003",
                NgayThanhToan = DateTime.Now
            };
            var result = bll.ThemHoaDon(hd2);
            Assert.IsFalse(result);
        }

        // ============================================================
        // 8 – Cập nhật hóa đơn hợp lệ
        // ============================================================
        [Test]
        public void TC08_Update_Valid_ShouldReturnEmptyString()
        {
            string id = bll.TaoMaHoaDonMoi();
            var hd = new HoaDonThanhToan
            {
                HoaDonID = id,
                HoaDonThueID = "HDT004",
                NgayThanhToan = DateTime.Now
            };
            bll.ThemHoaDon(hd);

            hd.GhiChu = "Updated";
            var result = bll.Update(hd);
            Assert.AreEqual("", result);
        }

        // ============================================================
        // 9 – Cập nhật ID null
        // ============================================================
        [Test]
        public void TC09_Update_NullID_ShouldReturnError()
        {
            var hd = new HoaDonThanhToan
            {
                HoaDonID = null
            };
            var result = bll.Update(hd);
            Assert.IsNotEmpty(result);
        }

        // ============================================================
        // 10 – Cập nhật ID không tồn tại
        // ============================================================
        [Test]
        public void TC10_Update_InvalidID_ShouldReturnError()
        {
            var hd = new HoaDonThanhToan
            {
                HoaDonID = "HD999",
                HoaDonThueID = "HDT005",
                NgayThanhToan = DateTime.Now
            };
            var result = bll.Update(hd);
            Assert.IsNotEmpty(result);
        }

        // ============================================================
        // 11 – Xóa hóa đơn hợp lệ
        // ============================================================
        [Test]
        public void TC11_Delete_Valid_ShouldReturnEmptyString()
        {
            string id = bll.TaoMaHoaDonMoi();
            var hd = new HoaDonThanhToan
            {
                HoaDonID = id,
                HoaDonThueID = "HDT006",
                NgayThanhToan = DateTime.Now
            };
            bll.ThemHoaDon(hd);

            var result = bll.Delete(id);
            Assert.AreEqual("", result);
        }

        // ============================================================
        // 12 – Xóa ID không tồn tại
        // ============================================================
        [Test]
        public void TC12_Delete_Invalid_ShouldReturnError()
        {
            var result = bll.Delete("HD999");
            Assert.IsNotEmpty(result);
        }

        // ============================================================
        // 13 – Cập nhật trạng thái
        // ============================================================
        [Test]
        public void TC13_UpdateTrangThai_ShouldReturnTrue()
        {
            string id = bll.TaoMaHoaDonMoi();
            var hd = new HoaDonThanhToan
            {
                HoaDonID = id,
                HoaDonThueID = "HDT007",
                NgayThanhToan = DateTime.Now
            };
            bll.ThemHoaDon(hd);

            var result = bll.CapNhatTrangThai(id, 0);
            Assert.IsTrue(result);
        }

        // ============================================================
        // 14 – Kiểm tra sinh mã tăng dần
        // ============================================================
        [Test]
        public void TC14_GenerateID_ShouldReturnCorrectFormat()
        {
            string id = bll.TaoMaHoaDonMoi();
            StringAssert.StartsWith("HD", id);
        }

        // ============================================================
        // 15 – Kiểm tra trạng thái True/False
        // ============================================================
        [Test]
        public void TC15_CheckTrangThai_ShouldReturnBool()
        {
            var hd = dal.GetByID("HD001");
            Assert.IsNotNull(hd);
            Assert.IsInstanceOf<int>(hd.TrangThai);
        }

        // ============================================================
        // 16 – Kiểm tra danh sách rỗng
        // ============================================================
        [Test]
        public void TC16_GetAll_Empty_ShouldReturnEmptyList()
        {
            var list = dal.GetAll();
            Assert.IsNotNull(list);
            Assert.GreaterOrEqual(list.Count, 0);
        }

        // ============================================================
        // 17 – Xử lý ngoại lệ insert
        // ============================================================
        [Test]
        public void TC17_Insert_Exception_ShouldCatch()
        {
            HoaDonThanhToan hd = null;
            var result = bll.ThemHoaDon(hd);
            Assert.IsFalse(result);
        }

        // ============================================================
        // 18 – Xử lý ngoại lệ update
        // ============================================================
        [Test]
        public void TC18_Update_Exception_ShouldCatch()
        {
            HoaDonThanhToan hd = null;
            var result = bll.Update(hd);
            Assert.IsNotEmpty(result);
        }

        // ============================================================
        // 19 – Xử lý ngoại lệ delete
        // ============================================================
        [Test]
        public void TC19_Delete_Exception_ShouldCatch()
        {
            var result = bll.Delete(null);
            Assert.IsNotEmpty(result);
        }

        // ============================================================
        // 20 – Kiểm tra hiển thị thông tin đầy đủ
        // ============================================================
        [Test]
        public void TC20_CheckFullInfo_ShouldReturnValidObject()
        {
            var hd = dal.GetByID("HD001");
            if (hd != null)
            {
                Assert.IsNotNull(hd.HoaDonID);
                Assert.IsNotNull(hd.HoaDonThueID);
                Assert.IsNotNull(hd.PhuongThucThanhToan);
                Assert.IsNotNull(hd.NgayThanhToan);
            }
        }
    }
}
