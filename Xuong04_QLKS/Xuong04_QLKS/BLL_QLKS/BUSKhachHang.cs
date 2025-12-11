using DAL_QLKS;
using DTO_QLKS;
using System;
using System.Collections.Generic;

public class BUSKhachHang
{
    private readonly DALKhachHang _dal;

    // ✅ Constructor chạy thật
    public BUSKhachHang()
    {
        _dal = new DALKhachHang();
    }

    // ✅ Constructor để Unit Test dùng Fake DAL
    public BUSKhachHang(DALKhachHang dal)
    {
        _dal = dal;
    }

    // Lấy tất cả khách hàng
    public List<KhachHang> GetAll()
    {
        return _dal.selectAll();
    }

    // Lấy danh sách (alias cho GetAll)
    public List<KhachHang> GetKhachHangList()
    {
        return _dal.selectAll();
    }

    // Lấy theo ID
    public KhachHang GetKhachHangById(string id)
    {
        return _dal.selectById(id);
    }

    // Thêm khách hàng
    public string InsertKhachHang(KhachHang kh)
    {
        try
        {
            kh.KhachHangID = _dal.generateKhachHangID();

            if (string.IsNullOrEmpty(kh.KhachHangID))
                return "Mã khách hàng không hợp lệ.";

            _dal.insertKhachHang(kh);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    // Cập nhật
    public string UpdateKhachHang(KhachHang kh)
    {
        try
        {
            if (string.IsNullOrEmpty(kh.KhachHangID))
                return "Mã khách hàng không hợp lệ.";

            _dal.updateKhachHang(kh);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    // Xóa
    public string DeleteKhachHang(string id)
    {
        try
        {
            _dal.deleteKhachHang(id);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    // Sinh mã
    public string GenerateKhachHangID()
    {
        return _dal.generateKhachHangID();
    }
}
