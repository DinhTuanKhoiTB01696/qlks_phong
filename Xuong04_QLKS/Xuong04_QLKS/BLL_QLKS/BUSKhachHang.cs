using DAL_QLKS;
using DTO_QLKS;

public class BUSKhachHang
{
    DALKhachHang dalKhachHang = new DALKhachHang();

    // ✅ Thêm phương thức GetAll() để tương thích với LoadComboBox
    public List<KhachHang> GetAll()
    {
        return GetKhachHangList();
    }

    public List<KhachHang> GetKhachHangList()
    {
        return dalKhachHang.selectAll();
    }

    public KhachHang GetKhachHangById(string id)
    {
        return dalKhachHang.selectById(id);
    }

    public string InsertKhachHang(KhachHang kH)
    {
        try
        {
            kH.KhachHangID = dalKhachHang.generateKhachHangID();
            if (string.IsNullOrEmpty(kH.KhachHangID))
            {
                return "Mã phòng không hợp lệ.";
            }
            dalKhachHang.insertKhachHang(kH);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    public string UpdateKhachHang(KhachHang kH)
    {
        try
        {
            if (string.IsNullOrEmpty(kH.KhachHangID))
            {
                return "Mã Khách Hàng không hợp lệ.";
            }
            dalKhachHang.updateKhachHang(kH);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }

    public string DeleteKhachHang(string KhachHangId)
    {
        try
        {
            dalKhachHang.deleteKhachHang(KhachHangId);
            return string.Empty;
        }
        catch (Exception ex)
        {
            return "Lỗi: " + ex.Message;
        }
    }




    public string GenerateKhachHangID()
    {
        return dalKhachHang.generateKhachHangID();
    }

}
