-- ⚠ SPOILER: solution to the challenge at the end of the video.
-- "Find every user who has no purchases" with LEFT JOIN + WHERE.
--
-- LEFT JOIN keeps every user; users without a match get NULL in the purchase columns.
-- WHERE p.Id IS NULL keeps only those rows. (Use IS NULL: "= NULL" is never true.)
-- Result: Sophia (3) and Olivia (5).

SELECT
    u.Id,
    u.Name
FROM Users AS u
LEFT JOIN Purchases AS p
    ON u.Id = p.UserId
WHERE p.Id IS NULL;
