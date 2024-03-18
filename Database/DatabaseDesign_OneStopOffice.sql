CREATE DATABASE PRN221_OneStopOffice;

-- drop database if exists PRN221_OneStopOffice;

-- use master;

-- Switch to the newly created database context
USE PRN221_OneStopOffice;

-- Create the 'role' table
/*
CREATE TABLE [role] (
    role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name NVARCHAR(500) NOT NULL
);
*/
-- Create the 'user' table
CREATE TABLE [User] (
    [user_id] VARCHAR(50) PRIMARY KEY,
    full_name NVARCHAR(100) NOT NULL,
    dob DATETIME,
    gender BIT,
    [address] NVARCHAR(500),
    phone_number VARCHAR(15),
    email VARCHAR(255),
	token VARCHAR(1000),
	is_token_valid BIT,
	[password] varchar(50) 
);

-- Create the 'staff' table
CREATE TABLE [Staff] (
    staff_id INT PRIMARY KEY IDENTITY(1,1),
    [user_id] VARCHAR(50) NOT NULL,
    is_super_admin BIT NOT NULL,
    FOREIGN KEY ([user_id]) REFERENCES [user]([user_id])
);

-- Create the 'category' table
CREATE TABLE Category (
    category_id INT PRIMARY KEY IDENTITY(1,1),
    category_name NVARCHAR(100) NOT NULL,
    description NVARCHAR(500)
);

-- Create the 'category_staff' table
CREATE TABLE Staff_Category (
    staff_id INT,
    category_id INT,
    FOREIGN KEY (category_id) REFERENCES category(category_id),
	FOREIGN KEY (staff_id) REFERENCES staff(staff_id),
	PRIMARY KEY (staff_id, category_id)
);

-- Create the 'request' table
CREATE TABLE Request (
    request_id INT PRIMARY KEY IDENTITY(1,1),
    [user_id] VARCHAR(50),
    category_id INT,
    reason TEXT,
    attachment TEXT,
    FOREIGN KEY ([user_id]) REFERENCES [user]([user_id]),
    FOREIGN KEY (category_id) REFERENCES category(category_id)
);

/*
IF OBJECT_ID('[request]', 'U') IS NOT NULL DROP TABLE [request];
IF OBJECT_ID('[staff_category]', 'U') IS NOT NULL DROP TABLE [staff_category];
IF OBJECT_ID('[category]', 'U') IS NOT NULL DROP TABLE [category];
IF OBJECT_ID('[staff]', 'U') IS NOT NULL DROP TABLE [staff];
IF OBJECT_ID('[user]', 'U') IS NOT NULL DROP TABLE [user];
IF OBJECT_ID('[role]', 'U') IS NOT NULL DROP TABLE [role];
*/


-- SELECT * FROM [role];
SELECT * FROM [User];
SELECT * FROM [Staff];
SELECT * FROM [Category]
SELECT * FROM [Staff_Category];
SELECT * FROM [Request];
