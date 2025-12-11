CREATE DATABASE Xuong_QuanLyKhachSan1
go
use Xuong_QuanLyKhachSan1


USE master
DROP DATABASE Xuong_QuanLyKhachSan1


-- Bảng Khách Hàng
CREATE TABLE KhachHang (
    KhachHangID CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    DiaChi NVARCHAR(255),
    GioiTinh NVARCHAR(10),
    SoDienThoai VARCHAR(15),
    CCCD VARCHAR(20),
    NgayTao DATE,
    GhiChu NVARCHAR(255)
);

-- Bảng Loại Phòng
CREATE TABLE LoaiPhong (
    MaLoaiPhong CHAR(10) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(100),
	GiaPhong DECIMAL(18, 2),
    NgayTao DATE,
    TrangThai BIT DEFAULT 1, -- 1: Active, 0: Inactive
    GhiChu NVARCHAR(255)
);

-- Bảng Phòng
CREATE TABLE Phong (
    PhongID CHAR(10) PRIMARY KEY,
    TenPhong NVARCHAR(100),
    MaLoaiPhong CHAR(10) FOREIGN KEY REFERENCES LoaiPhong(MaLoaiPhong),
    GiaPhong DECIMAL(18,2),
    NgayTao DATE,
    TinhTrang BIT DEFAULT 1, -- 1: Available, 0: Unavailable
    GhiChu NVARCHAR(255)
);

-- Bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNV CHAR(10) PRIMARY KEY,
    HoTen NVARCHAR(100),
    GioiTinh NVARCHAR(10),
    Email NVARCHAR(100) UNIQUE NOT NULL,
    MatKhau NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255),
    VaiTro NVARCHAR(50),
    TinhTrang BIT DEFAULT 1 -- 1: Working, 0: Resigned
);

-- Bảng Loại Trạng Thái Đặt Phòng (Không gán trực tiếp vào DatPhong)
CREATE TABLE LoaiTrangThaiDatPhong (
    LoaiTrangThaiID CHAR(10) PRIMARY KEY,
    TenTrangThai NVARCHAR(50) UNIQUE
);


-- Bảng Đặt Phòng
CREATE TABLE DatPhong (
    HoaDonThueID CHAR(10) PRIMARY KEY,
    KhachHangID CHAR(10) FOREIGN KEY REFERENCES KhachHang(KhachHangID),
    PhongID CHAR(10) FOREIGN KEY REFERENCES Phong(PhongID),
    NgayDen DATE,
    NgayDi DATE,
    MaNV CHAR(10) FOREIGN KEY REFERENCES NhanVien(MaNV),
    GhiChu NVARCHAR(255),
	TrangThai BIT NOT NULL DEFAULT 0
);

-- Bảng Trạng Thái Đặt Phòng (Lưu lịch sử trạng thái của từng đặt phòng)
CREATE TABLE TrangThaiDatPhong (
    TrangThaiID CHAR(10) PRIMARY KEY,
    HoaDonThueID CHAR(10) FOREIGN KEY REFERENCES DatPhong(HoaDonThueID),
    LoaiTrangThaiID CHAR(10) FOREIGN KEY REFERENCES LoaiTrangThaiDatPhong(LoaiTrangThaiID),
    NgayCapNhat DATETIME DEFAULT GETDATE()
);

	-- Bảng Loại Dịch Vụ
	CREATE TABLE LoaiDichVu (
		LoaiDichVuID CHAR(10) PRIMARY KEY,
		TenDichVu NVARCHAR(100),
		GiaDichVu DECIMAL(18,2),
		DonViTinh NVARCHAR(50),
		NgayTao DATE,
		TrangThai BIT DEFAULT 1, -- 1: Active, 0: Inactive
		GhiChu NVARCHAR(255)
	);

	-- Bảng Dịch Vụ (Tránh lưu loại dịch vụ trực tiếp, chỉ tham chiếu đến LoaiDichVu)
	CREATE TABLE DichVu (
		DichVuID CHAR(10) PRIMARY KEY,
		HoaDonThueID CHAR(10) FOREIGN KEY REFERENCES DatPhong(HoaDonThueID),
		LoaiDichVuID CHAR(10),
		NgayTao DATE,
		TrangThai BIT DEFAULT 1, -- 1: Active, 0: Inactive
		GhiChu NVARCHAR(255)
	);

	CREATE TABLE ChiTietDichVu (
		ChiTietDichVuID CHAR(10) PRIMARY KEY,
		HoaDonThueID CHAR(10) FOREIGN KEY REFERENCES DatPhong(HoaDonThueID),
		DichVuID CHAR(10) FOREIGN KEY REFERENCES DichVu(DichVuID),
		LoaiDichVuID CHAR(10) FOREIGN KEY REFERENCES LoaiDichVu(LoaiDichVuID),
		SoLuong INT,
		NgayBatDau DATE,
		NgayKetThuc DATE,
		GhiChu NVARCHAR(255)
	);


CREATE TABLE HoaDonThanhToan (
    HoaDonID CHAR(10) PRIMARY KEY,
    HoaDonThueID CHAR(10) NOT NULL,  
    NgayThanhToan DATE NOT NULL,
    PhuongThucThanhToan NVARCHAR(50),
    GhiChu NVARCHAR(255),
    TrangThai BIT NOT NULL DEFAULT 0,  -- 0: chưa thanh toán, 1: đã thanh toán
    CONSTRAINT FK_HoaDonThanhToan_DatPhong
        FOREIGN KEY (HoaDonThueID) REFERENCES DatPhong(HoaDonThueID)
);

DROP TABLE HoaDonThanhToan;




WITH TongTienDichVu AS (
    SELECT 
        ctdv.HoaDonThueID,
        SUM(ISNULL(ldv.GiaDichVu, 0) * ISNULL(ctdv.SoLuong, 0)) AS TongTienDV
    FROM ChiTietDichVu ctdv
    JOIN LoaiDichVu ldv ON ctdv.LoaiDichVuID = ldv.LoaiDichVuID
    GROUP BY ctdv.HoaDonThueID
)

SELECT 
    dp.HoaDonThueID,
    (DATEDIFF(DAY, dp.NgayDen, dp.NgayDi) * ISNULL(p.GiaPhong, 0)) AS TienPhong,
    ISNULL(tdv.TongTienDV, 0) AS TienDichVu,
    (DATEDIFF(DAY, dp.NgayDen, dp.NgayDi) * ISNULL(p.GiaPhong, 0)) + ISNULL(tdv.TongTienDV, 0) AS TongTien
FROM DatPhong dp
JOIN Phong p ON dp.PhongID = p.PhongID
LEFT JOIN TongTienDichVu tdv ON dp.HoaDonThueID = tdv.HoaDonThueID;









CREATE PROCEDURE TKDoanhThuTheoNhanVien
    @TuNgay DATE,
    @DenNgay DATE
AS
BEGIN
    SELECT 
        nv.MaNV,
        nv.HoTen,
        COUNT(DISTINCT dp.HoaDonThueID) AS SoPhieuThue,
        SUM(
            DATEDIFF(DAY, dp.NgayDen, dp.NgayDi) * p.GiaPhong + 
            ISNULL(tdv.TongTienDV, 0)
        ) AS TongDoanhThu
    FROM DatPhong dp
    JOIN Phong p ON dp.PhongID = p.PhongID
    JOIN NhanVien nv ON dp.MaNV = nv.MaNV
    JOIN TrangThaiDatPhong tt ON dp.HoaDonThueID = tt.HoaDonThueID
    LEFT JOIN (
        SELECT 
            ctdv.HoaDonThueID,
            SUM(ldv.GiaDichVu * ctdv.SoLuong) AS TongTienDV
        FROM ChiTietDichVu ctdv
        JOIN LoaiDichVu ldv ON ctdv.LoaiDichVuID = ldv.LoaiDichVuID
        GROUP BY ctdv.HoaDonThueID
    ) tdv ON dp.HoaDonThueID = tdv.HoaDonThueID
    WHERE 
        dp.NgayDen >= @TuNgay AND dp.NgayDi <= @DenNgay
        AND tt.LoaiTrangThaiID = 'TT003'
    GROUP BY nv.MaNV, nv.HoTen
    ORDER BY TongDoanhThu DESC;
END;
GO




CREATE PROCEDURE TKDoanhThuTheoLoaiPhong
    @TuNgay DATE,
    @DenNgay DATE
AS
BEGIN
    SELECT 
        lp.MaLoaiPhong,
        lp.TenLoaiPhong,
        COUNT(DISTINCT dp.HoaDonThueID) AS SoPhieuThue,
        SUM(
            DATEDIFF(DAY, dp.NgayDen, dp.NgayDi) * ISNULL(p.GiaPhong, 0)
            + ISNULL(tdv.TongTienDV, 0)
        ) AS TongDoanhThu
    FROM DatPhong dp
    JOIN Phong p ON dp.PhongID = p.PhongID
    JOIN LoaiPhong lp ON p.MaLoaiPhong = lp.MaLoaiPhong
    JOIN TrangThaiDatPhong tt ON dp.HoaDonThueID = tt.HoaDonThueID
    LEFT JOIN (
        SELECT 
            ctdv.HoaDonThueID,
            SUM(ldv.GiaDichVu * ctdv.SoLuong) AS TongTienDV
        FROM ChiTietDichVu ctdv
        JOIN LoaiDichVu ldv ON ctdv.LoaiDichVuID = ldv.LoaiDichVuID
        GROUP BY ctdv.HoaDonThueID
    ) tdv ON dp.HoaDonThueID = tdv.HoaDonThueID
    WHERE 
        dp.NgayDen >= @TuNgay AND dp.NgayDi <= @DenNgay
        AND tt.LoaiTrangThaiID = 'TT003'
    GROUP BY lp.MaLoaiPhong, lp.TenLoaiPhong
    ORDER BY TongDoanhThu DESC;
END;
GO




CREATE PROCEDURE sp_LayPhongTrong
    @NgayDen DATE,
    @NgayDi DATE
AS
BEGIN
    SELECT *
    FROM Phong p
    WHERE p.TinhTrang = 1
      AND NOT EXISTS (
          SELECT 1
          FROM DatPhong dp
          JOIN TrangThaiDatPhong ttdp ON dp.HoaDonThueID = ttdp.HoaDonThueID
          JOIN LoaiTrangThaiDatPhong lttdp ON ttdp.LoaiTrangThaiID = lttdp.LoaiTrangThaiID
          WHERE dp.PhongID = p.PhongID
            AND @NgayDen < dp.NgayDi AND @NgayDi > dp.NgayDen
            AND lttdp.TenTrangThai != N'Hủy'
            AND ttdp.NgayCapNhat = (
                SELECT MAX(ttdp2.NgayCapNhat)
                FROM TrangThaiDatPhong ttdp2
                WHERE ttdp2.HoaDonThueID = dp.HoaDonThueID
            )
      )
END;
GO


CREATE PROCEDURE sp_KiemTraPhongDaDuocDat
    @PhongID CHAR(10),
    @NgayDen DATE,
    @NgayDi DATE
AS
BEGIN
    SELECT COUNT(*) AS SoLanDat
    FROM DatPhong dp
    JOIN TrangThaiDatPhong ttdp ON dp.HoaDonThueID = ttdp.HoaDonThueID
    JOIN LoaiTrangThaiDatPhong lttdp ON ttdp.LoaiTrangThaiID = lttdp.LoaiTrangThaiID
    WHERE dp.PhongID = @PhongID
      AND lttdp.TenTrangThai != N'Hủy'
      AND ttdp.NgayCapNhat = (
            SELECT MAX(ttdp2.NgayCapNhat)
            FROM TrangThaiDatPhong ttdp2
            WHERE ttdp2.HoaDonThueID = dp.HoaDonThueID
      )
      AND NOT (
          dp.NgayDi <= @NgayDen OR dp.NgayDen >= @NgayDi
      )
END;
GO






CREATE PROCEDURE KiemTraPhongTrong
    @PhongID CHAR(10),
    @NgayDen DATE,
    @NgayDi DATE
AS
BEGIN
    SELECT dp.*, lttdp.TenTrangThai
    FROM DatPhong dp
    JOIN TrangThaiDatPhong ttdp ON dp.HoaDonThueID = ttdp.HoaDonThueID
    JOIN LoaiTrangThaiDatPhong lttdp ON ttdp.LoaiTrangThaiID = lttdp.LoaiTrangThaiID
    WHERE dp.PhongID = @PhongID
      AND lttdp.TenTrangThai NOT LIKE N'%Hủy%'
      AND NOT (
          dp.NgayDi <= @NgayDen OR dp.NgayDen >= @NgayDi
      );
END;



-- Thêm dữ liệu vào bảng Khách Hàng
INSERT INTO KhachHang (KhachHangID, HoTen, DiaChi, GioiTinh, SoDienThoai, CCCD, NgayTao, GhiChu) VALUES
('KH001', N'Nguyễn Văn An' , N'Hà Nội'         , N'Nam', '0934567890', '123456789012', '2025-05-01', N'Khách quen'),
('KH002', N'Trần Thị Bích' , N'Tp. Hồ Chí Minh', N'Nữ' , '0987654321', '987654321098', '2025-05-02', N''),
('KH003', N'Lê Hữu Cường'  , N'Đà Nẵng'        , N'Nam', '0369854123', '456789123456', '2025-05-03', N''),
('KH004', N'Phạm Văn Đức'  , N'Hải Phòng'      , N'Nam', '0912345678', '789456123789', '2025-05-04', N'Ưu tiên phòng VIP'),
('KH005', N'Hoàng Thị Minh', N'Cần Thơ'        , N'Nữ' , '0897654321', '654321987654', '2025-05-05', N'');

-- Thêm dữ liệu vào bảng Loại Phòng
INSERT INTO LoaiPhong (MaLoaiPhong, TenLoaiPhong, NgayTao, TrangThai, GhiChu) VALUES
('LP001', N'Deluxe'  , '2025-01-01', 1, N''),
('LP002', N'Standard', '2025-01-02', 1, N''),
('LP003', N'Suite'   , '2025-01-03', 1, N''),
('LP004', N'Family'  , '2025-01-04', 1, N''),
('LP005', N'Superior', '2025-01-05', 1, N'');

-- Thêm dữ liệu vào bảng Phòng
INSERT INTO Phong (PhongID, TenPhong, MaLoaiPhong, GiaPhong, NgayTao, TinhTrang, GhiChu) VALUES
('P001', N'Phòng Deluxe'  , 'LP001', 1000000, '2025-01-01', 0, N'View biển'),
('P002', N'Phòng Standard', 'LP002', 600000 , '2025-01-02', 0, N'View vườn'),
('P003', N'Phòng Suite'   , 'LP003', 2500000, '2025-01-03', 0, N'Có ban công'),
('P004', N'Phòng Family'  , 'LP004', 1500000, '2025-01-04', 0, N'2 giường đôi'),
('P005', N'Phòng Superior', 'LP005', 800000 , '2025-01-05', 0, N'View hồ bơi');

-- Thêm dữ liệu vào bảng Nhân Viên
INSERT INTO NhanVien (MaNV, HoTen, GioiTinh, Email, MatKhau, DiaChi, VaiTro, TinhTrang) VALUES
('NV001', N'Nguyễn Văn Nam', N'Nam', 'nam.nguyen@hotel.com', 'abc123', N'Hà Nội'         , N'Lễ Tân' , 1),
('NV002', N'Trần Minh Tuấn', N'Nam', 'tuan.tran@hotel.com' , 'abc123', N'Tp. Hồ Chí Minh', N'Quản Lý', 1),
('NV003', N'Lê Thị Hoa'    , N'Nữ' , 'hoa.le@hotel.com'    , 'abc123', N'Đà Nẵng'        , N'Phục Vụ', 1),
('NV004', N'Phạm Văn Bình' , N'Nam', 'binh.pham@hotel.com' , 'abc123', N'Hải Phòng'      , N'Bảo Vệ' , 1),
('NV005', N'Hoàng Thị Lan' , N'Nữ' , 'lan.hoang@hotel.com' , 'abc123', N'Cần Thơ'        , N'Tạp Vụ' , 1);

-- Thêm dữ liệu vào bảng Loại Trạng Thái Đặt Phòng
INSERT INTO LoaiTrangThaiDatPhong (LoaiTrangThaiID, TenTrangThai) VALUES
('TT001', N'Đang ở'),
('TT002', N'Hủy'),
('TT003', N'Đã thanh toán'),
('TT004', N'Chưa thanh toán'),
('TT005', N'Đã đặt thành công'),
('TT006', N'Chờ xác nhận'),
('TT007', N'Phòng đang sửa chữa');

-- Thêm dữ liệu vào bảng Đặt Phòng
INSERT INTO DatPhong (HoaDonThueID, KhachHangID, PhongID, NgayDen, NgayDi, MaNV, GhiChu, TrangThai) 
VALUES
('HD001', 'KH001', 'P001', '2025-05-10', '2025-05-15', 'NV001', N'Yêu cầu phòng tầng cao'	,1), 
('HD002', 'KH002', 'P002', '2025-05-12', '2025-05-14', 'NV002', N''							,1),                         
('HD003', 'KH003', 'P003', '2025-05-13', '2025-05-17', 'NV003', N'Ăn sáng miễn phí'			,0),         
('HD004', 'KH004', 'P004', '2025-05-14', '2025-05-18', 'NV004', N''							,0),                         
('HD005', 'KH005', 'P005', '2025-05-15', '2025-05-20', 'NV005', N'Xe đưa đón sân bay'		,1);     
     
-- Thêm dữ liệu vào bảng Trạng Thái Đặt Phòng
INSERT INTO TrangThaiDatPhong (TrangThaiID, HoaDonThueID, LoaiTrangThaiID, NgayCapNhat) VALUES
('TTDP001', 'HD001', 'TT001', '2025-05-10 10:00:00'),
('TTDP002', 'HD002', 'TT005', '2025-05-12 12:00:00'),
('TTDP003', 'HD003', 'TT004', '2025-05-13 15:00:00'),
('TTDP004', 'HD004', 'TT002', '2025-05-14 08:00:00'),
('TTDP005', 'HD005', 'TT003', '2025-05-15 18:00:00');

-- Thêm dữ liệu vào bảng Loại Dịch Vụ
INSERT INTO LoaiDichVu (LoaiDichVuID, TenDichVu, GiaDichVu, DonViTinh, NgayTao, TrangThai, GhiChu) VALUES
('DV001', N'Dịch vụ ăn sáng'        , 150000 , N'Lượt', '2025-01-01', 1, N''),
('DV002', N'Dịch vụ giặt ủi'        , 50000  , N'Lần' , '2025-01-02', 1, N''),
('DV003', N'Dịch vụ spa'            , 500000 , N'Lượt', '2025-01-03', 1, N''),
('DV004', N'Dịch vụ đưa đón sân bay', 200000 , N'Lượt', '2025-01-04', 1, N''),
('DV005', N'Dịch vụ thuê xe'        , 300000 , N'Ngày', '2025-01-05', 1, N'');

-- Thêm dữ liệu vào bảng Dịch Vụ
INSERT INTO DichVu (DichVuID, HoaDonThueID, NgayTao, TrangThai, GhiChu) VALUES
('DVHD001', 'HD001', '2025-05-10', 1, N''),
('DVHD002', 'HD002', '2025-05-12', 1, N''),
('DVHD003', 'HD003', '2025-05-13', 1, N''),
('DVHD004', 'HD004', '2025-05-14', 1, N''),	
('DVHD005', 'HD005', '2025-05-15', 1, N'');

-- Thêm dữ liệu vào bảng Chi Tiết Dịch Vụ
INSERT INTO ChiTietDichVu (ChiTietDichVuID, HoaDonThueID, DichVuID, LoaiDichVuID, SoLuong, NgayBatDau, NgayKetThuc, GhiChu) VALUES
('CTDV001', 'HD001', 'DVHD001', 'DV001', 2, '2025-05-10', '2025-05-15', N'Sử dụng ăn sáng miễn phí'),
('CTDV002', 'HD002', 'DVHD002', 'DV002', 1, '2025-05-12', '2025-05-12', N'Spa trị liệu 90 phút'),
('CTDV003', 'HD003', 'DVHD003', 'DV003', 3, '2025-05-13', '2025-05-17', N'Thuê xe tự lái'),
('CTDV004', 'HD004', 'DVHD004', 'DV004', 1, '2025-05-14', '2025-05-14', N'Xe đưa đón sân bay'),
('CTDV005', 'HD005', 'DVHD005', 'DV005', 5, '2025-05-15', '2025-05-20', N'Dịch vụ giặt ủi hàng ngày');


INSERT INTO HoaDonThanhToan (HoaDonID, HoaDonThueID, NgayThanhToan, PhuongThucThanhToan, GhiChu, TrangThai)
VALUES 
('HDTT001', 'HD001', '2025-05-15', N'Tiền mặt'      , N'Thanh toán đầy đủ (gồm ăn sáng miễn phí)', 0),
('HDTT002', 'HD002', '2025-05-14', N'Chuyển khoản'  , N'Chỉ sử dụng dịch vụ giặt ủi 1 lần', 1),
('HDTT003', 'HD003', '2025-05-17', N'Momo'          , N'Dùng 3 lượt spa, có thuê xe', 0),
('HDTT004', 'HD004', '2025-05-18', N'Tiền mặt'      , N'Đã hủy đặt phòng, không thu phí', 0),
('HDTT005', 'HD005', '2025-05-20', N'Chuyển khoản'  , N'Dùng dịch vụ giặt ủi & đưa đón sân bay', 1);



