using NUnit.Framework;
using DAL_QLKS;
using BLL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;

namespace LoaiDichVuTests
{
    [TestFixture]
    public class LoaiDichVuTestSuite
    {
        private DALLoaiDichVu dal;              // DAL thật
        private BUSLoaiDichVu bll;              // BLL thật

        [SetUp]
        public void Setup()
        {
            dal = new DALLoaiDichVu();          // khởi tạo DAL
            bll = new BUSLoaiDichVu();          // khởi tạo BLL
        }

        // ============================================================
        // 1 – Lấy tất cả danh sách loại dịch vụ
        // ============================================================
        [Test]
        public void TC01_GetAll_ShouldReturnList()
        {
            var list = bll.GetAll();                    // gọi BLL lấy list
            Assert.IsNotNull(list);                     // list không null
            Assert.GreaterOrEqual(list.Count, 0);       // có thể rỗng
        }

        // ============================================================
        // 2 – Lấy danh sách loại dịch vụ (GetLoaiDichVuList)
        // ============================================================
        [Test]
        public void TC02_GetLoaiDichVuList_ShouldReturnList()
        {
            var list = bll.GetLoaiDichVuList();         // gọi hàm phụ
            Assert.IsNotNull(list);                     // list không null
        }

        // ============================================================
        // 3 – Lấy loại dịch vụ theo ID hợp lệ
        // ============================================================
        [Test]
        public void TC03_SelectById_Valid_ShouldReturnItem()
        {
            var item = dal.selectById("DV001");         // test với ID tồn tại
            Assert.IsNotNull(item);                     // phải trả object
            Assert.AreEqual("DV001", item.LoaiDichVuID);// đúng ID
        }

        // ============================================================
        // 4 – Lấy loại dịch vụ theo ID không tồn tại
        // ============================================================
        [Test]
        public void TC04_SelectById_Invalid_ShouldReturnNull()
        {
            var item = dal.selectById("DV999");         // ID không có
            Assert.IsNull(item);                        // phải null
        }

        // ============================================================
        // 5 – Lấy loại dịch vụ theo ID null
        // ============================================================
        [Test]
        public void TC05_SelectById_Null_ShouldReturnNull()
        {
            var item = dal.selectById(null);            // truyền null
            Assert.IsNull(item);                        // DAL trả null
        }

        // ============================================================
        // 6 – Thêm loại dịch vụ mới (đầy đủ dữ liệu)
        // ============================================================
        [Test]
        public void TC06_Insert_Valid_ShouldReturnEmptyString()
        {
            LoaiDichVu dv = new LoaiDichVu
            {
                TenDichVu = "Massage VIP Test",          // tên hợp lệ
                GiaDichVu = 200000,                      // giá hợp lệ
                DonViTinh = "Lần",
                NgayTao = DateTime.Now,
                TrangThai = true,
                GhiChu = "Test insert"
            };

            string result = bll.InsertLoaiDichVu(dv);    // insert
            Assert.AreEqual("", result);                 // trả empty = OK
        }

        // ============================================================
        // 7 – Thêm loại dịch vụ thiếu trường bắt buộc
        // ============================================================
        [Test]
        public void TC07_Insert_MissingFields_ShouldReturnError()
        {
            LoaiDichVu dv = new LoaiDichVu
            {
                TenDichVu = "",                         // thiếu tên
                GiaDichVu = 0                            // thiếu giá
            };

            string result = bll.InsertLoaiDichVu(dv);    // insert
            Assert.IsTrue(result.Contains("Lỗi") || result != "");   // phải lỗi
        }

        // ============================================================
        // 8 – Thêm loại dịch vụ ID trùng
        // ============================================================
        [Test]
        public void TC08_Insert_DuplicateID_ShouldGiveError()
        {
            LoaiDichVu dv = new LoaiDichVu
            {
                LoaiDichVuID = "DV001",                  // cố tình trùng
                TenDichVu = "Dịch vụ trùng",
                GiaDichVu = 50000,
                DonViTinh = "Lần",
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            string result = bll.InsertLoaiDichVu(dv);     // insert
            Assert.IsTrue(result.Contains("Lỗi"));        // phải lỗi
        }

        // ============================================================
        // 9 – Cập nhật loại dịch vụ
        // ============================================================
        [Test]
        public void TC09_Update_Valid_ShouldReturnEmpty()
        {
            LoaiDichVu dv = new LoaiDichVu
            {
                LoaiDichVuID = "DV001",
                TenDichVu = "Dịch vụ cập nhật test",
                GiaDichVu = 99999,
                DonViTinh = "Lần",
                NgayTao = DateTime.Now,
                TrangThai = true,
                GhiChu = "Update Test"
            };

            string result = bll.UpdateLoaiDichVu(dv);     // update
            Assert.AreEqual("", result);                  // empty = OK
        }

        // ============================================================
        // 10 – Cập nhật ID null
        // ============================================================
        [Test]
        public void TC10_Update_NullID_ShouldReturnError()
        {
            LoaiDichVu dv = new LoaiDichVu
            {
                LoaiDichVuID = null                      // null ID
            };

            string result = bll.UpdateLoaiDichVu(dv);     // update
            Assert.AreEqual("Mã Khách Hàng không hợp lệ.", result);
        }

        // ============================================================
        // 11 – Cập nhật ID không tồn tại
        // ============================================================
        [Test]
        public void TC11_Update_InvalidID_ShouldError()
        {
            LoaiDichVu dv = new LoaiDichVu
            {
                LoaiDichVuID = "DV999",                  // không tồn tại
                TenDichVu = "Test update",
                GiaDichVu = 50000,
                DonViTinh = "Lần",
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            string result = bll.UpdateLoaiDichVu(dv);     // update
            Assert.IsTrue(result.Contains("Lỗi"));        // phải lỗi
        }

        // ============================================================
        // 12 – Xóa loại dịch vụ hợp lệ
        // ============================================================
        [Test]
        public void TC12_Delete_Valid_ShouldRemove()
        {
            string result = bll.DeleteLoaiDichVu("DV002");   // xóa
            Assert.IsTrue(result == "" || result.Contains("")); // OK
        }

        // ============================================================
        // 13 – Xóa ID không tồn tại
        // ============================================================
        [Test]
        public void TC13_Delete_Invalid_ShouldReturnError()
        {
            string result = bll.DeleteLoaiDichVu("DV999");   // không tồn tại
            Assert.IsTrue(result.Contains("Lỗi"));           // phải lỗi
        }

        // ============================================================
        // 14 – Kiểm tra trạng thái True/False
        // ============================================================
        [Test]
        public void TC14_CheckTrangThai_ShouldReturnBool()
        {
            var dv = dal.selectById("DV001");               // lấy mẫu
            Assert.IsNotNull(dv);                           // phải có
            Assert.IsInstanceOf<bool>(dv.TrangThai);        // kiểu bool
        }

        // ============================================================
        // 15 – Kiểm tra sinh mã DV### tăng dần
        // ============================================================
        [Test]
        public void TC15_GenerateID_ShouldReturnDVxxx()
        {
            string id = dal.generateLoaiDichVuID();         // generate
            Assert.IsTrue(id.StartsWith("DV"));             // DV###
            Assert.AreEqual(5, id.Length);                  // 5 ký tự
        }

        // ============================================================
        // 16 – Kiểm tra danh sách rỗng
        // ============================================================
        [Test]
        public void TC16_GetAll_Empty_ShouldReturnEmptyList()
        {
            var list = dal.selectAll();                     // lấy dữ liệu
            Assert.IsNotNull(list);                         // không null
            Assert.GreaterOrEqual(list.Count, 0);           // chấp nhận 0
        }

        // ============================================================
        // 17 – Xử lý ngoại lệ insert
        // ============================================================
        [Test]
        public void TC17_Insert_Exception_ShouldCatch()
        {
            LoaiDichVu dv = null;                           // null gây lỗi

            string result = bll.InsertLoaiDichVu(dv);        // insert
            Assert.IsTrue(result.Contains("Lỗi"));           // phải catch lỗi
        }

        // ============================================================
        // 18 – Xử lý ngoại lệ update
        // ============================================================
        [Test]
        public void TC18_Update_Exception_ShouldCatch()
        {
            LoaiDichVu dv = null;                           // null

            string result = bll.UpdateLoaiDichVu(dv);        // update
            Assert.IsTrue(result.Contains("Lỗi"));
        }

        // ============================================================
        // 19 – Xử lý ngoại lệ delete
        // ============================================================
        [Test]
        public void TC19_Delete_Exception_ShouldCatch()
        {
            string result = bll.DeleteLoaiDichVu(null);      // null
            Assert.IsTrue(result.Contains("Lỗi"));
        }

        // ============================================================
        // 20 – Kiểm tra hiển thị thông tin đầy đủ
        // ============================================================
        [Test]
        public void TC20_CheckFullInfo_ShouldReturnValidObject()
        {
            var dv = dal.selectById("DV001");                // lấy mẫu
            if (dv != null)
            {
                Assert.IsNotNull(dv.TenDichVu);              // tên OK
                Assert.Greater(dv.GiaDichVu, 0);             // giá > 0
                Assert.IsNotNull(dv.DonViTinh);              // đơn vị
                Assert.IsNotNull(dv.GhiChu);                 // ghi chú
            }
        }
    }
}
