using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLTV.Tests.TestDoubles
{
    /// <summary>
    /// Fake DALKhachHang để test BUS mà không đụng DB thật
    /// Dữ liệu nằm trong RAM
    /// </summary>
    public class FakeDALKhachHang : DALKhachHang
    {
        // Dữ liệu giả trong RAM
        private readonly List<KhachHang> _list = new List<KhachHang>();

        // Lấy tất cả khách hàng (trả bản sao cho an toàn)
        public new List<KhachHang> selectAll()
            => _list.ToList();

        // Tìm kiếm khách hàng theo nhiều tiêu chí
        public new List<KhachHang> SearchKhachHang(string keyword)
            => _list.Where(kh =>
                   (kh.KhachHangID?.Contains(keyword) ?? false) ||
                   (kh.HoTen?.Contains(keyword) ?? false) ||
                   (kh.SoDienThoai?.Contains(keyword) ?? false) ||
                   (kh.CCCD?.Contains(keyword) ?? false) ||
                   (kh.DiaChi?.Contains(keyword) ?? false))
                   .ToList();

        // Tạo mã mới tự động KH001, KH002...
        public new string generateMaKhachHang()
            => "KH" + (_list.Count + 1).ToString("000");

        // Kiểm tra số điện thoại hoặc CCCD có trùng không
        public new bool checkEmailExists(string soDienThoai)
            => _list.Any(kh => kh.SoDienThoai == soDienThoai);

        // Thêm khách hàng mới
        public new void insertKhachHang(KhachHang kh)
            => _list.Add(kh);

        // Cập nhật khách hàng
        public new void updateKhachHang(KhachHang kh)
        {
            var existing = _list.FirstOrDefault(x => x.KhachHangID == kh.KhachHangID);
            if (existing != null)
            {
                existing.HoTen = kh.HoTen;
                existing.DiaChi = kh.DiaChi;
                existing.GioiTinh = kh.GioiTinh;
                existing.SoDienThoai = kh.SoDienThoai;
                existing.CCCD = kh.CCCD;
                existing.NgayTao = kh.NgayTao;
                existing.GhiChu = kh.GhiChu;
            }
        }

        // Xóa khách hàng
        public new void deleteKhachHang(string maKH)
            => _list.RemoveAll(x => x.KhachHangID == maKH);
    }
}
