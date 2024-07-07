use master 
go
create database DOAN_QUANLYTHUVIEN
go
use DOAN_QUANLYTHUVIEN

go
create table TheLoai
(
	MaTL varchar(10) not null,
	TenTL nvarchar(50) not null,
	constraint pk_tl primary key (MaTL)
)

create table NXB
(
	MaNXB varchar(10) not null,
	TenNXB nvarchar(60) not null, 
	constraint pk_nxb primary key (MaNXB)
)


create table Sach
(
	MaSach varchar(10) not null,
	TenSach nvarchar(60) not null,
	NamXB int not null,
	TenTG nvarchar(60) not null,
	TinhTrang nvarchar(50) not null,
	MaTL varchar(10) not null,
	MaNXB varchar(10) not null,
	constraint pk_sach primary key(MaSach),
	constraint fk_sach_tl foreign key (MaTL) references TheLoai(MaTL),
	constraint fk_sach_nxb foreign key (MaNXB) references NXB(MaNXB)
)



create table SinhVien
(
	MaSV varchar(10) not null,
	TenSV nvarchar(50) not null,
	GioiTinh nvarchar(40) not null,
	Nsinh date not null,
	constraint pk_sv primary key (MaSV),
)


create table PhieuThue
(
	MaPT varchar(10) not null,
	MaSV varchar(10) not null,
	SoLuong int,
	constraint pk_pt primary key (MaPT, MaSV),
	constraint fk_pt_sv foreign key (MaSV) references SinhVien(MaSV)
)


create table CTPT
(
	MaCTPT int IDENTITY(1,1)  not null,
	MaPT varchar(10) not null,
	MaSV varchar(10) not null,
	MaSach varchar(10),
	NgayMuon date,
	NgayTra date,
	constraint pk_ctpt primary key (MaPT,MaCTPT,MaSV),
	constraint fk_ctpt_sach foreign key (MaSach) references Sach(MaSach),
	constraint fk_ctpt_pt foreign key (MaPT,MaSV) references PhieuThue(MaPT,MaSV)
)



create table users
(
	taikhoan varchar(30) not null,
	matkhau nvarchar(200) not null,
	tenuser nvarchar(50) not null,
	mk_md5 varchar(200) not null,
	phone varchar(11),
	role varchar(6),
	constraint pk_us primary key(taikhoan)
)
go
set dateformat DMY


go
insert into TheLoai
values
	('TL01',N'Chính trị'),
	('TL02',N'Truyện ngắn'),
	('TL03',N'Marketing'),
	('TL04',N'Kĩ năng sống'),
	('TL05',N'Tiểu thuyết'),
	('TL06',N'Tâm lý'),
	('TL07', N'Khoa học'),
	('TL08', N'Tự truyện'),
	('TL09', N'Kinh tế'),
	('TL10', N'Nghệ thuật'),
	('TL11', N'Văn học'),
	('TL12', N'Lịch sử');
select * from TheLoai
go



go
insert into NXB
values
	('NXB01',N'NXB Kim Đồng'),
	('NXB02',N'NXB Lao Động'),
	('NXB03',N'NXB Trẻ'),
	('NXB04',N'NXB Tổng hợp TPHCM'),
	('NXB05',N'NXB Hội Nhà Văn'),
	('NXB06',N'NXB Thanh Hóa');
select * from CTPT
delete PhieuThue

go
insert into Sach
values
	('SA01',N'Người đua diều',2023,N'Khaled Hosseini',N'Chưa mượn','TL05','NXB05'),
	('SA02',N'Mắt Biếc',2019,N'Nguyễn Ngọc Ánh',N'Chưa mượn','TL05','NXB03'),
	('SA03',N'21 Kỹ Năng Trí Thông Minh Nội Tâm',1998,N'Cindy Wigglesworth',N'Chưa mượn','TL03','NXB01'),
	('SA04',N'Trưởng Thành Khi Yêu',2023,N'David Richo',N'Chưa mượn','TL04','NXB02'),
	('SA05',N'Cây Cam Ngọt Của Tôi',2020,N'José Mauro de Vasconcelos',N'Chưa mượn','TL05','NXB05'),
	('SA06', N'Đường Mây Qua Miền Mưa', 2022, N'Nguyễn Nhật Ánh', N'Chưa mượn', 'TL05', 'NXB04'),
	('SA07', N'Những Ngôi Sao Xanh', 2021, N'John Green', N'Chưa mượn', 'TL05', 'NXB03'),
	('SA08', N'Thế Giới Mới', 2023, N'Aldous Huxley', N'Chưa mượn', 'TL05', 'NXB02'),
	('SA09', N'Chiến Lược Đầu Tư Chứng Khoán', 2020, N'Benjamin Graham',N'Chưa mượn', 'TL03', 'NXB01'),
	('SA10', N'Điểm Dịch Chuyển', 2023, N'Malcolm Gladwell', N'Chưa mượn', 'TL03', 'NXB02'),
	('SA11', N'Vũ trụ trong vỏ hạt dẻ', 2023, N'Stephen Hawking', N'Chưa mượn', 'TL07', 'NXB01'),
	('SA12', N'Tôi, Malala', 2023, N'Malala Yousafzai', N'Chưa mượn', 'TL08', 'NXB02'),
	('SA13', N'Tư duy nhanh và chậm', 2023, N'Daniel Kahneman', N'Chưa mượn', 'TL09', 'NXB03'),
	('SA14', N'Nghệ thuật của sự hạnh phúc', 2023, N'Dalai Lama', N'Chưa mượn', 'TL10', 'NXB04'),
	('SA15', N'Người lớn', 2023, N'Nguyễn Nhật Ánh', N'Chưa mượn', 'TL11', 'NXB05'),
	('SA16', N'Lịch sử thế giới', 2023, N'H.G. Wells', N'Chưa mượn', 'TL12', 'NXB06');


delete Sach
select * from Sach

insert into SinhVien(MaSV, TenSV, GioiTinh, Nsinh)
values
	('SV01',N'Nguyễn Thanh Bình',N'Nam','19/10/2003'),
	('SV02',N'Nguyễn Hoàng Loan',N'Nữ','21/03/2003'),
	('SV03',N'Trần Hữu Long',N'Nam','13/05/2003'),
	('SV04',N'Lý Ngọc Hoa',N'Nữ','5/09/2002'),
	('SV05',N'Lê Thanh Tú',N'Nam','12/03/2002'),
	('SV06',N'Trần Khôi Nguyên',N'Nam','4/02/2001');
	select * from SinhVien
delete SinhVien
go

go
insert into PhieuThue
values
	('PT01','SV01',0),
	('PT01','SV02',0),
	('PT01','SV03',0),
	('PT02','SV01',0),
	('PT02','SV03',0),
	('PT02','SV02',0);
	select * from PhieuThue

delete CTPT
delete PhieuThue


insert into CTPT(MaPT,MaSV,MaSach)
values
go
select * from CTPT

	



insert into users
values 
	('admin1', '123456',N'Khánh Nam','e10adc3949ba59abbe56e057f20f883e', '0522807296','Admin'),
	('admin2', '123456',N'Đức Tài' ,'e10adc3949ba59abbe56e057f20f883e','0522807296','Admin'),
	('user1', '123456',N'Đức Thiện','e10adc3949ba59abbe56e057f20f883e','0522807296','User'),
	('user2', '123456',N'Tuấn Kiệt' ,'e10adc3949ba59abbe56e057f20f883e','0522807296','User')
	select * from users





---RBTV
ALTER TABLE Sach
ADD CONSTRAINT UC_TenSach UNIQUE (TenSach);

alter table sach
add constraint df_TinhTrang default N'Chưa mượn' for tinhtrang

alter table PhieuThue
add constraint chk_SLThue check(SoLuong <=3)

alter table PhieuThue
add constraint df_SoLuong default 0 for SoLuong


CREATE TRIGGER trg_CheckNgayMuon
ON CTPT
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE NgayMuon > GETDATE() OR NgayMuon > NgayTra)
    BEGIN
        RAISERROR ('NgayMuon phải nhỏ hơn ngày hiện tại và nhỏ hơn NgayTra', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END
END;