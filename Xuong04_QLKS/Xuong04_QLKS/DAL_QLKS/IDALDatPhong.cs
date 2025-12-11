using System;
using System.Collections.Generic;
using DTO_QLKS;

public interface IDALDatPhong
{
    List<DatPhong> selectAll();
    DatPhong selectById(string id);
    bool insertDatPhong(DatPhong dp);
    void updateDatPhong(DatPhong dp);
    void deleteDatPhong(string id);
    string generateHoaDonThueID();

    // === PHẦN BỔ SUNG KHỚP BUS ===
    bool KiemTraPhongDaDuocDat_SP(string phongID, DateTime ngayDen, DateTime ngayDi);
    DatPhongView GetThongTinDatPhongChiTiet(string hoaDonThueID);
}
