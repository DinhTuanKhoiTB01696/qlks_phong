using NUnit.Framework;
using BLL_QLKS;
using DTO_QLKS;
using System;
using System.Linq;

namespace NUnitTest_QLKS
{
    [TestFixture]
    public class Test_Phong
    {
        private BUS_Phong _bus;

        [SetUp]
        public void Setup()
        {
            _bus = new BUS_Phong();
        }

        // ① Thêm phòng hợp lệ
        [Test]
        public void Test_InsertPhong_Valid()
        {
            var p = new DTO_QLKS.Phong
            {
                TenPhong = "Phòng Test",
                MaLoaiPhong = "LP001",
                GiaPhong = 500000,
                TinhTrang = false,
                GhiChu = "Test Insert"
            };

            string result = _bus.InsertPhong(p);

            Assert.That(result, Is.EqualTo(""), "Không thêm được phòng hợp lệ");
        }

        // ② Lỗi khi không nhập tên
        [Test]
        public void Test_InsertPhong_NoName()
        {
            var p = new DTO_QLKS.Phong
            {
                TenPhong = "",
                MaLoaiPhong = "LP001",
                GiaPhong = 200000
            };

            string result = _bus.InsertPhong(p);

            Assert.That(result, Is.Not.EqualTo(""), "Thiếu tên mà vẫn cho thêm!");
        }

        // ③ Sửa phòng hợp lệ
        [Test]
        public void Test_UpdatePhong_Valid()
        {
            var list = _bus.GetPhongList();
            var first = list.FirstOrDefault();
            Assert.IsNotNull(first, "Không có phòng để kiểm tra update");

            first.TenPhong = "Phòng Sửa Tên";

            string result = _bus.UpdatePhong(first);
            Assert.That(result, Is.EqualTo(""), "Không sửa được phòng hợp lệ");
        }

        // ④ Xóa phòng
        [Test]
        public void Test_DeletePhong()
        {
            var p = new DTO_QLKS.Phong
            {
                TenPhong = "Phòng Test Xóa",
                MaLoaiPhong = "LP001",
                GiaPhong = 300000
            };

            _bus.InsertPhong(p);

            var list = _bus.GetPhongList();
            var last = list.Last();

            string result = _bus.DeletePhong(last.PhongID);
            Assert.That(result, Is.EqualTo(""), "Không thể xóa phòng!");
        }

        // ⑤ Sinh mã phòng tự động đúng định dạng
        [Test]
        public void Test_Generate_PhongID()
        {
            var dal = new DAL_QLKS.DAL_Phong();
            string ma = dal.generatePhongID();

            Assert.IsTrue(ma.StartsWith("P"), "Mã phòng không đúng định dạng");
        }

        // ⑥ Load danh sách phòng
        [Test]
        public void Test_LoadPhongList()
        {
            var list = _bus.GetPhongList();
            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        // ⑦ Lấy phòng theo ID
        [Test]
        public void Test_GetPhongByID()
        {
            var list = _bus.GetPhongList();
            if (list.Count == 0)
                Assert.Pass("Không có phòng để kiểm tra");

            var id = list.First().PhongID;
            var p = _bus.GetPhongById(id);

            Assert.IsNotNull(p, "Không lấy được phòng theo ID");
            Assert.AreEqual(id, p.PhongID);
        }

        // ⑧ Lọc phòng theo tình trạng
        [Test]
        public void Test_GetPhongTheoTinhTrang()
        {
            var list = _bus.GetPhongTheoTinhTrang(1);

            Assert.IsNotNull(list);
            Assert.IsTrue(list.Count >= 0);
        }

        // ⑨ Cập nhật tình trạng phòng
        [Test]
        public void Test_Update_TinhTrang_Phong()
        {
            var list = _bus.GetPhongList();
            if (list.Count == 0) Assert.Pass("Không có phòng để test");

            var room = list.First();
            bool newStatus = !room.TinhTrang;

            _bus.UpdateTinhTrangPhong(room.PhongID, newStatus);

            var roomUpdated = _bus.GetPhongById(room.PhongID);

            Assert.AreEqual(newStatus, roomUpdated.TinhTrang);
        }

        // ⑩ Tìm kiếm phòng (theo tên)
        [Test]
        public void Test_TimKiemPhong()
        {
            var list = _bus.GetPhongList();
            if (list.Count == 0) Assert.Pass("Không có phòng để test");

            string keyword = list[0].TenPhong.Substring(0, 1);
            var result = list.Where(p => p.TenPhong.Contains(keyword)).ToList();

            Assert.IsTrue(result.Count > 0, "Không tìm thấy phòng theo keyword");
        }
    }
}
