/*
Database: UsersCRUDNET
Description: Users CRUD y Login
Author: Marvin Alexander Gamez Lozano */

IF DB_ID('UsersCRUDNET') IS NULL
    CREATE DATABASE UsersCRUDNET;
GO

USE UsersCRUDNET;
GO

-- Table
IF OBJECT_ID('dbo.Users','U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        UserId INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Email NVARCHAR(120) NOT NULL,
        PasswordHash CHAR(64) NOT NULL,
        Role INT NOT NULL,          -- 0 = Admin, 1 = Standard
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
    );
END

GO
-- Usuario administrador por defecto
INSERT INTO Usuarios (FullName, Username, Email, PasswordHash, Role, IsActive)
VALUES (
    'Administrador del Sistema',
    'admin',
    'admin2026@gmail.com',
    'f3f3b01b1c0a43d9b8a4c6c65b0e3d7dfc9a58a2f5e6e3f5e9c1c4f5a7e3b1a9',
    0,  -- 0 = Admin
    1   -- Activo
);

-- Read
CREATE OR ALTER PROCEDURE spUsers_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT UserId, FullName, Username, Email, Role, IsActive
    FROM Users
    ORDER BY FullName;
END
GO

-- Create
CREATE OR ALTER PROCEDURE spUsers_Insert
    @FullName NVARCHAR(100),
    @Username NVARCHAR(50),
    @Email NVARCHAR(120),
    @PasswordHash CHAR(64),
    @Role INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
    BEGIN
        RAISERROR('Username already exists.',16,1)
        RETURN
    END

    INSERT INTO Users(FullName,Username,Email,PasswordHash,Role,IsActive)
    VALUES(@FullName,@Username,@Email,@PasswordHash,@Role,@IsActive)
END
GO

-- Update 
CREATE OR ALTER PROCEDURE spUsers_Update
    @UserId INT,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(120),
    @Role INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Users
    SET FullName=@FullName,
        Email=@Email,
        Role=@Role,
        IsActive=@IsActive
    WHERE UserId=@UserId;
END
GO

-- Delete
CREATE OR ALTER PROCEDURE spUsers_Delete
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Users WHERE UserId=@UserId;
END
GO

-- Login
CREATE OR ALTER PROCEDURE spUsers_Login
    @Username NVARCHAR(50),
    @PasswordHash CHAR(64)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT UserId, FullName, Role, IsActive
    FROM Users
    WHERE Username=@Username AND PasswordHash=@PasswordHash AND IsActive=1;
END
GO
