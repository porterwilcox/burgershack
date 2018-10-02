-- CREATE TABLE burgers (
--     id int NOT NULL AUTO_INCREMENT, 
--     name VARCHAR(50) NOT NULL,
--     description VARCHAR(255) NOT NULL,
--     price DECIMAL NOT NULL,
--     PRIMARY KEY(id)
-- );

-- INSERT INTO burgers (
--     name, 
--     description, 
--     price) 
--     VALUES (
--         "The Plain Jane",
--         "Cheeseburger",
--          4.99
--     );

-- SELECT * FROM burgers;

-- ALTER TABLE burgers MODIFY COLUMN price DECIMAL(10, 2);

-- UPDATE burgers SET price = 4.99 WHERE id = 1;

DELETE FROM burgers WHERE id = 1;