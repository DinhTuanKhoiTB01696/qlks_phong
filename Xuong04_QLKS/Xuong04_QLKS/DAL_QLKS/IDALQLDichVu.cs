using System.Collections.Generic;
using DTO_QLKS;

namespace DAL_QLKS
{
    public interface IDALQLDichVu
    {
        List<DichVu> selectAll();
        DichVu selectById(string id);
        void insertDichVu(DichVu dv);
        void updateDichVu(DichVu dv);
        void deleteDichVu(string DichVuID);
        string GenerateNextMaDichVuID();
        // Giả định SelectBySql là private hoặc không cần thiết cho BLL, nếu cần thì thêm vào đây
        // List<DichVu> SelectBySql(string sql, Dictionary<string, object> args, CommandType cmdType = CommandType.Text);
    }
}