-- =============================================
-- HỆ THỐNG QUẢN LÝ KHÁCH SẠN
-- Database Creation Script
-- =============================================

USE master;
GO

-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'HotelDB')
BEGIN
    ALTER DATABASE HotelDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE HotelDB;
END
GO

-- Create database with Vietnamese collation
CREATE DATABASE HotelDB
COLLATE Vietnamese_CI_AS;
GO

USE HotelDB;
GO

-- =============================================
-- CREATE TABLES
-- =============================================

-- Table: TAIKHOAN
CREATE TABLE TAIKHOAN (
    TenDangNhap NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(50)
);
GO

-- Table: LOAIPHONG
CREATE TABLE LOAIPHONG (
    MaLoaiPhong NVARCHAR(10) PRIMARY KEY,
    TenLoaiPhong NVARCHAR(50),
    GiaTheoGio DECIMAL(18,2),
    GiaTheoNgay DECIMAL(18,2),
    SoNguoiToiDa INT,
    MoTa NVARCHAR(MAX)
);
GO

-- Table: PHONG
CREATE TABLE PHONG (
    MaPhong NVARCHAR(10) PRIMARY KEY,
    TenPhong NVARCHAR(50),
    MaLoaiPhong NVARCHAR(10),
    Tang NVARCHAR(10),
    TrangThai NVARCHAR(50),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (MaLoaiPhong) REFERENCES LOAIPHONG(MaLoaiPhong)
);
GO

-- Table: KHACHHANG
CREATE TABLE KHACHHANG (
    ID_KH INT IDENTITY(1,1) PRIMARY KEY,
    MaKH NVARCHAR(20),
    HoTen NVARCHAR(100),
    CMND NVARCHAR(20),
    SoDienThoai NVARCHAR(15),
    DiaChi NVARCHAR(200),
    Email NVARCHAR(100),
    NgaySinh DATE,
    GioiTinh NVARCHAR(10)
);
GO

-- Table: DATPHONG
CREATE TABLE DATPHONG (
    ID_DatPhong INT IDENTITY(1,1) PRIMARY KEY,
    MaDatPhong NVARCHAR(20),
    ID_KH INT,
    MaPhong NVARCHAR(10),
    NgayNhanPhong DATETIME,
    NgayTraPhong DATETIME,
    SoNguoi INT,
    TienCoc DECIMAL(18,2),
    TongTien DECIMAL(18,2),
    TrangThai NVARCHAR(50),
    GhiChu NVARCHAR(MAX),
    FOREIGN KEY (ID_KH) REFERENCES KHACHHANG(ID_KH),
    FOREIGN KEY (MaPhong) REFERENCES PHONG(MaPhong)
);
GO

-- Table: DICHVU
CREATE TABLE DICHVU (
    MaDichVu NVARCHAR(10) PRIMARY KEY,
    TenDichVu NVARCHAR(100),
    DonGia DECIMAL(18,2),
    DonViTinh NVARCHAR(20),
    MoTa NVARCHAR(MAX),
    TrangThai BIT
);
GO

-- Table: CHITIETDICHVU
CREATE TABLE CHITIETDICHVU (
    ID_ChiTiet INT IDENTITY(1,1) PRIMARY KEY,
    ID_DatPhong INT,
    MaDichVu NVARCHAR(10),
    SoLuong INT,
    DonGia DECIMAL(18,2),
    ThanhTien DECIMAL(18,2),
    NgaySuDung DATETIME,
    FOREIGN KEY (ID_DatPhong) REFERENCES DATPHONG(ID_DatPhong),
    FOREIGN KEY (MaDichVu) REFERENCES DICHVU(MaDichVu)
);
GO

-- =============================================
-- INSERT SAMPLE DATA
-- =============================================

-- Insert TAIKHOAN
INSERT INTO TAIKHOAN (TenDangNhap, MatKhau) VALUES 
('admin', '123456'),
('user1', '123456'),
('manager', 'manager123');
GO

-- Insert LOAIPHONG
INSERT INTO LOAIPHONG (MaLoaiPhong, TenLoaiPhong, GiaTheoGio, GiaTheoNgay, SoNguoiToiDa, MoTa) VALUES
('LP001', N'Phòng Standard', 100000, 500000, 2, N'Phòng tiêu chuẩn với đầy đủ tiện nghi cơ bản'),
('LP002', N'Phòng Deluxe', 150000, 800000, 2, N'Phòng cao cấp với view đẹp'),
('LP003', N'Phòng VIP', 200000, 1200000, 3, N'Phòng VIP sang trọng'),
('LP004', N'Phòng Family', 180000, 1000000, 4, N'Phòng gia đình rộng rãi'),
('LP005', N'Suite', 300000, 2000000, 2, N'Suite cao cấp nhất');
GO

-- Insert PHONG
INSERT INTO PHONG (MaPhong, TenPhong, MaLoaiPhong, Tang, TrangThai, GhiChu) VALUES
('P101', N'Phòng 101', 'LP001', N'Tầng 1', N'Trống', N''),
('P102', N'Phòng 102', 'LP001', N'Tầng 1', N'Trống', N''),
('P103', N'Phòng 103', 'LP002', N'Tầng 1', N'Trống', N''),
('P201', N'Phòng 201', 'LP002', N'Tầng 2', N'Trống', N''),
('P202', N'Phòng 202', 'LP003', N'Tầng 2', N'Trống', N''),
('P203', N'Phòng 203', 'LP003', N'Tầng 2', N'Trống', N''),
('P301', N'Phòng 301', 'LP004', N'Tầng 3', N'Trống', N''),
('P302', N'Phòng 302', 'LP004', N'Tầng 3', N'Trống', N''),
('P303', N'Phòng 303', 'LP005', N'Tầng 3', N'Trống', N'Suite cao cấp'),
('P401', N'Phòng 401', 'LP005', N'Tầng 4', N'Trống', N'Suite Tổng thống');
GO

-- Insert KHACHHANG
INSERT INTO KHACHHANG (MaKH, HoTen, CMND, SoDienThoai, DiaChi, Email, NgaySinh, GioiTinh) VALUES
('KH001', N'Nguyễn Văn An', '001234567890', '0901234567', N'123 Nguyễn Huệ, Q1, HCM', 'nvan@email.com', '1990-01-15', N'Nam'),
('KH002', N'Trần Thị Bình', '001234567891', '0902345678', N'456 Lê Lợi, Q1, HCM', 'ttbinh@email.com', '1992-05-20', N'Nữ'),
('KH003', N'Lê Văn Cường', '001234567892', '0903456789', N'789 Hai Bà Trưng, Q3, HCM', 'lvcuong@email.com', '1988-11-10', N'Nam'),
('KH004', N'Phạm Thị Dung', '001234567893', '0904567890', N'321 Pasteur, Q3, HCM', 'ptdung@email.com', '1995-07-25', N'Nữ'),
('KH005', N'Hoàng Văn Em', '001234567894', '0905678901', N'654 Võ Văn Tần, Q3, HCM', 'hvem@email.com', '1991-03-18', N'Nam');
GO

-- Insert DATPHONG (Sample bookings)
INSERT INTO DATPHONG (MaDatPhong, ID_KH, MaPhong, NgayNhanPhong, NgayTraPhong, SoNguoi, TienCoc, TongTien, TrangThai, GhiChu) VALUES
('DP001', 1, 'P101', '2025-01-01', '2025-01-03', 2, 200000, 1000000, N'Đã trả phòng', N''),
('DP002', 2, 'P201', '2025-01-05', '2025-01-07', 2, 300000, 1600000, N'Đã trả phòng', N''),
('DP003', 3, 'P202', '2025-01-10', '2025-01-12', 2, 500000, 2400000, N'Đã nhận phòng', N'Khách VIP');
GO

-- Insert DICHVU
INSERT INTO DICHVU (MaDichVu, TenDichVu, DonGia, DonViTinh, MoTa, TrangThai) VALUES
('DV001', N'Giặt ủi', 50000, N'Bộ', N'Dịch vụ giặt ủi quần áo', 1),
('DV002', N'Ăn sáng buffet', 150000, N'Người', N'Buffet sáng đa dạng món ăn', 1),
('DV003', N'Massage', 300000, N'Giờ', N'Dịch vụ massage thư giãn', 1),
('DV004', N'Thuê xe máy', 100000, N'Ngày', N'Thuê xe máy tham quan', 1),
('DV005', N'Karaoke', 200000, N'Giờ', N'Phòng karaoke cao cấp', 1),
('DV006', N'Nước uống', 20000, N'Chai', N'Nước suối, nước ngọt', 1),
('DV007', N'Đưa đón sân bay', 500000, N'Chuyến', N'Dịch vụ đưa đón sân bay', 1),
('DV008', N'Tour tham quan', 800000, N'Ngày', N'Tour tham quan thành phố', 1);
GO

-- Insert CHITIETDICHVU (Sample service usage)
INSERT INTO CHITIETDICHVU (ID_DatPhong, MaDichVu, SoLuong, DonGia, ThanhTien, NgaySuDung) VALUES
(1, 'DV002', 4, 150000, 600000, '2025-01-01'),
(1, 'DV001', 1, 50000, 50000, '2025-01-02'),
(2, 'DV002', 4, 150000, 600000, '2025-01-05'),
(2, 'DV003', 2, 300000, 600000, '2025-01-06'),
(3, 'DV002', 4, 150000, 600000, '2025-01-10'),
(3, 'DV007', 1, 500000, 500000, '2025-01-10');
GO

-- =============================================
-- CREATE VIEWS (Optional - for reporting)
-- =============================================

-- View: Danh sách phòng với thông tin loại phòng
CREATE VIEW vw_PhongDetails AS
SELECT 
    p.MaPhong,
    p.TenPhong,
    p.Tang,
    p.TrangThai,
    lp.TenLoaiPhong,
    lp.GiaTheoGio,
    lp.GiaTheoNgay,
    lp.SoNguoiToiDa,
    p.GhiChu
FROM PHONG p
INNER JOIN LOAIPHONG lp ON p.MaLoaiPhong = lp.MaLoaiPhong;
GO

-- View: Danh sách đặt phòng với thông tin chi tiết
CREATE VIEW vw_DatPhongDetails AS
SELECT 
    dp.ID_DatPhong,
    dp.MaDatPhong,
    kh.HoTen AS TenKhachHang,
    kh.SoDienThoai,
    p.TenPhong,
    lp.TenLoaiPhong,
    dp.NgayNhanPhong,
    dp.NgayTraPhong,
    DATEDIFF(day, dp.NgayNhanPhong, dp.NgayTraPhong) AS SoNgay,
    dp.SoNguoi,
    dp.TienCoc,
    dp.TongTien,
    dp.TrangThai,
    dp.GhiChu
FROM DATPHONG dp
INNER JOIN KHACHHANG kh ON dp.ID_KH = kh.ID_KH
INNER JOIN PHONG p ON dp.MaPhong = p.MaPhong
INNER JOIN LOAIPHONG lp ON p.MaLoaiPhong = lp.MaLoaiPhong;
GO

-- =============================================
-- CREATE STORED PROCEDURES (Optional)
-- =============================================

-- Procedure: Thống kê doanh thu theo tháng
CREATE PROCEDURE sp_ThongKeDoanhThuThang
    @Thang INT,
    @Nam INT
AS
BEGIN
    SELECT 
        COUNT(*) AS SoDatPhong,
        SUM(TongTien) AS TongDoanhThu,
        SUM(TienCoc) AS TongTienCoc,
        AVG(TongTien) AS TrungBinhDoanhThu
    FROM DATPHONG
    WHERE MONTH(NgayNhanPhong) = @Thang 
        AND YEAR(NgayNhanPhong) = @Nam
        AND TrangThai IN (N'Đã nhận phòng', N'Đã trả phòng');
END
GO

-- Procedure: Lấy danh sách phòng trống
CREATE PROCEDURE sp_LayPhongTrong
AS
BEGIN
    SELECT 
        p.*,
        lp.TenLoaiPhong,
        lp.GiaTheoNgay
    FROM PHONG p
    INNER JOIN LOAIPHONG lp ON p.MaLoaiPhong = lp.MaLoaiPhong
    WHERE p.TrangThai = N'Trống'
    ORDER BY p.Tang, p.TenPhong;
END
GO

-- Procedure: Tìm kiếm khách hàng
CREATE PROCEDURE sp_TimKiemKhachHang
    @KeyWord NVARCHAR(100)
AS
BEGIN
    SELECT *
    FROM KHACHHANG
    WHERE HoTen LIKE N'%' + @KeyWord + N'%'
        OR MaKH LIKE N'%' + @KeyWord + N'%'
        OR CMND LIKE N'%' + @KeyWord + N'%'
        OR SoDienThoai LIKE N'%' + @KeyWord + N'%'
    ORDER BY HoTen;
END
GO

-- =============================================
-- STATISTICS QUERIES
-- =============================================

-- Thống kê tổng quan
SELECT 
    (SELECT COUNT(*) FROM PHONG) AS TongSoPhong,
    (SELECT COUNT(*) FROM PHONG WHERE TrangThai = N'Trống') AS PhongTrong,
    (SELECT COUNT(*) FROM PHONG WHERE TrangThai = N'Đã đặt') AS PhongDaDat,
    (SELECT COUNT(*) FROM KHACHHANG) AS TongKhachHang,
    (SELECT COUNT(*) FROM DATPHONG) AS TongDatPhong,
    (SELECT SUM(TongTien) FROM DATPHONG WHERE TrangThai = N'Đã trả phòng') AS TongDoanhThu;
GO

-- =============================================
-- SAMPLE QUERIES FOR TESTING
-- =============================================

-- Xem tất cả phòng với loại phòng
SELECT * FROM vw_PhongDetails;

-- Xem tất cả đặt phòng
SELECT * FROM vw_DatPhongDetails;

-- Tìm phòng trống
EXEC sp_LayPhongTrong;

-- Thống kê doanh thu tháng 1/2025
EXEC sp_ThongKeDoanhThuThang @Thang = 1, @Nam = 2025;

-- Tìm kiếm khách hàng
EXEC sp_TimKiemKhachHang @KeyWord = N'Nguyễn';

PRINT N'Database HotelDB đã được tạo thành công!';
PRINT N'Tài khoản đăng nhập: admin / 123456';
GO
