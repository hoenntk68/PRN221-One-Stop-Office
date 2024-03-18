-- Insert into [user]
INSERT INTO [User] ([user_id], full_name, dob, gender, address, phone_number, email)
VALUES
    ('123456789012', N'Nguyễn Thị Ánh', '1995-02-15', 0, N'123 Đường Láng, Hà Nội', '+123456789012', 'nguyenthianh@example.com'),
    ('234567890123', N'Trần Văn Bảo', '1987-08-20', 1, N'456 Đường Hồ Chí Minh, Hồ Chí Minh', '+234567890123', 'tranbao@example.com'),
    ('345678901234', N'Lê Thị Cẩm', '1990-12-25', 0, N'789 Đường Nguyễn Huệ, Đà Nẵng', '+345678901234', 'lecam@example.com'),
    ('456789012345', N'Phạm Đức Danh', '1983-07-10', 1, N'321 Đường Trần Phú, Huế', '+456789012345', 'phamdanh@example.com'),
    ('567890123456', N'Hồ Văn Dũng', '1998-05-30', 1, N'654 Đường Lê Lợi, Vũng Tàu', '+567890123456', 'hodung@example.com'),
    ('678901234567', N'Bùi Thị Hà', '1991-09-18', 0, N'789 Đường Phan Bội Châu, Hội An', '+678901234567', 'buiha@example.com'),
    ('789012345678', N'Vũ Đức Hoàng', '1980-04-22', 1, N'852 Đường Trần Hưng Đạo, Quảng Ngãi', '+789012345678', 'vudhoang@example.com'),
    ('890123456789', N'Lý Thị Kiều', '1989-11-05', 0, N'963 Đường Lý Tự Trọng, Cần Thơ', '+890123456789', 'lykieu@example.com'),
    ('901234567890', N'Hoàng Văn Lâm', '1994-03-12', 1, N'147 Đường Nguyễn Công Trứ, Biên Hòa', '+901234567890', 'hoanglam@example.com'),
    ('012345678901', N'Nguyễn Thị Mai', '1986-10-08', 0, N'258 Đường Nguyễn Đình Chiểu, Nha Trang', '+012345678901', 'nguyenmai@example.com'),
    ('123456789013', N'Trần Văn Nam', '1997-07-14', 1, N'369 Đường Nguyễn Thị Minh Khai, Buôn Ma Thuột', '+123456789012', 'trannam@example.com'),
    ('234567890124', N'Lê Thị Ngọc', '1984-06-29', 0, N'741 Đường Hoàng Diệu, Đà Lạt', '+234567890123', 'lengoc@example.com'),
    ('345678901235', N'Đặng Văn Phúc', '1992-02-17', 1, N'963 Đường Hùng Vương, Thanh Hóa', '+345678901234', 'dangphuc@example.com'),
    ('456789012346', N'Phan Thị Quỳnh', '1981-09-03', 0, N'753 Đường Phan Chu Trinh, Hà Tĩnh', '+456789012345', 'phanquynh@example.com'),
    ('567890123457', N'Lý Văn Sơn', '1996-11-28', 1, N'852 Đường Trần Hưng Đạo, Quảng Ngãi', '+567890123456', 'lyson@example.com'),
    ('678901234568', N'Nguyễn Thị Thanh', '1988-04-19', 0, N'963 Đường Lê Lợi, Vũng Tàu', '+678901234567', 'nguyenthanh@example.com'),
    ('789012345679', N'Bùi Văn Tú', '1983-01-22', 1, N'147 Đường Trần Hưng Đạo, Quảng Ngãi', '+789012345678', 'buitu@example.com'),
    ('890123456790', N'Vũ Thị Uyên', '1990-08-10', 0, N'258 Đường Lý Tự Trọng, Cần Thơ', '+890123456789', 'vuyen@example.com'),
    ('901234567891', N'Nguyễn Văn Vinh', '1985-03-27', 1, N'369 Đường Trần Phú, Huế', '+901234567890', 'nguyenvinh@example.com'),
    ('012345678902', N'Đỗ Thị Xinh', '1999-12-01', 0, N'741 Đường Lê Lai, Đà Nẵng', '+012345678901', 'doxinh@example.com'),
    ('123456789014', N'Lê Văn Y', '1987-06-08', 1, N'963 Đường Nguyễn Văn Linh, Hồ Chí Minh', '+123456789012', 'levany@example.com'),
    ('234567890125', N'Phạm Thị Zụ', '1992-05-23', 0, N'852 Đường Phan Chu Trinh, Hà Tĩnh', '+234567890123', 'phamzu@example.com'),
    ('345678901236', N'Trần Văn Ẩn', '1995-08-15', 1, N'963 Đường Trần Hưng Đạo, Quảng Ngãi', '+345678901234', 'trananh@example.com'),
    ('456789012347', N'Lương Thị Bắc', '1984-09-30', 0, N'753 Đường Lê Lợi, Vũng Tàu', '+456789012345', 'luongbac@example.com'),
    ('567890123458', N'Bùi Văn Bắc', '1993-04-24', 1, N'369 Đường Trần Phú, Huế', '+567890123456', 'buibac@example.com'),
    ('678901234569', N'Võ Thị Bình', '1981-07-11', 0, N'741 Đường Nguyễn Văn Linh, Hồ Chí Minh', '+678901234567', 'vobinh@example.com'),
    ('789012345680', N'Đỗ Văn Chính', '1998-09-07', 1, N'852 Đường Trần Hưng Đạo, Quảng Ngãi', '+789012345678', 'dochinh@example.com'),
    ('890123456791', N'Đinh Thị Chi', '1990-01-16', 0, N'147 Đường Nguyễn Công Trứ, Biên Hòa', '+890123456789', 'dinhchi@example.com'),
    ('901234567892', N'Trần Thị Cúc', '1985-03-20', 1, N'258 Đường Hoàng Diệu, Đà Lạt', '+901234567890', 'trancuc@example.com'),
    ('012345678903', N'Lê Văn Cương', '1992-11-05', 0, N'369 Đường Lê Lợi, Vũng Tàu', '+012345678901', 'lecuong@example.com'),
    ('123456789015', N'Lê Thị Dung', '1996-06-28', 1, N'963 Đường Phan Bội Châu, Hội An', '+123456789012', 'ledung@example.com'),
    ('234567890126', N'Phạm Văn Điệp', '1987-08-15', 0, N'852 Đường Nguyễn Đình Chiểu, Nha Trang', '+234567890123', 'phamdiep@example.com'),
    ('345678901237', N'Võ Thị Điệp', '1990-02-02', 1, N'741 Đường Lý Tự Trọng, Cần Thơ', '+345678901234', 'vodiep@example.com'),
    ('456789012348', N'Bùi Văn Độ', '1983-05-19', 0, N'753 Đường Lê Lợi, Vũng Tàu', '+456789012345', 'buido@example.com'),
    ('567890123459', N'Đỗ Thị Đông', '1998-09-03', 1, N'963 Đường Nguyễn Văn Linh, Hồ Chí Minh', '+567890123456', 'dodong@example.com'),
    ('678901234570', N'Hoàng Văn Dũng', '1984-12-20', 0, N'147 Đường Hoàng Diệu, Đà Lạt', '+678901234567', 'hoangdung@example.com'),
    ('789012345681', N'Lý Thị Dung', '1989-02-09', 1, N'258 Đường Lê Lợi, Vũng Tàu', '+789012345678', 'lydung@example.com'),
    ('890123456792', N'Nguyễn Thị Dung', '1994-11-01', 0, N'369 Đường Phan Bội Châu, Hội An', '+890123456789', 'nguyendung@example.com'),
    ('901234567893', N'Trần Văn Dũng', '1986-03-25', 1, N'741 Đường Trần Hưng Đạo, Quảng Ngãi', '+901234567890', 'trandung@example.com'),
    ('012345678904', N'Phạm Thị Dung', '1999-07-14', 0, N'852 Đường Lê Lợi, Vũng Tàu', '+012345678901', 'phamdung@example.com'),
    ('123456789016', N'Võ Văn Đạt', '1988-06-29', 1, N'963 Đường Phan Chu Trinh, Hà Tĩnh', '+123456789012', 'vodat@example.com'),
    ('234567890127', N'Võ Thị Điệp', '1991-04-15', 0, N'147 Đường Nguyễn Công Trứ, Biên Hòa', '+234567890123', 'vodiep@example.com'),
    ('345678901238', N'Đặng Thị Điệp', '1982-11-12', 1, N'258 Đường Nguyễn Đình Chiểu, Nha Trang', '+345678901234', 'dangdiep@example.com'),
    ('456789012349', N'Hoàng Văn Đông', '1997-08-30', 0, N'369 Đường Nguyễn Thị Minh Khai, Buôn Ma Thuột', '+456789012345', 'hoangdong@example.com'),
    ('567890123460', N'Nguyễn Thị Đông', '1983-10-24', 1, N'741 Đường Hoàng Diệu, Đà Lạt', '+567890123456', 'nguyendong@example.com'),
	-- staff 
	('111111111111', N'Triệu Thạch Ân', '2003-02-05', 0, N'852 Đường Lê Lợi, Vũng Tàu', '+012345678901', 'antt@example.com'),
    ('222222222222', N'Khiếu Minh Đức', '2003-07-25', 1, N'963 Đường Phan Chu Trinh, Hà Tĩnh', '+012345678901', 'duckm@example.com'),
    ('333333333333', N'Phạm Trường Giang', '2003-06-18', 0, N'147 Đường Nguyễn Công Trứ, Biên Hòa', '+012345678901', 'giangpt@example.com'),
    ('444444444444', N'Nguyễn Thị Khánh Huyền', '2003-08-06', 1, N'258 Đường Nguyễn Đình Chiểu, Nha Trang', '+012345678901', 'huyenntk@example.com'),
    ('555555555555', N'Hoàng Mai Phương', '2003-01-29', 0, N'369 Đường Nguyễn Thị Minh Khai, Buôn Ma Thuột', '+012345678901', 'phuonghm@example.com'),
	-- super admin
    ('000000000000', N'Khoa Thị Văn Học', '2003-07-21', 1, N'Km29, Đại lộ Thăng Long, Thạch Thất, Hà Nội', '+567890123456', 'eikh@example.com');

-- ALTER TABLE [User] ADD [password] varchar(50); 
UPDATE [User] SET [password] = '1';

INSERT INTO Staff([user_id], is_super_admin) VALUES
	('111111111111', 0),
	('222222222222', 0),
	('333333333333', 0),
	('444444444444', 0),
	('555555555555', 0),
	('000000000000', 1);


INSERT INTO Category(category_name, description)
VALUES
    (N'Hồ sơ lĩnh vực đất đai', N''),
    (N'Hồ sơ lĩnh vực tài nguyên nước', N''),
    (N'Hồ sơ lĩnh vực địa chất và khoáng sản', N''),
    (N'Hồ sơ lĩnh vực môi trường', N''),
    (N'Hồ sơ lĩnh vực khí tượng thủy văn', N''),
    (N'Hồ sơ lĩnh vực đo đạc, bản đồ và thông tin địa lý', N''),
    (N'Hồ sơ lĩnh vực biển và hải đảo', N''),
    (N'Hồ sơ lĩnh vực biến đổi khí hậu', N''),
    (N'Hồ sơ lĩnh vực viễn thám', N''),
    (N'Hồ sơ khai thác và sử dụng thông tin, dữ liệu tài nguyên và môi trường', N'');


INSERT INTO Staff_Category(staff_id, category_id)
VALUES 
	(1,1),
	(1,2),
	(2,3),
	(2,4),
	(3,5),
	(3,6),
	(4,7),
	(4,8),
	(5,9),
	(5,10),
	(6,1),
	(6,2),
	(6,3),
	(6,4),
	(6,5),
	(6,6),
	(6,7),
	(6,8),
	(6,9),
	(6,10);

-- select * from staff;
-- select * from [user];
-- select * from category;
-- select * from staff_category;
-- select * from request;

-- delete from [user];
-- delete from [staff];
-- delete from [category];
-- delete from [staff_category];
-- delete from [request];
