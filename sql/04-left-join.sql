-- LEFT JOIN: every user, with or without purchases (the query written in the video).
-- Result: 6 rows. Sophia and Olivia appear with NULL purchase columns.

SELECT
    u.Id AS UserId,
    u.Name,
    p.Id AS PurchaseId,
    p.Product,
    p.Amount
FROM Users AS u
LEFT JOIN Purchases AS p
    ON u.Id = p.UserId
ORDER BY u.Id;
