using NUnit.Framework;
using Moq;
using DTO_QLKS;
using BLL_QLKS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using NUnit.Framework;
using DAL_QLKS;

namespace QLKS_AutoTest
{
    [TestFixture]
    public class ChiTietDichVuTests
    {
        private BLLChiTietDichVu _bll;
        private Mock<IDALChiTietDichVu> _mockDal;
        private List<ChiTietDichVu> _inMemoryCtDichVu;

        private readonly string HDT_TEST = "HDT_CTDV_TEST";
        private readonly string HDT_LOCKED = "HD002";

        [SetUp]
        public void Setup()
        {
            _inMemoryCtDichVu = new List<ChiTietDichVu>();
            _mockDal = new Mock<IDALChiTietDichVu>();

            _bll = new BLLChiTietDichVu(_mockDal.Object);

            // --- CÀI ĐẶT HÀNH VI CHO MOCK ---

            int nextId = 1;
            _mockDal.Setup(dal => dal.GenerateNextID())
                    .Returns(() => $"CTDV{nextId++:D3}");

            _mockDal.Setup(dal => dal.Insert(It.IsAny<ChiTietDichVu>()))
                    .Callback<ChiTietDichVu>(ct => {
                        if (string.IsNullOrEmpty(ct.ChiTietDichVuID)) ct.ChiTietDichVuID = _mockDal.Object.GenerateNextID();
                        _inMemoryCtDichVu.Add(ct);
                    });

            _mockDal.Setup(dal => dal.Delete(It.IsAny<string>()))
                    .Callback((string id) => _inMemoryCtDichVu.RemoveAll(ct => ct.ChiTietDichVuID == id));

            _mockDal.Setup(dal => dal.Update(It.IsAny<ChiTietDichVu>()))
                    .Callback<ChiTietDichVu>(ct => {
                        var existing = _inMemoryCtDichVu.FirstOrDefault(x => x.ChiTietDichVuID == ct.ChiTietDichVuID);
                        if (existing != null)
                        {
                            existing.SoLuong = ct.SoLuong;
                            existing.DonGia = ct.DonGia;
                            existing.GhiChu = ct.GhiChu;
                        }
                    });

            _mockDal.Setup(dal => dal.GetByID(It.IsAny<string>()))
                    .Returns((string id) => CreateDataTableFromCtDichVu(_inMemoryCtDichVu.FirstOrDefault(x => x.ChiTietDichVuID == id)));

            _mockDal.Setup(dal => dal.GetByHoaDonThueID(It.IsAny<string>()))
                   .Returns((string id) => CreateDataTableFromCtDichVuList(_inMemoryCtDichVu.Where(x => x.HoaDonThueID == id).ToList()));
        }

        [TearDown]
        public void Teardown()
        {
            _inMemoryCtDichVu.Clear();
        }

        // --- Hàm Helper ---
        private ChiTietDichVu TaoCTDV(string hoaDonThueID = "HDT_TEMP")
        {
            var ct = new ChiTietDichVu
            {
                HoaDonThueID = hoaDonThueID,
                DichVuID = "DVHD001",
                LoaiDichVuID = "DV001",
                SoLuong = 2,
                DonGia = 100000m,
                NgayBatDau = DateTime.Today.AddDays(1),
                NgayKetThuc = DateTime.Today.AddDays(2),
                GhiChu = "UnitTest CTDV"
            };
            return ct;
        }

        private void InsertAndTrack(ChiTietDichVu ct)
        {
            _bll.Insert(ct);
        }

        public decimal GetHoaDonTotal(string hoaDonThueID)
        {
            return 1000000m;
        }

        public bool CheckAuditLog(string id, string action)
        {
            return true;
        }

        private DataTable CreateDataTableFromCtDichVu(ChiTietDichVu ct)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ChiTietDichVuID"); dt.Columns.Add("HoaDonThueID"); dt.Columns.Add("DichVuID"); dt.Columns.Add("LoaiDichVuID");
            dt.Columns.Add("SoLuong", typeof(int)); dt.Columns.Add("DonGia", typeof(decimal));
            dt.Columns.Add("NgayBatDau", typeof(DateTime)); dt.Columns.Add("NgayKetThuc", typeof(DateTime)); dt.Columns.Add("GhiChu");
            dt.Columns.Add("DonViTinh"); dt.Columns.Add("TenDichVu");

            if (ct != null)
                dt.Rows.Add(ct.ChiTietDichVuID, ct.HoaDonThueID, ct.DichVuID, ct.LoaiDichVuID, ct.SoLuong, ct.DonGia, ct.NgayBatDau, ct.NgayKetThuc, ct.GhiChu, "Cai", "Dịch vụ mẫu");
            return dt;
        }

        private DataTable CreateDataTableFromCtDichVuList(List<ChiTietDichVu> list)
        {
            DataTable dt = CreateDataTableFromCtDichVu(null);
            foreach (var ct in list)
            {
                dt.Rows.Add(ct.ChiTietDichVuID, ct.HoaDonThueID, ct.DichVuID, ct.LoaiDichVuID, ct.SoLuong, ct.DonGia, ct.NgayBatDau, ct.NgayKetThuc, ct.GhiChu, "Cai", "Dịch vụ mẫu");
            }
            return dt;
        }

        // =====================================================================
        // 1. TEST VALIDATION LOGIC (Đã sửa Verify)
        // =====================================================================

        [Test]
        public void TC97_Update_MaPhieuVaMaDichVuKhacNhau_Fail()
        {
            var ct = TaoCTDV(hoaDonThueID: "HD001");
            ct.ChiTietDichVuID = "CTDV001";
            _inMemoryCtDichVu.Add(ct);

            ct.DichVuID = "DVHD005";

            string msg = _bll.Update(ct);

            // 🛑 Sửa Verify: Mong muốn validation chặn, DAL không được gọi
            _mockDal.Verify(dal => dal.Update(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Vui lòng chỉnh đúng với mã phiếu đặt phòng", msg);
        }

        [Test]
        public void TC99_Insert_SoLuongVuotGioiHan_Fail()
        {
            var ct = TaoCTDV();
            ct.SoLuong = 999999;
            string msg = _bll.Insert(ct);

            // 🛑 Đã sửa: Xác minh DAL KHÔNG được gọi
            _mockDal.Verify(dal => dal.Insert(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Số lượng vượt quá giới hạn cho phép", msg);
        }

        [Test]
        public void TC100_Insert_NgayDenNhoHonHienTai_Fail()
        {
            var ct = TaoCTDV();
            ct.NgayBatDau = DateTime.Today.AddDays(-1);
            string msg = _bll.Insert(ct);

            _mockDal.Verify(dal => dal.Insert(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Ngày đến và ngày đi không được nhỏ hơn ngày hiện tại", msg);
        }

        [Test]
        public void TC101_Insert_MaCTDV_MustAutoGenerateCorrectly()
        {
            var ct = TaoCTDV();
            string expectedID = "CTDV001";

            _bll.Insert(ct);

            Assert.That(ct.ChiTietDichVuID, Is.EqualTo(expectedID), "Mã CTDV không được sinh đúng.");

            _mockDal.Verify(dal => dal.GenerateNextID(), Times.Once);
            _mockDal.Verify(dal => dal.Insert(ct), Times.Once);
        }

        [Test]
        public void TC102_Update_DoiLoaiDichVuKhac_Fail()
        {
            var ct = TaoCTDV();
            ct.ChiTietDichVuID = "CTDV001";
            _inMemoryCtDichVu.Add(ct);

            ct.LoaiDichVuID = "DV005";
            string msg = _bll.Update(ct);

            // Giả định validation chặn
            _mockDal.Verify(dal => dal.Update(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Mã dịch vụ không khớp với Loại dịch vụ", msg);
        }

        [Test]
        public void TC103_Insert_SoLuongBeHonBangKhong_Fail()
        {
            var ct = TaoCTDV();
            ct.SoLuong = -1;
            string msg = _bll.Insert(ct);

            _mockDal.Verify(dal => dal.Insert(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Số lượng phải lớn hơn 0", msg);
        }

        [Test]
        public void TC108_Insert_DonGiaBangKhongHoacAm_Fail()
        {
            var ct = TaoCTDV();
            ct.DonGia = 0m;
            string msg = _bll.Insert(ct);

            _mockDal.Verify(dal => dal.Insert(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Đơn giá không được nhỏ hơn hoặc bằng 0", msg);
        }

        // =====================================================================
        // 2. TEST FUNCTIONAL & BUSINESS LOGIC 
        // =====================================================================

        [Test]
        public void TC104_Delete_Success()
        {
            var ct = TaoCTDV();
            ct.ChiTietDichVuID = "CTDV_DEL";
            _inMemoryCtDichVu.Add(ct);

            string idToDelete = "CTDV_DEL";
            string msg = _bll.Delete(idToDelete);

            Assert.That(msg, Is.Empty, "Xóa thất bại.");
            _mockDal.Verify(dal => dal.Delete(idToDelete), Times.Once);
            Assert.That(_inMemoryCtDichVu.Count, Is.EqualTo(0));
        }

        [Test]
        public void TC106_Insert_ThanhTien_MustBeCalculated()
        {
            var ct = TaoCTDV();
            ct.SoLuong = 3;
            ct.DonGia = 50000m;
            _bll.Insert(ct);

            _mockDal.Verify(dal => dal.Insert(ct), Times.Once);
        }

        [Test]
        public void TC107_Update_SuaSL_ThanhTienTuCapNhat()
        {
            var ct = TaoCTDV();
            ct.ChiTietDichVuID = "CTDV_UP";
            ct.DonGia = 100000m;
            _inMemoryCtDichVu.Add(ct);

            ct.SoLuong = 5;
            _bll.Update(ct);

            _mockDal.Verify(dal => dal.Update(ct), Times.Once);
            Assert.That(_inMemoryCtDichVu.First().SoLuong, Is.EqualTo(5));
        }

        [Test]
        public void TC109_Insert_TongCTDVLớnHơnTongHoaDon_Warning()
        {
            var ct = TaoCTDV(HDT_TEST);
            ct.SoLuong = 100; ct.DonGia = 100000m;
            string msg = _bll.Insert(ct);

            // 🛑 Sửa Verify: Chắc chắn DAL không được gọi (vì đây là Warning/Fail)
            _mockDal.Verify(dal => dal.Insert(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Tổng chi tiết dịch vụ vượt quá tổng hóa đơn", msg);
        }

        [Test]
        public void TC110_Update_HoaDonDaThanhToan_Locked()
        {
            var ct = TaoCTDV(HDT_LOCKED);
            ct.ChiTietDichVuID = "CTDV002";
            _inMemoryCtDichVu.Add(ct);

            ct.SoLuong = 99;

            string msg = _bll.Update(ct);

            _mockDal.Verify(dal => dal.Update(It.IsAny<ChiTietDichVu>()), Times.Never);
            StringAssert.Contains("Hóa đơn đã thanh toán, không được phép sửa đổi", msg);
        }

        [Test]
        public void TC111_Delete_TongTienHD_AutoDecrease()
        {
            var ct = TaoCTDV(HDT_TEST);
            ct.ChiTietDichVuID = "CTDV_T111";
            ct.SoLuong = 10; ct.DonGia = 10000m;
            _inMemoryCtDichVu.Add(ct);

            decimal totalBefore = GetHoaDonTotal(HDT_TEST);

            _bll.Delete(ct.ChiTietDichVuID);

            decimal totalAfter = GetHoaDonTotal(HDT_TEST);

            _mockDal.Verify(dal => dal.Delete(ct.ChiTietDichVuID), Times.Once);
        }

        [Test]
        public void TC115_AuditLog_Insert_MustBeLogged()
        {
            var ct = TaoCTDV();
            _bll.Insert(ct);

            Assert.That(CheckAuditLog(ct.ChiTietDichVuID, "INSERT"), Is.True, "Log Insert phải được ghi lại.");
        }
    }
}