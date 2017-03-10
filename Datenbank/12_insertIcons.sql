use innovation4austria
go

update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/computer.png', Single_Blob) as img) where id=1
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/chair.png', Single_Blob) as img) where id=2
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/couch.png', Single_Blob) as img) where id=3
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/desk.png', Single_Blob) as img) where id=4
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/wlan.png', Single_Blob) as img) where id=5
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/aircondition.png', Single_Blob) as img) where id=6
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/beamer.png', Single_Blob) as img) where id=7
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/computer_active.png', Single_Blob) as img) where id=1
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/chair_active.png', Single_Blob) as img) where id=2
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/couch_active.png', Single_Blob) as img) where id=3
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/desk_active.png', Single_Blob) as img) where id=4
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/wlan_active.png', Single_Blob) as img) where id=5
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/aircondition_active.png', Single_Blob) as img) where id=6
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'D:/Arbeit/Kunden - aktiv/BBRZ/ITCC/LAP Informatiker/ITIN20 - 2016/Datenbank/Icons/beamer_active.png', Single_Blob) as img) where id=7

select * from Facilities
