Create Database MovieDB;

CREATE TABLE Movies (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(255) NOT NULL,
    CoverImg NVARCHAR(500) NULL,
    ReleaseDate DATETIME NOT NULL,
    Genre NVARCHAR(100) NOT NULL,
    Duration INT NOT NULL,
    CreateDate DATETIME NULL,
    ModifyDate DATETIME NULL
);
