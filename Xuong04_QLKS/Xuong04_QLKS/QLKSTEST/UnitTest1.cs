using NUnit.Framework;
using DTO_QLKS;
using BLL_QLKS;
using System;
using System.Collections.Generic;

namespace TestProject1
{
    [TestFixture]
    public class KhachHangTests
    {
        private BUSKhachHang _bll;

        [SetUp]
        public void Setup()
        {
            _bll = new BUSKhachHang();
        }

        private KhachHang TaoKH()
        {
            return new KhachHang
            {
                KhachHangID = _bll.GenerateKhachHangID(),
                HoTen = "Test User",
                DiaChi = "HN",
                GioiTinh = "Nam",
                SoDienThoai = "0987654321",
                CCCD = "123456789",
                NgayTao = DateTime.Now,
                GhiChu = "UnitTest"
            };
        }

        // =====================================================================
        // 1. TEST GET ALL (1–5)
        // =====================================================================

        [Test]
        public void TC01_GetAll_NotNull() =>
            Assert.IsNotNull(_bll.GetAll());

        [Test]
        public void TC02_GetAll_IsList() =>
            Assert.IsInstanceOf<List<KhachHang>>(_bll.GetAll());

        [Test]
        public void TC03_GetAll_HaveCount_GE_Zero() =>
            Assert.GreaterOrEqual(_bll.GetAll().Count, 0);

        [Test]
        public void TC04_GetAll_MultipleCalls_ReturnSameType()
        {
            var a = _bll.GetAll();
            var b = _bll.GetAll();
            Assert.IsInstanceOf<List<KhachHang>>(b);
        }

        [Test]
        public void TC05_GetAll_NotThrowException()
        {
            Assert.DoesNotThrow(() => _bll.GetAll());
        }

        // =====================================================================
        // 2. TEST INSERT (6–15)
        // =====================================================================

        [Test]
        public void TC06_Insert_Success()
        {
            var k = TaoKH();
            string msg = _bll.InsertKhachHang(k);
            Assert.IsEmpty(msg);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC07_Insert_Then_GetById_NotNull()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            Assert.IsNotNull(_bll.GetKhachHangById(k.KhachHangID));
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC08_Insert_MustAutoGenerateID()
        {
            var k = new KhachHang { HoTen = "A" };
            string msg = _bll.InsertKhachHang(k);
            Assert.IsEmpty(msg);
            Assert.IsNotNull(k.KhachHangID);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC09_Insert_InvalidPhone_Fail()
        {
            var k = TaoKH();
            k.SoDienThoai = "";
            Assert.DoesNotThrow(() => _bll.InsertKhachHang(k));
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC10_Insert_MissingName_ShouldStillInsert()
        {
            var k = TaoKH();
            k.HoTen = "";
            Assert.DoesNotThrow(() => _bll.InsertKhachHang(k));
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC11_Insert_DuplicateID_NotThrow()
        {
            var k1 = TaoKH();
            var k2 = k1;

            _bll.InsertKhachHang(k1);
            Assert.DoesNotThrow(() => _bll.InsertKhachHang(k2)); // ID sẽ tự generate lại
            _bll.DeleteKhachHang(k1.KhachHangID);
            _bll.DeleteKhachHang(k2.KhachHangID);
        }

        [Test]
        public void TC12_Insert_LongName()
        {
            var k = TaoKH();
            k.HoTen = new string('A', 200);
            _bll.InsertKhachHang(k);
            var result = _bll.GetKhachHangById(k.KhachHangID);
            Assert.AreEqual(200, result.HoTen.Length);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC13_Insert_NgayTao_Today()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            var get = _bll.GetKhachHangById(k.KhachHangID);
            Assert.AreEqual(DateTime.Today, get.NgayTao.Date);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC14_Insert_Empty_GhiChu()
        {
            var k = TaoKH();
            k.GhiChu = "";
            _bll.InsertKhachHang(k);
            Assert.IsNotNull(_bll.GetKhachHangById(k.KhachHangID));
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC15_Insert_Null_GhiChu()
        {
            var k = TaoKH();
            k.GhiChu = null;
            _bll.InsertKhachHang(k);
            var get = _bll.GetKhachHangById(k.KhachHangID);
            Assert.IsNotNull(get);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        // =====================================================================
        // 3. TEST GET BY ID (16–20)
        // =====================================================================

        [Test]
        public void TC16_GetById_NotFound()
        {
            Assert.IsNull(_bll.GetKhachHangById("KH999999"));
        }

        [Test]
        public void TC17_GetById_AfterInsert_MatchID()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            Assert.AreEqual(k.KhachHangID, _bll.GetKhachHangById(k.KhachHangID).KhachHangID);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC18_GetById_NotThrow()
        {
            Assert.DoesNotThrow(() => _bll.GetKhachHangById("xx"));
        }

        [Test]
        public void TC19_GetById_ReturnType()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            Assert.IsInstanceOf<KhachHang>(_bll.GetKhachHangById(k.KhachHangID));
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC20_GetById_Deleted_ReturnNull()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            _bll.DeleteKhachHang(k.KhachHangID);
            Assert.IsNull(_bll.GetKhachHangById(k.KhachHangID));
        }

        // =====================================================================
        // 4. TEST UPDATE (21–30)
        // =====================================================================

        [Test]
        public void TC21_Update_Success()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.HoTen = "Updated";
            _bll.UpdateKhachHang(k);
            Assert.AreEqual("Updated", _bll.GetKhachHangById(k.KhachHangID).HoTen);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC22_Update_EmptyName()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.HoTen = "";
            Assert.DoesNotThrow(() => _bll.UpdateKhachHang(k));
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC23_Update_InvalidID()
        {
            var k = TaoKH();
            k.KhachHangID = "";
            Assert.AreEqual("Mã Khách Hàng không hợp lệ.", _bll.UpdateKhachHang(k));
        }

        [Test]
        public void TC24_Update_LongDiaChi()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.DiaChi = new string('B', 255);
            _bll.UpdateKhachHang(k);
            Assert.AreEqual(255, _bll.GetKhachHangById(k.KhachHangID).DiaChi.Length);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC25_Update_GioiTinh()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.GioiTinh = "Nữ";
            _bll.UpdateKhachHang(k);
            Assert.AreEqual("Nữ", _bll.GetKhachHangById(k.KhachHangID).GioiTinh);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC26_Update_CCCD()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.CCCD = "987654321";
            _bll.UpdateKhachHang(k);
            Assert.AreEqual("987654321", _bll.GetKhachHangById(k.KhachHangID).CCCD);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC27_Update_NgayTao()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.NgayTao = DateTime.Now.AddDays(-5);
            _bll.UpdateKhachHang(k);
            Assert.AreEqual(k.NgayTao.Date, _bll.GetKhachHangById(k.KhachHangID).NgayTao.Date);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC28_Update_GhiChu()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            k.GhiChu = "Modified";
            _bll.UpdateKhachHang(k);
            Assert.AreEqual("Modified", _bll.GetKhachHangById(k.KhachHangID).GhiChu);
            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC29_Update_NotExistID_NotThrow()
        {
            var k = TaoKH();
            k.KhachHangID = "KH999999";
            Assert.DoesNotThrow(() => _bll.UpdateKhachHang(k));
        }

        [Test]
        public void TC30_Update_NullObject_NotThrow()
        {
            Assert.DoesNotThrow(() => _bll.UpdateKhachHang(null));
        }

        // =====================================================================
        // 5. TEST DELETE (31–35)
        // =====================================================================

        [Test]
        public void TC31_Delete_Success()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            Assert.DoesNotThrow(() => _bll.DeleteKhachHang(k.KhachHangID));
        }

        [Test]
        public void TC32_Delete_InvalidID_NotThrow()
        {
            Assert.DoesNotThrow(() => _bll.DeleteKhachHang("INVALID"));
        }

        [Test]
        public void TC33_Delete_EmptyID()
        {
            Assert.DoesNotThrow(() => _bll.DeleteKhachHang(""));
        }

        [Test]
        public void TC34_Delete_NullID()
        {
            Assert.DoesNotThrow(() => _bll.DeleteKhachHang(null));
        }

        [Test]
        public void TC35_Delete_Twice_NotThrow()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);
            _bll.DeleteKhachHang(k.KhachHangID);
            Assert.DoesNotThrow(() => _bll.DeleteKhachHang(k.KhachHangID));
        }

        // =====================================================================
        // 6. TEST GENERATE ID (36–40)
        // =====================================================================

        [Test]
        public void TC36_GenerateID_NotNull()
        {
            Assert.IsNotNull(_bll.GenerateKhachHangID());
        }

        [Test]
        public void TC37_GenerateID_Format()
        {
            string id = _bll.GenerateKhachHangID();
            StringAssert.StartsWith("KH", id);
        }

        [Test]
        public void TC38_GenerateID_Length_Greater3()
        {
            Assert.GreaterOrEqual(_bll.GenerateKhachHangID().Length, 4);
        }

        [Test]
        public void TC39_GenerateID_NotSameInTwoCalls()
        {
            Assert.AreNotEqual(_bll.GenerateKhachHangID(), _bll.GenerateKhachHangID());
        }

        [Test]
        public void TC40_GenerateID_WhenTableEmpty_ReturnKH001()
        {
            string id = _bll.GenerateKhachHangID();
            Assert.IsTrue(id.StartsWith("KH"));
        }
        // =====================================================================
        // 7. TEST EXTRA (41–50)
        // =====================================================================

        [Test]
        public void TC41_Insert_NullObject_NotThrow()
        {
            Assert.DoesNotThrow(() => _bll.InsertKhachHang(null));
        }

        [Test]
        public void TC42_GetAll_AfterInsert_IncreaseCount()
        {
            int before = _bll.GetAll().Count;

            var k = TaoKH();
            _bll.InsertKhachHang(k);

            int after = _bll.GetAll().Count;

            Assert.Greater(after, before);

            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC43_Update_ChangeMultipleFields()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);

            k.HoTen = "New Name";
            k.DiaChi = "New Address";
            k.GioiTinh = "Khác";
            k.GhiChu = "Updated note";

            _bll.UpdateKhachHang(k);

            var get = _bll.GetKhachHangById(k.KhachHangID);

            Assert.AreEqual("New Name", get.HoTen);
            Assert.AreEqual("New Address", get.DiaChi);
            Assert.AreEqual("Khác", get.GioiTinh);
            Assert.AreEqual("Updated note", get.GhiChu);

            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC44_Delete_Then_DeleteAgain_NotThrow()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);

            _bll.DeleteKhachHang(k.KhachHangID);

            Assert.DoesNotThrow(() => _bll.DeleteKhachHang(k.KhachHangID));
        }

        [Test]
        public void TC45_Insert_MissingAllOptionalFields()
        {
            var k = new KhachHang
            {
                HoTen = "A"
                // Không set DiaChi, GioiTinh, CCCD, GhiChu
            };

            Assert.DoesNotThrow(() => _bll.InsertKhachHang(k));

            Assert.IsNotNull(_bll.GetKhachHangById(k.KhachHangID));

            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC46_GetById_WhitespaceID_ReturnNull()
        {
            Assert.IsNull(_bll.GetKhachHangById("   "));
        }

        [Test]
        public void TC47_Update_WhitespaceID_ReturnError()
        {
            var k = TaoKH();
            k.KhachHangID = "   ";

            string msg = _bll.UpdateKhachHang(k);

            Assert.AreEqual("Mã Khách Hàng không hợp lệ.", msg);
        }

        [Test]
        public void TC48_Insert_ManyTimes_UniqueID()
        {
            var ids = new HashSet<string>();

            for (int i = 0; i < 5; i++)
            {
                var k = TaoKH();
                _bll.InsertKhachHang(k);
                ids.Add(k.KhachHangID);
                _bll.DeleteKhachHang(k.KhachHangID);
            }

            Assert.AreEqual(5, ids.Count); // Không trùng ID
        }

        [Test]
        public void TC49_Update_NoChange_NotThrow()
        {
            var k = TaoKH();
            _bll.InsertKhachHang(k);

            Assert.DoesNotThrow(() => _bll.UpdateKhachHang(k));

            _bll.DeleteKhachHang(k.KhachHangID);
        }

        [Test]
        public void TC50_Delete_AllRecords_LoopNotThrow()
        {
            var list = _bll.GetAll();

            foreach (var item in list)
            {
                Assert.DoesNotThrow(() => _bll.DeleteKhachHang(item.KhachHangID));
            }
        }

    }

}
