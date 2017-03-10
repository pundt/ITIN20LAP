echo off 

set logfile=create_Innovation4Austria.log
set servername=.\SQLEXPRESS
set database=Anlaufstelle
set username=sa
set passwd=123user!

echo > %logfile%
call :showmsg .
call :showmsg "starting database creation %TIME%"
call :showmsg .

call :docmd "try dropping database" 01_drop_db.sql
call :docmd "try creating database" 02_create_db.sql
call :docmddb "try creating tables" 03_create_table.sql
call :docmddb "try creating primary keys" 04_create_pk.sql
call :docmddb "try creating foreign keys" 05_create_fk.sql
call :docmddb "try inserting values" 10_insert.sql
call :docmddb "create login" 11_createLogin.sql
call :docmddb "try inserting icons" 12_insertIcons.sql


call :showmsg .
call :showmsg "stopping database creation %TIME%"
call :showmsg .
pause

goto :eof

:docmd
	call :showmsg %1
	sqlcmd -S %servername% -U %username% -P %passwd% -d master -i %2 >> %logfile%
	echo. >> %logfile%
	echo. >> %logfile%
	goto :eof
	
:docmddb
	call :showmsg %1
	sqlcmd -S %servername% -U %username% -P %passwd% -d %database% -i %2 >> %logfile%
	echo. >> %logfile%
	echo. >> %logfile%
	goto :eof

:showmsg
	echo %1
	echo %1 >> %logfile%

:eof
