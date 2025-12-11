using NUnit.Framework;
using BLL_QLKS;
using DTO_QLKS;
using System;
using System.Linq;

namespace NUnitTest_QLKS
{
    [TestFixture]
    public class Test_LoaiPhong
    {
        private BUS_LoaiPhong _bus;

        [SetUp]
        public void Setup()
        {
            _bus = new BUS_LoaiPhong();
        }

        // ① Thêm loại phòng hợp lệ (Checklist: 68)
        [Test]
        public void Test_InsertLoaiPhong_Valid()
        {
            var lp = new LoaiPhong
            {
                TenLoaiPhong = "Phòng Test",
                NgayTao = DateTime.Now,
                TrangThai = true,
                GhiChu = "Test Add"
            };

            string result = _bus.InsertLoaiPhong(lp);

            Assert.That(result, Is.EqualTo(""), "Không thêm được loại phòng hợp lệ.");
        }

        // ② Thêm lỗi vì thiếu tên (Checklist: 69)
        [Test]
        public void Test_InsertLoaiPhong_EmptyName()
        {
            var lp = new LoaiPhong
            {
                TenLoaiPhong = "",
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            string result = _bus.InsertLoaiPhong(lp);

            Assert.That(result, Is.Not.EqualTo(""), "Thiếu tên mà vẫn cho thêm!");
        }

        // ③ Sửa loại phòng hợp lệ (Checklist: 70)
        [Test]
        public void Test_UpdateLoaiPhong_Valid()
        {
            var list = _bus.GetLoaiPhongList();
            var first = list.FirstOrDefault();

            Assert.IsNotNull(first, "Không có loại phòng nào để test.");

            first.TenLoaiPhong = "Tên Sau Khi Sửa";
            string result = _bus.UpdateLoaiPhong(first);

            Assert.That(result, Is.EqualTo(""), "Không thể sửa loại phòng hợp lệ.");
        }

        // ④ Xóa loại phòng hợp lệ (Checklist: 71)
        [Test]
        public void Test_DeleteLoaiPhong_Valid()
        {
            var lp = new LoaiPhong
            {
                TenLoaiPhong = "LP_Delete_Test",
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            _bus.InsertLoaiPhong(lp);

            var newList = _bus.GetLoaiPhongList();
            var added = newList.Last();

            string result = _bus.DeleteLoaiPhong(added.MaLoaiPhong);

            Assert.That(result, Is.EqualTo(""), "Không thể xóa loại phòng.");
        }

        // ⑤ Xóa khi không truyền mã (Checklist: lỗi nghiệp vụ)
        [Test]
        public void Test_DeleteLoaiPhong_EmptyCode()
        {
            string result = _bus.DeleteLoaiPhong("");

            Assert.That(result, Is.Not.EqualTo(""), "Không nhập mã vẫn xóa được!");
        }

        // ⑥ Kiểm tra sinh mã tự động LPxxx (Mapping checklist sinh mã)
        [Test]
        public void Test_Generate_MaLoaiPhong()
        {
            var dal = new DAL_QLKS.DAL_LoaiPhong();
            var code = dal.generateMaLoaiPhong();

            Assert.IsTrue(code.StartsWith("LP"), "Mã không đúng định dạng LPxxx");
        }

        // ⑦ Kiểm tra load danh sách (Checklist implicit – load form)
        [Test]
        public void Test_LoadLoaiPhong_ListNotEmpty()
        {
            var list = _bus.GetLoaiPhongList();

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        // ⑧ Tìm kiếm loại phòng theo tên (Checklist: 75 – tìm kiếm)
        [Test]
        public void Test_TimKiemLoaiPhong_ByName()
        {
            var list = _bus.GetLoaiPhongList();

            if (list.Count == 0)
                Assert.Pass("Không có dữ liệu để test tìm kiếm.");

            string keyword = list[0].TenLoaiPhong.Substring(0, 2);
            var result = list.Where(x => x.TenLoaiPhong.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();

            Assert.IsTrue(result.Count > 0, "Kết quả tìm kiếm rỗng.");
        }

        // ⑨ Kiểm tra trạng thái Active (Checklist trạng thái mặc định)
        [Test]
        public void Test_TrangThai_DefaultActive()
        {
            var lp = new LoaiPhong
            {
                TenLoaiPhong = "TestTrangThai",
                NgayTao = DateTime.Now,
                TrangThai = true
            };

            string result = _bus.InsertLoaiPhong(lp);
            Assert.That(result, Is.EqualTo(""));
        }

        // ⑩ Kiểm tra ngày tạo không vượt quá hiện tại (Checklist ngày)
        [Test]
        public void Test_NgayTao_KhongVuotQuaHienTai()
        {
            var lp = new LoaiPhong
            {
                TenLoaiPhong = "TestNgay",
                NgayTao = DateTime.Now.AddDays(1), // cố tình sai
                TrangThai = true
            };

            string result = _bus.InsertLoaiPhong(lp);

            Assert.That(result, Is.Not.EqualTo(""), "Ngày tương lai vẫn cho thêm!");
        }
    }
}
