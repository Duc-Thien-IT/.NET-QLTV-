----------THAO TÁC TRÊN BẢNG USER----------------------

create proc dsU
as 
begin
	select * from users
end


go 
create proc themU @tk varchar(10), @mk varchar(200), @ht nvarchar(30), @mk_md5 varchar(200), @sdt varchar(11), @role varchar(6)
as
	begin 
		insert into users
		values(@tk,@mk,@ht,@mk_md5,@sdt,@role)
	end
go
create proc suaU @tk varchar(10),@mk varchar(200), @ht nvarchar(30), @mk_md5 varchar(200), @sdt varchar(11)
as
	begin
		update users
		set matkhau = @mk, tenuser = @ht, mk_md5 = @mk_md5, phone = @sdt
		where taikhoan = @tk
	end
go
create proc xoaU @tk varchar(10)
as
	begin
		delete users
		where taikhoan = @tk
	end



-------------THAO TÁC TRÊN BẢNG SÁCH, THỂ LOẠI, NHÀ XUẤT BẢN--------------------
go
create proc dsSach
as 
	begin
		SELECT  s.MaSach, s.TenSach, s.NamXB ,s.TinhTrang, s.TenTG, NXB.TenNXB, tl.TenTL
		FROM Sach s
		INNER JOIN NXB ON s.MaNXB = NXB.MaNXB
		INNER JOIN TheLoai tl ON s.MaTL = tl.MaTL;
	end
go 

create proc dsTheLoai
as
begin
	select * from theloai
end

go
create proc dsNXB
as 
	begin
		select * from NXB
	end


go
CREATE PROCEDURE ThemSach
	@MaSach varchar(10),
	@TenSach nvarchar(60),
	@NamXB int,
	@TenTG nvarchar(60),
	@MaTL varchar(10),
	@MaNXB varchar(10)
AS
BEGIN
	INSERT INTO Sach(MaSach, TenSach, NamXB, TenTG, MaTL, MaNXB)
	VALUES (@MaSach, @TenSach, @NamXB, @TenTG, @MaTL, @MaNXB)
END
go
create proc suaSach
	@MaSach varchar(10),
	@TenSach nvarchar(60),
	@NamXB int,
	@TenTG nvarchar(60),
	@MaTL varchar(10),
	@MaNXB varchar(10)
as
	begin
		update Sach
		set MaTL = @MaTL , MaNXB = @MaNXB, NamXB = @NamXB ,TenTG = @TenTG, TenSach = @TenSach
		where MaSach = @MaSach
	end
go

create proc xoaSach
	@MaS varchar(10)
as
	begin
		delete Sach
		where MaSach = @MaS
	end
go

CREATE PROCEDURE TimKiemSachTheoTen
    @TenSach nvarchar(60)
AS
BEGIN
    SELECT  s.MaSach, s.TenSach, s.NamXB ,s.TinhTrang, s.TenTG, NXB.TenNXB, tl.TenTL
    FROM Sach s
    INNER JOIN NXB ON s.MaNXB = NXB.MaNXB
    INNER JOIN TheLoai tl ON s.MaTL = tl.MaTL
    WHERE s.TenSach LIKE '%' + @TenSach + '%';
END
CREATE PROCEDURE sachCM
AS
BEGIN
    SELECT MaSach, TenSach, NamXB, TenTG, TinhTrang, MaTL, MaNXB
    FROM Sach
    WHERE TinhTrang = N'chưa mượn'
END


------THAO TÁC TRÊN BẢNG SINH VIÊN----------------------
go
create proc dsSV
as
begin
	select * from sinhvien
end

go
create proc themSV
	@masv varchar(10),
	@hoten nvarchar(50),
	@gt Nvarchar(10),
	@ns date
as
begin
	insert into SinhVien
	values(@masv, @hoten, @gt, @ns)
end
go
create proc suaSV
	@masv varchar(10),
	@hoten nvarchar(50),
	@gt nvarchar(10),
	@ns date
as
begin	
	update SinhVien
	set TenSV = @hoten, Nsinh = @ns, GioiTinh = @gt
	where MaSV = @masv
end

go
create proc xoaSV
	@masv varchar(10)
as
begin	
	delete SinhVien
	where masv = @masv
end


go 
create proc timSV
	@masv varchar(10)
as
begin	
	select * from sinhvien
	where masv = @masv
end

-------THAO TÁC TRÊN BẢNG PHIẾU THUÊ, CHI TIẾT PHIẾU THUÊ----------------------
go
create proc timSV_PT
	@masv varchar(10)
as 
begin 
	select MaSV, MaPT, SoLuong
	from PhieuThue 
	where MaSV = @masv
end

go
create proc dsPhieuThue
as 
begin
	select * from PhieuThue
end
go

CREATE PROCEDURE lapPT
    @MaSV varchar(10),
    @MaPT varchar(10)
AS
BEGIN
    -- Đếm số dòng trong CTPT
    DECLARE @SoDong int;
    SELECT @SoDong = COUNT(*)
    FROM CTPT
    WHERE MaSV = @MaSV AND MaPT = @MaPT;

    -- Kiểm tra xem số dòng có bằng 3 không
    IF @SoDong < 3
    BEGIN
        -- Thêm vào PhieuThue
        INSERT INTO PhieuThue(MaSV, MaPT)
        VALUES (@MaSV, @MaPT)

        -- Thêm vào CTPT
        INSERT INTO CTPT(MaPT, MaSV)
        VALUES (@MaPT, @MaSV),
               (@MaPT, @MaSV),
               (@MaPT, @MaSV)
    END
    ELSE
    BEGIN
        -- Nếu số dòng bằng 3, hiển thị thông báo lỗi
        RAISERROR ('Không thể tạo thêm vì số chi tiết trong phiếu thuê đã giới hạn', 16, 1);
    END
END;

go
create PROCEDURE sp_GetCTPTDetails
    @MaPT varchar(10),
    @MaSV varchar(10)
AS
BEGIN
    SELECT CTPT.MaPT, CTPT.MaSV, CTPT.MaSach, Sach.TenSach, CTPT.NgayMuon, CTPT.NgayTra, CTPT.MaCTPT
    FROM CTPT 
    left JOIN Sach ON CTPT.MaSach = Sach.MaSach
    WHERE CTPT.MaPT = @MaPT AND CTPT.MaSV = @MaSV
END

go
create proc taoCTPT
	@mapt varchar(10),
	@masv varchar(10),
	@mactpt int,
	@masach varchar(10),
	@ngaymuon date
as
begin 
	set dateformat DMY
	update CTPT
	set MaSach = @masach, NgayMuon = @ngaymuon
	where MaSV = @masv and MaPT = @mapt and MaCTPT = @mactpt

	update Sach
	set TinhTrang = N'Đang mượn'
	where MaSach = @masach

	exec UpdateSoLuong @mapt,@masv
end
go

CREATE PROCEDURE xoaCTPT
	@mapt varchar(10),
	@mactpt int,
	@masv varchar(10),
	@masach varchar(10)
AS
BEGIN 

	UPDATE Sach
	SET TinhTrang = N'Chưa mượn'
	WHERE MaSach = @masach


	UPDATE CTPT
	SET ctpt.MaSach = NULL, NgayMuon = NULL
	WHERE MaSV = @masv AND MaPT = @mapt AND MaCTPT = @mactpt


	exec UpdateSoLuong @mapt,@masv
END

go
CREATE PROCEDURE UpdateSoLuong
    @MaPT varchar(10),
    @MaSV varchar(10)
AS
BEGIN
    DECLARE @SoLuong int;

    SELECT @SoLuong = COUNT(*)
    FROM CTPT
    WHERE MaPT = @MaPT AND MaSV = @MaSV and NgayMuon != '';

    UPDATE PhieuThue
    SET SoLuong = @SoLuong
    WHERE MaPT = @MaPT AND MaSV = @MaSV;
END;

go

create PROCEDURE traSach
    @MaPT varchar(10),
    @MaCTPT int,
    @MaSV varchar(10),
    @MaSach varchar(10)
AS
BEGIN
	set dateformat DMY
    UPDATE CTPT
    SET NgayTra = GETDATE()
    WHERE MaPT = @MaPT AND MaCTPT = @MaCTPT AND MaSV = @MaSV;

    UPDATE Sach
    SET TinhTrang = N'Chưa mượn'
    WHERE MaSach = @MaSach;
END;
go