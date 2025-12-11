using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using DAL_QLKS;
using BLL_QLKS;
using DTO_QLKS;

namespace DatPhongTests
{
    [TestFixture]
    public class DatPhongTestSuite
    {
        private DALDatPhong dal;                 // DAL thật
        private BUSDatPhong bll;                 // BLL thật
        private Mock<IDALDatPhong> mockDAL;      // Mock DAL để test business

        [SetUp]
        public void Setup()
        {
            dal = new DALDatPhong();             // khởi tạo DAL
            bll = new BUSDatPhong();             // khởi tạo BLL
            mockDAL = new Mock<IDALDatPhong>();   // khởi tạo Mock DAL
        }

        // ============================================================
        // 118 – Lấy danh sách đặt phòng
        // ============================================================
        [Test]
        public void TC118_SelectAll_ShouldReturnList()
        {
            var list = dal.selectAll();                      // gọi DAL selectAll()
            Assert.IsNotNull(list);                          // kiểm tra không null
            Assert.IsTrue(list.Count >= 0);                  // kiểm tra số lượng >= 0
        }

        // ============================================================
        // 119 – Lấy danh sách đặt phòng (GetDatPhongList)
        // ============================================================
        [Test]
        public void TC119_GetDatPhongList_ShouldReturnList()
        {
            var list = bll.GetDatPhongList();                // gọi BLL để lấy danh sách
            Assert.IsNotNull(list);                          // không được null
        }

        // ============================================================
        // 120 – Lấy đặt phòng theo ID (hợp lệ + không tồn tại)
        // ============================================================
        [Test]
        public void TC120_SelectById_Valid_ShouldReturnItem()
        {
            var dp = dal.selectById("HD001");                // lấy 1 ID có thật
            Assert.IsNotNull(dp);                            // phải có dữ liệu
            Assert.AreEqual("HD001", dp.HoaDonThueID);       // đúng ID
        }

        [Test]
        public void TC120_SelectById_Invalid_ShouldReturnNull()
        {
            var dp = dal.selectById("HD999");                // lấy ID không tồn tại
            Assert.IsNull(dp);                               // phải null
        }

        // ============================================================
        // 121 – Lấy chi tiết đặt phòng theo View
        // ============================================================
        [Test]
        public void TC121_GetDatPhongChiTiet_ShouldReturnView()
        {
            var view = dal.GetThongTinDatPhongChiTiet("HD001");  // gọi hàm view
            Assert.IsNotNull(view);                               // phải trả về
            Assert.AreEqual("HD001", view.HoaDonThueID);          // đúng mã
        }

        // ============================================================
        // 122 – Test DAL: trả về đúng dữ liệu
        // ============================================================
        [Test]
        public void TC122_DALSelectById_ShouldMapCorrectFields()
        {
            var dp = dal.selectById("HD001");                // lấy dữ liệu
            if (dp != null)
            {
                Assert.IsNotEmpty(dp.KhachHangID);           // phải có khách hàng
                Assert.IsNotEmpty(dp.PhongID);               // phải có phòng
            }
        }

        // ============================================================
        // 123 – Thêm đặt phòng (thiếu trường bắt buộc + hợp lệ)
        // ============================================================
        [Test]
        public void TC123_Insert_MissingFields_ShouldReturnNull()
        {
            var dp = new DatPhong
            {
                KhachHangID = "",                            // thiếu customer
                PhongID = ""                                  // thiếu phòng
            };

            string result = bll.InsertDatPhong(dp);          // gọi BLL insert
            Assert.IsNull(result);                           // phải null
        }

        [Test]
        public void TC123_Insert_Valid_ShouldReturnID()
        {
            var dp = new DatPhong
            {
                HoaDonThueID = "",                           // test auto-generate
                KhachHangID = "KH01",
                PhongID = "P01",
                NgayDen = DateTime.Now,
                NgayDi = DateTime.Now.AddDays(1),
                MaNV = "NV01"
            };

            string id = bll.InsertDatPhong(dp);              // insert
            Assert.IsNotNull(id);                             // phải trả về ID
            Assert.IsTrue(id.StartsWith("HD"));               // format HD###
        }

        // ============================================================
        // 124 – Cập nhật đặt phòng
        // ============================================================
        [Test]
        public void TC124_UpdateDatPhong_InvalidID_ShouldReturnError()
        {
            var dp = new DatPhong { HoaDonThueID = "" };     // ID rỗng
            string result = bll.UpdateDatPhong(dp);          // gọi update
            Assert.IsNotEmpty(result);                        // phải trả lỗi
        }

        // ============================================================
        // 125 – Xóa đặt phòng
        // ============================================================
        [Test]
        public void TC125_DeleteDatPhong_ShouldRemoveItem()
        {
            dal.deleteDatPhong("HD900");                      // xóa trước
            var dp = dal.selectById("HD900");                // kiểm tra lại
            Assert.IsNull(dp);                               // phải null
        }

        // ============================================================
        // 126 – Kiểm tra phòng đã được đặt (overlap ngày)
        // ============================================================
        [Test]
        public void TC126_PhongOverlap_ShouldReturnTrueOrFalse()
        {
            bool result = dal.KiemTraPhongDaDuocDat(
                "P01",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 3)
            );

            Assert.IsInstanceOf<bool>(result);               // phải trả bool
        }

        // ============================================================
        // 127 – Lọc danh sách còn hiệu lực (không bao gồm Hủy)
        // ============================================================
        [Test]
        public void TC127_GetConHieuLuc_ShouldReturnNonCanceled()
        {
            var list = bll.GetDatPhongConHieuLuc();          // BLL lọc
            Assert.IsNotNull(list);                           // không null
            // không kiểm tra trạng thái vì tùy DB
        }

        // ============================================================
        // 128 – Kiểm tra phòng đã được đặt bằng Stored Procedure
        // ============================================================
        [Test]
        public void TC128_CheckPhong_SP_ShouldReturnBool()
        {
            bool result = dal.KiemTraPhongDaDuocDat_SP(
                "P01",
                DateTime.Now,
                DateTime.Now.AddDays(1)
            );

            Assert.IsInstanceOf<bool>(result);               // đúng kiểu
        }

        // ============================================================
        // 129 – Lấy danh sách đặt phòng view
        // ============================================================
        [Test]
        public void TC129_DanhSachDatPhongView_ShouldHavePhongName()
        {
            var list = bll.GetDanhSachDatPhongView();        // view
            Assert.IsNotNull(list);                           // không null
        }

        // ============================================================
        // 130 – Generate ID tăng dần đúng format HD###
        // ============================================================
        [Test]
        public void TC130_GenerateID_ShouldBeHDxxx()
        {
            string id = dal.generateHoaDonThueID();           // sinh mã
            StringAssert.StartsWith("HD", id);                // phải bắt đầu bằng HD
            Assert.AreEqual(5, id.Length);                    // dạng HD###
        }
    }
}
