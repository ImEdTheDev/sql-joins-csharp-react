-- Tables used in the video. Run against an empty database (SQL Server).
-- The API can also create them for you on first run (EnsureCreated).

CREATE TABLE Users (
    Id   INT          NOT NULL PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Purchases (
    Id      INT            NOT NULL PRIMARY KEY,
    UserId  INT            NOT NULL REFERENCES Users(Id),
    Product NVARCHAR(50)   NOT NULL,
    Amount  DECIMAL(10, 2) NOT NULL
);
