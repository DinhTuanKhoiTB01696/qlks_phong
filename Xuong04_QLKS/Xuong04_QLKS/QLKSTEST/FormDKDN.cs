using NUnit.Framework;
using BLL_QLKS;
using DTO_QLKS;
using System;
using System.Threading; // CẦN THIẾT cho TC30 (Thread.Sleep)

namespace FormDKDN
{
    [TestFixture]
    public class Login30TestCases
    {
        private BUSNhanVien _bll;

        // TÀI KHOẢN GỐC TỪ DB MẪU:
        // - Lễ Tân: nam.nguyen@hotel.com (Mật khẩu: abc123)
        // - Quản Lý: tuan.tran@hotel.com (Mật khẩu: abc123)
        // - Khóa: locked@hotel.com (Giả định TinhTrang = 0)

        [SetUp]
        public void Setup()
        {
            _bll = new BUSNhanVien();
        }

        // =================================================================
        // 1. TEST CHÍNH XÁC & CƠ BẢN
        // =================================================================

        // 1. Đăng nhập đúng
        [Test]
        public void TC01_CorrectLogin() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "abc123"), Is.Not.Null);

        // 2. Sai mật khẩu
        [Test]
        public void TC02_WrongPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "wrong"), Is.Null);

        // 3. Tài khoản không tồn tại
        [Test]
        public void TC03_UserNotExist() =>
            Assert.That(_bll.DangNhap("noone@gmail.com", "123"), Is.Null);

        // 4. Username rỗng
        [Test]
        public void TC04_EmptyUsername() =>
            Assert.That(_bll.DangNhap("", "abc123"), Is.Null);

        // 5. Password rỗng
        [Test]
        public void TC05_EmptyPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", ""), Is.Null);

        // 6. Null username
        [Test]
        public void TC06_NullUsername() =>
            Assert.That(_bll.DangNhap(null, "abc123"), Is.Null);

        // 7. Null password
        [Test]
        public void TC07_NullPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", null), Is.Null);

        // 8. Cả hai null
        [Test]
        public void TC08_BothNull() =>
            Assert.That(_bll.DangNhap(null, null), Is.Null);

        // =================================================================
        // 2. TEST BẢO MẬT & VALIDATION
        // =================================================================

        // 9. SQL Injection 1
        [Test]
        public void TC09_SQLInjection1() =>
            Assert.That(_bll.DangNhap("' OR 1=1 --", "123"), Is.Null);

        // 10. SQL Injection 2
        [Test]
        public void TC10_SQLInjection2() =>
            Assert.That(_bll.DangNhap("\" OR \"\"=\"", "123"), Is.Null);

        // 11. Username có khoảng trắng cuối (PASS nếu BLL dùng Trim())
        [Test]
        public void TC11_TrimUsername() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com ", "abc123"), Is.Not.Null);

        // 12. Password có khoảng trắng đầu (PASS nếu BLL dùng Trim())
        [Test]
        public void TC12_TrimPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", " abc123 "), Is.Not.Null);

        // 13. Username quá dài
        [Test]
        public void TC13_TooLongUsername() =>
            Assert.That(_bll.DangNhap(new string('a', 200) + "@a.com", "abc123"), Is.Null);

        // 14. Password quá dài
        [Test]
        public void TC14_TooLongPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", new string('b', 200)), Is.Null);

        // 15. Username toàn ký tự đặc biệt
        [Test]
        public void TC15_SpecialCharsUsername() =>
            Assert.That(_bll.DangNhap("@#$%^&*", "123"), Is.Null);

        // 16. Password toàn ký tự đặc biệt
        [Test]
        public void TC16_SpecialCharsPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "@#$%^&*"), Is.Null);

        // 17. Username viết hoa
        [Test]
        public void TC17_UpperCaseUsername() =>
            Assert.That(_bll.DangNhap("NAM.NGUYEN@HOTEL.COM", "abc123"), Is.Not.Null);

        // 18. Password viết hoa
        [Test]
        public void TC18_UpperCasePassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "ABC"), Is.Null);

        // 23. Email sai định dạng 1
        [Test]
        public void TC23_InvalidEmail1() =>
            Assert.That(_bll.DangNhap("abc@", "123"), Is.Null);

        // 24. Email sai định dạng 2
        [Test]
        public void TC24_InvalidEmail2() =>
            Assert.That(_bll.DangNhap("abc.com", "123"), Is.Null);

        // 25. Email sai định dạng 3
        [Test]
        public void TC25_InvalidEmail3() =>
            Assert.That(_bll.DangNhap("@gmail.com", "123"), Is.Null);

        // 26. Mật khẩu chứa khoảng trắng giữa
        [Test]
        public void TC26_PasswordContainSpaceMid() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "ab c123"), Is.Null);

        // 27. Username số
        [Test]
        public void TC27_NumericUsername() =>
            Assert.That(_bll.DangNhap("123456", "123"), Is.Null);

        // 28. Password số
        [Test]
        public void TC28_NumericPassword() =>
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "123456"), Is.Null);

        // 29. Login với ký tự Unicode
        [Test]
        public void TC29_UnicodeChars() =>
            Assert.That(_bll.DangNhap("tên@khách.sạn", "123"), Is.Null);

        // =================================================================
        // 3. TEST TRẠNG THÁI & PHÂN QUYỀN
        // =================================================================

        // 19. Nhập nhanh liên tục 5 lần (Chỉ test logic cơ bản)
        [Test]
        public void TC19_ManyAttempts()
        {
            NhanVien result = null;
            for (int i = 0; i < 5; i++)
                result = _bll.DangNhap("nam.nguyen@hotel.com", "wrong");
            Assert.That(result, Is.Null);
        }

        // 20. Tài khoản bị khóa (PASS nếu DAL kiểm tra TinhTrang = 1)
        [Test]
        public void TC20_LockedAccount() =>
            Assert.That(_bll.DangNhap("locked@hotel.com", "123"), Is.Null);

        // 21. Kiểm tra phân quyền Admin
        [Test]
        public void TC21_CheckAdminRole()
        {
            NhanVien admin = _bll.DangNhap("tuan.tran@hotel.com", "abc123");
            Assert.That(admin, Is.Not.Null);
            Assert.That(admin.VaiTro, Is.EqualTo("Quản Lý"), "Vai trò phải là Quản Lý.");
        }

        // 22. Kiểm tra phân quyền User
        [Test]
        public void TC22_CheckUserRole()
        {
            NhanVien user = _bll.DangNhap("nam.nguyen@hotel.com", "abc123");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.VaiTro, Is.EqualTo("Lễ Tân"), "Vai trò phải là Lễ Tân.");
        }

        // 30. Đăng nhập khi server chậm (giả lập)
        [Test]
        public void TC30_LoginSlowServer()
        {
            Thread.Sleep(500); // mô phỏng server chậm
            Assert.That(_bll.DangNhap("nam.nguyen@hotel.com", "sai"), Is.Null);
        }
    }
}