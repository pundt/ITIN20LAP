USE innovation4austria;
GO

ALTER TABLE [Users]
ADD
CONSTRAINT FK_User_Role
FOREIGN KEY (idRole)
REFERENCES Roles(id);
GO

ALTER TABLE [Users]
ADD
CONSTRAINT FK_User_Company
FOREIGN KEY (idCompany)
REFERENCES Companies(id);
GO

ALTER TABLE [Rooms]
ADD
CONSTRAINT FK_Room_Building
FOREIGN KEY (idBuilding)
REFERENCES Buildings(id);
GO

ALTER TABLE [RoomFacilities]
ADD
CONSTRAINT FK_RoomFacilities_Room
FOREIGN KEY (idRoom)
REFERENCES Rooms(id);
GO

ALTER TABLE [RoomFacilities]
ADD
CONSTRAINT FK_RoomFacilities_Facilities
FOREIGN KEY (idFacility)
REFERENCES Facilities(id);
GO

ALTER TABLE [Bookings]
ADD
CONSTRAINT FK_Bookings_Company
FOREIGN KEY (idCompany)
REFERENCES Companies(id);
GO


ALTER TABLE [Bookings]
ADD
CONSTRAINT FK_Bookings_Room
FOREIGN KEY (idRoom)
REFERENCES Rooms(id);
GO


ALTER TABLE [BookingDetails]
ADD
CONSTRAINT FK_BookingDetails_Booking
FOREIGN KEY (idBooking)
REFERENCES Bookings(id);
GO


ALTER TABLE [Bills]
ADD
CONSTRAINT FK_Bills_Companies
FOREIGN KEY (idCompany)
REFERENCES Companies(id);
GO

ALTER TABLE [BillDetails]
ADD
CONSTRAINT FK_BillDetails_BookingDetail
FOREIGN KEY (idBookingDetail)
REFERENCES BookingDetails(id);
GO

ALTER TABLE [BillDetails]
ADD
CONSTRAINT FK_BillDetails_Bill
FOREIGN KEY (idBill)
REFERENCES Bills(id);
GO


ALTER TABLE [Log]
ADD
CONSTRAINT FK_Log_User
FOREIGN KEY (idUser)
REFERENCES Users(id);
GO

