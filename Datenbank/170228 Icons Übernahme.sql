use innovation4austria
go

update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/computer.png', Single_Blob) as img) where id=1
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/chair.png', Single_Blob) as img) where id=2
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/couch.png', Single_Blob) as img) where id=3
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/desk.png', Single_Blob) as img) where id=4
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/wlan.png', Single_Blob) as img) where id=5
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/aircondition.png', Single_Blob) as img) where id=6
update Facilities set facilityImage = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/beamer.png', Single_Blob) as img) where id=7
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/computer_active.png', Single_Blob) as img) where id=1
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/chair_active.png', Single_Blob) as img) where id=2
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/couch_active.png', Single_Blob) as img) where id=3
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/desk_active.png', Single_Blob) as img) where id=4
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/wlan_active.png', Single_Blob) as img) where id=5
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/aircondition_active.png', Single_Blob) as img) where id=6
update Facilities set facilityImageActive = (SELECT BulkColumn FROM Openrowset( Bulk 'd:/temp/beamer_active.png', Single_Blob) as img) where id=7

select * from Facilities
