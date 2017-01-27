use innovation4austria

If not Exists (select * from master.dbo.syslogins 
    where name = 'innovation4austria' )
Begin
	CREATE LOGIN i4a_User WITH PASSWORD = '123user!';
end
go

use innovation4austria
create user i4a_User for login i4a_User
EXEC sp_addrolemember N'db_owner', N'innovation4austria'
go
