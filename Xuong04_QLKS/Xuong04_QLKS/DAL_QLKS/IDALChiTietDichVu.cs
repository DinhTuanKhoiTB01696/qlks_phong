using System.Data;
using DTO_QLKS;
using System.Collections.Generic;

namespace DAL_QLKS
{
    public interface IDALChiTietDichVu
    {
        DataTable GetAll();
        DataTable GetByHoaDonThueID(string hoaDonThueID);
        DataTable GetByID(string chiTietDichVuID);
        void Insert(ChiTietDichVu ct);
        void Update(ChiTietDichVu ct);
        void Delete(string id);
        string GenerateNextID();
    }
}