USE innovation4austria;
GO

-- innovation4austria entities
CREATE TABLE [Users] (
	id INT IDENTITY NOT NULL,
	idRole int not null,
	idCompany int not null,
	username NVARCHAR(100) NOT NULL,
	[password] VARBINARY(1000) NOT NULL,
	firstname NVARCHAR(50) not NULL,
	lastname NVARCHAR(50) not NULL,
	active bit default 1 not null
);

CREATE TABLE [Roles] (
	id INT IDENTITY NOT NULL,
	rolename NVARCHAR(100) NOT NULL,
	active bit default 1 not null
);

create table [Companies](
	id int identity not null,
	companyName nvarchar(255) not null,
	street NVARCHAR(100) not NULL,
	number NVARCHAR(100) not NULL,
	city NVARCHAR(100) not NULL,
	zip NVARCHAR(100) not NULL
);

CREATE TABLE [Buildings] (
	id INT IDENTITY NOT NULL,
	street NVARCHAR(100) NULL,
	number NVARCHAR(100) NULL,
	city NVARCHAR(100) NULL,
	zip NVARCHAR(100) NULL,
	buildingName NVARCHAR(100) NOT NULL,
	orderNumber int null,
	active bit default 1 not null
);

CREATE TABLE [Rooms] (
	id INT IDENTITY NOT NULL,
	idBuilding int not null,
	size decimal not null,
	pricePerDay decimal not null,
	roomName NVARCHAR(100) NOT NULL,
	orderNumber int null,
	active bit default 1 not null
);

CREATE TABLE [Facilities] (
	id INT IDENTITY NOT NULL,
	facilityName NVARCHAR(100) NOT NULL,
	orderNumber int null,
	active bit default 1 not null
);

CREATE TABLE [RoomFacilities] (
	idRoom INT NOT NULL,
	idFacility INT NOT NULL,
	quantity int not null
);

create table [Bookings] (
	id INT IDENTITY NOT NULL,
	idCompany int not null,
	idRoom int not null,
	active bit default 1 not null
);

create table [BookingDetails] (
	id INT IDENTITY NOT NULL,
	idBooking int not null,
	[date] smalldatetime not null,
	price decimal not null
);

create table [Bills] (
	id INT IDENTITY NOT NULL,
	[createDate] smalldatetime not null default getdate(),
	[fromDate] smalldatetime not null, -- first day of month
	[toDate] smalldatetime not null, -- last day of month
	idCompany int not null
);

create table [BillDetails] (
	idBookingDetail int not null,
	idBill int not null
);


-- log4net Table
CREATE TABLE [Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL,
	[idUser] int null
)

GO