echo off 

set environment=test
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
REM call :docmddb "try creating indexes" 06_create_index.sql
REM call :docmddb "try creating checks" 07_create_check.sql
REM call :docmddb "try creating function" 08_create_function.sql
call :docmddb "try inserting values" 10_insert.sql
call :docmddb "try inserting users" 11_insertBenutzer.sql
call :docmddb "create login" 12_createLogin.sql
if %environment% == test call :docmddb "try inserting DEMO Values" T1_insertTestData.sql

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
