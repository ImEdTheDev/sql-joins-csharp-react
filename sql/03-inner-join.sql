-- INNER JOIN: only rows that match on both sides.
-- Result: 4 rows. Sophia and Olivia are not in the result.

SELECT
    u.Name,
    p.Product,
    p.Amount
FROM Users AS u
INNER JOIN Purchases AS p
    ON u.Id = p.UserId
ORDER BY u.Id, p.Id;
