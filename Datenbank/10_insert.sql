USE innovation4austria;
GO

-- Roles
insert into Roles (roleName)
values ('innovation4austria')

insert into Roles (roleName)
values ('startup')

-- Companies
insert into Companies (companyName, street, number, city, zip)
values ('innovation4austria', 'Am Burgring', '27a', 'Wien', 'A-1010')

insert into Companies (companyName, street, number, city, zip)
values ('ACME', 'Musterstraße', '1a', 'Wien', 'A-1010')

-- Users
insert into Users (idRole, idCompany, username, [password], firstname, lastname)
values (1, 1, 'franz.pilgerstorfer@i4a.com', HASHBYTES('SHA2_256', '123user!'), 'Franz', 'Pilgerstorfer')

insert into Users (idRole, idCompany, username, [password], firstname, lastname)
values (2, 2, 'jon.doe@acme.com', HASHBYTES('SHA2_256', '123user!'), 'Jon', 'Doe')

-- Buildings
insert into Buildings (street, number, city, zip, buildingName, orderNumber)
values ('Industriestraße',  '29', '1230', 'Wien', 'A-1010', 1)

-- RoomTypes
insert into RoomType (roomtype) values ('Büro')
insert into RoomType (roomtype) values ('Seminar-Raum')
insert into RoomType (roomtype) values ('Konferenz-Raum')


-- Rooms
insert into Rooms(idRoomType, idBuilding, size,  pricePerDay, roomName, orderNumber)
values (1, 1, 45, 7.75, 'IA001 - Büro klein', 1)

insert into Rooms(idRoomType, idBuilding, size,  pricePerDay, roomName, orderNumber)
values (1, 1, 120, 7.2, 'IA002 - Büro mittel', 2)

insert into Rooms(idRoomType, idBuilding, size,  pricePerDay, roomName, orderNumber)
values (1, 1, 200, 6.5, 'IA003 - Büro groß', 3)

insert into Rooms(idRoomType, idBuilding, size,  pricePerDay, roomName, orderNumber)
values (2, 1, 35, 3.25, 'IA004 - Seminarraum', 4)

insert into Rooms(idRoomType, idBuilding, size,  pricePerDay, roomName, orderNumber)
values (3, 1, 20, 4.75, 'IA005 - Besprechungsraum', 5)

-- Facilities
insert into Facilities (facilityName, orderNumber)
values ('Computer', 1)

insert into Facilities (facilityName, orderNumber)
values ('Bestuhlung (Sessel)', 2)

insert into Facilities (facilityName, orderNumber)
values ('Bestuhlung (Couch)', 3)

insert into Facilities (facilityName, orderNumber)
values ('Tisch', 4)

insert into Facilities (facilityName, orderNumber)
values ('WLAN', 5)

insert into Facilities (facilityName, orderNumber)
values ('Klima-Anlage', 6)

insert into Facilities (facilityName, orderNumber)
values ('Beamer', 7)

-- RoomFacilities
insert into RoomFacilities (idRoom, idFacility, quantity)
values (1, 5, 1)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (1, 6, 1)

insert into RoomFacilities (idRoom, idFacility, quantity)
values (2, 5, 1)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (2, 6, 1)

insert into RoomFacilities (idRoom, idFacility, quantity)
values (3, 5, 1)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (3, 6, 1)

insert into RoomFacilities (idRoom, idFacility, quantity)
values (4, 1, 20)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (4, 2, 20)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (4, 4, 10)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (4, 7, 1)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (4, 6, 1)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (4, 5, 1)


insert into RoomFacilities (idRoom, idFacility, quantity)
values (5, 5, 5)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (5, 1, 1)
insert into RoomFacilities (idRoom, idFacility, quantity)
values (5, 6, 1)








