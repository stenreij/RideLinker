USE master;
GO

-- Controleer of de database al bestaat en verwijder deze indien aanwezig
IF DB_ID('RideLinker') IS NOT NULL
BEGIN
    ALTER DATABASE RideLinker
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RideLinker;
END
GO

-- Maak de nieuwe database aan
CREATE DATABASE RideLinker;
GO
