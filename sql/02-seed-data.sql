-- Demo data from the video.
-- Emma has one purchase, James has two, Liam has one. Sophia and Olivia have none.

INSERT INTO Users (Id, Name) VALUES
    (1, 'Emma'),
    (2, 'James'),
    (3, 'Sophia'),
    (4, 'Liam'),
    (5, 'Olivia');

INSERT INTO Purchases (Id, UserId, Product, Amount) VALUES
    (101, 2, 'Keyboard',   79.99),
    (102, 1, 'Mouse',      24.99),
    (103, 2, 'Monitor',   249.99),
    (104, 4, 'Headphones', 59.99);
