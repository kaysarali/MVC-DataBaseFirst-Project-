DROP TABLE Purchase
DROP TABLE Sales
DROP TABLE Supplier
DROP TABLE Customar
DROP TABLE ProductModel
DROP TABLE Product
DROP TABLE Attendance
DROP TABLE Employee
DROP TABLE Roletb


----- **********************************************************************
--                                Create Table
----- **********************************************************************
-- AspProjectDB2


create table Roletb (
	RoletbId int primary key identity(1,1) not null,
	RollName varchar (50) not null
);
go


create table Employee (
	EmployeeId int primary key identity(1001,1) not null,
	EmpName varchar (40) not null,
	EmpUserName varchar (40) not null,
	EmpAddress varchar (max) not null,
	DateOfBirth date not null,

	EmpEmail varchar (40) not null,
	EmpPassword varchar (max) not null,
	ImgPath varchar (max) null,
	IsActive bit not null,

	LoginTime time null,
	LogoutTime time null,
	RoletbId int References Roletb (RoletbId) ON DELETE CASCADE ON UPDATE CASCADE not null
);
go


create table Attendance (
	AttendanceId int primary key identity(1,1) not null,
	AttDate date not null,
	LoginTime time null,
	LogoutTime time null,
	Late time null,
	OverTime time null,
	PartTime time null,
	EmployeeId int References Employee (EmployeeId) ON DELETE CASCADE ON UPDATE CASCADE not null
);
go


----------------------------------------

create table Product (
	ProductId int primary key identity(51,1) not null,
	ProductName varchar (30) not null,
);
go

create table ProductModel (
	ProductModelId int primary key identity(51,1) not null,
	ModelName varchar (30) not null,
	ProductId int References Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE not null,
);
go
----------------------------------------

create table Customar (
	CustomarId int primary key identity(501,1) not null,
	CustomarName varchar (30) not null,
	CustAddress varchar (50) not null,
	CustMobile varchar (20) not null,
);


create table Sales (
	SalesId int primary key identity(201,1) not null,
	MemoNo int not null,
	SalesDate datetime not null,
	Quantity int not null,
	UnitPrice decimal(16,2) not null,

	ProductModelId int References ProductModel (ProductModelId) ON DELETE CASCADE ON UPDATE CASCADE not null,
	CustomarId int References Customar (CustomarId) ON DELETE CASCADE ON UPDATE CASCADE not null,
	EmployeeId int References Employee (EmployeeId) ON DELETE CASCADE ON UPDATE CASCADE not null,
);
go


create table Supplier (
	SupplierId int primary key identity(11,1) not null,
	SupplierName varchar (50) not null,
);
go


create table Purchase (
	PurchaseId int primary key identity(101,1) not null,
	VoucherNo int not null,
	PurchaseDate datetime not null,
	Quantity int not null,
	TotalPrice decimal not null,
	ImgPath varchar (max) null,
	IsActive bit not null,
	ProductModelId int References ProductModel (ProductModelId) ON DELETE CASCADE ON UPDATE CASCADE not null,
	SupplierId int References Supplier (SupplierId) ON DELETE CASCADE ON UPDATE CASCADE not null,
	EmployeeId int References Employee (EmployeeId) ON DELETE CASCADE ON UPDATE CASCADE not null,
	
);
go