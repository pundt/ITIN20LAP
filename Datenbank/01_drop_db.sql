USE master;
GO

IF NOT DB_ID('innovation4austria') IS NULL ALTER DATABASE innovation4austria SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
IF NOT DB_ID('innovation4austria') IS NULL DROP DATABASE innovation4austria;
GO