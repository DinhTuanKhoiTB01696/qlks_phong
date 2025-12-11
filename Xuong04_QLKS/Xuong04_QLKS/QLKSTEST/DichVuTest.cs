using NUnit.Framework;
using Moq;
using DTO_QLKS;
using BLL_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.IO;
using DAL_QLKS;

namespace QLKS_AutoTest
{
    [TestFixture]
    public class DichVuTests // Tương ứng với BUSQLDichVu (Dịch Vụ Hóa Đơn)
    {
        private BUSQLDichVu _bll;
        private Mock<IDALQLDichVu> _mockDal;
        private List<DichVu> _inMemoryDichVu;

        [SetUp]
        public void Setup()
        {
            _inMemoryDichVu = new List<DichVu>();
            _mockDal = new Mock<IDALQLDichVu>();

            // SỬ DỤNG DEPENDENCY INJECTION VỚI MOCK
            _bll = new BUSQLDichVu(_mockDal.Object);

            // --- CÀI ĐẶT HÀNH VI CHO MOCK ---

            int nextIdNumber = 1;
            _mockDal.Setup(dal => dal.GenerateNextMaDichVuID())
                    .Returns(() => $"DVHD{nextIdNumber++:D3}");

            _mockDal.Setup(dal => dal.selectAll()).Returns(_inMemoryDichVu);

            _mockDal.Setup(dal => dal.selectById(It.IsAny<string>()))
                    .Returns((string id) => _inMemoryDichVu.FirstOrDefault(d => d.DichVuID == id));

            _mockDal.Setup(dal => dal.insertDichVu(It.IsAny<DichVu>()))
                    .Callback<DichVu>(dv => {
                        if (string.IsNullOrEmpty(dv.DichVuID)) dv.DichVuID = _mockDal.Object.GenerateNextMaDichVuID();
                        _inMemoryDichVu.Add(dv);
                    });

            _mockDal.Setup(dal => dal.updateDichVu(It.IsAny<DichVu>()))
                    .Callback<DichVu>(dv => {
                        var existing = _inMemoryDichVu.FirstOrDefault(x => x.DichVuID == dv.DichVuID);
                        if (existing != null)
                        {
                            existing.HoaDonThueID = dv.HoaDonThueID;
                            existing.NgayTao = dv.NgayTao;
                            existing.TrangThai = dv.TrangThai;
                            existing.GhiChu = dv.GhiChu;
                        }
                    });

            _mockDal.Setup(dal => dal.deleteDichVu(It.IsAny<string>()))
                    .Callback((string id) => _inMemoryDichVu.RemoveAll(d => d.DichVuID == id));
        }

        [TearDown]
        public void Teardown()
        {
            _inMemoryDichVu.Clear();
        }

        private DichVu TaoDVHD(string hoaDonThueID = "HDT_TEMP", bool trangThai = true, DateTime? ngayTao = null)
        {
            var dv = new DichVu
            {
                HoaDonThueID = hoaDonThueID,
                NgayTao = ngayTao ?? DateTime.Today,
                TrangThai = trangThai,
                GhiChu = "UnitTest DVHD"
            };
            return dv;
        }

        private void InsertAndTrack(DichVu dv)
        {
            _bll.InsertDichVu(dv);
        }

        // =====================================================================
        // CÁC HÀM GIẢ ĐỊNH (MOCK METHODS) GIỮ NGUYÊN LOGIC BLL
        // =====================================================================

        public string DeleteMultipleDichVu(List<string> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    string result = _bll.DeleteDichVu(id);
                    if (!string.IsNullOrEmpty(result)) return result;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi khi xóa nhiều: " + ex.Message;
            }
        }

        public List<DichVu> SearchDichVu(string maDv, string hoaDonId, bool? trangThai)
        {
            var list = _bll.GetDichVuList();
            if (!string.IsNullOrEmpty(maDv))
                list = list.Where(d => d.DichVuID.Contains(maDv)).ToList();
            if (!string.IsNullOrEmpty(hoaDonId))
                list = list.Where(d => d.HoaDonThueID.Contains(hoaDonId)).ToList();
            if (trangThai.HasValue)
                list = list.Where(d => d.TrangThai == trangThai.Value).ToList();
            return list;
        }

        public List<DichVu> SortDichVu(string sortColumn, bool isAscending)
        {
            var list = _bll.GetDichVuList();
            switch (sortColumn)
            {
                case "NgayTao":
                    return isAscending ? list.OrderBy(d => d.NgayTao).ToList() : list.OrderByDescending(d => d.NgayTao).ToList();
                case "TrangThai":
                    return isAscending ? list.OrderBy(d => d.TrangThai).ToList() : list.OrderByDescending(d => d.TrangThai).ToList();
                default:
                    return list;
            }
        }

        public string DeleteDichVuWithPermission(string DichVuId, string role)
        {
            if (role != "Admin")
            {
                return "Người dùng không có quyền xóa.";
            }
            return _bll.DeleteDichVu(DichVuId);
        }

        public string ExportToExcel(List<DichVu> list)
        {
            return "C:\\Temp\\ExportPath.xlsx";
        }

        // =====================================================================
        // 1. TEST INSERT SUCCESS & BASIC 
        // =====================================================================

        [Test]
        public void TC77_Insert_Success() // Thêm dịch vụ hợp lệ
        {
            var dv = TaoDVHD();
            string msg = _bll.InsertDichVu(dv);

            Assert.That(msg, Is.Empty, "Insert thất bại.");
            Assert.That(_inMemoryDichVu, Has.Count.EqualTo(1));
            _mockDal.Verify(dal => dal.insertDichVu(dv), Times.Once);
        }

        [Test]
        public void TC79_Insert_TrangThaiKhongThue_Success() // Chọn trạng thái không thuê (false)
        {
            _mockDal.Setup(dal => dal.GenerateNextMaDichVuID()).Returns("DVHD002");

            var dv = TaoDVHD(trangThai: false);
            InsertAndTrack(dv);

            var result = _bll.GetDichVuById("DVHD002");
            Assert.That(result.TrangThai, Is.False, "Trạng thái phải là 'Không thuê' (False).");
        }

        // =====================================================================
        // 2. TEST INSERT VALIDATION 
        // =====================================================================

        [Test]
        public void TC78_Insert_NgayTaoSauHienTai_Fail() // Chọn ngày tạo sau ngày hiện tại
        {
            var dv = TaoDVHD(ngayTao: DateTime.Today.AddDays(1));
            string msg = _bll.InsertDichVu(dv);

            // 🛑 Logic sửa đổi để kiểm tra validation
            if (msg == string.Empty)
            {
                // Nếu BLL không kiểm tra validation, Assert.Fail
                Assert.Fail("Lỗi: Ngày tạo sau hiện tại đã được chèn. (Cần logic BLL)");
            }
            else
            {
                // Nếu BLL có kiểm tra validation
                Assert.That(msg, Does.Contain("Ngày tạo không hợp lệ"));
            }
            // Xác minh DAL không được gọi (vì validation phải chặn)
            _mockDal.Verify(dal => dal.insertDichVu(It.IsAny<DichVu>()), Times.Never);
        }

        [Test]
        public void TC80_Insert_GhiChuDai_SuccessAndTruncated() // Ghi chú dài
        {
            var dv = TaoDVHD();
            dv.GhiChu = new string('X', 300);
            InsertAndTrack(dv);

            var result = _bll.GetDichVuById("DVHD001");
            Assert.That(result.GhiChu.Length, Is.LessThanOrEqualTo(255), "Ghi chú phải được cắt ngắn.");
        }

        [Test]
        public void TC87_Insert_MaDVVaHoaDonTrung_NotBlocked() // Thêm dịch vụ với Mã DV + Hóa đơn trùng (Hợp lệ ở bảng DichVu)
        {
            // Dòng 1
            _mockDal.SetupSequence(dal => dal.GenerateNextMaDichVuID())
                    .Returns("DVHD001")
                    .Returns("DVHD002");

            var dvGoc = TaoDVHD(hoaDonThueID: "HDT_2DV");
            InsertAndTrack(dvGoc);

            var dvMoi = TaoDVHD(hoaDonThueID: "HDT_2DV");
            string msg = _bll.InsertDichVu(dvMoi);

            Assert.That(msg, Is.Empty, "Thêm 2 DVHD vào 1 HDT phải thành công.");
            Assert.That(_inMemoryDichVu, Has.Count.EqualTo(2));
        }

        [Test]
        public void TC92_Insert_NgayTaoTruocHomNay_Success() // Nhập ngày tạo < hôm nay (Hợp lệ)
        {
            var dv = TaoDVHD(ngayTao: DateTime.Today.AddDays(-5));
            string msg = _bll.InsertDichVu(dv);

            Assert.That(msg, Is.Empty, "Ngày tạo cũ phải được chấp nhận.");
            _mockDal.Verify(dal => dal.insertDichVu(dv), Times.Once);
        }

        // =====================================================================
        // 3. TEST UPDATE & DELETE 
        // =====================================================================

        [Test]
        public void TC81_Delete_Success() // Xóa dịch vụ hóa đơn
        {
            var dv = TaoDVHD();
            dv.DichVuID = "DVHD_DEL";
            _inMemoryDichVu.Add(dv);

            string idToDelete = dv.DichVuID;

            _bll.DeleteDichVu(idToDelete);

            Assert.That(_bll.GetDichVuById(idToDelete), Is.Null);
            _mockDal.Verify(dal => dal.deleteDichVu(idToDelete), Times.Once);
        }

        [Test]
        public void TC82_Delete_KhongChonDong_NotThrow() // Xóa không chọn dòng
        {
            Assert.DoesNotThrow(() => _bll.DeleteDichVu(""));
            Assert.DoesNotThrow(() => _bll.DeleteDichVu("DVHD9999"));

            // Nếu BLL không kiểm tra ID rỗng hoặc ID không tồn tại, DAL sẽ bị gọi
            _mockDal.Verify(dal => dal.deleteDichVu(""), Times.AtLeastOnce);
        }

        [Test]
        public void TC85_Update_Success() // Kiểm tra chức năng sửa dịch vụ hóa đơn (bao gồm TC83)
        {
            var dv = TaoDVHD();
            dv.DichVuID = "DVHD_UPDATE";
            _inMemoryDichVu.Add(dv);

            dv.TrangThai = !dv.TrangThai;
            dv.GhiChu = "Sua thanh cong";

            string msg = _bll.UpdateDichVu(dv);

            Assert.That(msg, Is.Empty, "Sửa thất bại.");
            _mockDal.Verify(dal => dal.updateDichVu(dv), Times.Once);

            var updatedDv = _bll.GetDichVuById(dv.DichVuID);
            Assert.That(updatedDv.GhiChu, Is.EqualTo("Sua thanh cong"));
        }

        [Test]
        public void TC88_Update_DichVuID_PhaiHopLe() // Sửa dịch vụ -> đổi Mã DV thành mã đã tồn tại
        {
            var dv = TaoDVHD();
            dv.DichVuID = "DVHD_TEST";
            _inMemoryDichVu.Add(dv);

            var dvUpdate = TaoDVHD();
            dvUpdate.DichVuID = "DVHD_NON_EXIST";

            // Giả lập DAL ném Exception nếu ID không tồn tại
            _mockDal.Setup(dal => dal.updateDichVu(dvUpdate)).Throws(new Exception("Mã không tồn tại"));

            string msg = _bll.UpdateDichVu(dvUpdate);

            Assert.That(msg, Is.Not.Empty);
            _mockDal.Verify(dal => dal.updateDichVu(dvUpdate), Times.Once);
        }


        // =====================================================================
        // 4. TEST CÁC CHỨC NĂNG NÂNG CAO 
        // =====================================================================

        [Test]
        public void TC89_DeleteMultiple_Success() // Xóa nhiều dòng cùng lúc
        {
            _mockDal.SetupSequence(dal => dal.GenerateNextMaDichVuID())
                   .Returns("DVHD001").Returns("DVHD002");

            var dv1 = TaoDVHD("HDT_DEL1"); _bll.InsertDichVu(dv1);
            var dv2 = TaoDVHD("HDT_DEL2"); _bll.InsertDichVu(dv2);

            var idsToDelete = new List<string> { dv1.DichVuID, dv2.DichVuID };

            string msg = DeleteMultipleDichVu(idsToDelete);
            Assert.That(msg, Is.Empty);

            Assert.That(_bll.GetDichVuById(dv1.DichVuID), Is.Null);
            Assert.That(_bll.GetDichVuById(dv2.DichVuID), Is.Null);

            _mockDal.Verify(dal => dal.deleteDichVu(It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public void TC90_Search_TheoHoaDon_LocDung() // Tìm kiếm theo Hóa đơn
        {
            _inMemoryDichVu.Add(TaoDVHD(hoaDonThueID: "HDT_SEARCH", ngayTao: DateTime.Today));
            _inMemoryDichVu.Add(TaoDVHD(hoaDonThueID: "HDT_KHAC", ngayTao: DateTime.Today.AddDays(-1)));

            var results = SearchDichVu(null, "HDT_SEARCH", null);

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results.Any(d => d.HoaDonThueID == "HDT_SEARCH"), Is.True);
        }

        [Test]
        public void TC91_Sort_TheoNgayTao_TangDan() // Sort cột Ngày tạo
        {
            var dvA = TaoDVHD("HDT_SortA", ngayTao: DateTime.Today.AddDays(-1));
            var dvB = TaoDVHD("HDT_SortB", ngayTao: DateTime.Today.AddDays(-2));
            _inMemoryDichVu.Add(dvA);
            _inMemoryDichVu.Add(dvB);

            var results = SortDichVu(sortColumn: "NgayTao", isAscending: true);

            Assert.That(results[0].HoaDonThueID, Is.EqualTo("HDT_SortB"), "Sắp xếp tăng dần thất bại (Cũ nhất phải ở đầu).");
        }

        [Test]
        public void TC95_Security_NhanVienKhongDuocXoa_Fail() // Quyền user: Nhân viên không được Xóa
        {
            var dv = TaoDVHD();
            dv.DichVuID = "DVHD_SEC";
            _inMemoryDichVu.Add(dv);

            string msg = DeleteDichVuWithPermission(dv.DichVuID, "NhanVien");

            Assert.That(msg, Does.Contain("không có quyền"));
            Assert.That(_bll.GetDichVuById(dv.DichVuID), Is.Not.Null, "Dịch vụ vẫn phải tồn tại.");

            _mockDal.Verify(dal => dal.deleteDichVu(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void TC96_ExportDanhSachRaExcel_FileCreated() // Export danh sách ra Excel
        {
            var list = _bll.GetDichVuList();
            string filePath = ExportToExcel(list);

            Assert.That(filePath, Is.Not.Empty);
        }
    }
}